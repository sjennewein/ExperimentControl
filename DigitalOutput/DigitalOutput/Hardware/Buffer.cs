using System;
using System.Threading;
using DigitalOutput.Model;
using NationalInstruments.DAQmx;
using fastJSON;
using Timing = DigitalOutput.Model.Timing;

namespace DigitalOutput.Hardware
{
    public class Buffer
    {
        private readonly ManualResetEvent _nextRunGate = new ManualResetEvent(true);
        private ModelCard _data;
        private Thread _myWorker;
        private UInt32[] _outputSequence;
        private bool _run = true;
        private bool _running;
        private string _serializedData;
        private bool _updated;
        private volatile bool _waitingForNextRun = false;

        public void UpdateData(string newData)
        {
            _serializedData = newData;
            _updated = true;
        }

        public void WorkingLoop()
        {
            TriggerEvent(Started);
            _running = true;
            while (_run)
            {
                
                
                if (_updated)
                {
                    _data = (ModelCard) JSON.Instance.ToObject(_serializedData);
                    _outputSequence = Translator.GenerateOutput(_data);
                    _updated = false;
                }

                //initialize output card
                var digitalOutputTask = new Task("PCI6534");
                digitalOutputTask.DOChannels.CreateChannel("/Dev1/port0_32", "",
                                                           ChannelLineGrouping.OneChannelForAllLines);

                double sampleRate = _data.SampleRate.GetFrequency(Timing.Frequency.Hz);

                digitalOutputTask.Timing.ConfigureSampleClock("", sampleRate,
                                                              SampleClockActiveEdge.Rising,
                                                              SampleQuantityMode.FiniteSamples, _outputSequence.Length);
                digitalOutputTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger("/Dev1/PFI6",
                                                                                    DigitalEdgeStartTriggerEdge.Rising);

                //write output to card
                var writer = new DigitalSingleChannelWriter(digitalOutputTask.Stream);
                writer.WriteMultiSamplePort(false, _outputSequence);

                if(_waitingForNextRun)
                    TriggerEvent(ReadyForNextRun);

                _nextRunGate.WaitOne();
                Console.WriteLine("starting next run: " + DateTime.Now);

                //start and wait until everything is done
                digitalOutputTask.Start();
                digitalOutputTask.WaitUntilDone(60000);

                //free hardware
                digitalOutputTask.Stop();
                digitalOutputTask.Dispose();

                TriggerEvent(CycleDone);                
            }
            TriggerEvent(Stopped);
            _running = false;
        }

        public void Pause()
        {
            _nextRunGate.Reset();
            _waitingForNextRun = true;
        }

        public void Resume()
        {
            _nextRunGate.Set();
            _waitingForNextRun = false;
        }

        public void Start(string data)
        {
            if (!_running)
            {
                _serializedData = data;
                _updated = true;
                _myWorker = new Thread(WorkingLoop);
                _myWorker.Start();
            }
            _run = true;
        }

        public void Stop()
        {
            _run = false;
            _nextRunGate.Set();
        }

        private void TriggerEvent(EventHandler newEvent, EventArgs e = null)
        {
            EventHandler triggerEvent = newEvent;
            if (triggerEvent != null)
                triggerEvent(this, new EventArgs());
        }

        public event EventHandler Started;
        public event EventHandler Stopped;
        public event EventHandler CycleDone;
        public event EventHandler ReadyForNextRun;
    }
}
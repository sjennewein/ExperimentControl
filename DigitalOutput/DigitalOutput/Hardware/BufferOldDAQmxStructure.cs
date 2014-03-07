using System;
using System.Diagnostics;
using System.Threading;
using DigitalOutput.Model;
using NationalInstruments.DAQmx;
using fastJSON;

namespace DigitalOutput.Hardware
{
    public class Buffer
    {
        private readonly ManualResetEvent _signal = new ManualResetEvent(true);
        private ModelCard _data;
        private Thread _myWorker;
        private UInt32[] _outputSequence;
        private bool _networkMode;
        private bool _run;
        private bool _running;
        private string _serializedData;
        private bool _updated;
        private bool _newRun = false;
        private int _cyclesPerRun;
        private int _cycleCounter;
        //public int CyclesPerRun { set { _cyclesPerRun = value; } }
        

        public void UpdateData(string newData)
        {
            _serializedData = newData;
            _updated = true;
        }

        private void WorkingLoop()
        {
            TriggerEvent(Started);
            _running = true;
            _cycleCounter = 0;
            //var digitalOutputTask = new Task("PCI6534");
            while (_run)
            {
                _signal.WaitOne();

                if (_updated)
                {                    
                    digitalOutputTask.Dispose();
                    digitalOutputTask = new Task("PCI6534");

                    _data = (ModelCard) JSON.Instance.ToObject(_serializedData);
                    _outputSequence = Translator.GenerateOutput(_data);
                    _updated = false;

                    //initialize output card
                    sw.Start();

                    digitalOutputTask.DOChannels.CreateChannel("/Dev1/port0_32", "",
                                                               ChannelLineGrouping.OneChannelForAllLines);

                    double sampleRate = 10000000;

                    digitalOutputTask.Timing.ConfigureSampleClock("", sampleRate,
                                                                  SampleClockActiveEdge.Rising,
                                                                  SampleQuantityMode.FiniteSamples, _outputSequence.Length);
                    digitalOutputTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger("/Dev1/PFI6",
                                                                                        DigitalEdgeStartTriggerEdge.Rising);

                    //write output to card
                    var writer = new DigitalSingleChannelWriter(digitalOutputTask.Stream);
                    writer.WriteMultiSamplePort(false, _outputSequence);

                    
                }

                
                
                if (_newRun)
                    TriggerEvent(DataProcessed);
                                               
                _signal.WaitOne();

              

                //start and wait until everything is done
                digitalOutputTask.Start();
                
                if (_newRun)
                {
                    _newRun = false;
                    TriggerEvent(RunLaunched);
                }

                digitalOutputTask.WaitUntilDone(30000000);


                //free hardware
                digitalOutputTask.Stop();
          

                
                _cycleCounter++;                
                
                TriggerEvent(CycleDone);
                
                if (_cyclesPerRun > 0 && _cycleCounter >= _cyclesPerRun)
                {
                    Pause();
                    TriggerEvent(RunDone);
                }

                if (_newRun)
                {
                    _signal.WaitOne();
                    _signal.Reset();
                }

            }
            digitalOutputTask.Dispose();
            TriggerEvent(Stopped);
            _running = false;
        }

        public void Pause()
        {           
            _signal.Reset();            
        }

        public void Resume()
        {
            _cycleCounter = 0;
            _signal.Set();
            Console.WriteLine("Hardware resumed");
        }

        public void Start(bool networkMode, string data, int cyclesPerRun = 0)
        {
            if (!_running)
            {
                _cyclesPerRun = cyclesPerRun;                
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
            _signal.Set();
        }

        private void TriggerEvent(EventHandler newEvent, EventArgs e = null)
        {
            EventHandler triggerEvent = newEvent;
            if (triggerEvent != null)
                triggerEvent(this, new EventArgs());
        }

        public event EventHandler Started;
        public event EventHandler Stopped;
        public event EventHandler CycleFinished;
        public event EventHandler RunStarted;
        public event EventHandler RunFinished;
    }
}
using System;
using System.Diagnostics;
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
        private volatile bool _newRun = false;
        private volatile int _cyclesPerRun;
        private volatile int _cycleCounter;
        public int CyclesPerRun { set { _cyclesPerRun = value; } }
        private Stopwatch sw = new Stopwatch();

        public void UpdateData(string newData)
        {
            _serializedData = newData;
            _updated = true;
        }

        public void WorkingLoop()
        {
            TriggerEvent(Started);
            _running = true;
            var digitalOutputTask = new Task("PCI6534");
            while (_run)
            {                                
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

                    double sampleRate = _data.SampleRate.GetFrequency(Timing.Frequency.Hz);

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
                                               
                _nextRunGate.WaitOne();

                

                Console.WriteLine("starting next run: " + DateTime.UtcNow.ToString("HH:mm:ss.ffffff"));

                //start and wait until everything is done
                digitalOutputTask.Start();
                if (_newRun)
                {
                    _newRun = false;
                    TriggerEvent(RunLaunched);
                }
                sw.Stop();
                Console.WriteLine(sw.ElapsedMilliseconds);
                sw.Reset();
                digitalOutputTask.WaitUntilDone(3000000);

                //free hardware
                digitalOutputTask.Stop();
                

                Console.WriteLine("run finished: " + DateTime.UtcNow.ToString("HH:mm:ss.ffffff"));
                _cycleCounter++;                
                Console.WriteLine(_cycleCounter);
                TriggerEvent(CycleDone);
                
                if (_cyclesPerRun > 0 && _cycleCounter >= _cyclesPerRun)
                {
                    Pause();
                    TriggerEvent(RunDone);
                }

                if (_newRun)
                {
                    _nextRunGate.WaitOne();
                    _nextRunGate.Reset();
                }
            }
            digitalOutputTask.Dispose();
            TriggerEvent(Stopped);
            _running = false;
        }

        public void Pause()
        {
            Console.WriteLine("pausing hardware : " + DateTime.UtcNow.ToString("HH:mm:ss.ffffff"));
            _nextRunGate.Reset();
            _newRun = true;
        }

        public void ProcessNewData()
        {
            _nextRunGate.Set();
        }

        public void StartNextRun()
        {
            _cycleCounter = 0;
            _nextRunGate.Set();            
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
        public event EventHandler RunLaunched;
        public event EventHandler RunDone;
        public event EventHandler DataProcessed;
    }
}
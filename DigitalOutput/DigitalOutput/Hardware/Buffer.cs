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
        private bool _newRun;
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
            while (_run)
            {
                _signal.WaitOne();

                if (_updated)
                {
                    _data = (ModelCard) JSON.Instance.ToObject(_serializedData);
                    _outputSequence = Translator.GenerateOutput(_data);
                    _updated = false;
                }

                using (var myTask = new Task("PCI6534"))
                {
                    myTask.DOChannels.CreateChannel("/Dev1/port0_32", "", ChannelLineGrouping.OneChannelForAllLines);
                    myTask.Timing.ConfigureSampleClock("",10000000,SampleClockActiveEdge.Rising,SampleQuantityMode.FiniteSamples,_outputSequence.Length);
                    myTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger("/Dev1/PFI6",DigitalEdgeStartTriggerEdge.Rising);

                    var writer = new DigitalSingleChannelWriter(myTask.Stream);
                    writer.WriteMultiSamplePort(false, _outputSequence);

                    myTask.Start();

                    if (_newRun)
                        TriggerEvent(RunStarted);

                    _newRun = false;
                    try
                    {
                        myTask.WaitUntilDone(3600000);
                    }
                    catch (DaqException e)
                    {
                        _run = false;
                    }
                    
                    myTask.Stop();
                }

                _cycleCounter++;
                TriggerEvent(CycleFinished);

                if (_networkMode && _cycleCounter >= _cyclesPerRun)
                {
                    Pause();
                    TriggerEvent(RunFinished);
                    _newRun = true;
                }            

            }
            _running = false;
            TriggerEvent(Stopped);
            
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
                _networkMode = networkMode;
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
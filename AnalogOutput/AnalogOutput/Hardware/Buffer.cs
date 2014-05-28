using System;
using System.Threading;
using AnalogOutput.Data;
using NationalInstruments.DAQmx;
using fastJSON;

namespace AnalogOutput.Hardware
{
    public class Buffer
    {        
        private int _iCycle;
        private int _cycles;
        private DataCard _data;
        private Thread _myWorker;
        private bool _networkMode;
        private double[,] _outputSequence;
        private bool _run;
        private bool _running;
        private string _serializedData;
        private bool _updated;
        private bool _newRun = true;

        public void UpdateData(string data)
        {
            _serializedData = data;
            _updated = true;
        }

        private void WorkingLoop()
        {
            TriggerEvent(Started);
            _running = true;
            _iCycle = 0;
            while (_run)
            {                
                if (_updated)
                {
                    _data = (DataCard) JSON.Instance.ToObject(_serializedData);
                    _outputSequence = Translator.GenerateDAQmxSequence(_data);
                    _updated = false;                                        
                }

                using (var myTask = new Task())
                {
                    myTask.AOChannels.CreateVoltageChannel("Dev2/ao0:7", "aoChannel", -10.0, 10.0, AOVoltageUnits.Volts);
                    myTask.Timing.ConfigureSampleClock("", 500000, SampleClockActiveEdge.Rising,
                                                       SampleQuantityMode.FiniteSamples, _outputSequence.GetLength(1));
                    myTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger("/Dev2/PFI8",
                                                                             DigitalEdgeStartTriggerEdge.Rising);
                    var writer = new AnalogMultiChannelWriter(myTask.Stream);
                    writer.WriteMultiSample(false, _outputSequence);                    

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
                _iCycle++;
                TriggerEvent(CycleFinished);

                if (_networkMode && _iCycle >= _cycles)
                {                    
                    TriggerEvent(RunFinished);
                    _newRun = true;
                }                          
            }
            _running = false;
            TriggerEvent(Stopped);
        }

        public void Start(bool networkMode, string data, int cyclesPerRun = 0)
        {
            if (!_running)
            {
                _newRun = true;
                _cycles = cyclesPerRun;
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
            if(!_running)
                TriggerEvent(Stopped);
            _run = false;
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
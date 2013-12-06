using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AnalogOutput.Data;
using fastJSON;
using NationalInstruments.DAQmx;

namespace AnalogOutput.Hardware
{
    public class Buffer
    {
        private bool _updated;
        private string _serializedData;
        private bool _run;
        private bool _running;
        private Thread _myWorker;
        private DataCard _data;
        private double[,] _outputSequence;
        private bool _networkMode = false;
        private int _cyclesPerRun;
        private int _cycleCounter;
        private readonly ManualResetEvent _signal = new ManualResetEvent(true);

        public void UpdateData(string data)
        {
            _serializedData = data;
            _updated = true;
        }

        private void WorkingLoop()
        {
            TriggerEvent(Started);
            _running = true;
            while (_run)
            {

                if (_updated)
                {
                    _data = (DataCard) JSON.Instance.ToObject(_serializedData);
                    _outputSequence = Translator.GenerateDAQmxSequence(_data);
                    _updated = false;
                }

                using (Task myTask = new Task())
                {
                    myTask.AOChannels.CreateVoltageChannel("Dev2/ao0:7", "aoChannel", -10.0, 10.0, AOVoltageUnits.Volts);
                    myTask.Timing.ConfigureSampleClock("", 500000, SampleClockActiveEdge.Rising,SampleQuantityMode.FiniteSamples, _outputSequence.GetLength(1));
                    myTask.Triggers.StartTrigger.ConfigureDigitalEdgeTrigger("/Dev2/PFI8", DigitalEdgeStartTriggerEdge.Rising);
                    AnalogMultiChannelWriter writer = new AnalogMultiChannelWriter(myTask.Stream);
                    writer.WriteMultiSample(false, _outputSequence);

                    _signal.WaitOne();
                                      
                    myTask.Start();

                    if (_networkMode)
                        TriggerEvent(RunStarted);

                    myTask.WaitUntilDone(3600000);
                }
                _cycleCounter++;
                TriggerEvent(CycleFinished);

                if (_networkMode && _cycleCounter++ >= _cyclesPerRun)
                {
                    Pause();
                    TriggerEvent(RunFinished);
                }

                _signal.WaitOne();

                if (_networkMode)
                {                   
                    _signal.Reset();
                }
            }
            TriggerEvent(Stopped);
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

        public void Pause()
        {
            _signal.Reset();
            
        }

        public void Resume()
        {
            _cycleCounter = 0;
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

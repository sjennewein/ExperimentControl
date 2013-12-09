using System;
using System.Threading;
using AnalogOutput.Data;
using NationalInstruments.DAQmx;
using fastJSON;

namespace AnalogOutput.Hardware
{
    public class Buffer
    {
        private readonly ManualResetEvent _signal = new ManualResetEvent(true);
        private int _cycleCounter;
        private int _cyclesPerRun;
        private DataCard _data;
        private Thread _myWorker;
        private bool _networkMode;
        private double[,] _outputSequence;
        private bool _run;
        private bool _running;
        private string _serializedData;
        private bool _updated;

        public void UpdateData(string data)
        {
            _serializedData = data;
            _updated = true;
        }

        private void WorkingLoop()
        {
            TriggerEvent(Started);
            _running = true;
            _cycleCounter = 0;
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

                    _signal.WaitOne();

                    myTask.Start();

                    if (_networkMode)
                        TriggerEvent(RunStarted);

                    myTask.WaitUntilDone(3600000);
                    myTask.Stop();
                }
                _cycleCounter++;
                TriggerEvent(CycleFinished);

                if (_networkMode && _cycleCounter >= _cyclesPerRun)
                {
                    Pause();
                    TriggerEvent(RunFinished);
                }

                _signal.WaitOne();

                //if (_networkMode)
                //{
                //    _signal.Reset();
                //}
            }
            _running = false;
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
            Console.WriteLine("Hardware resumed");
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
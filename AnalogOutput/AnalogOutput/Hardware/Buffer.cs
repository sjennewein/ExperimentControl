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
         

        public void UpdateData(string data)
        {
            _serializedData = data;
            _updated = true;
        }

        private void WorkingLoop()
        {
            TriggerEvent(Started);
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
                    myTask.AOChannels.CreateVoltageChannel("Dev1/ao0:7", "aoChannel", -10.0, 10.0, AOVoltageUnits.Volts);
                    AnalogMultiChannelWriter writer = new AnalogMultiChannelWriter(myTask.Stream);
                    //writer.WriteMultiSample(false, );
                }
            }
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

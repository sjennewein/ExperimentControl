using System;
using System.Collections.Generic;
using AnalogOutput.Data;
using AnalogOutput.Interpolation;
using fastJSON;

namespace AnalogOutput.Logic
{
    public class LogicCard
    {
        private readonly DataCard _data;
        public List<LogicPattern> Patterns = new List<LogicPattern>();

        public LogicCard(DataCard data)
        {
            _data = data;

            for (int iPattern = 0; iPattern < _data.Patterns.Length; iPattern++)
            {
                DataPattern pattern = _data.Patterns[iPattern];
                Patterns.Add(new LogicPattern(pattern, this));
            }
        }

        public string Flow
        {
            get { return _data.Flow; }
            set { _data.Flow = value; }
        }

        public void InputChanged()
        {
            TriggerEvent(NewInput);
        }

        public string GetChannelName(int index)
        {
            return _data.ChannelNames[index];
        }

        public void SetChannelName(string name, int index)
        {
            _data.ChannelNames[index] = name;
            TriggerEvent(ChannelNameChanged);
        }

        public string GetUnit(int index)
        {
            return _data.Calibration[index].Unit;
        }

        public void SetUnit(string unit, int index)
        {
            _data.Calibration[index].Unit = unit;
            TriggerEvent(CalibrationChanged);
        }

        public List<Point> GetCalibration(int index)
        {
            return _data.Calibration[index].DataPoints;
        }

        public void SetCalibration(List<Point> dataPoints, int index)
        {
            _data.Calibration[index].DataPoints = dataPoints;           
        }

        public string ToJson()
        {
            return JSON.Instance.ToJSON(_data);
        }

        private void TriggerEvent(EventHandler newEvent, EventArgs e = null)
        {
            EventHandler triggerEvent = newEvent;
            if (triggerEvent != null)
                triggerEvent(this, new EventArgs());
        }

        public event EventHandler CalibrationChanged;
        public event EventHandler ChannelNameChanged;
        public event EventHandler NewInput;
    }
}
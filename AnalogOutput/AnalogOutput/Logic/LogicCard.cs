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
        public List<LogicPattern> Patterns =new List<LogicPattern>();

        public LogicCard(DataCard data)
        {
            _data = data;
            
            for (int iPattern = 0; iPattern < _data.Patterns.Length; iPattern++)
            {
                DataPattern pattern = _data.Patterns[iPattern];
                Patterns.Add(new LogicPattern(pattern, this));
            }
        }

        public List<Tuple<string, List<Point>>> Calibration
        {
            get { return _data.Calibration; }
            set
            {
                _data.Calibration = value;
                TriggerEvent(CalibrationChanged);
            }
        }

        public string Flow
        {
            get { return _data.Flow; }
            set { _data.Flow = value; }
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
    }
}
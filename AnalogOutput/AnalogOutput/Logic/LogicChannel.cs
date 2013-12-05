using System;
using System.Collections.Generic;
using System.IO;
using AnalogOutput.Data;
using AnalogOutput.Interpolation;
using LumenWorks.Framework.IO.Csv;

namespace AnalogOutput.Logic
{
    public class LogicChannel
    {
        private readonly DataChannel _data;
        private readonly LogicPattern _parent;
        public List<LogicStep> Steps = new List<LogicStep>();

        public LogicChannel(DataChannel data, LogicPattern parent)
        {
            _data = data;
            _parent = parent;
            _parent.CalibrationChanged += delegate { OnCalibrationChanged(); };
            
            for (int iStep = 0; iStep < _data.Steps.Length; iStep++)
            {
                DataStep step = _data.Steps[iStep];
                Steps.Add(new LogicStep(step, this));
            }
        }

        public string Unit
        {
            get
            {
                string unit = _parent.GetUnit(this);
                if (string.IsNullOrEmpty(unit))
                    return "Voltage [V]:";

                return unit;
            }            
        }

        public string Name
        {
            get { return _data.Name; }
            set { _data.Name = value; }
        }

        public float Value
        {
            get { return _data.InitialValue; }
            set { _data.InitialValue = value; }
        }

        public void LoadFile(string fileName)
        {
            string unit;
            var dataSeries = new List<Point>();

            using (var csv = new CsvReader(new StreamReader(fileName), true))
            {
                string[] headers = csv.GetFieldHeaders();

                unit = headers[0];

                while (csv.ReadNextRecord())
                {
                    double x = Convert.ToDouble(csv[0]);
                    double y = Convert.ToDouble(csv[1]);
                    dataSeries.Add(new Point(x, y));
                }
            }

            var data = new Tuple<string, List<Point>>(unit, dataSeries);
            _parent.SetCalibration(data, this);

        }

        private void OnCalibrationChanged()
        {
            TriggerEvent(CalibrationChanged);
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
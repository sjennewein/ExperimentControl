﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using AnalogOutput.Data;
using AnalogOutput.Interpolation;
using LumenWorks.Framework.IO.Csv;

namespace AnalogOutput.Logic
{
    public class LogicChannel : INotifyPropertyChanged
    {
        private readonly DataChannel _data;
        private readonly LogicPattern _parent;
        public List<LogicStep> Steps = new List<LogicStep>();

        public LogicChannel(DataChannel data, LogicPattern parent)
        {
            _data = data;
            _parent = parent;
            _parent.CalibrationChanged += delegate { OnCalibrationChanged(); };
            _parent.ChannelNameChanged += delegate { OnChannelNameChanged(); };
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

                return unit;
            }
        }

        public void InputChanged()
        {
            _parent.InputChanged();            
        }

        public string Name
        {
            get { return _parent.GetChannelName(this); }
            set { _parent.SetChannelName(value, this); }
        }

        public double Value
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
                    dataSeries.Add(new Point {X = x, Y = y});
                }
            }

            _parent.SetCalibration(dataSeries, this);
            _parent.SetUnit(unit, this);
        }

        private void OnCalibrationChanged()
        {
            TriggerEvent(CalibrationChanged);
        }

        private void OnChannelNameChanged()
        {
            PropertyChangedEvent("Name");
        }

        private void PropertyChangedEvent(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (null != propertyChanged)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void TriggerEvent(EventHandler newEvent, EventArgs e = null)
        {
            EventHandler triggerEvent = newEvent;
            if (triggerEvent != null)
                triggerEvent(this, new EventArgs());
        }

        public event EventHandler CalibrationChanged;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
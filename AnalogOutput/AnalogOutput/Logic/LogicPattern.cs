using System;
using System.Collections.Generic;
using AnalogOutput.Data;
using AnalogOutput.Interpolation;

namespace AnalogOutput.Logic
{
    public class LogicPattern
    {
        public readonly List<LogicChannel> Channels = new List<LogicChannel>();
        private readonly DataPattern _data;
        private readonly LogicCard _parent;

        public LogicPattern(DataPattern data, LogicCard parent)
        {
            _parent = parent;
            _data = data;

            _parent.CalibrationChanged += delegate { OnCalibrationChanged(); };
            _parent.ChannelNameChanged += delegate { OnChannelNameChanged(); };

            for (int iChannel = 0; iChannel < _data.Channels.Length; iChannel++)
            {
                DataChannel channel = _data.Channels[iChannel];
                Channels.Add(new LogicChannel(channel, this));
            }
        }

        public string Name
        {
            get { return _data.Name; }
            set { _data.Name = value; }
        }

        public string GetChannelName(LogicChannel enquirer)
        {
            int index = Channels.IndexOf(enquirer);
            return _parent.GetChannelName(index);
        }


        public void SetChannelName(string name, LogicChannel enquirer)
        {
            int index = Channels.IndexOf(enquirer);
            _parent.SetChannelName(name, index);
        }

        public List<Point> GetCalibration(LogicChannel enquirer)
        {
            int index = Channels.IndexOf(enquirer);
            return _parent.GetCalibration(index);
        }

        public void SetCalibration(List<Point> calibration, LogicChannel source)
        {
            int index = Channels.IndexOf(source);
            _parent.SetCalibration(calibration, index);
        }

        public string GetUnit(LogicChannel enquirer)
        {
            int index = Channels.IndexOf(enquirer);
            return _parent.GetUnit(index);
        }

        public void SetUnit(string unit, LogicChannel enquirer)
        {
            int index = Channels.IndexOf(enquirer);
            _parent.SetUnit(unit, index);
        }

        private void OnCalibrationChanged()
        {
            TriggerEvent(CalibrationChanged);
        }

        private void OnChannelNameChanged()
        {
            TriggerEvent(ChannelNameChanged);
        }

        private void TriggerEvent(EventHandler newEvent, EventArgs e = null)
        {
            EventHandler triggerEvent = newEvent;
            if (triggerEvent != null)
                triggerEvent(this, new EventArgs());
        }

        public event EventHandler CalibrationChanged;
        public event EventHandler ChannelNameChanged;
    }
}
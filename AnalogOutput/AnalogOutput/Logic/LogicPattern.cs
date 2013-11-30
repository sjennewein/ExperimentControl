using AnalogOutput.Data;

namespace AnalogOutput.Logic
{
    public class LogicPattern
    {
        public readonly LogicChannel[] Channels;
        private readonly DataPattern _data;
        private readonly LogicCard _parent;

        public LogicPattern(DataPattern data, LogicCard parent)
        {
            _parent = parent;
            _data = data;
            Channels = new LogicChannel[_data.Channels.Length];
            for (int iChannel = 0; iChannel < _data.Channels.Length; iChannel++)
            {
                DataChannel channel = _data.Channels[iChannel];
                Channels[iChannel] = new LogicChannel(channel, this);
            }
        }

        public string Name
        {
            get { return _data.Name; }
            set { _data.Name = value; }
        }
    }
}
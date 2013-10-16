using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnalogOutput.Model;

namespace AnalogOutput.Controller
{
    public class ControllerPattern
    {
        private readonly ModelPattern _model;
        public ControllerChannel[] Channels;

        public string Name { get { return _model.Name; }}

        public ControllerPattern(ModelPattern model)
        {
            _model = model;
            Channels = new ControllerChannel[model.Channels.Length];

            for (int iChannel = 0; iChannel < _model.Channels.Length; iChannel++ )
            {
                ModelChannel channelModel = _model.Channels[iChannel];
                Channels[iChannel] = new ControllerChannel(channelModel);

            }
        }
    }
}

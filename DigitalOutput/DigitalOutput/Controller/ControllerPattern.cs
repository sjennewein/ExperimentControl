using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DigitalOutput.Model;

namespace DigitalOutput.Controller
{
    public class ControllerPattern
    {
        private readonly ModelPattern _model;
        public ControllerChannel[] Channels;

        public ControllerPattern(ModelPattern model)
        {            
            _model = model;
            Channels = new ControllerChannel[model.Channels.Length];
            for(int iChannels = 0; iChannels < _model.Channels.Length; iChannels++)
            {
                var channelModel = _model.Channels[iChannels];
                Channels[iChannels] = new ControllerChannel(channelModel);
            }
        }


    }
}

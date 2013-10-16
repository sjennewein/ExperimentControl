﻿using DigitalOutput.Model;

namespace DigitalOutput.Controller
{
    public class ControllerStep
    {
        private readonly ModelStep _model;

        public ControllerChannel[] Channels;        

        public ControllerStep(ModelStep model)
        {
            _model = model;
            Channels = new ControllerChannel[_model.Channels.Length];
            for (int iChannel = 0; iChannel < _model.Channels.Length; iChannel++)
            {
                ModelData channelModel = _model.Channels[iChannel];
                Channels[iChannel] = new ControllerChannel(channelModel, iChannel);
            }
        }

        public int Duration { get; set; }
    }
}
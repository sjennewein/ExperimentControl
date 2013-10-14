using DigitalOutput.Model;

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
            for (int iChannels = 0; iChannels < _model.Channels.Length; iChannels++)
            {
                ModelData channelModel = _model.Channels[iChannels];
                Channels[iChannels] = new ControllerChannel(channelModel);
            }
        }

        public int Duration { get; set; }
    }
}
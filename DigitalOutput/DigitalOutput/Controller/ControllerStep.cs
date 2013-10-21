using System.ComponentModel;
using DigitalOutput.Model;

namespace DigitalOutput.Controller
{
    public class ControllerStep : INotifyPropertyChanged
    {
        private readonly ModelStep _model;
        private int _syncedValue;

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

        public string Description
        {
            get { return _model.Description; }
            set { _model.Description = value; }
        }

        public void StoreSyncedValues()
        {
            _syncedValue = Duration;
            foreach (ControllerChannel channel in Channels)
            {
                channel.StoreSyncedValue();
            }
        }

        public void RestoreSyncedValues()
        {
            if(_syncedValue != Duration)
            {
                Duration = _syncedValue;
                PropertyHasChanged("Duration");
            }

            foreach (ControllerChannel channel in Channels)
            {
                channel.RestoreSyncedValue();
            }
        }

        public int Duration { get { return _model.Duration.Value; } set { _model.Duration.Value = value; } }

        private void PropertyHasChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (null != propertyChanged)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
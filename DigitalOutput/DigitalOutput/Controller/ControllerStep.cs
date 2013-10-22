using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DigitalOutput.Model;
using Hulahoop.Controller;

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

        public void UpdateContextMenu(object sender, EventArgs e)
        {
            var origSender = (ContextMenu) sender;
            
            ContextMenu contextMenu = new ContextMenu();
            foreach (var iterator in HoopManager.Iterators)
            {
                var item = new MenuItem(iterator.Name, SwitchToHooping);                
                contextMenu.MenuItems.Add(item);
            }
            foreach (var EveryXthRun in HoopManager.EveryXRun)
            {
                var item = new MenuItem(EveryXthRun.Name, SwitchToHooping);
                contextMenu.MenuItems.Add(item);
            }            
            contextMenu.MenuItems.Add(new MenuItem("Enable", SwitchToManual));
            contextMenu.Show(origSender.SourceControl,new Point(0));
        }

        public void SwitchToHooping(object sender, EventArgs e)
        {
            var item = (MenuItem) sender;
            var menu = (ContextMenu) item.Parent;
            var textBox = (TextBox) menu.SourceControl;
            textBox.Text = item.Text;
            textBox.ReadOnly = true;
            textBox.DataBindings.RemoveAt(0);
        }

        public void SwitchToManual(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            var menu = (ContextMenu)item.Parent;
            var textBox = (TextBox)menu.SourceControl;
            textBox.ReadOnly = false;
            textBox.DataBindings.Add("Text", this, "Duration", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
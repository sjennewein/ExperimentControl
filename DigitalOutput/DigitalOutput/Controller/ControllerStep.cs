using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DigitalOutput.Model;
using Hulahoop.Controller;
using Hulahoop.Interface;

namespace DigitalOutput.Controller
{
    public class ControllerStep : INotifyPropertyChanged, IteratorObserver
    {
        private readonly ModelStep _model;
        private readonly ControllerPattern _parent;

        public ControllerChannel[] Channels;
        private int _syncedDuration;
        private string _syncedIterator;

        public ControllerStep(ModelStep model, ControllerPattern parent)
        {
            _parent = parent;
            _model = model;
            if (String.IsNullOrEmpty(_model.Iterator))
            {
                foreach (IteratorSubject iterator in HoopManager.Iterators)
                {
                    if (iterator.Name() == _model.Iterator)
                        iterator.Register(this);
                }
            }

            Channels = new ControllerChannel[_model.Channels.Length];
            for (int iChannel = 0; iChannel < _model.Channels.Length; iChannel++)
            {
                ModelData channelModel = _model.Channels[iChannel];
                Channels[iChannel] = new ControllerChannel(channelModel, iChannel, this);
            }
        }

        public string Description
        {
            get { return _model.Description; }
            set { _model.Description = value; }
        }

        public int Duration
        {
            get { return _model.Duration.Value; }
            set
            {
                SomethingHasChanged();
                _model.Duration.Value = value;
            }
        }

        public string Iterator
        {
            get { return _model.Iterator; }
            set
            {
                SomethingHasChanged();
                _model.Iterator = value;
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region IteratorObserver Members

        public void NewValue(double value)
        {
            Duration = (int) value;
        }

        public void NewName(string name)
        {
            Iterator = name;
            PropertyHasChanged("Iterator");
        }

        #endregion

        public void StoreSyncedValues()
        {
            _syncedDuration = Duration;           
            foreach (ControllerChannel channel in Channels)
            {
                channel.StoreSyncedValue();
            }
        }

        public void RestoreSyncedValues()
        {       
            if (_syncedDuration != Duration)
            {
                Duration = _syncedDuration;
                PropertyHasChanged("Duration");
            }

            foreach (ControllerChannel channel in Channels)
            {
                channel.RestoreSyncedValue();
            }
        }

        private void PropertyHasChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (null != propertyChanged)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateContextMenu(object sender, EventArgs e)
        {
            var origSender = (ContextMenu) sender;

            var contextMenu = new ContextMenu();
            foreach (IteratorSubject iterator in HoopManager.Iterators)
            {
                var item = new MenuItem(iterator.Name(), SwitchToHooping);
                contextMenu.MenuItems.Add(item);
            }
            
            contextMenu.MenuItems.Add(new MenuItem("Enable", SwitchToManual));
            contextMenu.Show(origSender.SourceControl, new Point(0));
        }

        public void SwitchToHooping(object sender, EventArgs e)
        {
            var item = (MenuItem) sender;
            var menu = (ContextMenu) item.Parent;
            var textBox = (TextBox) menu.SourceControl;
            
            UnregisterFromSubject();
            Iterator = item.Text;
            RegisterToSubject();
            textBox.ReadOnly = true;
            textBox.DataBindings.RemoveAt(0);

            textBox.DataBindings.Add("Text", this, "Iterator", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void RegisterToSubject()
        {
            foreach (IteratorSubject iterator in HoopManager.Iterators)
            {
                if (iterator.Name() == Iterator)
                    iterator.Register(this);
            }
        }

        private void UnregisterFromSubject()
        {
            foreach (IteratorSubject iterator in HoopManager.Iterators)
            {
                if (iterator.Name() == Iterator)
                    iterator.UnRegister(this);
            }
        }

        public void SwitchToManual(object sender, EventArgs e)
        {
            var item = (MenuItem) sender;
            var menu = (ContextMenu) item.Parent;
            var textBox = (TextBox) menu.SourceControl;

            UnregisterFromSubject();
            Iterator = "";

            textBox.ReadOnly = false;
            textBox.DataBindings.RemoveAt(0);
            textBox.DataBindings.Add("Text", this, "Duration", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        public void SomethingHasChanged()
        {
            _parent.SomethingHasChanged();
        }

        public void NewValue(double value, string sender)
        {
            throw new NotImplementedException();
        }

        public void NewName(string newName, string oldName)
        {
            throw new NotImplementedException();
        }
    }
}
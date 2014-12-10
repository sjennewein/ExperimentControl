using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AspherixGPIB.Data;
using Hulahoop.Controller;
using Hulahoop.Interface;

namespace AspherixGPIB.Controller
{
    public class CtrlGPIBArbParam : IteratorObserver, INotifyPropertyChanged
    {
        private DataGPIBArbParam _data;

        public CtrlGPIBArbParam(DataGPIBArbParam data)
        {
            UpdateData(data);
        }

        public void UpdateData(DataGPIBArbParam data)
        {
            _data = data;
            RegisterToSubject(_data.Iterator);
            TriggerEvent(DataLoaded);
        }

        public double Value
        {
            get { return _data.Value; }
            set { _data.Value = value; }
        }

        public string Iterator
        {
            get { return _data.Iterator; }
            set
            {
                UnregisterFromSubject();
                RegisterToSubject(value);
                _data.Iterator = value;                
            }
        }

        private void RegisterToSubject(string name)
        {
            foreach (IteratorSubject iterator in HoopManager.Iterators)
            {
                if (iterator.Name() == name)
                    iterator.Register(this);
            }
        }

        private void UnregisterFromSubject()
        {
            foreach (IteratorSubject iterator in HoopManager.Iterators)
                {
                    if (iterator.Name() == _data.Iterator)
                        iterator.UnRegister(this);
                }
            _data.Iterator = null;
        }

        public void NewValue(double value, string sender)
        {
            Value = value;
        }

        public void NewName(string newName, string oldName)
        {
            _data.Iterator = newName;
            PropertyHasChanged("Iterator");
        }

        private void PropertyHasChanged(string propertyName)
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

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler DataLoaded;
    }
}

using System;
using System.ComponentModel;
using System.IO;
using AnalogOutput.Data;
using Hulahoop.Controller;
using Hulahoop.Interface;

namespace AnalogOutput.Logic
{
    public class LogicStep : INotifyPropertyChanged, IteratorObserver
    {
        private readonly DataStep _data;
        private readonly LogicChannel _parent;

        public LogicStep(DataStep data, LogicChannel parent)
        {
            _data = data;
            _parent = parent;
        }

        public StepType Type
        {
            get { return _data.Type; }
            set { _data.Type = value; }
        }

        public double Value
        {
            get { return _data.Value; }
            set { _data.Value = value; }
        }

        public int Duration
        {
            get { return _data.Duration; }
            set { _data.Duration = value; }
        }

        public string FileName
        {
            get { return _data.FileName; }
            set { _data.FileName = value; }
        }

        public string Description
        {
            get { return _data.Description; }
            set { _data.Description = value; }
        }

        public string DurationIterator
        {
            get { return _data.DurationIterator; }
            set
            {
                UnregisterFromSubject(_data.DurationIterator);
                _data.DurationIterator = value;
                RegisterToSubject(_data.DurationIterator);
            }
        }

        public string ValueIterator
        {
            get { return _data.ValueIterator; }
            set
            {
                UnregisterFromSubject(_data.ValueIterator);
                _data.ValueIterator = value;
                RegisterToSubject(_data.ValueIterator);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void LoadFile()
        {
            string[] lines = File.ReadAllLines(FileName);
            double[] myRamp = Array.ConvertAll(lines, double.Parse);
            _data.Ramp = myRamp;

            if(myRamp.Length % 2 != 0)
                throw new Exception("The ramp has an odd number of samples!");

            Duration = myRamp.Length;
            PropertyChangedEvent("Duration");
        }

        private void PropertyChangedEvent(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (null != propertyChanged)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void NewValue(double value, string sender)
        {
            if (sender == DurationIterator)
                Duration = (int)value;

            if (sender == ValueIterator)
                Value = value;
        }

        public void NewName(string newName, string oldName)
        {            
            if (oldName == DurationIterator)
            {
                _data.DurationIterator = newName;      
                PropertyChangedEvent("DurationIterator");
            }

            if (oldName == ValueIterator)
            {
                _data.ValueIterator = newName;
                PropertyChangedEvent("ValueIterator");
            }
        }

        private void RegisterToSubject(string iteratorName)
        {
            foreach (ControllerIterator iterator in HoopManager.LinearIterators)
            {
                if (iterator.Name == iteratorName)
                    iterator.Register(this);
            }
        }

        private void UnregisterFromSubject(string iteratorName)
        {
            foreach (ControllerIterator iterator in HoopManager.LinearIterators)
            {
                if (iterator.Name == iteratorName)
                    iterator.UnRegister(this);
            }
        }
    }
}
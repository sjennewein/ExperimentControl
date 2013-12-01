using System;
using System.ComponentModel;
using System.IO;
using AnalogOutput.Data;

namespace AnalogOutput.Logic
{
    public class LogicStep : INotifyPropertyChanged
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

        public double Duration
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
    }
}
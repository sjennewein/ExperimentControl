using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Hulahoop.Interface;
using Hulahoop.Model;
using Ionic.Zip;
using fastJSON;

namespace Hulahoop.Controller
{
    public class ControllerFileIterator : IteratorSubject, INotifyPropertyChanged
    {
        private readonly ModelFileIterator _model;
        private readonly List<IteratorObserver> _observers = new List<IteratorObserver>();
        public UserControl myGUI;
        private int _counter;
        private int _fileLength;

        public ControllerFileIterator(ModelFileIterator model)
        {            
            _model = model;
            if(_model.Iterations != null)
                _fileLength = _model.Iterations.Length;
        }

        public int Counter
        { get { return _counter; } }

        public double CurrentValue
        {
            get
            {
                if (_model.Iterations != null && _model.Iterations.Length != 0)
                    return _model.Iterations[_counter];
                return 0;
            } }

        public string Name
        {
            get { return _model.Name; }
            set
            {
                string oldName = _model.Name;
                _model.Name = value;
                foreach (var observer in _observers)
                {
                    observer.NewName(_model.Name,oldName);
                }
            }
        }

        public string FileName
        {
            get { return _model.FileName; }
            set
            {
                _model.FileName = value;
                PropertyChangedEvent("FileName");
            }
        }

        public int Lines
        {
            get { return _fileLength; }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region IteratorSubject Members

        public void Register(IteratorObserver newObserver)
        {
            _observers.Add(newObserver);
            if (_model.Iterations != null && _model.Iterations.Length > 0)
                newObserver.NewValue(_model.Iterations[0], Name);
        }

        public void UnRegister(IteratorObserver goneObserver)
        {
            _observers.Remove(goneObserver);
        }

        #endregion

        public void LoadFile()
        {
            string[] lines = File.ReadAllLines(FileName);
            double[] Iterations = Array.ConvertAll(lines, double.Parse);
            _model.Iterations = Iterations;
            _fileLength = Iterations.Length;
            PropertyChangedEvent("Lines");

            foreach (IteratorObserver observer in _observers)
            {
                if (_model.Iterations.Length > 0)
                    observer.NewValue(_model.Iterations[0], Name);
            }
        }

        private void PropertyChangedEvent(string propertyName)
        {
            if (myGUI.InvokeRequired)
            {
                GuiUpdate callback = PropertyChangedEvent;
                myGUI.Invoke(callback, propertyName);
            }
            else
            {
                PropertyChangedEventHandler propertyChanged = PropertyChanged;
                if (null != propertyChanged)
                    propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void Reset()
        {
            _counter = 0;

            double newValue = _model.Iterations[_counter];

            foreach (IteratorObserver observer in _observers)
            {
                observer.NewValue(newValue, Name);
            }
        }

        public void Increment()
        {
            _counter++;

            int length = _model.Iterations.Length;

            if (_counter >= length)
                _counter = length - 1;

            double newValue = _model.Iterations[_counter];

            foreach (IteratorObserver observer in _observers)
            {
                observer.NewValue(newValue, Name);
            }
            PropertyChangedEvent("Counter");
            PropertyChangedEvent("CurrentValue");
        }

        public void Save(ZipFile zip)
        {
            string json = JSON.Instance.ToJSON(_model);
            zip.AddEntry("FileIterator_" + Name + ".txt", json);
        }


        string IteratorSubject.Name()
        {
            return Name;
        }

        private delegate void GuiUpdate(string propertyName);
    }
}
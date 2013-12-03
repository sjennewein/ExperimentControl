using System;
using System.Collections.Generic;
using Hulahoop.Interface;
using Hulahoop.Model;
using Ionic.Zip;
using fastJSON;

namespace Hulahoop.Controller
{
    public class ControllerLinearIterator : IteratorSubject
    {
        private readonly ModelLinearIterator _model;
        private readonly List<IteratorObserver> _observers = new List<IteratorObserver>();

        private int _counter;

        public ControllerLinearIterator(ModelLinearIterator model)
        {
            _model = model;
        }

        public int Start
        {
            get { return _model.Start; }
            set
            {
                _model.Start = value;
                foreach (IteratorObserver observer in _observers)
                {
                    observer.NewValue(value, Name);
                }
            }
        }

        public int Stop
        {
            get { return _model.Stop; }
            set { _model.Stop = value; }
        }

        public int StepSize
        {
            get { return _model.StepSize; }
            set { _model.StepSize = value; }
        }

        public string Name
        {
            get { return _model.Name; }
            set
            {
                string oldName = _model.Name;
                _model.Name = value;
                foreach (IteratorObserver observer in _observers)
                {
                    observer.NewName(_model.Name, oldName);
                }
            }
        }

        #region IteratorSubject Members

        public void Register(IteratorObserver newObserver)
        {
            _observers.Add(newObserver);
            newObserver.NewValue(Start, Name);
        }

        public void UnRegister(IteratorObserver goneObserver)
        {
            _observers.Remove(goneObserver);
        }

        #endregion

        public void Increment()
        {
            _counter++;

            int newValue = Start + _counter*StepSize;

            if (Math.Abs(newValue) >= Math.Abs(Stop))
                newValue = Stop;

            foreach (IteratorObserver observer in _observers)
            {
                observer.NewValue(newValue, Name);
            }
        }

        public void Reset()
        {
            _counter = 0;
        }

        public void Close()
        {
        }

        public void Save(ZipFile zip)
        {
            string json = JSON.Instance.ToJSON(_model);
            zip.AddEntry("LinearIterator_" + Name + ".txt", json);
        }


        string IteratorSubject.Name()
        {
            return Name;
        }
    }
}
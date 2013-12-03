using System.Collections.Generic;
using Hulahoop.Interface;
using Hulahoop.Model;
using Ionic.Zip;
using fastJSON;

namespace Hulahoop.Controller
{
    public class ControllerIterator : IteratorSubject
    {
        private readonly ModelIterator _model;
        private readonly List<IteratorObserver> _observers = new List<IteratorObserver>();

        public ControllerIterator(ModelIterator model)
        {
            _model = model;
        }

        private int _value = 0;

        public int Start
        {
            get { return _model.Start; }
            set { _model.Start = value; }
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
                var oldName = _model.Name;
                _model.Name = value;
                foreach (var observer in _observers)
                {
                    observer.NewName(_model.Name, oldName);
                }
                
            }
        }

        public void Increment()
        {
            if (_value > Stop) 
                return;

            _value += StepSize;
            
            foreach (var observer in _observers)
            {
                observer.NewValue(_value, Name);
            }
        }

        public void Close()
        {
        }

        public void Save(ZipFile zip)
        {
            string json = JSON.Instance.ToJSON(_model);
            zip.AddEntry("Iterator_" + Name + ".txt", json);
        }

        public void Register(IteratorObserver newObserver)
        {
            _observers.Add(newObserver);
        }

        public void UnRegister(IteratorObserver goneObserver)
        {
            _observers.Remove(goneObserver);
        }
    }
}
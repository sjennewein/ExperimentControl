using System.Collections.Generic;
using Hulahoop.Interface;
using Hulahoop.Model;

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
            set { _model.Name = value; }
        }

        public void Increment()
        {
            if (_value > Stop) 
                return;

            _value += StepSize;
            
            foreach (var observer in _observers)
            {
                observer.NewValue(_value);
            }
        }

        public void Close()
        {
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
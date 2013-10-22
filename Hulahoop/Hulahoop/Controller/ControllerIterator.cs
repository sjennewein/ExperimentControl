using Hulahoop.Model;

namespace Hulahoop.Controller
{
    public class ControllerIterator
    {
        private readonly ModelIterator _model;

        public ControllerIterator(ModelIterator model)
        {
            _model = model;
        }

        private int Value = 0;

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
            if(Value <= Stop)
            {
                Value += StepSize;
            }
        }

        public void Close()
        {
        }
    }
}
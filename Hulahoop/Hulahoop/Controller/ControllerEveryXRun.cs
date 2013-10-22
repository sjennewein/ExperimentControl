using Hulahoop.Model;

namespace Hulahoop.Controller
{
    public class ControllerEveryXRun
    {
        private readonly ModelRunEveryX _model;

        public ControllerEveryXRun(ModelRunEveryX model)
        {
            _model = model;
        }

        public string Name
        {
            get { return _model.Name; }
            set { _model.Name = value; }
        }

        public int EveryXthRun
        {
            get { return _model.EveryXthRun; }
            set { _model.EveryXthRun = value; }
        }

        public bool IsValid(int actualRun)
        {
            bool answer = actualRun%_model.EveryXthRun == 0;
                // if division is without remainder then it's true otherwise false
            return answer;
        }
    }
}
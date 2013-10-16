using DigitalOutput.Model;

namespace DigitalOutput.Controller
{
    public class ControllerPattern
    {
        private readonly ModelPattern _model;
        public ControllerStep[] Steps;

        public string Name { get { return _model.Name; } }

        public ControllerPattern(ModelPattern model)
        {
            _model = model;
            Steps = new ControllerStep[model.Step.Length];
            for (int iSteps = 0; iSteps < _model.Step.Length; iSteps++)
            {
                ModelStep stepModel = _model.Step[iSteps];
                Steps[iSteps] = new ControllerStep(stepModel);
            }
        }
    }
}
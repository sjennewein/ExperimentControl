using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DigitalOutput.Model;

namespace DigitalOutput.Controller
{
    public class ControllerChannel
    {
        private ModelChannel _model;
        public ControllerStep[] Steps;

        public ControllerChannel(ModelChannel model)
        {
            _model = model;
            Steps = new ControllerStep[_model.Steps.Length];
            for(int iSteps = 0; iSteps < _model.Steps.Length; iSteps++)
            {
                var modelStep = _model.Steps[iSteps];
                Steps[iSteps] = new ControllerStep(modelStep);
            }
        }
    }
}

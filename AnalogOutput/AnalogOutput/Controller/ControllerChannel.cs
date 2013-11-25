using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnalogOutput.Model;

namespace AnalogOutput.Controller
{
    public class ControllerChannel
    {
        private readonly ModelChannel _model;

        public ControllerStep[] Steps;

        public ControllerChannel(ModelChannel model)
        {
            _model = model;
            Steps = new ControllerStep[_model.Steps.Length];
            for (int iStep = 0; iStep < _model.Steps.Length; iStep++)
            {
                ModelStep stepModel = _model.Steps[iStep];
                Steps[iStep] = new ControllerStep(stepModel);
            }
    }
    }
}

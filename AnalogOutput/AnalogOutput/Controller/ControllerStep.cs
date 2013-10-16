using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnalogOutput.Model;

namespace AnalogOutput.Controller
{
    public class ControllerStep
    {
        private ModelStep _model;

        public ControllerStep(ModelStep model)
        {
            _model = model;
        }

        public double Value
        {
            get { return _model.Value.Value; }
            set { _model.Value.Value = value; }
        }
    }
}

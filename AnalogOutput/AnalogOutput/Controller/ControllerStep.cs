using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnalogOutput.Model;

namespace AnalogOutput.Controller
{
    public class ControllerStep
    {
        private ModelData _model;

        public ControllerStep(ModelData model)
        {
            _model = model;
        }

        public double Value
        {
            get { return _model.Value; }
            set { _model.Value = value; }
        }
    }
}

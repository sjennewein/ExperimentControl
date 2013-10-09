using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DigitalOutput.Model;

namespace DigitalOutput.Controller
{
    public class ControllerStep
    {
        private ModelStep _model;
        

        public ControllerStep(ModelStep model)
        {
            _model = model;
        }
    }
}

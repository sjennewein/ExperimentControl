using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnalogOutput.Model;

namespace AnalogOutput.Controller
{
    public class ControllerCard
    {
        private readonly ModelCard _model;
        public ControllerPattern[] Patterns;

        public ControllerCard(ModelCard model)
        {
            _model = model;
            Patterns = new ControllerPattern[_model.Patterns.Length];
            for (int iPattern = 0; iPattern < _model.Patterns.Length; iPattern++)
            {
                ModelPattern modelPattern = _model.Patterns[iPattern];
                Patterns[iPattern] = new ControllerPattern(modelPattern);

            }
    }
    }
}

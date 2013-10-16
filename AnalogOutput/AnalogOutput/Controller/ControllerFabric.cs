using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnalogOutput.Model;

namespace AnalogOutput.Controller
{
    public class ControllerFabric
    {
        public static ControllerCard GenerateCard(ModelCard model = null)
        {
            if (model == null)
                model = ModelFabric.GenerateCard();

            return new ControllerCard(model);
        }
    }
}

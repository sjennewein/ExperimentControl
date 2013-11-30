using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnalogOutput.Logic;

namespace AnalogOutput
{
    public class Controller
    {
        public LogicCard Hardware;
        
        public void Initialize(string data = null)
        {
            Hardware = LogicFabric.GenerateCard();
        }
    }
}

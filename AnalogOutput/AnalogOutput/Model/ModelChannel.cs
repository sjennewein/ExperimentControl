using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalogOutput.Model
{
    public class ModelChannel
    {
        public ModelStep[] Steps;
        public string Name;

        public ModelChannel(int steps)
        {
            Steps = new ModelStep[steps];
        }
    }
}

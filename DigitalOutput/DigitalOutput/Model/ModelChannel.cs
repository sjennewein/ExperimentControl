using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalOutput.Model
{
    public class ModelChannel
    {
        public ModelStep[] Steps;

        public ModelChannel(int steps)
        {
            Steps = new ModelStep[steps];
        }
    }
}

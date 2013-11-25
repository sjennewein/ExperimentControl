using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalogOutput.Model
{
    public class ModelStep
    {
        public ModelData Value;
        public ModelData Duration;

        public ModelStep()
        {
            Value = new ModelData(DataType.Data);
            Duration = new ModelData(DataType.Time);    
        }
    }
}

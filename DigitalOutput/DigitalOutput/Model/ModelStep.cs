using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalOutput.Model
{
    public class ModelStep
    {
        public ModelData Duration = new ModelData(DataType.Time);
        public ModelData Value = new ModelData(DataType.Data);
    }
}

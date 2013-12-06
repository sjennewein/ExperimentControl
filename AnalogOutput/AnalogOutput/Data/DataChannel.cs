using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnalogOutput.Interpolation;

namespace AnalogOutput.Data
{
    public class DataChannel
    {               
        public DataStep[] Steps;
        public string Name = "";
        public double InitialValue;
    }
}

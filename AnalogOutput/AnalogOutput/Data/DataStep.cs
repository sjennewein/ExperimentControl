using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalogOutput.Data
{
    public enum StepType
    {
        File,
        Manual,
        Iterator
    };

    public class DataStep
    {
        public StepType Type = StepType.Manual;
        public double Value;
        public int Duration;
        public string Description = "";
        //public string FileName = "";
        public string DurationIterator = null;
        public string ValueIterator = null;
        public double[] Ramp;
    }
}

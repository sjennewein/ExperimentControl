using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalogOutput.Data
{
    public enum StepType
    {
        File,
        GUI
    };

    public class DataStep
    {
        public StepType Type = StepType.GUI;
        public double Value;
        public int Duration;
        public string Description = "";
        public string FileName = "";
        public double[] Ramp;
    }
}

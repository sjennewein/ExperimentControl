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
        public float Value;
        public float Duration;
        public string Description = "";
        public string FileName = "";
    }
}

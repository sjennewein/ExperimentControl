using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using AnalogOutput.Interpolation;

namespace AnalogOutput.Data
{
    public class DataCard
    {
        public List<DataCalibration> Calibration = new List<DataCalibration>();
        public DataPattern[] Patterns;
        public string Flow;
    }
}

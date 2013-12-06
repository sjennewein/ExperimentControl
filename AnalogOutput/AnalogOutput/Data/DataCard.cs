using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using AnalogOutput.Interpolation;

namespace AnalogOutput.Data
{
    public class DataCard
    {
        public List<Tuple<String, List<Point>>> Calibration = new List<Tuple<string, List<Point>>>();
        public DataPattern[] Patterns;
        public string Flow;
    }
}

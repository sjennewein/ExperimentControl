using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AspherixGPIB.Data
{
    public class DataGPIBArb
    {
        public int AmplitudeVolt;
        public int SamplingFrequency;
        public string Address = "00xCCAAFFEE";
        public int Samples;
        public int X0;
        public int Sigma;
        public int Amplitude;
    }
}

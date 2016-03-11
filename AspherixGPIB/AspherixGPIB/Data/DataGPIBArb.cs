using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspherixGPIB.Data
{
    public class DataGPIBArb
    {
        public string Address = "GPIB0::XX";
        public DataGPIBArbParam AmplitudeVolt;
        public DataGPIBArbParam SamplingFrequency;        
        public DataGPIBArbParam Samples;
        public DataGPIBArbParam X0;
        public DataGPIBArbParam Sigma;
        public DataGPIBArbParam Amplitude;
        public DataGPIBArbParam Offset;
    }
}

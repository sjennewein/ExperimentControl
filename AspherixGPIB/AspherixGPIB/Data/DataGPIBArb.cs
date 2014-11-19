using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspherixGPIB.Data
{
    public class DataGPIBArb
    {
        public string Address = "";
        public DataGPIBArbParam AmplitudeVolt = new DataGPIBArbParam();
        public DataGPIBArbParam SamplingFrequency = new DataGPIBArbParam();        
        public DataGPIBArbParam Samples = new DataGPIBArbParam();
        public DataGPIBArbParam X0 = new DataGPIBArbParam();
        public DataGPIBArbParam Sigma = new DataGPIBArbParam();
        public DataGPIBArbParam Amplitude = new DataGPIBArbParam();
    }
}

using System.Collections.Generic;

namespace AnalogOutput.Data
{
    public class DataCard
    {
        public List<DataCalibration> Calibration = new List<DataCalibration>();
        public string Flow;
        public List<string> ChannelNames = new List<string>();
        public DataPattern[] Patterns;
    }
}
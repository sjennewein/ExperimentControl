using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspherixGPIB.Data;
using Ivi.Visa.Interop;

namespace AspherixGPIB.Controller
{
    public class CtrlGPIBArb
    {
        private DataGPIBArb _data;
        private ResourceManager rm;
        private FormattedIO488 ioArbFG = new FormattedIO488();
        private IMessage msg;

        public CtrlGPIBArb(DataGPIBArb data = null)
        {
            if (data == null)
                _data = new DataGPIBArb();
            else
                _data = data;
        }

        public string Address
        {
            get { return _data.Address; }
            set { _data.Address = value; }
        }

        public int AmplitudeVolt
        {
            get { return _data.AmplitudeVolt; }
            set { _data.AmplitudeVolt = value; }
        }

        public int SamplingFrequency
        {
            get { return _data.SamplingFrequency; }
            set { _data.SamplingFrequency = value; }
        }

        public int Samples
        {
            get { return _data.Samples; }
            set { _data.Samples = value; }
        }

        public int X0
        {
            get { return _data.X0; }
            set { _data.X0 = value; }
        }

        public int Sigma
        {
            get { return _data.Sigma; }
            set { _data.Sigma = value; }
        }

        public int Amplitude
        {
            get { return _data.Amplitude; }
            set { _data.Amplitude = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspherixGPIB.Data;
using Ionic.Zip;
using Ivi.Visa.Interop;
using fastJSON;

namespace AspherixGPIB.Controller
{
    public class CtrlGPIBArb
    {
        private DataGPIBArb _data;
        private ResourceManager rm;
        private FormattedIO488 ioArbFG = new FormattedIO488();
        private IMessage msg;
        public string Address
        {
            get { return _data.Address; }
            set { _data.Address = value; }
        }
        public CtrlGPIBArbParam AmplitudeVolt;
        public CtrlGPIBArbParam SamplingFrequency;
        public CtrlGPIBArbParam Samples;
        public CtrlGPIBArbParam X0;
        public CtrlGPIBArbParam Sigma;
        public CtrlGPIBArbParam Amplitude;

        public CtrlGPIBArb(DataGPIBArb data = null)
        {
            if (data == null)
                _data = new DataGPIBArb();
            else
                _data = data;

            Initialize();            
        }

        private void Initialize()
        {
            AmplitudeVolt = new CtrlGPIBArbParam(_data.AmplitudeVolt);
            SamplingFrequency = new CtrlGPIBArbParam(_data.SamplingFrequency);
            Samples = new CtrlGPIBArbParam(_data.Samples);
            X0 = new CtrlGPIBArbParam(_data.X0);
            Sigma = new CtrlGPIBArbParam(_data.Sigma);
            Amplitude = new CtrlGPIBArbParam(_data.Amplitude);
        }



        public void FromJSON(string gpibArbWave)
        {
            _data = (DataGPIBArb)fastJSON.JSON.Instance.ToObject(gpibArbWave);
            Initialize();
        }

        public void Save(ZipFile zip)
        {
            string json = JSON.Instance.ToJSON(_data);
            zip.AddEntry("GPIBArbWave.txt", json);
        }
    }
}

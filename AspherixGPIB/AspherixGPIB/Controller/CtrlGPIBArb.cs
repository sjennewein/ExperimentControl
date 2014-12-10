using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspherixGPIB.Data;
using Ionic.Zip;
using Ivi.Visa.Interop;
using fastJSON;
using System.Windows.Forms;

namespace AspherixGPIB.Controller
{
    public class CtrlGPIBArb
    {
        private DataGPIBArb _data;
        private ResourceManager rm;
        private FormattedIO488 _gpib = new FormattedIO488();
        private IMessage msg;
        public bool Activated = false;

        private enum gpibState
        {
            Connected,
            Disconnected
        };

        private gpibState _deviceState = gpibState.Disconnected;

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

        private void GpibConnect()
        {
            rm = new ResourceManager();
            try
            {
                msg = (rm.Open(Address)) as IMessage;
                _gpib.IO = msg;
                _deviceState = gpibState.Connected;
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Open failed on " + Address + " " + ex.Source + "  " + ex.Message, "ApplyBurst");
                _gpib.IO = null;
                _deviceState = gpibState.Disconnected;
            }
        }

        private void UpdateGPIB()
        {
            if (!Activated)
                return;
            
            if(_deviceState == gpibState.Disconnected)
                GpibConnect();
            
            string volt = Convert.ToString(AmplitudeVolt.Value);
            string sampleFreq = Convert.ToString(SamplingFrequency.Value);
            int samples = Convert.ToInt16(Samples.Value);
            int x0 = Convert.ToInt16(X0.Value);
            int sigma = Convert.ToInt16(Sigma.Value);
            int amplitude = Convert.ToInt16(Amplitude.Value);
            short[] myData = new short[samples];
            short sum = 0;

            for (int i = 0; i < samples; i++)
            {
                double exponent = -1 * Math.Pow(i - x0, 2) / (2 * Math.Pow(sigma, 2));
                myData[i] = (short)(amplitude * Math.Exp(exponent));

                sum += myData[i];
            }

            int byteLength = Buffer.ByteLength(myData);
            byte[] myBytes = new byte[byteLength];
            Buffer.BlockCopy(myData, 0, myBytes, 0, byteLength);

            _gpib.WriteString("*RST");
            _gpib.WriteString("LDWF?0," + samples.ToString() + "\n");


            if (Convert.ToInt16(_gpib.ReadString()) == 1)
            {
                //ioArbFG.WriteString(output,false);
                _gpib.IO.Timeout = 10000;
                _gpib.IO.Write(myBytes, 2 * samples + 2);
            }
            _gpib.WriteString("FUNC5\n");


            _gpib.WriteString("AMPL" + volt + "VP");
            _gpib.WriteString("FSMP" + volt);
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

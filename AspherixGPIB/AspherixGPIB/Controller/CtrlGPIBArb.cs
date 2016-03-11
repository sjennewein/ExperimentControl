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
        public CtrlGPIBArbParam Offset;

        public CtrlGPIBArb(DataGPIBArb data = null)
        {
            if (data == null)
            {
                _data = new DataGPIBArb();
                _data.Amplitude = new DataGPIBArbParam();
                _data.AmplitudeVolt = new DataGPIBArbParam();
                _data.Samples = new DataGPIBArbParam();
                _data.SamplingFrequency = new DataGPIBArbParam();
                _data.Sigma = new DataGPIBArbParam();
                _data.X0 = new DataGPIBArbParam();
                _data.Offset = new DataGPIBArbParam();
            }
            else
            {
                   _data = data;
            }

            AmplitudeVolt = new CtrlGPIBArbParam(_data.AmplitudeVolt);
            SamplingFrequency = new CtrlGPIBArbParam(_data.SamplingFrequency);
            Samples = new CtrlGPIBArbParam(_data.Samples);
            X0 = new CtrlGPIBArbParam(_data.X0);
            Sigma = new CtrlGPIBArbParam(_data.Sigma);
            Amplitude = new CtrlGPIBArbParam(_data.Amplitude);            
            Offset = new CtrlGPIBArbParam(_data.Offset);
        }

        private void Initialize()
        {
            AmplitudeVolt.UpdateData(_data.AmplitudeVolt);
            SamplingFrequency.UpdateData(_data.SamplingFrequency);
            Samples.UpdateData(_data.Samples);
            X0.UpdateData(_data.X0);
            Sigma.UpdateData(_data.Sigma);
            Amplitude.UpdateData(_data.Amplitude);
            Offset.UpdateData(_data.Offset);
        }

        private void GpibConnect()
        {
            rm = new ResourceManager();
            try
            {
                msg = (rm.Open(Address)) as IMessage;
                _gpib.IO = msg;
                //_gpib.WriteString("*CAL?");
                _deviceState = gpibState.Connected;
            }
            catch (SystemException ex)
            {
                MessageBox.Show("Open failed on " + Address + " " + ex.Source + "  " + ex.Message, "ApplyBurst");
                _gpib.IO = null;
                _deviceState = gpibState.Disconnected;
            }
        }

        public void ManualSet()
        {
            UpdateGPIB();
        }

        private void UpdateGPIB()
        {
            if (!Activated)
                return;
            
            if(_deviceState == gpibState.Disconnected)
                GpibConnect();
            
            string volt = Convert.ToString(AmplitudeVolt.Value);
            string freq = Convert.ToString(SamplingFrequency.Value);
            int samples = Convert.ToInt16(Samples.Value);
            int x0 = Convert.ToInt16(X0.Value);
            int sigma = Convert.ToInt16(Sigma.Value);
            int amplitude = Convert.ToInt16(Amplitude.Value);
            short[] myData = new short[samples+1];
            short sum = 0;

            for (int i = 0; i < samples; i++)
            {
                double exponent = -1 * Math.Pow(i - x0, 2) / (2 * Math.Pow(sigma, 2));
                myData[i] = (short)(amplitude * Math.Exp(exponent));

                sum += myData[i];
            }
            myData[samples] = sum;
            int byteLength = Buffer.ByteLength(myData);
            byte[] myBytes = new byte[byteLength];
            Buffer.BlockCopy(myData, 0, myBytes, 0, byteLength);

            _gpib.WriteString("*RST");
            _gpib.WriteString("TSRC 2");
            _gpib.WriteString("BCNT 1");
            _gpib.WriteString("MTYP 5");
            _gpib.WriteString("MENA 1");
            _gpib.WriteString("LDWF?0," + samples.ToString() + "\n");


            if (Convert.ToInt16(_gpib.ReadString()) == 1)
            {
                //ioArbFG.WriteString(output,false);
                _gpib.IO.Timeout = 10000;
                _gpib.IO.Write(myBytes, 2 * samples+2);
            }
            _gpib.WriteString("FUNC5");

            _gpib.WriteString("FSMP" + freq);
            _gpib.WriteString("OFFS 0");
            _gpib.WriteString("AMPL" + volt + "VP");
            _gpib.WriteString("AMPL?");
            System.Console.WriteLine("Arb Waveform Ampl: " +_gpib.ReadString());
        }

        public void FromJSON(string gpibArbWave)
        {
            _data = (DataGPIBArb)fastJSON.JSON.Instance.ToObject(gpibArbWave);
            if(_data.Offset == null)
                _data.Offset = new DataGPIBArbParam();
            Initialize();
        }

        public void Save(ZipFile zip)
        {
            string json = JSON.Instance.ToJSON(_data);
            zip.AddEntry("GPIBArbWave.txt", json);
        }

        public void Update()
        {
            UpdateGPIB();
        }
    }
}

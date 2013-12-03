//using System;

using System.ComponentModel;
using System.IO;
using AnalogOutput.Data;
using AnalogOutput.Hardware;
using AnalogOutput.Logic;
using fastJSON;
using Hulahoop.Controller;
using Ionic.Zip;

namespace AnalogOutput
{
    public class Controller : INotifyPropertyChanged
    {
        private readonly Buffer _daqmx = new Buffer();
        public LogicCard Hardware = null;
        public LogicNetwork Network = new LogicNetwork();

        public Controller()
        {
            Network.DataUpdated += delegate { RemoteStart(); };
            Network.StartRun += delegate { OnNwStartRun(); };
            _daqmx.CycleFinished += delegate { OnHwCycleFinished(); };
            _daqmx.RunFinished += delegate { OnHwRunFinished(); };
            _daqmx.RunStarted += delegate { OnHwRunStarted(); };
        }

        public int CycleCounter { get; set; }
        public int RunCounter { get; set; }

        public int CyclesPerRun
        {
            get { return Network.Data; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Initialize(string data = null)
        {
            DataCard card = null;

            if (data != null)
                card = (DataCard) JSON.Instance.ToObject(data);

            Hardware = LogicFabric.GenerateCard(card);
        }

        public void Save(string fileName)
        {
            string json = Hardware.ToJson();
            using (var zip = new ZipFile())
            {
                zip.AddEntry("AnalogData.txt", json);
                // HoopManager.Save(zip);
                zip.Save(fileName);
            }
        }

        public void Load(string fileName)
        {
            string cardData = "";

            using (ZipFile zip = ZipFile.Read(fileName))
            {
                using (var ms = new MemoryStream())
                {
                    ZipEntry entry = zip["AnalogData.txt"];
                    entry.Extract(ms);
                    ms.Flush();
                    ms.Position = 0;
                    cardData = new StreamReader(ms).ReadToEnd();
                    ms.Close();
                }
                Initialize(cardData);
                //HoopManager.Load(zip); // has to be restored before the card fabric is called
            }
        }

        public void Start()
        {
            CycleCounter = 0;
            RunCounter = 0;

            if (Network.Activated)
            {
                Network.Connect();
                _daqmx.Pause();
                Network.ListenToTrigger();

                return;
            }
            string json = Hardware.ToJson();
            _daqmx.Start(false, json);
        }

        public void Stop()
        {
            if (Network.Activated)
            {
                Network.Disconnect();
            }
            _daqmx.Stop();
        }

        private void RemoteStart()
        {
            string json = Hardware.ToJson();
            _daqmx.Start(true, json, CyclesPerRun);
        }

        private void OnNwStartRun()
        {
            _daqmx.Resume();
        }

        private void OnHwRunStarted()
        {
            Network.HardwareStarted();
        }

        private void OnHwCycleFinished()
        {
            CycleCounter++;
            PropertyChangedEvent("CycleCounter");
        }

        private void OnHwRunFinished()
        {
            RunCounter++;
            CycleCounter = 0;
            PropertyChangedEvent("RunCounter");
            HoopManager.Increment();
            _daqmx.Resume();
            Network.StartNextRun();
        }

        private void PropertyChangedEvent(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (null != propertyChanged)
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
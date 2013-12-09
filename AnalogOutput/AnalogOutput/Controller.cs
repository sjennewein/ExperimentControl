using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using AnalogOutput.Data;
using AnalogOutput.Logic;
using fastJSON;
using Hulahoop.Controller;
using Ionic.Zip;
using Buffer = AnalogOutput.Hardware.Buffer;

namespace AnalogOutput
{
    public class Controller : INotifyPropertyChanged
    {
        private readonly Buffer _daqmx = new Buffer();
        public LogicCard Hardware = null;
        public LogicNetwork Network = new LogicNetwork();
        private Form _myGui;

        public Controller(Form gui)
        {
            _myGui = gui;
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
            Hardware.NewInput += delegate { OnInputChanged(); };
        }

        public void Save(string fileName)
        {
            string hardwareJSON = Hardware.ToJson();
            string networkJSON = Network.ToJSON();
            using (var zip = new ZipFile())
            {
                zip.AddEntry("NetworkData.txt", networkJSON);
                zip.AddEntry("AnalogData.txt", hardwareJSON);
                HoopManager.Save(zip);
                zip.Save(fileName);
            }
        }

        public void Load(string fileName)
        {
            string analogData = null;
            string networkData = null;
            using (ZipFile zip = ZipFile.Read(fileName))
            {
                using (var ms = new MemoryStream())
                {
                    ZipEntry entry = zip["AnalogData.txt"];
                    entry.Extract(ms);
                    ms.Flush();
                    ms.Position = 0;
                    analogData = new StreamReader(ms).ReadToEnd();
                    ms.Close();
                }

                using (var ms = new MemoryStream())
                {
                    ZipEntry entry = zip["NetworkData.txt"];
                    entry.Extract(ms);
                    ms.Flush();
                    ms.Position = 0;
                    networkData = new StreamReader(ms).ReadToEnd();
                    ms.Close();
                }
                Network.FromJSON(networkData);
                HoopManager.Load(zip); // has to be restored before the card fabric is called
            }
            Initialize(analogData);
        }

        public void Start()
        {
            CycleCounter = 0;
            RunCounter = 0;

            if (Network.Activated)
            {
                Network.Connect();
                TriggerEvent(NetworkConnected);
                _daqmx.Pause();
                Network.ListenToTrigger();
                return;
            }
            string json = Hardware.ToJson();
            _daqmx.Start(false, json);
            TriggerEvent(DaqmxStarted);
            TriggerEvent(BufferSynced);
        }

        public void Stop()
        {
            if (Network.Activated)
            {
                Network.Disconnect();
                TriggerEvent(NetworkDisconnected);
            }
            _daqmx.Stop();
            TriggerEvent(DaqmxStopped);
        }

        private void RemoteStart()
        {
            string json = Hardware.ToJson();
            _daqmx.Start(true, json, CyclesPerRun);
            TriggerEvent(DaqmxStarted);
            PropertyChangedEvent("CyclesPerRun");
        }

        public void CopyToBuffer()
        {
            _daqmx.UpdateData(Hardware.ToJson());
            TriggerEvent(BufferSynced);
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
            CopyToBuffer();
            Network.StartNextRun();
        }

        private void PropertyChangedEvent(string propertyName)
        {
            if (_myGui.InvokeRequired)
            {
                GuiUpdate callback = PropertyChangedEvent;
                _myGui.Invoke(callback, propertyName);
            }
            else
            {
                PropertyChangedEventHandler propertyChanged = PropertyChanged;
                if (null != propertyChanged)
                    propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private delegate void GuiUpdate(string propertyName);

        private void TriggerEvent(EventHandler newEvent, EventArgs e = null)
        {
            EventHandler triggerEvent = newEvent;
            if (triggerEvent != null)
                triggerEvent(this, new EventArgs());
        }

        private void OnInputChanged()
        {
            TriggerEvent(BufferUnsynced);
        }

        public event EventHandler DaqmxStarted;
        public event EventHandler DaqmxStopped;
        public event EventHandler NetworkConnected;
        public event EventHandler NetworkDisconnected;
        public event EventHandler BufferSynced;
        public event EventHandler BufferUnsynced;
    }
}
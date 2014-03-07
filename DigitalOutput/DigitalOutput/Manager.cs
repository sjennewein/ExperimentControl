using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DigitalOutput.Controller;
using DigitalOutput.Model;
using fastJSON;
using Hulahoop.Controller;
using Ionic.Zip;
using Buffer = DigitalOutput.Hardware.Buffer;

namespace DigitalOutput
{
    public class Manager : INotifyPropertyChanged
    {
        private readonly Buffer _daqmx = new Buffer();
        public ControllerCard Hardware = null;
        public ControllerNetwork Network = new ControllerNetwork();
        private Form _myGui;

        public Manager(Form gui)
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

        public void Initialize(string data = null)
        {

            ModelCard card = null;

            if (data != null)
                card = (ModelCard)JSON.Instance.ToObject(data);

            Hardware = ControllerFabric.GenerateCard(card);
            Hardware.RunDataChanged += delegate { OnInputChanged(); };
        }

        public void Save(string fileName)
        {
            string hardwareJSON = Hardware.ToJson();
            string networkJSON = Network.ToJSON();
            using (var zip = new ZipFile())
            {
                zip.AddEntry("NetworkData.txt", networkJSON);
                zip.AddEntry("DigitalData.txt", hardwareJSON);
                HoopManager.Save(zip);
                zip.Save(fileName);
            }
        }

        public void Load(string fileName)
        {
            string digitalData = null;
            string networkData = null;
            using (ZipFile zip = ZipFile.Read(fileName))
            {
                using (var ms = new MemoryStream())
                {
                    ZipEntry entry = zip["DigitalData.txt"];
                    entry.Extract(ms);
                    ms.Flush();
                    ms.Position = 0;
                    digitalData = new StreamReader(ms).ReadToEnd();
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
            Initialize(digitalData);
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

        private void OnInputChanged()
        {
            TriggerEvent(BufferUnsynced);
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

        private void TriggerEvent(EventHandler newEvent, EventArgs e = null)
        {
            EventHandler triggerEvent = newEvent;
            if (triggerEvent != null)
                triggerEvent(this, new EventArgs());
        }

        private delegate void GuiUpdate(string propertyName);

        public event EventHandler DaqmxStarted;
        public event EventHandler DaqmxStopped;
        public event EventHandler NetworkConnected;
        public event EventHandler NetworkDisconnected;
        public event EventHandler BufferSynced;
        public event EventHandler BufferUnsynced;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

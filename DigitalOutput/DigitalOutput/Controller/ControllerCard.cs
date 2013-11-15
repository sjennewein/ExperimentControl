using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Threading;
using ColdNetworkStack.Client;
using DigitalOutput.Model;
using Hulahoop.Controller;
using Ionic.Zip;
using fastJSON;
using Buffer = DigitalOutput.Hardware.Buffer;

namespace DigitalOutput.Controller
{
    public class ControllerCard : INotifyPropertyChanged
    {
        private readonly Buffer _hardwareBuffer;
        private readonly ModelCard _model;
        private readonly DigitalMainwindow _myGui;
        public bool Networking = false;
        public ControllerPattern[] Patterns;

        private volatile Client _tcpClient;

        public ControllerCard(ModelCard model, Buffer buffer, DigitalMainwindow gui)
        {
            _model = model;
            Patterns = new ControllerPattern[_model.Patterns.Length];
            _hardwareBuffer = buffer;
            _myGui = gui;
            _hardwareBuffer.CycleDone += delegate { UpdateCycles(); };
            

            for (int iPattern = 0; iPattern < _model.Patterns.Length; iPattern++)
            {
                ModelPattern modelPattern = _model.Patterns[iPattern];
                Patterns[iPattern] = new ControllerPattern(modelPattern, this);
            }
        }

        public string Ip
        {
            get { return _model.Ip; }
            set { _model.Ip = value; }
        }

        public int Port
        {
            get { return _model.Port; }
            set { _model.Port = value; }
        }

        public string Flow
        {
            get { return _model.Flow; }
            set { _model.Flow = value; }
        }

        public int CyclesDone { get; set; }

        public int CyclesPerRun
        {
            get
            {
                if (_tcpClient != null)
                    return _tcpClient.CyclesPerRun;
                return 0;
            }
        }

        public int RunsDone { get; set; }
        public string Status { get; set; }
        public Color StatusColor { get; set; }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void UpdateCycles()
        {
            CyclesDone++;
            PropertyChangedEvent("CyclesDone");

            if (_tcpClient == null || !_tcpClient.Connection)
                return;

            // if network connection exist pause hardware when a run is finished
            if (CyclesDone >= _tcpClient.CyclesPerRun)
            {
                TriggerEvent(UpdateDataForNextRun);
                Console.WriteLine("pausing hardware : " + DateTime.UtcNow.ToString("HH:mm:ss.ffffff"));
                _hardwareBuffer.Pause();

                CyclesDone = 0;
                
                RunsDone++;
                PropertyChangedEvent("CyclesDone");
                PropertyChangedEvent("RunsDone");
            }
        }

        public void Start()
        {
            CyclesDone = 0;
            RunsDone = 0;

            if (Networking)
            {
                if (_tcpClient == null || !_tcpClient.Connection) //check if no connection is established yet
                {
                    Connect();
                    Console.WriteLine("created tcp object");
                }

                _hardwareBuffer.Pause();
                StartTriggerMode();
            }

            _hardwareBuffer.Start(JSON.Instance.ToJSON(_model));
        }

        public void Stop()
        {
            if (Networking)
                if (_tcpClient.Connection)
                    Disconnect();

            _hardwareBuffer.Stop();
        }

        public void Save(string fileName)
        {
            string json = JSON.Instance.ToJSON(_model);
            using (var zip = new ZipFile())
            {
                zip.AddEntry("DigitalData.txt", json);
                HoopManager.Save(zip);
                zip.Save(fileName);
            }
        }

        public void ResumeHardwareOutput()
        {
            Thread.Sleep(1);
            Console.WriteLine("resuming hardware: " + DateTime.UtcNow.ToString("HH:mm:ss.ffffff"));
            _hardwareBuffer.Resume();
        }

        public void CopyToBuffer()
        {
            string data = JSON.Instance.ToJSON(_model);
            _hardwareBuffer.UpdateData(data);
        }

        /// <summary>
        /// Saves Values for UNDO
        /// </summary>
        public void StoreSyncedValues()
        {
            foreach (ControllerPattern pattern in Patterns)
            {
                pattern.StoreSyncedValues();
            }
        }

        /// <summary>
        /// Restores Values for UNDO
        /// </summary>
        public void RestoreSyncedValues()
        {
            foreach (ControllerPattern pattern in Patterns)
            {
                pattern.RestoreSyncedValues();
            }
        }

        public void RunChanged()
        {
            TriggerEvent(RunDataChanged);
        }

        private void TriggerEvent(EventHandler newEvent, EventArgs e = null)
        {
            EventHandler triggerEvent = newEvent;
            if (triggerEvent != null)
                triggerEvent(this, e);
        }

        public void Connect()
        {
            _tcpClient = new Client("DigitalCard");
            _tcpClient.RunTriggered += delegate { ResumeHardwareOutput(); };
            _tcpClient.DataReceived += delegate { PropertyChangedEvent("CyclesPerRun"); };
            _hardwareBuffer.ReadyForNextRun += delegate { EverythingPreparedForNextRun(); };
            _tcpClient.Connect(IPAddress.Parse(Ip), Port);
            
            RunsDone = 0;
        }

        public void EverythingPreparedForNextRun()
        {
            _tcpClient.ReadyForNextRun();
        }

        private void StartTriggerMode()
        {
            _tcpClient.StartLoop();
            _tcpClient.ReadyForNextRun();
        }

        public void Disconnect()
        {            
            _tcpClient.Disconnect();
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

        public event EventHandler RunDataChanged;

        public event EventHandler UpdateDataForNextRun;

        #region Nested type: GuiUpdate

        private delegate void GuiUpdate(string propertyName);

        #endregion
    }
}
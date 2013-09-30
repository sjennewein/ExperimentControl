using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using APDTrigger.Hardware;

namespace APDTrigger_WinForms.Helper
{
    public class Controller : INotifyPropertyChanged
    {
        #region RunType enum

        public enum RunType
        {
            Measurement,
            Monitor
        };

        #endregion

        private const string _myBaseSaveFolder = "d:\\Manipe\\APD\\";

        private readonly object _listPadlock = new object();
        private readonly List<AgingDataPoint> _myDataList = new List<AgingDataPoint>();

        private readonly Control _myGUI;
        public RunType Run = RunType.Monitor;
        private int _atoms;
        private int _cyclesDone = 1;
        private bool _isRunning;
        private int[] _myBinnedSpectrum;

        private Counter _myCounterHardware;
        private int[] _myHistogramData = new int[600];
        private bool _mySaveApdSignal;
        private int[] _mySpectrum;
        private TcpDataTrigger _myTcpDataTrigger;
        private Thread _myWorker;
        private int _noAtoms;
        private double _recapturerate;
        private int _runsDone = 1;
        private string _saveFolder;
        private DateTime _today = DateTime.Now;
        private StreamWriter writer;

        public Controller(Control gui)
        {
            _myGUI = gui;
            _saveFolder = _myBaseSaveFolder + _today.Year + "\\" + _today.Month + "\\" + _today.Day + "\\";
            _myTcpDataTrigger = new TcpDataTrigger();
        }

        public bool IsRunning
        {
            get { return _isRunning; }
        }

        //Trigger parameters
        public int Binning { get; set; }
        public int DetectionBins { get; set; }
        public int Threshold { get; set; }
        public int TotalRuns { get; set; }
        public int Cycles { get; set; }

        //Acquisition parameters
        public int Samples2Acquire { get; set; }
        public int APDBinsize { get; set; }
        public bool Recapture { get; set; }

        //Results/Statistics
        public int CyclesDone
        {
            get { return _cyclesDone; }
        }

        public int RunsDone
        {
            get { return _runsDone; }
        }

        public double RecaptureRate
        {
            get { return _recapturerate; }
        }

        public int NoAtoms
        {
            get { return _noAtoms; }
        }

        public int Atoms
        {
            get { return _atoms; }
        }

        public int[] BinnedSpectrum
        {
            get { return _myBinnedSpectrum; }
        }

        public bool SaveApdSignal
        {
            get { return _mySaveApdSignal; }
            set
            {
                if (value == false)
                {
                    if (writer != null)
                    {
                        writer.Close();
                        writer = null;
                    }
                }

                _mySaveApdSignal = value;
            }
        }

        public bool SaveSpectrum { get; set; }

        public int Data
        {
            get { return _myCounterHardware.NewSample; }
        }

        public int[] HistogramData
        {
            get { return _myHistogramData; }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void UpdateFolder()
        {
            DateTime now = DateTime.Now;
            if (_today.Date != now.Date)
            {
                _today = now;
                _saveFolder = _myBaseSaveFolder + _today.Year + "\\" + _today.Month + "\\" + _today.Day + "\\";
            }
        }

        public void Start()
        {
            bool monitorMode = (Run == RunType.Monitor); //set endless true if run type is endless
            _atoms = 0;
            _noAtoms = 0;
            _recapturerate = 0;
            _runsDone = 1;
            _cyclesDone = 0;

            _myCounterHardware = new Counter(Threshold, DetectionBins, APDBinsize, Binning, monitorMode, Recapture,
                                             Samples2Acquire);
            _myCounterHardware.Finished += OnFinished;
            _myCounterHardware.NewData += OnNewData;
            _myCounterHardware.CycleFinished += OnCyleDone;
            _myCounterHardware.RecaptureMeasurementDone += OnRecaptureDone;
            //_myCounterHardware.EmergencyStop += OnEmergencyStop;

            _myWorker = new Thread(BackgroundWork);
            _myWorker.Name = "Worker";
            _myWorker.Start();

            _isRunning = true;
        }

        //initializing the card is done in a separate thread otherwise the GUI lags from time to time
        private void BackgroundWork()
        {
            _myCounterHardware.AimTrigger();
            _myCounterHardware.PrepareAcquisition();
            _myCounterHardware.StartMeasurement();
        }

        public void Stop()
        {
            
            _myCounterHardware.StopMeasurement();
            if (_mySaveApdSignal)
            {
                writer.Flush();
            }
            _isRunning = false;
        }

        public void Quit()
        {
            if (IsRunning)
                Stop();
            _myTcpDataTrigger.Stop();
            
        }

        private void SaveApdData()
        {
            if (DateTime.Now != _today)
            {
                UpdateFolder();
                writer.Close();
                writer = null;
            }

            if (writer == null)
            {
                Directory.CreateDirectory(_saveFolder);
                writer = new StreamWriter(_saveFolder + "ApdSignal.txt", true);
            }
            writer.WriteLine(Data);
        }

        private void OnFinished(object sender, EventArgs e)
        {
            EventHandler finished = Finished;
            if (null != finished)
                finished(this, new EventArgs());
        }

        private void OnNewData(object sender, EventArgs e)
        {
            AddHistogramData();
            if (SaveApdSignal)
            {
                SaveApdData();
            }
        }

        private void OnCyleDone(object sender, EventArgs e)
        {
            if (_cyclesDone > Cycles) //if all cycles per run are done increment runs
            {
                _cyclesDone = 0;


                if (SaveSpectrum)
                    SaveRunSpectrum();

                _mySpectrum = null;
                _myBinnedSpectrum = null;

                _runsDone++;
                OnPropertyChanged("RunsDone");

                EventHandler runDone = RunDone;
                if (null != runDone)
                {
                    var runData = new RunEventData(_runsDone, _recapturerate, _atoms, _noAtoms);
                    runDone(this, runData);
                }


                if (_runsDone > TotalRuns) //if all runs are done stop the run
                {
                    Stop();
                    return;
                }
            }

            AddSpectrum();
            AddBinnedSpectrum();

            EventHandler cycleDone = CycleDone;
            if (null != cycleDone)
                cycleDone(this, new EventArgs());


            _myTcpDataTrigger.Data = new NetworkData(_atoms, _noAtoms, _cyclesDone, _runsDone, TotalRuns, _recapturerate );

            _cyclesDone++;
            OnPropertyChanged("CyclesDone");
        }

        private void AddSpectrum()
        {
            int amountOfSamples = _myCounterHardware.Spectrum.Length;
            if (_mySpectrum == null)
                _mySpectrum = new int[amountOfSamples];

            for (int i = 0; i < amountOfSamples; i++)
            {
                _mySpectrum[i] += _myCounterHardware.Spectrum[i];
            }
        }

        private void AddBinnedSpectrum()
        {
            int amountOfBins = _myCounterHardware.BinnedSpectrum.Length;
            if (_myBinnedSpectrum == null)
                _myBinnedSpectrum = new int[amountOfBins];

            for (int i = 0; i < amountOfBins; i++)
            {
                _myBinnedSpectrum[i] += _myCounterHardware.BinnedSpectrum[i];
            }
        }

        private void SaveRunSpectrum()
        {
            if (DateTime.Now != _today)
            {
                UpdateFolder();
            }
            DateTime now = DateTime.Now;
            Directory.CreateDirectory(_saveFolder);
            var spectrumWriter =
                new StreamWriter(
                    _saveFolder + "Spectrum_" + now.Hour + "-" + now.Minute + "-" + now.Second + "_Run_" + RunsDone +
                    ".txt", true);
            foreach (int bin in _mySpectrum)
            {
                spectrumWriter.WriteLine(bin);
            }
            spectrumWriter.Close();
        }

        private void OnRecaptureDone(object sender, EventArgs e)
        {
            var result = (CycleEventData) e;

            switch (result.Data)
            {
                case CycleEventData.RecaptureType.Captured:
                    _atoms++;
                    break;
                case CycleEventData.RecaptureType.Lost:
                    _noAtoms++;
                    break;
            }
            _recapturerate = (double) _atoms/_cyclesDone;
            OnPropertyChanged("Atoms");
            OnPropertyChanged("NoAtoms");
            OnPropertyChanged("RecaptureRate");
        }

        private void AddHistogramData()
        {
            var lifetime = new TimeSpan(0, 0, 0, 10);
            var newDataPoint = new AgingDataPoint(Data, lifetime, _myDataList);
            lock (_listPadlock)
            {
                _myDataList.Add(newDataPoint);
            }
        }

        public void UpdateHistogramData()
        {
            //arbitrary chosen 600 bins 
            const int bucketNumber = 600;
            const int interval = 5; // 5 counts per bucket => max 3000 counts

            var buckets = new int[bucketNumber + 1]; // extra bucket is for the rubbish


            //bucket sort
            lock (_listPadlock)
            {
                for (int iPoint = 0; iPoint < _myDataList.Count; iPoint++)
                {
                    _myDataList[iPoint].CheckLifetime(); //call this only when locked and not with foreach
                }

                foreach (AgingDataPoint dataPoint in _myDataList)
                {
                    if (dataPoint.Value/interval > bucketNumber)
                    {
                        buckets[bucketNumber + 1]++; //put too big values in the trash
                    }
                    else
                    {
                        buckets[dataPoint.Value/interval]++;
                    }
                }
            }
            _myHistogramData = buckets;
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (_myGUI.InvokeRequired)
            {
                GuiUpdate callback = OnPropertyChanged;
                _myGUI.Invoke(callback, propertyName);
            }
            else
            {
                PropertyChangedEventHandler propertyChanged = PropertyChanged;
                if (null != propertyChanged)
                    propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public event EventHandler Finished;

        //public event EventHandler NewData;

        public event EventHandler CycleDone;

        public event EventHandler RunDone;

        #region Nested type: GuiUpdate

        private delegate void GuiUpdate(string propertyName);

        #endregion
    }
}
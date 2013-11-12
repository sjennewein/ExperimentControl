using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using APDTrigger.Hardware;
using ColdNetworkStack.Server;

namespace APDTrigger_WinForms.Helper
{
    public class Controller : INotifyPropertyChanged
    {
        #region RunType enum

        public enum RunType
        {
            Network,
            Measurement,
            Monitor
        };

        #endregion

        #region private variables

        private const string _BaseSaveFolder = "d:\\Manipe\\APD\\";

        private readonly object _HistogramDataLock = new object();
        private readonly List<AgingDataPoint> _histogramDataPoints = new List<AgingDataPoint>();


        private readonly Control _myGUI;
        private readonly Server _tcpServer;
        public RunType Mode = RunType.Monitor;
        private int[] _Spectrum;
        private Thread _Worker;
        private int _atoms;
        private int[] _binnedSpectrumData;
        private int _cyclesDone = 1;


        private int[] _histogramData = new int[600];
        private Counter _myCounterHardware;
        private int _noAtoms;
        private double _recapturerate;
        private int _runsDone = 1;
        private bool _saveApdSignal;
        private string _saveFolder;
        private DateTime _today = DateTime.Now;
        private StreamWriter writer;

        #endregion

        public Controller(Control gui)
        {
            _myGUI = gui;
            _saveFolder = _BaseSaveFolder + _today.Year + "\\" + _today.Month + "\\" + _today.Day + "\\";
            _tcpServer = new Server(IPAddress.Any, 9898);
            _tcpServer.AllClientsAreReady += NextRun;
        }

        #region BindingProperties

        //Trigger parameters
        public int Binning { get; set; }
        public int DetectionBins { get; set; }
        public int Threshold { get; set; }
        public int TotalRuns { get; set; }
        public int Cycles { get; set; }

        //Acquisition parameters
        public int Samples2Acquire { get; set; }
        public int APDBinsize { get; set; }

        //FrequencyGenerator
        public double Frequency { get; set; }

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
            get { return _binnedSpectrumData; }
        }

        public bool SaveApdSignal
        {
            get { return _saveApdSignal; }
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

                _saveApdSignal = value;
            }
        }

        public bool SaveSpectrum { get; set; }

        public int Data
        {
            get { return _myCounterHardware.NewDataPoint; }
        }

        public int[] HistogramData
        {
            get { return _histogramData; }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /// <summary>
        /// Creates a new folder for saving data if date changed
        /// </summary>
        private void UpdateSaveFolder()
        {
            DateTime now = DateTime.Now;
            if (_today.Date != now.Date)
            {
                _today = now;
                _saveFolder = _BaseSaveFolder + _today.Year + "\\" + _today.Month + "\\" + _today.Day + "\\";
            }
        }

        /// <summary>
        /// Initializes the counter with the parameters from the GUI and starts the counter
        /// </summary>
        public void Start(bool runFrequencyGenerator = false)
        {
            Initialize();
            //_myCounterHardware.EmergencyStop += OnEmergencyStop;

            _Worker = new Thread(BackgroundWork) {Name = "Worker"};
            _Worker.Start();

            if (runFrequencyGenerator)
                _myCounterHardware.StartFrequencyGenerator();
            //_isRunning = true;
        }

        private void Initialize()
        {
            bool monitorMode = (Mode == RunType.Monitor); //enable monitor mode if user selected that
            _atoms = 0;
            _noAtoms = 0;
            _recapturerate = 0;
            _runsDone = 0;
            _cyclesDone = 0;
            _Spectrum = null;
            _binnedSpectrumData = null;
            _histogramData = new int[600];

            _myCounterHardware = new Counter(Threshold, DetectionBins, APDBinsize, Binning, monitorMode,
                                             Samples2Acquire, Frequency);
            _myCounterHardware.APDStopped += OnApdStoppedEvent;
            _myCounterHardware.NewAPDValue += OnNewApdValue;
            _myCounterHardware.CycleFinished += OnCyleFinished;

            if(Mode == RunType.Network)
                _myCounterHardware.Pause();
        }

        //initializing the card is done in a separate thread otherwise the GUI lags from time to time
        /// <summary>
        /// Is used for multi-threading purposes
        /// </summary>
        private void BackgroundWork()
        {
            _myCounterHardware.AimTrigger();
            _myCounterHardware.PrepareAcquisition();
            _myCounterHardware.InitializeMeasurementTimer();
        }

        /// <summary>
        /// Stops the counter card gracefully
        /// </summary>
        public void Stop()
        {
            if (_myCounterHardware != null)
            {
                _myCounterHardware.StopAPDTrigger();
                _myCounterHardware.Resume();
            }
                

            if (_saveApdSignal)
            {
                writer.Flush();
            }
        }

        /// <summary>
        /// Is called when the window is closing
        /// </summary>
        public void Quit()
        {
            Stop();


            _tcpServer.Stop();
        }

        /// <summary>
        /// Saves the signal from the apd
        /// </summary>
        private void SaveApdData()
        {
            if (DateTime.Now.Date != _today.Date)
            {
                UpdateSaveFolder();
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

        private void OnNewApdValue(object sender, EventArgs e)
        {
            AddHistogramData();
            if (SaveApdSignal)
            {
                SaveApdData();
            }
        }

        private void NextRun(object sender, EventArgs e)
        {
            _tcpServer.StartNextRun();
            _myCounterHardware.Resume();
        }

        /// <summary>
        /// Updates the values for the cycle counter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCyleFinished(object sender, EventArgs e)
        {
            var data = (RecaptureResult) e;

            if (_cyclesDone >= Cycles) //check if the whole run has finished
            {
                _cyclesDone = 0;
                _noAtoms = 0;
                _atoms = 0;
                _recapturerate = 0;

                if (SaveSpectrum)
                    SaveRunSpectrum();

                _Spectrum = null;
                _binnedSpectrumData = null;

                OnRunDoneEvent();

                //_myTcpServer.SetTriggerData(
                //  new NetworkData(_atoms, _noAtoms, _cyclesDone, _runsDone, TotalRuns, _recapturerate).Serialize());

                //stop the counter for a certain amount of time
                if (Mode == RunType.Network)
                    _myCounterHardware.Pause();

                _runsDone++;
                PropertyChangedEvent("RunsDone");


                if (_runsDone >= TotalRuns) //if all runs are done stop the run
                {
                    Stop();
                    return;
                }
            }

            UpdateSpectrumDatePoint();
            UpdateBinnedSpectrum();

            UpdateRecaptureResult(data);

            _cyclesDone++;
            PropertyChangedEvent("CyclesDone");
        }

        /// <summary>
        /// Adds the spectrum data from the current cycle 
        /// </summary>
        private void UpdateSpectrumDatePoint()
        {
            int amountOfSamples = _myCounterHardware.Spectrum.Length;
            if (_Spectrum == null)
                _Spectrum = new int[amountOfSamples];

            for (int i = 0; i < amountOfSamples; i++)
            {
                _Spectrum[i] += _myCounterHardware.Spectrum[i];
            }
        }

        /// <summary>
        /// Adds the binned spectrum from the current cycle
        /// </summary>
        private void UpdateBinnedSpectrum()
        {
            int amountOfBins = _myCounterHardware.BinnedSpectrum.Length;
            if (_binnedSpectrumData == null)
                _binnedSpectrumData = new int[amountOfBins];

            for (int i = 0; i < amountOfBins; i++)
            {
                _binnedSpectrumData[i] += _myCounterHardware.BinnedSpectrum[i];
            }
        }

        /// <summary>
        /// Saves the spectrum of the whole run
        /// </summary>
        private void SaveRunSpectrum()
        {
            if (DateTime.Now.Date != _today.Date)
            {
                UpdateSaveFolder();
            }
            DateTime now = DateTime.Now;
            Directory.CreateDirectory(_saveFolder);
            var spectrumWriter =
                new StreamWriter(
                    _saveFolder + "Spectrum_" + now.Hour + "-" + now.Minute + "-" + now.Second + "_Run_" + RunsDone +
                    ".txt", true);
            foreach (int bin in _Spectrum)
            {
                spectrumWriter.WriteLine(bin);
            }
            spectrumWriter.Close();
        }

        /// <summary>
        /// Updates the values for the recapture rate measurement
        /// </summary>
        private void UpdateRecaptureResult(RecaptureResult result)
        {
            switch (result.Data)
            {
                case RecaptureResult.State.Captured:
                    _atoms++;
                    break;
                case RecaptureResult.State.Lost:
                    _noAtoms++;
                    break;
            }
            _recapturerate = (double) _atoms/_cyclesDone;
            PropertyChangedEvent("Atoms");
            PropertyChangedEvent("NoAtoms");
            PropertyChangedEvent("RecaptureRate");
        }

        /// <summary>
        /// Adds data-point to the histogram 
        /// </summary>
        private void AddHistogramData()
        {
            var lifetime = new TimeSpan(0, 0, 0, 10);
            var newDataPoint = new AgingDataPoint(Data, lifetime, _histogramDataPoints);
            lock (_HistogramDataLock)
            {
                _histogramDataPoints.Add(newDataPoint);
            }
        }

        /// <summary>
        /// Updates the histogram and checks which data-points are expired
        /// </summary>
        public void UpdateHistogramData()
        {
            //arbitrary chosen 600 bins 
            const int bucketNumber = 600;
            const int interval = 5; // 5 counts per bucket => max 3000 counts

            var buckets = new int[bucketNumber + 1]; // extra bucket is for the rubbish


            //bucket sort
            lock (_HistogramDataLock)
            {
                for (int iPoint = 0; iPoint < _histogramDataPoints.Count; iPoint++)
                {
                    _histogramDataPoints[iPoint].CheckLifetime(); //call this only when locked and not with foreach
                }

                foreach (AgingDataPoint dataPoint in _histogramDataPoints)
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
            _histogramData = buckets;
        }

        private void OnRunDoneEvent()
        {
            EventHandler runDone = RunHasFinished;
            if (null != runDone)
            {
                var runData = new RunEventData(_runsDone, _recapturerate, _atoms, _noAtoms);
                runDone(this, runData);
            }
        }

        private void OnApdStoppedEvent(object sender, EventArgs e)
        {
            EventHandler finished = APDStopped;
            if (null != finished)
                finished(this, new EventArgs());
        }

        /// <summary>
        /// Handles the GUI update events with invoking it to the right thread
        /// </summary>
        /// <param name="propertyName"></param>
        private void PropertyChangedEvent(string propertyName)
        {
            if (_myGUI.InvokeRequired)
            {
                GuiUpdate callback = PropertyChangedEvent;
                _myGUI.Invoke(callback, propertyName);
            }
            else
            {
                PropertyChangedEventHandler propertyChanged = PropertyChanged;
                if (null != propertyChanged)
                    propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event EventHandler APDStopped;

        public event EventHandler RunHasFinished;

        #region Nested type: GuiUpdate

        private delegate void GuiUpdate(string propertyName);

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using APDTrigger.Hardware;

namespace APDTrigger_WinForms.Helper
{
    public class Controller
    {
        public enum RunType
        {
            triggered,
            endless
        };

        public RunType Run = RunType.endless;
        private TcpServer test = new TcpServer();
        private readonly object _listPadlock = new object();
        private readonly List<AgingDataPoint> _myDataList = new List<AgingDataPoint>();

        private const string _myBaseSaveFolder = "d:\\Manipe\\APD\\";

        private string _saveFolder;

        private Counter _myCounterHardware;
        private int[] _myHistogramData = new int[600];
        private bool _mySave;
        private Thread _myWorker;
        private DateTime _today = DateTime.Now;
        private StreamWriter writer;
        private bool _isRunning = false;
        
        public bool IsRunning {get { return _isRunning; }}

        //Trigger parameters
        public int Binning { get; set; }
        public int DetectionBins { get; set; }
        public int Threshold { get; set; }
        public int Runs { get; set; }
        public int Cycles { get; set; }

        //Acquisition parameters
        public int Samples2Acquire { get; set; }
        public int APDBinsize { get; set; }
        public bool Recapture { get; set; }

        //Results/Statistics
        public int CyclesDone { get { return _cyclesDone; }}
        private int _cyclesDone;
        public int RunsDone {get { return _runsDone; }}
        private int _runsDone;
        public double RecaptureRate { get { return _recapturerate; } }
        private double _recapturerate;
        public int NoAtoms { get { return _noAtoms; } }
        private int _noAtoms;
        public int Atoms { get { return _atoms; } }
        private int[] _myBinnedSpectrum;
        public int[] BinnedSpectrum { get { return _myBinnedSpectrum; } }

        private int _atoms;

        public bool Save
        {
            get { return _mySave; }
            set
            {
                if (value == false)
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
                else
                {
                    if (writer == null)
                    {
                        _saveFolder = _myBaseSaveFolder + _today.Year + "\\" + _today.Month + "\\" + _today.Day + "\\";
                        Directory.CreateDirectory(_saveFolder);
                        writer = new StreamWriter(_saveFolder + "ApdSignal.txt", true);
                    }
                }
                _mySave = value;
            }
        }

        public bool SaveSpectrum { get; set; }

        private int[] _mySpectrum;

        public int Data
        {
            get { return _myCounterHardware.NewSample; }
        }

        public int[] HistogramData
        {
            get { return _myHistogramData; }
        }

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
            bool endless = (Run == RunType.endless);  //set endless true if run type is endless
            
            _myCounterHardware = new Counter(Threshold, DetectionBins, APDBinsize, Binning, endless, Recapture);
            _myCounterHardware.Finished += OnFinished;
            _myCounterHardware.NewData += OnNewData;
            _myCounterHardware.CycleFinished += OnCyleDone;
            _myCounterHardware.RecaptureMeasurementDone += OnRecaptureDone;
            _myWorker = new Thread(BackgroundWork);
            _myWorker.Name = "Worker";
            _myWorker.Start();

            _isRunning = true;
        }

        //initializing the card is done in a separate thread otherwise the GUI lags from time to time
        private void BackgroundWork()
        {
            _myCounterHardware.AimTrigger(Samples2Acquire);
            _myCounterHardware.PrepareAcquisition(Samples2Acquire);
            _myCounterHardware.StartMeasurement();
            
        }

        public void Stop()
        {
            _myCounterHardware.StopMeasurement();
            if (_mySave)
            {
                writer.Flush();
            }
            _isRunning = false;
        }

        private void SaveData()
        {
            if (DateTime.Now != _today.Date)
            {
                UpdateFolder();
                writer.Close();
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
            if (Save)
            {
                SaveData();
            }
        }

        private void OnCyleDone(object sender, EventArgs e)
        {

            if (_cyclesDone == Cycles)  //if all cycles per run are done increment runs
            {
                _cyclesDone = 0;

                if(SaveSpectrum)
                    SaveRunSpectrum();

                _mySpectrum = null;
                _myBinnedSpectrum = null;
                _runsDone++;

                if (_runsDone > Runs)  //if all runs are done stop the run
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
            
            _cyclesDone++;

        }

        private void AddSpectrum()
        {
            int amountOfSamples = _myCounterHardware.Spectrum.Length;
            if(_mySpectrum == null)
                _mySpectrum = new int[amountOfSamples];

            for (int i = 0; i < amountOfSamples; i++)
            {
                _mySpectrum[i] += _myCounterHardware.Spectrum[i];
            }
        
        }

        private void AddBinnedSpectrum()
        {
            int amountOfBins = _myCounterHardware.BinnedSpectrum.Length;
            if(_myBinnedSpectrum == null)
                _myBinnedSpectrum = new int[amountOfBins];

            for(int i = 0; i< amountOfBins; i++)
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
            var spectrumWriter = new StreamWriter(_saveFolder + "Spectrum_" + RunsDone + ".txt", true);
            foreach (int bin in _mySpectrum)
            {
                spectrumWriter.WriteLine(bin);
            }
            spectrumWriter.Close();

        }

        private void OnRecaptureDone(object sender, EventArgs e)
        {
            object intermediateObject = (object) e;
            Counter.RecaptureType result = (Counter.RecaptureType) intermediateObject;
            switch (result)
            {
                case Counter.RecaptureType.Captured:
                    _atoms++;
                    break;
                case Counter.RecaptureType.Lost:
                    _noAtoms++;
                    break;
            }
            _recapturerate  = (double) _atoms/_cyclesDone;
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

        public event EventHandler Finished;

        public event EventHandler NewData;

        public event EventHandler CycleDone;
    }
}
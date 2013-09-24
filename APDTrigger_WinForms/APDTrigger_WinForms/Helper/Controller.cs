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
        private readonly object _listPadlock = new object();
        private readonly List<AgingDataPoint> _myDataList = new List<AgingDataPoint>();
        private const string _myBaseSaveFolder = "d:\\Manipe\\APD\\";
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
                        string saveFolder = _myBaseSaveFolder + _today.Year + "\\" + _today.Month + "\\" + _today.Day;
                        Directory.CreateDirectory(saveFolder);
                        writer = new StreamWriter(saveFolder + "\\ApdSignal.txt", true);
                    }
                }
                _mySave = value;
            }
        }

        public int Data
        {
            get { return _myCounterHardware.NewSample; }
        }

        public int[] HistogramData
        {
            get { return _myHistogramData; }
        }

        private void UpdateWriter()
        {
            DateTime now = DateTime.Now;
            if (_today.Date != now.Date)
            {
                writer.Close();
                _today = now;
                string saveFolder = _myBaseSaveFolder + _today.Year + "\\" + _today.Month + "\\" + _today.Day;
                writer = new StreamWriter(saveFolder + "\\ApdSignal.txt", true);
            }
        }

        public void Start()
        {
            bool endless = (Run == RunType.endless);  //set endless true if run type is endless
            
            _myCounterHardware = new Counter(Threshold, DetectionBins, APDBinsize, Binning, endless, Recapture);
            _myCounterHardware.Finished += OnFinished;
            _myCounterHardware.NewData += OnNewData;
            _myCounterHardware.CycleFinished += OnCyleDone;
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
            UpdateWriter();
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

            if (_cyclesDone == Cycles)  //if cycles per run are done increment runs
            {
                _cyclesDone = 0;
                _runsDone++;

                if (_runsDone > Runs)  //if all runs are done stop the run
                {
                    Stop();
                    return;
                }
            }

            _cyclesDone++;
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
    }
}
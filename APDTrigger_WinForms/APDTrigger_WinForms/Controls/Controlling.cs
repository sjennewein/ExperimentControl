using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using APDTrigger.Hardware;

namespace APDTrigger_WinForms.Controls
{
    public class Controlling
    {       
        private Counter _myCounterHardware;
        private Thread _myWorker;

        //Trigger parameters
        public int Binning { get; set; }
        public int DetectionBins { get; set; }
        public int Threshold { get; set; }
        public int Runs { get; set; }
        public int Cycles { get; set; }

        //Acquisition parameters
        public int Samples2Acquire { get; set; }
        public int APDBinsize { get; set; }

        //Results/Statistics

        public int RecaptureRate { get; set; }
        public int NoAtoms { get; set; }
        public int Atoms { get; set; }

        public int Data {get { return _myCounterHardware.NewSample; }}

        private int[] _myHistogramData = new int[600];
        private List<AgingDataPoint> _myDataList = new List<AgingDataPoint>();
        private object _listPadlock = new object();
        public int[] HistogramData {get { return _myHistogramData; }}


        public void Start()
        {
            _myCounterHardware = new Counter(Threshold, DetectionBins, APDBinsize);
            _myCounterHardware.Finished += OnFinished;
            _myCounterHardware.NewData += OnNewData;
            _myWorker =  new Thread(BackgroundWork);
            _myWorker.Name = "Worker";
            _myWorker.Start();
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
        }

        private void AddHistogramData()
        {
            TimeSpan lifetime = new TimeSpan(0, 0, 0, 10);
            AgingDataPoint newDataPoint = new AgingDataPoint(Data, lifetime, _myDataList);
            lock (_listPadlock)
            {
                _myDataList.Add(newDataPoint);    
            }
            
        }

        public void UpdateHistogramData()
        {
            //arbitrary chosen 600 bins 
            const int bucketNumber = 60;
            const int interval = 3; // 5 counts per bucket => max 3000 counts

            int[] buckets = new int[bucketNumber + 1]; // extra bucket is for the rubbish

            

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
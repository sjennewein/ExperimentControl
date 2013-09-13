using System;
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

        public double[] Data {get { return _myCounterHardware.Samples; }}

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
            EventHandler UpdateData = NewData;
            if(null != UpdateData)
                UpdateData(this, new EventArgs());
        }

        public event EventHandler Finished;

        public event EventHandler NewData;

    }
}
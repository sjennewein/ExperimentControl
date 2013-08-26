using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NationalInstruments.DAQmx;


namespace APDTrigger.Hardware
{
    public class Counter
    {
        private readonly Task _myTask;
        private CounterReader _myCounterReader;
        private AsyncCallback _myCallBack;
        private Task _runningTask;
        private double[] _samples;

        public Counter()
        {
            _myTask = new Task();
        }

        public void AimTrigger(int samples2acquire)
        {
            //minimum and maximum counter value are ignored in this measurement mode
            //measurement time is in seconds
            //divisor has to be 4 copied from documentation

            const double minValue = 0;
            const double maxValue = 0;
            const long divisor = 4;
            const double measurementTime = 0.01;
            const CIPeriodStartingEdge edge = CIPeriodStartingEdge.Rising;
                                     
            _myTask.CIChannels.CreatePeriodChannel("", "", minValue, maxValue, edge,
                                                        CIPeriodMeasurementMethod.HighFrequencyTwoCounter, measurementTime, divisor,
                                                        CIPeriodUnits.Ticks);
            
            _myTask.Timing.ConfigureImplicit(SampleQuantityMode.ContinuousSamples,samples2acquire);
            
            _runningTask = _myTask;

            _myCounterReader = new CounterReader(_myTask.Stream);
            _myCounterReader.SynchronizeCallbacks = true;
            
            _myCallBack = new AsyncCallback(CounterInCallback);
            _samples = new double[samples2acquire];
            

            //_myCounterReader.BeginMemoryOptimizedReadMultiSampleDouble(samples2acquire, _myCallBack, _myTask, _samples);
        }

        private void CounterInCallback(IAsyncResult ar)
        {
            

            try
            {
                if(_runningTask != null && _runningTask == ar.AsyncState)
                {}
            }
            catch(DaqException exception)
            {
                _myTask.Dispose();
                _runningTask = null;
            }
        }

        public void InitializeRecapture()
        {
            
        }
    }
}

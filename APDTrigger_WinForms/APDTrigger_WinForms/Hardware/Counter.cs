using System;
using System.Threading;
using APDTrigger_WinForms.Helper;
using NationalInstruments.DAQmx;

namespace APDTrigger.Hardware
{
    /************************************************************************/
    /*                                                                      */
    /* This class is for initializing the counter devices                   */
    /* used in our cold atom experiment.                                    */
    /* For starting an experiment one counter is measuring                  */
    /* the amount of pulses send from the APD. If those                     */
    /* reach a certain threshold we assume that we have an                  */
    /* atom trapped and send a start trigger to the other                   */
    /* cards.                                                               */
    /* At the end of a run normally we do a release and                     */
    /* recapture measurement. For that we use a 1MHz external               */
    /* frequency generator connected to one of the                          */
    /* counter. This clock samples the counter with 1us                     */
    /* into the buffer. With this you get the total amount of               */
    /* photons over time => the derivative gives the                        */
    /* amount of photons at that time point.                                */
    /*                                                                      */
    /************************************************************************/

    public class Counter
    {
        private readonly int _apdBinSize;
        private readonly int _detectionBins;
        private readonly object _lockExperiment = new object();
        private readonly bool _monitor;
        private readonly Task _myAcquisitionTask;
        private readonly int _myClockEdges;
        private readonly bool _myRecapture;
        private readonly Task _myThresholdTask;
        private readonly Task _myTriggerTask;
        private readonly double _threshold;
        private readonly double _triggerBin;
        private readonly Random rand = new Random(17);
        private int _NewSample;
        private int _detectedBins;
        private int[] _myAcquiredData;
        private int[] _myBinnedSpectrum;
        private int[] _mySpectrum;
        private Timer _myTimer;
        private bool _running;

        /// <summary>
        ///     Provides the functionality of a standard ASPHERIX experiment in terms of the counter card
        /// </summary>
        /// <param name="threshold">The value from the APD above you think you have an atom in the trap</param>
        /// <param name="detectionBins">How often this value has to appear in a row before the trigger is send</param>
        /// <param name="apdBinSize">The number of original datapoints per bin (normaly we sample with 1us and want to have 10ms)</param>
        /// <param name="triggerBin">Measurement length for the high-frequency counter in milliseconds</param>
        public Counter(double threshold, int detectionBins, int apdBinSize, double triggerBin, bool monitor,
                       bool recapture, int clockEdges)
        {
            _myThresholdTask = new Task();
            _myTriggerTask = new Task();
            _myAcquisitionTask = new Task();
            _threshold = threshold;
            _detectionBins = detectionBins;
            _apdBinSize = apdBinSize;
            _triggerBin = triggerBin/1000.0;
            _monitor = monitor;
            _myRecapture = recapture;
            _myClockEdges = clockEdges;
        }

        public int NewSample
        {
            get { return _NewSample; }
        }

        public int[] Spectrum
        {
            get { return _mySpectrum; }
        }

        public int[] BinnedSpectrum
        {
            get { return _myBinnedSpectrum; }
        }


        /// <summary>
        ///     Initialize the threshold measurement and the trigger which will be send if it's successful.
        /// </summary>
        /// <param name="samples2Acquire">
        ///     Define the amount of read samples same value as for PrepareAcquisition (in continuous mode
        ///     it's the buffer size)
        /// </param>
        public void AimTrigger()
        {
            //minimum and maximum counter values are ignored in this measurement mode, but shouldn't be the same
            //measurement time is in seconds
            //divisor has to be 4 copied from documentation

            const double minValue = 0;
            const double maxValue = 5;
            const long divisor = 4;
            const CIPeriodStartingEdge edge = CIPeriodStartingEdge.Rising;

            _myThresholdTask.CIChannels.CreatePeriodChannel("/Dev1/ctr1", "", minValue, maxValue, edge,
                                                            CIPeriodMeasurementMethod.HighFrequencyTwoCounter,
                                                            _triggerBin, divisor,
                                                            CIPeriodUnits.Ticks);

            _myThresholdTask.CIChannels.All.DuplicateCountPrevention = true;
            _myThresholdTask.Timing.ConfigureImplicit(SampleQuantityMode.ContinuousSamples, _myClockEdges);


            /*************************************************************************************/
            /*     The trigger is fired when the read in counts are over a certain threshold     */
            /*************************************************************************************/

            _myTriggerTask.COChannels.CreatePulseChannelTicks("/Dev1/ctr2", "", "", COPulseIdleState.Low, 0, 200, 200);
            _myTriggerTask.Timing.ConfigureImplicit(SampleQuantityMode.FiniteSamples, 1);
        }

        /// <summary>
        ///     Initialize the second counter for the release and recapture measurement
        /// </summary>
        /// <param name="clockEdges">Define the amount of clock-cycles to acquire (will fail if it gets less)</param>
        public void PrepareAcquisition()
        {
            const CICountEdgesActiveEdge edge = CICountEdgesActiveEdge.Rising;
            const CICountEdgesCountDirection countDirection = CICountEdgesCountDirection.Up;
            const SampleClockActiveEdge clockActiveEdge = SampleClockActiveEdge.Rising;

            _myAcquisitionTask.CIChannels.CreateCountEdgesChannel("/Dev1/ctr0", "", edge, 0, countDirection);

            _myAcquisitionTask.CIChannels.All.DuplicateCountPrevention = true;

            _myAcquisitionTask.Timing.ConfigureSampleClock("/Dev1/PFI0", 1000000.0, clockActiveEdge,
                                                           SampleQuantityMode.FiniteSamples, _myClockEdges);
        }

        /// <summary>
        ///     Set the timer with which the experiment is monitored
        /// </summary>
        public void StartMeasurement()
        {
            if (_running == false)
            {
                _myTimer = new Timer(RunExperiment, null, 0, 10);

                _running = true;
            }
        }

        /// <summary>
        ///     Stop the experiment
        /// </summary>
        public void StopMeasurement()
        {
            if (_running)
            {
                _myTimer.Change(Timeout.Infinite, Timeout.Infinite);

                Thread.Sleep(30); //wait until all timer processes are finished
                _myTimer.Dispose();

                _myTriggerTask.Stop();
                _myThresholdTask.Stop();
                _myAcquisitionTask.Stop();

                //send finishing event
                EventHandler finished = Finished;
                if (null != finished)
                    finished(this, new EventArgs());
                _running = false;
            }
        }

        private void RunExperiment(object state)
        {
            
            //keeps on reading data every 10ms but only evaluates it if no other experiment is running
            if (Monitor.TryEnter(_lockExperiment))
            {
                try
                {
                    ReadThresholdCounter();                    
                    PerformAcquisition();

                    if (_monitor) //if we only monitor then ignore all fancy measurement functions
                        return;

                    //check if the value read from the counter is bigger than the set threshold
                    if (_NewSample >= _threshold)
                        _detectedBins++;
                    else
                        _detectedBins = 0;

                    //check if enough bins have been over the threshold => atom in the trap
                    if (_detectedBins >= _detectionBins)
                    {
                        _myThresholdTask.Stop();

                        _detectedBins = 0;

                        //send a trigger to the digital output card
                        _myTriggerTask.Start();
                        _myTriggerTask.Stop();

                        PerformAcquisition();
                        EvaluateRecapture();                        
                    }

                    Thread.Sleep(20);
                }
                finally
                {
                    Monitor.Exit(_lockExperiment);
                }
            }
        }

        private void ReadThresholdCounter()
        {
            _myThresholdTask.Start();
            var thresholdReader = new CounterReader(_myThresholdTask.Stream);
            int[] readOutData = thresholdReader.ReadMultiSampleInt32(2); //read 2 elements because the first one is mostly zero   
            _myThresholdTask.Stop();

            if (readOutData.Length >= 1)
            {
                _NewSample = readOutData[1]; //use only the last value from the buffer and throw everything else away
                                             //in Monitor mode the 0st element in the array is mostly zero so we use the 1st
                //for (int i = 0; i < readOutData.Length; i++)
                //    Console.WriteLine(i + ": " + readOutData[i]);

                EventHandler dataUpdate = NewData;
                if (null != dataUpdate)
                    dataUpdate(this, new EventArgs());
            }


            //_NewSample = 1000 + rand.Next(0,1000);
        }

        private void PerformAcquisition()
        {
            _myAcquisitionTask.Start();
            var acquisitionReader = new CounterReader(_myAcquisitionTask.Stream);
            var data = new int[_myClockEdges];
            int readOutSamples = 0;
            acquisitionReader.MemoryOptimizedReadMultiSampleInt32(_myClockEdges, ref data,
                                                                  ReallocationPolicy.DoNotReallocate, out readOutSamples);
            
            _myAcquisitionTask.Stop();
            _myAcquiredData = data;


            var derivedSamples = new int[_myAcquiredData.Length - 1];

            //the counter spits out an integrated photon value so we have to derive this
            for (int counter = 1; counter < _myAcquiredData.Length; counter++)
            {
                derivedSamples[counter - 1] = _myAcquiredData[counter] - _myAcquiredData[counter - 1];
            }

            _mySpectrum = derivedSamples;

            //1us is to fine grain for most of our stuff so we have to bin the acquired data
            var bins = (int)Math.Ceiling(derivedSamples.Length / (double)_apdBinSize);

            _myBinnedSpectrum = bin(derivedSamples, bins);

            CycleDone();
        }

        private void EvaluateRecapture()
        {
            EventData.RecaptureType result = EventData.RecaptureType.Lost;

            if (_myBinnedSpectrum[1] + _myBinnedSpectrum[2] > 2*_threshold)
                result = EventData.RecaptureType.Captured;

            RecaptureDone(result);
        }

        /// <summary>
        ///     bin APD data into bins
        /// </summary>
        private int[] bin(int[] data, int bins)
        {
            int binCounter = 0;

            var binnedData = new int[bins];

            for (int binStep = 0; binStep < data.Length; binStep += _apdBinSize)
            {
                int tempData = 0;
                for (int counter = 0; counter < _apdBinSize; counter++)
                {
                    if (binStep == 0 && counter == 0) //remove first 10 bins they might be rubbish
                        counter += 10;

                    if (binStep + counter > data.Length - 1)
                        break;

                    tempData += data[binStep + counter];
                }
                binnedData[binCounter] = tempData;
                binCounter++;
            }
            return binnedData;
        }

        private void CycleDone()
        {
            EventHandler cycleFinished = CycleFinished;
            if (null != cycleFinished)
                cycleFinished(this, new EventArgs());
        }

        private void RecaptureDone(EventData.RecaptureType recapture)
        {
            EventHandler recaptureDone = RecaptureMeasurementDone;
            var e = new EventData();
            e.Data = recapture;

            if (null != recaptureDone)
                recaptureDone(this, e);
        }

        public event EventHandler Finished;
        public event EventHandler NewData;
        public event EventHandler CycleFinished;
        public event EventHandler RecaptureMeasurementDone;
    }
}
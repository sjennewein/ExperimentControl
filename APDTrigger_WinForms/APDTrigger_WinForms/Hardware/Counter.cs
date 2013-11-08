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
        private readonly Task _mySampleClock;
        private readonly Task _myThresholdTask;
        private readonly Task _myTriggerTask;
        private readonly Task _myFrequencyGenerator;
        private readonly ManualResetEvent _pauseCycling = new ManualResetEvent(true);
        private readonly double _threshold;
        private readonly double _triggerBin;

        private int _detectedBins;
        private bool _finalized;
        private int[] _myAcquiredData;
        private int[] _myBinnedSpectrum;
        private int[] _mySpectrum;
        private Timer _myTimer;
        private int _newDataPoint;
        private volatile bool _running;
        private readonly double _frequency;

        /// <summary>
        ///     Provides the functionality of a standard ASPHERIX experiment in terms of the counter card
        /// </summary>
        /// <param name="threshold">The value from the APD above you think you have an atom in the trap</param>
        /// <param name="detectionBins">How often this value has to appear in a row before the trigger is send</param>
        /// <param name="apdBinSize">The number of original datapoints per bin (normaly we sample with 1us and want to have 10ms)</param>
        /// <param name="triggerBin">Measurement length for the high-frequency counter in milliseconds</param>
        /// <param name="monitor"> </param>        
        /// <param name="clockEdges"> </param>
        public Counter(double threshold, int detectionBins, int apdBinSize, double triggerBin, bool monitor,
                       int clockEdges, double frequency)
        {
            _myThresholdTask = new Task("ThresholdTask");
            _myTriggerTask = new Task("TriggerTask");
            _myAcquisitionTask = new Task("AcquisitionTask");
            _mySampleClock = new Task("SampleClockTask");
            _myFrequencyGenerator = new Task("FrequencyGenerator");
            _frequency = frequency;
            _threshold = threshold;
            _detectionBins = detectionBins;
            _apdBinSize = apdBinSize;
            _triggerBin = triggerBin/1000.0; //_triggerBin is needed in seconds triggerBin comes in ms 
            _monitor = monitor;
            _myClockEdges = clockEdges;
        }

        public int NewDataPoint
        {
            get { return _newDataPoint; }
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
        /// Initialize the threshold measurement and the trigger which will be send to the digital card
        /// </summary>
        /// <param name="samples2Acquire">
        /// Define the amount of read samples same value as for PrepareAcquisition (in continuous mode
        /// it's the buffer size)
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

            _myThresholdTask.CIChannels.CreatePeriodChannel("/Dev3/ctr2", "Threshold", minValue, maxValue, edge,
                                                            CIPeriodMeasurementMethod.HighFrequencyTwoCounter,
                                                            _triggerBin, divisor,
                                                            CIPeriodUnits.Ticks);

            _myThresholdTask.CIChannels.All.DuplicateCountPrevention = true;
            _myThresholdTask.Timing.ConfigureImplicit(SampleQuantityMode.ContinuousSamples, _myClockEdges);


            /*************************************************************************************/
            /*     The trigger is fired when the read in counts are over a certain threshold     */
            /*************************************************************************************/

            _myTriggerTask.COChannels.CreatePulseChannelTicks("/Dev3/ctr1", "Trigger", "", COPulseIdleState.Low, 0, 200, 200);
            _myTriggerTask.Timing.ConfigureImplicit(SampleQuantityMode.FiniteSamples, 1);
        }

        /// <summary>
        /// Initialize the counter for the release and recapture measurement
        /// </summary>
        /// <param name="clockEdges">Define the amount of clock-cycles to acquire (will fail if it gets less)</param>
        public void PrepareAcquisition()
        {
            const CICountEdgesActiveEdge edge = CICountEdgesActiveEdge.Rising;
            const CICountEdgesCountDirection countDirection = CICountEdgesCountDirection.Up;
            const SampleClockActiveEdge clockActiveEdge = SampleClockActiveEdge.Rising;

            _myAcquisitionTask.CIChannels.CreateCountEdgesChannel("/Dev3/ctr3", "Acquisition", edge, 0, countDirection);

            _myAcquisitionTask.CIChannels.All.DuplicateCountPrevention = true;

            _myAcquisitionTask.CIChannels.All.DataTransferMechanism = CIDataTransferMechanism.Dma;


            _myAcquisitionTask.Timing.ConfigureSampleClock("/Dev3/PFI12", 1000000, clockActiveEdge,
                                                           SampleQuantityMode.FiniteSamples, _myClockEdges);
            InitializeSampleClock();
        }

        private void InitializeSampleClock()
        {
            //create 1MHz sampling clock internally
            _mySampleClock.COChannels.CreatePulseChannelFrequency("/Dev3/ctr0", "ClkOutput",
                                                                      COPulseFrequencyUnits.Hertz, COPulseIdleState.Low,
                                                                      0, 1000000, 0.5);
            _mySampleClock.Triggers.PauseTrigger.ConfigureDigitalLevelTrigger("/Dev3/PFI9", DigitalLevelPauseTriggerCondition.Low);
            _mySampleClock.Timing.ConfigureImplicit(SampleQuantityMode.ContinuousSamples);
        }

        public void StartFrequencyGenerator()
        {
            _myFrequencyGenerator.COChannels.CreatePulseChannelFrequency("/Dev3/ctr1", "Trigger",
                                                                         COPulseFrequencyUnits.Hertz,
                                                                         COPulseIdleState.Low, 0, _frequency, 0.5);
            _myFrequencyGenerator.Timing.ConfigureImplicit(SampleQuantityMode.ContinuousSamples);




            _myFrequencyGenerator.Start();
        }

        /// <summary>
        /// Sets the timer with which the experiment is started
        /// </summary>
        public void InitializeMeasurementTimer()
        {
            if (_running == false)
            {
                _running = true;
                _myTimer = new Timer(RunAPDTrigger, null, 0, 10);
                
            }
        }

        /// <summary>
        /// Stops the experiment
        /// </summary>
        public void StopAPDTrigger()
        {            
            _running = false;            
        }

        /// <summary>
        /// Runs the experiment
        /// </summary>
        /// <param name="state"></param>
        private void RunAPDTrigger(object state)
        {
            if (!_running)
            {
                ReleaseResources();
                return;
            }


            //the trigger keeps on starting this function every 10ms but only evaluates it if it's not running
            if (Monitor.TryEnter(_lockExperiment))
            {
                try
                {
                    //used for pausing between runs signaled from the controller
                    _pauseCycling.WaitOne();

                    ReadHighFrequencyCounter();

                    if (_monitor) //if we only monitor then ignore all fancy measurement functions
                        return;

                    //check if the value read from the counter is bigger than the set threshold
                    if (_newDataPoint >= _threshold)
                        _detectedBins++;
                    else
                        _detectedBins = 0;

                    //check if enough bins have been over the threshold => atom in the trap
                    if (_detectedBins >= _detectionBins)
                    {
                        _detectedBins = 0;

                        //send a trigger to the digital output card
                        _myTriggerTask.Start();
                        _myTriggerTask.Stop();


                        MeasureSpectrum();
                        EvaluateRecapture();
                    }
                }
                finally
                {
                    Monitor.Exit(_lockExperiment);
                }
            }
        }

        private void ReleaseResources()
        {
            if (_finalized)
                return;

            lock (_lockExperiment)
            {
                if (_finalized)
                    return;

                _finalized = true;

                _myTimer.Change(Timeout.Infinite, Timeout.Infinite);
                _myTimer.Dispose();

                _myTriggerTask.Stop();
                _myTriggerTask.Dispose();

                _myThresholdTask.Stop();
                _myThresholdTask.Dispose();

                _myAcquisitionTask.Stop();
                _myAcquisitionTask.Dispose();
            }

            //send finishing event
            ApdStoppedEvent();
        }

        /// <summary>
        /// Reads values from the high frequency counting
        /// </summary>
        private void ReadHighFrequencyCounter()
        {
            int[] readOutData;
            try
            {
                _myThresholdTask.Start();
                var thresholdReader = new CounterReader(_myThresholdTask.Stream);
                //read 2 elements because the first one is mostly zero   
                readOutData = thresholdReader.ReadMultiSampleInt32(2);
            }
            finally
            {
                _myThresholdTask.Stop();
            }


            if (readOutData.Length >= 1)
            {
                _newDataPoint = readOutData[1];


                NewAPDValueEvent();
            }


            //_NewSample = 1000 + rand.Next(0,1000);
        }

        /// <summary>
        /// pauses the counter
        /// </summary>
        public void Pause()
        {
            _pauseCycling.Reset();
        }

        /// <summary>
        /// resumes the counter
        /// </summary>
        public void Resume()
        {
            _pauseCycling.Set();
        }

        /// <summary>
        /// Acquires the data from the spectrum/recapture measurement
        /// </summary>
        private void MeasureSpectrum()
        {
            var data = new int[_myClockEdges];
            int readOutSamples = 0;
            try
            {
                _myAcquisitionTask.Start();
                var acquisitionReader = new CounterReader(_myAcquisitionTask.Stream);
                acquisitionReader.MemoryOptimizedReadMultiSampleInt32(_myClockEdges, ref data, out readOutSamples);
            }
            catch
            {
                return;
            }
            finally
            {
                _myAcquisitionTask.Stop();
            }


            _myAcquiredData = data;


            var derivedSamples = new int[_myAcquiredData.Length - 1];

            //the counter spits out an integrated photon value so we have to derive this
            for (int counter = 1; counter < _myAcquiredData.Length; counter++)
            {
                derivedSamples[counter - 1] = _myAcquiredData[counter] - _myAcquiredData[counter - 1];
            }

            _mySpectrum = derivedSamples;

            //1us is to fine grain for most of our stuff so we have to bin the acquired data
            var bins = (int) Math.Ceiling(derivedSamples.Length/(double) _apdBinSize);

            _myBinnedSpectrum = bin(derivedSamples, bins);
        }

        /// <summary>
        /// Evaluate the data taken from the last spectrum/recapture measurement
        /// </summary>
        private void EvaluateRecapture()
        {
            var result = new RecaptureResult {Data = RecaptureResult.State.Lost};

            if (_myBinnedSpectrum[1] + _myBinnedSpectrum[2] > 2*_threshold)
                result.Data = RecaptureResult.State.Captured;

            CycleFinishedEvent(result);
        }

        /// <summary>
        /// Bin APD data
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
                    if (binStep == 0 && counter == 0) //remove first 10 data-points they might be rubbish
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


        private void NewAPDValueEvent()
        {
            EventHandler dataUpdate = NewAPDValue;
            if (null != dataUpdate)
                dataUpdate(this, new EventArgs());
        }

        private void ApdStoppedEvent()
        {           
            EventHandler finished = APDStopped;
            if (null != finished)
                finished(this, new EventArgs());
        }

        /// <summary>
        /// Send event that run is over
        /// </summary>
        private void CycleFinishedEvent(RecaptureResult result)
        {           
            EventHandler cycleFinished = CycleFinished;
            if (null != cycleFinished)
                cycleFinished(this, result);
        }

        public event EventHandler APDStopped;
        public event EventHandler NewAPDValue;
        public event EventHandler CycleFinished;
    }
}
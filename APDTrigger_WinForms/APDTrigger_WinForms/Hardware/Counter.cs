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
        private readonly int _cycles;
        private readonly int _detectionBins;
        private readonly double _frequency;
        private readonly object _lockExperiment = new object();
        private readonly int _myClockEdges;
        private readonly Task _myEdgeCountingTask;
        private readonly Task _myFrequencyGenerator;
        private readonly Task _mySampleClock;
        private readonly Task _myThresholdTask;
        private readonly Task _myTriggerTask;        
        private readonly double _threshold;
        private readonly double _triggerBin;
        private volatile int _cycleCounter;
        private readonly bool _monitor;
            
        private int _detectedBins;
        private bool _finalized;
        private DateTime _lastRun;
        private int[] _myAcquiredData;
        private int[] _myBinnedSpectrum;
        private int[] _mySpectrum;        
        private Timer _myTimer;
        private int _newDataPoint;
        private volatile bool _running = false;

        /// <summary>
        ///     Provides the functionality of a standard ASPHERIX experiment
        /// </summary>
        /// <param name="threshold">The value from the APD above you think you have an atom in the trap</param>
        /// <param name="detectionBins">How often this value has to appear in a row before the trigger is send</param>
        /// <param name="apdBinSize">The number of original datapoints per bin (normaly we sample with 1us and want to have 10ms)</param>
        /// <param name="triggerBin">Measurement length for the high-frequency counter in milliseconds</param>
        /// <param name="monitor"> </param>        
        /// <param name="clockEdges"> </param>
        public Counter(double threshold, int detectionBins, int apdBinSize, double triggerBin, bool monitor,
                       int clockEdges, double frequency, int cycles = 0)
        {
            _monitor = monitor;
            _cycles = cycles;
            _myThresholdTask = new Task("ThresholdTask");
            _myTriggerTask = new Task("TriggerTask");
            _myEdgeCountingTask = new Task("AcquisitionTask");
            _mySampleClock = new Task("SampleClockTask");
            _myFrequencyGenerator = new Task("FrequencyGenerator");
            _frequency = frequency;
            _threshold = threshold;
            _detectionBins = detectionBins;
            _apdBinSize = apdBinSize;
            _triggerBin = triggerBin/1000.0; //_triggerBin is needed in seconds triggerBin comes in ms            
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
        public void PrepareTrigger()
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

            _myTriggerTask.COChannels.CreatePulseChannelTicks("/Dev3/ctr1", "Trigger", "", COPulseIdleState.Low, 0, 200,
                                                              200);
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

            _myEdgeCountingTask.CIChannels.CreateCountEdgesChannel("/Dev3/ctr3", "Acquisition", edge, 0, countDirection);

            _myEdgeCountingTask.CIChannels.All.DuplicateCountPrevention = true;

            _myEdgeCountingTask.CIChannels.All.DataTransferMechanism = CIDataTransferMechanism.Dma;


            _myEdgeCountingTask.Timing.ConfigureSampleClock("/Dev3/PFI12", 1000000, clockActiveEdge,
                                                            SampleQuantityMode.FiniteSamples, _myClockEdges);
            InitializeSampleClock();
        }

        private void InitializeSampleClock()
        {
            //create 1MHz sampling clock internally
            _mySampleClock.COChannels.CreatePulseChannelFrequency("/Dev3/ctr0", "ClkOutput",
                                                                  COPulseFrequencyUnits.Hertz, COPulseIdleState.Low,
                                                                  0, 1000000, 0.5);
            _mySampleClock.Triggers.PauseTrigger.ConfigureDigitalLevelTrigger("/Dev3/PFI9",
                                                                              DigitalLevelPauseTriggerCondition.Low);
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
            if(_running == false)
            {
                ReleaseResources();
            }
            else
            {
                _running = false;                
            }

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
                    ReadHighFrequencyCounter();

                    if (_monitor) //if we only monitor then ignore all fancy measurement functions
                        return;

                    //check if the value read from the counter is bigger than the set threshold
                    if (_newDataPoint >= _threshold)
                    {
                        _detectedBins++;
                    }
                    else
                    {
                        _detectedBins = 0;
                        return;
                    }

                    TimeSpan timeSinceLastRun = DateTime.Now - _lastRun;

                    //check if enough bins have been over the threshold => atom in the trap
                    if (_detectedBins >= _detectionBins && timeSinceLastRun.TotalMilliseconds >= 400)
                    {
                        _detectedBins = 0;

                        //send a trigger to the digital output card                                                
                        _myTriggerTask.Start();
                        _myTriggerTask.Stop();

                        //MeasureSpectrum();

                        //RecaptureResult result = EvaluateRecapture();

                        _cycleCounter++;

                        if (_cycleCounter >= _cycles)
                        {
                            _running = false;
                            ReleaseResources();
                        }
                        

                        //CycleFinishedEvent(result);
                        _lastRun = DateTime.Now;
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

                if (_myTimer != null)
                {
                    _myTimer.Change(Timeout.Infinite, Timeout.Infinite);
                    _myTimer.Dispose();
                }

                _myTriggerTask.Stop();
                _myTriggerTask.Dispose();

                _myThresholdTask.Stop();
                _myThresholdTask.Dispose();

                _myEdgeCountingTask.Stop();
                _myEdgeCountingTask.Dispose();

                _mySampleClock.Stop();
                _mySampleClock.Dispose();

                _myFrequencyGenerator.Stop();
                _myFrequencyGenerator.Dispose();
            }

            //send finishing event
            ApdStoppedEvent();
        }

        /// <summary>
        /// Reads values from the high frequency counting / Used to display the apd counts
        /// </summary>
        private void ReadHighFrequencyCounter()
        {
            int[] readOutData = new[]{0};
            try
            {
                _myThresholdTask.Start();
                var thresholdReader = new CounterReader(_myThresholdTask.Stream);
                //read 2 elements because the first one is mostly zero  this seems to be only a problem
                //with the PCI cards the pci-express card doesn't have that problem
                readOutData = thresholdReader.ReadMultiSampleInt32(1);
            }
            finally
            {
                _myThresholdTask.Stop();
            }

            _newDataPoint = readOutData[0];
            NewAPDValueEvent();
            
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
                _mySampleClock.Start();
                _myEdgeCountingTask.Start();
                var acquisitionReader = new CounterReader(_myEdgeCountingTask.Stream);
                acquisitionReader.MemoryOptimizedReadMultiSampleInt32(_myClockEdges, ref data, out readOutSamples);                
            }
            catch (DaqException e)
            {
                _running = false;
                return;
            }
            finally
            {
                _myEdgeCountingTask.Stop();
                _mySampleClock.Stop();
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
        private RecaptureResult EvaluateRecapture()
        {
            var result = new RecaptureResult {Data = RecaptureResult.State.Lost};

            if (_myBinnedSpectrum == null) //nasty workaround
                return result;

            if (_myBinnedSpectrum[1] + _myBinnedSpectrum[2] > 2*_threshold)
                result.Data = RecaptureResult.State.Captured;

            return result;
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

            //remove all event subscription so the object gets disposed
            NewAPDValue = null;
            CycleFinished = null;
            APDStopped = null;
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
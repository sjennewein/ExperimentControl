using System;
using System.Threading;
using System.Windows.Forms;
using NationalInstruments.DAQmx;
using Timer = System.Threading.Timer;

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
        private readonly int _binSize;
        private readonly Task _myAcquisitionTask;
        private readonly Task _myThresholdTask;
        private readonly Task _myTriggerTask;
        private readonly int _thresholdBin;
        private readonly double _thresholdTrigger;
        private int _detectedBins;
        private object _lockExperiment;
        private Timer _myTimer;
        private double[] _samples;
        private bool _running = false;

        /// <summary>
        ///     Provides the functionality of a standard ASPHERIX experiment in terms of the counter card
        /// </summary>
        /// <param name="thresholdTrigger">The value from the APD above you think you have an atom in the trap</param>
        /// <param name="thresholdBin">How often this value has to appear in a row before the trigger is send</param>
        /// <param name="binSize">The number of original datapoints per bin (normaly we sample with 1us and want to have 10ms)</param>
        public Counter(double thresholdTrigger, int thresholdBin, int binSize)
        {
            _myThresholdTask = new Task();
            _myTriggerTask = new Task();
            _myAcquisitionTask = new Task();
            _thresholdTrigger = thresholdTrigger;
            _thresholdBin = thresholdBin;
            _binSize = binSize;
        }

        public double[] Samples
        {
            get { return _samples; }
        }

        /// <summary>
        ///     Initialize the threshold measurement and the trigger which will be send if it's successful.
        /// </summary>
        /// <param name="samples2Acquire">Define the amount of read samples (in continous mode it's the buffersize)</param>
        public void AimTrigger(int samples2Acquire)
        {
            //minimum and maximum counter values are ignored in this measurement mode
            //measurement time is in seconds
            //divisor has to be 4 copied from documentation

            const double minValue = 0;
            const double maxValue = 0;
            const long divisor = 4;
            const double measurementTime = 0.01; //10ms binning
            const CIPeriodStartingEdge edge = CIPeriodStartingEdge.Rising;

            _myThresholdTask.CIChannels.CreatePeriodChannel("", "", minValue, maxValue, edge,
                CIPeriodMeasurementMethod.HighFrequencyTwoCounter,
                measurementTime, divisor,
                CIPeriodUnits.Ticks);

            _myThresholdTask.CIChannels.All.DuplicateCountPrevention = true;
            _myThresholdTask.Timing.ConfigureImplicit(SampleQuantityMode.ContinuousSamples, samples2Acquire);


            /*************************************************************************************/
            /*     The trigger is fired when the read in counts are over a certain threshold     */
            /*************************************************************************************/

            _myTriggerTask.COChannels.CreatePulseChannelTicks("/Dev1/ctr2", "", "", COPulseIdleState.Low, 0, 0, 200);
            _myTriggerTask.Timing.ConfigureImplicit(SampleQuantityMode.FiniteSamples, 1);
        }

        /// <summary>
        ///     Initialize the second counter for the release and recapture measurement
        /// </summary>
        /// <param name="clockEdges">Define the amount of clock-cycles to acquire (will fail if it gets less)</param>
        public void PrepareAcquisition(int clockEdges)
        {
            const CICountEdgesActiveEdge edge = CICountEdgesActiveEdge.Rising;
            const CICountEdgesCountDirection countDirection = CICountEdgesCountDirection.Up;
            const SampleClockActiveEdge clockActiveEdge = SampleClockActiveEdge.Rising;

            _myAcquisitionTask.CIChannels.CreateCountEdgesChannel("", "", edge, 0, countDirection);

            _myAcquisitionTask.CIChannels.All.DuplicateCountPrevention = true;

            _myAcquisitionTask.Timing.ConfigureSampleClock("/Dev1/PFI0", 1000000, clockActiveEdge,
                SampleQuantityMode.FiniteSamples, clockEdges);
        }

        /// <summary>
        /// Start the experiment
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
        /// Stop the experiment
        /// </summary>
        public void StopExperiment()
        {
            if (_running == true)
            {
                _myTimer.Change(Timeout.Infinite, Timeout.Infinite);
                _myTimer = null;
                _running = false;
            }
        }

        private void RunExperiment(object state)
        {
            ReadThresholdCounter();

            //keeps on reading data every 10ms but only evaluates it if no other experiment is running
            if (Monitor.TryEnter(_lockExperiment))
            {
                try
                {
                    //check if the value read from the counter is bigger than the set threshold
                    if (_samples[0] >= _thresholdTrigger)
                        _detectedBins++;
                    else
                        _detectedBins = 0;

                    //check if enough bins have been over the threshold => atom in the trap
                    if (_detectedBins >= _thresholdBin)
                    {
                        _detectedBins = 0;
                        //_myThresholdTask.Stop(); // might be useful (old style)

                        //send a trigger to the digital output card
                        _myTriggerTask.Start();
                        _myTriggerTask.Stop();

                        PerformAcquisition();
                    }
                }
                finally
                {
                    Monitor.Exit(_lockExperiment);
                }
            }
        }

        private void ReadThresholdCounter()
        {
            var thresholdReader = new CounterReader(_myThresholdTask.Stream);
            _samples = thresholdReader.ReadMultiSampleDouble(-1);
        }

        private void PerformAcquisition()
        {
            _myAcquisitionTask.Start();
            var acquisitionReader = new CounterReader(_myAcquisitionTask.Stream);

            int[] samples = acquisitionReader.ReadMultiSampleInt32(-1);
            var derivedSamples = new int[samples.Length - 1];

            //the counter spits out an integrated photon value so we have to derive this
            for (int counter = 1; counter < samples.Length; counter++)
            {
                derivedSamples[counter - 1] = samples[counter] - samples[counter - 1];
            }

            //1us is to fine grain for most of our stuff so we have to bin the acquired data
            var bins = (int) Math.Ceiling(derivedSamples.Length/(double) _binSize);

            int[] binnedData = bin(derivedSamples, bins);
        }

        /// <summary>
        ///     bin APD data into bins
        /// </summary>
        private int[] bin(int[] data, int bins)
        {
            int binCounter = 0;

            var binnedData = new int[bins];

            for (int binStep = 0; binStep < data.Length; binStep += _binSize)
            {
                int tempData = 0;
                for (int counter = 0; counter < _binSize; counter++)
                {
                    if (binStep + counter > data.Length)
                        break;

                    tempData += data[binStep + counter];
                }
                binnedData[binCounter] = tempData;
                binCounter++;
            }
            return binnedData;
        }
    }
}
using System;
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
    /* recapture experiment. Therefor a 1MHz external                       */
    /* frequency generator is connected to one of the                       */
    /* counter. This clock samples the counter with 1us                     */
    /* into the buffer. With this you get total amount of                   */
    /* photons over time => the derivative gives the                        */
    /* amount of photons at that time point.                                */
    /*                                                                      */
    /************************************************************************/

    public class Counter
    {
        private readonly int _thresholdBin;
        private readonly Task _myAcquisitionTask;
        private readonly Task _myThresholdTask;
        private readonly Task _myTriggerTask;
        private readonly double _thresholdTrigger;
        private readonly int _binSize;
        private int _detectedBins;
        private double[] _samples;


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
            //don't know if needed copied from old stuff

            /*************************************************************************************/
            /*     The trigger is fired when the read in counts are over a certain threshold     */
            /*************************************************************************************/

            _myTriggerTask.COChannels.CreatePulseChannelTicks("/Dev1/ctr2", "", "", COPulseIdleState.Low, 0, 0, 200);
            _myTriggerTask.Timing.ConfigureImplicit(SampleQuantityMode.FiniteSamples, 1);
        }

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

        public void StartMeasurement()
        {
        }

        private void RunExperiment()
        {
            ReadThresholdCounter();

            //check if the value read from the counter is bigger than the set threshold
            if (_samples[0] >= _thresholdTrigger)
                _detectedBins++;
            else
                _detectedBins = 0;

            //check if enough bins have been over the threshold => atom in the trap
            if (_detectedBins >= _thresholdBin)
            {
                _detectedBins = 0;
                _myThresholdTask.Stop();
                
                //send a trigger to the digital output card
                _myTriggerTask.Start();
                _myTriggerTask.Stop();

                PerformAcquisition();
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
            int[] derivedSamples = new int[samples.Length - 1];

            //the counter spits out an integrated photon value so we have to derive this
            for (int counter = 1; counter < samples.Length; counter++)
            {
                derivedSamples[counter - 1] = samples[counter] - samples[counter - 1];
            }

            //1us is to fine grain for most of our stuff so we bin that
            int binnedSize = (int) Math.Ceiling( derivedSamples.Length / (double) _binSize );                     

            double[] binnedSamples = new double[binnedSize];

            int binCounter = 0;
            for (int offset = 0; offset < derivedSamples.Length; offset += _binSize)
            {
                double tempData = 0;
                for (int counter = 0; counter < _binSize; counter++)
                {
                    if (offset + counter > derivedSamples.Length)
                        break;

                    tempData += derivedSamples[offset + counter];
                }
                binnedSamples[binCounter] = tempData;
                binCounter++;
            }


        }
    }
}
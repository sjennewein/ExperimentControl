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
    /* amount of photons at that timepoint.                                 */
    /*                                                                      */
    /************************************************************************/

    public class Counter
    {
        private readonly Task _myAcquisitionTask;
        private readonly Task _myThresholdTask;
        private readonly Task _myTriggerTask;
        private AsyncCallback _myCallBack;
        private CounterReader _myCounterReader;
        private Task _runningTask;
        private double[] _samples;

        public Counter()
        {
            _myThresholdTask = new Task();
            _myTriggerTask = new Task();
            _myAcquisitionTask = new Task();
        }

        public void AimTrigger(int samples2acquire)
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
            _myThresholdTask.Timing.ConfigureImplicit(SampleQuantityMode.ContinuousSamples, samples2acquire);
                //don't know if needed copied from old stuff

            /*************************************************************************************/
            /*     The trigger is fired when the read in counts are over a certain threshold     */
            /*************************************************************************************/

            _myTriggerTask.COChannels.CreatePulseChannelTicks("/Dev1/ctr2", "", "", COPulseIdleState.Low, 0, 0, 200);
            _myTriggerTask.Timing.ConfigureImplicit(SampleQuantityMode.FiniteSamples, 1);
        }

        public void PrepareAquisition(int clockEdges)
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
        {}

        private void ReadCounter()
        {
            var test = new CounterReader(_myThresholdTask.Stream);
            test.


        }
    }
}
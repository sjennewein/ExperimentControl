using System;

namespace DigitalOutput.Model
{
    public class Timing
    {
        #region Frequency enum

        public enum Frequency
        {
            GHz,
            MHz,
            kHz,
            Hz
        }

        #endregion

        #region Time enum

        public enum Time
        {
            ns,
            us,
            ms,
            s
        }

        #endregion

        public double _frequency;

        public void SetFrequency(double digits, Frequency unit)
        {           
            _frequency = digits*GetFrequencyBase(unit);
        }

        public double GetFrequency(Frequency unit)
        {
            return _frequency/GetFrequencyBase(unit);
        }

        public void SetTime(double digits, Time unit)
        {
            _frequency = 1/( digits*GetTimeBase(unit));
        }

        public double GetTime(Time unit)
        {
            return GetTimeBase(unit)/_frequency;
        }

        private double GetFrequencyBase(Frequency unit)
        {
            double frequency = 0;
            switch (unit)
            {
                case Frequency.GHz:
                    frequency = Math.Pow(10,9);
                    break;
                case Frequency.MHz:
                    frequency = Math.Pow(10,6);
                    break;
                case Frequency.kHz:
                    frequency = Math.Pow(10,3);
                    break;
                case Frequency.Hz:
                    frequency = 1;
                    break;
            }
            return frequency;
        }

        private double GetTimeBase(Time unit)
        {
            double timebase = 0;
            switch (unit)
            {
                case Time.ns:
                    timebase = Math.Pow(10,9);
                    break;
                case Time.us:
                    timebase = Math.Pow(10,6);
                    break;
                case Time.ms:
                    timebase = Math.Pow(10,3);
                    break;
                case Time.s:
                    timebase = 1;
                    break;
            }
            return timebase;
        }
    }
}
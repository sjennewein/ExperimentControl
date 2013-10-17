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

        private double _frequency;

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

        private int GetFrequencyBase(Frequency unit)
        {
            int frequency = 0;
            switch (unit)
            {
                case Frequency.GHz:
                    frequency = 10 ^ 9;
                    break;
                case Frequency.MHz:
                    frequency = 10 ^ 6;
                    break;
                case Frequency.kHz:
                    frequency = 10 ^ 3;
                    break;
                case Frequency.Hz:
                    frequency = 1;
                    break;
            }
            return frequency;
        }

        private int GetTimeBase(Time unit)
        {
            int timebase = 0;
            switch (unit)
            {
                case Time.ns:
                    timebase = 10 ^ 9;
                    break;
                case Time.us:
                    timebase = 10 ^ 6;
                    break;
                case Time.ms:
                    timebase = 10 ^ 3;
                    break;
                case Time.s:
                    timebase = 1;
                    break;
            }
            return timebase;
        }
    }
}
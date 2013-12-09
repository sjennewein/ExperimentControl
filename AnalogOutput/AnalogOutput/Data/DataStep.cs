namespace AnalogOutput.Data
{
    public enum StepType
    {
        File,
        Manual,
        Iterator
    };

    public class DataStep
    {
        public string Description = "";
        public int Duration;
        public string DurationIterator = null;
        public double[] Ramp;
        public StepType Type = StepType.Manual;
        public double Value;
        public string ValueIterator = null;
    }
}
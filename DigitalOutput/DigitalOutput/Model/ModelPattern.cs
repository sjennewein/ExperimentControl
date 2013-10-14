namespace DigitalOutput.Model
{
    public class ModelPattern
    {
        public ModelStep[] Step;
        public string Name;

        public ModelPattern(int steps)
        {
            Step = new ModelStep[steps];
        }
    }
}
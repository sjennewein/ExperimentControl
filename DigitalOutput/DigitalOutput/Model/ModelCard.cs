namespace DigitalOutput.Model
{
    public class ModelCard
    {
        public ModelPattern[] Patterns;
        public int SampleRate; //in Hz
        public string[] ChannelDescription;

        public ModelCard(int patterns)
        {
            Patterns = new ModelPattern[patterns];
        }
    }
}
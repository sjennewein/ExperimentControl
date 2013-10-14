namespace DigitalOutput.Model
{
    public class ModelStep
    {
        public ModelData[] Channels;
        public ModelData Duration = new ModelData(DataType.Time);

        public ModelStep(int channels)
        {
            Channels = new ModelData[channels];
        }
    }
}
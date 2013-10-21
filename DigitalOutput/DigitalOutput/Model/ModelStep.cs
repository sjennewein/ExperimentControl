namespace DigitalOutput.Model
{
    public class ModelStep
    {
        public ModelData[] Channels;
        public ModelData Duration = new ModelData(){Type = DataType.Time}; //everything is meant to be in microseconds
        public string Description = "";
    }
}
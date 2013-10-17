namespace DigitalOutput.Model
{
    public class ModelStep
    {
        public ModelData[] Channels;
        public ModelData Duration = new ModelData(){Type = DataType.Time};
        public string Description = "";
    }
}
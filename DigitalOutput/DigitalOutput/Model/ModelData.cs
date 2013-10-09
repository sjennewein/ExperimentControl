namespace DigitalOutput.Model
{
    public enum DataType
    {
        Time,
        Data
    };

    public class ModelData
    {
        public double Value;
        public readonly DataType Type;

        public ModelData(DataType type)
        {
            Type = type;
        }
    }
}

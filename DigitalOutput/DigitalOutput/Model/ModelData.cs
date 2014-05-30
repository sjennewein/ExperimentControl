namespace DigitalOutput.Model
{
    public enum DataType
    {
        Time,
        Data
    };

    public class ModelData
    {
        public int Value;
        public DataType Type;
        public string Iterator;
    }
}

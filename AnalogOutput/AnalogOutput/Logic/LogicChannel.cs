using AnalogOutput.Data;

namespace AnalogOutput.Logic
{
    public class LogicChannel
    {
        private readonly DataChannel _data;
        private readonly LogicPattern _parent;
        public LogicStep[] Steps;

        public LogicChannel(DataChannel data, LogicPattern parent)
        {
            _data = data;
            _parent = parent;

            Steps = new LogicStep[_data.Steps.Length];
            for (int iStep = 0; iStep < _data.Steps.Length; iStep++)
            {
                DataStep step = _data.Steps[iStep];
                Steps[iStep] = new LogicStep(step, this);
            }
        }

        public string Name
        {
            get { return _data.Name; }
            set { _data.Name = value; }
        }

        public float Value
        {
            get { return _data.InitialValue; }
            set { _data.InitialValue = value; }
        }
    }
}
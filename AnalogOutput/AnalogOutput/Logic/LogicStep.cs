using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnalogOutput.Data;

namespace AnalogOutput.Logic
{
    public class LogicStep
    {
        private readonly DataStep _data;
        private readonly LogicChannel _parent;

        public LogicStep(DataStep data, LogicChannel parent)
        {
            _data = data;
            _parent = parent;
        }

        public StepType Type { get { return _data.Type; } set { _data.Type = value; } }
        public float Value { get { return _data.Value; } set { _data.Value = value; } }
        public float Duration { get { return _data.Duration; } set { _data.Duration = value; } }
        public string FileName { get { return _data.FileName; } set { _data.FileName = value; } }
        public string Description { get { return _data.Description; } set { _data.Description = value; } }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnalogOutput.Data;
using fastJSON;

namespace AnalogOutput.Logic
{
    public class LogicCard
    {
        private readonly DataCard _data;
        public LogicPattern[] Patterns;

        public LogicCard(DataCard data)
        {
            _data = data;
            Patterns =  new LogicPattern[_data.Patterns.Length];
            for (int iPattern = 0; iPattern < _data.Patterns.Length; iPattern++)
            {
                DataPattern pattern = _data.Patterns[iPattern];
                Patterns[iPattern] =  new LogicPattern(pattern, this);
            }
        }

        public string ToJson()
        {
            return JSON.Instance.ToJSON(_data);
        }

        public string Flow { get { return _data.Flow; } set { _data.Flow = value; } }
    }

 
}

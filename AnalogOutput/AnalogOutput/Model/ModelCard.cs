using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalogOutput.Model
{
    public class ModelCard
    {
        public int SampleRate; //in Hz
        public string Name;

        public ModelPattern[] Patterns;

        public ModelCard(int patterns)
        {
            Patterns = new ModelPattern[patterns];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalogOutput.Model
{
    public class ModelPattern
    {
        public ModelChannel[] Channels;
        public string Name;

        public ModelPattern(int channels)
        {
            Channels = new ModelChannel[channels];
        }
    }
}

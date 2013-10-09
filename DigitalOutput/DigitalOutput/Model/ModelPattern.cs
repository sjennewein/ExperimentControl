using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalOutput.Model
{
    public class ModelPattern
    {
        public ModelChannel[] Channels;

        public ModelPattern(int channels)
        {
            Channels = new ModelChannel[channels];
        }
    }
}

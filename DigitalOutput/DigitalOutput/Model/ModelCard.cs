using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalOutput.Model
{
    public class ModelCard
    {
        public ModelPattern[] Patterns;

        public ModelCard(int patterns)
        {
            Patterns = new ModelPattern[patterns];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APDTrigger.Control
{
    public class Elements
    {
        public int BinSize { get; set; }
        public int Samplerate { get; set; }
        public int Threshold { get; set; }

        public int NumberOfAtoms { get; set; }
        public int RecaptureRate { get; set; }
        public int NoAtoms { get; set; }
        public int Atoms { get; set; }
    }
}

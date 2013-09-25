using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APDTrigger_WinForms.Helper
{
    public class RunEventData : EventArgs
    {
        public readonly int Run;
        public readonly double RecaptureRate;
        public readonly int Atoms;
        public readonly int NoAtoms;

        public RunEventData(int run, double recaptureRate, int atoms, int noAtoms)
        {
            Run = run;
            RecaptureRate = recaptureRate;
            Atoms = atoms;
            NoAtoms = noAtoms;
        }
    }
}

using System.ComponentModel;

namespace APDTrigger_WinForms.Controls
{
    public class Controlling
    {
        //Trigger parameters
        public int Binning { get; set; }
        public int DetectionBins { get; set; }
        public int Threshold { get; set; }
        public int Runs { get; set; }
        public int Cycles { get; set; }

        //Acquisition parameters
        public int Samples2Acquire { get; set; }
        public int APDBinsize { get; set; }

        //Results/Statistics

        public int RecaptureRate { get; set; }
        public int NoAtoms { get; set; }
        public int Atoms { get; set; }

        public void Start()
        {
            
        }

        public void Stop()
        {
            
        }

        
    }
}
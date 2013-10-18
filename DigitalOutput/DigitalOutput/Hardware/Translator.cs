using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DigitalOutput.Model;

namespace DigitalOutput.Hardware
{
    public class Translator
    {
        public static UInt32[] GenerateOutput(ModelCard input)
        {
            var sampleRate = (int) input.SampleRate.GetFrequency(Timing.Frequency.Hz);
            string strippedFlow = input.Flow.Replace("\r\n", String.Empty);
            strippedFlow = strippedFlow.Replace(" ", String.Empty);
            
            //input.Patterns.
            return new uint[]{0,0};
        }

        private static int GetSize(string flow)
        {
            foreach (char c in flow)
            {
                
            }
        }
    }
}

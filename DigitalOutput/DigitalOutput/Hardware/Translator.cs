using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DigitalOutput.Model;

namespace DigitalOutput.Hardware
{
    public class Translator
    {
        public static UInt32[] GenerateOutput(ModelCard input)
        {
            int index = 0;
            List<string> unrolledFlow = new List<string>();
            LoopUnroll(input.Flow, unrolledFlow, ref index);
            uint[] hardwareOutput = GenerateSequence(unrolledFlow, input);
            return hardwareOutput;
        }

        private static void LoopUnroll(string flow, List<string> unrolledFlow,  ref int index )
        {
            string strippedFlow = flow.Replace("\r\n", String.Empty);
            strippedFlow = strippedFlow.Replace(" ", String.Empty);
            string[] splitFlow = strippedFlow.Split(';');


            for (int iLine = index ; iLine < splitFlow.Length; iLine++) //a command ends with a ;
            {
                string command = splitFlow[iLine];

                //compare strings case insensitive
                if (command.Length >= 5)
                {
                    if (String.Equals(command.Substring(0, 5), "loop(", StringComparison.OrdinalIgnoreCase))
                    //check if there is a loop statement
                    {
                        string loopParameter = Regex.Match(command, @"\d+").Value;
                        int loopCnt = int.Parse(loopParameter);

                        int nextLine = 0;
                        for (int iLoop = 0; iLoop < loopCnt; iLoop++)
                        {
                            nextLine = iLine + 1;                       
                            LoopUnroll(flow, unrolledFlow, ref nextLine); //unroll inner loop
                        }
                        iLine = nextLine;  //go to end of inner loop to avoid having stuff twice
                        continue;
                    }
                }


                if (String.Equals(command, "end", StringComparison.OrdinalIgnoreCase))
                {
                    index = iLine;
                    return;
                }

                if (String.Equals(command, String.Empty)) //ignore empty strings
                    continue;


                unrolledFlow.Add(command); //add the pattern x-times according to loop counter

            }            
        }

        private static UInt32[] GenerateSequence(List<string> unrolledFlow, ModelCard card)
        {
            var hardwareOutput = new List<uint>();
            var patternSequence = new Dictionary<string, List<uint>>();
            var sampleRate = (int) card.SampleRate.GetFrequency(Timing.Frequency.Hz);

            foreach (ModelPattern pattern in card.Patterns) //generate each sequence once and put it in a dictionary
            {
                List<uint> patternOutput = GeneratePattern(pattern);
                patternSequence.Add(pattern.Name.ToLower(), patternOutput);
            }

            foreach (string command in unrolledFlow)
                //iterate over the unrolled flow and place the sequences as often as needed
            {
                if (patternSequence.ContainsKey(command.ToLower()))
                    hardwareOutput.AddRange(patternSequence[command.ToLower()]);
            }

            if (hardwareOutput.Count < 2)
                //check if more than the minimum of steps has been generated if not throw exception
                throw new Exception("Run contains no data");

            hardwareOutput.Add(hardwareOutput[0]);

            return hardwareOutput.ToArray();
        }

        private static List<uint> GeneratePattern(ModelPattern pattern)
        {
            var patternOutput = new List<uint>();
            foreach (ModelStep step in pattern.Steps) //loop over the steps in a pattern
            {
                UInt32 stepOutput = 0;
                for (int iChannel = 0; iChannel < step.Channels.Length; iChannel++)
                    //loop over each channel and create the bit pattern for the step
                {
                    if (step.Channels[iChannel].Value != 0)
                    {
                        uint value = (uint) 1 << iChannel; //shift the bit to the right position
                        stepOutput |= value; //add it to the pattern
                    }
                }

                for (int iTimeStep = 0; iTimeStep < step.Duration.Value; iTimeStep++)
                {
                    patternOutput.Add(stepOutput); //add the step as many times needed for the duration
                }
            }

            return patternOutput;
        }
    }
}
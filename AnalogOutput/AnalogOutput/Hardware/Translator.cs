using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AnalogOutput.Data;

namespace AnalogOutput.Hardware
{
    public class Translator
    {
        public static double[,] GenerateDAQmxSequence(DataCard data)
        {
            int index = 0;
            List<string> unrolledFlow = new List<string>();
            //LoopUnroll(input.Flow, unrolledFlow, ref index);
            //double[,] daqmxSequence = GenerateSequence(unrolledFlow, input);
            //return daqmxSequence;
            return new double[3,4];
        }

        private static void LoopUnroll(string flow, List<string> unrolledFlow, ref int index)
        {
            string strippedFlow = flow.Replace("\r\n", String.Empty);
            strippedFlow = strippedFlow.Replace(" ", String.Empty);
            string[] splitFlow = strippedFlow.Split(';');


            for (int iLine = index; iLine < splitFlow.Length; iLine++) //a command ends with a ;
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

        private static List<double[,]> GeneratePattern()
        {
            
        }
    }
}

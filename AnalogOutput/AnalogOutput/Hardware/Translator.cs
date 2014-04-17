using System;

using System.Collections.Generic;
using System.Text.RegularExpressions;
using AnalogOutput.Data;
using AnalogOutput.Interpolation;

namespace AnalogOutput.Hardware
{
    public class Translator
    {
        public static double[,] GenerateDAQmxSequence(DataCard data)
        {
            int index = 0;
            int samples = 0;
            var unrolledFlow = new List<string>();
            LoopUnroll(data.Flow, unrolledFlow, ref index);            
            var patterns = new Dictionary<string, double[,]>();
            foreach (var pattern in data.Patterns)
            {
                patterns.Add(pattern.Name.ToLower(), GeneratePattern(pattern, data.Calibration));
            }

            foreach (var line in unrolledFlow)
            {
                samples += patterns[line.ToLower()].GetLength(1);
            }

            double[,] daqmxSequence = new double[8, samples];
            int columnCounter = 0;
            foreach (var line in unrolledFlow)
            {
                var pattern = patterns[line.ToLower()];
                int rows = pattern.GetLength(0);
                int columns = pattern.GetLength(1);
                for (int iColumn = 0; iColumn < columns; iColumn++)
                {                    
                    for (int iRow = 0; iRow < rows; iRow++)
                    {
                        if(Math.Abs(pattern[iRow,iColumn] - 0) > 0.0001)
                            daqmxSequence[iRow, columnCounter] = pattern[iRow,iColumn];
                    }
                    columnCounter++;
                }
            }
            return daqmxSequence;            
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
                    if (String.Equals(command.Substring(0, 5).ToLower(), "loop(", StringComparison.OrdinalIgnoreCase))
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
                        iLine = nextLine; //go to end of inner loop to avoid having stuff twice
                        continue;
                    }
                }


                if (String.Equals(command.ToLower(), "end", StringComparison.OrdinalIgnoreCase))
                {
                    index = iLine;
                    return;
                }

                if (String.Equals(command, String.Empty)) //ignore empty strings
                    continue;


                unrolledFlow.Add(command.ToLower()); //add the pattern x-times according to loop counter
            }
        }
        /// <summary>
        /// Returns the length in samples
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static int GetLength(DataPattern pattern)
        {
            int pLength = 0;
            foreach (DataChannel channel in pattern.Channels)
            {
                int cLength = 1; //start from one there is an extra step at the beginning 

                foreach (DataStep step in channel.Steps)
                {
                    cLength += step.Duration / 2;
                }

                if (pLength < cLength)
                    pLength = cLength;
            }
            

            return pLength + 1;
        }


        private static double[,] GeneratePattern(DataPattern pattern, List<DataCalibration> calibrations)
        {
            int length = GetLength(pattern); // there is an initial value for each channel
            double[,] sequence = new double[pattern.Channels.Length,length];

            for (int iChannel = 0; iChannel < pattern.Channels.Length; iChannel++)
            {
                var channel = pattern.Channels[iChannel];
                var interpolator = new Polyline(calibrations[iChannel].DataPoints);

                int sampleCounter = 0;
                
                //set the initial value for this channel
                sequence[iChannel, sampleCounter] = interpolator.Interpolate(channel.InitialValue);
                sampleCounter++;
                bool allStepsAreZero = true;

                //check if all steps are empty 
                foreach (var step in channel.Steps)
                {
                    if (step.Duration != 0 || step.Value != 0)
                    {
                        allStepsAreZero = false;
                        break;
                    }                        
                }

                //then the initial value defines the whole pattern
                if (allStepsAreZero)
                {
                    for (int iStep = 0; iStep < length; iStep++ )
                    {
                        sequence[iChannel, iStep] = interpolator.Interpolate(channel.InitialValue);
                    }
                    continue;
                }

                foreach (DataStep step in channel.Steps)
                {
                    if (step.Type == StepType.File)
                    {
                        //treat input loaded from file
                        foreach (double sample in step.Ramp)
                        {
                            var voltage = interpolator.Interpolate(sample);
                            sequence[iChannel, sampleCounter] = voltage;
                            sampleCounter++;
                        }
                    }
                    else
                    {
                        //treat user input
                        double lastValue = sequence[iChannel, sampleCounter - 1];

                        int samples = step.Duration / 2;
                        
                        if(samples == 1)
                        {
                            var voltage = interpolator.Interpolate(step.Value);
                            sequence[iChannel, sampleCounter] = voltage;
                            sampleCounter++;
                            continue;
                        }
                        
                        double stepSize = (interpolator.Interpolate(step.Value) - lastValue) / samples;

                        for (int iSample = 1; iSample <= samples; iSample++)
                        {
                            var voltage = interpolator.Interpolate(lastValue + iSample*stepSize);
                            sequence[iChannel, sampleCounter] = voltage;
                            sampleCounter++;
                        }
                    }
                    
                }
                while(sampleCounter < length - 1)
                {
                    sequence[iChannel, sampleCounter] = sequence[iChannel, sampleCounter - 1];
                    sampleCounter++;
                }

                sequence[iChannel, length-1] = sequence[iChannel, 0];
            }

            return sequence;
        }
    }
}
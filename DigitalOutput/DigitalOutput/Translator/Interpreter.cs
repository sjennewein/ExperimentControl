using DigitalOutput.Model;

namespace DigitalOutput.Translator
{
    public class Interpreter
    {
        public static int[,] PatternToArray(ModelPattern input, int frequency)
        {
            int length = 0;
            int channels = input.Steps[0].Channels.Length;
            
            foreach (ModelStep step in input.Steps)
            {
                length += step.Duration.Value;
            }

            length *= frequency;

            var output = new int[length,channels];

            int offset = 0;
            for (int iStep = 0; iStep < input.Steps.Length; iStep++)
            {
                ModelStep step = input.Steps[iStep];

                for (int iChannels = 0; iChannels < step.Channels.Length; iChannels++)
                {
                    ModelData channel = step.Channels[iChannels];

                    for (int iOutput = 0; iOutput < step.Duration.Value*frequency; iOutput++)
                    {
                        if (channel.Value == 0)
                            continue;

                        int position = offset + iOutput;
                        output[position, iChannels] = channel.Value;
                    }

                }

                offset += step.Duration.Value*frequency;
            }

            return output;
        }
    }
}
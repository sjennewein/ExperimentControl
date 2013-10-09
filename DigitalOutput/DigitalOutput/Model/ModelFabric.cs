using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DigitalOutput.Model
{
    public class ModelFabric
    {
        public static ModelCard GenerateCard()
        {
            const int channels = 32;
            const int steps = 20;
            const int patterns = 10;

            ModelCard newCard = new ModelCard(patterns);

            for (int iPattern = 0; iPattern < patterns; iPattern++ )
            {
                ModelPattern newPattern = new ModelPattern(channels);
                newCard.Patterns[iPattern] = newPattern;

                for(int iChannel = 0; iChannel < channels; iChannel++)
                {
                    ModelChannel newChannel = new ModelChannel(steps);
                    newPattern.Channels[iChannel] = newChannel;

                    for(int iSteps = 0; iSteps < steps; iSteps++)
                    {
                        newChannel.Steps[iSteps] = new ModelStep();
                    }
                }                
            }

            return newCard;
        }
    }
}

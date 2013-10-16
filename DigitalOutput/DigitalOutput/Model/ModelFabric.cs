namespace DigitalOutput.Model
{
    public class ModelFabric
    {
        public static ModelCard GenerateCard()
        {
            const int channels = 32;
            const int steps = 20;
            const int patterns = 5;

            var newCard = new ModelCard(patterns);            

            for (int iPattern = 0; iPattern < patterns; iPattern++)
            {
                var newPattern = new ModelPattern(steps);
                newPattern.Name = "Pattern" + iPattern;
                newCard.Patterns[iPattern] = newPattern;

                for (int iSteps = 0; iSteps < steps; iSteps++)
                {
                    var newStep = new ModelStep(channels);
                    newPattern.Step[iSteps] = newStep;

                    for (int iChannels = 0; iChannels < channels; iChannels++)
                    {
                        newStep.Channels[iChannels] = new ModelData(DataType.Data);
                    }
                }
            }

            return newCard;
        }
    }
}
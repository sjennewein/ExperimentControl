namespace AnalogOutput.Model
{
    public class ModelFabric
    {
        public static ModelCard GenerateCard()
        {
            const int channels = 8;
            const int steps = 10;
            const int patterns = 1;

            var newCard = new ModelCard(patterns);

            for (int iPattern = 0; iPattern < patterns; iPattern++)
            {
                var newPattern = new ModelPattern(channels);
                newPattern.Name = "Pattern" + iPattern;
                newCard.Patterns[iPattern] = newPattern;

                for (int iChannel = 0; iChannel < channels; iChannel++)
                {
                    var newChannel = new ModelChannel(steps);
                    newPattern.Channels[iChannel] = newChannel;

                    for (int iStep = 0; iStep < steps; iStep++)
                    {
                        newChannel.Steps[iStep] = new ModelStep();
                    }
                }
            }
        }
    }
}
namespace AnalogOutput.Data
{
    public class DataFabric
    {
        public static DataCard GenerateModelCard()
        {
            const int channels = 8;
            const int steps = 5;
            const int patterns = 5;
            var newCard = new DataCard
                              {
                                  Patterns = new DataPattern[patterns]
                              };

            for (int iPattern = 0; iPattern < patterns; iPattern++)
            {
                var newPattern = new DataPattern {Channels = new DataChannel[channels]};
                newPattern.Name = "Pattern" + iPattern;

                for (int iChannel = 0; iChannel < channels; iChannel++)
                {
                    var newChannel = new DataChannel {Steps = new DataStep[steps]};
                    for (int iStep = 0; iStep < steps; iStep++)
                    {
                        newChannel.Steps[iStep] = new DataStep();
                    }
                    newPattern.Channels[iChannel] = newChannel;
                }
                newCard.Patterns[iPattern] = newPattern;
            }

            return newCard;
        }
    }
}
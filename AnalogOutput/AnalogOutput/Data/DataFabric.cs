using System.Collections.Generic;
using AnalogOutput.Interpolation;

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
                    Patterns = new DataPattern[patterns],
                };

            for (int iChannel = 0; iChannel < channels; iChannel++)
            {
                var points = new List<Point> {new Point(){X=-10,Y=-10}, new Point(){X=10,Y=10}};
                var calibration = new DataCalibration() {DataPoints = points, Unit = "Voltage [V]:"};
                newCard.Calibration.Add(calibration);
            }

            for (int iPattern = 0; iPattern < patterns; iPattern++)
            {
                var newPattern = new DataPattern {Channels = new DataChannel[channels], Name = "Pattern" + iPattern};

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
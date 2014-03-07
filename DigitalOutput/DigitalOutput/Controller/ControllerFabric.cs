using DigitalOutput.Hardware;
using DigitalOutput.Model;

namespace DigitalOutput.Controller
{
    public class ControllerFabric
    {
        public static ControllerCard GenerateCard(ModelCard data = null)
        {
            if (data == null)
                data = ModelFabric.GenerateCard();

            var newControllerCard = new ControllerCard(data);            

            Description[] newDescriptions = new Description[data.ChannelDescription.Length];
            for (int iDescription = 0; iDescription < data.ChannelDescription.Length; iDescription++ )
            {
                newDescriptions[iDescription] = new Description(data.ChannelDescription, iDescription);
            }

            foreach (ControllerPattern pattern in newControllerCard.Patterns)
            {
                pattern.Descriptions = newDescriptions;
            }
            return newControllerCard;
        }
    }
}
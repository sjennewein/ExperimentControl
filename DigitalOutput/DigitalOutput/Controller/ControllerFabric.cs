using DigitalOutput.Model;

namespace DigitalOutput.Controller
{
    public class ControllerFabric
    {
        public static ControllerCard GenerateCard(ModelCard model = null)
        {
            if (model == null)
                model = ModelFabric.GenerateCard();

            var newControllerCard = new ControllerCard(model);

            Description[] newDescriptions = new Description[model.ChannelDescription.Length];
            for (int iDescription = 0; iDescription < model.ChannelDescription.Length; iDescription++ )
            {
                newDescriptions[iDescription] = new Description(model.ChannelDescription, iDescription);
            }

            foreach (ControllerPattern pattern in newControllerCard.Patterns)
            {
                pattern.Descriptions = newDescriptions;
            }
            return newControllerCard;
        }
    }
}
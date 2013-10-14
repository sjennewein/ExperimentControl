using DigitalOutput.Model;

namespace DigitalOutput.Controller
{
    public class ControllerFabric
    {
        public static ControllerCard GenerateCard(ModelCard model = null)
        {
            if (model == null)
                model = ModelFabric.GenerateCard();

            return new ControllerCard(model);
        }
    }
}
using DigitalOutput.Model;

namespace DigitalOutput.Controller
{
    public class ControllerCard
    {
        private readonly ModelCard _model;
        public ControllerPattern[] Patterns;

        public ControllerCard(ModelCard model)
        {
            _model = model;
            Patterns = new ControllerPattern[_model.Patterns.Length];
            for (int iPattern = 0; iPattern < _model.Patterns.Length; iPattern++)
            {
                ModelPattern modelPattern = _model.Patterns[iPattern];
                Patterns[iPattern] = new ControllerPattern(modelPattern);
            }
        }

        //public string[] Description
        //{
        //    get { return _model.ChannelDescription; }
        //    set { _model.ChannelDescription = value; }
        //}
    }
}
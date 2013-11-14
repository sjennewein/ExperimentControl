using System.ComponentModel;
using DigitalOutput.Model;

namespace DigitalOutput.Controller
{
    public class ControllerPattern : INotifyPropertyChanged
    {
        private readonly ModelPattern _model;
        public Description[] Descriptions;
        public ControllerStep[] Steps;
        private ControllerCard _parent;

        public ControllerPattern(ModelPattern model, ControllerCard parent)
        {
            _parent = parent;
            _model = model;

            Steps = new ControllerStep[model.Steps.Length];
            for (int iSteps = 0; iSteps < _model.Steps.Length; iSteps++)
            {
                ModelStep stepModel = _model.Steps[iSteps];
                Steps[iSteps] = new ControllerStep(stepModel, this);
            }
        }

        public string Name
        {
            get { return _model.Name; }
        }

        public void StoreSyncedValues()
        {
            foreach (ControllerStep step in Steps)
            {
                step.StoreSyncedValues();
            }
        }

        public void RestoreSyncedValues()
        {
            foreach (ControllerStep step in Steps)
            {
                step.RestoreSyncedValues();
            }
        }

        public void SomethingHasChanged()
        {
            _parent.RunChanged();
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    public class Description
    {
        private readonly string[] _descriptions;
        private readonly int _element;

        public Description(string[] descriptions, int element)
        {
            _descriptions = descriptions;
            _element = element;
        }

        public string Text
        {
            get { return _descriptions[_element]; }
            set { _descriptions[_element] = value; }
        }
    }
}
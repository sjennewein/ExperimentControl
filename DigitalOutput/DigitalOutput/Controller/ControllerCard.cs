using System;
using System.Collections.Generic;
using System.ComponentModel;
using DigitalOutput.Model;
using fastJSON;

namespace DigitalOutput.Controller
{
    public class ControllerCard
    {
        private readonly ModelCard _data;
        public List<ControllerPattern> Patterns = new List<ControllerPattern>();

        public ControllerCard(ModelCard data)
        {
            _data = data;

            foreach (ModelPattern modelPattern in _data.Patterns)
            {
                Patterns.Add(new ControllerPattern(modelPattern,this));
            }
        }

        public string Flow
        {
            get { return _data.Flow; }
            set { _data.Flow = value; }
        }


        public string ToJson()
        {
            return JSON.Instance.ToJSON(_data);
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region EVENTS

        private void TriggerEvent(EventHandler newEvent, EventArgs e = null)
        {
            EventHandler triggerEvent = newEvent;
            if (triggerEvent != null)
                triggerEvent(this, e);
        }

        public void RunChanged()
        {
            TriggerEvent(RunDataChanged);
        }

        public event EventHandler RunDataChanged;

        #endregion

        #region Nested type: GuiUpdate

        private delegate void GuiUpdate(string propertyName);

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;

namespace APDTrigger_WinForms.Controls
{
    public class AgingDataPoint
    {
        private readonly TimeSpan _myLifetime;
        private readonly List<AgingDataPoint> _myParent;
        private readonly DateTime _myTimeOfBirth = DateTime.Now;
        private readonly int _myValue;

        public AgingDataPoint(int value, TimeSpan lifetime, List<AgingDataPoint> parent)
        {
            _myValue = value;
            _myLifetime = lifetime;
            _myParent = parent;
        }

        public int Value
        {
            get { return _myValue; }
        }

        public void CheckLifetime()
        {            
            if (DateTime.Now - _myTimeOfBirth >= _myLifetime)
                _myParent.Remove(this);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APDTrigger_WinForms.Helper
{
    public class CycleEventData : EventArgs
    {
        public enum RecaptureType
        {
            Captured,
            Lost
        };

        public RecaptureType Data { get; set; }
    }
}

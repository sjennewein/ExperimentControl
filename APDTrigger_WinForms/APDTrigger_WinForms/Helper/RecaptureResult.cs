using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APDTrigger_WinForms.Helper
{
    public class RecaptureResult : EventArgs
    {
        public enum State
        {
            Captured,
            Lost
        };

        public State Data { get; set; }
    }
}

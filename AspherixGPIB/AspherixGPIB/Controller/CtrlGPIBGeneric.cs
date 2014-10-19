using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspherixGPIB.Data;
using Ivi.Visa.Interop;

namespace AspherixGPIB.Controller
{
    public class CtrlGPIBGeneric
    {
        private DataGPIBGeneric _data;
        private ResourceManager rm;
        private FormattedIO488 ioArbFG = new FormattedIO488();
        private IMessage msg;

        public CtrlGPIBGeneric(DataGPIBGeneric data = null)
        {
            if (data == null)
                _data = new DataGPIBGeneric();
            else
                _data = data;
        }

        public string Address
        {
            get { return _data.Address; }
            set { _data.Address = value; }
        }

        public string Commands
        {
            get { return _data.Commands; }
            set { _data.Commands = value; }
        }
    }
}

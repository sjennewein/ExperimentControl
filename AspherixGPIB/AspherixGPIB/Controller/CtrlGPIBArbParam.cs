using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspherixGPIB.Data;

namespace AspherixGPIB.Controller
{
    public class CtrlGPIBArbParam
    {
        private DataGPIBArbParam _data;

        public CtrlGPIBArbParam(DataGPIBArbParam data)
        {
            _data = data;
        }

        public int Value
        {
            get { return _data.Value; }
            set { _data.Value = value; }
        }

        public string Iterator
        {
            get { return _data.Iterator; }
            set { _data.Iterator = value; }
        }
    }
}

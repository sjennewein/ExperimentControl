using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspherixGPIB.Data
{
    public class DataGPIBGeneric
    {
        public string Address;
        public string Commands;
        public List<KeyValuePair<string,double>> Iterator = new List<KeyValuePair<string,double>>();
    }
}

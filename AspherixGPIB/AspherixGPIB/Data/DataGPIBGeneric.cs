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
        public Dictionary<string,double> Iterator = new Dictionary<string, double>();
    }
}

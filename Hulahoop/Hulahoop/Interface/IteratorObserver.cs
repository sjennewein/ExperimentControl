using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulahoop.Interface
{
    public interface IteratorObserver
    {
        void NewValue(double value);
        void NewName(string name);
    }
}

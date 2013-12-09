using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hulahoop.Interface
{
    public interface IteratorSubject
    {
        void Register(IteratorObserver newObserver);
        void UnRegister(IteratorObserver goneObserver);
        string Name();
    }
}

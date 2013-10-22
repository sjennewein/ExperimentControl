using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace Hulahoop.Controller
{
    public static class HoopManager
    {
        public static readonly List<ControllerIterator> Iterators = new List<ControllerIterator>();
        public static readonly List<ControllerEveryXRun> EveryXRun = new List<ControllerEveryXRun>();
        
        public static void Increment()
        {
            foreach (ControllerIterator iterator in Iterators)
            {
                iterator.Increment();
            }
        }


    }    
}
using System.Collections.Generic;
using System.IO;
using Hulahoop.Model;
using Ionic.Zip;

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

        public static void Save(ZipFile zip)
        {
            foreach (ControllerIterator iterator in Iterators)
            {
                iterator.Save(zip);
            }
        }

        public static void Load(ZipFile zip)
        {
            Iterators.Clear();
            foreach (ZipEntry e in zip)
            {
                
                if (e.FileName.Substring(0, 9) == "Iterator_")
                {
                    string input = "";
                    using (var ms = new MemoryStream())
                    {
                        e.Extract(ms);
                        ms.Flush();
                        ms.Position = 0;
                        input = new StreamReader(ms).ReadToEnd();
                        ms.Close();
                    } 
                    var newModelIterator = (ModelIterator)fastJSON.JSON.Instance.ToObject(input);
                    Iterators.Add(new ControllerIterator(newModelIterator));
                }
                
            }
        }
    }
}
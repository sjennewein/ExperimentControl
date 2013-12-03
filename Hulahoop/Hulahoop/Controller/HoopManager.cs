using System.Collections.Generic;
using System.IO;
using Hulahoop.Model;
using Ionic.Zip;

namespace Hulahoop.Controller
{
    public static class HoopManager
    {
        public static readonly List<ControllerIterator> LinearIterators = new List<ControllerIterator>();        
        public static readonly List<ControllerEveryXRun> FileIterators = new List<ControllerEveryXRun>();

        public static void Increment()
        {
            foreach (ControllerIterator iterator in LinearIterators)
            {
                iterator.Increment();
            }
        }

        public static void Save(ZipFile zip)
        {
            foreach (ControllerIterator iterator in LinearIterators)
            {
                iterator.Save(zip);
            }
        }

        public static void Load(ZipFile zip)
        {
            LinearIterators.Clear();
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
                    LinearIterators.Add(new ControllerIterator(newModelIterator));
                }
                
            }
        }
    }
}
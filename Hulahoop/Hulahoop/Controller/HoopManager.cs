using System.Collections.Generic;
using System.IO;
using Hulahoop.Interface;
using Hulahoop.Model;
using Ionic.Zip;

namespace Hulahoop.Controller
{
    public static class HoopManager
    {
        public static readonly List<ControllerLinearIterator> LinearIterators = new List<ControllerLinearIterator>();        
        public static readonly List<ControllerFileIterator> FileIterators = new List<ControllerFileIterator>();

        public static List<IteratorSubject> Iterators
        {
            get
            {
                var iterators = new List<IteratorSubject>();

                iterators.AddRange(LinearIterators);
                iterators.AddRange(FileIterators);

                return iterators;
            }
        }

        public static void Reset()
        {
            foreach (ControllerLinearIterator iterator in LinearIterators)
            {
                iterator.Reset();
            }

            foreach (ControllerFileIterator iterator in FileIterators)
            {
                iterator.Reset();
            }
        }

        public static void Increment()
        {
            foreach (ControllerLinearIterator iterator in LinearIterators)
            {
                iterator.Increment();
            }

            foreach(ControllerFileIterator iterator in FileIterators)
            {
                iterator.Increment();
            }
        }

        public static void Save(ZipFile zip)
        {
            foreach (ControllerLinearIterator iterator in LinearIterators)
            {
                iterator.Save(zip);
            }
            foreach (ControllerFileIterator iterator in FileIterators)
            {
                iterator.Save(zip);
            }
        }

        public static void Load(ZipFile zip)
        {
            LinearIterators.Clear();
            FileIterators.Clear();

            foreach (ZipEntry e in zip)
            {
                if(e.FileName.Length < 13)
                    continue;
                
                if (e.FileName.Substring(0, 13) == "FileIterator_")
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
                    var newModelIterator = (ModelFileIterator)fastJSON.JSON.Instance.ToObject(input);
                    FileIterators.Add(new ControllerFileIterator(newModelIterator));
                }

                if (e.FileName.Length < 15)
                    continue;

                if (e.FileName.Substring(0, 15) == "LinearIterator_")
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
                    var newModelIterator = (ModelLinearIterator)fastJSON.JSON.Instance.ToObject(input);
                    LinearIterators.Add(new ControllerLinearIterator(newModelIterator));
                }

                
                
            }
        }
    }
}
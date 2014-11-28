using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AspherixGPIB.Data;
using Hulahoop.Controller;
using Hulahoop.Interface;

namespace AspherixGPIB.Controller
{
    public class CtrlGPIBArbParam : IteratorObserver
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

        public void RegisterToSubject(string name)
        {
            foreach (IteratorSubject iterator in HoopManager.Iterators)
            {
                if (iterator.Name() == name)
                    iterator.Register(this);
            }
        }

        private void UnregisterFromSubject()
        {
            foreach (IteratorSubject iterator in HoopManager.Iterators)
                {
                    if (iterator.Name() == _data.Iterator)
                        iterator.UnRegister(this);
                }

            _data.Iterator = null;
        }

        public void NewValue(double value, string sender)
        {
            throw new NotImplementedException();
        }

        public void NewName(string newName, string oldName)
        {
            throw new NotImplementedException();
        }
    }
}

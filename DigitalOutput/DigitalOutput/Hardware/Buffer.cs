using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DigitalOutput.Model;
using NationalInstruments.DAQmx;

namespace DigitalOutput.Hardware
{
    public class Buffer
    {
        private string _data;
        private readonly Task _digitalOutputTask = new Task("Pci6534");
        

        public void UpdateData(string newData)
        {
            _data = newData;
        }

        public void Start(string data)
        {
        }

        public void Stop()
        {
            
        }
    }
}

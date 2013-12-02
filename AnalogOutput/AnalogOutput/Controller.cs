//using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnalogOutput.Data;
using AnalogOutput.Hardware;
using AnalogOutput.Logic;
using ColdNetworkStack.Client;
using fastJSON;
using Ionic.Zip;
using ColdNetworkStack.Client;

namespace AnalogOutput
{
    public class Controller
    {
        public LogicCard Hardware = null;
        public LogicNetwork Network = new LogicNetwork();
        private Buffer _outputDevice = new Buffer();
        
        public void Initialize(string data = null)
        {
            DataCard card = null;

            if(data != null)
                card = (DataCard)JSON.Instance.ToObject(data);

            Hardware = LogicFabric.GenerateCard(card);
        }

        public void Save(string fileName)
        {
            string json = Hardware.ToJson();
            using (var zip = new ZipFile())
            {
                zip.AddEntry("AnalogData.txt", json);
               // HoopManager.Save(zip);
                zip.Save(fileName);
            }
        }

        public void Load(string fileName)
        {
            string cardData = "";

            using (ZipFile zip = ZipFile.Read(fileName))
            {
                using (var ms = new MemoryStream())
                {
                    ZipEntry entry = zip["AnalogData.txt"];
                    entry.Extract(ms);
                    ms.Flush();
                    ms.Position = 0;
                    cardData = new StreamReader(ms).ReadToEnd();
                    ms.Close();
                }
                Initialize(cardData);
                //HoopManager.Load(zip); // has to be restored before the card fabric is called
            }
        }

        public void Start()
        {
            string json = Hardware.ToJson();            
            _outputDevice.Start(json);
        }
    }
}

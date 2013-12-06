using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnalogOutput.Data;

namespace AnalogOutput.Logic
{
    public class LogicFabric
    {
        public static LogicCard GenerateCard(DataCard data = null)
        {
            if (data == null)
                data = DataFabric.GenerateModelCard();

            var newCard = new LogicCard(data);
            return newCard;
        }
     
    }
}

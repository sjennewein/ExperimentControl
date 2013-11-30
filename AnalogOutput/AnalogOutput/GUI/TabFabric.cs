using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnalogOutput.Logic;

namespace AnalogOutput.GUI
{
    public class TabFabric
    {
        private static readonly List<TabPage>  GeneratedPages = new List<TabPage>();

        private static void GenerateTabPage(LogicPattern pattern)
        {
            int columns = pattern.Channels[0].Steps.Length;
            int rows = pattern.Channels.Length;

            const int rowHeight = 115;
            const int columnWidth = 128;

            var newElements = new Control[rows * columns + rows];
            int elementCounter = 0;

            int yOffset = 0;
            
            foreach (var channel in pattern.Channels)
            {
                int xOffset = 0;

                var header = new ChannelHeader(channel) {Location = new Point(xOffset, yOffset)};
                newElements[elementCounter] = header;
                elementCounter++;
                xOffset += columnWidth;

                for (int iStep = 0; iStep < channel.Steps.Length; iStep++)
                {
                    var logicStep = channel.Steps[iStep];
                    var newStep = new Step(logicStep) {Location = new Point(xOffset, yOffset)};
                    newElements[elementCounter] = newStep;
                    xOffset += columnWidth;
                    elementCounter++;
                }
                yOffset += rowHeight;
            }

            var newTab = new TabPage(pattern.Name);
            newTab.AutoScroll = true;
            newTab.Controls.AddRange(newElements);
            GeneratedPages.Add(newTab);
        }

        public static void GenerateTabView(TabControl tab, LogicCard card)
        {            
            foreach (var pattern in card.Patterns)
            {
                GenerateTabPage(pattern);
            }

            tab.Controls.AddRange(GeneratedPages.ToArray());
        }
    }
}

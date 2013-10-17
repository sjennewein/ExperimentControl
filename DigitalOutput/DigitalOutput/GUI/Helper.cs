using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DigitalOutput.Controller;
using System.Diagnostics;

namespace DigitalOutput.GUI
{
    public class Helper
    {
        private static TabPage GenerateTabPage(ControllerPattern pattern)
        {
            var columns = pattern.Steps.Length;
            var rows = pattern.Steps[0].Channels.Length;
            var newTab = new TabPage(pattern.Name);
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            newTab.SuspendLayout();
            
            Control[] newElements = new Control[columns * rows];            

            int counter = 0;
            for (int iStep = 0; iStep < pattern.Steps.Length; iStep++)
            {
                ControllerStep step = pattern.Steps[iStep];

                for (int iChannel = 0; iChannel < step.Channels.Length; iChannel++)
                {
                    var channel = step.Channels[iChannel];

                    var newLabel = new Label
                    {
                        Size = new Size(49, 19),
                        Margin = new Padding(0),
                        Location = new Point(iStep*50, iChannel*20),
                        BackColor = channel.Value == 1 ? channel.OnColor : channel.OffColor
                    };

                    newLabel.MouseClick += channel.ChangeValue;             
                    //newLabel.Click += channel.ChangeValue;             
                    newElements[counter] = newLabel;

                    counter++;
                }

            }
            newTab.Controls.AddRange(newElements);
            
            newTab.ResumeLayout(false);
            //sw.Stop();
            //Console.WriteLine(sw.ElapsedMilliseconds);
            return newTab;
        }

        public static void GenerateTabView(TabControl tab, ControllerCard card)
        {
            
            tab.SuspendLayout();

            TabPage[] newPages = new TabPage[card.Patterns.Length];
            
            for (int iPattern = 0; iPattern < card.Patterns.Length; iPattern++)
            {
                var pattern = card.Patterns[iPattern];
                newPages[iPattern] = GenerateTabPage(pattern);
            }

            tab.Controls.AddRange(newPages);
            tab.ResumeLayout();
        }

        public static void ColorPicker(int line)
        {

        }
    }
}
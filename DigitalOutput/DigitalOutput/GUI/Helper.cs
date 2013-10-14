using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DigitalOutput.Controller;

namespace DigitalOutput.GUI
{
    public class Helper
    {
        private static TabPage GenerateTabPage(ControllerPattern pattern)
        {
            var columns = pattern.Steps.Length;
            var rows = pattern.Steps[0].Channels.Length;
            var newTab = new TabPage(pattern.Name);

            var newPanel = new TableLayoutPanel();


            newPanel.Dock = DockStyle.Fill;

            for (int iStep = 0; iStep < pattern.Steps.Length; iStep++)
            {
                ControllerStep step = pattern.Steps[iStep];
                CheckBox[] newStep = new CheckBox[step.Channels.Length];

                for (int iChannel = 0; iChannel < step.Channels.Length; iChannel++)
                {
                    var channel = step.Channels[iChannel];
                    var newCheckbox = new CheckBox();
                    newCheckbox.Appearance = Appearance.Button;
                    newCheckbox.Size = new Size(50, 20);
                    newCheckbox.Margin = new Padding(0);
                    newCheckbox.Click += channel.ChangeValue;
                    newStep[iChannel] = newCheckbox;
                }
                newPanel.Controls.AddRange(newStep);                    
            }
            
            newTab.Controls.Add(newPanel);

            return newTab;
        }

        public static void GenerateTabView(TabControl tab, ControllerCard card)
        {

            TabPage[] newPages = new TabPage[card.Patterns.Length];

            for (int iPattern = 0; iPattern < card.Patterns.Length; iPattern++)
            {
                var pattern = card.Patterns[iPattern];
                newPages[iPattern] = GenerateTabPage(pattern);
            }

            tab.Controls.AddRange(newPages);
        }

        public static void ColorPicker(int line)
        {

        }
    }
}
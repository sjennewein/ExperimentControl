using System.Drawing;
using System.Windows.Forms;
using DigitalOutput.Controller;

namespace DigitalOutput.GUI
{
    public class Helper
    {
        private static TabPage GenerateTabPage(ControllerPattern pattern)
        {
            int columns = pattern.Steps.Length;
            int rows = pattern.Steps[0].Channels.Length;
            int channelDescription = pattern.Descriptions.Length;
            int stepDescription = pattern.Steps.Length;

            var newElements = new Control[columns*rows + channelDescription + stepDescription];

            int elementCounter = 0;
            int xOffset = 0;
            int yOffset = 0;

            //generate channel description text boxes
            for (int iChannelDescription = 0; iChannelDescription < pattern.Descriptions.Length; iChannelDescription++)
            {
                
                const int yOffsetLocal = 20;
                
                var newDescriptionTextBox = new TextBox
                    {Location = new Point(0, iChannelDescription*20 + yOffsetLocal), Size = new Size(100, 19)};
                newDescriptionTextBox.DataBindings.Add("Text", pattern.Descriptions[iChannelDescription],
                                                       "Text", true, DataSourceUpdateMode.OnPropertyChanged);
                newElements[elementCounter] = newDescriptionTextBox;
                
                elementCounter++;
                xOffset = newDescriptionTextBox.Size.Width;
            }
            

            //generate step description text boxes
            for (int iStepDescription = 0; iStepDescription < pattern.Steps.Length; iStepDescription++)
            {
                const int xOffsetLocal = 100;
                var newDescriptionTextBox = new TextBox
                    {Location = new Point(xOffsetLocal + iStepDescription * 55, 0), Size = new Size(54, 19)};
                newDescriptionTextBox.DataBindings.Add("Text", pattern.Steps[iStepDescription], "Description", true,
                                                       DataSourceUpdateMode.OnPropertyChanged);
                newElements[elementCounter] = newDescriptionTextBox;

                elementCounter++;
                yOffset = newDescriptionTextBox.Size.Height;
            }


            //generate the labels for channels and steps
            for (int iStep = 0; iStep < pattern.Steps.Length; iStep++)
            {
                ControllerStep step = pattern.Steps[iStep];


                for (int iChannel = 0; iChannel < step.Channels.Length; iChannel++)
                {
                    ControllerChannel channel = step.Channels[iChannel];

                    var newLabel = new Label
                        {
                            Size = new Size(54, 19),
                            Margin = new Padding(0),
                            Location = new Point(iStep * 55 + xOffset, iChannel * 20 + yOffset),
                            BackColor = channel.Value == 1 ? channel.OnColor : channel.OffColor
                        };

                    newLabel.MouseClick += channel.ChangeValue;
                    newElements[elementCounter] = newLabel;

                    elementCounter++;
                }
            }

            var newTab = new TabPage(pattern.Name);
            newTab.Controls.AddRange(newElements);

            return newTab;
        }

        public static void GenerateTabView(TabControl tab, ControllerCard card)
        {
            tab.SuspendLayout();

            var newPages = new TabPage[card.Patterns.Length];

            for (int iPattern = 0; iPattern < card.Patterns.Length; iPattern++)
            {
                ControllerPattern pattern = card.Patterns[iPattern];
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
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DigitalOutput.Controller;

namespace DigitalOutput.GUI
{
    public class Helper
    {
        private static readonly List<TabPage> GeneratedPages = new List<TabPage>();

        private static void GenerateTabPage(ControllerPattern pattern)
        {
            const int columnWidth = 64;
            const int rowHeight = 19;

            int columns = pattern.Steps.Length;
            int rows = pattern.Steps[0].Channels.Length;
            int channelDescription = pattern.Descriptions.Length;
            int stepDescription = pattern.Steps.Length;
            int stepDuration = pattern.Steps.Length;

            var newElements = new Control[columns*rows + channelDescription + stepDescription + stepDuration];

            int elementCounter = 0;
            int xOffset = 0;
            int yOffset = 0;

            //generate channel description text boxes
            for (int iChannelDescription = 0; iChannelDescription < pattern.Descriptions.Length; iChannelDescription++)
            {
                const int yOffsetLocal = 2 * rowHeight;

                var descriptionTextBox = new TextBox
                    {Location = new Point(0, iChannelDescription*(rowHeight+1) + yOffsetLocal), Size = new Size(100, rowHeight)};
                descriptionTextBox.DataBindings.Add("Text", pattern.Descriptions[iChannelDescription],
                                                       "Text", true, DataSourceUpdateMode.OnPropertyChanged);
                newElements[elementCounter] = descriptionTextBox;

                elementCounter++;
                xOffset = descriptionTextBox.Size.Width;
            }


            //generate step description text boxes
            for (int iStepDescription = 0; iStepDescription < pattern.Steps.Length; iStepDescription++)
            {
                const int xOffsetLocal = 100;
                var descriptionTextBox = new TextBox
                    {Location = new Point(xOffsetLocal + iStepDescription*(columnWidth+1), 0), Size = new Size(columnWidth, rowHeight)};
                descriptionTextBox.DataBindings.Add("Text", pattern.Steps[iStepDescription], "Description", true,
                                                       DataSourceUpdateMode.OnPropertyChanged);
                newElements[elementCounter] = descriptionTextBox;

                elementCounter++;
                yOffset = descriptionTextBox.Size.Height;
            }


            for (int iStepDuration = 0; iStepDuration < pattern.Steps.Length; iStepDuration++)
            {
                
                var stepDurationTextBox = new TextBox {Location = new Point(xOffset + iStepDuration*(columnWidth +1), yOffset), Size = new Size(columnWidth, rowHeight)};
                stepDurationTextBox.DataBindings.Add("Text", pattern.Steps[iStepDuration], "Duration", true,
                                                     DataSourceUpdateMode.OnPropertyChanged);
                newElements[elementCounter] = stepDurationTextBox;
                elementCounter++;
                

            }
            yOffset += rowHeight;

            //generate the labels for channels and steps
            for (int iStep = 0; iStep < pattern.Steps.Length; iStep++)
            {
                ControllerStep step = pattern.Steps[iStep];


                for (int iChannel = 0; iChannel < step.Channels.Length; iChannel++)
                {
                    ControllerChannel channel = step.Channels[iChannel];

                    var newLabel = new Label
                        {
                            Size = new Size(columnWidth, rowHeight),
                            Margin = new Padding(0),
                            Location = new Point(iStep*(columnWidth+1) + xOffset, iChannel*(rowHeight+1) + yOffset),
                            BackColor = channel.Value == 1 ? channel.OnColor : channel.OffColor
                        };                                            
                    newLabel.MouseClick += channel.ChangeValue;
                    newElements[elementCounter] = newLabel;

                    elementCounter++;
                }
            }

            var newTab = new TabPage(pattern.Name);
            newTab.Controls.AddRange(newElements);

            GeneratedPages.Add(newTab);
        }

        public static void GenerateTabView(TabControl tab, ControllerCard card)
        {
            tab.SuspendLayout();

            //var newPages = new TabPage[card.Patterns.Length];

            for (int iPattern = 0; iPattern < card.Patterns.Length; iPattern++)
            {
                ControllerPattern pattern = card.Patterns[iPattern];
                GenerateTabPage(pattern);
            }
            tab.Controls.AddRange(GeneratedPages.ToArray());
            //tab.Controls.AddRange(newPages);
            tab.ResumeLayout();
        }

        public static void DisposeTabs(TabControl tab)
        {
            int pages = GeneratedPages.Count;
            var toDelete = new List<TabPage>();

            foreach (TabPage page in GeneratedPages)
            {
                if (tab.Controls.Contains(page))
                    toDelete.Add(page);
            }

            foreach (TabPage page in toDelete)
            {
                GeneratedPages.Remove(page);
                tab.Controls.Remove(page);
            }
        }
    }
}
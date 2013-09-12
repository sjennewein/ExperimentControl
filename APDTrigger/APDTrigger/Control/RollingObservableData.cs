using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace APDTrigger.Control
{
    public class RollingObservableData : ObservableDataSource<Point>
    {
        private int itemCounter = 0;
        private int _itemsToShow = 0;
        public RollingObservableData(int itemsToShow) : base()
        {
            _itemsToShow = itemsToShow;          
        }

        public void initializeData()
        {
            for (int i = 0; i < _itemsToShow; i++)
            {
                collection.Add(new Point(i, 0));
            }
            itemCounter = _itemsToShow + 1;
        }

        public int Index { get { return itemCounter; } }

        public new void AppendAsync(Dispatcher dispatcher, Point item)
        {
            
            dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() =>
                           {
                               
                               
                               Collection.Add(item);
                               

                               collection.RemoveAt(0);
                               itemCounter++;
                               
                               RaiseDataChanged();
                           }));

        }

       
    }
}

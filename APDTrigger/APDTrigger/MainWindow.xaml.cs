//using APDTrigger.Control;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using APDTrigger.Control;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.DataSources;


namespace APDTrigger
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private readonly Elements controller = new Elements();
        private readonly Random _myRandomnes = new Random(10);
        private Thread _myThread;
        private ObservableCollection<Point> source1;
        private RollingObservableData source2;
        private int i = 0;
        private DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.033) };

        public MainWindow()
        {
            InitializeComponent();
            //DataContext = controller;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            source1 = new ObservableCollection<Point>();
            source2 = new RollingObservableData(500);
            source2.initializeData();
            source2.SetXYMapping(p => p);
            
            for (i = 0; i < 500; i++)
            {
                source1.Add(new Point(i,0));
                //source2.Collection.Add(new Point(i, 0));
            }
            APDSignal.NewLegendVisible = false;
            APDSignal.Viewport.UseApproximateContentBoundsComparison = false;
            //APDSignal.AddLineGraph(source1.AsDataSource());
            APDSignal.AddLineGraph(source2,2);
            
            //Viewport2D.SetUsesApproximateContentBoundsComparison(line, false);
            //APDSignal.Legend.Visibility = Visibility.Hidden;

            //timer.Tick += new EventHandler(timer_tick);
            //timer.Start();
            _myThread = new Thread(test2);
            _myThread.IsBackground = true;
            _myThread.Start();
        }

        void timer_tick(object sender, EventArgs e)
        {
            test();
            APDSignal.FitToView();

        }


        private void test()
        {                       
                var p1 = new Point(i, _myRandomnes.Next(20));
                source1.Add(p1);           
                
                i++;
            
        }

        private void test2()
        {
            while (true)
            {

                var p1 = new Point(source2.Index, _myRandomnes.Next(20));
                
                source2.AppendAsync(Dispatcher, p1);
                
                Thread.Sleep(33);
            }
        }
    }
}
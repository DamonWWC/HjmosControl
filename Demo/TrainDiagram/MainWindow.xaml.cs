using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrainDiagram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            this.Loaded += MainWindow_Loaded;
            TrainDatas = new List<List<TrainData>>();
            for (int i = 0; i < 180; i++)
            {
                var hour = Math.Truncate(6d);
                var min = (6 - hour) * 60;
                DateTime data;
                DateTime now = DateTime.Now;

                data = new DateTime(now.Year, now.Month, now.Day, (int)hour, (int)min, 0);
                data = data.AddMinutes(5 * i);
              var  TrainData = new List<TrainData>
            {
                new TrainData{ Datetime=data, Value=8 },
                new TrainData{ Datetime=data.AddMinutes(5), Value=7 },
                new TrainData{ Datetime=data.AddMinutes(6), Value=7 },
                new TrainData{ Datetime=data.AddMinutes(11), Value=6 },
                new TrainData{ Datetime=data.AddMinutes(12), Value=6},
                new TrainData{ Datetime=data.AddMinutes(14), Value=5 },
                new TrainData{ Datetime=data.AddMinutes(15), Value=5 },
                new TrainData{ Datetime=data.AddMinutes(18), Value=4 },
                new TrainData{ Datetime=data.AddMinutes(20), Value=4 },
                new TrainData{ Datetime=data.AddMinutes(25), Value=3 },
                new TrainData{ Datetime=data.AddMinutes(27), Value=3 },
                new TrainData{ Datetime=data.AddMinutes(34), Value=2 },
                new TrainData{ Datetime=data.AddMinutes(36), Value=2 },
                new TrainData{ Datetime=data.AddMinutes(40), Value=1 },
                new TrainData{ Datetime=data.AddMinutes(43), Value=1 },
                new TrainData{ Datetime=data.AddMinutes(48), Value=0 },
                new TrainData{ Datetime=data.AddMinutes(50), Value=0 },
            };
                TrainDatas.Add(TrainData);
            }
              
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public List<List<TrainData>> TrainDatas { get; set; }
    }
}

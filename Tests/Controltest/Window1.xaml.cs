using Hjmos.BaseControls.Controls;
using LiveCharts;
using LiveCharts.Wpf;
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
using System.Windows.Shapes;

namespace Controltest
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : ExWindow
    {
        public Window1()
        {
            InitializeComponent();
            //DateTime data;
            //DateTime now = DateTime.Now;
            //data = new DateTime(now.Year, now.Month, now.Day, 24, 0, 0, DateTimeKind.Local);
            //string da = data.ToString("yyyy-MM-dd-HH-mm-ss");
            //string daa = data.ToLongTimeString();
            //string daaa = data.ToShortDateString();
           
            PreviewBrush = new ImageBrush(BitmapFrame.Create(new Uri("D:\\Users\\90462\\Desktop\\20200509094055.png", UriKind.RelativeOrAbsolute), BitmapCreateOptions.IgnoreImageCache, BitmapCacheOption.None));



            List<double> vs = new List<double> { 1, 2, 3, 4 };
            ChartValues<double> vs1 = new ChartValues<double>(vs);
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,7 }
                },
                //new StackedColumnSeries
                //{
                //    Title = "Series 2",
                //    Values = new ChartValues<double> { 6, 7, 3, 4 ,6 }
                //}
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            Labels1 = new[] { "Jan1", "Feb1", "Mar1", "Apr1", "May1", "fff", "sss" };
            YFormatter = value => value.ToString("C");

            //modifying the series collection will animate and update the chart
            SeriesCollection.Add(new LineSeries
            {
                Values = new ChartValues<double> { 5, 3, 2, 4, 6 },
                ScalesXAt = 1,
                LineSmoothness = 0 //straight lines, 1 really smooth lines
            });

            //modifying any series values will also animate and update the chart
            //  SeriesCollection[2].Values.Add(5d);

            DataContext = this;
        }

        private double GetTicks(double num)
        {
            var hour = Math.Truncate(num);
            var min = (num - hour) * 60;
            double sec = 0;
            DateTime data;
            DateTime now = DateTime.Now;
            if (num == 24)
            {
                hour = 23;
                min = 60;
                sec = 60;

            }

            data = new DateTime(now.Year, now.Month, now.Day, (int)hour, (int)min, (int)sec, DateTimeKind.Utc);

            return data.Ticks;
        }




        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public string[] Labels1 { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public Brush PreviewBrush { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
             new ImageBrowser("D:\\Users\\90462\\Desktop\\20200509094055.png").Show();
        }
    }
}

using CefSharp;
using Hjmos.CustomCharts;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Window2.xaml 的交互逻辑
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
            DirectionList = new ObservableCollection<string>() { "总客流", "进站", "出战" };
            DateTime now = DateTime.Now;
            DateTime data = new DateTime(now.Year, now.Month, now.Day, 5, 0, 0);

            var r = new Random();
            double _trend = 0;
            MeasuredValues = new ChartValues<ChartDataModel>();
            ForecastValues = new ChartValues<ChartDataModel>();
            for (int i = 0; i < 18; i++)
            {
                _trend = r.Next(1, 1000);
                MeasuredValues.Add(new ChartDataModel { DateTime = data.AddHours(i), Value = _trend });
            }

            for (int i = 0; i < 18; i++)
            {
                _trend = r.Next(1, 1000);
                ForecastValues.Add(new ChartDataModel { DateTime = data.AddHours(i), Value = _trend });
            }
            DataContext = this;
        }

        public ObservableCollection<string> DirectionList { get; set; }

        public ChartValues<ChartDataModel> MeasuredValues { get; set; }
        public ChartValues<ChartDataModel> ForecastValues { get; set; }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           // cefsharp1.ExecuteScriptAsync($"initchart()","1",2);
        }

    }
}

using CefSharp;
using Hjmos.CustomCharts;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class Window2 : Window, INotifyPropertyChanged
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


        private ChartValues<ChartDataModel> _MeasuredValues;

        public ChartValues<ChartDataModel> MeasuredValues 
        { 
            get { return _MeasuredValues; }
            set { _MeasuredValues = value;OnPropertyChanged(); }
        }
        public ChartValues<ChartDataModel> ForecastValues { get; set; }


        private bool _IsUpdate;
        public bool IsUpdate
        {
            get { return _IsUpdate; }
            set { _IsUpdate = value;OnPropertyChanged(); }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           // cefsharp1.ExecuteScriptAsync($"initchart()","1",2);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime data = new DateTime(now.Year, now.Month, now.Day, 5, 0, 0);
            var r = new Random();
            double _trend = 0;
           // MeasuredValues = new ChartValues<ChartDataModel>();
        
            for (int i = 0; i < 18; i++)
            {
                _trend = r.Next(1, 1000);
                MeasuredValues.Add(new ChartDataModel { DateTime = data.AddHours(i), Value = _trend });
            }
            IsUpdate = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged([CallerMemberName] string propertyName=null)
        {
            if(this.PropertyChanged!=null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}

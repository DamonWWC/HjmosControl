using CefSharp;
using Hjmos.CustomCharts;
using Hjmos.CustomCharts.Controls;
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
            DateTimeFormatter = value => new DateTime((long)value).ToString("HH:mm");


            Datas = new ObservableCollection<FlowData>() { new FlowData { PostValue = 100, RealTimeValue = 20, Title = "1号线" },
            new FlowData{ PostValue=200,RealTimeValue=120,Title="2号线"},
            new FlowData{ PostValue=230,RealTimeValue=233,Title="3号线"},
            new FlowData{ PostValue=350,RealTimeValue=423,Title="4号线"},
            new FlowData{ PostValue=530,RealTimeValue=432,Title="5号线"},
            new FlowData{ PostValue=543,RealTimeValue=654,Title="6号线"},
            new FlowData{ PostValue=675,RealTimeValue=234,Title="7号线"},
            new FlowData{ PostValue=123,RealTimeValue=73,Title="8号线"},
            new FlowData{ PostValue=654,RealTimeValue=123,Title="9号线"},
            new FlowData{ PostValue=956,RealTimeValue=567,Title="10号线"}};
            //Datas = new ObservableCollection<FlowData>() { new FlowData { PostValue = 100, RealTimeValue = 80, Title = "1号线" },
            //new FlowData{ PostValue=450,RealTimeValue=120,Title="2号线"},
            //new FlowData{ PostValue=235,RealTimeValue=233,Title="3号线"},
            //new FlowData{ PostValue=543,RealTimeValue=423,Title="4号线"},
            //new FlowData{ PostValue=765,RealTimeValue=432,Title="5号线"},
            //new FlowData{ PostValue=754,RealTimeValue=654,Title="6号线"},
            //new FlowData{ PostValue=325,RealTimeValue=234,Title="7号线"},
            //new FlowData{ PostValue=773,RealTimeValue=73,Title="8号线"},
            //new FlowData{ PostValue=344,RealTimeValue=123,Title="9号线"},
            //new FlowData{ PostValue=676,RealTimeValue=567,Title="10号线"}};
            DataContext = this;


            List<string> a = new List<string>();
            
        }

        public Func<double,string> DateTimeFormatter { get; set; }

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

        public ObservableCollection<FlowData> _Datas;
        public ObservableCollection<FlowData> Datas 
        {
            get { return _Datas; }
            set{ _Datas = value;OnPropertyChanged(); }
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

        private void OneDayPassengerFlowTrend_ParamChanged(object sender, RoutedPropertyChangedEventArgs<string> e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Datas[1].RealTimeValue = 35;
            //var temp = Datas[0];
            //Datas[0] = Datas[1];
            //Datas[1] = temp;
            Datas = new ObservableCollection<FlowData>() { new FlowData { PostValue = 100, RealTimeValue = 80, Title = "1号线" },
            new FlowData{ PostValue=450,RealTimeValue=120,Title="2号线"},
            new FlowData{ PostValue=235,RealTimeValue=233,Title="3号线"},
            new FlowData{ PostValue=543,RealTimeValue=423,Title="4号线"},
            new FlowData{ PostValue=765,RealTimeValue=432,Title="5号线"},
            new FlowData{ PostValue=754,RealTimeValue=654,Title="6号线"},
            new FlowData{ PostValue=325,RealTimeValue=234,Title="7号线"},
            new FlowData{ PostValue=773,RealTimeValue=73,Title="8号线"},
            new FlowData{ PostValue=344,RealTimeValue=123,Title="9号线"},
            new FlowData{ PostValue=676,RealTimeValue=567,Title="10号线"}};
        }
    }
}

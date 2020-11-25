﻿using CefSharp;
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
using Svg2Xaml;
using Hjmos.CommonControls;
using Hjmos.CustomCharts.Data;
using Hjmos.BaseControls;

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


            TrainCongestionDatas = new ObservableCollection<TrainCongestionData>
            {
                 new TrainCongestionData{ConfestionStatus=ConfestionStatus.Normal,Index=2},
                new TrainCongestionData{  ConfestionStatus=ConfestionStatus.Easy, Index=1},               
                new TrainCongestionData{ConfestionStatus=ConfestionStatus.Congestion,Index=3},
                 new TrainCongestionData{ConfestionStatus=ConfestionStatus.Normal,Index=5},
                new TrainCongestionData{  ConfestionStatus=ConfestionStatus.Easy, Index=6},
                new TrainCongestionData{ConfestionStatus=ConfestionStatus.Congestion,Index=4}
            };

            bigDataItemModels = new ObservableCollection<BigDataItemModel>
            {
                new BigDataItemModel("1",Unit.G,111,@"pack://application:,,,/Controltest;component/image/宽松.svg")
            };

            valuetext = 23;
            DataList = GetDataList();
           
            Content1 = "预计08-31 18:26 黄村站、文冲站、科韵路站迎来下班高峰期客流，建议相关站点启动二级站控模式。        08-31 18:25 天河智慧城站上报乘客丢失一部白色的iPhone11。     08-31 18:25 天河智慧城站上···";
      
            titlelistbox = new List<string>() { "运力费正常下降", "接触轨失电" };

            ChartValue = new ChartValues<double> { 20, 30, 45, 34, 88, 100 };

            WeatherData = new WeatherData() { Condition = "晴", Temperature = 20, WindDirection = "东北风", WindPower = "2级", Precipitation = "0.0mm", Humidity = "46%", Pressure = "1018hpa" };
            //MaxPageRange = new List<int>() { 1, 2, 3, 4 };
            DataContext = this;


            List<string> a = new List<string>();
            
        }

        private ObservableCollection<DemoDataModel> GetDataList()
        {
            return new ObservableCollection<DemoDataModel>
            {
                new DemoDataModel{ Index = 1,  Name = "Name1", IsSelected = false,  Remark = "111" },
                new DemoDataModel{ Index = 2,  Name = "Name2", IsSelected = true,  Remark = "222" },
                new DemoDataModel{ Index = 3,  Name = "Name3", IsSelected = true,  Remark = "333" },
                new DemoDataModel{ Index = 4,  Name = "Name4", IsSelected = false,  Remark = "444" },
                new DemoDataModel{ Index = 5,  Name = "Name5", IsSelected = false,  Remark = "555" },
                new DemoDataModel{ Index = 6,  Name = "Name6", IsSelected = false,  Remark = "666" },
                new DemoDataModel{ Index = 7,  Name = "Name7", IsSelected = true,  Remark = "777" },
                new DemoDataModel{ Index = 8,  Name = "Name8", IsSelected = false,  Remark = "888" },
                new DemoDataModel{ Index = 9,  Name = "Name9", IsSelected = false,  Remark = "999" },
            };
        }

        public WeatherData WeatherData { get; set; }

        public List<int> MaxPageRange { get; set; }


        public List<string> titlelistbox { get; set; }


        private string _ListboxItem;

        public string ListboxItem
        {
            get { return _ListboxItem; }
            set 
            { 
                _ListboxItem = value;
               // OnPropertyChanged(); 
            }
        }


        private ChartValues<double> _ChartValue;

        public ChartValues<double> ChartValue
        {
            get { return _ChartValue; }
            set { _ChartValue = value; OnPropertyChanged(); }
        }



        public int valuetext { get; set; }


        public ObservableCollection<BigDataItemModel> bigDataItemModels { get; set; }


        private ObservableCollection<TrainCongestionData> _TrainCongestionDatas;

        public ObservableCollection<TrainCongestionData> TrainCongestionDatas
        {
            get { return _TrainCongestionDatas; }
            set { _TrainCongestionDatas = value;OnPropertyChanged(); }
        }


        private string _Content;

        public string Content1
        {
            get { return _Content; }
            set { _Content = value; OnPropertyChanged(); }
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

        private ObservableCollection<DemoDataModel> _DataList;
        public ObservableCollection<DemoDataModel> DataList
        {
            get { return _DataList; }
            set { _DataList = value;OnPropertyChanged(); }
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
            //TrainCongestionDatas = new ObservableCollection<TrainCongestionData>
            //{
            //     new TrainCongestionData{ConfestionStatus=ConfestionStatus.Normal,Index=2},
            //    new TrainCongestionData{  ConfestionStatus=ConfestionStatus.Easy, Index=1},
            //    new TrainCongestionData{ConfestionStatus=ConfestionStatus.Congestion,Index=3},
            //     new TrainCongestionData{ConfestionStatus=ConfestionStatus.Normal,Index=5},
            //    new TrainCongestionData{  ConfestionStatus=ConfestionStatus.Easy, Index=6},
            //    new TrainCongestionData{ConfestionStatus=ConfestionStatus.Congestion,Index=4}
            //};

            TrainCongestionDatas[0].ConfestionStatus = ConfestionStatus.Congestion;


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

        private void Pagination_PageUpdated(object sender, Hjmos.BaseControls.Data.FunctionEventArgs<int> e)
        {

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // Content1 = "预计08-31 18:26 黄村站、文冲站、科韵路站迎来下班高峰期客流，建议相关站点启动二级站控模式。        08-31 18:25 天河智慧城站上报乘客丢失一部白色的iPhone11。     08-31 18:25 天河智慧城站上···";
            Content1 = "1234567890qwertyuiop";
        }
    }

    public class DemoDataModel
    {
        public int Index { get; set; }

        public string Name { get; set; }
       


        public bool IsSelected { get; set; }

        public string Remark { get; set; }

        //public DemoType Type { get; set; }

        public string ImgPath { get; set; }

        public ObservableCollection<DemoDataModel> DataList { get; set; }

        // Card
        public string Header { get; set; }

        public string Content { get; set; }

        public string Footer { get; set; }

        // Avatar
        public string DisplayName { get; set; }

        public string Link { get; set; }

        public string AvatarUri { get; set; }

    }
}

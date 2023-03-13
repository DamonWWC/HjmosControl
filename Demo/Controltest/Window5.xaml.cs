using Hjmos.CustomCharts;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Window5.xaml 的交互逻辑
    /// </summary>
    public partial class Window5 : Window, INotifyPropertyChanged
    {
        public Window5()
        {
            InitializeComponent();
            this.Loaded += Window5_Loaded;         
        }

        private void Window5_Loaded(object sender, RoutedEventArgs e)
        {
            var mapper = Mappers.Xy<ChartDataModel>()
             .X(model => model.DateTime.Ticks)
              .Y(model => model.Value);
            Charting.For<ChartDataModel>(mapper);
            DateTimeFormatter = value => (new DateTime((long)value).Day != DateTime.Now.Day ? "24:00" : new DateTime((long)value).ToString("HH:mm"));
            //Formatter = value => value == 0 ? value.ToString() : string.Format("{0}K", value / 1000.0);

            Labels = new string[] { "车站18", "车站17", "车站16", "车站15", "车站14", "车站13", "车站12", "车站11", "车站10", "车站9", "车站8", "车站7", "车站6", "车站5", "车站4", "车站3", "车站2", "车站1"};
            AxisMax = GetTicks(24);
            AxisMin = GetTicks(6);
            AxisStep = TimeSpan.FromHours(1).Ticks;
            AxisUnit = TimeSpan.TicksPerMinute;
            var passengerFlow = new SeriesCollection();
            for (int i = 0; i < 200; i++)
            {
                var hour = Math.Truncate(6d);
                var min = (6 - hour) * 60;
                DateTime data;
                DateTime now = DateTime.Now;

                data = new DateTime(now.Year, now.Month, now.Day, (int)hour, (int)min, 0);
                data = data.AddMinutes(8*i);
                var date = new ChartValues<ChartDataModel>
            {
                new ChartDataModel{ DateTime=data, Value=8 },
                new ChartDataModel{ DateTime=data.AddMinutes(5), Value=7 },
                new ChartDataModel{ DateTime=data.AddMinutes(6), Value=7 },
                new ChartDataModel{ DateTime=data.AddMinutes(11), Value=6 },
                new ChartDataModel{ DateTime=data.AddMinutes(12), Value=6},
                new ChartDataModel{ DateTime=data.AddMinutes(14), Value=5 },
                new ChartDataModel{ DateTime=data.AddMinutes(15), Value=5 },
                new ChartDataModel{ DateTime=data.AddMinutes(18), Value=4 },
                new ChartDataModel{ DateTime=data.AddMinutes(20), Value=4 },
                new ChartDataModel{ DateTime=data.AddMinutes(25), Value=3 },
                new ChartDataModel{ DateTime=data.AddMinutes(27), Value=3 },
                new ChartDataModel{ DateTime=data.AddMinutes(34), Value=2 },
                new ChartDataModel{ DateTime=data.AddMinutes(36), Value=2 },
                new ChartDataModel{ DateTime=data.AddMinutes(40), Value=1 },
                new ChartDataModel{ DateTime=data.AddMinutes(43), Value=1 },
                new ChartDataModel{ DateTime=data.AddMinutes(48), Value=0 },
                new ChartDataModel{ DateTime=data.AddMinutes(50), Value=0 },
            };
                passengerFlow.Add(CreateSeries(date, "ss"));
            }

            //for (int i = 0; i < 50; i++)
            //{
            //    var hour = Math.Truncate(6d);
            //    var min = (6 - hour) * 60;
            //    DateTime data;
            //    DateTime now = DateTime.Now;

            //    data = new DateTime(now.Year, now.Month, now.Day, (int)hour, (int)min, 0);
            //    data = data.AddMinutes(8 * i);
            //    var date = new ChartValues<ChartDataModel>
            //{
            //    new ChartDataModel{ DateTime=data, Value=0 },
            //    new ChartDataModel{ DateTime=data.AddMinutes(5), Value=1 },
            //    new ChartDataModel{ DateTime=data.AddMinutes(6), Value=1 },
            //    new ChartDataModel{ DateTime=data.AddMinutes(11), Value=2 },
            //    new ChartDataModel{ DateTime=data.AddMinutes(12), Value=2},
            //    new ChartDataModel{ DateTime=data.AddMinutes(14), Value=3 },
            //    new ChartDataModel{ DateTime=data.AddMinutes(15), Value=3 },
            //    new ChartDataModel{ DateTime=data.AddMinutes(18), Value=4 },
            //    new ChartDataModel{ DateTime=data.AddMinutes(20), Value=4 },
            //    new ChartDataModel{ DateTime=data.AddMinutes(25), Value=5 },
            //    new ChartDataModel{ DateTime=data.AddMinutes(27), Value=5 },
            //    new ChartDataModel{ DateTime=data.AddMinutes(34), Value=6 },
            //    new ChartDataModel{ DateTime=data.AddMinutes(36), Value=6 },
            //    new ChartDataModel{ DateTime=data.AddMinutes(40), Value=7 },
            //    new ChartDataModel{ DateTime=data.AddMinutes(43), Value=7 },
            //    new ChartDataModel{ DateTime=data.AddMinutes(48), Value=8 },
            //    new ChartDataModel{ DateTime=data.AddMinutes(50), Value=8 },
            //};
            //    passengerFlow.Add(CreateSeries(date, "ss"));
            //}

            PassengerFlowSeries = passengerFlow;


            DataContext = this;
        }

        private LineSeries CreateSeries(ChartValues<ChartDataModel> chartDatas, string title)
        {
            return new LineSeries
            {
                Values = chartDatas,
                Title = title,
                StrokeThickness = 2,
                PointGeometrySize = 0,
                Fill = Brushes.Transparent,
                PointGeometry = null,
                LineSmoothness = 0,
                Stroke = Brushes.Red
            };
        }


        private SeriesCollection _PassengerFlowSeries;
        /// <summary>
        /// 客流曲线
        /// </summary>
        public SeriesCollection PassengerFlowSeries
        {
            get { return _PassengerFlowSeries; }
            set { _PassengerFlowSeries = value;
                OnPropertyChanged("PassengerFlowSeries");
            }
        }
        private Func<double, string> _DateTimeFormatter;
        /// <summary>
        /// 横坐标样式
        /// </summary>
        public Func<double, string> DateTimeFormatter
        {
            get { return _DateTimeFormatter; }
            set { _DateTimeFormatter = value;
                OnPropertyChanged("DateTimeFormatter");
            }
        }

        private string[] _Labels;
        public string[] Labels
        {
            get { return _Labels; }
            set { _Labels = value;
                OnPropertyChanged("Labels");
            }
        }

        private Func<double, string> _Formatter;
        /// <summary>
        /// 纵坐标样式
        /// </summary>
        public Func<double, string> Formatter
        {
            get { return _Formatter; }
            set { _Formatter = value;
                OnPropertyChanged("Formatter");
            }
        }


        private double _AxisMax;
        /// <summary>
        /// 横坐标最大范围
        /// </summary>
        public double AxisMax
        {
            get { return _AxisMax; }
            set {
                _AxisMax = value;
                OnPropertyChanged("AxisMax");
            }
        }

        private double _AxisMin;
        /// <summary>
        /// 横坐标最大范围
        /// </summary>
        public double AxisMin
        {
            get { return _AxisMin; }
            set {
                _AxisMin = value;
                OnPropertyChanged("AxisMin");
            }
        }


        private double _AxisUnit;
        public double AxisUnit
        {
            get { return _AxisUnit; }
            set { _AxisUnit = value;
                OnPropertyChanged("AxisUnit");
            }
        }

        private double _AxisStep;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public double AxisStep
        {
            get { return _AxisStep; }
            set { _AxisStep = value;
                OnPropertyChanged("AxisStep");
            }
        }

        /// <summary>
        /// 数字转换为对应的时间
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private double GetTicks(double num)
        {
            var hour = Math.Truncate(num);
            var min = (num - hour) * 60;
            DateTime data;
            DateTime now = DateTime.Now;
            if (num == 24)
            {
                now = now.AddDays(1);
                hour = 0;
            }
            data = new DateTime(now.Year, now.Month, now.Day, (int)hour, (int)min, 0);
            var aa = data - DateTime.Now;
            return data.Ticks;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AxisMax = GetTicks(24);
            AxisMin = GetTicks(6);
        }
    }

   
}

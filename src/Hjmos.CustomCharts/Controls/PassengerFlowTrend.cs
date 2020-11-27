using LiveCharts;
using LiveCharts.Configurations;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;

namespace Hjmos.CustomCharts.Controls
{
    public class PassengerFlowTrend : Control
    {
        public PassengerFlowTrend()
        {
            var mapper = Mappers.Xy<ChartDataModel>()
                .X(model => model.DateTime.Ticks)
                .Y(model => model.Value);
            Charting.For<ChartDataModel>(mapper);
            LineVisibility = new ObservableCollection<bool> { true, true, true, true, true };
            this.Loaded += PassengerFlowTrend_Loaded; ;
        }

        private void PassengerFlowTrend_Loaded(object sender, RoutedEventArgs e)
        {
            if (DateTimeFormatter == null)
            {
                DateTimeFormatter = value => new DateTime((long)value).ToString("H:mm");
            }
            if (Formatter == null)
            {
                Formatter = value => value == 0 ? value.ToString() : string.Format("{0:N1}K", value / 1000);
            }
            
        }



        /// <summary>
        /// 坐标字体颜色
        /// </summary>
        public Brush AxisForeground
        {
            get { return (Brush)GetValue(AxisForegroundProperty); }
            set { SetValue(AxisForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AxisForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AxisForegroundProperty =
            DependencyProperty.Register("AxisForeground", typeof(Brush), typeof(PassengerFlowTrend), new PropertyMetadata(Brushes.Black));


        /// <summary>
        /// 线是否显示
        /// </summary>
        internal ObservableCollection<bool> LineVisibility
        {
            get { return (ObservableCollection<bool>) GetValue(LineVisibilityProperty); }
            set { SetValue(LineVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ForecastVisibility.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty LineVisibilityProperty =
            DependencyProperty.Register("LineVisibility", typeof(ObservableCollection<bool>), typeof(PassengerFlowTrend), new PropertyMetadata(default(ObservableCollection<bool>)));



        /// <summary>
        /// 进站值
        /// </summary>
        public ChartValues<ChartDataModel> InStationValues
        {
            get { return (ChartValues<ChartDataModel>)GetValue(InStationValuesProperty); }
            set { SetValue(InStationValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InStationValuesProperty =
            DependencyProperty.Register("InStationValues", typeof(ChartValues<ChartDataModel>), typeof(PassengerFlowTrend), new PropertyMetadata(new ChartValues<ChartDataModel>()));

        /// <summary>
        /// 出站值
        /// </summary>
        public ChartValues<ChartDataModel> OutStationValues
        {
            get { return (ChartValues<ChartDataModel>)GetValue(OutStationValuesProperty); }
            set { SetValue(OutStationValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutStationValuesProperty =
            DependencyProperty.Register("OutStationValues", typeof(ChartValues<ChartDataModel>), typeof(PassengerFlowTrend), new PropertyMetadata(new ChartValues<ChartDataModel>()));



        /// <summary>
        /// 乘降值
        /// </summary>
        public ChartValues<ChartDataModel> StandValues
        {
            get { return (ChartValues<ChartDataModel>)GetValue(StandValuesProperty); }
            set { SetValue(StandValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StandValuesProperty =
            DependencyProperty.Register("StandValues", typeof(ChartValues<ChartDataModel>), typeof(PassengerFlowTrend), new PropertyMetadata(new ChartValues<ChartDataModel>()));




        /// <summary>
        /// 预测值
        /// </summary>

        public ChartValues<ChartDataModel> ForecastValues
        {
            get { return (ChartValues<ChartDataModel>)GetValue(ForecastValuesProperty); }
            set { SetValue(ForecastValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ForecastValues.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ForecastValuesProperty =
            DependencyProperty.Register("ForecastValues", typeof(ChartValues<ChartDataModel>), typeof(PassengerFlowTrend), new PropertyMetadata(new ChartValues<ChartDataModel>()));

        /// <summary>
        /// 实测值
        /// </summary>
        public ChartValues<ChartDataModel> MeasuredValues
        {
            get { return (ChartValues<ChartDataModel>)GetValue(MeasuredValuesProperty); }
            set { SetValue(MeasuredValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MeasuredValuesProperty =
            DependencyProperty.Register("MeasuredValues", typeof(ChartValues<ChartDataModel>), typeof(PassengerFlowTrend), new PropertyMetadata(new ChartValues<ChartDataModel>()));

        /// <summary>
        /// 横坐标文本样式
        /// </summary>
        public Func<double, string> DateTimeFormatter
        {
            get { return (Func<double, string>)GetValue(DateTimeFormatterProperty); }
            set { SetValue(DateTimeFormatterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataTimeFormatter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateTimeFormatterProperty =
            DependencyProperty.Register("DateTimeFormatter", typeof(Func<double, string>), typeof(PassengerFlowTrend), new PropertyMetadata(default(Func<double, string>)));

        /// <summary>
        /// 纵坐标文本样式
        /// </summary>
        public Func<double, string> Formatter
        {
            get { return (Func<double, string>)GetValue(FormatterProperty); }
            set { SetValue(FormatterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Formatter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormatterProperty =
            DependencyProperty.Register("Formatter", typeof(Func<double, string>), typeof(PassengerFlowTrend), new PropertyMetadata(default(Func<double, string>)));

        public bool IsUpdate
        {
            get { return (bool)GetValue(IsUpdateProperty); }
            set { SetValue(IsUpdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsUpdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsUpdateProperty =
            DependencyProperty.Register("IsUpdate", typeof(bool), typeof(PassengerFlowTrend), new PropertyMetadata(false,
                (o, args) =>
                {
                    var ct1 = (PassengerFlowTrend)o;
                    var v = (bool)args.NewValue;
                    if (v)
                    {
                        ct1.AxisMin = GetTicks(ct1.StartTime);
                        ct1.AxisMax = GetTicks(ct1.EndTime);
                    }
                }));


        /// <summary>
        /// 显示的起始时间
        /// </summary>
        public double StartTime
        {
            get { return (double)GetValue(StartTimeProperty); }
            set { SetValue(StartTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartTimeProperty =
            DependencyProperty.Register("StartTime", typeof(double), typeof(PassengerFlowTrend), new PropertyMetadata(5d,
                (o, args) =>
                {
                    var ct1 = (PassengerFlowTrend)o;
                    var v = (double)args.NewValue;
                    ct1.AxisMin = GetTicks(v);
                }));

        /// <summary>
        /// 终止时间
        /// </summary>
        public double EndTime
        {
            get { return (double)GetValue(EndTimeProperty); }
            set { SetValue(EndTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndTime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndTimeProperty =
            DependencyProperty.Register("EndTime", typeof(double), typeof(PassengerFlowTrend), new PropertyMetadata(23d,
                (o, args) =>
                {
                    var ct1 = (PassengerFlowTrend)o;
                    var v = (double)args.NewValue;
                    ct1.AxisMax = GetTicks(v);
                }));

        /// <summary>
        /// 数字转时间
        /// </summary>
        /// <param name="num">数字</param>
        /// <returns></returns>
        private static double GetTicks(double num)
        {
            var hour = Math.Truncate(num);
            var min = (num - hour) * 60;

            DateTime now = DateTime.Now;
            DateTime data = new DateTime(now.Year, now.Month, now.Day, (int)hour, (int)min, 0);

            return data.Ticks;
        }

        internal double AxisMax
        {
            get { return (double)GetValue(AxisMaxProperty); }
            set { SetValue(AxisMaxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AxisMax.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty AxisMaxProperty =
            DependencyProperty.Register("AxisMax", typeof(double), typeof(PassengerFlowTrend), new PropertyMetadata(GetTicks(23)));

        internal double AxisMin
        {
            get { return (double)GetValue(AxisMinProperty); }
            set { SetValue(AxisMinProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AxisMin.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty AxisMinProperty =
            DependencyProperty.Register("AxisMin", typeof(double), typeof(PassengerFlowTrend), new PropertyMetadata(GetTicks(5)));

        public double AxisStep
        {
            get { return (double)GetValue(AxisStepProperty); }
            set { SetValue(AxisStepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AxisStep.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AxisStepProperty =
            DependencyProperty.Register("AxisStep", typeof(double), typeof(PassengerFlowTrend), new PropertyMetadata((double)TimeSpan.FromHours(2).Ticks));

        public double AxisUnit
        {
            get { return (double)GetValue(AxisUnitProperty); }
            set { SetValue(AxisUnitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AxisUnit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AxisUnitProperty =
            DependencyProperty.Register("AxisUnit", typeof(double), typeof(PassengerFlowTrend), new PropertyMetadata((double)TimeSpan.TicksPerMinute));

        /// <summary>
        /// 条件改变事件
        /// </summary>
        public static readonly RoutedEvent ParamChangedEvent =
            EventManager.RegisterRoutedEvent("ParamChangedEvent", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<string>), typeof(PassengerFlowTrend));

        /// <summary>
        /// 条件改变事件
        /// </summary>
        public event RoutedPropertyChangedEventHandler<string> ParamChanged
        {
            add => AddHandler(ParamChangedEvent, value);
            remove => RemoveHandler(ParamChangedEvent, value);
        }

    }
}

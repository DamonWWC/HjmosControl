using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Hjmos.CustomCharts.Controls
{

    public class OneDayPassengerFlowTrend : Control
    {
        //static OneDayPassengerFlowTrend()
        //{
        //    //DefaultStyleKeyProperty.OverrideMetadata(typeof(OneDayPassengerFlowTrend), new FrameworkPropertyMetadata(typeof(OneDayPassengerFlowTrend)));
        //}



        
        public OneDayPassengerFlowTrend()
        {
            var mapper = Mappers.Xy<ChartDataModel>()
                 .X(model => model.DateTime.Ticks)
                 .Y(model => model.Value);
            Charting.For<ChartDataModel>(mapper);
            DateTimeFormatter = value => new DateTime((long)value).ToString("H:mm");

        }
        /// <summary>
        /// 客流方向 总客流，进站，出站
        /// </summary>
        public ObservableCollection<string> DirectionList
        {
            get { return (ObservableCollection<string>)GetValue(DirectionListProperty); }
            set { SetValue(DirectionListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DirectionList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DirectionListProperty =
            DependencyProperty.Register("DirectionList", typeof(ObservableCollection<string>), typeof(OneDayPassengerFlowTrend), new PropertyMetadata(default(ObservableCollection<string>)));


        public string SelectedDirection
        {
            get { return (string)GetValue(SelectedDirectionProperty); }
            set { SetValue(SelectedDirectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedDirection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedDirectionProperty =
            DependencyProperty.Register("SelectedDirection", typeof(string), typeof(OneDayPassengerFlowTrend), new PropertyMetadata(default(string)));


        /// <summary>
        /// 初始前景色
        /// </summary>
        public Brush FlowForeground
        {
            get { return (Brush)GetValue(FlowForegroundProperty); }
            set { SetValue(FlowForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FlowForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FlowForegroundProperty =
            DependencyProperty.Register("FlowForeground", typeof(Brush), typeof(OneDayPassengerFlowTrend), new PropertyMetadata(Brushes.Black));

        /// <summary>
        /// 初始背景色
        /// </summary>
        public Brush FlowBackgroud
        {
            get { return (Brush)GetValue(FlowBackgroudProperty); }
            set { SetValue(FlowBackgroudProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FlowBackgroud.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FlowBackgroudProperty =
            DependencyProperty.Register("FlowBackgroud", typeof(Brush), typeof(OneDayPassengerFlowTrend), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6308506C"))));

        /// <summary>
        /// 选中选项时的颜色
        /// </summary>
        public Brush CheckedBackground
        {
            get { return (Brush)GetValue(CheckedBackgroundProperty); }
            set { SetValue(CheckedBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CheckedBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckedBackgroundProperty =
            DependencyProperty.Register("CheckedBackground", typeof(Brush), typeof(OneDayPassengerFlowTrend), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF08506C"))));

        /// <summary>
        /// 鼠标在选项上时的颜色
        /// </summary>
        public Brush MouseHoverBackground
        {
            get { return (Brush)GetValue(MouseHoverBackgroundProperty); }
            set { SetValue(MouseHoverBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MouseHoverBackgroud.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseHoverBackgroundProperty =
            DependencyProperty.Register("MouseHoverBackground", typeof(Brush), typeof(OneDayPassengerFlowTrend), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF08506C"))));


        /// <summary>
        /// 预测线是否显示
        /// </summary>
        internal bool ForecastVisibility
        {
            get { return (bool)GetValue(ForecastVisibilityProperty); }
            set { SetValue(ForecastVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ForecastVisibility.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty ForecastVisibilityProperty =
            DependencyProperty.Register("ForecastVisibility", typeof(bool), typeof(OneDayPassengerFlowTrend), new PropertyMetadata(true));



        /// <summary>
        /// 实测线是否显示
        /// </summary>
        internal bool MeasuredVisibility
        {
            get { return (bool)GetValue(MeasuredVisibilityProperty); }
            set { SetValue(MeasuredVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MeasuredVisibility.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty MeasuredVisibilityProperty =
            DependencyProperty.Register("MeasuredVisibility", typeof(bool), typeof(OneDayPassengerFlowTrend), new PropertyMetadata(false));

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
            DependencyProperty.Register("MeasuredValues", typeof(ChartValues<ChartDataModel>), typeof(OneDayPassengerFlowTrend), new PropertyMetadata(default(ChartValues<ChartDataModel>)));


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
            DependencyProperty.Register("ForecastValues", typeof(ChartValues<ChartDataModel>), typeof(OneDayPassengerFlowTrend), new PropertyMetadata(default(ChartValues<ChartDataModel>)));



        public Func<double,string> DateTimeFormatter
        {
            get { return (Func<double,string>)GetValue(DateTimeFormatterProperty); }
            set { SetValue(DateTimeFormatterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataTimeFormatter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateTimeFormatterProperty =
            DependencyProperty.Register("DateTimeFormatter", typeof(Func<double,string>), typeof(OneDayPassengerFlowTrend), new PropertyMetadata(default(Func<double,string>)));


        public Func<double,string> Formatter
        {
            get { return (Func<double,string>)GetValue(FormatterProperty); }
            set { SetValue(FormatterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Formatter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormatterProperty =
            DependencyProperty.Register("Formatter", typeof(Func<double,string>), typeof(OneDayPassengerFlowTrend), new PropertyMetadata(default(Func<double,string>)));


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
            DependencyProperty.Register("StartTime", typeof(double), typeof(OneDayPassengerFlowTrend), new PropertyMetadata(5d,
                (o,args)=>
                {
                    var ct1 = (OneDayPassengerFlowTrend)o;
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
            DependencyProperty.Register("EndTime", typeof(double), typeof(OneDayPassengerFlowTrend), new PropertyMetadata(23d,
                (o,args)=>
                {
                    var ct1 = (OneDayPassengerFlowTrend)o;
                    var v = (double)args.NewValue;
                    ct1.AxisMax = GetTicks(v);
                }));


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
            DependencyProperty.Register("AxisMax", typeof(double), typeof(OneDayPassengerFlowTrend), new PropertyMetadata(GetTicks(23)));



        internal double AxisMin
        {
            get { return (double)GetValue(AxisMinProperty); }
            set { SetValue(AxisMinProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AxisMin.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty AxisMinProperty =
            DependencyProperty.Register("AxisMin", typeof(double), typeof(OneDayPassengerFlowTrend), new PropertyMetadata(GetTicks(5)));





        public double AxisStep
        {
            get { return (double)GetValue(AxisStepProperty); }
            set { SetValue(AxisStepProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AxisStep.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AxisStepProperty =
            DependencyProperty.Register("AxisStep", typeof(double), typeof(OneDayPassengerFlowTrend), new PropertyMetadata((double)TimeSpan.FromHours(2).Ticks));




        public double AxisUnit
        {
            get { return (double)GetValue(AxisUnitProperty); }
            set { SetValue(AxisUnitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AxisUnit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AxisUnitProperty =
            DependencyProperty.Register("AxisUnit", typeof(double), typeof(OneDayPassengerFlowTrend), new PropertyMetadata((double)TimeSpan.TicksPerMinute));






    }
  
}

using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hjmos.CustomCharts.Controls
{
    public class MoreDayPassengerFlowTrend : Control
    {
        public MoreDayPassengerFlowTrend()
        {
            this.Loaded += MoreDayPassengerFlowTrend_Loaded;
        }

        private void MoreDayPassengerFlowTrend_Loaded(object sender, RoutedEventArgs e)
        {
            if(Formatter==null)
            {
                Formatter = value => value.ToString("N0");
            }
            SelectedDateRange = DateRangeList?.FirstOrDefault();
        }

        /// <summary>
        /// 客流周期
        /// </summary>
        public ObservableCollection<string> DateRangeList
        {
            get { return (ObservableCollection<string>)GetValue(DateRangeListProperty); }
            set { SetValue(DateRangeListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DirectionList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateRangeListProperty =
            DependencyProperty.Register("DateRangeList", typeof(ObservableCollection<string>), typeof(MoreDayPassengerFlowTrend), new PropertyMetadata(default(ObservableCollection<string>)));

        /// <summary>
        /// 选择的客流方向
        /// </summary>
        internal string SelectedDateRange
        {
            get { return (string)GetValue(SelectedDateRangeProperty); }
            set { SetValue(SelectedDateRangeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedDirection.  This enables animation, styling, binding, etc...
        internal static readonly DependencyProperty SelectedDateRangeProperty =
            DependencyProperty.Register("SelectedDateRange", typeof(string), typeof(MoreDayPassengerFlowTrend), new PropertyMetadata(default(string),
                (o, args) =>
                {
                    var ct1 = (OneDayPassengerFlowTrend)o;
                    var v = (string)args.NewValue;
                    ct1.RaiseEvent(new RoutedPropertyChangedEventArgs<string>((string)args.OldValue, (string)args.NewValue, ParamChangedEvent));
                }));

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
            DependencyProperty.Register("FlowForeground", typeof(Brush), typeof(MoreDayPassengerFlowTrend), new PropertyMetadata(Brushes.Black));

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
            DependencyProperty.Register("FlowBackgroud", typeof(Brush), typeof(MoreDayPassengerFlowTrend), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6308506C"))));


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
            DependencyProperty.Register("CheckedBackground", typeof(Brush), typeof(MoreDayPassengerFlowTrend), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF08506C"))));

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
            DependencyProperty.Register("MouseHoverBackground", typeof(Brush), typeof(MoreDayPassengerFlowTrend), new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF08506C"))));

        /// <summary>
        /// 图表名称
        /// </summary>
        public string SeriesTitle
        {
            get { return (string)GetValue(SeriesTitleProperty); }
            set { SetValue(SeriesTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SeriesTitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeriesTitleProperty =
            DependencyProperty.Register("SeriesTitle", typeof(string), typeof(MoreDayPassengerFlowTrend), new PropertyMetadata(default(string)));

        public ChartValues<double> ChartValues
        {
            get { return (ChartValues<double>)GetValue(ChartValuesProperty); }
            set { SetValue(ChartValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChartValues.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChartValuesProperty =
            DependencyProperty.Register("ChartValues", typeof(ChartValues<double>), typeof(MoreDayPassengerFlowTrend), new PropertyMetadata(default(ChartValues<double>)));

        public string[] Labels
        {
            get { return (string[])GetValue(LabelsProperty); }
            set { SetValue(LabelsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Labels.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelsProperty =
            DependencyProperty.Register("Labels", typeof(string[]), typeof(MoreDayPassengerFlowTrend), new PropertyMetadata(default(string[]),
                (o,args)=>
                {
                    var ct1 = (MoreDayPassengerFlowTrend)o;
                    var v = (string[])args.NewValue;

                    if(v!=null)
                    {
                        ct1.XAxisMin = -0.1;
                        var num = v.Count();
                        ct1.XAxisMax = num > 15 ? 15.1 : num + 0.1;
                    }
                }));

        public double XAxisMin
        {
            get { return (double)GetValue(XAxisMinProperty); }
            set { SetValue(XAxisMinProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XAxisMin.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XAxisMinProperty =
            DependencyProperty.Register("XAxisMin", typeof(double), typeof(MoreDayPassengerFlowTrend), new PropertyMetadata(default(double)));

        public double XAxisMax
        {
            get { return (double)GetValue(XAxisMaxProperty); }
            set { SetValue(XAxisMaxProperty, value); }
        }

        // Using a DependencyProperty as the backing store for XAxisMax.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XAxisMaxProperty =
            DependencyProperty.Register("XAxisMax", typeof(double), typeof(MoreDayPassengerFlowTrend), new PropertyMetadata(default(double)));

        public Func<double,string> Formatter
        {
            get { return (Func<double,string>)GetValue(FormatterProperty); }
            set { SetValue(FormatterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Formatter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormatterProperty =
            DependencyProperty.Register("Formatter", typeof(Func<double,string>), typeof(MoreDayPassengerFlowTrend), new PropertyMetadata(default(Func<double,string>)));

        
        public static readonly RoutedEvent ParamChangedEvent =
            EventManager.RegisterRoutedEvent("ParamChangedEvent", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<string>), typeof(MoreDayPassengerFlowTrend));
       
        /// <summary>
        /// 条件改变触发事件
        /// </summary>
        public event RoutedPropertyChangedEventHandler<string> ParamChanged
        {
            add => AddHandler(ParamChangedEvent, value);
            remove => RemoveHandler(ParamChangedEvent, value);
        }

    }
}

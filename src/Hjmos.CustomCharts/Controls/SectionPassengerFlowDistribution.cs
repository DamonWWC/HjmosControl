using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Hjmos.CustomCharts.Controls
{
    public class SectionPassengerFlowDistribution : Control
    {
        public SectionPassengerFlowDistribution()
        {
            this.Loaded += SectionPassengerFlowDistribution_Loaded;
        }

        private void SectionPassengerFlowDistribution_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if(Formatter==null)
            {
                Formatter = value => value == 0 ? value.ToString() : string.Format("{0:N1}K", Math.Abs(value) / 1000);
            }
        }


        private object GetChartValue(object value)
        {
            if(value is IList<double> FlowData)
            {
              
                int n = FlowData.Count;
                for(int i=0;i<2*n+1;i+=2)
                {
                    FlowData.Insert(i, 0);
                }
                return FlowData;

            }
            else if(value is IList<string> Label)
            {
                int n = Label.Count;
                for(int i=1;i<2*n-1;i+=2)
                {
                    Label.Insert(i, "");
                }
                return Label.ToArray();               
            }
            return null;
        }

        #region Data
        /// <summary>
        /// 上行区间客流
        /// </summary>
        public ChartValues<double> UpFlowDatas
        {
            get { return (ChartValues<double>)GetValue(UpFlowDatasProperty); }
            set { SetValue(UpFlowDatasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UpFlowDatas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpFlowDatasProperty =
            DependencyProperty.Register("UpFlowDatas", typeof(ChartValues<double>), typeof(SectionPassengerFlowDistribution), new FrameworkPropertyMetadata(default(ChartValues<double>), OnUpFlowDatasChanged, CoerceUpFlowDatas));

        private static void OnUpFlowDatasChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private static object CoerceUpFlowDatas(DependencyObject d, object value)
        {
            if (value == null) return DependencyProperty.UnsetValue;
            var ct1 = d as SectionPassengerFlowDistribution;
            return ct1.GetChartValue(value);     
        }

        /// <summary>
        /// 下行区间客流
        /// </summary>
        public ChartValues<double> DownFlowDatas
        {
            get { return (ChartValues<double>)GetValue(DownFlowDatasProperty); }
            set { SetValue(DownFlowDatasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UpFlowDatas.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DownFlowDatasProperty =
            DependencyProperty.Register("DownFlowDatas", typeof(ChartValues<double>), typeof(SectionPassengerFlowDistribution), new PropertyMetadata(default(ChartValues<double>), OnDownFlowDatasChanged, CoerceDownFlowDatas));

        private static void OnDownFlowDatasChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private static object CoerceDownFlowDatas(DependencyObject d, object value)
        {
            if (value == null) return DependencyProperty.UnsetValue;
            var ct1 = d as SectionPassengerFlowDistribution;
            var flowData = value as ChartValues<double>;
            for(int i=0;i<flowData.Count;i++)
            {
                flowData[i] *= -1;
            }
            return ct1.GetChartValue(flowData);
        }


        /// <summary>
        /// 横坐标车站
        /// </summary>
        public string[] Labels
        {
            get { return (string[])GetValue(LabelsProperty); }
            set { SetValue(LabelsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Labels.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelsProperty =
            DependencyProperty.Register("Labels", typeof(string[]), typeof(SectionPassengerFlowDistribution), new PropertyMetadata(default(string[]),OnLabelsChanged,CoerceLabels));
        private static void OnLabelsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private static object CoerceLabels(DependencyObject d, object value)
        {
            if (value == null) return DependencyProperty.UnsetValue;
            var ct1 = d as SectionPassengerFlowDistribution;
            var label = value as string[];
            var data= ct1.GetChartValue(label.ToList());
            return data;
        }


        /// <summary>
        /// 纵坐标数据显示样式
        /// </summary>
        public Func<double, string> Formatter
        {
            get { return (Func<double, string>)GetValue(FormatterProperty); }
            set { SetValue(FormatterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Formatter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormatterProperty =
            DependencyProperty.Register("Formatter", typeof(Func<double, string>), typeof(SectionPassengerFlowDistribution), new PropertyMetadata(default(Func<double, string>)));




        #endregion


    }
}

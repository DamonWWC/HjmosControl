using LiveCharts;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Hjmos.CustomCharts.Controls
{
    public class DeviceAlarmBarChart : Control
    {

        public DeviceAlarmBarChart()
        {
            this.Loaded += DeviceAlarmBarChart_Loaded;
        }

        private void DeviceAlarmBarChart_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if(YFormatter==null)
            {
                YFormatter = value => value.ToString("N0");
            }
            
            DateTime now = DateTime.Now;
            DateTime firstDate = new DateTime(now.Year, now.Month, now.Day, 6, 0, 0);
            DateTime lastDate = new DateTime(now.Year, now.Month, now.Day+1, 0, 0, 0);
            List<string> labels = new List<string>();
            while(firstDate<=lastDate)
            {
                labels.Add(firstDate.ToString("H:mm"));
                firstDate=firstDate.AddHours(1);
            }
            Labels = labels.ToArray();                
        }


        public string SeriesTtitle
        {
            get { return (string)GetValue(SeriesTtitleProperty); }
            set { SetValue(SeriesTtitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SeriesTtitle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeriesTtitleProperty =
            DependencyProperty.Register("SeriesTtitle", typeof(string), typeof(DeviceAlarmBarChart), new PropertyMetadata(default(string)));


        public ChartValues<double> ChartValues
        {
            get { return (ChartValues<double>)GetValue(ChartValuesProperty); }
            set { SetValue(ChartValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChartValues.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ChartValuesProperty =
            DependencyProperty.Register("ChartValues", typeof(ChartValues<double>), typeof(DeviceAlarmBarChart), new PropertyMetadata(default(ChartValues<double>)));


        public string[] Labels
        {
            get { return (string[])GetValue(LabelsProperty); }
            set { SetValue(LabelsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Labels.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelsProperty =
            DependencyProperty.Register("Labels", typeof(string[]), typeof(DeviceAlarmBarChart), new PropertyMetadata(default(string[])));


        public Func<double, string> YFormatter
        {
            get { return (Func<double, string>)GetValue(YFormatterProperty); }
            set { SetValue(YFormatterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Formatter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YFormatterProperty =
            DependencyProperty.Register("YFormatter", typeof(Func<double, string>), typeof(DeviceAlarmBarChart), new PropertyMetadata(default(Func<double, string>)));


        public Func<double, string> XFormatter
        {
            get { return (Func<double, string>)GetValue(XFormatterProperty); }
            set { SetValue(XFormatterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Formatter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XFormatterProperty =
            DependencyProperty.Register("XFormatter", typeof(Func<double, string>), typeof(DeviceAlarmBarChart), new PropertyMetadata(default(Func<double, string>)));
    }
}

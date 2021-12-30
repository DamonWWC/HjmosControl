using System.Collections.Generic;
using System.Windows;

namespace Hjmos.BaseControls.Controls
{
    public class RadarSeries : FrameworkElement
    {
        public string SeriesName
        {
            get { return (string)GetValue(SeriesNameProperty); }
            set { SetValue(SeriesNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SeriesName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeriesNameProperty =
            DependencyProperty.Register("SeriesName", typeof(string), typeof(RadarSeries), new PropertyMetadata(default));

        /// <summary>
        /// 值
        /// </summary>
        public IList<double> Values
        {
            get { return (IList<double>)GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Values.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValuesProperty =
            DependencyProperty.Register("Values", typeof(IList<double>), typeof(RadarSeries), new PropertyMetadata(default, (d, e) =>
             {
                 RadarSeries radarSeries = d as RadarSeries;
                 if (radarSeries.RadarChartModel != null && e.NewValue != null)
                 {
                     radarSeries.RadarChartModel.Updater();
                 }
             }));

        public IRadarChart RadarChartModel { get; set; }
    }
}
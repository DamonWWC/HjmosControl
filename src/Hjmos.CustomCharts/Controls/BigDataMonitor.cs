using Hjmos.CustomCharts.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Hjmos.CustomCharts.Controls
{
    public class BigDataMonitor : Control
    {

        public BigDataMonitor()
        {

        }

        /// <summary>
        /// 数据总量
        /// </summary>
        public long TotalQuanity
        {
            get { return (long)GetValue(TotalQuanityProperty); }
            set { SetValue(TotalQuanityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TotalQuanity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalQuanityProperty =
            DependencyProperty.Register("TotalQuanity", typeof(long), typeof(BigDataMonitor), new PropertyMetadata(0L));






        public ObservableCollection<BigDataItemModel> BigDataList
        {
            get { return (ObservableCollection<BigDataItemModel>)GetValue(BigDataListProperty); }
            set { SetValue(BigDataListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BigDataList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BigDataListProperty =
            DependencyProperty.Register("BigDataList", typeof(ObservableCollection<BigDataItemModel>), typeof(BigDataMonitor), new PropertyMetadata(default(ObservableCollection<BigDataItemModel>)));





    }
}

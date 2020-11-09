using Hjmos.CustomCharts.Tools.Extension;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace Hjmos.CustomCharts.Controls
{
    public class AverageDailyPassengerFlowTop10 : ItemsControl
    {
        public AverageDailyPassengerFlowTop10()
        {
            this.SizeChanged += AverageDailyPassengerFlowTop10_SizeChanged;
        }
        
        private void AverageDailyPassengerFlowTop10_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ItemsSource != null)
            {
                SetWidth((ObservableCollection<FlowData>)ItemsSource, ActualWidth);
            }
        }
       /// <summary>
       /// 数据
       /// </summary>
        public ObservableCollection<FlowData> FlowDatas
        {
            get { return (ObservableCollection<FlowData>)GetValue(FlowDatasProperty); }
            set { SetValue(FlowDatasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FlowDatasProperty =
            DependencyProperty.Register("FlowDatas", typeof(ObservableCollection<FlowData>), typeof(AverageDailyPassengerFlowTop10), new PropertyMetadata(default(ObservableCollection<FlowData>),
                (o,args)=>
                {
                    var ct1 = (AverageDailyPassengerFlowTop10)o;
                    var v = (ObservableCollection<FlowData>)args.NewValue;                  
                    if(v != null)
                    {
                        v.Descend();
                        SetWidth(v, ct1.ActualWidth);
                        ct1.ItemsSource = v;
                    }
                }));


        private static void SetWidth(ObservableCollection<FlowData> flowDatas,double actualwidth)
        {
            var maxvalue = flowDatas.Max(p => p.PostValue);
            var proportion = (actualwidth-200) / maxvalue;
            for(int i=0;i<flowDatas.Count();i++)
            {
                flowDatas[i].Width = proportion * flowDatas[i].PostValue;
            }          
        }
      
    }
}

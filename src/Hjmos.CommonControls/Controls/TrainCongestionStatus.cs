using Hjmos.CommonControls.Tools.Extension;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Hjmos.CommonControls.Controls
{
    public class TrainCongestionStatus : ListBox
    {

        public TrainCongestionStatus()
        {

        }


        public ObservableCollection<TrainCongestionData> TrainCongestionDatas
        {
            get { return (ObservableCollection<TrainCongestionData>)GetValue(TrainCongestionDatasProperty); }
            set { SetValue(TrainCongestionDatasProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TrainCongestionData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrainCongestionDatasProperty =
            DependencyProperty.Register("TrainCongestionDatas", typeof(ObservableCollection<TrainCongestionData>), typeof(TrainCongestionStatus), new PropertyMetadata(default(ObservableCollection<TrainCongestionData>),
                (o,args)=>
                {
                    var ct1 = (TrainCongestionStatus)o;
                    var v = (ObservableCollection<TrainCongestionData>)args.NewValue;
                    if (v != null)
                    {
                        v.Ascend();
                        ct1.ItemsSource = v;
                    }
                }));
    }
}

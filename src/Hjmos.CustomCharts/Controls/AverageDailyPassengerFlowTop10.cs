using Hjmos.BaseControls.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;

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
                    var newvalue = (ObservableCollection<FlowData>)args.NewValue;
                    var oldvalue = args.OldValue;
                    if(newvalue != null)
                    {
                        newvalue.Sort<FlowData>();
                        SetWidth(newvalue, ct1.ActualWidth);
                        ct1.ItemsSource = newvalue;
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

    public static class ObservableExtension
    {
        public static void Sort<T>(this ObservableCollection<T> collection) where T : IComparable<T>
        {
            List<T> sortedList = collection.OrderByDescending(x => x).ToList();//降序
            for (int i = 0; i < sortedList.Count(); i++)
            {
                collection.Move(collection.IndexOf(sortedList[i]), i);
            }
        }
    }

    public class FlowData:INotifyPropertyChanged,IComparable<FlowData>
    {
        private double _Width;
        public double Width
        {
            get { return _Width; }
            set
            {
                if(_Width != value)
                {
                    _Width = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _Title;
        public string Title 
        {
            get { return _Title; }
            set
            {
                if(_Title!=value)
                {
                    _Title = value;
                    OnPropertyChanged();
                }
            }
        }


        private double _RealTimeValue;
        public double RealTimeValue
        {
            get { return _RealTimeValue; }
            set
            {
                if(_RealTimeValue!=value)
                {
                    _RealTimeValue = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _PostValue;
        public double PostValue
        {
            get { return _PostValue; }
            set
            {
                if(_PostValue!=value)
                {
                    _PostValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public int CompareTo(FlowData other)
        {
            return this._RealTimeValue.CompareTo(other._RealTimeValue);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}

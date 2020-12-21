using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Controltest
{
    /// <summary>
    /// Window4.xaml 的交互逻辑
    /// </summary>
    public partial class Window4 : Window
    {
        public Window4()
        {
            InitializeComponent();

            Formatter = value => value == 0 ? value.ToString() : string.Format("{0:N1}K",Math.Abs(value) / 1000);

            var v1 = new List<double> { 1000, 2000, 1000, 2000, 1000, 2000, 1000, 2000, 1000 };
            var v2=new List<double> { 1000, 2000, 1000, 2000, 1000, 2000, 1000, 2000, 1000 };
            var label = new List<string> { "员村", "天河公园", "棠东", "黄村", "大观南路", "天河智慧城", "神州路", "科学城", "苏元", "水西" };
            //Value1 = new ChartValues<double>( GetRealValue(v1) as List<double>);
            //Value2 =new ChartValues<double>( GetRealValue(v2) as List<double>);
            //Labels =(GetRealValue(label) as List<string>).ToArray();
            Value1 = new ChartValues<double>(v1);
            Value2 = new ChartValues<double>(v2);
            Labels = label.ToArray();

            Lines = new List<string> { "1号线", "1号线", "1号线", "1号线", "1号线", "1号线", "1号线", "1号线" };
            DataContext = this;
        }

        private object GetRealValue(object value)
        {

            if(value is IList<double> va)
            {
                int i = 0;
                int n = va.Count;
                while(i<2*n+1)
                {
                    va.Insert(i, 0);
                    i += 2;
                }
                return va;
                   
            }
            else if(value is List<string> va1)
            {
                int n = va1.Count;
                for (int i=1;i<2*n-1;i+=2)
                {
                    va1.Insert(i, "");                  
                }
                return va1;
            }
            return null;
        }


        public List<string> Lines { get; set; }

        public Func<double,string> Formatter { get; set; }

        public ChartValues<double> Value1 { get; set; }
        public ChartValues<double> Value2 { get; set; }

        public string[] Labels { get; set; }
    }


    
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controltest
{
    /// <summary>
    /// UserControl2.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl2 : UserControl, INotifyPropertyChanged
    {
        public UserControl2()
        {
            InitializeComponent();


            TypeData =new List<Data> { new Data { TypeName = "预警事件类型", AlarmEventType = new List<string> { "运力非正常下降", "接触轨失电", "地外伤害", "异物侵入" }, SelectionMode=SelectionMode.Multiple },
                                       new Data { TypeName = "预警发布单位", AlarmEventType = new List<string> { "线网中心", "线路控制中心", "气象台", "气象局" },SelectionMode=SelectionMode.Multiple },
                                       new Data { TypeName = "预警发布时间", AlarmEventType = new List<string> { "今天", "昨天", "过去一周", "未来一周" },SelectionMode=SelectionMode.Single } };

            DataList = GetDataList();
            DataContext = this;
        }

        private ObservableCollection<DemoDataModel> _DataList;
        public ObservableCollection<DemoDataModel> DataList
        {
            get { return _DataList; }
            set { _DataList = value; OnPropertyChanged(); }
        }

        private List<Data> _TypeData;

        public List<Data> TypeData
        {
            get { return _TypeData; }
            set { _TypeData = value; }
        }


        private ObservableCollection<DemoDataModel> GetDataList()
        {
            return new ObservableCollection<DemoDataModel>
            {
                new DemoDataModel{ Index = 1,  Name = "Name1", IsSelected = false,  Remark = "111" },
                new DemoDataModel{ Index = 2,  Name = "Name2", IsSelected = true,  Remark = "222" },
                new DemoDataModel{ Index = 3,  Name = "Name3", IsSelected = true,  Remark = "333" },
                new DemoDataModel{ Index = 4,  Name = "Name4", IsSelected = false,  Remark = "444" },
                new DemoDataModel{ Index = 5,  Name = "Name5", IsSelected = false,  Remark = "555" },
                new DemoDataModel{ Index = 6,  Name = "Name6", IsSelected = false,  Remark = "666" },
                new DemoDataModel{ Index = 7,  Name = "Name7", IsSelected = true,  Remark = "777" },
                new DemoDataModel{ Index = 8,  Name = "Name8", IsSelected = false,  Remark = "888" },
                new DemoDataModel{ Index = 9,  Name = "Name9", IsSelected = false,  Remark = "999" },
            };
        }



        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

    public class Data
    {
        public string TypeName { get; set; }
        public List<string> AlarmEventType { get; set; }
        public SelectionMode SelectionMode { get; set; }
    }

}

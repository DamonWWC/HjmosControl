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
    /// UserControl4.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl4 : UserControl, INotifyPropertyChanged
    {
        public UserControl4()
        {
            InitializeComponent();
            DataList=new ObservableCollection<string> {  "获取突发事件情况", "向公司领导汇报", "向上级汇报", "协调指挥运营企业" };
        
            DataContext = this;
        }

        private ObservableCollection<string> _DataList;
        public ObservableCollection<string> DataList
        {
            get { return _DataList; }
            set { _DataList = value; OnPropertyChanged(); }
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

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controltest.LineFlood
{
    /// <summary>
    /// StationFloodSituation.xaml 的交互逻辑
    /// </summary>
    public partial class StationFloodSituation : UserControl
    {
        public StationFloodSituation()
        {
            InitializeComponent();
            DataContext = this;
            itemsSource = new List<FloodInformation>
            {
                new FloodInformation{StationName="湖南工业大学"},
                new FloodInformation{StationName ="白鸽站"},
                new FloodInformation{StationName ="湘雅三医院站"},
                new FloodInformation{StationName ="六沟垅站",IsChangeStation=true},
                new FloodInformation{StationName ="文昌阁站" ,IsChangeStation=true},
                new FloodInformation{StationName ="湘雅医院站"},
                new FloodInformation{StationName="湖南工业大学1"},
                new FloodInformation{StationName ="白鸽站1"},
                new FloodInformation{StationName ="湘雅三医院站1"},
                new FloodInformation{StationName ="六沟垅站1",IsChangeStation=true},
                new FloodInformation{StationName ="文昌阁站1" ,IsChangeStation=true},
                new FloodInformation{StationName ="湘雅医院站1" ,IsLast=true}
            };
        }

        public List<FloodInformation> itemsSource { get; set; }
    }
}
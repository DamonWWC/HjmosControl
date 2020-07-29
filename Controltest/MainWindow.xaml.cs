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

namespace Controltest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            data = new string[] { "asb", "bcd" };
            text = "客流";
            this.DataContext = this;
           
        }
        string[] _data;
        public string[] data
        {
            get { return _data; }
            set { _data = value; }
        }
        string _text;
        public string text
        {
            get { return _text; }
            set { _text = value; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HjmosControl.Controls.Growl.Info("Info Message", "InfoMessage");
        }

        private void StackPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ContextMenu menu = (sender as StackPanel).ContextMenu;
            MenuItem item = menu.Items[0] as MenuItem;
            item.Header = "清空";
        }

        private void colorPicker_SelectedColorChanged(object sender, HjmosControl.Data.FunctionEventArgs<Color> e)
        {

        }

        private void colorPicker_Canceled(object sender, EventArgs e)
        {

        }
    }
}

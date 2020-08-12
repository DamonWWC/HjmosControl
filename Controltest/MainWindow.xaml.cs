using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

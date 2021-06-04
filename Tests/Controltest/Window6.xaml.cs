using Hjmos.BaseControls.Controls;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Controltest
{
    /// <summary>
    /// Window6.xaml 的交互逻辑
    /// </summary>
    public partial class Window6 : FullScreenWindow, INotifyPropertyChanged
    {
        public Window6()
        {
            InitializeComponent();
            IsOpen = true;
            DataContext = this;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IsOpen = false;
        }

        private bool _IsOpen;
        public bool IsOpen
        {
            get { return _IsOpen; }
            set { _IsOpen=value; OnPropertyChanged(); }
        }
     
        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IsOpen = true;
        }

        private void UserControl3_MouseMove(object sender, MouseEventArgs e)
        {
            var obj = sender as UserControl3;
            var point = e.GetPosition(obj);
            ww.Text= string.Format("坐标{0},{1}", point.X, point.Y);

        }
    }
}

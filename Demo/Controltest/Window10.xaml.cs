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
    /// Window10.xaml 的交互逻辑
    /// </summary>
    public partial class Window10 : Window
    {
        public Window10()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Hjmos.BaseControls.Controls.MessageBox.Warning("1111122222222222222222222222221111111111111113333333333333333333333333333333333333333333333333333333333555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555555");
        }
    }
}
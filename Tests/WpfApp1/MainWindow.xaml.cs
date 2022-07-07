using NLog;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //ConfigurationBuilder configuration = new ConfigurationBuilder();
            
        }
        Logger logge1r = LogManager.GetLogger("file"); //初始化日志类
        Logger logger = LogManager.GetCurrentClassLogger(typeof(Logger));
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            logger.Info("2222");
            logger.Warn(new Exception("sss"),"1111");
        }
    }

    public class aa
    {

    }
}

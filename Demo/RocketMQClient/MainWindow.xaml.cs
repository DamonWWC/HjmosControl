using NewLife.RocketMQ;
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

namespace RocketMQClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // PublishMQ();
            RocketMqClient();
        }

        private void RocketMqClient()
        {
            var consumer = new NewLife.RocketMQ.Consumer
            {
                Topic = "TBW102",
                Group = "C#",
                NameServerAddress = "127.0.0.1:8089",
                BatchSize = 1,
                Server = "*",
                AccessKey = "*"
            };

            consumer.OnConsume = (q, s) =>
              {
                  return true;
              };
            consumer.Start();
        }

        private void PublishMQ()
        {
            var mq = new Producer
            {
                Topic = "C#Test",
                NameServerAddress = "127.0.0.1:8089"
            };
            mq.Start();

            string log = "111";
            var sr = mq.Publish(log, "tagA");
        }
    }
}
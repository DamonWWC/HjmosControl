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
using Hjmos.MQProxy;

namespace Controltest
{
    /// <summary>
    /// Window14.xaml 的交互逻辑
    /// </summary>
    public partial class Window14 : Window
    {
        private IConsumer consumer;

        public Window14()
        {
            InitializeComponent();
            consumer = ConnectionFactory.PushConsumer(new RocketMQPara
            {
                //Topic= "device_systemAlarmCount",
                NameServerAddress = "http://10.51.9.130:30899",
                ConsumerGroupID = "Holoception"
            });
            consumer.Subscribe("emergency-command", "*");
            consumer.OnConsume = Receive;
            var resutl = consumer.Start();
        }

        private void Receive(IMessage message)
        {
        }
    }
}
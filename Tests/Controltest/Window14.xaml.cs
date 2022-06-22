using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Controltest
{
    /// <summary>
    /// Window14.xaml 的交互逻辑
    /// </summary>
    public partial class Window14 : Window
    {
        private IConsumer consumer1;
        private IConsumer consumer2;

        BlockingCollection<string> linkEventInfos = new BlockingCollection<string>();
        public Window14()
        {
            InitializeComponent();


            linkEventInfos.Add("1");
            linkEventInfos.Add("2");
            linkEventInfos.Add("3");
            linkEventInfos.Add("4");


            linkEventInfos.TryTake(out string aa);
            linkEventInfos.TryTake(out string bb);
            linkEventInfos.TryTake(out string cc);
            linkEventInfos.TryTake(out string dd);

            //SetTime();


            //var dic = new Dictionary<string, string>()
            //{
            //    {"1","11" },
            //    {"2", "22"}
            //};

            //set(dic);

            //var key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCaiEkusx7F3oY/1lGCLol9SzE54znGJSjgRNRRmWsy1tQQMEA8l76Eqih36/Qv+67yZDJ6RTaewpD7mr+Qm7PwniR2y0iDQf7ieOHabxpCSgRSfBGJPO+YGjNk7R4od2QvAr1zwvvj7Bek0+PgCIrFNY20buMVRYmu6SVBHzEM9QIDAQAB";
            ////var ss = RSAEncrypt($"<RSAKeyValue><Modulus>{key}</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>", "123454");
            //var sss = RSAEncrypt(key, "123456");
            //string password = "1234";
            //string salt = BCrypt.Net.BCrypt.GenerateSalt(10, 'a');
            //string passwordHash = BCrypt.Net.BCrypt.HashPassword(password, salt);

            //var aa = BCrypt.Net.BCrypt.Verify("1234", passwordHash);


            //consumer1 = ConnectionFactory.PushConsumer(new RocketMQPara
            //{
            //    //Topic= "device_systemAlarmCount",
            //    NameServerAddress = "http://10.51.9.130:30076",
            //    ConsumerGroupID = "Holoception1"
            //});
            //consumer1.Subscribe("emergency-command", "*");
            //consumer1.Subscribe("pflow_todayTypePflowTrend", "*");//线路/车站 PF1003 客流实时趋势
            //consumer1.Subscribe("pflow_transferStationSituation", "*");//线路 PF1004 换乘站情况
            //consumer1.OnConsume = Receive;
            //var resutl = consumer1.Start();


            //consumer2 = ConnectionFactory.PushConsumer(new RocketMQPara
            //{

            //    NameServerAddress = "http://10.51.9.130:30076",
            //    ConsumerGroupID = "Holoception2"
            //});
            //consumer2.Subscribe("emergency-command", "*");
            //consumer2.Subscribe("pflow_todayTypePflowTrend", "*");//线路/车站 PF1003 客流实时趋势
            //consumer2.Subscribe("pflow_transferStationSituation", "*");//线路 PF1004 换乘站情况
            //consumer2.OnConsume = Receive;
            //var resut2 = consumer2.Start();






            //Test1 test1 = new Test1();
            //test1.Call(3);
        }

        private void SetTime()
        {
            var time = DateTime.Now - DateTime.Now.AddMinutes(-6000);
            int hour = (time.Days * 24 + time.Hours) ;
            int mim = time.Minutes;
            string tim = $"{time}小时{mim}分";
        }








        private void Receive(IMessage message)
        {
        }


        private string RSAEncrypt(string publickey,string password)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(ToXmlPublicKey(publickey));
            //var bty = Convert.FromBase64String(publickey);
            //rsa.ImportCspBlob(bty);
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(password), false);
            return Convert.ToBase64String(cipherbytes);

        }

        private string ToXmlPublicKey(string publicKey)
        {
            RsaKeyParameters p = PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey)) as RsaKeyParameters;
            using(RSACryptoServiceProvider rsa =new RSACryptoServiceProvider())
            {
                RSAParameters rsaparams = new RSAParameters
                {
                    Modulus = p.Modulus.ToByteArrayUnsigned(),
                    Exponent = p.Exponent.ToByteArrayUnsigned()
                };
                rsa.ImportParameters(rsaparams);
                return rsa.ToXmlString(false);
            }
        }


        private void set(Dictionary<string ,string> dic)
        {
            dic.Remove("1");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            window.Width = 100;
            window.Height = 100;
            window.Owner = this;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();

        }
    }


    public interface ITest
    {
        void Add(int a=1);
    }
}
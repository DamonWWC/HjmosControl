using System;
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
        private IConsumer consumer;

        public Window14()
        {
            InitializeComponent();

           

            var dic = new Dictionary<string, string>()
            {
                {"1","11" },
                {"2", "22"}
            };

            set(dic);

            var key = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCaiEkusx7F3oY/1lGCLol9SzE54znGJSjgRNRRmWsy1tQQMEA8l76Eqih36/Qv+67yZDJ6RTaewpD7mr+Qm7PwniR2y0iDQf7ieOHabxpCSgRSfBGJPO+YGjNk7R4od2QvAr1zwvvj7Bek0+PgCIrFNY20buMVRYmu6SVBHzEM9QIDAQAB";
            //var ss = RSAEncrypt($"<RSAKeyValue><Modulus>{key}</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>", "123454");
            var sss = RSAEncrypt(key, "123456");
            string password = "1234";
            string salt = BCrypt.Net.BCrypt.GenerateSalt(10, 'a');
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password, salt);

            var aa = BCrypt.Net.BCrypt.Verify("1234", passwordHash);


            //consumer = ConnectionFactory.PushConsumer(new RocketMQPara
            //{
            //    //Topic= "device_systemAlarmCount",
            //    NameServerAddress = "http://10.51.9.130:30899",
            //    ConsumerGroupID = "Holoception"
            //});
            //consumer.Subscribe("emergency-command", "*");
            //consumer.OnConsume = Receive;
            //var resutl = consumer.Start();
            //Test1 test1 = new Test1();
            //test1.Call(3);
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
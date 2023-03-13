using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        //D:\A日常编程练习\C#\HjmosControl\Tests\Work
        //C:\Users\BAS\Documents\Projects\Mics3.0_Client\ISCSClient\Work
        Assembly ass = Assembly.LoadFrom(@"D:\A日常编程练习\C#\HjmosControl\Tests\Work\WCFServerInterface.dll");

        Assembly assparam = Assembly.LoadFrom(@"D:\A日常编程练习\C#\HjmosControl\Tests\Work\MICSClient.Core.dll");
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Assembly ass = Assembly.LoadFrom(@"D:\A日常编程练习\C#\HjmosControl\Tests\ClassLibrary1\bin\Debug\net6.0\ClassLibrary1.dll");
            //Type? t = ass.GetType("ClassLibrary1.Class1");




            //var method = t.GetMethod("GetData");


            //var insta = t.GetField("Instance");




            //var cmdtype=ass.GetType("ClassLibrary1.CmdType");

            //var nam = cmdtype.GetField("StartProcess");


            //var cmd = Enum.Parse(cmdtype, "StartProcess");


            //Type? data = ass.GetType("ClassLibrary1.DataDemo");

            //object obj = Activator.CreateInstance(data);
            //var argument = data.GetProperty("Argument");
            //argument.SetValue(obj, "2345");
            //var type = data.GetProperty("Type");
            //type.SetValue(obj, cmd);

            //var result = method.Invoke(insta.GetValue(null), new object[] { obj });

            Type? wcfclient = ass.GetType("WCFServerInterface.ConsoleClient");

            var instance = wcfclient?.GetField("Instance");

            var init = wcfclient?.GetMethod("Init");

            Type? cmdtype = assparam.GetType("MICSClient.Core.Domain.DataDef.CmdType");
            var cmd = Enum.Parse(cmdtype, "StartProcess");

            Array array = Array.CreateInstance(cmdtype, 1);
            array.SetValue(cmd, 0);
            init?.Invoke(instance.GetValue(null), new object[] { array });


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
               
                Type? wcfclient = ass.GetType("WCFServerInterface.ConsoleClient");
                var instance = wcfclient?.GetField("Instance");
                var sendcommand = wcfclient?.GetMethod("SendCommand");


                #region param

             


                #region cmdtype

                Type? cmdtype = assparam.GetType("MICSClient.Core.Domain.DataDef.CmdType");
                var cmd = Enum.Parse(cmdtype, "StartProcess");




                #endregion



                Type? consolecommand = assparam.GetType("MICSClient.Core.Domain.Model.ConsoleCommand");
                object obj = Activator.CreateInstance(consolecommand);

                var argument = consolecommand.GetProperty("Argument");
                var type = consolecommand.GetProperty("Type");
                var parameter = consolecommand.GetProperty("Parameter");


                type?.SetValue(obj, cmd);
                argument?.SetValue(obj, "MICSClient.BASManager.exe");

                parameter?.SetValue(obj, $"--type=4 --Left=0");

                #endregion


                sendcommand?.Invoke(instance.GetValue(null), new object[] { obj });
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
        }
    }

    public class aa
    {

    }
}

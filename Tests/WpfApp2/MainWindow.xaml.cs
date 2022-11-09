using System;
using System.Diagnostics;
using System.IO;
using System.Windows;


namespace WpfApp2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //System.Environment.CurrentDirectory = "D:\\A日常编程练习\\C#\\HjmosControl\\Tests\\Work";
        }
        //D:\A日常编程练习\C#\HjmosControl\Tests\Work
        //C:\Users\BAS\Documents\Projects\Mics3.0_Client\ISCSClient\Work
       // Assembly ass = Assembly.LoadFrom(@"C:\Users\BAS\Desktop\WpfApp2\bin\Debug\WCFServerInterface.dll");

     //   Assembly assparam = Assembly.LoadFrom(@"C:\Users\BAS\Desktop\WpfApp2\bin\Debug\MICSClient.Core.dll");
        //Assembly aa= Assembly.LoadFrom(@"C:\Users\BAS\Documents\Projects\Mics3.0_Client\ISCSClient\Work\PCI.Framework.Log.dll");
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var aa = System.IO.Directory.GetCurrentDirectory();
            var bb = AppDomain.CurrentDomain.BaseDirectory;
            var dd = Process.GetCurrentProcess().MainModule.FileName;
            var cc = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);



            string ConfigPath = $"{AppDomain.CurrentDomain.BaseDirectory}\\config\\Hjmos_Ncc.config";
            var isfile = File.Exists(ConfigPath);
            ExecuteInCmd("mklink /d  \"D:\\A日常编程练习\\C#\\HjmosControl\\Tests\\WpfApp2\\bin\\Debug\\config\" \"D:\\A日常编程练习\\C#\\HjmosControl\\Tests\\mics\"");
            //Type wcfclient = ass.GetType("WCFServerInterface.ConsoleClient");

            //var instance = wcfclient?.GetField("Instance");

            //var init = wcfclient?.GetMethod("Init");

            //Type cmdtype = assparam.GetType("MICSClient.Core.Domain.DataDef.CmdType");
            //var cmd = Enum.Parse(cmdtype, "StartProcess");

            //Array array = Array.CreateInstance(cmdtype, 1);
            //array.SetValue(cmd, 0);
            //init?.Invoke(instance.GetValue(null), new object[] { array });
        }
        public  string ExecuteInCmd(string cmdline)
        {
            using (var process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.StandardInput.AutoFlush = true;
                process.StandardInput.WriteLine(cmdline + "&exit");

                //获取cmd窗口的输出信息  
                string output = process.StandardOutput.ReadToEnd();

                process.WaitForExit();
                process.Close();
                return output;
            }
        }
        public  string ExecuteOutCmd(string argument, string applocaltion)
        {
            using (var process = new Process())
            {
                process.StartInfo.Arguments = argument;
                process.StartInfo.FileName = applocaltion;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.StandardInput.AutoFlush = true;
                process.StandardInput.WriteLine("exit");

                //获取cmd窗口的输出信息  
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                process.Close();
                return output;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //try
            //{

            //    Type wcfclient = ass.GetType("WCFServerInterface.ConsoleClient");
            //    var instance = wcfclient?.GetField("Instance");
            //    var sendcommand = wcfclient?.GetMethod("SendCommand");


            //    #region param




            //    #region cmdtype

            //    Type cmdtype = assparam.GetType("MICSClient.Core.Domain.DataDef.CmdType");
            //    var cmd = Enum.Parse(cmdtype, "StartProcess");




            //    #endregion



            //    Type consolecommand = assparam.GetType("MICSClient.Core.Domain.Model.ConsoleCommand");
            //    object obj = Activator.CreateInstance(consolecommand);

            //    var argument = consolecommand.GetProperty("Argument");
            //    var type = consolecommand.GetProperty("Type");
            //    var parameter = consolecommand.GetProperty("Parameter");


            //    type.SetValue(obj, cmd,null);
            //    argument?.SetValue(obj, "MICSClient.BASManager.exe",null);

            //    parameter?.SetValue(obj, $"--type=4 --Left=0",null);

            //    #endregion


            //    sendcommand?.Invoke(instance.GetValue(null), new object[] { obj });
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }
    }
}

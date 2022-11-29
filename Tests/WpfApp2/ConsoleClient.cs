using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WpfApp2
{
    public class ConsoleClient
    {
        private static readonly Lazy<ConsoleClient> ConsoleClientInstance = new Lazy<ConsoleClient>(() => new ConsoleClient());
        public static ConsoleClient Instance => ConsoleClientInstance.Value;
        private ConsoleClient()
        {
            SetAssembly();
        }

        private string GetMicsPath()
        {
            //var index = AddressConfigEnum.MicsClinetPath.GetValue().LastIndexOf('\\');
            //var micsbaseDirectory = AddressConfigEnum.MicsClinetPath.GetValue().Substring(0, index);
            return @"C:\Users\BAS\Documents\Projects\Mics3.0_Client\ISCSClient\Work";
        }
        Assembly WCFServerInterface;
        Assembly MicsClientCore;
        private void SetAssembly()
        {
            try
            {
                var wcfserverPath = $"{GetMicsPath()}\\WCFServerInterface.dll";
                var micsClinetPath = $"{GetMicsPath()}\\MICSClient.Core.dll";
                if (!File.Exists(wcfserverPath) || !File.Exists(micsClinetPath)) throw new Exception("综合监控目录内对应的库不存在！");
                WCFServerInterface = Assembly.LoadFrom(wcfserverPath);
                MicsClientCore = Assembly.LoadFrom(micsClinetPath);
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex);
            }
        }

        public void InitClient()
        {
            try
            {
                Type wcfClient = WCFServerInterface.GetType("WCFServerInterface.ConsoleClient");
                var wcfClinetInstance = wcfClient?.GetField("Instance");
                var initMethod = wcfClient?.GetMethod("Init");
                #region param
                Type cmdType = MicsClientCore.GetType("MICSClient.Core.Domain.DataDef.CmdType");
                var cmdStartProcess = Enum.Parse(cmdType, "StartProcess");
                Array cmdArray = Array.CreateInstance(cmdType, 1);
                cmdArray.SetValue(cmdStartProcess, 0);
                #endregion

                initMethod?.Invoke(wcfClinetInstance.GetValue(null), new object[] { cmdArray });

            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">对应这综合监控的argument</param>
        /// <param name="argument">对应这综合监控的parameter</param>
        public void SendCommand(string value, string argument)
        {
            try
            {
                Type wcfClient = WCFServerInterface.GetType("WCFServerInterface.ConsoleClient");
                var wcfClinetInstance = wcfClient?.GetField("Instance");
                var sendCommandMethod = wcfClient?.GetMethod("SendCommand");

                #region param
                Type cmdType = MicsClientCore.GetType("MICSClient.Core.Domain.DataDef.CmdType");
                var cmdStartProcess = Enum.Parse(cmdType, "StartProcess");


                Type consoleCommandType = MicsClientCore.GetType("MICSClient.Core.Domain.Model.ConsoleCommand");

                object consoleCommand = Activator.CreateInstance(consoleCommandType);
                var argumentPara = consoleCommandType.GetProperty("Argument");
                var type = consoleCommandType.GetProperty("Type");
                var parameter = consoleCommandType.GetProperty("Parameter");

                type?.SetValue(consoleCommand, cmdStartProcess);
                argumentPara?.SetValue(consoleCommand, value);

                //var screen = Application.Current.MainWindow.GetScreen();
                //var left = screen.WorkingArea.Left;
                double left = 0;
                parameter?.SetValue(consoleCommand, $"{argument} --Left={left}");


                #endregion

                sendCommandMethod?.Invoke(wcfClinetInstance.GetValue(null), new object[] { consoleCommand });
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex);
            }


        }


    }
}

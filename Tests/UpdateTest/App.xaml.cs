using AutoUpdaterDotNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace UpdateTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {

            //AutoUpdater.DownloadPath = Environment.CurrentDirectory;
           // AutoUpdater.Start("ftp://10.51.9.142/upload/AutoUpdater.xml", new NetworkCredential("newftpuser", "123456"));
            base.OnStartup(e);
           //BackgroundWorker background = new BackgroundWorker();
            //background.ProgressChanged += Background_ProgressChanged;
            
            
        }

        //private void Background_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        //{
            
        //}
    }
}

using AutoUpdaterDotNET;
using FluentFTP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
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


namespace UpdateTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //HjmosWebClient hjmosWebClient = new HjmosWebClient();

            // Process.Start(new ProcessStartInfo("https://baidu.com") { UseShellExecute = true });

            //UpLoadFile();
            // ftp.DownloadFileAsync(progress: p);
            //AutoUpdater.Start("ftp://10.51.9.142/upload/AutoUpdater.xml", new NetworkCredential("newftpuser", "123456"));
            var aa = BytesToString(1000);
            var bb = Byts(1024);
            var cc = 1024 >> 10;
            var dd = 2050 >> 10;
            var ee = Math.Pow(2, 10);
            var ww = Math.Log(9, 3);
        }
        private string BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return $"{(Math.Sign(byteCount) * num).ToString(CultureInfo.InvariantCulture)} {suf[place]}";
        }
        public string Byts(long Length)
        {
            var size = Length switch


            {
                >= 1 << 30 => $"{Length >> 30}Gb",
                >= 1 << 20 => $"{Length >> 20}Mb",
                >= 1 << 10 => $"{Length >> 10}Kb",
                _ => Length.ToString()
            };
            return size;
        }


        private async void ftp()
        {


            var aaa = Path.GetTempFileName();
            var bb = Path.GetTempPath();
          await  Task.Run(async() =>
            {
                //BackgroundWorker 
                var s = Path.GetTempPath();
                var aa = Path.GetTempFileName();
                Progress<FtpProgress> p = new Progress<FtpProgress>();
                Action<FtpProgress> pp = ee => pp1(ee);
                //pp += ee => pp1(ee);
                p.ProgressChanged += P_ProgressChanged;
                FtpStatus status = FtpStatus.Failed;

                var pat = Path.GetTempFileName();
              
                 var local = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), "upgradeFiles"), "Hjmos_Client.zip");
                //var local = Path.Combine(Directory.GetCurrentDirectory(), "Hjmos_Client.zip");
                var remate = Path.Combine("/upload/", "Hjmos_Client1.zip");
                using (FtpClient ftp = new FtpClient("10.51.9.142", "newftpuser", "123456"))
                {
                    
                  //ftp.DownloadDataType = FtpDataType.Binary;
                  // ftp.EncryptionMode = FtpEncryptionMode.Implicit;
                  //ftp.Encoding = Encoding.UTF8;

                  //ftp.DownloadDataType = FtpDataType.Binary;

                  ftp.RetryAttempts = 5;


                  await ftp.ConnectAsync();
                    var size = await ftp.GetFileSizeAsync(remate);
                    //var status1 = ftp.DownloadDirectory(Path.Combine(Directory.GetCurrentDirectory(), "upgradeFiles"), @"/upload/Hjmos_Client", FtpFolderSyncMode.Update,progress:pp);
                    status =await ftp.DownloadFileAsync(local, remate, FtpLocalExists.Overwrite,FtpVerify.Retry,progress:p);
                    

                    //FileStream fs = new FileStream(local, FileMode.Create, FileAccess.ReadWrite);
                    //var aaa = await ftp.DownloadAsync(fs, remate);
                    
                }

                //if (status == FtpStatus.Success)
                //{
                //    Progress<Info> progress = new Progress<Info>(p => text.Text = p.content);
                //    progress.ProgressChanged += Progress_ProgressChanged;

                //  await  Task.Run(() =>
                //    {


                //        {
                //            try
                //            {
                //                var archive = ZipFile.OpenRead(local);
                //                var entries = archive.Entries;

                //                for (var index = 0; index < entries.Count; index++)
                //                {
                //                    var entry = entries[index];
                //                    string filePaht = Path.Combine(Directory.GetCurrentDirectory(), entry.FullName);
                //                    if (!entry.IsDirectory())
                //                    {
                //                        var parentDirectory = Path.GetDirectoryName(filePaht);
                //                        if (!Directory.Exists(parentDirectory))
                //                        {
                //                            Directory.CreateDirectory(parentDirectory);
                //                        }
                //                        entry.ExtractToFile(filePaht, true);
                //                    }
                //                    int num = (index + 1) * 100 / entries.Count;
                //                    ((IProgress<Info>)progress).Report(new Info(num, entry.FullName));

                //                }
                //            }
                //            catch (IOException ex)
                //            {

                //            }
                //        }
                //    });

                //}
            });
           



        }

        private void pp1(FtpProgress e)
        {
            this.Dispatcher.Invoke(() =>
            {
                text1.Text = e.TransferSpeedToString();
                text.Text = e.Progress.ToString();
            });
        }

        private void Progress_ProgressChanged(object? sender, Info e)
        {
            this.Dispatcher.Invoke(() =>
            {
                text.Text = e.num.ToString();
                text1.Text = e.content;
            });
        }

        private async void UpLoadFile()
        {
            try
            {
                using FtpClient ftpClient = new FtpClient("10.51.9.142", "newftpuser", "123456");
                ftpClient.Encoding = Encoding.UTF8;
                ftpClient.RetryAttempts = 5;
                await ftpClient.ConnectAsync();
                var remate = Path.Combine("/upload/", "IOMS3DViewer.zip");
                using FileStream fs = new FileStream(@"D:\A佳都工作资料\在线项目\HJMosClient\Client\CS6\1-2-IOMS3DViewer\IOMS3DViewer.zip", FileMode.Open);
                var status = await ftpClient.UploadAsync(fs, remate);

            }
            catch (Exception ex)
            {

            }
        }

        private void P_ProgressChanged(object? sender, FtpProgress e)
        {
            this.Dispatcher.Invoke(() =>
            {
                text1.Text = e.TransferSpeedToString();
                text.Text = e.Progress.ToString();
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ftp();
        }
    }
    public static class ExtensionMethod
    {
        public static bool IsDirectory(this ZipArchiveEntry entry)
        {
            return string.IsNullOrEmpty(entry.Name) && (entry.FullName.EndsWith("/") || entry.FullName.EndsWith(@"\"));
        }
    }
    public record Info(int num,string content);
   
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Handlers;
using System.Text;
using System.Threading.Tasks;

namespace UpdateTest
{
    public class HjmosWebClient : HttpClient
    {
        //ProgressMessageHandler messageHandler;
        //HttpClient httpClient;
        //public HjmosWebClient()
        //{
        //    HttpClientHandler httpClientHandler = new HttpClientHandler();
        //    messageHandler = new ProgressMessageHandler(httpClientHandler);
            
        //    httpClient = new HttpClient(messageHandler);
        //    messageHandler.HttpReceiveProgress += MessageHandler_HttpReceiveProgress;
        //    DownLoad(@"ftp://10.51.9.142/upload/Hjmos_Client.zip", AppDomain.CurrentDomain.BaseDirectory);
        //}


        //private void DownLoad(string uri,string localFileName)
        //{
        //    var server = new Uri(uri);
        //    var p = Path.GetDirectoryName(localFileName);
        //    var responseMessage = httpClient.GetAsync(server).Result;
        //    if(responseMessage.IsSuccessStatusCode)
        //    {
        //        using(var fs=File.Create(localFileName))
        //        {
        //            var streamFromService = responseMessage.Content.ReadAsStreamAsync().Result;
        //            streamFromService.CopyTo(fs);
        //        }
        //    }
        //}


        //private void MessageHandler_HttpReceiveProgress(object? sender, HttpProgressEventArgs e)
        //{
        //    var aa = e.ProgressPercentage;
        //}
    }
}

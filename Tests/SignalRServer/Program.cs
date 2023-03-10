

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SignalR;

namespace SignalRServer
{
    internal class Program
    {
        static string serverUrl = "http://localhost:51180";
        static void Main(string[] args)
        {
            StartServer();
            Console.ReadLine();
                
        }

        static async void StartServer()
        {
            try
            {
                //var builder = WebApplication.CreateBuilder();

                //builder.Services.AddSignalR();
                //var app = builder.Build();
                //app.UseRouting();
                //app.UseEndpoints(endpoints => endpoints.MapHub<MyHub>("/MyHub"));
           

                var _host = Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder => webBuilder.UseUrls(serverUrl)
                  .ConfigureServices(services => services.AddSignalR())
                  .Configure(app =>
                  {
                      app.UseRouting();
                      app.UseEndpoints(endpoints => endpoints.MapHub<MyHub>("/MyHub"));
                      app.UseEndpoints(endpoints => endpoints.MapHub<MyHub>("/MyHub1"));

                  }))
                      .Build();
                var context = _host.Services.GetService(typeof(IHubContext<MyHub>));
            
                await _host.StartAsync();
                Console.WriteLine("启动成功！");

               
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }

   internal class ABC
    {
        public ABC(IHubContext<MyHub> context)
        {

        }
    }
}
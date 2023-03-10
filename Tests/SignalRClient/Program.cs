


using Microsoft.AspNet.SignalR.Client;

namespace SignalRClient
{
    internal class Program
    {
        const string ServerURI = "http://localhost:51180/";
        static HubConnection connection;
        static void Main(string[] args)
        {
            ConnectServer();
            Console.ReadLine();
        }

        static async void ConnectServer()
        {
            connection = new HubConnection(ServerURI);
            var hubproxy = connection.CreateHubProxy("MyHub");
            hubproxy.On<string>("FormServerMessage", msg =>
            {
                Console.WriteLine(msg);
            });
            
            await connection.Start();

                // connection = new HubConnectionBuilder().WithUrl(ServerURI).WithAutomaticReconnect().Build();


                // connection.Closed += async (error) =>
                // {
                //     await connection.StartAsync();
                // };
                // connection.On<string>("FormServerMessage", msg =>
                // {
                //     Console.WriteLine(msg);
                // });
                //await connection.StartAsync();
                //await connection.InvokeAsync("SendAsync", "aaa");
            }
    }
}
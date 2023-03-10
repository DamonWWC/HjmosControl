using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRServer
{
    public class MyHub:Hub
    {
        public override Task OnConnectedAsync()
        {
            var a = Context.ConnectionId;
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendAsync(string name)
        {
          
            var c = Context.ConnectionId.AsEnumerable();
            await Clients.All.SendAsync("FormServerMessage", "1234");
        }
    }
}

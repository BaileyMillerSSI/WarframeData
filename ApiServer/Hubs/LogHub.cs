using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServer.Hubs
{
    public class LogHub : Hub
    {
        public async Task LogEventBroadcast(string message)
        {
            await Clients.Others.SendAsync("LogEvent", message);
        }

        public async Task ItemDataLoaded(object data)
        {
            await Clients.Others.SendAsync("ItemLoaded", data);
        }
    }
}

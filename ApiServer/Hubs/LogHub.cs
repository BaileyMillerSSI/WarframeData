using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warframe.Data;

namespace ApiServer.Hubs
{
    public class LogHub : Hub
    {
        public async Task LogEventBroadcast(string message)
        {
            await Clients.Others.SendAsync("LogEvent", message);
        }

        public async Task ItemDataLoaded(WeaponData data)
        {
            await Clients.Others.SendAsync("ItemLoaded", data);
        }
    }
}

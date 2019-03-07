using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper
{
    public static class LoggingService
    {

        private static HubConnection connection;

        static LoggingService()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("http://apiserver:59930/LogHub")
                .Build();
        }


        public static Dictionary<DateTime, string> Log
        {
            get
            {
                return conCurLog.ToDictionary(entry => entry.Key, entry => entry.Value);
            }
        }

        private static ConcurrentDictionary<DateTime, string> conCurLog = new ConcurrentDictionary<DateTime, string>();

        /// <summary>
        /// Adds a new event to the event stream being posted to the server
        /// </summary>
        /// <param name="message">The message to be logged</param>
        public static async Task LogEventAsync(string message)
        {
            if (connection.State == HubConnectionState.Connected)
            {
                await connection.InvokeAsync("LogEventBroadcast", message);
            }
            //else
            //{
            //    await connection.StartAsync();
            //    await connection.InvokeAsync("LogEventBroadcast", message);
            //}
            conCurLog.TryAdd(DateTime.Now, message);
        }


        /// <summary>
        /// Will setup a socket connection to the server to broadcast events inside the execution of the code
        /// </summary>
        public static async Task PrepareLoggingService()
        {
            try
            {
                using (var webby = new HttpClient())
                {
                    var data = await webby.GetAsync("https://apiserver:44334/css/site.css");
                }

                await Task.Delay(TimeSpan.FromSeconds(5));
                await connection.StartAsync();
            }
            catch (Exception error)
            {
                
            }
            finally
            {
                await LogEventAsync("Service is ready");
            }
        }
    }
}

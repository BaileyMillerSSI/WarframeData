using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper
{
    public static class LoggingService
    {
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
        public static void LogEvent(string message)
        {
            conCurLog.TryAdd(DateTime.Now, message);
        }


        /// <summary>
        /// Will setup a socket connection to the server to broadcast events inside the execution of the code
        /// </summary>
        public static void PrepareLoggingService()
        {
            Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            LogEvent("Service is ready");
        }
    }
}

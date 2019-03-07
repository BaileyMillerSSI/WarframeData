using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServer.Helpers
{
    //public static class DockerSupport
    //{
    //    static Process LongRunningEventListener;

    //    static DockerSupport()
    //    {
    //        // Listen for start events on the webscraper image
    //        var processInfo = new ProcessStartInfo("docker", $"events --filter 'image=webscraper' --format '{{json .}}'");
    //        processInfo.CreateNoWindow = true;
    //        processInfo.UseShellExecute = false;
    //        processInfo.RedirectStandardOutput = true;
    //        processInfo.RedirectStandardError = true;

    //        LongRunningEventListener = new Process() { StartInfo = processInfo };
    //        LongRunningEventListener.OutputDataReceived += EventOutputData;

    //        LongRunningEventListener.Start();
    //        LongRunningEventListener.BeginOutputReadLine();
    //    }
        

    //    private static void EventOutputData(object sender, DataReceivedEventArgs e)
    //    {
    //        Debug.WriteLine(e.Data);
    //    }

    //    public static Process StartWebScraper()
    //    {
    //        const string helperPath = @"D:\Source\WarframeData\WebScraper";

    //        var processInfo = new ProcessStartInfo("docker", $"run -d --rm webscraper");

    //        processInfo.CreateNoWindow = true;
    //        processInfo.UseShellExecute = false;
    //        processInfo.RedirectStandardOutput = true;
    //        processInfo.RedirectStandardError = true;

    //        var scrapperProc = new Process() {
    //            StartInfo = processInfo
    //        };

    //        scrapperProc.Start();

    //        return scrapperProc;
    //    }
    //}
}

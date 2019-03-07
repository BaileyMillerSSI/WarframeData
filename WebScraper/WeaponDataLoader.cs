using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Warframe.Data;

namespace WebScraper
{
    public static class WeaponDataLoader
    {

        private static HttpClient Web = new HttpClient();

        public static async Task LoadDataAsync(this WeaponData Wep)
        {
            // Load Page
            var rawHtmlDoc = await Web.GetStringAsync(Wep.WikiLink);

            var webDoc = new HtmlDocument();
            webDoc.LoadHtml(rawHtmlDoc);



            var webDocNodes = webDoc.DocumentNode;

            var CodexEntryStart = webDocNodes.Descendants("div")
                .Where(div => div.HasClass("codexflower"))
                .SelectMany(div => div.Descendants("#text"))
                .First();

            Wep.CodexEntry = CodexEntryStart.InnerHtml;

            var creds = webDocNodes.Descendants("p")
                .Where(hasTextSpanChild =>
                    hasTextSpanChild.Line > CodexEntryStart.Line &&
                    hasTextSpanChild.Descendants("#text").Count() != 0 &&
                    hasTextSpanChild.InnerHtml.Contains("This weapon can be sold for"))
                    .Select(childNodes => new
                    {
                        rootText = childNodes.FirstChild.InnerHtml,
                        creditCount = childNodes.Descendants("b").First().InnerHtml.Replace(",", "")
                    }).FirstOrDefault();


            if (creds != null)
            {
                Wep.CreditSellPrice = int.Parse(creds.creditCount);
            }

            // Finish Up
            await LoggingService.LogEventAsync($"Finished Loading {Wep.Title} into local cache");
            // Upload to Api Service!
            await Web.PostAsync($"http://apiserver/api/WeaponData", new StringContent(JsonConvert.SerializeObject(Wep),Encoding.UTF8, "application/json"));
        }
    }
}

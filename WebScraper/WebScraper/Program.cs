using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebScraper
{
    class Program
    {
        const string WikiLink = "https://warframe.fandom.com/wiki/Category:Primary_Weapons";
        const string RootWikiLink = "https://warframe.fandom.com";

        static HttpClient _Web = new HttpClient();

        static int Main(string[] args)
        {
            Console.WriteLine("Preparing to download Primary Weapon Data from Warframe Wiki");

            LoggingService.PrepareLoggingService().Wait();

            var data = GetWeaponListAsync().Result;
            
            Console.WriteLine("\n\n");

            foreach (var wep in data)
            {
                Console.WriteLine($"{wep.Title} - '{wep.CodexEntry}'");
                Console.WriteLine($"Credits: {wep.CreditSellPrice}");
            }

            LoggingService.LogEventAsync("Ready to exit").Wait();
            return 0;
        }


        static async Task<List<WeaponData>> GetWeaponListAsync()
        {
            var rawHtmlData = await _Web.GetStringAsync(WikiLink).ConfigureAwait(false);

            var webDoc = new HtmlDocument();
            webDoc.LoadHtml(rawHtmlData);

            // With LINQ 
            var nodes = webDoc.DocumentNode.Descendants("li")
             .Where(x => x.HasClass("category-page__member"))
             .Select(x => x.Descendants())
             .Select(x => new WeaponData(x.Where(inner => inner.Name == "a" && inner.HasClass("category-page__member-link")).Select(innerVal => innerVal.Attributes["title"].Value).FirstOrDefault(), x.Where(inner => inner.Name == "a" && inner.HasClass("category-page__member-link")).Select(innerVal => string.Concat(RootWikiLink, innerVal.Attributes["href"].Value)).FirstOrDefault())
             {
                 ThumbnailImage = x.Where(inner=> inner.Name == "div" && inner.HasClass("category-page__member-left")).SelectMany(divDesc=> divDesc.Descendants()).Where(innerDiv=>innerDiv.Name == "img").Where(imgSrc=>imgSrc.Attributes["src"].Value.Contains("http")).Select(img=> img.Attributes["src"].Value).FirstOrDefault()
             })
             .Where(x=>!x.Title.Contains("Conclave") && !x.Title.Contains("File:") && !x.Title.Contains("Category:") && !x.Title.Contains(" (Weapon)"))
             .ToList();

            //await nodes.FirstOrDefault().LoadDataAsync(_Web);

            nodes.AsParallel().ForAll((wep)=> 
            {
                try
                {
                    LoggingService.LogEventAsync($"Loading {wep.Title} into local cache").Wait();
                    wep.LoadDataAsync(_Web).Wait();
                }
                catch (Exception)
                {

                }
            });

            return nodes;
        }
    }
}

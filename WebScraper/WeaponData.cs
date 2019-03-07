using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebScraper
{
    public class WeaponData
    {
        public string Title { get; private set; }
        public string WikiLink { get; private set; }
        public string ThumbnailImage { get; set; }

        public WeaponData(string title, string wikiLink)
        {
            Title = title;
            WikiLink = wikiLink;
        }


        public string CodexEntry { get; private set; }
        public string WikiEntry { get; private set; }

        public int CreditSellPrice { get; private set; }
        public string LargerImage { get; private set; }

        // Statistics
        public int MasteryRank { get; private set; }
        public object SlotType { get; private set; }
        public object WeaponType { get; private set; }
        public string TriggerType { get; private set; }

        // Utility
        public object AmmoType { get; private set; }
        public int RangeLimit { get; private set; }
        public object NoiseLevel { get; private set; }
        public double FireRatePerSecond { get; private set; }
        public double Accuracy { get; private set; }
        public double MagazineSize { get; private set; }
        public double MaxAmmo { get; private set; }
        public double ReloadTime { get; private set; }

        // Normal Attacks
        public double TotalDamage { get; private set; }
        public double CritChance { get; private set; }
        public double CritMultiplier { get; private set; }
        public double StatusChance { get; private set; }
        public double AmmoCost { get; private set; }

        // Miscellaneous
        public string WeaponUsers { get; private set; }
        public string Introduced { get; private set; }

        // Skins
        public List<object> Skins { get; private set; } = new List<object>();





        public async Task LoadDataAsync(HttpClient Web)
        {
            // Load Page
            var rawHtmlDoc = await Web.GetStringAsync(WikiLink);

            var webDoc = new HtmlDocument();
            webDoc.LoadHtml(rawHtmlDoc);



            var webDocNodes = webDoc.DocumentNode;

            var CodexEntryStart = webDocNodes.Descendants("div")
                .Where(div => div.HasClass("codexflower"))
                .SelectMany(div => div.Descendants("#text"))
                .First();

            CodexEntry = CodexEntryStart.InnerHtml;

            var creds = webDocNodes.Descendants("p")
                .Where(hasTextSpanChild =>
                    hasTextSpanChild.Line > CodexEntryStart.Line &&
                    hasTextSpanChild.Descendants("#text").Count() != 0 && 
                    hasTextSpanChild.InnerHtml.Contains("This weapon can be sold for"))
                    .Select(childNodes=> new
                    {
                        rootText = childNodes.FirstChild.InnerHtml,
                        creditCount = childNodes.Descendants("b").First().InnerHtml.Replace(",", "")
                    }).FirstOrDefault();


            if (creds != null)
            {
                CreditSellPrice = int.Parse(creds.creditCount);
            }


            // Finish Up
            await LoggingService.LogItemLoadedAsync(this, $"Finished Loading {Title} into local cache");
        }
    }
}
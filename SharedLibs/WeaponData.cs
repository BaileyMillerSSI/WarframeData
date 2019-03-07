using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Warframe.Data
{
    public class WeaponData
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string WikiLink { get; set; }
        public string ThumbnailImage { get; set; }

        public WeaponData(string title, string wikiLink)
        {
            Title = title;
            WikiLink = wikiLink;
        }


        public string CodexEntry { get; set; }
        public string WikiEntry { get; set; }

        public int CreditSellPrice { get; set; }
        public string LargerImage { get; set; }

        // Statistics
        public int MasteryRank { get; set; }
        public string SlotType { get; set; }
        public string WeaponType { get; set; }
        public string TriggerType { get; set; }

        // Utility
        public string AmmoType { get; set; }
        public int RangeLimit { get; set; }
        public string NoiseLevel { get; set; }
        public double FireRatePerSecond { get; set; }
        public double Accuracy { get; set; }
        public double MagazineSize { get; set; }
        public double MaxAmmo { get; set; }
        public double ReloadTime { get; set; }

        // Normal Attacks
        public double TotalDamage { get; set; }
        public double CritChance { get; set; }
        public double CritMultiplier { get; set; }
        public double StatusChance { get; set; }
        public double AmmoCost { get; set; }

        // Miscellaneous
        public string WeaponUsers { get; set; }
        public string Introduced { get; set; }

        // Skins
        //public List<string> Skins { get; set; } = new List<string>();
        
        
    }
}
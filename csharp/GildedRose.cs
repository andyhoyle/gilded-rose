using System;
using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        public const string AGED_BRIE = "Aged Brie";
        public const string BACKSTAGE_PASS = "Backstage passes to a TAFKAL80ETC concert";
        public const string SULFURAS = "Sulfuras, Hand of Ragnaros";
        public const string CONJURED = "Conjured Mana Cake";

        IList<Item> Items;

        // To allow for local unit tests
        internal GildedRose() 
        { 
            Items = new List<Item>();
        }

        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                item.Quality = item.Name switch
                {
                    AGED_BRIE => BrieQuality(item),
                    BACKSTAGE_PASS => BackstagePassQuality(item),
                    SULFURAS => 80,
                    CONJURED => ConjuredQuality(item),
                    _ => StandardItemQuality(item)
                };

                if (item.Name != SULFURAS) item.SellIn--;
            }
        }

        internal int BrieQuality(Item item)
        {
            if (item.Name != AGED_BRIE) throw new ArgumentException($"Cannot use this quality calculation with anything other than {AGED_BRIE}");

            int quality = item.Quality;

            if (quality < 50)
            {
                quality++;
                if (item.SellIn <= 0 && quality < 50) quality++;
                if (quality > 50) quality = 50;
            }

            return quality;
        }

        internal int BackstagePassQuality(Item item)
        {
            if (item.Name != BACKSTAGE_PASS) throw new ArgumentException($"Cannot use this quality calculation with anything other than {BACKSTAGE_PASS}");

            int quality = item.Quality;

            if (quality < 50)
            {
                quality++;
                if (item.SellIn <= 10 && quality < 50) quality++;
                if (item.SellIn <= 5 && quality < 50) quality++;
            }

            if (item.SellIn <= 0)
            {
                quality = 0;
            }

            return quality;
        }

        internal int StandardItemQuality(Item item)
        {
            int quality = item.Quality;

            if (quality > 0)
            {
                quality--;
                if (item.SellIn <= 0 && quality > 0) quality--;
            }

            return quality;
        }

        internal int ConjuredQuality(Item item)
        {
            if (item.Name != CONJURED) throw new ArgumentException($"Cannot use this quality calculation with anything other than {CONJURED}");

            int quality = item.Quality;

            if (quality > 0)
            {
                quality-=2;
                if (item.SellIn <= 0 && quality > 0) quality-=2;
                if(quality < 0) quality = 0;

            }

            return quality;
        }
    }
}

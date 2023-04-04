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

        private int BrieQuality(Item item)
        {
            int quality = item.Quality;

            if (quality < 50)
            {
                quality++;
            }

            return quality;
        }

        private int BackstagePassQuality(Item item)
        {
            int quality = item.Quality;

            if (quality < 50)
            {
                quality++;
                if (item.SellIn <= 10 && item.Quality < 50) quality++;
                if (item.SellIn <= 5 && item.Quality < 50) quality++;
            }

            if (item.SellIn <= 0)
            {
                quality = 0;
            }

            return quality;
        }

        private int StandardItemQuality(Item item)
        {
            int quality = item.Quality;

            if (quality > 0)
            {
                quality--;
                if (item.SellIn <= 0 && quality > 0) quality--;
            }

            return quality;
        }

        private int ConjuredQuality(Item item)
        {
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

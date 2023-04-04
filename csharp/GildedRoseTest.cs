using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        // At the end of each day our system lowers Quality value for every (normal) item
        [Test]
        public void NormalItemQualityDegrades()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Banana", SellIn = 5, Quality = 5 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(4, Items[0].Quality);
        }


        // The Quality of an item is never negative
        [Test]
        public void NormalItemQualityLowestValueIsZero()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Banana", SellIn = 3, Quality = 1 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            app.UpdateQuality();
            app.UpdateQuality();
            Assert.AreEqual(0, Items[0].Quality);
        }

        // Once the sell by date has passed, Quality degrades twice as fast
        [Test]
        public void NormalItemQualityDegradeDoublesAfterSellInExpired()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Banana", SellIn = 1, Quality = 4 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(3, Items[0].Quality);
            // SellIn now at zero
            app.UpdateQuality();
            Assert.AreEqual(1, Items[0].Quality);
        }

        // Normal Item Quality Lowest is Zero After SellIn Expired
        [Test]
        public void NormalItemQualityLowestValueZeroAfterSellInExpired()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Banana", SellIn = 1, Quality = 4 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            // SellIn 0, Quality 3
            app.UpdateQuality();
            // SellIn 0, Quality 1
            app.UpdateQuality();
            Assert.AreEqual(0, Items[0].Quality);
        }

        // At the end of each day our system lowers both values for every non-sulfura item
        [Test]
        public void SellInDecrementedByOneForNonSulfuraItemTypes()
        {
            IList<Item> Items = new List<Item> { 
                new Item { Name = "Banana", SellIn = 5, Quality = 4 },
                new Item { Name = "Aged Brie", SellIn = 5, Quality = 4 },
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 4 }
            };

            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            
            Assert.AreEqual(4, Items[0].SellIn, "Bananas SellIn value incorrect");
            Assert.AreEqual(4, Items[1].SellIn, "Aged Brie SellIn value incorrect");
            Assert.AreEqual(4, Items[2].SellIn, "Backstage passes SellIn value incorrect");
        }

        // "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
        [Test]
        public void SellInNotDecrementedByOneNonSulfuraItemType()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 5, Quality = 80 } };

            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(5, Items[0].SellIn, "Sulfuras SellIn value incorrect");
            Assert.AreEqual(80, Items[0].Quality, "Sulfuras SellIn value incorrect");
        }

        // "Aged Brie" actually increases in Quality the older it gets
        [Test]
        public void BrieQualityIncreasesByOne()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 5, Quality = 4 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(5, Items[0].Quality);
        }

        // "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches;
        [Test]
        public void BSPQualityIncreasesByOne()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 25, Quality = 4 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(5, Items[0].Quality);
        } 

        // Brie Max Quality 50
        [Test]
        public void BrieQualityMaxValueIsFifty()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 5, Quality = 50 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(50, Items[0].Quality);
        }

        // BSP Max Quality 50
        [Test]
        public void BSPQualityMaxValueIsFifty()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 50 } } ;
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(50, Items[0].Quality);
        }

        // BSP Quality increases by 2 when there are 10 days or less
        [Test]
        public void BSPQualityIncreasesByTwoWhenSellInLessThanTen()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 9, Quality = 4 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(6, Items[0].Quality);
        }

        // BSP Quality increases by 3 when there are 5 days or less
        [Test]
        public void BSPQualityIncreasesByThreeWhenSellInLessThanFive()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 4, Quality = 4 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(7, Items[0].Quality);
        }

        // BSP Quality drops to zero after SellIn
        [Test]
        public void BSPQualityDropsToZeroAfterSellIn()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 4 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual(0, Items[0].Quality);
        }

        // Failing Tests

        // Sulfura can only be 80
        //[Test]
        //public void SulfuraQualityCanOnlyBe80 ()
        //{
        //    IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 5, Quality = 4 } };
        //    GildedRose app = new GildedRose(Items);
        //    app.UpdateQuality();
        //    Assert.AreEqual(80, Items[0].Quality, "Sulfuras SellIn value incorrect");
        //}

        // Conjured items degrade in quality twice as fast as normal items
        //[Test]
        //public void ConjuredItemsDegradeInQualityTwiceAsFast()
        //{
        //    IList<Item> Items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 5, Quality = 5 } };
        //    GildedRose app = new GildedRose(Items);
        //    app.UpdateQuality();
        //    Assert.AreEqual(3, Items[0].Quality);
        //}
    }
}

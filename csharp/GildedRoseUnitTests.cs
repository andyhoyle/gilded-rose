using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseUnitTests
    {
        private GildedRose _app;

        [SetUp]
        public void Setup()
        {
            _app = new GildedRose();
        }

        // At the end of each day our system lowers Quality value for every (normal) item
        [Test]
        public void NormalItemQualityDegrades()
        {
            var quality = _app.StandardItemQuality(new Item { Name = "Banana", SellIn = 5, Quality = 5 } );
            Assert.AreEqual(4, quality);
        }

        // The Quality of an item is never negative
        [Test]
        public void NormalItemQualityLowestValueIsZero()
        {
            var quality = _app.StandardItemQuality(new Item { Name = "Banana", SellIn = 3, Quality = 0 } );
            Assert.AreEqual(0, quality);
        }

        // Once the sell by date has passed, Quality degrades twice as fast
        [Test]
        public void NormalItemQualityDegradeDoublesAfterSellInExpired()
        {
            var quality = _app.StandardItemQuality(new Item { Name = "Banana", SellIn = 0, Quality = 4 } );
            Assert.AreEqual(2, quality);
        }

        // Normal Item Quality Lowest is Zero After SellIn Expired
        [Test]
        public void NormalItemQualityLowestValueZeroAfterSellInExpired()
        {
            var quality = _app.StandardItemQuality(new Item { Name = "Banana", SellIn = 0, Quality = 0 } );
            Assert.AreEqual(0, quality);
        }

        // "Aged Brie" actually increases in Quality the older it gets
        [Test]
        public void BrieQualityIncreasesByOne()
        {
            var quality = _app.BrieQuality(new Item { Name = GildedRose.AGED_BRIE, SellIn = 5, Quality = 4 } );
            Assert.AreEqual(5, quality);
        }

        // "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches;
        [Test]
        public void BSPQualityIncreasesByOne()
        {
            var quality = _app.BackstagePassQuality(new Item { Name = GildedRose.BACKSTAGE_PASS, SellIn = 25, Quality = 4 } );
            Assert.AreEqual(5, quality);
        } 

        // Brie Max Quality 50
        [Test]
        public void BrieQualityMaxValueIsFifty()
        {
            var quality = _app.BrieQuality(new Item { Name = GildedRose.AGED_BRIE, SellIn = 5, Quality = 50 } );
            Assert.AreEqual(50, quality);
        }

        // BSP Max Quality 50
        [Test]
        public void BSPQualityMaxValueIsFifty()
        {
            var quality = _app.BackstagePassQuality(new Item { Name = GildedRose.BACKSTAGE_PASS, SellIn = 5, Quality = 50 } );
            Assert.AreEqual(50, quality);
        }

        // BSP Quality increases by 2 when there are 10 days or less
        [Test]
        [TestCase(10)]
        [TestCase(9)]
        [TestCase(8)]
        [TestCase(7)]
        [TestCase(6)]
        public void BSPQualityIncreasesByTwoWhenSellInLessThanTen(int sellIn)
        {
            var quality = _app.BackstagePassQuality(new Item { Name = GildedRose.BACKSTAGE_PASS, SellIn = sellIn, Quality = 4 });
            Assert.AreEqual(6, quality);
        }

        // BSP Quality increases by 3 when there are 5 days or less
        [Test]
        [TestCase(5)]
        [TestCase(4)]
        [TestCase(3)]
        [TestCase(2)]
        [TestCase(1)]
        public void BSPQualityIncreasesByThreeWhenSellInLessThanFive(int sellIn)
        {
            var quality = _app.BackstagePassQuality(new Item { Name = GildedRose.BACKSTAGE_PASS, SellIn = sellIn, Quality = 4 });
            Assert.AreEqual(7, quality);
        }

        // BSP Quality drops to zero after SellIn
        [Test]
        public void BSPQualityDropsToZeroAfterSellIn()
        {
            var quality = _app.BackstagePassQuality(new Item { Name = GildedRose.BACKSTAGE_PASS, SellIn = 0, Quality = 4 });
            Assert.AreEqual(0, quality);
        }

        // Conjured items degrade in quality twice as fast as normal items
        [Test]
        public void ConjuredItemsDegradeInQualityTwiceAsFast()
        {
            var quality = _app.ConjuredQuality(new Item { Name = GildedRose.CONJURED, SellIn = 5, Quality = 5 });
            Assert.AreEqual(3, quality);
        }

        // Calling Aged Brie Quality calculation with other items causes exception
        [Test]
        [TestCase(GildedRose.SULFURAS, 0, 80)]
        [TestCase(GildedRose.BACKSTAGE_PASS, 0, 0)]
        [TestCase(GildedRose.CONJURED, 0, 0)]
        [TestCase("Bananas", 0, 0)]
        public void AgedBrieQualityCalulationThrows(string item, int sellIn, int quality)
        {
            Assert.Throws<ArgumentException>(() => _app.BrieQuality(new Item { Name = item, SellIn = sellIn, Quality = quality }));
        }

        // Calling Conjured Quality calculation with other items causes exception
        [Test]
        [TestCase(GildedRose.SULFURAS, 0, 80)]
        [TestCase(GildedRose.BACKSTAGE_PASS, 0, 0)]
        [TestCase(GildedRose.AGED_BRIE, 0, 0)]
        [TestCase("Bananas", 0, 0)]
        public void ConjuredQualityCalulationThrows(string item, int sellIn, int quality)
        {
            Assert.Throws<ArgumentException>(() => _app.ConjuredQuality(new Item { Name = item, SellIn = sellIn, Quality = quality }));
        }

        // Calling Backstage Quality calculation with other items causes exception
        [Test]
        [TestCase(GildedRose.SULFURAS, 0, 80)]
        [TestCase(GildedRose.CONJURED, 0, 0)]
        [TestCase(GildedRose.AGED_BRIE, 0, 0)]
        [TestCase("Bananas", 0, 0)]
        public void BackstageQualityCalulationThrows(string item, int sellIn, int quality)
        {
            Assert.Throws<ArgumentException>(() => _app.BackstagePassQuality(new Item { Name = item, SellIn = sellIn, Quality = quality }));
        }
    }
}

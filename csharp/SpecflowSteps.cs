using System.Collections.Generic;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace csharp
{
    [Binding]
    public class SPecflowSteps
    {
        private List<Item> _items;
        private GildedRose _app;

        [Given(@"a list of items in stock:")]
        public void GivenAListOfItems(Table table)
        {
            _items = new List<Item>();
            
            foreach (var row in table.Rows)
            {
                var item = new Item { Name = row["Name"], SellIn = int.Parse(row["SellIn"]), Quality = int.Parse(row["Quality"]) };
                _items.Add(item);
            }

            _app = new GildedRose(_items);
        }

        [When(@"a single day has elapsed")]
        public void WhenASingleDayHasElapsed()
        {
            _app.UpdateQuality();
        }

        [When(@"(.*) days have elapsed")]
        public void WhenAMultipleDaysHaveElapsed(int days)
        {
            for (int i = 0; i < days; i++)
            {
                _app.UpdateQuality();
            }
        }

        [Then(@"the quality of the item ""(.*)"" should be (.*)")]
        public void ThenTheQualityOfTheItemShouldBe(string itemName, int expectedQuality)
        {
            var item = _items.Find(i => i.Name == itemName);
            Assert.AreEqual(expectedQuality, item.Quality);
        }

        [Then(@"the sell in of the item ""(.*)"" should be (.*)")]
        public void ThenTheSellInOfTheItemShouldBe(string itemName, int expectedSellIn)
        {
            var item = _items.Find(i => i.Name == itemName);
            Assert.AreEqual(expectedSellIn, item.SellIn);
        }
    }
}

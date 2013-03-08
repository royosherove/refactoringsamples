using System.Collections.Generic;
using System.Linq;
using GildedRose.Console;
using NUnit.Framework;

namespace GildedRose.Tests
{
    [TestFixture]
    public class GildedRoseAdjusterUpdateShould
    {
        private const int DefaultQuality = 10;
        private const int DefaultSellIn = 5;
        private List<Item> GetSword()
        {
            return new List<Item>()
                {new Item(){Name="Sword", 
                    Quality = DefaultQuality,
                    SellIn=DefaultSellIn}};
        }

        private List<Item> GetExpiredSword()
        {
            return new List<Item>()
                {new Item(){Name="Expired Sword", 
                    Quality = DefaultQuality,
                    SellIn=0}};
        }

        private List<Item> GetSulfurus()
        {
            return new List<Item>()
                {new Item(){Name="Sulfuras, Hand of Ragnaros", 
                    Quality = DefaultQuality,
                    SellIn=DefaultSellIn}};
        }

        [Test]
        public void LowerQualityOfBasicItem()
        {
            var items = GetSword();
            var adjuster = new GildedRoseQualityAdjuster(
                items);

            adjuster.UpdateQuality();

            Assert.AreEqual(DefaultQuality-1, items.First().Quality);
            Assert.AreEqual(DefaultSellIn-1, items.First().SellIn);
        }

        [Test]
        public void NotLowerQualityOfBasicItemBelowZero()
        {
            var items = GetSword();
            var adjuster = new GildedRoseQualityAdjuster(
                items);

            for (int i = 0; i < DefaultQuality; i++)
            {
                adjuster.UpdateQuality();
            }
            adjuster.UpdateQuality();

            Assert.AreEqual(0, items.First().Quality);
        }

        [Test]
        public void LowerQualityOfBasicItemByTwoAfterSellInDate()
        {
            var items = GetExpiredSword();
            var adjuster = new GildedRoseQualityAdjuster(
                items);

            adjuster.UpdateQuality();

            Assert.AreEqual(DefaultQuality-2, items.First().Quality);
        }

        [Test]
        public void NotLowerQualityOrSellInOfSulfuras()
        {
            var items = GetSulfurus();
            var adjuster = new GildedRoseQualityAdjuster(items);

            adjuster.UpdateQuality();

            Assert.AreEqual(DefaultQuality, items.First().Quality);
            Assert.AreEqual(DefaultSellIn, items.First().SellIn);
        }
    }
}
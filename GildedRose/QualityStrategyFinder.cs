using System.Collections.Generic;

namespace GildedRose.Console
{
    public class QualityStrategyFinder
    {
        public IQualityStrategy FindStrategy(Item item)
        {
            List<IQualityStrategy> strategies = new List<IQualityStrategy>()
                                                    {
                                                        new AgedBrieStrategy(),
                                                        new BackStagePassesStrategy(),
                                                        new SulfurasStrategy(),
                                                        new DefaultStrategy()
                                                    };
            foreach (var strategy in strategies)
            {
                if (strategy.IsRelevantTo(item))
                {
                    return strategy;
                }
            }
            return new DefaultStrategy();
        }
    }

    public class DefaultStrategy : IQualityStrategy
    {
        public bool IsRelevantTo(Item item)
        {
            return true;
        }

        public void UpdateQuality(Item item)
        {
            item.SellIn--;

            DecreaseQuality(item);
            if (item.SellIn < 0)
            {
                DecreaseQuality(item);
            } 
        }

        private static void DecreaseQuality(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality--;
            }
        }
    }

    public class SulfurasStrategy : IQualityStrategy
    {
        public bool IsRelevantTo(Item item)
        {
            return item.Name.Contains("Sulfuras");
        }

        public void UpdateQuality(Item item)
        {
        }
    }
}
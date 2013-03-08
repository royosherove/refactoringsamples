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
                                                        new BackStagePassesStrategy()
                                                    };
            foreach (var strategy in strategies)
            {
                if (strategy.IsRelevantTo(item))
                {
                    return strategy;
                }
            }
            return null;
        }
    }
}
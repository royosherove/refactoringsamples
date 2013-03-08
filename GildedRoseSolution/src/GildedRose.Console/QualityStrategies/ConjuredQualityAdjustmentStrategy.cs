using System;

namespace GildedRose.Console
{
    class ConjuredQualityAdjustmentStrategy : IQualityAdjustmentStrategy
    {
        public void Update(Item item)
        {
        }

        public bool CanBeUsed(Item item)
        {
            return item.Name.Contains("Conjured");
        }
    }
}
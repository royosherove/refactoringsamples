using System.Collections.Generic;
using System.Linq;

namespace GildedRose.Console
{
    public class QualityAdjustStrategySelector
    {
        private List<IQualityAdjustmentStrategy> _adjustments = 
            new List<IQualityAdjustmentStrategy>
                {
                    new SulfurasQualityAdjustmentStrategy(),
                    new ConjuredQualityAdjustmentStrategy()
                };

        public IQualityAdjustmentStrategy SelectStrategy(Item item)
        {
            IQualityAdjustmentStrategy found = _adjustments.FirstOrDefault(x => x.CanBeUsed(item));
            //if (found!=null)
            //{
            return found;
            //}
            //return new AgedBrieQualityStrategy();
        }
    }
}
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
                    new QualityAdjustmentStrategy(),
                };

        public IQualityAdjustmentStrategy SelectStrategy(Item item)
        {
            return _adjustments.FirstOrDefault(x => x.CanBeUsed(item));
        }
    }
}
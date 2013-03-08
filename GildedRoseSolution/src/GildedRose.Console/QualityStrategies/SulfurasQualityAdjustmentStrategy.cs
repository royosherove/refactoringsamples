namespace GildedRose.Console
{
    public class SulfurasQualityAdjustmentStrategy : IQualityAdjustmentStrategy
    {
        public void Update(Item item)
        {
            // Intentionally blank. We don't need to change Sulfuras.
        }

        public bool CanBeUsed(Item item)
        {
            return item.Name == "Sulfuras, Hand of Ragnaros";
        }
    }
}
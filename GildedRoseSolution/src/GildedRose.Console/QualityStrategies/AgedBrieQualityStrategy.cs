namespace GildedRose.Console
{
    public class AgedBrieQualityStrategy : IQualityAdjustmentStrategy
    {
        public void Update(Item item)
        {
                item.Quality++;
            if (item.SellIn > 0)
            {
                item.SellIn--;
            }
        }

        public bool CanBeUsed(Item item)
        {
            return item.Name.Contains("Brie");
        }
    }
}
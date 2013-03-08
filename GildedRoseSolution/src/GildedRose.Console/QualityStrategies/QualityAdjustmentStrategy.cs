namespace GildedRose.Console
{
    public class QualityAdjustmentStrategy : IQualityAdjustmentStrategy
    {
        public void Update(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality--;
                if (item.SellIn == 0)
                {
                    if (item.Quality > 0)
                    {
                        item.Quality--;
                    }
                }
            }
            if (item.SellIn > 0)
            {
                item.SellIn--;
            }
        }

        public bool CanBeUsed(Item item)
        {
            // TODO: Check that no other ones apply
            return true;
        }
    }
}
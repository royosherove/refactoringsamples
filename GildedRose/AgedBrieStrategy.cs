namespace GildedRose.Console
{
    class AgedBrieStrategy : IQualityStrategy
    {
        public bool IsRelevantTo(Item item)
        {
            return item.Name.Contains("Brie");
        }

        public void UpdateQuality(Item item)
        {
            item.SellIn--;
            IncreaseQualityIfRelevant(item);
            if (item.SellIn<0)
            {
                IncreaseQualityIfRelevant(item);
            }
        }

        private  void IncreaseQualityIfRelevant(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality = item.Quality + 1;
            }
        }
    }
    class BackStagePassesStrategy : IQualityStrategy
    {
        public bool IsRelevantTo(Item item)
        {
            return item.Name.Contains("Backstage");
        }

        public void UpdateQuality(Item item)
        {
            
            IncreaseQualityIfRelevant(item);

            item.SellIn--;
            if (item.SellIn<10)
            {
                IncreaseQualityIfRelevant(item);
            }
            if (item.SellIn<5)
            {
                IncreaseQualityIfRelevant(item);
            }
            if (item.SellIn<0)
            {
                item.Quality = 0;
            }
        }

        private  void IncreaseQualityIfRelevant(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality = item.Quality + 1;
            }
        }
    }
}
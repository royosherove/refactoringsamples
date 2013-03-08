namespace GildedRose.Console
{
    public interface IQualityStrategy
    {
        bool IsRelevantTo(Item item);
        void UpdateQuality(Item item);
    }
}
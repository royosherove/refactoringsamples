namespace GildedRose.Console
{
    public interface IQualityAdjustmentStrategy
    {
        void Update(Item item);
        bool CanBeUsed(Item item);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRose.Console
{
    public interface IQualityAdjustmentStrategy
    {
        void Update(Item item);
        bool CanBeUsed(Item item);
    }

    class ConjuredQualityAdjustmentStrategy : IQualityAdjustmentStrategy
    {
        public void Update(Item item)
        {
            throw new NotImplementedException();
        }

        public bool CanBeUsed(Item item)
        {
            throw new NotImplementedException();
        }
    }

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

    public class GildedRoseQualityAdjuster
    {
        public GildedRoseQualityAdjuster(IList<Item> items)
        {
            Items = items;
        }

        IList<Item> Items;
        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                var selector = new QualityAdjustStrategySelector();
                var strategy = selector.SelectStrategy(Items[i]);

                if (strategy != null)
                {
                    strategy.Update(Items[i]);
                    continue;
                }

                if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    var adjustmentStrategy = new QualityAdjustmentStrategy();
                    adjustmentStrategy.Update(Items[i]);
                    //if (Items[i].Quality > 0)
                    //{
                    // TODO: Write a test for Sulfuras!!!
                    //    if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                    //    {
                    //        Items[i].Quality = Items[i].Quality - 1;
                    //    }
                    //}
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                //if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                //{
                //    Items[i].SellIn = Items[i].SellIn - 1;
                //}

                if (Items[i].SellIn < 0)
                {
                    if (Items[i].Name != "Aged Brie")
                    {
                        if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].Quality > 0)
                            {
                                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                                {
                                    Items[i].Quality = Items[i].Quality - 1;
                                }
                            }
                        }
                        else
                        {
                            Items[i].Quality = Items[i].Quality - Items[i].Quality;
                        }
                    }
                    else
                    {
                        if (Items[i].Quality < 50)
                        {
                            Items[i].Quality = Items[i].Quality + 1;
                        }
                    }
                }
            }
        }
    }
}
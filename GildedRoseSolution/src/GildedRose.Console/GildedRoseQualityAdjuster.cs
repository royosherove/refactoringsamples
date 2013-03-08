using System.Collections.Generic;

namespace GildedRose.Console
{
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
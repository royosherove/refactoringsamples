using System.Collections.Generic;

namespace GildedRose.Console
{
    public class Program
    {
        IList<Item> Items;
        public static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
                          {
                              Items = new List<Item>
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 10,
                                                      Quality = 49
                                                  },
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 5,
                                                      Quality = 49
                                                  },
                                              // this conjured item does not work properly yet
                                              new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          }

                          };

            for (var i = 0; i < 31; i++)
            {
                System.Console.WriteLine("-------- day " + i + " --------");
                System.Console.WriteLine("name, sellIn, quality");
                for (var j = 0; j < app.Items.Count; j++)
                {
                    System.Console.WriteLine(app.Items[j].Name + ", " + app.Items[j].SellIn + ", " + app.Items[j].Quality);
                }
                System.Console.WriteLine("");
                app.UpdateQuality();
            }

        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                Item item = Items[i];
                QualityStrategyFinder finder = new QualityStrategyFinder();
                IQualityStrategy strategy = finder.FindStrategy(item);
                if (strategy!=null)
                {
                    strategy.UpdateQuality(item);
                    continue;
                }

                bool notBrie = item.Name != "Aged Brie";
                bool notBackStage = item.Name != "Backstage passes to a TAFKAL80ETC concert";
                bool notRegnaros = item.Name != "Sulfuras, Hand of Ragnaros";
                bool isStandardItem = notBrie && notBackStage;
                if (isStandardItem && notRegnaros)
                {
                        DecraseQuality(item);
                }
                else
                {
                    IncreaseQualityIfRelevant(item);
                }

                if (notRegnaros)
                {
                    item.SellIn = item.SellIn - 1;
                }

                if (item.SellIn < 0)
                {
                    if (notRegnaros)
                    {
                        DecraseQuality(item);
                    }
                }
            }
        }

        private static void HandleBackstage(Item item)
        {
            SetNoQuality(item);
        }

        private static void HandleBrie(Item item)
        {
            IncreaseQualityIfRelevant(item);
        }

        private static void SetNoQuality(Item item)
        {
            item.Quality = item.Quality - item.Quality;
        }

        private static void HandleBackstagePasses(Item item)
        {
            if (item.SellIn < 11)
            {
                IncreaseQualityIfRelevant(item);
            }

            if (item.SellIn < 6)
            {
                IncreaseQualityIfRelevant(item);
            }
        }

        private static void IncreaseQualityIfRelevant(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality = item.Quality + 1;
            }
        }

        private static void DecraseQuality(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality = item.Quality - 1;
            }
        }
    }
}

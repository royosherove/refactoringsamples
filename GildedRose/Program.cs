using System.Collections.Generic;

namespace GildedRose.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program(new DayReporter(),GetItems())
            app.Run();
        }

        public IList<Item> Items;
        public DayReporter Reporter { get; set; }

        private Program(DayReporter dayReporter, List<Item> items)
        {
            Reporter = dayReporter;
            Items = items;
        }


        public void Run()
        {
            for (var i = 0; i < 31; i++)
            {
                Reporter.Report(i, this);
                UpdateQuality();
            }
        }

        private static List<Item> GetItems()
        {
            return new List<Item>
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
                       };
        }



        public void UpdateQuality()
        {
            foreach (Item item in Items)
            {
                QualityStrategyFinder finder = new QualityStrategyFinder();
                IQualityStrategy strategy = finder.FindStrategy(item);
                strategy.UpdateQuality(item);
            }
        }
    }
}

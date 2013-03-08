namespace GildedRose.Console
{
    public class DayReporter : IDayReporter
    {
        public void Report(int dayNumber, Program app)
        {

            System.Console.WriteLine("-------- day " + dayNumber + " --------");
            System.Console.WriteLine("name, sellIn, quality");
            for (var j = 0; j < app.Items.Count; j++)
            {
                System.Console.WriteLine(app.Items[j].Name + ", " + app.Items[j].SellIn + ", " + app.Items[j].Quality);
            }
            System.Console.WriteLine("");

        }
    }
}
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GildedRose.Console
{
    public class Program
    {
        IList<Item> Items;

        #region item type defintions
        static IList<Regex> LegendaryItems = new List<Regex>()
        {
            new Regex(@".*(Sulfuras).*", RegexOptions.IgnoreCase)
        };
        static IList<Regex> BackstagePasses = new List<Regex>()
        {
            new Regex(@".*(Backstage passes).*", RegexOptions.IgnoreCase),
        };
        static IList<Regex> BetterWhenAged = new List<Regex>()
        {
            new Regex(@".*(Aged Brie).*", RegexOptions.IgnoreCase),
        };
        static IList<Regex> ConjuredItems = new List<Regex>()
        {
            new Regex(@".*(Conjured).*", RegexOptions.IgnoreCase),
        };
        #endregion

        #region app constants
        public const int itemMinQuality = 0;
        public const int itemMaxQuality = 50;
        #endregion

        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
            {
                Items = new List<Item>
                {
                    new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                    new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                    new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                    new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                    new Item
                    {
                        Name = "Backstage passes to a TAFKAL80ETC concert",
                        SellIn = 15,
                        Quality = 20
                    },
                    new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
                },
            };

            app.Items = UpdateQuality(app.Items);

            System.Console.ReadKey();

        }

        public static IList<Item> UpdateQuality(IList<Item> itemsToUpdate)
        {
            foreach (Item item in itemsToUpdate)
            {
                // move a day closer to selling it
                item.SellIn = item.SellIn - 1;

                if (ItemMatchesType(item, LegendaryItems))
                {
                    // Item is fixed quality and sellin date
                    item.SellIn = 0;
                    item.Quality = 80;
                }

                else if (ItemMatchesType(item, BackstagePasses))
                {
                    // Item is worthless when event has passed
                    if (item.SellIn <= 0)
                    {
                        item.Quality = 0;
                    }
                    else
                    {
                        item.Quality = IncreaseBackstageQuality(item);
                    }
                }

                else if (ItemMatchesType(item, BetterWhenAged))
                {
                    // Items age till max quality
                    item.Quality = Math.Min(item.Quality + 1, itemMaxQuality);
                }

                else if (ItemMatchesType(item, ConjuredItems))
                {
                    // Items age till max quality
                    item.Quality = Math.Max(item.Quality - 2, itemMinQuality);
                }

                else
                {
                    item.Quality = Math.Max(item.Quality - (item.SellIn >= 0 ? 1 : 2), itemMinQuality);
                }
            }

            return itemsToUpdate;
        }

        // Check type combination of regex and see if item is within that type
        public static bool ItemMatchesType(Item item, IList<Regex> TypeDefinition)
        {
            foreach (Regex regex in TypeDefinition)
            {
                if (regex.IsMatch(item.Name))
                {
                    return true;
                }
            }

            return false;
        }

        // Get quality modifier for backstage items
        public static int IncreaseBackstageQuality(Item item)
        {
            var amountToIncrease = 2;

            if (item.SellIn >= 10)
            {
                amountToIncrease = 1;
            }

            if (item.SellIn <= 5)
            {
                amountToIncrease = 3;
            }

            // when sellin is between 10 en 5
            return Math.Min(item.Quality + amountToIncrease, itemMaxQuality);
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}

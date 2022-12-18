using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GildedRose.Console
{
    class Program
    {
        IList<Item> Items;
        
        IList<Regex> LegendaryItems;
        IList<Regex> BackstagePasses;
        IList<Regex> BetterWhenAged;
        IList<Regex> ConjuredItems;

        const int itemMinQuality = 50;
        const int itemMaxQuality = 50;
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
                LegendaryItems = new List<Regex>()
                {
                    new Regex(@".*(Sulfuras).*", RegexOptions.IgnoreCase)
                },
                BackstagePasses = new List<Regex>()
                {
                    new Regex(@".*(Backstage passes).*", RegexOptions.IgnoreCase),
                },
                BetterWhenAged = new List<Regex>()
                {
                    new Regex(@".*(Aged Brie).*", RegexOptions.IgnoreCase),
                },
                ConjuredItems = new List<Regex>() { 
                    new Regex(@".*(Conjured).*", RegexOptions.IgnoreCase), 
                },
            };

            app.UpdateQuality(app.Items);

            System.Console.ReadKey();

        }

        public void UpdateQuality(IList<Item> itemsToUpdate)
        {
            foreach (Item item in itemsToUpdate) {
                // move a day closer to selling it
                item.SellIn = item.SellIn - 1;

                #region legendary
                if (ItemMatchesType(item, LegendaryItems))
                {
                    // Item is fixed quality and sellin date
                    item.SellIn = 0;
                    item.Quality = 80;
                }
                #endregion

                #region backstagepasses
                else if (ItemMatchesType(item, BackstagePasses))
                {
                    // Item is worthless when event has passed
                    if (item.SellIn <= 0)
                    {
                        item.Quality = 0;
                    }
                    else
                    {
                        item.Quality = item.Quality + IncreaseBackstageQuality(item);
                        item.Quality = Math.Min(item.Quality, itemMaxQuality);
                    }
                }
                #endregion

                #region better aged
                else if (ItemMatchesType(item, BetterWhenAged))
                {
                    // Items age till max quality
                    item.Quality = Math.Min(item.Quality + 1, itemMaxQuality);
                }
                #endregion

                #region conjured
                else if (ItemMatchesType(item, ConjuredItems))
                {
                    // Items age till max quality
                    item.Quality = Math.Max(item.Quality - 2, itemMinQuality);
                }
                #endregion

                #region default process
                else
                {
                    item.Quality = Math.Max(item.Quality - 1, itemMinQuality);
                }
                #endregion
            }
        }

        // Check type combination of regex and see if item is within that type
        public Boolean ItemMatchesType (Item item, IList<Regex> TypeDefinition)
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
        public int IncreaseBackstageQuality(Item item)
        {
            if (item.SellIn >= 10)
            {
                return 1;
            }

            if (item.SellIn <= 5)
            {
                return 3;
            }

            // when sellin is between 10 en 5
            return 2;
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}

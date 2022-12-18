using Xunit;
using GildedRose.Console;
using System.Collections.Generic;

namespace GildedRose.Tests
{
    public class TestLegendaryItems
    {
        [Fact]
        public void ItemShouldntChangeQuality()
        {
            List<Item> items = new List<Item>()
            {
                new Item()
                {
                    Quality = 5,
                    Name = "Test Sulfuras item",
                    SellIn = -1,
                },
            };

            Program.UpdateQuality(items);

            Assert.Equal(items[0].Quality, 80);
            Assert.Equal(items[0].SellIn, 0);
        }
    }

    public class TestConjuredItems
    {
        [Fact]
        public void ItemShouldLoseQualityTwiceAsFast()
        {
            List<Item> items = new List<Item>()
            {
                new Item()
                {
                    Quality = 5,
                    Name = "Conjured test item",
                    SellIn = 5,
                },
                new Item() {
                    Quality = 5,
                    Name = "Conjured test item",
                    SellIn = -8,
                },
            };

            Program.UpdateQuality(items);

            Assert.Equal(items[0].Quality, 3);
            Assert.Equal(items[0].SellIn, 4);
            Assert.Equal(items[1].Quality, 3);
            Assert.Equal(items[1].SellIn, -9);
        }

        [Fact]
        public void ItemShouldNotGoUnterZero()
        {
            List<Item> items = new List<Item>()
            {
                new Item()
                {
                    Quality = 0,
                    Name = "Conjured test item",
                    SellIn = 5,
                },
            };

            Program.UpdateQuality(items);

            Assert.Equal(items[0].Quality, 0);
            Assert.Equal(items[0].SellIn, 4);
        }
    }

    public class TestAgedItems
    {
        [Fact]
        public void ItemShouldIncreaseQuality()
        {
            List<Item> items = new List<Item>()
            {
                new Item()
                {
                    Quality = 5,
                    Name = "Aged Brie test item",
                    SellIn = 5,
                },
                new Item() {
                    Quality = 5,
                    Name = "Aged Brie test item",
                    SellIn = -5,
                },
            };

            Program.UpdateQuality(items);

            Assert.Equal(items[0].Quality, 6);
            Assert.Equal(items[0].SellIn, 4);
            Assert.Equal(items[1].Quality, 6);
            Assert.Equal(items[1].SellIn, -6);
        }

        [Fact]
        public void ItemQualityShouldntGoOverMax()
        {
            List<Item> items = new List<Item>()
            {
                new Item()
                {
                    Quality = Program.itemMaxQuality + 5,
                    Name = "Aged Brie test item",
                    SellIn = 5,
                },
                new Item() {
                    Quality = Program.itemMaxQuality + 5,
                    Name = "Aged Brie test item",
                    SellIn = -5,
                },
            };

            Program.UpdateQuality(items);

            Assert.Equal(items[0].Quality, Program.itemMaxQuality);
            Assert.Equal(items[0].SellIn, 4);
            Assert.Equal(items[1].Quality, Program.itemMaxQuality);
            Assert.Equal(items[1].SellIn, -6);
        }
    }

    public class TestBackstageItems
    {
        [Fact]
        public void ItemShouldHaveNoQualityAfterSellin()
        {
            List<Item> items = new List<Item>()
            {
                new Item()
                {
                    Quality = 5,
                    Name = "Backstage passes 1",
                    SellIn = -1,
                }
            };

            Program.UpdateQuality(items);

            Assert.Equal(items[0].Quality, 0);
            Assert.Equal(items[0].SellIn, -2);
        }

        [Fact]
        public void ItemShouldIncreaseQuality()
        {
            List<Item> items = new List<Item>()
            {
                new Item()
                {
                    Quality = 5,
                    Name = "Backstage passes 1",
                    SellIn = 15,
                },
                new Item()
                {
                    Quality = 5,
                    Name = "Backstage passes 2",
                    SellIn = 10,
                },
                new Item()
                {
                    Quality = 5,
                    Name = "Backstage passes 3",
                    SellIn = 5,
                },
                new Item()
                {
                    Quality = 5,
                    Name = "Backstage passes 3",
                    SellIn = 11,
                }
            };

            Program.UpdateQuality(items);

            Assert.Equal(items[0].Quality, 6);
            Assert.Equal(items[1].Quality, 7);
            Assert.Equal(items[2].Quality, 8);
        }

        [Fact]
        public void ItemQualityShouldntGoOverMax()
        {
            List<Item> items = new List<Item>()
            {
                new Item()
                {
                    Quality = Program.itemMaxQuality + 10,
                    Name = "Backstage passes 1",
                    SellIn = 4,
                }
            };

            Program.UpdateQuality(items);

            Assert.Equal(items[0].Quality, Program.itemMaxQuality);
        }
    }

    public class TestDefaultItems
    {
        [Fact]
        public void ItemShouldLoseQuality()
        {
            List<Item> items = new List<Item>()
            {
                new Item()
                {
                    Quality = 5,
                    Name = "Test Item",
                    SellIn = 5,
                }
            };

            Program.UpdateQuality(items);

            Assert.Equal(items[0].Quality, 4);
            Assert.Equal(items[0].SellIn, 4);
        }

        [Fact]
        public void ItemShouldLoseQualityTwiceAfterSellInDate()
        {
            List<Item> items = new List<Item>()
            {
                new Item()
                {
                    Quality = 5,
                    Name = "Test Item",
                    SellIn = -1,
                }
            };

            Program.UpdateQuality(items);

            Assert.Equal(items[0].Quality, 3);
            Assert.Equal(items[0].SellIn, -2);
        }

        [Fact]
        public void ItemQualityShouldNotGoUnderZero()
        {
            List<Item> items = new List<Item>()
            {
                new Item()
                {
                    Quality = 0,
                    Name = "Test Item",
                    SellIn = -1,
                }
            };

            Program.UpdateQuality(items);

            Assert.Equal(items[0].Quality, 0);
            Assert.Equal(items[0].SellIn, -2);
        }
    }

    public class TestProgramFlow
    {
        [Fact]
        public void IntegrationTest()
        {
            List<Item> items = new List<Item>()
            {
                new Item()
                {
                    Quality = 5,
                    Name = "Test Item",
                    SellIn = 5,
                },
                 new Item()
                {
                    Quality = 5,
                    Name = "Backstage passes 1",
                    SellIn = 15,
                },
                new Item()
                {
                    Quality = 5,
                    Name = "Aged Brie test item",
                    SellIn = 8,
                },
                new Item()
                {
                    Quality = 5,
                    Name = "Conjured test item",
                    SellIn = 3,
                },
                new Item {
                    Quality = 5,
                    Name = "Test Sulfuras item",
                    SellIn = -1,
                },
            };

            Program.UpdateQuality(items);

            Assert.Equal(items[0].Quality, 4);
            Assert.Equal(items[0].SellIn, 4);
            Assert.Equal(items[1].Quality, 6);
            Assert.Equal(items[1].SellIn, 14);
            Assert.Equal(items[2].Quality, 6);
            Assert.Equal(items[2].SellIn, 7);
            Assert.Equal(items[3].Quality, 3);
            Assert.Equal(items[3].SellIn, 2);
            Assert.Equal(items[4].Quality, 80);
            Assert.Equal(items[4].SellIn, 0);
        }
    }
}
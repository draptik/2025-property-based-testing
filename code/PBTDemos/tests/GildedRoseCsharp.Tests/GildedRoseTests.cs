using FsCheck;
using FsCheck.Fluent;
using FsCheck.Xunit;

using PBTDemos.GildedRose;

// ReSharper disable HeapView.ObjectAllocation

namespace GildedRoseCsharp.Tests;

public class GildedRoseTests
{
  private static Item GenerateItem(string name, int sellIn, PositiveInt quality)
  {
    return new Item { Name = name, SellIn = sellIn, Quality = quality.Get };
  }

  [Property]
  public bool Quality_remains_between_0_and_50_after_update(string name, int sellIn, PositiveInt quality)
  {
    var item = GenerateItem(name, sellIn, quality);
    var actual = item.UpdateQuality();
    return actual.Quality is >= 0 and <= 50;
  }

  [Property]
  public bool Aged_Brie_quality_increases_but_not_beyond_50(PositiveInt sellIn, PositiveInt quality)
  {
    var item = GenerateItem("Aged Brie", sellIn.Get, quality);
    var actual = item.UpdateQuality();
    return quality.Get < 50
      ? actual.Quality == quality.Get + 1
      : actual.Quality == 50;
  }

  [Property]
  public bool SellIn_decreases_by_1_unless_it_is_Sulfuras_v1(string name, int sellIn, PositiveInt quality)
  {
    var item = GenerateItem(name, sellIn, quality);
    var actual = item.UpdateQuality();
    return name == "Sulfuras, Hand of Ragnaros"
      ? actual.SellIn == sellIn
      : actual.SellIn == sellIn - 1;
  }

  [Property(Arbitrary = [typeof(ItemArb)])]
  public bool SellIn_decreases_by_1_unless_it_is_Sulfuras_v2(Item item)
  {
    var sellIn = item.SellIn;
    var actual = item.UpdateQuality();
    return item.Name == "Sulfuras, Hand of Ragnaros"
      ? actual.SellIn == sellIn
      : actual.SellIn == sellIn - 1;
  }

  private class ItemArb
  {
    public static Arbitrary<Item> Generate()
    {
      var generator =
        from name in Gen.Elements(
          "Aged Brie",
          "Sulfuras, Hand of Ragnaros",
          "Backstage passes to a TAFKAL80ETC concert")
        from sellin in Gen.Choose(1, 100)
        from quality in Gen.Choose(1, int.MaxValue)
        select GenerateItem(name, sellin, PositiveInt.NewPositiveInt(quality));
      return Arb.From(generator);
    }
  }
}
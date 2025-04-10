// ReSharper disable InconsistentNaming

using System.Diagnostics.CodeAnalysis;
// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable ForCanBeConvertedToForeach
// ReSharper disable ConvertToCompoundAssignment
// ReSharper disable InvertIf

namespace PBTDemos.GildedRose;

[SuppressMessage("Style", "IDE1006:Naming Styles")]
[SuppressMessage("Style", "IDE0290:Use primary constructor")]
[SuppressMessage("Style", "IDE0078:Use pattern matching")]
public class GildedRose
{
  private readonly IList<Item> Items;

  public GildedRose(IList<Item> Items)
  {
    this.Items = Items;
  }

  public void UpdateQuality()
  {
    for (var i = 0; i < Items.Count; i++)
    {
      if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
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

      if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
      {
        Items[i].SellIn = Items[i].SellIn - 1;
      }

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
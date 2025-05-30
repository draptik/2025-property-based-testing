#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
namespace PBTDemos.GildedRose;

// ReSharper disable once ClassNeverInstantiated.Global
public class Item
{
  public string Name { get; set; }
  public int SellIn { get; set; }
  public int Quality { get; set; }

  public override string ToString()
  {
    return $"name: '{Name}', sellIn: '{SellIn}', quality: '{Quality}'";
  }
}
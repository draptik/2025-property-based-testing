using System.Diagnostics.CodeAnalysis;

namespace HelloWorld;

[SuppressMessage("Style", "IDE0046:Convert to conditional expression")]
public static class Reverser
{
  public static List<int> MyReverse_1(List<int> input)
  {
    return [3, 2, 1];
  }

  public static List<int> MyReverse_2(List<int> input)
  {
    if (input.SequenceEqual((List<int>)[6, 7, 8]))
    {
      return [8, 7, 6];
    }

    return [3, 2, 1];
  }

  public static List<int> MyReverse_3(List<int> input)
  {
    if (input.Count == 0)
    {
      return [];
    }

    if (input.SequenceEqual((List<int>)[6, 7, 8]))
    {
      return [8, 7, 6];
    }

    return [3, 2, 1];
  }

  public static List<int> MyReverse(List<int> input)
  {
    if (input.Count == 0)
    {
      return [];
    }

    if (input.SequenceEqual((List<int>)[1, 2, 3, 4, 5, 6, 7, 8, 9, 10]))
    {
      return [10, 9, 8, 7, 6, 5, 4, 3, 2, 1];
    }

    if (input.SequenceEqual((List<int>)[6, 7, 8]))
    {
      return [8, 7, 6];
    }

    return [3, 2, 1];
  }

  public static List<int> MyReverse_final(List<int> input)
  {
    return input.AsEnumerable().Reverse().ToList();
  }
}
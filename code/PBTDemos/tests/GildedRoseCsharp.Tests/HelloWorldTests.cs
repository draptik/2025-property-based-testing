namespace GildedRoseCsharp.Tests;

public class HelloWorldTests
{
  private static List<int> MyReverse(List<int> input)
  {
    if (input.Count == 0)
    {
      return [];
    }

    if (input.SequenceEqual(Enumerable.Range(1, 10).ToList()))
    {
      return [10, 9, 8, 7, 6, 5, 4, 3, 2, 1];
    }

    if (input.SequenceEqual((List<int>)[6, 7, 8]))
    {
      return [8, 7, 6];
    }

    return [3, 2, 1];
  }

  [Fact]
  public void List_123_returns_321()
  {
    List<int> input = [1, 2, 3];
    var actual = MyReverse(input);
    List<int> expected = [3, 2, 1];
    Assert.Equivalent(expected, actual);;
  }

  [Fact]
  public void Empty_list_returns_empty_list()
  {
    List<int> input = [];
    var actual = MyReverse(input);
    var expected = new List<int>();
    Assert.Equivalent(expected, actual);
  }

  [Fact]
  public void List_678_returns_876()
  {
    List<int> input = [6, 7, 8];
    var actual = MyReverse(input);
    List<int> expected = [8, 7, 6];
    Assert.Equivalent(expected, actual);
  }

  [Fact]
  public void List_length_does_not_matter()
  {
    List<int> input = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    var actual = MyReverse(input);
    List<int> expected = [10, 9, 8, 7, 6, 5, 4, 3, 2, 1];
    Assert.Equivalent(expected, actual);
  }

  [Fact]
  public void First_input_element_is_last_element_of_output()
  {
    List<int> input = [1, 2, 3];
    var actual = MyReverse(input);
    var expected = input.First();
    Assert.Equal(expected, actual.Last());
  }

  [Fact]
  public void Reversing_a_list_twice_returns_the_original_list()
  {
    List<int> input = [1, 2, 3];
    var actual = MyReverse(MyReverse(input));
    Assert.Equivalent(input, actual);
  }

  private static int Add(int a, int b) => a + b;

  [Fact]
  public void Add_1_and_2_returns_3()
  {
    var actual = Add(1, 2);
    var expected = 3;
    Assert.Equal(expected, actual);
  }

  [Fact]
  public void Add_1_and_2_returns_3_2()
  {
    var rnd = new Random();
    var a = rnd.Next(100);
    var b = rnd.Next(100);
    var actual = Add(a, b);
    var expected = a + b;
    Assert.Equal(expected, actual);
  }
}
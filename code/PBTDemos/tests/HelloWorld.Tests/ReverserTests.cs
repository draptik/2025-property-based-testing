using static HelloWorld.Reverser;

namespace HelloWorld.Tests;

public class ReverserTests
{
  [Fact]
  public void List_123_returns_321()
  {
    var actual = MyReverse_1([1, 2, 3]);
    var expected = new List<int> { 3, 2, 1 };
    Assert.Equal(expected, actual);
  }

  [Fact]
  public void List_678_returns_876()
  {
    List<int> input = [6, 7, 8];
    // var actual = MyReverse_1(input);
    var actual = MyReverse_2(input);
    List<int> expected = [8, 7, 6];
    Assert.Equal(expected, actual);
  }

  [Fact]
  public void Empty_list_returns_empty_list()
  {
    List<int> input = [];
    // var actual = MyReverse_1(input);
    // var actual = MyReverse_2(input);
    var actual = MyReverse_3(input);
    Assert.Equal([], actual);
  }

  [Fact]
  public void List_length_does_not_change()
  {
    List<int> input = [1, 2, 3];
    var actual = MyReverse(input);
    var expected = input.Count;
    Assert.Equivalent(expected, actual.Count);
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
}
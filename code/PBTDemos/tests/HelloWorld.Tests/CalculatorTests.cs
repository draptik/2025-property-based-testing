using System.Diagnostics.CodeAnalysis;

using static HelloWorld.Calculator;

namespace HelloWorld.Tests;

[SuppressMessage("Style", "IDE0022:Use block body for method")]
public class CalculatorTests
{
  // [Fact]
  [Fact(Skip = "example")]
  public void Add_1_and_2_returns_3()
  {
    var actual = Add_1(1, 2);
    var expected = 3;
    Assert.Equal(expected, actual);
  }

  // [Fact]
  [Fact(Skip = "example")]
  public void Add_2_and_2_returns_4()
  {
    // var actual = Add_1(2, 2);
    var actual = Add_2(2, 2);
    var expected = 4;
    Assert.Equal(expected, actual);
  }

  /// <summary>
  /// Yes, this can also be done using a parameterized test.
  /// </summary>
  // [Fact]
  [Fact(Skip = "example")]
  public void Add_two_numbers_returns_the_sum()
  {
    var testData = new List<(int, int, int)>
    {
      (1, 2, 3),
      (2, 2, 4),
      (3, 5, 8),
      (27, 15, 42)
    };
    foreach (var (a, b, expected) in testData)
    {
      // var actual = Add_1(a, b);
      // var actual = Add_2(a, b);
      var actual = Add_3(a, b);
      Assert.Equal(expected, actual);
    }
  }

  // [Fact]
  [Fact(Skip = "example")]
  public void Add_two_random_numbers_returns_the_sum_1()
  {
    var rnd = new Random();
    var a = rnd.Next(100);
    var b = rnd.Next(100);
    var actual = Add(a, b);
    var expected = a + b;
    Assert.Equal(expected, actual);
  }

  // [Fact]
  [Fact(Skip = "example")]
  public void Add_two_random_numbers_returns_the_sum_2()
  {
    var rnd = new Random();
    foreach (var _ in Enumerable.Range(1, 100))
    {
      var a = rnd.Next(100);
      var b = rnd.Next(100);
      var actual = Add(a, b);
      var expected = a + b; // ⚠️
      Assert.Equal(expected, actual);
    }
  }

  // --------------------------------------------------------------------------
  // Finding properties of the `Add` function
  // --------------------------------------------------------------------------

  [Fact]
  public void Add_is_independent_of_order_Commutativity()
  {
    var rnd = new Random();
    foreach (var _ in Enumerable.Range(1, 100))
    {
      var a = rnd.Next(100);
      var b = rnd.Next(100);
      var result1 = Add_4(a, b);
      var result2 = Add_4(b, a); // <- ⚠️also applies to `Multiply`
      Assert.Equal(result1, result2);
    }
  }

  [Fact]
  public void Add_number_to_itself_is_equivalent_to_multiplying_by_2()
  {
    var rnd = new Random();
    foreach (var _ in Enumerable.Range(1, 100))
    {
      var a = rnd.Next(100);
      var result1 = Add(a, a);
      var result2 = a * 2; // <- ⚠️now dependent on multiplication
      Assert.Equal(result1, result2);
    }
  }

  [Fact]
  public void Add_1_twice_is_equivalent_to_add_2()
  {
    var rnd = new Random();
    foreach (var _ in Enumerable.Range(1, 100))
    {
      var a = rnd.Next(100);
      var result1 = Add(Add(a, 1), 1);
      var result2 = Add(a, 2);
      Assert.Equal(result1, result2);
    }
  }

  [Fact]
  public void Add_zero_is_a_no_op()
  {
    var rnd = new Random();
    foreach (var _ in Enumerable.Range(1, 100))
    {
      var a = rnd.Next(100);
      var result1 = Add(a, 0);
      var result2 = a;
      Assert.Equal(result1, result2);
    }
  }
}
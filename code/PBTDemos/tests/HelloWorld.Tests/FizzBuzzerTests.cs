using FsCheck;
using FsCheck.Fluent;
using FsCheck.Xunit;

using JetBrains.Annotations;

namespace HelloWorld.Tests;

public static class FizzBuzzing
{
  public static string FizzBuzz(int n)
  {
    if (n % 3 == 0 && n % 5 == 0)
    {
      return "FizzBuzz";
    }

    if (n % 3 == 0)
    {
      return "Fizz";
    }

    if (n % 5 == 0)
    {
      return "Buzz";
    }

    return n.ToString();
  }
}

[UsedImplicitly]
public class FizzBuzzTests
{
  public class NaiveTests
  {
    [Fact]
    public void IsANumber()
    {
      var arb = Arb.From(
        Gen.Choose(0, 100)
          .Where(x => x % 3 != 0 && x % 5 != 0));

      Prop.ForAll(arb, number =>
          FizzBuzzing.FizzBuzz(number) == number.ToString())
        .QuickCheckThrowOnFailure();
    }

    [Fact]
    public void DivisibleByThree()
    {
      var arb = Arb.From(
        Gen.Choose(0, 100)
          .Select(x => x * 3));

      Prop.ForAll(arb, number =>
          FizzBuzzing.FizzBuzz(number).StartsWith("Fizz"))
        .QuickCheckThrowOnFailure();
    }

    [Fact]
    public void DivisibleByFive()
    {
      var arb = Arb.From(
        Gen.Choose(0, 100)
          .Select(x => x * 5)
          .Where(x => x % 3 != 0));

      Prop.ForAll(arb, number =>
          FizzBuzzing.FizzBuzz(number) == "Buzz")
        .QuickCheckThrowOnFailure();
    }

    [Fact]
    public void DivisibleByFifteen()
    {
      var arb = Arb.From(
        Gen.Choose(0, 100)
          .Select(x => x * 15));

      Prop.ForAll(arb, number =>
          FizzBuzzing.FizzBuzz(number) == "FizzBuzz")
        .QuickCheckThrowOnFailure();
    }
  }

  public class XunitPropertyAttributePattern
  {
    [Property(Arbitrary = [typeof(NonDivisibleNumberArbitrary)])]
    public void NonDivisibleNumbers(int number)
    {
      Assert.Equal(number.ToString(), FizzBuzzing.FizzBuzz(number));
    }

    [Property(Arbitrary = [typeof(DivisibleByThreeArbitrary)])]
    public void DivisibleByThree(int number)
    {
      Assert.Equal("Fizz", FizzBuzzing.FizzBuzz(number));
    }

    [Property(Arbitrary = [typeof(DivisibleByFiveArbitrary)])]
    public void DivisibleByFive(int number)
    {
      Assert.Equal("Buzz", FizzBuzzing.FizzBuzz(number));
    }

    [Property(Arbitrary = [typeof(DivisibleByFifteenArbitrary)])]
    public void DivisibleByFifteen(int number)
    {
      Assert.Equal("FizzBuzz", FizzBuzzing.FizzBuzz(number));
    }

    private class NonDivisibleNumberArbitrary
    {
      public static Arbitrary<int> NonDivisibleNumber()
      {
        return Arb.From(
          Gen.Choose(0, 100)
            .Where(x => x % 3 != 0 && x % 5 != 0));
      }
    }

    private class DivisibleByThreeArbitrary
    {
      public static Arbitrary<int> DivisibleByThree()
      {
        return Arb.From(
          Gen.Choose(0, 100)
            .Select(x => x * 3)
            .Where(x => x % 5 != 0));
      }
    }

    private class DivisibleByFiveArbitrary
    {
      public static Arbitrary<int> DivisibleByFive()
      {
        return Arb.From(
          Gen.Choose(0, 100)
            .Select(x => x * 5)
            .Where(x => x % 3 != 0));
      }
    }

    private class DivisibleByFifteenArbitrary
    {
      public static Arbitrary<int> DivisibleByFifteen()
      {
        return Arb.From(
          Gen.Choose(0, 100)
            .Select(x => x * 15));
      }
    }
  }

  public class PropertyTests
  {
    [Property]
    public void AtLeastOneNumberInThreeConsecutiveValues(int i)
    {
      var range = Enumerable.Range(i, 3);
      var actual = range
        .Select(FizzBuzzing.FizzBuzz)
        .Select(s => int.TryParse(s, out var num) ? (int?)num : null)
        .Count(x => x.HasValue);

      Assert.True(actual >= 1);
    }

    [Property]
    public void OnlyOneBuzzInFiveConsecutiveValues(int i)
    {
      var range = Enumerable.Range(i, 5);
      var actual = range
        .Select(FizzBuzzing.FizzBuzz)
        .Count(s => s.EndsWith("Buzz"));

      Assert.Equal(1, actual);
    }

    [Property]
    public void AtLeastOneLiteralBuzzInTenValues(int i)
    {
      var range = Enumerable.Range(i, 10);
      var actual = range
        .Select(FizzBuzzing.FizzBuzz)
        .Count(s => s == "Buzz");

      Assert.True(actual >= 1);
    }

    [Property]
    public void NumbersRoundTrip(int i)
    {
      var range = Enumerable.Range(i, 3).ToList();
      var actual = range
        .Select(FizzBuzzing.FizzBuzz)
        .Select(s => int.TryParse(s, out var num) ? (int?)num : null)
        .Where(x => x.HasValue)
        .All(x => range.Contains(x.Value));

      Assert.True(actual);
    }
  }
}
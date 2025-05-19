using FsCheck;
using FsCheck.Fluent;
using FsCheck.Xunit;

namespace HelloWorld.Tests;

public class ShrinkerDemoTests
{
  // Shrinker Example 1: IsValid ==============================================
  //
  private static bool IsValid(string s)
  {
    return s.StartsWith('a');
  }

  [Property(Skip = "should fail")]
  // [Property(Verbose = true, Replay = "3881495029246094755,17747962478717517451")]
  public void Validation_works(string input)
  {
    Assert.True(IsValid(input));
  }

  // Shrinker Example 2: Age ==================================================
  //
  private static bool IsOldEnough(int age)
  {
    return age >= 18;
  }

  private class AgeArbitrary
  {
    public static Arbitrary<int> From()
    {
      return Arb.From(Gen.Choose(17, 23));
    }
  }

  [Property(Skip = "should fail")]
  // [Property(Arbitrary = [typeof(AgeArbitrary)], Verbose = true)]
  public bool IsOldEnough_works(int age)
  {
    return IsOldEnough(age);
  }
}
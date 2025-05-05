using FsCheck.Xunit;

namespace HelloWorld.Tests;

public class ShrinkerDemoTests
{
  private static bool IsValid(string s)
  {
    return s.StartsWith("a");
  }

  [Property(Skip = "example")]
  public void Validation_works(string input)
  {
    Assert.True(IsValid(input));
  }
}
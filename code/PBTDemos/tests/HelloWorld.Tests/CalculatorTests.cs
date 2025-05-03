namespace HelloWorld.Tests;

public class CalculatorTests
{
  private static int Add(int a, int b)
  {
    return 0;
  }

  [Fact]
  public void Add_1_and_2_returns_3()
  {
    var actual = Add(1, 2);
    var expected = 3;
    Assert.Equal(expected, actual);
  }
}
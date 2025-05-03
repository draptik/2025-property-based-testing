using System.Diagnostics.CodeAnalysis;

namespace HelloWorld;

[SuppressMessage("Style", "IDE0046:Convert to conditional expression")]
public static class Calculator
{
  public static int Add_1(int a, int b)
  {
    if (a == 1 && b == 2)
    {
      return 3;
    }

    return 0;
  }
  public static int Add_2(int a, int b)
  {
    if (a == 1 && b == 2)
    {
      return 3;
    }

    if (a == 2 && b == 2)
    {
      return 4;
    }

    return 0;
  }

  public static int Add_3(int a, int b)
  {
    return (a, b) switch
    {
      (1, 2) => 3,
      (2, 2) => 4,
      (3, 5) => 8,
      (27, 15) => 42,
      _ => 0
    };
  }

  public static int Add_4(int a, int b)
  {
    return a * b;
  }

  public static int Add(int a, int b)
  {
    return a + b;
  }
}
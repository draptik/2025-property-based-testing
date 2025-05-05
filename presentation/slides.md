---
theme: ./mathema-2023
defaults:
  layout: "default-with-footer"
title: Property-Based Testing
occasion: "ADC 2025"
occasionLogoUrl: "./images/logo-adc.png"
company: "MATHEMA GmbH"
presenter: "Patrick Drechsler"
contact: "patrick.drechsler@mathema.de"
info:
  Property-Based Testing
layout: cover
transition: slide-left
mdc: true
download: true

src: ./pages/00-title.md
---

---
src: ./pages/01-intro.md
---

---

## Roadmap

- Problem w/ Example-Based Testing
- Examples solving the problem manually
- Property-Based Testing Frameworks
  - Vocabulary w/ examples
  - introduction to Generators
  - more examples w/ Generators
- Comparing FsCheck and CsCheck
- PBT Strategies
- Summary

---
layout: two-cols-header
---

## Example based tests

::left::

Example:

```cs
public int Add(int a, int b)
  => a + b;
```

::right::

Unit test:

```cs
[Fact]
public void Add_works()
  => Add(1, 2).Should().Be(3);
```

More examples with parameterized test:

```cs
[Theory]
[InlineData(1, 2, 3)]
[InlineData(0, 1, 1)]
public void Add_works(int a, int b, int expected)
  => Add(a, b).Should().Be(expected);
```

---
layout: image-left
image: /images/developer_from_hell.png
---

### 😈 Developer From Hell (DFH)

- Test-Driven Development (TDD)
- Only implement enough to make the test pass

---
layout: image-left
image: /images/developer_from_hell.png
---

### 😈 DFH 1/3

```csharp
[Fact]
public void List_123_returns_321()
{
  List<int> input = [1, 2, 3];
  var actual = MyReverse(input);
  List<int> expected = [3, 2, 1];
  Assert.Equal(expected, actual);
}
```

```csharp
// 😈
public List<int> MyReverse(List<int> input)
  => [3, 2, 1];
```

---
layout: image-left
image: /images/developer_from_hell.png
---

### 😈 DFH 2/3

```csharp
[Fact]
public void Empty_list_returns_empty_list()
{
  List<int> input = [];
  var actual = MyReverse(input);
  Assert.Equal([], actual);
}
```

```csharp
// 😈
public List<int> MyReverse(List<int> input)
{
  if (input.Count == 0)
    return [];

  return [3, 2, 1];
}
```

---
layout: image-left
image: /images/developer_from_hell.png
---

### 😈 DFH 3/3

```csharp
[Fact]
public void List_678_returns_876()
{
  List<int> input = [6, 7, 8];
  var actual = MyReverse(input);
  List<int> expected = [8, 7, 6];
  Assert.Equal(expected, actual);
}
```

```csharp
// 😈
public List<int> MyReverse(List<int> input)
{
  if (input.Count == 0)
    return [];
  if (input.SequenceEqual((List<int>)[6, 7, 8]))
    return [8, 7, 6];

  return [3, 2, 1];
}
```

---

## Let's find some properties for reversing a list

- result list has the same size
- result first element is last element of input list
- an empty list returns an empty list
- reversing a list twice returns the original list
- etc...

---

## Live Coding

- Setup without a framework:
  - Adding 2 numbers
  - Reversing a list

---

## PBT Framework Basics

- **Generator**
  - describe the input data!
- **Shrinker**
  - framework gives us minimal examples
- **Generator** ➕ **Shrinker** 👉 **Arbitrary**
- If something fails we not only get a falsifiable result: We get the closest result that does not fail.
- Some PBT Frameworks are called "Hypothesis" (i. e. in Python). Why?

---
layout: image-right
image: /images/Karl_Popper.jpg
---

### Philosophy and natural sciences teach us...

- we can't prove anything
- but we can **falsify** a **Hypothesis**
- finding a good hypothesis is the difficult part

---
layout: image
image: /images/black_swan.jpg
---

---

## Hello World FsCheck (1/2)

```csharp
using FsCheck;        // ⚠️
using FsCheck.Fluent; // ⚠️

[Fact]
public void Reversing_a_list_twice_gives_the_original_list_v1()
{
  // This "Property" must return bool
  static bool CheckFn(List<int> list)
  {
    return MyReverse(MyReverse(list)).SequenceEqual(list);
  }

  // The lambda creates the test data input `list`
  Prop.ForAll((List<int> list) => CheckFn(list))  // ⚠️
    .QuickCheckThrowOnFailure();                  // ⚠️

}
```

- `Prop.ForAll`: creates sample data
- `QuickCheckThrowOnFailure`: throws on first failure

---

## Hello World FsCheck (2/2)

```csharp
using FsCheck;        // ⚠️
using FsCheck.Fluent; // ⚠️
using FsCheck.Xunit;  // ⚠️

[Property] // ⚠️
public bool Reversing_a_list_twice_gives_the_original_list_v2(List<int> list) // ⚠️
{
  var actual = MyReverse(MyReverse(list));
  var expected = list;
  return actual.SequenceEqual(expected);
}
```

- use `Property` attribute
- test must return `bool`
- test must have input parameter(s)

---

## Generators

- data is generated via reflection by default
- generators can be fine-tuned
- FizzBuzz: Wrong-Than-Ok

---

## Live Coding

- FizzBuzz (C#, F#)

---

## What is the difference between CsCheck and FsCheck?

The only difference is in the Generator syntax.

Both can be used from C# and F#.

The name references the implementation, not the intended usage.

---

## Strategies: "There and Back Again"

- When you have a forward and reverse function, i. e. Serialize/Deserialize
- Example: Reversing a list twice returns the original list

---

## Strategies: "Some Things Never Change"

- When there is an invariant available
- Example: Sorting or Mapping a list never changes the length of the list

---

## Strategies: "Test Oracle"

- When 2 functions should return the same result
- Example: Compare output of `LegacyFn` with `RefactoredFn`

---

## Strategies: "The More Things Change, The More They Stay The Same"

- When there are idempotent properties
- Example: Adding `0`, multiplying by `1`

---

## Strategies: "Nuclear Exception"

- Just verify that the function does not throw an exception
- Example: Generate valid inputs, and assert that no exception is thrown

---

## Generators: Also for custom types

- Since most PBT frameworks use reflection under the hood, any type can be generated.

---

## Available for most languages

- Just search for "Property Based Testing" and your language!

---

## Conclusion

- Property-based testing is a technique for testing statements of the type:
  **For all x that satisfy some precondition then some predicates holds**
- It can give you confidence that your code behaves correctly across a wider range of inputs.
- It can help you find bugs in your code resulting from inputs you never would have thought to test.
- This technique can be used alongside existing unit tests.

---
src: ./pages/99-end.md
---

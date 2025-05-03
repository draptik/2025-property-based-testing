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
layout: two-cols-header
---

## Example based tests

::left::

Example:

```cs
public int Add(int a, int b)
  => a + b;
```

Imaging a difficult rule...

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

```cs
[Fact]
public void Add_works_2()
{
  // TODO
}
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
  Assert.Equivalent(expected, actual);
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
  Assert.Equivalent([], actual);
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
  Assert.Equivalent(expected, actual);
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

## Let's find some properties

- result list has the same size
- result first element is last element of input list
- an empty list returns an empty list
- reversing a list twice returns the original list
- etc...

---
layout: image
image: /images/black_swan.jpg
---

---
layout: image-right
image: /images/Karl_Popper.jpg
---

### Philosophy teaches us...

- we can't prove anything
- but we can **falsify**

---

## In Science...

...we need an hypotheses

- this is the most difficult part..
- but, we need the same mind set!

---

## Anatomy of a Property-Based Test framework

General terms:

- **Generator**
  - describe the input data!
- **Shrinker**
  - framework gives us minimal examples
- **Generator** ➕ **Shrinker** 👉 **Arbitrary**

If something fails we not only get a falsifiable result: We get the closest result that does not fail.

---

## What is the difference between CsCheck and FsCheck?

There is no difference.

Both can be used from C# and F#.

The name references the implementation, not the intended usage.

---
src: ./pages/99-end.md
---

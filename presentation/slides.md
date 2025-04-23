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

---

## PBT Hello World - reversing a List

```csharp
public List<int> MyReverse(List<int> input)
  => input.Reverse(input)
```

What are "Properties" of reversing a list?

- result list has the same size
- result first element is last element of input list
- an empty list returns an empty list
- reversing a list twice returns the original list
- etc...

---
layout: image-left
image: /images/developer_from_hell.png
---

### 😈 Developer From Hell (DFH)

- Test-Driven Development (TDD)
- Only implement enough to make the test pass

---
layout: image
image: /images/Karl_Popper.jpg
backgroundSize: contain
---

---
layout: image
image: /images/black_swan.jpg
backgroundSize: contain
---

---

## Anatomy of a Property-Based Test

- Generator
  - use a lot!
- Shrinker
  - as beginner: don't touch
- Generator plus Shrinker equals Arbitrary

If something fails we not only get a falsifiable result:

We get the closest result that does not fail.

---
src: ./pages/99-end.md
---

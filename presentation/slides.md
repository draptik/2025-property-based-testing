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

## In the Grand-Testing Universe - where are we?

TODO: Add a mermaid diagram, showing Testing, TDD, Approval-Testing, and finally PBT.

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

We aren't catching corner cases here...

---

## Another example

```csharp
public bool IsValid(string input)
{
  // ...
  // some logic
  // ...
  return true;
}
```

Wouldn't it be nice to catch all `input` errors early?

So, let's use a Faker library!

Oh, then we'll get a result like `foo bar 123` is invalid.

Let's take the `IsValid` method for a spin.

---
src: ./pages/99-end.md
---

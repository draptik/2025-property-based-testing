---
theme: ./mathema-2023
defaults:
  layout: "default-with-footer"
title: Property-Based Testing
occasion: "TODO occasion"
occasionLogoUrl: "./images/logo_socrates.png"
company: "MATHEMA GmbH"
presenter: "Patrick Drechsler"
contact: "patrick.drechsler@mathema.de"
info:
  TODO info
canvasWidth: 980
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

## Example based tests

Most testing strategies are "Example based".

Example:

<div class="badge" data-language="C#">

```cs
public void Add(int a, int b) => a + b;
```

</div>
<div class="badge" data-language="C#">

```cs
[Fact]
public void Add_works() => Add(1, 2).Should().Be(3);
```

</div>

More example data with parameterized test:

<div class="badge" data-language="C#">

```cs
[Theory]
[InlineData(1, 2, 3)]
[InlineData(0, 1, 1)]
public void Add_works(int a, int b, int expected) => Add(a, b).Should().Be(expected);
```

</div>

---

## PBT Hello World

<div class="badge" data-language="F#">

```fsharp
let reverseList (aList: int list) : int list =
  failwith "TODO"
```

</div>

<div class="badge" data-language="F#">

```fsharp
open Xunit
open FsCheck         // PBT library
open Swensen.Unquote // Assertion library

[<Fact>]
let ``reversing a list twice returns original list`` () =
  let checkFn (aList: int list) =
    let actual = aList |> reverseList |> reverseList
    let expected = aList
    test <@ actual = expected @> // Assertion

  Check.Quick checkFn // PBT Magic
```

</div>

---
src: ./pages/99-end.md
---

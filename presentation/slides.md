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

## Roadmap 1/2

- Example-based Testing
- Hello-World FsCheck
- `Property` attribute
- Arbitraries, Generators, and Shrinkers
- Strategies
- CsCheck: an alternative?
- Summary

---

## Roadmap 2/2

| PBT framework | Test framework | Language | PBT feature |
| --- | --- | --- | --- |
| FsCheck | Xunit | F# | Check.Quick / Check.QuickThrowOnFailure |
| FsCheck | Xunit | C# | Check.Quick / Check.QuickThrowOnFailure |
| FsCheck | NUnit | F# | Check.Quick / Check.QuickThrowOnFailure |
| FsCheck | NUnit | C# | Check.Quick / Check.QuickThrowOnFailure |
| FsCheck | Xunit | F# | Property attribute (no arguments) |
| FsCheck | Xunit | F# | Property attribute (with primitive arguments) |
| FsCheck | Xunit | F# | Properties, Arbitraries and Generators |
| FsCheck | Xunit | F# | Strategies: Randomness, Invariants, Nuclear, There-And-Back, etc |
| CsCheck | Xunit | F# | Syntax? |

<style>
  table {
    font-size: 8px;
    line-height: 8px;
}
</style>

---
layout: two-cols-header
---

## Example based tests

Most testing strategies are "Example based".

::left::

Example:

```cs
public void Add(int a, int b)
  => a + b;
```

Writing tests covering the corner cases is simple.

::right::

```cs
[Fact]
public void Add_works()
  => Add(1, 2).Should().Be(3);
```

More example data with parameterized test:

```cs
[Theory]
[InlineData(1, 2, 3)]
[InlineData(0, 1, 1)]
public void Add_works(int a, int b, int expected)
  => Add(a, b).Should().Be(expected);
```

---

## PBT Hello World (in .NET)

We start without a testing framework like xUnit/NUnit/MSTest.

Let's start with an interactive F# session (aka `fsi/fsx`)!

```fsharp
let reverseList (aList: int list) : int list =
  failwith "TODO"
```

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

---

## Playground magic move

````md magic-move
```fsharp
[<Fact>]
let ``reversing a list twice returns original list`` () =
  let checkFn (aList: int list) =
    let actual = aList |> reverseList |> reverseList
    let expected = aList
    test <@ actual = expected @> // Assertion

  Check.Quick checkFn // PBT Magic
```

```fsharp
[<Fact>]
let ``reversing a list twice returns original list`` () =
  let checkFn (aList: int list) =
    let actual = aList |> reverseList |> reverseList
    test <@ actual = aList @> // Assertion

  Check.Quick checkFn // PBT Magic
```

```fsharp
[<Fact>]
let ``reversing a list twice returns original list`` () =
  let checkFn (aList: int list) =
    test <@ aList |> reverseList |> reverseList = aList @> // Assertion

  Check.Quick checkFn // PBT Magic
```
````

To navigate backwards, use arrow-left.

---

## Playground continued

````md magic-move

```fsharp
open Xunit
open FsCheck

[<Fact>]
let ``reversing a list twice returns original list`` () =
  let checkFn (aList: int list) =
    let actual = aList |> List.rev |> List.rev
    test <@ actual = aList @>

  Check.Quick checkFn
```

```fsharp
open Xunit
open FsCheck

[<Fact>]
let ``reversing a list twice returns original list`` () =
  let checkFn (aList: int list) =
    let actual = aList |> List.rev |> List.rev
    test <@ actual = aList @>

  Check.QuickThrowOnFailure checkFn
```

```fsharp
open Xunit
open FsCheck
open FsCheck.Xunit

[<Property>]
let ``reversing a list twice returns original list`` (aList: int list) =
    let actual = aList |> List.rev |> List.rev
    actual = aList
```
````

---

## Gilded Rose

TODO: Check if this is a good example for this talk...

Use <https://github.com/emilybache/GildedRose-Refactoring-Kata/tree/main/csharp.xUnit> repository

---

## Strategies

- Randomness
- Nuclear Explosion
- There And Back Again
- Test Oracle

---

### Strategy: Randomness

Examples:

- "Pick One"

---

### Strategy: Nuclear Explosion

Test that nothing throws

---

### Strategy: There And Back Again

Examples:

- Serialize/Deserialize

---

### Strategy: Test Oracle

Compare 2 different implementations

---

## TestContainers and PBT: A good idea?

TODO

---

## F# Crash Course

- Syntax: OCaml based
- Immutable by default
- Piping
- Records & Discriminated Unions
- Type Inference
- Currying / Partial Application

---
src: ./pages/99-end.md
---

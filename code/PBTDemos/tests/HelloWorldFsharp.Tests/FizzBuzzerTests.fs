module FizzBuzzerTests

open System
open Xunit
open FsCheck
open FsCheck.FSharp
open FsCheck.Xunit

(*
FizzBuzz is actually an "advanced" example in the PBT context because it requires knowledge of
Arbitraries and Generators
*)
module FizzBuzzing =

  let fizzbuzz n =
    match n % 3, n % 5 with
    | 0, 0 -> "FizzBuzz"
    | 0, _ -> "Fizz"
    | _, 0 -> "Buzz"
    | _ -> n.ToString()

  module FactArbPropCheckPattern =

    [<Fact>]
    let ``Is a number`` () =
      let arb =
        Gen.choose (0, 100)
        |> Gen.filter (fun x -> x % 3 <> 0 && x % 5 <> 0)
        |> Arb.fromGen

      let property number = (fizzbuzz number) = number.ToString()
      Prop.forAll arb property |> Check.QuickThrowOnFailure

    [<Fact>]
    let ``Divisible by 3`` () =
      let arb = Gen.choose (0, 100) |> Gen.map (fun x -> x * 3) |> Arb.fromGen
      let property number = (fizzbuzz number).StartsWith("Fizz")
      Prop.forAll arb property |> Check.QuickThrowOnFailure

    [<Fact>]
    let ``Divisible by 5`` () =
      let arb =
        Gen.choose (0, 100)
        |> Gen.map (fun x -> x * 5)
        |> Gen.filter (fun x -> x % 3 <> 0)
        |> Arb.fromGen

      let property number = (fizzbuzz number).StartsWith("Buzz")
      Prop.forAll arb property |> Check.QuickThrowOnFailure

    [<Fact>]
    let ``Divisible by 15`` () =
      let arb = Gen.choose (0, 100) |> Gen.map (fun x -> x * 15) |> Arb.fromGen

      let property number =
        (fizzbuzz number).StartsWith("FizzBuzz")

      Prop.forAll arb property |> Check.QuickThrowOnFailure

  module XunitPropertyAttributePattern =

    let nonDivisibleNumberArb =
      Gen.choose (0, 100)
      |> Gen.filter (fun x -> x % 3 <> 0 && x % 5 <> 0)
      |> Arb.fromGen

    type NonDivisibleNumberArb =
      static member NonDivisibleNumber() = nonDivisibleNumberArb

    [<Property(Arbitrary = [| typeof<NonDivisibleNumberArb> |])>]
    let ``Non divisible numbers`` number = (fizzbuzz number) = number.ToString()

    let divisibleByThreeArb =
      Gen.choose (0, 100)
      |> Gen.map (fun x -> x * 3)
      |> Gen.filter (fun x -> x % 5 <> 0)
      |> Arb.fromGen

    type DivisibleByThreeArb =
      static member DivisibleByThree() = divisibleByThreeArb

    [<Property(Arbitrary = [| typeof<DivisibleByThreeArb> |])>]
    let ``Divisible by 3`` number = (fizzbuzz number) = "Fizz"

    let divisibleByFiveArb =
      Gen.choose (0, 100)
      |> Gen.map (fun x -> x * 5)
      |> Gen.filter (fun x -> x % 3 <> 0)
      |> Arb.fromGen

    type DivisibleByFiveArb =
      static member DivisibleByFive() = divisibleByFiveArb

    [<Property(Arbitrary = [| typeof<DivisibleByFiveArb> |])>]
    let ``Divisible by 5`` number = (fizzbuzz number) = "Buzz"

    let divisibleByFifteenArb =
      Gen.choose (0, 100) |> Gen.map (fun x -> x * 15) |> Arb.fromGen

    type DivisibleByFifteenArb =
      static member DivisibleByFifteen() = divisibleByFifteenArb

    [<Property(Arbitrary = [| typeof<DivisibleByFifteenArb> |])>]
    let ``Divisible by 15`` number = (fizzbuzz number) = "FizzBuzz"

(*
The above examples have one major flaw: The generation of test data is closely coupled to the implementation.

Here is a cleaner solution.

Finding these properties is the hard part...

Ported from Mark Seemann's Haskell examples:

https://web.archive.org/web/20240910144730/https://blog.ploeh.dk/2021/06/28/property-based-testing-is-not-the-same-as-partition-testing/
*)
module FizzBuzzingDoneCorrectly =

  let fizzBuzz n =
    match n % 3, n % 5 with
    | 0, 0 -> "FizzBuzz"
    | 0, _ -> "Fizz"
    | _, 0 -> "Buzz"
    | _, _ -> n.ToString()

  let fizzBuzzDeveloperFromHell1 n =
    if n % 15 = 0 then "FizzBuzz" else "2112"

  let fizzBuzzDeveloperFromHell2 n =
    match n % 3, n % 5 with
    | _, 0 -> "FizzBuzz"
    | 0, _ -> "Fizz"
    | _, _ -> "2112"

  let fizzBuzzDeveloperFromHell3 n =
    match n % 3, n % 5 with
    | 0, 0 -> "FizzBuzz"
    | 0, _ -> "Fizz"
    | _, 0 -> "Buzz"
    | _, _ -> "2112"

  [<Property>]
  let ``at least one number in 3 consecutive values`` (i: int) =
    let range = [ i .. i + 2 ]

    let tryToNumber (s: string) =
      match Int32.TryParse(s) with
      | true, value -> Some value
      | false, _ -> None

    let actual =
      range
      // |> List.map fizzBuzzDeveloperFromHell1
      // |> List.map fizzBuzzDeveloperFromHell2
      // |> List.map fizzBuzzDeveloperFromHell3
      |> List.map fizzBuzz
      |> List.map tryToNumber
      |> List.choose id
      |> List.length

    actual >= 1

  [<Property>]
  let ``only one Buzz in 5 consecutive values`` (i: int) =
    let range = [ i .. i + 4 ]

    let actual =
      range
      // |> List.map fizzBuzzDeveloperFromHell2
      // |> List.map fizzBuzzDeveloperFromHell3
      |> List.map fizzBuzz
      |> List.filter _.EndsWith("Buzz")
      |> List.length

    actual = 1

  [<Property>]
  let ``at least one literal Buzz in 10 values`` (i: int) =
    let range = [ i .. i + 9 ]

    let actual =
      range
      // |> List.map fizzBuzzDeveloperFromHell2
      // |> List.map fizzBuzzDeveloperFromHell3
      |> List.map fizzBuzz
      |> List.filter (fun s -> s = "Buzz")
      |> List.length

    actual >= 1

  [<Property>]
  let ``numbers round-trip (there-and-back-again)`` (i: int) =
    let range = [ i .. i + 2 ]

    let tryToNumber (s: string) =
      match Int32.TryParse(s) with
      | true, value -> Some value
      | false, _ -> None

    let actual =
      range
      |> List.map fizzBuzz
      |> List.map tryToNumber
      |> List.choose id
      |> List.forall (fun x -> List.contains x range)

    actual = true

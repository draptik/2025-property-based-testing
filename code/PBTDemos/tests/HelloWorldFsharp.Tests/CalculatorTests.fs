module HelloWorldFsharp.Tests.CalculatorTests

open FsCheck.Xunit

let add a b = a + b
// let add a b = a * b

[<Property>]
let ``add is independent of order (commutativity)`` (a: int, b: int) =
  let result1 = add a b
  let result2 = add b a
  result1 = result2

[<Property>]
let ``add zero is a no-op`` (a: int) =
  let result1 = add a 0
  let result2 = a
  result1 = result2
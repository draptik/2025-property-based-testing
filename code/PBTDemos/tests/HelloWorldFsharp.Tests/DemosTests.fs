module HelloWorldFsharp.Tests.DemosTests

open System
open FsCheck
open FsCheck.Xunit

[<Property>]
let ``sorting a list does not change its size`` (aList: int list) =
  let actual = aList |> List.sort |> List.length
  let expected = aList.Length
  actual = expected

[<Property>]
let ``test oracle example`` (c: int, d: int) =
  let add1 a b = a + b
  let add2 a b = a + b
  let actual1 = add1 c d
  let actual2 = add2 c d
  actual1 = actual2

[<Property>]
let ``adding zero is does not change the input`` (number: int) =
  let add a b = a + b
  let actual = add number 0
  let expected = number
  actual = expected

[<Property>]
let ``nuclear explosion - function does not throw`` (str: NonEmptyString) =
  let fn s = if String.IsNullOrEmpty(s) then failwith "ups" else s
  fn str.Get = str.Get



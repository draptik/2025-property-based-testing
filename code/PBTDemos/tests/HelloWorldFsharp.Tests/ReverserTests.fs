module HelloWorldFsharp.Tests.ReverserTests

open FsCheck.Xunit

[<Property>]
let ``reversing a list twice returns the original list`` (aList: int list) =
  let actual = aList |> List.rev |> List.rev
  let expected = aList
  actual = expected

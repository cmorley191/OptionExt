module OptionExt.Specs.OptionSpec

open NUnit.Framework
open Swensen.Unquote

open OptionExt

[<Test>]
let ``Converts NaNs to Nones, but any other float to Some`` () =
  test
    <@
      Option.ofFloat nan = None
      &&
      Option.ofFloat 5. = Some 5.
    @>

[<Test>]
let ``Collects multiple Options such that any None will make the whole thing None`` () =
  test
    <@
      Option.collect [ Some 1; Some 2; Some 3; Some 4 ] |> Option.map Seq.toList = Some [1 .. 4]
      &&
      Option.collect [ None; Some 2; Some 3; Some 4 ] = None
      &&
      Option.collect [ Some 1; None; Some 3; Some 4 ] = None
      &&
      Option.collect [ Some 1; Some 2; Some 3; None ] = None      
    @>
  
let expectException act = 
  try
    let (Lazy value) = act 
    raise (AssertionException "Expecting an Exception, but none was thrown")
  with
  | ex -> ex  

[<Test>]
let ``Extracts a value from an option, throwing an appropriate error otherwise`` () = 
  test <@ Some 5 |> Option.unless "Expecting to be able to extract a value from a Some Option" = 5 @>

  let ex = 
    expectException (lazy (None |> Option.unless "Can't find"))

  test <@ ex.Message = "Can't find" @>


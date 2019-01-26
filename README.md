[![Build Status](https://travis-ci.com/ntwilson/OptionExt.svg?branch=master)](https://travis-ci.com/ntwilson/OptionExt)

# OptionExt
This library contains additional extensions the F# Option module, as well as a computation expression for Options.

## Computation Expression

OptionExt provides a computation expression available as `option`.

```f#
let result = 
  option { 
    let! x = Some 5
    let! y = Some 10
    return x + y
  }
// returns Some 15

let result = 
  option { 
    let! x = Option.ofFloat a
    let! y = Option.ofFloat b
    return! Option.ofFloat (a / b) //catches 0.0 / 0.0
  }
```



## Extensions

#### `collect`

Lets you convert a `seq<Option<'a>>` to a `Option<seq<'a>>`.  If any of the elements in the sequence are `None`, the entire result is `None`, but if all of the elements are `Some` value, then the entire result is `Some` value that unwraps each element.  Only iterates through the collection once.

```F#
Option.collect [Some 1; Some 2; Some 3; Some 4]
//returns Some [1; 2; 3; 4]

Option.collect [Some 1; Some 2; None; Some 4]
//returns None
```



#### `unless`

Sometimes you end up with a `Option<'a>`, but you really just want the value, and the program should crash if the Option is None. Now you could do this with a regular Match statement:

```F#
let result = 
  match maybe with
  | Some val -> val
  | None -> raise (OptionExpectedException "Something went wrong")
```

But we provide a method just for doing that more conveniently:

```F#
let result = maybe |> Option.unless "Something went wrong"
```

(And if you track code coverage, you don't even need to assemble a test with the error condition to get full code coverage).



#### `ofFloat`

NaN is often used to represent a missing `float` value.  While it's a bit better than `null` since it doesn't raise `NullReferenceException`s, it still causes headaches because there's no way to distinguish between a `float` that definitely has a value and a `float` that may or may not have a value.  The `Option.ofFloat` function simply converts a `float` to an `Option<float>` where NaNs are converted to `None` and everything else is `Some`.

```F#
Option.ofFloat 5.
// returns Some 5.

Option.ofFloat nan 
// returns None
```




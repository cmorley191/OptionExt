module OptionExt.Option

open System

type OptionExpectedException (msg) = 
  inherit Exception (msg)

let unless msg maybe = 
  match maybe with
  | Some x -> x
  | None -> raise (OptionExpectedException msg)

let collect maybes = 
  Seq.fold 
    (fun state element -> 
      match state, element with
      | (Some xs, Some x) -> Some (Seq.append xs [x])
      | _ -> None)
    (Some (upcast []))
    maybes

let ofFloat (x:float) = 
  if Double.IsNaN x 
  then None
  else Some x 

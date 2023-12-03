module Helper

let ReadSample filename =
    System.IO.File.ReadAllLines filename |> Seq.ofArray

let charsToString : char seq -> string =
    Seq.fold (fun acc c -> acc + (c |> string)) ""

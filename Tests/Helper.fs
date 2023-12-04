[<RequireQualifiedAccess>]
module Helper

open System
open System.IO

let readSample filename =
    let inputPath = Path.Combine(__SOURCE_DIRECTORY__, "SampleData" , filename)  
    File.ReadAllLines inputPath |> Seq.ofArray

let charsToString (chars : char seq) : string =
    String(chars |> Array.ofSeq)
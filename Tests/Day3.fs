module Day3

open System.Text.RegularExpressions
open Swensen.Unquote
open Xunit

type PartNumber = { Number: int; X0: int; X1: int; Y: int }
type Symbol = { X: int; Y: int; Symbol: char }

let regexPartNumber = "(\d+)"
let regexSymbol = "([^\d|\.])" // Not a digit or a period

let getPartNumbers lines =
    lines
    |> List.mapi (fun lineIndex line ->
        Regex.Matches(line, regexPartNumber)
        |> Seq.cast<Match>
        |> Seq.map (fun m ->
            { Number = int m.Value; X0 = m.Index; X1 = m.Index + m.Length - 1; Y = lineIndex })
        |> Seq.toList
    )
    |> List.collect id

let getSymbols lines =
    lines
    |> List.mapi (fun lineIndex line ->
        Regex.Matches(line, regexSymbol)
        |> Seq.cast<Match>
        |> Seq.map (fun m ->
            { X = m.Index; Y = lineIndex; Symbol = char m.Value }
        )
        |> Seq.toList
    )
    |> List.collect id

let areAnySymbolsAdjacentToPartNumber (symbols: Symbol list) (partNumber: PartNumber) =
    let isSymbolAdjacentToPartNumber (symbol: Symbol) =
        let isInRange min max value = value >= min - 1 && value <= max + 1
        let isAdjacentX = isInRange partNumber.X0 partNumber.X1 symbol.X
        let isAdjacentY = isInRange partNumber.Y partNumber.Y symbol.Y
        isAdjacentX && isAdjacentY
    symbols |> List.exists isSymbolAdjacentToPartNumber

let getValidPartNumbers (line: string list) =
    let partNumbers = getPartNumbers line
    let symbols = getSymbols line
    
    let validPartNumbers =
        partNumbers |> List.filter (areAnySymbolsAdjacentToPartNumber symbols)
        
    validPartNumbers
    
[<Fact>]
let ``Sample Data`` () =
    // Arrange
    let lines = Helper.readSample "day03-exercise1-sampledata.txt" |> List.ofSeq
    
    // Act
    let validPartNumbers = getValidPartNumbers lines
    let actual = validPartNumbers |> List.sumBy (_.Number)
    
    // Assert
    let expected = 4361
    actual =! expected
    
[<Fact>]
let ``Solution`` () =
    // Arrange
    let lines = Helper.readSample "day03-data.txt" |> List.ofSeq
    
    // Act
    let validPartNumbers = getValidPartNumbers lines
    let actual = validPartNumbers |> List.sumBy (_.Number)
    
    // Assert
    let expected = 509115
    actual =! expected

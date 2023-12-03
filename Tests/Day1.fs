module Day1

open Helper
open System
open Swensen.Unquote
open Xunit

[<Literal>]
let day01SampleDataFile = "./day01-sampledata.txt"

[<Literal>]
let day01Exercise1File = "./day01-exercise1.txt"

let parseLine (line: string) : string =

    let rec getFirstDigit (input: string) : string =
        match input with
        | "" -> ""
        | _ ->
            let firstChar = input[0]
            if Char.IsDigit firstChar then
                firstChar |> string
            else
                getFirstDigit input[1..]
                
    let getLastDigit (input: string) : string =
        input
        |> Seq.rev
        |> charsToString
        |> getFirstDigit

    let firstDigit = getFirstDigit line
    let lastDigit = getLastDigit line
    
    firstDigit + lastDigit
 
[<Theory>]
[<InlineData("1abc2", "12")>]
[<InlineData("pqr3stu8vwx", "38")>]
[<InlineData("a1b2c3d4e5f", "15")>]
[<InlineData("treb7uchet", "77")>]
let ``Parsing a single line works`` (input: string) (expected: string) =
    let actual = parseLine input
    actual =! expected

let addNumbers (numbers: string seq) : string =
    numbers
    |> Seq.map (fun s -> s |> int)
    |> Seq.sum
    |> string

[<Fact>]
let ``Creating the sum of numbers as string works``() =
    // Arrange
    let one = "1"
    let two = "2"
    let input = seq [one; two]
    
    // Act
    let actual = addNumbers input
    
    // Assert
    let expected = "3"
    actual =! expected

[<Fact>]
let ``Day 1 Sample Data``() =
    // Arrange
    let input = ReadSample day01SampleDataFile
    
    // Act
    let actual =
        input
        |> Seq.map parseLine
        |> addNumbers

    // Assert
    let expected = "142"
    actual =! expected
    
[<Fact>]
let ``Day 1 - Exercise 1``() =
    // Arrange
    let input = ReadSample day01Exercise1File
    
    // Act
    let actual =
        input
        |> Seq.map parseLine
        |> addNumbers
    
    // Assert
    let expected = "54927"
    actual =! expected

module Day1

open System
open Swensen.Unquote
open Xunit

module Exercise1 =
        
    [<Literal>]
    let day01Exercise1SampleDataFile = "./day01-sampledata.txt"

    [<Literal>]
    let day01Exercise1File = "./day01-exercise1.txt"

    let parseLine (line: string) : string =

        let rec getFirstDigit (input: string) : string =
            match input with
            | "" ->
                ""
            | _ ->
                let firstChar = input[0]
                if Char.IsDigit firstChar then
                    firstChar |> string
                else
                    getFirstDigit input[1..]
                    
        let getLastDigit (input: string) : string =
            input
            |> Seq.rev
            |> Helper.charsToString
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
    let ``Sample Data``() =
        // Arrange
        let input = Helper.readSample day01Exercise1SampleDataFile
        
        // Act
        let actual =
            input
            |> Seq.map parseLine
            |> addNumbers

        // Assert
        let expected = "142"
        actual =! expected
        
    [<Fact>]
    let ``Solution``() =
        // Arrange
        let input = Helper.readSample day01Exercise1File
        
        // Act
        let actual =
            input
            |> Seq.map parseLine
            |> addNumbers
        
        // Assert
        let expected = "54927"
        actual =! expected

module Exercise2 =
    
    [<Literal>]
    let one = "one"
    [<Literal>]
    let two = "two"
    [<Literal>]
    let three = "three"
    [<Literal>]
    let four = "four"
    [<Literal>]
    let five = "five"
    [<Literal>]
    let six = "six"
    [<Literal>]
    let seven = "seven"
    [<Literal>]
    let eight = "eight"
    [<Literal>]
    let nine = "nine"
    
    let startingLetterDict =
        [
            one
            two
            three
            four
            five
            six
            seven
            eight
            nine
        ]
        |> Seq.groupBy (fun s -> s[0])
        |> Map.ofSeq
    
    let spelledNumberToIntString =
        Map [
            one, "1"
            two, "2"
            three, "3"
            four, "4"
            five, "5"
            six, "6"
            seven, "7"
            eight, "8"
            nine, "9"
        ]
        
    let tryMapSpelledNumberToIntString (input: string) : string option =
        match input with
        | "" ->
            None
        | _ ->
            let firstChar = input[0]
            match startingLetterDict.TryFind firstChar with
            | Some values ->
                if values |> Seq.contains input then
                    match spelledNumberToIntString.TryFind input with
                        | Some x -> Some x
                        | None -> None
                else
                    None
            | None -> None

    [<Theory>]
    [<InlineData("one", "1")>]
    [<InlineData("two", "2")>]
    [<InlineData("three", "3")>]
    [<InlineData("four", "4")>]
    [<InlineData("five", "5")>]
    [<InlineData("six", "6")>]
    [<InlineData("seven", "7")>]
    [<InlineData("eight", "8")>]
    [<InlineData("nine", "9")>]
    let ``Plain string parsing - 1 to 9`` (input: string) (expected: string) =
        let actual = tryMapSpelledNumberToIntString input
        actual =! Some expected
    
    [<Theory(Skip = "TODO")>]
    [<InlineData("two1nine", "29")>]
    [<InlineData("eightwothree", "83")>]
    [<InlineData("abcone2threexyz", "13")>]
    [<InlineData("xtwone3four", "24")>]
    [<InlineData("4nineeightseven2", "42")>]
    [<InlineData("zoneight234", "14")>]
    [<InlineData("7pqrstsixteen", "76")>]
    let ``Parsing a single line works`` (input: string) (expected: string) =
        "TODO" =! expected

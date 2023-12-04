module Day1

open System
open System.Text.RegularExpressions
open Swensen.Unquote
open Xunit

module Exercise1 =
        
    [<Literal>]
    let day01Exercise1SampleDataFile = "day01-exercise1-sampledata.txt"

    [<Literal>]
    let day01File = "day01-data.txt"

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
        let input = Helper.readSample day01File
        
        // Act
        let actual =
            input
            |> Seq.map parseLine
            |> addNumbers
        
        // Assert
        let expected = "54927"
        actual =! expected

/// Exercise 2: I tried to come up with a solution without regex, but failed :-(
/// Original: https://github.com/geodanila/advent_of_code_2023/blob/main/day_1.fsx
module Exercise2 =
    
    [<Literal>]
    let day01Exercise2SampleDataFile = "day01-exercise2-sampledata.txt"
    
    [<Literal>]
    let day01File = "day01-data.txt"
    
    let complexDigitRegex = @"(\d|one|two|three|four|five|six|seven|eight|nine)"

    let getDigitValue (digit: string) =
        match digit.ToLower() with
        | "one" -> 1
        | "two" -> 2
        | "three" -> 3
        | "four" -> 4
        | "five" -> 5
        | "six" -> 6
        | "seven" -> 7
        | "eight" -> 8
        | "nine" -> 9
        | _ -> digit |> int
        
    let findDigit (line: string) regex =
        let digit = Regex.Match(line, regex).Value
        getDigitValue digit
        
    let findLastDigit (line: string) regex =
        let lastDigit = Regex.Match(line, regex, RegexOptions.RightToLeft).Value
        getDigitValue lastDigit
    
    let getFirstAndLastDigits regex (line: string) : (int * int) =
        let firstDigit = findDigit line regex
        let lastDigit = findLastDigit line regex
        (firstDigit, lastDigit)   

    [<Theory>]
    [<InlineData("two1nine", "29")>]
    [<InlineData("eightwothree", "83")>]
    [<InlineData("abcone2threexyz", "13")>]
    [<InlineData("xtwone3four", "24")>]
    [<InlineData("4nineeightseven2", "42")>]
    [<InlineData("zoneight234", "14")>]
    [<InlineData("7pqrstsixteen", "76")>]
    let ``Replace spelled string with ascii string`` (input: string) (expected: string) =
        let actual =
            input
            |> getFirstAndLastDigits complexDigitRegex
            |> fun (first, last) -> first.ToString() + last.ToString()
        
        actual =! expected
    
    [<Fact>]
    let ``Sample Data`` () =
        let actual =
            day01Exercise2SampleDataFile
            |> Helper.readSample
            |> Seq.map (getFirstAndLastDigits complexDigitRegex)
            |> Seq.map (fun (first, last) -> first * 10 + last)
            |> Seq.sum
        
        let expected = 281
        actual =! expected
   
    [<Fact>]
    let ``Solution`` () =
        let actual =
            day01File
            |> Helper.readSample
            |> Seq.map (getFirstAndLastDigits complexDigitRegex)
            |> Seq.map (fun (first, last) -> first * 10 + last)
            |> Seq.sum
        
        let expected = 54581
        actual =! expected

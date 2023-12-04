module Day2

open System.Text.RegularExpressions
open Swensen.Unquote
open Xunit

type Game = {
    Id: int
    Red: int
    Green: int
    Blue: int
}

let idRegex = "Game (\d+):"
let getId regex line = Regex.Match(line, regex).Groups[1].Value |> int

let redRegex = "(\d+) red"
let greenRegex = "(\d+) green"
let blueRegex = "(\d+) blue"
let extractMaxValue line regex =
    Regex.Matches(line, regex)
    |> Seq.map (fun x -> x.Groups.[1].Value |> int)
    |> Seq.max

let parseLine (line: string) : Game =
    let id = getId idRegex line
    let red = extractMaxValue line redRegex
    let green = extractMaxValue line greenRegex
    let blue = extractMaxValue line blueRegex
    {
        Id = id
        Red = red
        Green = green
        Blue = blue
    }

let isGamePossible (game: Game) red green blue =
    game.Red <= red && game.Green <= green && game.Blue <= blue

module Exercise1 =
        
    [<Fact>]
    let ``Parse a single game`` () =
        // Arrange
        let input = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green"
        
        // Act
        let actual = parseLine input
        
        // Assert
        let expected = {
            Id = 1
            Red = 4
            Green = 2
            Blue = 6
        }
        actual =! expected
        
    [<Fact>]
    let ``Parse sample data`` () =
        // Arrange
        let possibleGames =
            Helper.readSample "day02-exercise1-sampledata.txt"
            |> Seq.map parseLine
            |> Seq.filter (fun game -> isGamePossible game 12 13 14)
        
        // Act
        let actual = possibleGames |> Seq.sumBy _.Id   
        
        // Assert
        let expected = 8
        actual =! expected    

    [<Fact>]
    let ``Solution`` () =
        // Arrange
        let possibleGames =
            Helper.readSample "day02-data.txt"
            |> Seq.map parseLine
            |> Seq.filter (fun game -> isGamePossible game 12 13 14)
        
        // Act
        let actual = possibleGames |> Seq.sumBy _.Id   
        
        // Assert
        let expected = 2169
        actual =! expected
        

module Exercise2 =
        
    let power (game: Game) : int = game.Red * game.Green * game.Blue
    
    [<Fact>]
    let ``Parse a single game`` () =
        // Arrange
        let input = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green"
        
        // Act
        let actual = parseLine input |> power
        
        // Assert
        let expected = 48
        actual =! expected
    
    [<Fact>]
    let ``Parse sample data`` () =
        // Act
        let actual =
            Helper.readSample "day02-exercise1-sampledata.txt"
            |> Seq.map (parseLine >> power)
            |> Seq.sum
        
        // Assert
        let expected = 2286
        actual =! expected    

    [<Fact>]
    let ``Solution`` () =
        // Act
        let actual =
            Helper.readSample "day02-data.txt"
            |> Seq.map (parseLine >> power)
            |> Seq.sum
        
        // Assert
        let expected = 60948
        actual =! expected    
              
module Day3

open System.Text.RegularExpressions
open Swensen.Unquote
open Xunit

module Exercise1 =
        
    type PartNumber = { Number: int; X0: int; X1: int; Y: int }
    type Symbol = { X: int; Y: int }

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
                { X = m.Index; Y = lineIndex }
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
        let lines = Helper.readSample "day03-sampledata.txt" |> List.ofSeq
        
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
        
open Exercise1

module Exercise2 =
        
    type PartNumber = { Number: int; X0: int; X1: int; Y: int }
    type Star = { X: int; Y: int }

    let regexPartNumber = "(\d+)"
    let regexStar = "(\*)" // A star

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

    let getStars lines =
        lines
        |> List.mapi (fun lineIndex line ->
            Regex.Matches(line, regexStar)
            |> Seq.cast<Match>
            |> Seq.map (fun m -> { X = m.Index; Y = lineIndex })
            |> Seq.toList)
        |> List.collect id

    let tryGetStar (star: Star) (partNumber: PartNumber) =
        let isInRange min max value = value >= min - 1 && value <= max + 1
        let isAdjacentX = isInRange partNumber.X0 partNumber.X1 star.X
        let isAdjacentY = isInRange partNumber.Y partNumber.Y star.Y
        let isGear = isAdjacentX && isAdjacentY
        if isGear then Some (star, partNumber)
        else None
    
    let gearRatio (partNumbers: PartNumber list) =
        partNumbers |> List.map (_.Number) |> List.reduce (*)
        
    let getGears (line: string list) =
        let partNumbers = getPartNumbers line
        let symbols = getStars line
        
        let gearMapping =
            symbols
            |> List.collect (fun star ->
                partNumbers
                |> List.choose (tryGetStar star))
            |> List.groupBy fst
            |> List.map (fun (star, partNumbersWithStars) ->
                star, List.map snd partNumbersWithStars)
            |> List.choose (fun (star, potentialGears) ->
                if potentialGears.Length > 1 then
                    Some (star, potentialGears |> gearRatio)
                else
                    None)
            |> Map.ofList
            
        gearMapping
        
    [<Fact>]
    let ``Sample Data`` () =
        // Arrange
        let lines = Helper.readSample "day03-sampledata.txt" |> List.ofSeq
        
        // Act
        let actual = lines |> getGears |> Map.values |> List.ofSeq
        let actual' = actual |> List.sum
        
        // Assert
        let expected = [16345; 451490]
        actual =! expected
        
        let expected' = 467835
        actual' =! expected'
        
    [<Fact>]
    let ``Solution`` () =
        // Arrange
        let lines = Helper.readSample "day03-data.txt" |> List.ofSeq
        
        // Act
        let actual =
            lines
            |> getGears
            |> Map.values
            |> List.ofSeq
            |> List.sum
        
        // Assert
        let expected = 75220503
        actual =! expected

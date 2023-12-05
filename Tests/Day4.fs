module Day4

//As far as the Elf has been able to figure out,
// you have to figure out which of the numbers you have appear in the list of winning numbers.
// The first match makes the card worth one point and each match after the first doubles the point value of that card.
//
// For example:
//
// Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
// Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
// Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
// Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
// Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
// Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11
//
// In the above example,
// card 1 has five winning numbers (41, 48, 83, 86, and 17) and eight numbers you have (83, 86, 6, 31, 17, 9, 48, and 53).
// Of the numbers you have, four of them (48, 83, 17, and 86) are winning numbers!
// That means card 1 is worth 8 points (1 for the first match, then doubled three times for each of the three matches after the first).

// Each card has to be parsed.
// A sample card looks like this: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
// The first 5 numbers are the winning numbers, the rest are the numbers you have.
// We need to parse the card and find the matches.
// The first match is worth 1 point, the second match is worth 2 points, the third match is worth 4 points, etc.
// We need to find the matches and then double the points for each match.
// We need to do this for each card and then sum the points for each card.

type Card = { WinningNumbers: int list; MyNumbers: int list }

let cards = [
    { WinningNumbers = [41; 48; 83; 86; 17]; MyNumbers = [83; 86; 6; 31; 17; 9; 48; 53] }
    { WinningNumbers = [13; 32; 20; 16; 61]; MyNumbers = [61; 30; 68; 82; 17; 32; 24; 19] }
    { WinningNumbers = [1; 21; 53; 59; 44]; MyNumbers = [69; 82; 63; 72; 16; 21; 14; 1] }
    { WinningNumbers = [41; 92; 73; 84; 69]; MyNumbers = [59; 84; 76; 51; 58; 5; 54; 83] }
    { WinningNumbers = [87; 83; 26; 28; 32]; MyNumbers = [88; 30; 70; 12; 93; 22; 82; 36] }
    { WinningNumbers = [31; 18; 13; 56; 72]; MyNumbers = [74; 77; 10; 23; 35; 67; 36; 11] }
]



   
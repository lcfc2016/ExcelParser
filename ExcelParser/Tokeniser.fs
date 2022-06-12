module Tokeniser

open System
open System.Text.RegularExpressions
open System.Collections.Generic
open Types

[<Struct>]
type TokenExprs = { regex: Regex; tokType: TokenType }

let numericPattern = @"^[-+]?\d+(?:\.\d+)?(?:[Ee][-+]?[0-9]+)?"
let textPattern = @"^"".*?"""
let boolPattern = @"^(?:true|false)\b(?!\()"
let errorPattern = @"^#(?:null!|div/0!|value!|ref!|name?|num!|n/a|getting_data|spill!|connect!|blocked!|unknown!|field!|calc!)"
let cellRangeExpr = {regex = Regex(@"^\$?[a-z]{1,3}\$?\d+(?::\$?[a-z]{1,3}\$?\d+)+", RegexOptions.IgnoreCase); tokType = CellRange};
let colRangeExpr = {regex = Regex(@"^\$?[a-z]{1,3}:\$?[a-z]{1,3}", RegexOptions.IgnoreCase); tokType = ColRange};
let cellReferenceExpr = {regex = Regex(@"^\$?[a-z]{1,3}\$?[1-9]\d*(?!\w)", RegexOptions.IgnoreCase); tokType = CellReference};

let tokenExprs = [
    // Delimiters
    {regex = Regex(@"^\s+"); tokType = Whitespace};
    {regex = Regex(@"^\("); tokType = LeftBracket};
    {regex = Regex(@"^\)"); tokType = RightBracket};
    {regex = Regex(@"^,"); tokType = Comma};
    {regex = Regex(@"^\{"); tokType = LeftBrace};
    {regex = Regex(@"^\}"); tokType = RightBrace};
    {regex = Regex(@"^;"); tokType = Semicolon};
    // Binary and unary operators
    {regex = Regex(@"^(?:<>|=)", RegexOptions.IgnoreCase); tokType = Equality};
    {regex = Regex(@"^(?:<=|>=|<|>)"); tokType = Comparison};
    {regex = Regex(@"^-"); tokType = Minus};
    {regex = Regex(@"^\+"); tokType = Addition};
    {regex = Regex(@"^&"); tokType = Concatenation};
    {regex = Regex(@"^[*/]"); tokType = Factor};
    {regex = Regex(@"^\^"); tokType = Expt};
    {regex = Regex(@"^%"); tokType = Percentage};
    // Out of sheet reference
    {regex = Regex(@"^(?:'.*?\[.*?\][^\\/?*\[\]]+?'|\[.*?\][^-'*\[\]:/?();{}#""=<>&+^%,\s]+)!"); tokType = FileReference};
    {regex = Regex(@"^(?:'[^\\/?*\[\]]+?'!|[^-'*\[\]:/?();{}#""=<>&+^%,\s]+!)"); tokType = SheetReference};
    // Literals
    {regex = Regex(numericPattern); tokType = Number};
    {regex = Regex(textPattern); tokType = Text};
    {regex = Regex(boolPattern, RegexOptions.IgnoreCase); tokType = Boolean};
    {regex = Regex(errorPattern, RegexOptions.IgnoreCase); tokType = XLError};
    // Functions
    {regex = Regex(@"^_?[\w.]+(?=\()", RegexOptions.IgnoreCase); tokType = FuncToken};
    // References
    cellRangeExpr;
    colRangeExpr;
    cellReferenceExpr;
    {regex = Regex(@"^:"); tokType = Colon};
    {regex = Regex(@"^[a-z_\\][\w\.?]*", RegexOptions.IgnoreCase); tokType = NamedRange}
]

let endToken = {value = "End"; tokenType = End}

// Tokeniser functions

let rec lexer (str: string) (tokens: list<Token>) =
    // Remaining string? If so attempt to parse, if not returns tokens plus end token
    if String.IsNullOrEmpty(str) then tokens @ [endToken]
    else tokenise str tokens 0

and tokenise str tokens index =
    // Check if any patterns left to try, if so, try to match the next pattern in the list, if not error as unrecognised construct found
    if index < tokenExprs.Length
    then
        // If next pattern matches, add matched text plus token type to tokens list and call back to lexer with the remaining unmatched text
        // If next pattern doesn't match, try again with next token in list
        let matched = tokenExprs.[index].regex.Match(str)
        if matched.Success
        then lexer (str.Substring(matched.Length)) (tokens @ [{value = matched.Value; tokenType = tokenExprs.[index].tokType;}])
        else tokenise str tokens (index + 1)
    else
        invalidOp ("Parse error at char: '" + str.Substring(0, 1) + "'")

// Named range tokenising

let tokeniseRange range =
    match range with
    | r when cellRangeExpr.regex.IsMatch(range) -> cellRangeExpr.tokType
    | col when colRangeExpr.regex.IsMatch(range) -> colRangeExpr.tokType
    | cell when cellReferenceExpr.regex.IsMatch(range) -> cellReferenceExpr.tokType
    | _ -> invalidOp ("Attempt to resolve range failed for: " + range)

let processNamedRange (namedRange: string) (namedRanges: Map<string, ParsedNamedRange>) =
    let lowered = namedRange.ToLower()
    // Try to find named range in map of known ranges for file, if it doesn't exist error, otherwise return a tuple of a sheet token and the reference itself
    match Map.tryFind lowered namedRanges with
    | Some range -> (Queue [for subRange in range.ranges -> ({value = range.sheet; tokenType = SheetReference}, {value = subRange; tokenType = tokeniseRange subRange})])
    | _ -> invalidOp ("Unknown/invalid named range: " + namedRange)

// Driver function

let run (str: string) (isFormula: bool) (namedRanges: Map<string, ParsedNamedRange>) =
    // Check if ClosedXML identified this cell as a formula, if so run lexical analysis, if not handle offer to the simplified literal matcher
    if isFormula
    then
        // Tokenise raw formula text
        let rawTokens = List.filter(fun x -> not(x.tokenType.Equals(Whitespace))) (lexer str [])
        // Iterate through tokens, for any that are named ranges attempt to resolve to a specific named range from those found in the file
        let mutable tokens = new Queue<Token>()
        List.iter (fun token ->
                match token.tokenType with
                | NamedRange ->
                    let mutable parsedRanges = processNamedRange token.value namedRanges
                    // Iterate through sub-ranges of matching range, if multiple sub-ranges, add to token list as a union, either way include sheet as part of reference
                    while parsedRanges.Count > 1 do
                        let next = parsedRanges.Dequeue()
                        tokens.Enqueue(fst next)
                        tokens.Enqueue(snd next)
                        tokens.Enqueue({value = ","; tokenType = Comma})
                    let next = parsedRanges.Dequeue()
                    tokens.Enqueue(fst next)
                    tokens.Enqueue(snd next)
                | _ -> tokens.Enqueue(token)
        ) rawTokens
        // Return tokens
        [ for token in tokens -> token ]
    else
        let trimmed = str.Trim()
        [{
            value = trimmed;
            tokenType = match trimmed with
                        // Add date and time literal handling to number, all matches should be of entire cell or revert to text
                        | g when String.IsNullOrEmpty trimmed -> General
                        | num when Regex.IsMatch(num, numericPattern + @"$|^\d\d/\d\d/\d\d\d\d(?:\s+\d\d:\d\d:\d\d)?$") -> Number
                        | bool when Regex.IsMatch(bool, boolPattern + @"$", RegexOptions.IgnoreCase) -> Boolean
                        | error when Regex.IsMatch(error, errorPattern + @"$", RegexOptions.IgnoreCase) -> Error
                        | _ -> Text
        };
        endToken
    ]
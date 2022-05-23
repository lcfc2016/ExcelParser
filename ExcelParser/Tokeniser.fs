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
    // Implicit intersection
    {regex = Regex(@"^@\w+"); tokType = Intersection};
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
    //{regex = Regex(@"^let"); tokType = Let};
    cellRangeExpr;
    colRangeExpr;
    cellReferenceExpr;
    {regex = Regex(@"^:"); tokType = Colon};
    {regex = Regex(@"^[a-z_\\][\w\.?]*", RegexOptions.IgnoreCase); tokType = NamedRange}
]

let endToken = {value = "End"; tokenType = End}

let rec lexer (str: string) (tokens: list<Token>) =
    if String.IsNullOrEmpty(str) then tokens @ [endToken]
    else tokenise str tokens 0

and tokenise str tokens index =
    if index < tokenExprs.Length
        then
            let matched = tokenExprs.[index].regex.Match(str)
            if matched.Success
                then lexer (str.Substring(matched.Length)) (tokens @ [{value = matched.Value; tokenType = tokenExprs.[index].tokType;}])
                else tokenise str tokens (index + 1)
        else
            invalidOp ("Parse error at char: '" + str.Substring(0, 1) + "'")

let tokeniseRange range =
    match range with
    | r when cellRangeExpr.regex.IsMatch(range) -> cellRangeExpr.tokType
    | col when colRangeExpr.regex.IsMatch(range) -> colRangeExpr.tokType
    | cell when cellReferenceExpr.regex.IsMatch(range) -> cellReferenceExpr.tokType
    | _ -> invalidOp ("Attempt to resolve range failed for: " + range)

let processNamedRange (namedRange: string) (namedRanges: Map<string, ParsedNamedRange>) =
    let lowered = namedRange.ToLower()
    match Map.tryFind lowered namedRanges with
    | Some range -> (Queue [for subRange in range.ranges -> ({value = range.sheet; tokenType = SheetReference}, {value = subRange; tokenType = tokeniseRange subRange})])
    | _ -> invalidOp ("Unknown/invalid named range: " + namedRange)

let run (str: string) (isFormula: bool) (namedRanges: Map<string, ParsedNamedRange>) =
    if isFormula
    then
        let rawTokens = List.filter(fun x -> not(x.tokenType.Equals(Whitespace))) (lexer str [])
        let mutable tokens = new Queue<Token>()
        List.iter (fun token ->
                match token.tokenType with
                | NamedRange ->
                    let mutable parsedRanges = processNamedRange token.value namedRanges
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
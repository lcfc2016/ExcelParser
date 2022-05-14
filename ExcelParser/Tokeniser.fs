module Tokeniser

open System
open System.Text.RegularExpressions
open System.Collections.Generic
open Types

[<Struct>]
type TokenExprs = { regex: Regex; tokType: TokenType }

let numericTokenExpr = {regex = Regex(@"^\d+(?:\.\d+)?"); tokType = Number}
let textTokenExpr = {regex = Regex(@"^"".*?"""); tokType = Text}
let boolTokenExpr = {regex = Regex(@"^(?:true|false)\b(?!\()", RegexOptions.IgnoreCase); tokType = Boolean}
let errorTokenExpr = {regex = Regex(@"^#(?:null!|div/0!|value!|ref!|name?|num!|n/a|getting_data|spill!|connect!|blocked!|unknown!|field!|calc!)", RegexOptions.IgnoreCase); tokType = XLError}
let cellRangeExpr = {regex = Regex(@"^\$?[a-z]{0,3}\$?\d+:\$?[a-z]{0,3}\$?\d+", RegexOptions.IgnoreCase); tokType = CellRange};
let colRangeExpr = {regex = Regex(@"^\$?[a-z]{0,3}:\$?[a-z]{0,3}", RegexOptions.IgnoreCase); tokType = ColRange};
let cellReferenceExpr = {regex = Regex(@"^\$?[a-z]{1,3}\$?[1-9]\d*", RegexOptions.IgnoreCase); tokType = CellReference};

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
    numericTokenExpr;
    textTokenExpr;
    boolTokenExpr;
    errorTokenExpr;
    // Functions
    {regex = Regex(@"^(?:_xlfn\.|_xll\.)?[\w.]+(?=\()", RegexOptions.IgnoreCase); tokType = FuncToken};
    // Misc
    //{regex = Regex(@"^let"); tokType = Let};
    cellRangeExpr;
    colRangeExpr;
    cellReferenceExpr;
    {regex = Regex(@"^[a-z_\][\w\.?]*", RegexOptions.IgnoreCase); tokType = NamedRange}
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
    | _ -> invalidOp ("Attempt to resolve range for failed: " + range)

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
    else [{
            value = str;
            tokenType = match str with
                        | num when numericTokenExpr.regex.IsMatch(num) -> Number
                        | bool when boolTokenExpr.regex.IsMatch(bool) -> Boolean
                        | error when errorTokenExpr.regex.IsMatch(error) -> Error
                        | _ -> Text
        };
        endToken
    ]
module Tokeniser

open System
open System.Text.RegularExpressions
open Types

[<Struct>]
type TokenExprs = { regex: Regex; tokType: TokenType }

let numericTokenExpr = {regex = Regex(@"^\d+(?:\.\d+)?"); tokType = Number};
let textTokenExpr = {regex = Regex(@"^"".*?"""); tokType = Text};
let boolTokenExpr = {regex = Regex(@"^(?:true|false)\b", RegexOptions.IgnoreCase); tokType = Boolean};
let errorTokenExpr = {regex = Regex(@"^#(?:null!|div/0!|value!|ref!|name?|num!|n/a|getting_data|spill!|connect!|blocked!|unknown!|field!|calc!)", RegexOptions.IgnoreCase); tokType = XLError};

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
    // Literals
    numericTokenExpr;
    textTokenExpr;
    boolTokenExpr;
    errorTokenExpr;
    // Out of sheet reference
    {regex = Regex(@"^(?:'.*?\[.*?\][^\\/?*\[\]]+?'|\[.*?\][^-'*\[\]:/?();{}#""=<>&+^%,\s]+)!"); tokType = FileReference};
    {regex = Regex(@"^(?:'[^\\/?*\[\]]+?'!|[^-'*\[\]:/?();{}#""=<>&+^%,\s]+!)"); tokType = SheetReference};
    // Functions
    {regex = Regex(@"^(?:_xlfn\.)?ifs(?=\()", RegexOptions.IgnoreCase); tokType = Case};
    {regex = Regex(@"^(?:_xlfn\.|_xll\.)?[\w.]+(?=\()", RegexOptions.IgnoreCase); tokType = FuncToken};
    // Misc
    //{regex = Regex(@"^let"); tokType = Let};
    {regex = Regex(@"^$?[a-z]{0,3}$?\d+:$?[a-z]{0,3}$?\d+", RegexOptions.IgnoreCase); tokType = CellRange};
    {regex = Regex(@"^$?[a-z]{0,3}:$?[a-z]{0,3}", RegexOptions.IgnoreCase); tokType = ColRange};
    {regex = Regex(@"^$?[a-z]+$?[1-9]\d*", RegexOptions.IgnoreCase); tokType = CellReference}
    
]

let endToken = {value = "End"; tokenType = End}

let rec lexer (str: string) (tokens: List<Token>) =
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

let run (str: string) (isFormula: bool) =
    if isFormula
    then List.filter(fun x -> not(x.tokenType.Equals(Whitespace))) (lexer str [])
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
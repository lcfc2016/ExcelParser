module Tokeniser

open System
open System.Text.RegularExpressions
open Types

[<Struct>]
type TokenExprs = { regex: Regex; tokType: TokenType }

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
    {regex = Regex(@"^\d+(?:\.\d+)?"); tokType = Number};
    {regex = Regex(@"^"".*?"""); tokType = Text};
    {regex = Regex(@"^(?:true|false)\b", RegexOptions.IgnoreCase); tokType = Boolean};
    // Cross-sheet reference
    {regex = Regex(@"^(?:'[^\\/?*\[\]]+?'!|[^-'*\[\]:/?();{}#""=<>&+^%,\s]+!)"); tokType = SheetReference};
    // Functions
    {regex = Regex(@"^if(?=\()", RegexOptions.IgnoreCase); tokType = GenericFunction};
    {regex = Regex(@"^ifs(?=\()", RegexOptions.IgnoreCase); tokType = Case};
    {regex = Regex(@"^(?:false|not|true|abs|acos|acosh|acot|acoth|asin|asinh|atan|atan2|atanh|ceiling|cos|cosh|cot|coth|csc|csch|degrees|even|exp|fact|factDouble|floor|gcd|lcm|ln|log|log10|mod|odd|pi|power|quotient|rand|round|sec|sech|sin|sinh|sqrt|tan|tanh|trunc|clean|dollar|exact|find|left|len|lower|mid|right|upper)(?=\()", RegexOptions.IgnoreCase); tokType = Function};
    {regex = Regex(@"^(?:sum|concat|and|or|max|min|average|counta?|product)(?=\()", RegexOptions.IgnoreCase); tokType = SetFunction};
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
                        | num when Regex.IsMatch(num, @"^\d+(?:\.\d+)?$") -> Number
                        | bool when Regex.IsMatch(bool, @"^(?:true|false)$", RegexOptions.IgnoreCase) -> Boolean
                        | _ -> Text
        };
        endToken
    ]
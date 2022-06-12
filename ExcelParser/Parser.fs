module Parser

open Types
open FuncParsers
open System
open System.Collections
open System.Text.RegularExpressions


let rangePattern = @"^\$?([a-z]{0,3})\$?(\d+)(?::\$?[a-z]{0,3}\$?\d+)*:\$?([a-z]{0,3})\$?(\d+)$"

let mutable tokens = new Generic.Queue<Token>()

let expect tokenType =
    if (tokens.Peek()).tokenType = tokenType
        then ignore (tokens.Dequeue())
        else invalidOp ("Expected: " + tokenType.ToString()
            + ", Got: " + (tokens.Peek()).tokenType.ToString()
            + ", At: " + (tokens.Peek()).value)

let isLiteral (token: Token) =
    match token.tokenType with
    | General
    | Boolean
    | Number
    | Text
    | XLError -> true
    | _ -> false

let parseLiteral (token: Token) =
    // Remove quotes unless literal empty string
    let value = if Regex.IsMatch(token.value, @"^""\s*""$") then token.value else token.value.Trim('"')
    Constant ( value,
        match token.tokenType with
        | General ->    SimpleType (TypeEnum.General)
        | Text ->       SimpleType (TypeEnum.Str)
        | Number ->     SimpleType (TypeEnum.Numeric)
        | Boolean ->    SimpleType (TypeEnum.Bool)
        | XLError ->    SimpleType (TypeEnum.Error)
        | _ -> invalidOp ("Parse error at " + token.value))

let rec parseExpr prec expectUnion =
    // Algorithm as per https://www.engr.mun.ca/~theo/Misc/exp_parsing.htm#climbing with additions for postfix ops and union context
    let rec iter expr =
        // Check if next token is post-fix, if so dequeue and use it as if it were a unary prefix operator
        let expr = if isPostfix (tokens.Peek())
                   then Node ( (Unary ((parsePostfixOp (tokens.Dequeue())), expr)))
                   else expr
        // If in union context (i.e. called from set function parse) dequeue comma if any, add parsed val into union and recurse
        if expectUnion && tokens.Peek().tokenType = Comma
        then
            expect Comma
            Union ( [expr] @ [parseExpr 0 expectUnion])
        // Otherwise, if next token is a binary operator and >= than current precision (though see below) continue to build right sub-tree, otherwise return current sub-tree
        elif isBinary (tokens.Peek()) && getBinOpPrec (tokens.Peek()) >= prec
        then
            let op = tokens.Dequeue()
            // Check if op right associative, if not bump precedence by 1, ensures that a^b^c is parsed correctly, i.e. as a^(b^c), whilst a/b/c is parsed as (a/b)/c
            let right = parseExpr (getBinOpPrec op + (if (rightAssoc op) then 0 else 1)) false
            iter (Node (Binary ((parseBinaryOp op), expr, right )))
        else expr
    parseVal expectUnion |> iter

and parseVal expectUnion =
    match tokens.Dequeue() with
    | tok when isUnary tok ->
        Node ( Unary ((parseUnaryOp tok), (parseExpr unaryPrecedence false)))
    | tok when tok.tokenType = LeftBracket ->
        let expr = parseExpr 0 expectUnion
        expect RightBracket
        expr
    | tok when tok.tokenType = LeftBrace ->
        Values ( parseLiteralArray () )
    | tok when isLiteral tok ->
        Leaf (parseLiteral tok)
    | tok when tok.tokenType = CellReference ->
        // Remove absolute reference symbol as irrelevant and complicates lookups in type check stage
        Leaf (Reference (tok.value.Replace("$", "")))
    | tok when tok.tokenType = FuncToken ->
        expect LeftBracket
        let f = parseFunc tok
        // Check function type as parsed differently, union context needed for set functions (SUM, etc), min vs max arity for regular functions, etc
        match f with
        | FixedArity f -> 
            let args = if tokens.Peek().tokenType <> RightBracket then parseList f.repr else []
            expect RightBracket
            if f.minArity <= args.Length && args.Length <= f.maxArity()
                then Node ( Func (f, args))
                else invalidOp (f.repr + " expects " + f.inputs.Length.ToString() + " inputs, got " + args.Length.ToString())
        | Variadic f ->
            let args =
                match parseExpr 0 true with
                | Union union -> union
                | e -> [e]
            expect RightBracket
            if args.Length > 0
            then Node ( SetFunc (f, args))
            else invalidOp ("No arguments provided to function: " + f.repr)
        | Generic f ->
            let args = if tokens.Peek().tokenType <> RightBracket then parseList f.repr else []
            expect RightBracket
            if f.minimumClauses() <= args.Length && args.Length <= f.maximumClauses()
            then Node ( GenericFunc (f, args))
            else invalidOp (f.repr + " expects between " + f.minimumClauses().ToString() + " and " + f.maximumClauses().ToString() + " arguments, got " + args.Length.ToString())
        // Special case SWITCH/IFS as parsed in a unique way
        | Switch ->
            let args = if tokens.Peek().tokenType <> RightBracket then parseList "SWITCH" else []
            expect RightBracket
            if 3 <= args.Length && args.Length <= 253
            then Node ( SwitchFunc args )
            else invalidOp ("SWITCH expects between 3 and 253 arguments, got " + args.Length.ToString())
        | Ifs ->
            let args = if tokens.Peek().tokenType <> RightBracket then parseList "IFS" else []
            expect RightBracket
            if 2 <= args.Length && args.Length <= 254 && (args.Length % 2 = 0)
            then Node ( IfsFunc args )
            else invalidOp ("IFS expects between 2 and 254 arguments, got " + args.Length.ToString())
    | tok when tok.tokenType = SheetReference ->
        // Trim extraneous punctuation to get sheet name, parse reference in specific function
        Leaf (Sheet (tok.value.Trim('!').Trim(''').Replace("''", "'"), parseRef tok.value))
    // Derive limits for the range, as column range, hard coded row numbers, remove absolute reference symbol
    | tok when tok.tokenType = ColRange ->
        let cols = [ for cell in tok.value.Split(':') -> cell.Replace("$", "") ]
        Leaf (Value.Range (1, 1048576, cols.[0], cols.[1]) )
    // Derive column and row limits for range, remove absolute reference symbol
    | tok when tok.tokenType = CellRange ->
        let elements = Regex.Match(tok.value, rangePattern, RegexOptions.IgnoreCase).Groups;
        Leaf (Value.Range (int32(elements.Item(2).Value), int32(elements.Item(4).Value), elements.Item(1).Value, elements.Item(3).Value) )
    // Error conditions and unimplemented features
    | tok when tok.tokenType = FileReference ->
        invalidOp ("External file reference: " + tok.value)
    | value -> invalidOp ("Error at " + value.value + ", " + tokens.Peek().value)

and parseLiteralArray () =
    let rec recur rows current =
        match tokens.Dequeue() with
        | tok when isUnary tok ->
            // If unary operator this should just be a numeric literal (though cannot be in date format), parse as such
            match tok.tokenType with
            | Minus
            | Addition ->
                match tokens.Dequeue() with
                | num when num.tokenType = Number -> recur rows current @ [parseLiteral { value = tok.value + num.value; tokenType = Number }]
                | t -> invalidOp("Unparsed literal in array: " + tok.value + ", " + t.value)
            | _ -> invalidOp("Unexpected unary operator in literal array: " + tok.value)
        | tok when isLiteral tok ->
            recur rows current @ [parseLiteral tok]
        | tok when tok.tokenType = Comma ->
            recur rows current
        | tok when tok.tokenType = Semicolon ->
            // End of row, add current row to existing rows and attempt to parse a new row
            recur (rows @ [current]) []
        | tok when tok.tokenType = RightBrace ->
            // End of literal array, if it's a literal check it's not a jagged array as that's invalid in Excel
            let dimension = List.length current
            if List.forall (fun lst -> List.length lst = dimension) rows
            then List.concat (rows @ [current])
            else invalidOp ("2D array with uneven dimensions")
        | tok -> invalidOp ("Error at " + tok.value + ", " + tokens.Peek().value)
    let result = recur [] []
    match result with
    | [] -> invalidOp ("Empty set near " + tokens.Peek().value)
    | _ -> List.rev result

and parseList (fName: string) =
    let args = [parseExpr 0 false]
    match tokens.Peek().tokenType with
    | Comma ->
        while tokens.Peek().tokenType = Comma do
            ignore (tokens.Dequeue())
        match tokens.Peek().tokenType with
        | RightBracket -> args
        | _ -> args @ parseList fName
    | RightBracket -> args
    | _ -> invalidOp ("Error in parameters of " + fName + ", at: " + tokens.Peek().value)

and parseRef sheetName =
    // As per reference parsing in parseVal, but handle "sheet!cell:sheet!cell" style constructs
    match tokens.Dequeue() with
    | tok when tok.tokenType = CellReference ->
        match tokens.Peek().tokenType with
        | Colon ->
            expect Colon
            match tokens.Dequeue() with
            | s when s.tokenType = SheetReference && String.Equals(sheetName, s.value, StringComparison.OrdinalIgnoreCase) ->
                match tokens.Dequeue() with
                | t2 when t2.tokenType = CellReference ->
                    let range = tok.value + ":" + t2.value
                    let elements = Regex.Match(range, rangePattern, RegexOptions.IgnoreCase).Groups
                    Value.Range (int32(elements.Item(2).Value), int32(elements.Item(4).Value), elements.Item(1).Value, elements.Item(3).Value)
                | t2 -> invalidOp ("Invalid cross-sheet range at: " + tok.value + ":" + s.value + t2.value)
            | s -> invalidOp ("Unparsed cross-sheet range at: " + tok.value + ":" + s.value)
        | _ -> Reference (tok.value.Replace("$", ""))
    | tok when tok.tokenType = ColRange ->
        let cols = [ for cell in tok.value.Split(':') -> cell.Replace("$", "") ]
        Value.Range (1, 1048576, cols.[0], cols.[1])
    | tok when tok.tokenType = CellRange ->
        let elements = Regex.Match(tok.value, rangePattern, RegexOptions.IgnoreCase).Groups
        Value.Range (int32(elements.Item(2).Value), int32(elements.Item(4).Value), elements.Item(1).Value, elements.Item(3).Value)
    | _ -> invalidOp ("Expected cell or range following reference to sheet " + sheetName.TrimEnd('!'))

let parse (tokenList: List<Token>) =
    tokens <- (Generic.Queue tokenList)
    let result = parseExpr 0 false
    expect End
    result

let run (tokenList: List<Token>) =
    parse tokenList

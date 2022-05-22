module Parser

open Types
open FuncParsers
open System
open System.Collections
open System.Text.RegularExpressions


let mutable tokens = new Generic.Queue<Token>()

let expect tokenType =
    if (tokens.Peek()).tokenType = tokenType
        then ignore (tokens.Dequeue())
        else invalidOp ("Expected: " + tokenType.ToString()
            + ", Got: " + (tokens.Peek()).tokenType.ToString()
            + ", At: " + (tokens.Peek()).value)

let isLiteral (token: Token) =
    match token.tokenType with
    | Boolean
    | Number
    | Text
    | XLError -> true
    | _ -> false

let parseLiteral (token: Token) =
    // Check if token string is double-quoted, if so remove quotes
    let value = if token.value = @"""""" then token.value else token.value.Trim('"')
    Constant ( value,
        match token.tokenType with
        | Text ->       SimpleType (TypeEnum.Str)
        | Number ->     SimpleType (TypeEnum.Numeric)
        | Boolean ->    SimpleType (TypeEnum.Bool)
        | XLError ->    SimpleType (TypeEnum.Error)
        | _ -> invalidOp ("Parse error at " + token.value))

let rec parseExpr prec expectUnion =
    let rec iter expr =
        let expr = if isPostfix (tokens.Peek())
                   then Node ( (Unary ((parsePostfixOp (tokens.Dequeue())), expr)))
                   else expr
        if expectUnion && tokens.Peek().tokenType = Comma
        then
            expect Comma
            Union ( [expr] @ [parseExpr 0 expectUnion])
        elif isBinary (tokens.Peek()) && getBinOpPrec (tokens.Peek()) >= prec
        then
            let op = tokens.Dequeue()
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
        Leaf (Reference (tok.value.Replace("$", "")))
    | tok when tok.tokenType = FuncToken ->
        expect LeftBracket
        let f = parseFunc tok
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
        Leaf (Sheet (tok.value.Trim('!').Trim('''), parseRef tok.value))
    | tok when tok.tokenType = FileReference ->
        invalidOp ("External sheet reference: " + tok.value)
    | tok when tok.tokenType = ColRange ->
        let cols = [ for cell in tok.value.Split(':') -> cell.Replace("$", "") ]
        Leaf (Value.Range (1, 1048576, cols.[0], cols.[1]) )
    | tok when tok.tokenType = CellRange ->
        let elements = Regex.Match(tok.value, @"\$?([a-z]{0,3})\$?(\d+):\$?([a-z]{0,3})\$?(\d+)", RegexOptions.IgnoreCase).Groups;
        Leaf (Value.Range (int32(elements.Item(2).Value), int32(elements.Item(4).Value), elements.Item(1).Value, elements.Item(3).Value) )
    | value -> invalidOp ("Error at " + value.value + ", " + tokens.Peek().value)

and parseLiteralArray () =
    let rec recur rows current =
        match tokens.Dequeue() with
        | tok when isLiteral tok ->
            recur rows current @ [parseLiteral (tokens.Dequeue())]
        | tok when tok.tokenType = Comma ->
            recur rows current
        | tok when tok.tokenType = RightBrace ->
            let dimension = List.length current
            if List.forall (fun lst -> List.length lst = dimension) rows
            then List.concat (rows @ current)
            else invalidOp ("2D array with uneven dimensions")
        | tok -> invalidOp ("Error at " + tok.value + ", " + tokens.Peek().value)
    let result = recur [] []
    match result with
    | [] -> invalidOp ("Empty set near " + tokens.Peek().value)
    | _ -> result

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
                    let elements = Regex.Match(range, @"\$?([a-z]{1,3})\$?(\d+):\$?([a-z]{1,3})\$?(\d+)", RegexOptions.IgnoreCase).Groups
                    Value.Range (int32(elements.Item(2).Value), int32(elements.Item(4).Value), elements.Item(1).Value, elements.Item(3).Value)
                | t2 -> invalidOp ("Invalid cross-sheet range at: " + tok.value + ":" + s.value + t2.value)
            | s -> invalidOp ("Unparsed cross-sheet range at: " + tok.value + ":" + s.value)
        | _ -> Reference (tok.value.Replace("$", ""))
    | tok when tok.tokenType = ColRange ->
        let cols = [ for cell in tok.value.Split(':') -> cell.Replace("$", "") ]
        Value.Range (1, 1048576, cols.[0], cols.[1])
    | tok when tok.tokenType = CellRange ->
        let elements = Regex.Match(tok.value, @"\$?([a-z]{1,3})\$?(\d+):\$?([a-z]{1,3})\$?(\d+)", RegexOptions.IgnoreCase).Groups
        Value.Range (int32(elements.Item(2).Value), int32(elements.Item(4).Value), elements.Item(1).Value, elements.Item(3).Value)
    | _ -> invalidOp ("Expected cell or range following reference to sheet " + sheetName)

let parse (tokenList: List<Token>) =
    tokens <- (Generic.Queue tokenList)
    let result = parseExpr 0 false
    expect End
    result

let run (tokenList: List<Token>) =
    parse tokenList

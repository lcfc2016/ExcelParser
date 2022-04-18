module Parser

open Types
open FuncParsers
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
    Constant ( token.value.Trim('"'),
        match token.tokenType with
        | Text ->       SimpleType (TypeEnum.Str)
        | Number ->     SimpleType (TypeEnum.Numeric)
        | Boolean ->    SimpleType (TypeEnum.Bool)
        | XLError ->    SimpleType (TypeEnum.Error)
        | _ -> invalidOp ("Parse error at " + token.value))

let rec parseExpr prec =
    let rec iter expr =
        let expr = if isPostfix (tokens.Peek())
                   then Node ( (Unary ((parsePostfixOp (tokens.Dequeue())), expr)))
                   else expr
        if isBinary (tokens.Peek()) && getBinOpPrec (tokens.Peek()) >= prec
        then
            let op = tokens.Dequeue()
            let right = parseExpr (getBinOpPrec op + (if (rightAssoc op) then 0 else 1))
            iter (Node (Binary ((parseBinaryOp op), expr, right )))
        else expr
    parseVal () |> iter

and parseVal () =
    match tokens.Dequeue() with
    | tok when isUnary tok ->
        Node ( Unary ((parseUnaryOp tok), (parseExpr unaryPrecedence)))
    | tok when tok.tokenType = LeftBracket ->
        let expr = parseExpr 0
        expect RightBracket
        expr
    | tok when tok.tokenType = LeftBrace ->
        let args = Values ( parseLiteralArray () )
        expect RightBrace
        args
    | tok when isLiteral tok ->
        Leaf (parseLiteral tok)
    | tok when tok.tokenType = CellReference ->
        Leaf (Reference (tok.value))
    | tok when tok.tokenType = FuncToken ->
        expect LeftBracket
        let args = (if tokens.Peek().tokenType <> RightBracket
                    then parseList ()
                    else [])
        expect RightBracket
        match parseFunc tok with
        | FixedArity f -> 
            if f.inputs.Length <> args.Length
                then invalidOp (f.repr + " expects " + f.inputs.Length.ToString() + " inputs, got " + args.Length.ToString())
                else Node ( Func (f, args))
        | Variadic f ->
            Node ( SetFunc (f, args))
        | Generic f ->
            if f.numberOfClauses() <> args.Length
            then invalidOp (f.repr + " expects " + f.numberOfClauses().ToString() + " clauses, got " + args.Length.ToString())
            else Node ( GenericFunc (f, args))
    | tok when tok.tokenType = Case ->
        expect LeftBracket
        let clauses = parseClause []
        Node ( CaseStatement clauses)
    | tok when tok.tokenType = SheetReference ->
        Leaf (Sheet (tok.value.Trim('!').Trim('''), parseRef tok.value))
    | tok when tok.tokenType = ColRange ->
        let cols = [ for cell in tok.value.Split(':') -> cell.Replace("$", "") ]
        Leaf (Range (1, 1048576, cols.[0], cols.[1]) )
    | tok when tok.tokenType = CellRange ->
        let elements = Regex.Match(tok.value, @"$?([a-z]{0,3})$?(\d+):$?([a-z]{0,3})$?(\d+)", RegexOptions.IgnoreCase).Groups;
        Leaf (Range (int32(elements.Item(2).Value), int32(elements.Item(4).Value), elements.Item(1).Value, elements.Item(3).Value) )
    | value -> invalidOp ("Error at " + value.value)

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
        | tok -> invalidOp ("Error at " + tok.value)
    recur [] []

and parseList () =
    let args = [parseExpr 0]
    match tokens.Peek().tokenType with
    | Comma ->
        ignore (tokens.Dequeue())
        args @ parseList ()
    | RightBracket -> args
    | _ -> invalidOp ("Error at " + tokens.Peek().value)

and parseClause clauses =
    let clause = parseExpr 0
    expect Comma
    let result = parseExpr 0
    match tokens.Dequeue().tokenType with
    | Comma -> parseClause (clauses @ [{cond = clause; result = result}])
    | RightBracket -> clauses @ [{cond = clause; result = result}]
    | _ -> invalidOp ("Error in case statement at " + tokens.Peek().value)

and parseRef sheetName =
    match tokens.Dequeue() with
    | tok when tok.tokenType = CellReference ->
        Reference (tok.value)
    | tok when tok.tokenType = ColRange ->
        let cols = [ for cell in tok.value.Split(':') -> cell.Replace("$", "") ]
        Range (1, 1048576, cols.[0], cols.[1])
    | tok when tok.tokenType = CellRange ->
        let elements = Regex.Match(tok.value, @"$?([a-z]{0,3})$?(\d+):$?([a-z]{0,3})$?(\d+)", RegexOptions.IgnoreCase).Groups;
        Range (int32(elements.Item(2).Value), int32(elements.Item(4).Value), elements.Item(1).Value, elements.Item(3).Value)
    | _ -> invalidOp ("Expected cell or range following reference to sheet " + sheetName)

let parse (tokenList: List<Token>) =
    tokens <- (Generic.Queue tokenList)
    let result = parseExpr 0
    expect End
    result

let run (tokenList: List<Token>) =
    parse tokenList

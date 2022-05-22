module Types

open System

type TokenType =
    // Delimiters
    | Whitespace
    | LeftBracket
    | RightBracket
    | LeftBrace
    | RightBrace
    | Comma
    | Colon
    | Semicolon
    // Function forms
    | FuncToken
    // Binary operators
    | Equality
    | Comparison
    | Concatenation
    | Minus
    | Addition
    | Factor
    | Expt
    // Literals
    | Number
    | Text
    | Boolean
    | XLError
    // Misc
    //| Let
    | Intersection
    | Percentage
    | FileReference
    | SheetReference
    | CellReference
    | CellRange
    | ColRange
    | NamedRange
    | UnaryOp
    | Error
    | End

type Token = { value: string; tokenType: TokenType }

//type AST = Expr of Expression
//    | Statement of Assignment
//    | Expr of Expression

//and Assignment = { id: string; value: Expression }

type XLType =
    | ComplexType of Set<TypeEnum>
    | SimpleType of TypeEnum

    member this.print () =
        match this with
        | ComplexType c -> "(" + (String.concat "/" (Set.map (fun (input: TypeEnum) -> input.ToString()) c)) + ")"
        | SimpleType s -> TypeEnum.GetName(s)
   
and TypeEnum =
    | General = 0
    | Bool = 1
    | Numeric = 2
    | Str = 3
    | Error = 4

type XLFunc =
    | FixedArity of Function
    | Variadic of SetFunction
    | Generic of GenericFunction
    | Switch
    | Ifs

and Function =
    { inputs: List<XLType>; output: XLType; minArity: int; repr: String }
    member this.maxArity() = this.inputs.Length
    member this.inputList(want) = String.concat "," (List.map (fun (input: XLType) -> input.print() ) (List.take want this.inputs))

and SetFunction = { input: XLType; output: XLType; repr: String }

and GenericFunction =
    { inputs: List<XLType>; minimumOutputs: Int32; maximumOutputs: Int32; repr: String }
    member this.minimumClauses() = this.inputs.Length + this.minimumOutputs
    member this.maximumClauses() = this.inputs.Length + this.maximumOutputs
    member this.inputList() = String.concat "," (List.map (fun (input: XLType) -> input.print() ) this.inputs)

and Expression =
    | Leaf of Value
    | Node of SubExpr
    | Values of Array
    | Union of ExpList

and SubExpr =
    | Unary of func:Function * arg:Expression
    | Binary of func:Function * left:Expression * right:Expression
    | Func of func:Function * args:List<Expression>
    | SetFunc of func:SetFunction * args:List<Expression>
    | GenericFunc of func:GenericFunction * args:List<Expression>
    | SwitchFunc of args:List<Expression>
    | IfsFunc of args:List<Expression>

and Array = List<Value>

and ExpList = List<Expression>

and Value =
    | Reference of name:String
    | Sheet of name:String * value:Value
    | Constant of value:String * xlType:XLType
    | Range of minRow:int * maxRow:int * minCol:String * maxCol:String

//    | Str of string
//    | Bool of bool
//    | Real of double

type Cell =
    | Unparsed of UnparsedCell
    | Parsed of ParsedCell
//    | TypeChecked of TypeCheckedCell

and UnparsedCell = { address: String; column: String; row: int; value: String; isFormula: bool }
and ParsedCell =
    | Success of CompletedParse
    | Failure of FailedParse
and CompletedParse = { column: String; row: int; ast: Expression }
and FailedParse = string

type TypeStatus =
    | Match = 0
    | PartialHandling = 1
    | Mismatch = 2

type Error = { sheet: string; cell: string; f: string; expected: string; actual: string; errorType: string; errorMessage: string }

type ErrorFormat =
    | PrettyPrint
    | CSV

type ParsedNamedRange = { sheet: string; ranges: List<string> }

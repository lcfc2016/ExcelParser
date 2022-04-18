module FuncParsers

open System
open Types
open BinaryOperators
open UnaryOperators
open SetFunctions
open Functions
open GenericFunctions


let isBinary (token: Token) =
    match token.tokenType with
    | Equality
    | Comparison
    | Concatenation
    | Minus
    | Addition
    | Factor
    | Expt -> true
    | _ -> false

let rightAssoc (token: Token) =
    match token.tokenType with
    | Expt -> true
    | _ -> false

let getBinOpPrec (token: Token) =
    match token.tokenType with
    | Equality
    | Comparison -> 0
    | Concatenation -> 1
    | Addition
    | Minus -> 2
    | Factor -> 3
    | Expt -> 4
    // Post fix precedence
    // Unary op precedence is here, defined below
    | _ -> invalidOp ("Parse error at " + token.value)

let postFixPrecedence = 5
let unaryPrecedence = 6

let parseBinaryOp (token: Token) =
    match token.value with
    | "+" -> add
    | "-" -> sub
    | "*" -> mult
    | "/" -> div
    | "^" -> expt
    | "&" -> binConcat
    | ">" -> gt
    | "<" -> lt
    | ">=" -> gte
    | "<=" -> lte
    | "=" -> equality
    | "<>" -> inequality
    | _ -> invalidOp ("Parse error at " + token.value)

let isUnary (token: Token) =
    match token.tokenType with
    | Minus
    | Addition
    | UnaryOp -> true
    | _ -> false

let parseUnaryOp (token: Token) =
    match token.value.ToLower() with
    | "-" -> negative
    | "+" -> positive
    | _ -> invalidOp ("Parse error at " + token.value)

let isPostfix (token: Token) =
    match token.tokenType with
    | Percentage -> true
    | _ -> false

let parsePostfixOp (token: Token) =
    match token.value.ToLower() with
    | "%" -> percentage
    | _ -> invalidOp ("Parse error at " + token.value)

let lowerAndRemovePrefix (string: String) =
    (if string.Contains("_xlfn.", StringComparison.CurrentCultureIgnoreCase)
    then string.Substring(6)
    else string).ToLower()

let parseFunc (token: Token) =
    match lowerAndRemovePrefix token.value with
    // Logical
    | "false" -> (FixedArity false')
    | "not" -> (FixedArity not)
    | "true" -> (FixedArity true')
    // Math and Trig
    | "abs" -> (FixedArity abs)
    | "acos" -> (FixedArity acos)
    | "acosh" -> (FixedArity acosh)
    | "acot" -> (FixedArity acot)
    | "acoth" -> (FixedArity acoth)
    | "asin" -> (FixedArity asin)
    | "asinh" -> (FixedArity asinh)
    | "atan" -> (FixedArity atan)
    | "atan2" -> (FixedArity atan2)
    | "atanh" -> (FixedArity atanh)
    | "ceiling" -> (FixedArity ceiling)
    | "cos" -> (FixedArity cos)
    | "cosh" -> (FixedArity cosh)
    | "cot" -> (FixedArity cot)
    | "coth" -> (FixedArity coth)
    | "csc" -> (FixedArity csc)
    | "csch" -> (FixedArity csch)
    | "degrees" -> (FixedArity degrees)
    | "even" -> (FixedArity even)
    | "exp" -> (FixedArity exp)
    | "fact" -> (FixedArity fact)
    | "factDouble" -> (FixedArity factDouble)
    | "floor" -> (FixedArity floor)
    | "gcd" -> (FixedArity gcd)
    | "lcm" -> (FixedArity lcm)
    | "ln" -> (FixedArity ln)
    | "log" -> (FixedArity log)
    | "log10" -> (FixedArity log10)
    | "mod" -> (FixedArity mod')
    | "odd" -> (FixedArity odd)
    | "pi" -> (FixedArity pi)
    | "power" -> (FixedArity power)
    | "quotient" -> (FixedArity quotient)
    | "rand" -> (FixedArity rand)
    | "round" -> (FixedArity round)
    | "sec" -> (FixedArity sec)
    | "sech" -> (FixedArity sech)
    | "sin" -> (FixedArity sin)
    | "sinh" -> (FixedArity sinh)
    | "sqrt" -> (FixedArity sqrt)
    | "tan" -> (FixedArity tan)
    | "tanh" -> (FixedArity tanh)
    | "trunc" -> (FixedArity trunc)
    // Text
    | "clean" -> (FixedArity clean)
    | "dollar" -> (FixedArity dollar)
    | "exact" -> (FixedArity exact)
    | "find" -> (FixedArity find)
    | "left" -> (FixedArity left)
    | "len" -> (FixedArity len)
    | "lower" -> (FixedArity lower)
    | "mid" -> (FixedArity mid)
    | "right" -> (FixedArity right)
    | "upper" -> (FixedArity upper)
    // Information
    | "cell" -> (FixedArity cell)
    | "error.type" -> (FixedArity errorType)
    | "info" -> (FixedArity info)
    | "isblank" -> (FixedArity isBlank)
    | "iserr" -> (FixedArity isErr)
    | "iserror" -> (FixedArity isError)
    | "iseven" -> (FixedArity isEven)
    | "isformula" -> (FixedArity isFormula)
    | "islogical" -> (FixedArity isLogical)
    | "isna" -> (FixedArity isNa)
    | "isnontext" -> (FixedArity isNontext)
    | "isnumber" -> (FixedArity isNumber)
    | "isodd" -> (FixedArity isOdd)
    | "isref" -> (FixedArity isRef)
    | "istext" -> (FixedArity isText)
    | "n" -> (FixedArity n)
    | "na" -> (FixedArity na)
    | "sheet" -> (FixedArity sheet)
    | "sheets" -> (FixedArity sheets)
    | "type" -> (FixedArity type')
    // Set Functions
    | "and" -> (Variadic and')
    | "or" -> (Variadic or')
    | "xor" -> (Variadic xor)
    | "average" -> (Variadic average)
    | "count" -> (Variadic count)
    | "counta" -> (Variadic counta)
    | "max" -> (Variadic max)
    | "min" -> (Variadic min)
    | "product" -> (Variadic product)
    | "sum" -> (Variadic sum)
    | "concat" -> (Variadic concat)
    // Generic functions
    | "if" -> (Generic if')
    | _ -> invalidOp ("Parse error at " + token.value)
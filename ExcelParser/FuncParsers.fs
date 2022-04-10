module FuncParsers

open Types
open BinaryOperators
open UnaryOperators
open SetFunctions
open Functions


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

let parseFunc (token: Token) =
    match token.value.ToLower() with
    | "false" -> false'
    | "not" -> not
    | "true" -> true'
    | "abs" -> abs
    | "acos" -> acos
    | "acosh" -> acosh
    | "acot" -> acot
    | "acoth" -> acoth
    | "asin" -> asin
    | "asinh" -> asinh
    | "atan" -> atan
    | "atan2" -> atan2
    | "atanh" -> atanh
    | "ceiling" -> ceiling
    | "cos" -> cos
    | "cosh" -> cosh
    | "cot" -> cot
    | "coth" -> coth
    | "csc" -> csc
    | "csch" -> csch
    | "degrees" -> degrees
    | "even" -> even
    | "exp" -> exp
    | "fact" -> fact
    | "factDouble" -> factDouble
    | "floor" -> floor
    | "gcd" -> gcd
    | "lcm" -> lcm
    | "ln" -> ln
    | "log" -> log
    | "log10" -> log10
    | "mod" -> mod'
    | "odd" -> odd
    | "pi" -> pi
    | "power" -> power
    | "quotient" -> quotient
    | "rand" -> rand
    | "round" -> round
    | "sec" -> sec
    | "sech" -> sech
    | "sin" -> sin
    | "sinh" -> sinh
    | "sqrt" -> sqrt
    | "tan" -> tan
    | "tanh" -> tanh
    | "trunc" -> trunc
    | "clean" -> clean
    | "dollar" -> dollar
    | "exact" -> exact
    | "find" -> find
    | "left" -> left
    | "len" -> len
    | "lower" -> lower
    | "mid" -> mid
    | "right" -> right
    | "upper" -> upper
    | _ -> invalidOp ("Parse error at " + token.value)

let parseSetFunc (token: Token) =
    match token.value.ToLower() with
    | "and" -> and'
    | "or" -> or'
    | "average" -> average
    | "count" -> count
    | "counta" -> counta
    | "max" -> max
    | "min" -> min
    | "product" -> product
    | "sum" -> sum
    | "concat" -> concat
    | _ -> invalidOp ("Parse error at " + token.value)

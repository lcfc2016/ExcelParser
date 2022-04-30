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
    // Fixed Arity functions
    | "date" -> (FixedArity date)
    | "datevalue" -> (FixedArity dateValue)
    | "day" -> (FixedArity day)
    | "days" -> (FixedArity days)
    | "days360" -> (FixedArity days360)
    | "edate" -> (FixedArity edate)
    | "eomonth" -> (FixedArity eomonth)
    | "hour" -> (FixedArity hour)
    | "iso.weeknum" -> (FixedArity isoWeekNum)
    | "minute" -> (FixedArity minute)
    | "month" -> (FixedArity month)
    | "networkdays" -> (FixedArity networkDays)
    | "networkdays.intl" -> (FixedArity networkDaysIntl)
    | "now" -> (FixedArity now)
    | "second" -> (FixedArity second)
    | "time" -> (FixedArity time)
    | "timevalue" -> (FixedArity timeValue)
    | "today" -> (FixedArity today)
    | "weekday" -> (FixedArity weekDay)
    | "weeknum" -> (FixedArity weekNum)
    | "workday" -> (FixedArity workDay)
    | "workday.intl" -> (FixedArity workDayIntl)
    | "year" -> (FixedArity year)
    | "yearfrac" -> (FixedArity yearFrac)
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
    | "false" -> (FixedArity false')
    | "not" -> (FixedArity not)
    | "true" -> (FixedArity true')
    | "address" -> (FixedArity address)
    | "column" -> (FixedArity column)
    | "columns" -> (FixedArity columns)
    | "filter" -> (FixedArity filter)
    | "formulatext" -> (FixedArity formulaText)
    | "hlookup" -> (FixedArity hlookup)
    | "hyperlink" -> (FixedArity hyperlink)
    | "index" -> (FixedArity index)
    | "indirect" -> (FixedArity indirect)
    | "match" -> (FixedArity match')
    | "offset" -> (FixedArity offset)
    | "row" -> (FixedArity row)
    | "rows" -> (FixedArity rows)
    | "sort" -> (FixedArity sort)
    | "transpose" -> (FixedArity transpose)
    | "unique" -> (FixedArity unique)
    | "vlookup" -> (FixedArity vlookup)
    | "abs" -> (FixedArity abs)
    | "acos" -> (FixedArity acos)
    | "acosh" -> (FixedArity acosh)
    | "acot" -> (FixedArity acot)
    | "acoth" -> (FixedArity acoth)
    | "arabic" -> (FixedArity arabic)
    | "asin" -> (FixedArity asin)
    | "asinh" -> (FixedArity asinh)
    | "atan" -> (FixedArity atan)
    | "atan2" -> (FixedArity atan2)
    | "atanh" -> (FixedArity atanh)
    | "base" -> (FixedArity base')
    | "ceiling" -> (FixedArity ceiling)
    | "ceiling.math" -> (FixedArity ceilingMath)
    | "ceiling.precise" -> (FixedArity ceilingPrecise)
    | "combin" -> (FixedArity combin)
    | "combina" -> (FixedArity combinA)
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
    | "factdouble" -> (FixedArity factDouble)
    | "floor" -> (FixedArity floor)
    | "floor.math" -> (FixedArity floorMath)
    | "floor.precise" -> (FixedArity floorPrecise)
    | "gcd" -> (FixedArity gcd)
    | "int" -> (FixedArity int')
    | "iso.ceiling" -> (FixedArity isoCeiling)
    | "lcm" -> (FixedArity lcm)
    | "ln" -> (FixedArity ln)
    | "log" -> (FixedArity log)
    | "log10" -> (FixedArity log10)
    | "mdeterm" -> (FixedArity mDeterm)
    | "minverse" -> (FixedArity mInverse)
    | "mmult" -> (FixedArity mMult)
    | "mod" -> (FixedArity mod')
    | "mround" -> (FixedArity mRound)
    | "munit" -> (FixedArity mUnit)
    | "odd" -> (FixedArity odd)
    | "pi" -> (FixedArity pi)
    | "power" -> (FixedArity power)
    | "quotient" -> (FixedArity quotient)
    | "radians" -> (FixedArity radians)
    | "rand" -> (FixedArity rand)
    | "randarray" -> (FixedArity randArray)
    | "randbetween" -> (FixedArity randBetween)
    | "roman" -> (FixedArity roman)
    | "round" -> (FixedArity round)
    | "rounddown" -> (FixedArity roundDown)
    | "roundup" -> (FixedArity roundUp)
    | "sec" -> (FixedArity sec)
    | "sech" -> (FixedArity sech)
    | "seriessum" -> (FixedArity seriesSum)
    | "sequence" -> (FixedArity sequence)
    | "sign" -> (FixedArity sign)
    | "sin" -> (FixedArity sin)
    | "sinh" -> (FixedArity sinh)
    | "sqrt" -> (FixedArity sqrt)
    | "sqrtpi" -> (FixedArity sqrtPi)
    | "sumif" -> (FixedArity sumIf)
    | "tan" -> (FixedArity tan)
    | "tanh" -> (FixedArity tanh)
    | "trunc" -> (FixedArity trunc)
    | "arraytotext" -> (FixedArity arrayToText)
    | "asc" -> (FixedArity asc)
    | "bahttext" -> (FixedArity bahtText)
    | "char" -> (FixedArity char)
    | "clean" -> (FixedArity clean)
    | "code" -> (FixedArity code)
    | "dbcs" -> (FixedArity dbcs)
    | "dollar" -> (FixedArity dollar)
    | "exact" -> (FixedArity exact)
    | "find" -> (FixedArity find)
    | "findb" -> (FixedArity findB)
    | "fixed" -> (FixedArity fixed')
    | "left" -> (FixedArity left)
    | "leftb" -> (FixedArity leftB)
    | "len" -> (FixedArity len)
    | "lenb" -> (FixedArity lenB)
    | "lower" -> (FixedArity lower)
    | "mid" -> (FixedArity mid)
    | "midb" -> (FixedArity midB)
    | "numbervalue" -> (FixedArity numberValue)
    | "phonetic" -> (FixedArity phonetic)
    | "proper" -> (FixedArity proper)
    | "replace" -> (FixedArity replace)
    | "replaceb" -> (FixedArity replaceB)
    | "rept" -> (FixedArity rept)
    | "right" -> (FixedArity right)
    | "rightb" -> (FixedArity rightB)
    | "search" -> (FixedArity search)
    | "searchb" -> (FixedArity searchB)
    | "substitute" -> (FixedArity substitute)
    | "t" -> (FixedArity t)
    | "text" -> (FixedArity text)
    | "unichar" -> (FixedArity uniChar)
    | "unicode" -> (FixedArity uniCode)
    | "trim" -> (FixedArity trim)
    | "upper" -> (FixedArity upper)
    | "value" -> (FixedArity value)
    | "valuetotext" -> (FixedArity valueToText)
    // Set Functions
    | "and" -> (Variadic and')
    | "or" -> (Variadic or')
    | "xor" -> (Variadic xor)
    | "areas" -> (Variadic areas)
    | "multinomial" -> (Variadic multinomial)
    | "product" -> (Variadic product)
    | "sum" -> (Variadic sum)
    | "average" -> (Variadic average)
    | "count" -> (Variadic count)
    | "counta" -> (Variadic counta)
    | "max" -> (Variadic max)
    | "min" -> (Variadic min)
    | "concat" -> (Variadic concat)
    | "concatenate" -> (Variadic concatenate)
    // Generic functions
    | "if" -> (Generic if')
    | "iferror" -> (Generic ifError)
    | "ifna" -> (Generic ifNA)
    | "choose" -> (Generic choose)
    | _ -> invalidOp ("Unknown function: " + token.value)
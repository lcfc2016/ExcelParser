module Functions

open System

open Types

// Logical
let false' = { inputs = []; output = (SimpleType TypeEnum.Bool); repr = "FALSE" }
let not = { inputs = [(SimpleType TypeEnum.Bool)]; output = (SimpleType TypeEnum.Bool); repr = "NOT" }
let true' = { inputs = []; output = (SimpleType TypeEnum.Bool); repr = "TRUE" }

// Math and Trig
let abs = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "ABS" }
let acos = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "ACOS" }
let acosh = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "ACOSH" }
let acot = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "ACOT" }
let acoth = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "ACOTH" }
let asin = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "ASIN" }
let asinh = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "ASINH" }
let atan = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "ATAN" }
let atan2 = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "ATAN2" }
let atanh = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "ATANH" }
let ceiling = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "CEILING" }
let cos = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "COS" }
let cosh = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "COSH" }
let cot = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "COT" }
let coth = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "COTH" }
let csc = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "CSC" }
let csch = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "CSCH" }
let degrees = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "DEGREES" }
let even = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "EVEN" }
let exp = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "EXP" }
let fact = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "FACT" }
let factDouble = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "FACTDOUBLE" }
let floor = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "FLOOR" }
let gcd = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "GCD" }
let lcm = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "LCM" }
let ln = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "LN" }
let log = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "LOG" }
let log10 = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "LOG10" }
let mod' = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "MOD" }
let odd = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "ODD" }
let pi = { inputs = []; output = (SimpleType TypeEnum.Numeric); repr = "PI" }
let power = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "POWER" }
let quotient = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "QUOTIENT" }
let rand = { inputs = []; output = (SimpleType TypeEnum.Numeric); repr = "RAND" }
let round = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "ROUND" }
let sec = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "SEC" }
let sech = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "SECH" }
let sin = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "SIN" }
let sinh = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "SINH" }
let sqrt = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "SQRT" }
let tan = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "TAN" }
let tanh = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "TANH" }
let trunc = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "TRUNC" }

// Text
let clean = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Str); repr = "CLEAN" }
let dollar = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); repr = "DOLLAR" }
let exact = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Bool); repr = "EXACT" }
let find = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Numeric); repr = "FIND" }
let left = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); repr = "LEFT" }
let len = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Numeric); repr = "LEN" }
let lower = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Str); repr = "LOWER" }
let mid = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); repr = "MID" }
let right = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); repr = "RIGHT" }
let upper = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Str); repr = "UPPER" }

// Information
let cell = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.General)]; output = (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); repr = "CELL"}
let errorType = { inputs = [(SimpleType TypeEnum.Error)]; output = (SimpleType TypeEnum.Numeric); repr = "ERROR.TYPE"}
let info = { inputs = [(SimpleType TypeEnum.Str)]; output = (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); repr = "INFO"}
let isBlank = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); repr = "ISBLANK"}
let isErr = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); repr = "ISERR"}
let isError = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); repr = "ISERROR"}
let isEven = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Bool); repr = "ISEVEN"}
let isFormula = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); repr = "ISFORMULA"}
let isLogical = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); repr = "ISLOGICAL"}
let isNa = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); repr = "ISNA"}
let isNontext = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); repr = "ISNONTEXT"}
let isNumber = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); repr = "ISNUMBER"}
let isOdd = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Bool); repr = "ISODD"}
let isRef = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); repr = "ISREF"}
let isText = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); repr = "ISTEXT"}
let n = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Numeric); repr = "N"}
let na = { inputs = []; output = (SimpleType TypeEnum.Error); repr = "NA"}
let sheet = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Numeric); repr = "SHEET"}
let sheets = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Numeric); repr = "SHEETS"}
let type' = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Numeric); repr = "TYPE"}

module Functions

open System

open Types

// Logical
let false' = { inputs = []; output = (SimpleType TypeEnum.Bool); minArity = 0; repr = "FALSE" }
let not = { inputs = [(SimpleType TypeEnum.Bool)]; output = (SimpleType TypeEnum.Bool); minArity = 1; repr = "NOT" }
let true' = { inputs = []; output = (SimpleType TypeEnum.Bool); minArity = 0; repr = "TRUE" }

// Lookup and Reference TODO - Add to func parser
let address = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 2; repr = "ADDRESS" }
//let choose = { inputs = []; output = (); repr = "CHOOSE" }
let column = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "COLUMN" }
let columns = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "COLUMNS" }
let filter = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.Bool); (SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.General); minArity = 2; repr = "FILTER" }
let formulaText = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); minArity = 1; repr = "FORMULATEXT" }
// let getPivotData = { inputs = []; output = (); repr = "GETPIVOTDATA" } Not convinced this is parseable
// let hlookup = { inputs = []; output = (); repr = "HLOOKUP" }
let hyperlink = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "HYPERLINK" }
// let index = { inputs = []; output = (); repr = "INDEX" }
// let indirect = { inputs = []; output = (); repr = "INDIRECT" }
// let lookup = { inputs = []; output = (); repr = "LOOKUP" }
// let match' = { inputs = []; output = (); repr = "MATCH" }
// let offset = { inputs = []; output = (); repr = "OFFSET" }
let row = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ROW" }
let rows = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ROWS" }
// let rtd = { inputs = []; output = (); repr = "RTD" }
// let sort = { inputs = []; output = (); repr = "SORT" }
// let sortBy = { inputs = []; output = (); repr = "SORTBY" }
// let transpose = { inputs = []; output = (); repr = "TRANSPOSE" }
// let unique = { inputs = []; output = (); repr = "UNIQUE" }
// let vlookup = { inputs = []; output = (); repr = "VLOOKUP" }
// let xlookup = { inputs = []; output = (); repr = "XLOOKUP" }
// let xmatch = { inputs = []; output = (); repr = "XMATCH" }


// Math and Trig
let abs = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ABS" }
let acos = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ACOS" }
let acosh = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ACOSH" }
let acot = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ACOT" }
let acoth = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ACOTH" }
let asin = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ASIN" }
let asinh = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ASINH" }
let atan = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ATAN" }
let atan2 = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ATAN2" }
let atanh = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ATANH" }
let ceiling = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "CEILING" }
let cos = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "COS" }
let cosh = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "COSH" }
let cot = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "COT" }
let coth = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "COTH" }
let csc = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "CSC" }
let csch = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "CSCH" }
let degrees = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "DEGREES" }
let even = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "EVEN" }
let exp = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "EXP" }
let fact = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "FACT" }
let factDouble = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "FACTDOUBLE" }
let floor = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "FLOOR" }
let gcd = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "GCD" }
let lcm = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "LCM" }
let ln = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "LN" }
let log = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "LOG" }
let log10 = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "LOG10" }
let mod' = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "MOD" }
let odd = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ODD" }
let pi = { inputs = []; output = (SimpleType TypeEnum.Numeric); minArity = 0; repr = "PI" }
let power = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "POWER" }
let quotient = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "QUOTIENT" }
let rand = { inputs = []; output = (SimpleType TypeEnum.Numeric); minArity = 0; repr = "RAND" }
let round = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "ROUND" }
let sec = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "SEC" }
let sech = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "SECH" }
let sin = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "SIN" }
let sinh = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "SINH" }
let sqrt = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "SQRT" }
let tan = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "TAN" }
let tanh = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "TANH" }
let trunc = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "TRUNC" }

// Text
let clean = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "CLEAN" }
let dollar = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "DOLLAR" }
let exact = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Bool); minArity = 2; repr = "EXACT" }
let find = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "FIND" }
let left = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 2; repr = "LEFT" }
let len = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "LEN" }
let lower = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "LOWER" }
let mid = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 3; repr = "MID" }
let right = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 2; repr = "RIGHT" }
let upper = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "UPPER" }

// Information
let cell = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.General)]; output = (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); minArity = 2; repr = "CELL"}
let errorType = { inputs = [(SimpleType TypeEnum.Error)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ERROR.TYPE"}
let info = { inputs = [(SimpleType TypeEnum.Str)]; output = (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); minArity = 1; repr = "INFO"}
let isBlank = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); minArity = 1; repr = "ISBLANK"}
let isErr = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); minArity = 1; repr = "ISERR"}
let isError = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); minArity = 1; repr = "ISERROR"}
let isEven = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Bool); minArity = 1; repr = "ISEVEN"}
let isFormula = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); minArity = 1; repr = "ISFORMULA"}
let isLogical = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); minArity = 1; repr = "ISLOGICAL"}
let isNa = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); minArity = 1; repr = "ISNA"}
let isNontext = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); minArity = 1; repr = "ISNONTEXT"}
let isNumber = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); minArity = 1; repr = "ISNUMBER"}
let isOdd = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Bool); minArity = 1; repr = "ISODD"}
let isRef = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); minArity = 1; repr = "ISREF"}
let isText = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); minArity = 1; repr = "ISTEXT"}
let n = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "N"}
let na = { inputs = []; output = (SimpleType TypeEnum.Error); minArity = 0; repr = "NA"}
let sheet = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "SHEET"}
let sheets = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "SHEETS"}
let type' = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "TYPE"}

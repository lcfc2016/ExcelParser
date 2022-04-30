module Functions

open System

open Types


// Date & Time - DONE
let date = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 3; repr = "DATE" }
let dateValue = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "DATEVALUE" }
let day = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "DAY" }
let days = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "DAYS" }
let days360 = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (SimpleType TypeEnum.Bool)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "DAYS360" }
let edate = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "EDATE" }
let eomonth = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "EOMONTH" }
let hour = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "HOUR" }
let isoWeekNum = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ISOWEEKNUM" }
let minute = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "MINUTE" }
let month = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "MONTH" }
let networkDays = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "NETWORKDAYS" }
let networkDaysIntl = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (SimpleType TypeEnum.Numeric); (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "NETWORKDAYS.INTL" }
let now = { inputs = []; output = (SimpleType TypeEnum.Numeric); minArity = 0; repr = "NOW" }
let second = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "SECOND" }
let time = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 3; repr = "TIME" }
let timeValue = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "TIMEVALUE" }
let today = { inputs = []; output = (SimpleType TypeEnum.Numeric); minArity = 0; repr = "TODAY" }
let weekDay = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "WEEKDAY" }
let weekNum = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "WEEKNUM" }
let workDay = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (SimpleType TypeEnum.Numeric); (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); minArity = 2; repr = "WORKDAY" }
let workDayIntl = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); minArity = 2; repr = "WORKDAY.INTL" }
let year = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "YEAR" }
let yearFrac = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "YEARFRAC" }

// Engineering

// Financial

// Information - DONE
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

// Logical
let false' = { inputs = []; output = (SimpleType TypeEnum.Bool); minArity = 0; repr = "FALSE" }
let not = { inputs = [(SimpleType TypeEnum.Bool)]; output = (SimpleType TypeEnum.Bool); minArity = 1; repr = "NOT" }
let true' = { inputs = []; output = (SimpleType TypeEnum.Bool); minArity = 0; repr = "TRUE" }
// TO DO
// IFS and SWITCH

// Lookup and Reference
let address = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 2; repr = "ADDRESS" }
let column = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "COLUMN" }
let columns = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "COLUMNS" }
let filter = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.Bool); (SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.General); minArity = 2; repr = "FILTER" }
let formulaText = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); minArity = 1; repr = "FORMULATEXT" }
// let getPivotData = { inputs = []; output = (); repr = "GETPIVOTDATA" } Not convinced this is parseable
let hlookup = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.General); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Bool)]; output = (SimpleType TypeEnum.General); minArity = 3; repr = "HLOOKUP" }
let hyperlink = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "HYPERLINK" }
let index = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.General); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.General); minArity = 2; repr = "INDEX" }
// If the below is to be made stricter it would likely need handling as a specific case, has a very odd arg pattern
let indirect = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Bool)]; output = (SimpleType TypeEnum.General); minArity = 1; repr = "INDIRECT" }
// let lookup = { inputs = []; output = (); repr = "LOOKUP" }
let match' = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.General); (SimpleType TypeEnum.Numeric)]; output = (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Error])); minArity = 2; repr = "MATCH" }
let offset = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.General); minArity = 3; repr = "OFFSET" }
let row = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ROW" }
let rows = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ROWS" }
// let rtd = { inputs = []; output = (); repr = "RTD" } Not parseable within this app's framework, refers to a COM process and to a server
let sort = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Bool)]; output = (SimpleType TypeEnum.General); minArity = 1; repr = "SORT" }
// sortBy has a fairly complex argument pattern, need to review
// let sortBy = { inputs = []; output = (); repr = "SORTBY" }
let transpose = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.General); minArity = 1; repr = "TRANSPOSE" }
let unique = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.Bool); (SimpleType TypeEnum.Bool)]; output = (SimpleType TypeEnum.General); minArity = 1; repr = "UNIQUE" }
let vlookup = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.General); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Bool)]; output = (SimpleType TypeEnum.General); minArity = 3; repr = "VLOOKUP" }
// Couple of odd Office365 functions, ignoring for now
// let xlookup = { inputs = []; output = (); repr = "XLOOKUP" }
// let xmatch = { inputs = []; output = (); repr = "XMATCH" }


// Math and Trig
let abs = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ABS" }
// let Aggregate = Hmm this looks a bit tricky, has two forms for a starter, bypassing for now
let acos = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ACOS" }
let acosh = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ACOSH" }
let acot = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ACOT" }
let acoth = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ACOTH" }
let arabic = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ARABIC" }
let asin = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ASIN" }
let asinh = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ASINH" }
let atan = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ATAN" }
let atan2 = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ATAN2" }
let atanh = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ATANH" }
let base' = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 2; repr = "BASE" }
let ceiling = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "CEILING" }
let ceilingMath = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "CEILING.MATH" }
let ceilingPrecise = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "CEILING.PRECISE" }
let combin = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "COMBIN" }
let combinA = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "COMBINA" }
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
let floorMath = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "FLOOR.MATH" }
let floorPrecise = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "FLOOR.PRECISE" }
let gcd = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "GCD" }
let int' = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "INT" }
let isoCeiling = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ISO.CEILING" }
let lcm = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "LCM" }
// let let' = unimplemented for now, may stay that way as will require the addition of assignment (have got code to do this in the Basic Interpreter proj)
let ln = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "LN" }
let log = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "LOG" }
let log10 = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "LOG10" }
let mDeterm = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "MDETERM" }
let mInverse = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "MINVERSE" }
let mMult = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "MMULT" }
let mod' = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "MOD" }
let mRound = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "MROUND" }
let mUnit = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "MUNIT" }
let odd = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ODD" }
let pi = { inputs = []; output = (SimpleType TypeEnum.Numeric); minArity = 0; repr = "PI" }
let power = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "POWER" }
let quotient = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "QUOTIENT" }
let radians = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "RADIANS" }
let rand = { inputs = []; output = (SimpleType TypeEnum.Numeric); minArity = 0; repr = "RAND" }
let randArray = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Bool)]; output = (SimpleType TypeEnum.Numeric); minArity = 0; repr = "RANDARRAY" }
let randBetween = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "RANDBETWEEN" }
let roman = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "ROMAN" }
let round = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "ROUND" }
let roundDown = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "ROUNDDOWN" }
let roundUp = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "ROUNDUP" }
let sec = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "SEC" }
let sech = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "SECH" }
let seriesSum = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 4; repr = "SERIESSUM" }
let sequence = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "SEQUENCE" }
let sign = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "SIGN" }
let sin = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "SIN" }
let sinh = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "SINH" }
let sqrt = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "SQRT" }
let sqrtPi = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "SQRTPI" }
// let subTotal = probably needs to be included in those functions like switch/ifs
let sumIf = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.General); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "SUMIF" }
// let sumIfs = Needs some thought
// let SUMPRODUCT
// let SUMSQ
// let SUMX2MY2
// let SUMX2PY2
// let SUMXMY2
let tan = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "TAN" }
let tanh = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "TANH" }
let trunc = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "TRUNC" }

// Statistical

// Text
let arrayToText = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "ARRAYTOTEXT" }
let asc = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "ASC" }
let bahtText = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "BAHTTEXT" }
let char = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "CHAR" }
let clean = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "CLEAN" }
let code = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "CODE" }
let dbcs = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "DBCS" }
let dollar = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "DOLLAR" }
let exact = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Bool); minArity = 2; repr = "EXACT" }
let find = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "FIND" }
let findB = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "FINDB" }
let fixed' = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Bool)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "FIXED" }
let left = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 2; repr = "LEFT" }
let leftB = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 2; repr = "LEFTB" }
let len = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "LEN" }
let lenB = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "LENB" }
let lower = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "LOWER" }
let mid = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 3; repr = "MID" }
let midB = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 3; repr = "MIDB" }
let numberValue = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "NUMBERVALUE" }
let phonetic = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "PHONETIC" }
let proper = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "PROPER" }
let replace = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Str); minArity = 4; repr = "REPLACE" }
let replaceB = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Str); minArity = 4; repr = "REPLACEB" }
let rept = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 2; repr = "REPT" }
let right = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "RIGHT" }
let rightB = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "RIGHTB" }
let search = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 2; repr = "SEARCH" }
let searchB = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 2; repr = "SEARCHB" }
let substitute = { inputs = [(SimpleType TypeEnum.Str); (SimpleType TypeEnum.Str); (SimpleType TypeEnum.Str); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 3; repr = "SUBSTITUTE" }
let t = { inputs = [(SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "T" }
let text = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Str); minArity = 2; repr = "TEXT" }
// TEXTJOIN - Needs a mixed fixed/set function type as per ifs/switch
let uniChar = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "CHAR" }
let uniCode = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "CODE" }
let trim = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "TRIM" }
let upper = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "UPPER" }
let value = { inputs = [(SimpleType TypeEnum.Str)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "VALUE" }
let valueToText = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Str); minArity = 1; repr = "VALUETOTEXT" }

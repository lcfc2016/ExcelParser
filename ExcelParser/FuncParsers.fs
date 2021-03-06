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
    // Unary op precedence
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
    (if string.StartsWith("_xludf.", StringComparison.CurrentCultureIgnoreCase) then invalidOp("User-defined functions not supported: " + string)
    elif string.StartsWith("_xlfn.", StringComparison.CurrentCultureIgnoreCase) then string.Substring(6)
    elif string.StartsWith("_xll.", StringComparison.CurrentCultureIgnoreCase) then string.Substring(5)
    else string).ToLower()

let parseFunc (token: Token) =
    match lowerAndRemovePrefix token.value with
    | "ifs" -> Ifs
    | "switch" -> Switch
    // Fixed Arity functions
    | "date" -> (FixedArity date)
    | "datevalue" -> (FixedArity dateValue)
    | "day" -> (FixedArity day)
    | "days" -> (FixedArity days)
    | "days360" -> (FixedArity days360)
    | "edate" -> (FixedArity edate)
    | "eomonth" -> (FixedArity eomonth)
    | "hour" -> (FixedArity hour)
    | "isoweeknum" -> (FixedArity isoWeekNum)
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
    | "besseli" -> (FixedArity besselI)
    | "besselj" -> (FixedArity besselJ)
    | "besselk" -> (FixedArity besselK)
    | "bessely" -> (FixedArity besselY)
    | "bin2dec" -> (FixedArity bin2Dec)
    | "bin2hex" -> (FixedArity bin2Hex)
    | "bin2oct" -> (FixedArity bin2Oct)
    | "bitand" -> (FixedArity bitAnd)
    | "bitlshift" -> (FixedArity bitLShift)
    | "bitor" -> (FixedArity bitOr)
    | "bitrshift" -> (FixedArity bitRShift)
    | "bitxor" -> (FixedArity bitXor)
    | "complex" -> (FixedArity complex)
    | "convert" -> (FixedArity convert)
    | "dec2bin" -> (FixedArity dec2Bin)
    | "dec2hex" -> (FixedArity dec2Hex)
    | "dec2oct" -> (FixedArity dec2Oct)
    | "delta" -> (FixedArity delta)
    | "erf" -> (FixedArity erf)
    | "erf.precise" -> (FixedArity erfPrecise)
    | "erfc" -> (FixedArity erfC)
    | "erfc.precise" -> (FixedArity erfCPrecise)
    | "gestep" -> (FixedArity gestep)
    | "hex2bin" -> (FixedArity hex2Bin)
    | "hex2dec" -> (FixedArity hex2Dec)
    | "hex2oct" -> (FixedArity hex2Oct)
    | "imabs" -> (FixedArity imAbs)
    | "imaginary" -> (FixedArity imAginary)
    | "imargument" -> (FixedArity imArgument)
    | "imconjugate" -> (FixedArity imConjugate)
    | "imcos" -> (FixedArity imCos)
    | "imcosh" -> (FixedArity imCosh)
    | "imcot" -> (FixedArity imCot)
    | "imcsc" -> (FixedArity imCsc)
    | "imcsch" -> (FixedArity imCsch)
    | "imdiv" -> (FixedArity imDiv)
    | "imexp" -> (FixedArity imExp)
    | "imln" -> (FixedArity imLn)
    | "imlog10" -> (FixedArity imLog10)
    | "imlog2" -> (FixedArity imLog2)
    | "impower" -> (FixedArity imPower)
    | "imreal" -> (FixedArity imReal)
    | "imsec" -> (FixedArity imSec)
    | "imsech" -> (FixedArity imSech)
    | "imsin" -> (FixedArity imSin)
    | "imsinh" -> (FixedArity imSinh)
    | "imsqrt" -> (FixedArity imSqrt)
    | "imsub" -> (FixedArity imSub)
    | "imtan" -> (FixedArity imTan)
    | "oct2bin" -> (FixedArity oct2Bin)
    | "oct2dec" -> (FixedArity oct2Dec)
    | "oct2hex" -> (FixedArity oct2Hex)
    | "accrint" -> (FixedArity accrInt)
    | "accrintm" -> (FixedArity accrIntM)
    | "amordegrc" -> (FixedArity amorDegrc)
    | "amorlinc" -> (FixedArity amorLinc)
    | "coupdaybs" -> (FixedArity coupDayBs)
    | "coupdays" -> (FixedArity coupDays)
    | "coupdaysnc" -> (FixedArity coupDaysNc)
    | "coupncd" -> (FixedArity coupNcd)
    | "coupnum" -> (FixedArity coupNum)
    | "couppcd" -> (FixedArity coupPcd)
    | "cumipmt" -> (FixedArity cumIpmt)
    | "cumprinc" -> (FixedArity cumPrinc)
    | "db" -> (FixedArity db)
    | "ddb" -> (FixedArity ddb)
    | "disc" -> (FixedArity disc)
    | "dollarde" -> (FixedArity dollarDe)
    | "dollarfr" -> (FixedArity dollarFr)
    | "duration" -> (FixedArity duration)
    | "effect" -> (FixedArity effect)
    | "fv" -> (FixedArity fv)
    | "fvschedule" -> (FixedArity fvSchedule)
    | "intrate" -> (FixedArity intRate)
    | "ipmt" -> (FixedArity ipmt)
    | "irr" -> (FixedArity irr)
    | "ispmt" -> (FixedArity isPmt)
    | "mduration" -> (FixedArity mDuration)
    | "mirr" -> (FixedArity mIrr)
    | "nominal" -> (FixedArity nominal)
    | "nper" -> (FixedArity nPer)
    | "npv" -> (FixedArity nPv)
    | "oddfprice" -> (FixedArity oddFPrice)
    | "oddfyield" -> (FixedArity oddFYield)
    | "oddlprice" -> (FixedArity oddLPrice)
    | "oddlyield" -> (FixedArity oddLYield)
    | "pduration" -> (FixedArity pDuration)
    | "pmt" -> (FixedArity pmt)
    | "ppmt" -> (FixedArity pPmt)
    | "price" -> (FixedArity price)
    | "pricedisc" -> (FixedArity priceDisc)
    | "pricemat" -> (FixedArity priceMat)
    | "pv" -> (FixedArity pv)
    | "rate" -> (FixedArity rate)
    | "received" -> (FixedArity received)
    | "rri" -> (FixedArity rri)
    | "sln" -> (FixedArity sln)
    | "syd" -> (FixedArity syd)
    | "tbilleq" -> (FixedArity tBillEq)
    | "tbillprice" -> (FixedArity tBillPrice)
    | "tbillyield" -> (FixedArity tBillYield)
    | "vdb" -> (FixedArity vDb)
    | "xirr" -> (FixedArity xIrr)
    | "xnpv" -> (FixedArity xNpv)
    | "yield" -> (FixedArity yield')
    | "yielddisc" -> (FixedArity yieldDisc)
    | "yieldmat" -> (FixedArity yieldMat)
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
    | "lookup" -> (FixedArity lookup)
    | "match" -> (FixedArity match')
    | "offset" -> (FixedArity offset)
    | "row" -> (FixedArity row)
    | "rows" -> (FixedArity rows)
    | "sort" -> (FixedArity sort)
    | "sortby" -> (FixedArity sortBy)
    | "transpose" -> (FixedArity transpose)
    | "unique" -> (FixedArity unique)
    | "vlookup" -> (FixedArity vlookup)
    | "xlookup" -> (FixedArity xlookup)
    | "xmatch" -> (FixedArity xmatch)
    | "abs" -> (FixedArity abs)
    | "aggregate" -> (FixedArity aggregate)
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
    | "subtotal" -> (FixedArity subTotal)
    | "sumif" -> (FixedArity sumIf)
    | "sumifs" -> (FixedArity sumIfs)
    | "sumx2my2" -> (FixedArity sumX2My2)
    | "sumx2py2" -> (FixedArity sumX2Py2)
    | "sumxmy2" -> (FixedArity sumXMy2)
    | "tan" -> (FixedArity tan)
    | "tanh" -> (FixedArity tanh)
    | "trunc" -> (FixedArity trunc)
    | "averageif" -> (FixedArity averageIf)
    | "averageifs" -> (FixedArity averageIfs)
    | "beta.dist" -> (FixedArity betaDist)
    | "betainv"
    | "beta.inv" -> (FixedArity betaInv)
    | "binomdist"
    | "binom.dist" -> (FixedArity binomDist)
    | "binom.dist.range" -> (FixedArity binomDistRange)
    | "binom.inv" -> (FixedArity binomInv)
    | "chisq.dist" -> (FixedArity chiSqDist)
    | "chidist"
    | "chisq.dist.rt" -> (FixedArity chiSqDistRt)
    | "chisq.inv" -> (FixedArity chiSqInv)
    | "chiinv"
    | "chisq.inv.rt" -> (FixedArity chiSqInvRt)
    | "chisq.test" -> (FixedArity chiSqTest)
    | "confidence"
    | "confidence.norm" -> (FixedArity confidenceNorm)
    | "confidence.t" -> (FixedArity confidenceT)
    | "correl" -> (FixedArity correl)
    | "countblank" -> (FixedArity countBlank)
    | "countif" -> (FixedArity countIf)
    | "countifs" -> (FixedArity countIfs)
    | "covar"
    | "covariance.p" -> (FixedArity covarianceP)
    | "covariance.s" -> (FixedArity covarianceS)
    | "expon.dist" -> (FixedArity exponDist)
    | "f.dist" -> (FixedArity fDist)
    | "fdist"
    | "f.dist.rt" -> (FixedArity fDistRt)
    | "f.inv" -> (FixedArity fInv)
    | "finv"
    | "f.inv.rt" -> (FixedArity fInvRt)
    | "f.test" -> (FixedArity fTest)
    | "fisher" -> (FixedArity fisher)
    | "fisherinv" -> (FixedArity fisherinv)
    | "forecast" -> (FixedArity forecast)
    | "forecast.ets" -> (FixedArity forecastEts)
    | "forecast.ets.confint" -> (FixedArity forecastEtsConfInt)
    | "forecast.ets.seasonality" -> (FixedArity forecastEtsSeasonality)
    | "forecast.ets.stat" -> (FixedArity forecastEtsStat)
    | "forecast.linear" -> (FixedArity forecastLinear)
    | "frequency" -> (FixedArity frequency)
    | "gamma" -> (FixedArity gamma)
    | "gamma.dist" -> (FixedArity gammaDist)
    | "gamma.inv" -> (FixedArity gammaInv)
    | "gammaln" -> (FixedArity gammaLn)
    | "gammaln.precise" -> (FixedArity gammaLnPrecise)
    | "gauss" -> (FixedArity gauss)
    | "growth" -> (FixedArity growth)
    | "hypgeom.dist" -> (FixedArity hypgeomDist)
    | "intercept" -> (FixedArity intercept)
    | "large" -> (FixedArity large)
    | "linest" -> (FixedArity linest)
    | "logest" -> (FixedArity logest)
    | "lognorm.dist" -> (FixedArity logNormDist)
    | "loginv"
    | "lognorm.inv" -> (FixedArity logNormInv)
    | "maxifs" -> (FixedArity maxIfs)
    | "minifs" -> (FixedArity minIfs)
    | "negbinom.dist" -> (FixedArity negBinomDist)
    | "normdist"
    | "norm.dist" -> (FixedArity normDist)
    | "norminv"
    | "norm.inv" -> (FixedArity normInv)
    | "normsdist"
    | "norm.s.dist" -> (FixedArity normSDist)
    | "normsinv"
    | "norm.s.inv" -> (FixedArity normSInv)
    | "pearson" -> (FixedArity pearson)
    | "percentile.exc" -> (FixedArity percentileExc)
    | "percentile"
    | "percentile.inc" -> (FixedArity percentileInc)
    | "percentrank.exc" -> (FixedArity percentrankExc)
    | "percentrank.inc" -> (FixedArity percentrankInc)
    | "permut" -> (FixedArity permut)
    | "permutationa" -> (FixedArity permutationa)
    | "phi" -> (FixedArity phi)
    | "poisson.dist" -> (FixedArity poissonDist)
    | "prob" -> (FixedArity prob)
    | "quartile"
    | "quartile.inc" -> (FixedArity quartileInc)
    | "quartile.exc" -> (FixedArity quartileExc)
    | "rank"
    | "rank.avg" -> (FixedArity rankAvg)
    | "rank.eq" -> (FixedArity rankEq)
    | "rsq" -> (FixedArity rsq)
    | "slope" -> (FixedArity slope)
    | "small" -> (FixedArity small)
    | "standardize" -> (FixedArity standardize)
    | "steyx" -> (FixedArity steyx)
    | "tdist"
    | "t.dist" -> (FixedArity tDist)
    | "t.dist.2t" -> (FixedArity tDist2t)
    | "t.dist.rt" -> (FixedArity tDistRt)
    | "tinv"
    | "t.inv" -> (FixedArity tInv)
    | "t.inv.2t" -> (FixedArity tInv2t)
    | "t.test" -> (FixedArity tTest)
    | "trend" -> (FixedArity trend)
    | "trimmean" -> (FixedArity trimmean)
    | "weibull.dist" -> (FixedArity weibullDist)
    | "z.test" -> (FixedArity zTest)
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
    | "textjoin" -> (FixedArity textJoin)
    | "trim" -> (FixedArity trim)
    | "unichar" -> (FixedArity uniChar)
    | "unicode" -> (FixedArity uniCode)
    | "upper" -> (FixedArity upper)
    | "value" -> (FixedArity value)
    | "valuetotext" -> (FixedArity valueToText)
    
    // Set Functions
    | "improduct" -> (Variadic imProduct)
    | "imsum" -> (Variadic imSum)
    | "and" -> (Variadic and')
    | "or" -> (Variadic or')
    | "xor" -> (Variadic xor)
    | "areas" -> (Variadic areas)
    | "multinomial" -> (Variadic multinomial)
    | "product" -> (Variadic product)
    | "sum" -> (Variadic sum)
    | "sumproduct" -> (Variadic sumProduct)
    | "sumsq" -> (Variadic sumSq)
    | "avedev" -> (Variadic avedev)
    | "average" -> (Variadic average)
    | "averagea" -> (Variadic averagea)
    | "count" -> (Variadic count)
    | "counta" -> (Variadic countA)
    | "devsq" -> (Variadic devSq)
    | "kurt" -> (Variadic kurt)
    | "geomean" -> (Variadic geoMean)
    | "harmean" -> (Variadic harMean)
    | "max" -> (Variadic max)
    | "maxa" -> (Variadic maxA)
    | "median" -> (Variadic median)
    | "min" -> (Variadic min)
    | "mina" -> (Variadic minA)
    | "mode.mult" -> (Variadic modeMult)
    | "mode"
    | "mode.sngl" -> (Variadic modeSngl)
    | "skew" -> (Variadic skew)
    | "skew.p" -> (Variadic skewP)
    | "stdevp"
    | "stdev.p" -> (Variadic stDevP)
    | "stdev"
    | "stdev.s" -> (Variadic stDevS)
    | "stdeva" -> (Variadic stDevA)
    | "stdevpa" -> (Variadic stDevPA)
    | "var.p" -> (Variadic varP)
    | "var"
    | "var.s" -> (Variadic varS)
    | "vara" -> (Variadic varA)
    | "varpa" -> (Variadic varPA)
    | "concat" -> (Variadic concat)
    | "concatenate" -> (Variadic concatenate)

    // Generic functions
    | "if" -> (Generic if')
    | "iferror" -> (Generic ifError)
    | "ifna" -> (Generic ifNA)
    | "choose" -> (Generic choose)
    | _ -> invalidOp ("Unknown function: " + token.value)
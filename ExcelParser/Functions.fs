module Functions

open System

open Types


// Date & Time
let date = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "DATE" }
let dateValue = { inputs = [SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "DATEVALUE" }
let day = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "DAY" }
let days = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "DAYS" }
let days360 = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "DAYS360" }
let edate = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "EDATE" }
let eomonth = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "EOMONTH" }
let hour = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "HOUR" }
let isoWeekNum = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "ISOWEEKNUM" }
let minute = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "MINUTE" }
let month = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "MONTH" }
let networkDays = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "NETWORKDAYS" }
let networkDaysIntl = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); SimpleType TypeEnum.Numeric; (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "NETWORKDAYS.INTL" }
let now = { inputs = []; output = SimpleType TypeEnum.Numeric; minArity = 0; repr = "NOW" }
let second = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "SECOND" }
let time = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "TIME" }
let timeValue = { inputs = [SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "TIMEVALUE" }
let today = { inputs = []; output = SimpleType TypeEnum.Numeric; minArity = 0; repr = "TODAY" }
let weekDay = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "WEEKDAY" }
let weekNum = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "WEEKNUM" }
let workDay = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); SimpleType TypeEnum.Numeric; (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); minArity = 2; repr = "WORKDAY" }
let workDayIntl = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); minArity = 2; repr = "WORKDAY.INTL" }
let year = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]))]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "YEAR" }
let yearFrac = { inputs = [(ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "YEARFRAC" }

// Engineering
let besselI = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "BESSELI"}
let besselJ = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "BESSELJ"}
let besselK = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "BESSELK"}
let besselY = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "BESSELY"}
let bin2Dec = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "BIN2DEC"}
let bin2Hex = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 1; repr = "BIN2HEX"}
let bin2Oct = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "BIN2OCT"}
let bitAnd = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "BITAND"}
let bitLShift = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "BITLSHIFT"}
let bitOr = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "BITOR"}
let bitRShift = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "BITRSHIFT"}
let bitXor = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "BITXOR"}
let complex = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Str; minArity = 2; repr = "COMPLEX"}
let convert = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Str; SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "CONVERT"}
let dec2Bin = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "DEC2BIN"}
let dec2Hex = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 1; repr = "DEC2HEX"}
let dec2Oct = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "DEC2OCT"}
let delta = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "DELTA"}
let erf = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "ERF"}
let erfPrecise = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "ERF.PRECISE"}
let erfC = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "ERFC"}
let erfCPrecise = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "ERFC.PRECISE"}
let gestep = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "GESTEP"}
let hex2Bin = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "HEX2BIN"}
let hex2Dec = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "HEX2DEC"}
let hex2Oct = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "HEX2OCT"}
let imAbs = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "IMABS"}
let imAginary = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "IMAGINARY"}
let imArgument = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "IMARGUMENT"}
let imConjugate = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 1; repr = "IMCONJUGATE"}
let imCos = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 1; repr = "IMCOS"}
let imCosh = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 1; repr = "IMCOSH"}
let imCot = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 1; repr = "IMCOT"}
let imCsc = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 1; repr = "IMCSC"}
let imCsch = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 1; repr = "IMCSCH"}
let imDiv = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 1; repr = "IMDIV"}
let imExp = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 1; repr = "IMEXP"}
let imLn = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 1; repr = "IMLN"}
let imLog10 = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 1; repr = "IMLOG10"}
let imLog2 = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 1; repr = "IMLOG2"}
let imPower = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); SimpleType TypeEnum.Numeric]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 2; repr = "IMPOWER"}
let imReal = { inputs = [ComplexType(Set[ TypeEnum.Numeric; TypeEnum.Str])]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "IMREAL"}
let imSec = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 1; repr = "IMSEC"}
let imSech = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 1; repr = "IMSECH"}
let imSin = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 1; repr = "IMSIN"}
let imSinh = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 1; repr = "IMSINH"}
let imSqrt = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 1; repr = "IMSQRT"}
let imSub = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 2; repr = "IMSUB"}
let imTan = { inputs = [ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 1; repr = "IMTAN"}
let oct2Bin = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "OCT2BIN"}
let oct2Dec = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "OCT2DEC"}
let oct2Hex = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str]); minArity = 1; repr = "OCT2HEX"}

// Financial
let accrInt = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 6; repr = "ACCRINT"}
let accrIntM = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "ACCRINTM"}
let amorDegrc = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 6; repr = "AMORDEGRC"}
let amorLinc = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 6; repr = "AMORLINC"}
let coupDayBs = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "COUPDAYBS"}
let coupDays = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "COUPDAYS"}
let coupDaysNc = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "COUPDAYSNC"}
let coupNcd = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "COUPNCD"}
let coupNum = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "COUPNUM"}
let coupPcd = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "COUPPCD"}
let cumIpmt = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 6; repr = "CUMIPMT"}
let cumPrinc = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 6; repr = "CUMPRINC"}
let db = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "DB"}
let ddb = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "DDB"}
let disc = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "DISC"}
let dollarDe = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "DOLLARDE"}
let dollarFr = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "DOLLARFR"}
let duration = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 5; repr = "DURATION"}
let effect = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "EFFECT"}
let fv = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "FV"}
let fvSchedule = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "FVSCHEDULE"}
let intRate = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "INTRATE"}
let ipmt = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "IPMT"}
let irr = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "IRR"}
let isPmt = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "ISPMT"}
let mDuration = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 5; repr = "MDURATION"}
let mIrr = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "MIRR"}
let nominal = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "NOMINAL"}
let nPer = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "NPER"}
let nPv = { inputs = [ for i in 1..255 -> ignore i; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "NPV"}
let oddFPrice = { inputs = [ for i in 1..9 -> ignore i; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 8; repr = "ODDFPRICE"}
let oddFYield = { inputs = [ for i in 1..9 -> ignore i; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 8; repr = "ODDFYIELD"}
let oddLPrice = { inputs = [ for i in 1..8 -> ignore i; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 7; repr = "ODDLPRICE"}
let oddLYield = { inputs = [ for i in 1..8 -> ignore i; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 7; repr = "ODDLYIELD"}
let pDuration = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "PDURATION"}
let pmt = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "PMT"}
let pPmt = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "PPMT"}
let price = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 6; repr = "PRICE"}
let priceDisc = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "PRICEDISC"}
let priceMat = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 5; repr = "PRICEMAT"}
let pv = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "PV"}
let rate = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "RATE"}
let received = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "RECEIVED"}
let rri = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "RRI"}
let sln = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "SLN"}
let syd = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "SYD"}
let tBillEq = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "TBILLEQ"}
let tBillPrice = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "TBILLPRICE"}
let tBillYield = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "TBILLYIELD"}
let vDb = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 5; repr = "VDB"}
let xIrr = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "XIRR"}
let xNpv = { inputs = [SimpleType TypeEnum.Numeric;SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "XNPV"}
let yield' = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 6; repr = "YIELD"}
let yieldDisc = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "YIELDDISC"}
let yieldMat = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 5; repr = "YIELDMAT"}

// Information
let cell = { inputs = [SimpleType TypeEnum.Str; SimpleType TypeEnum.General]; output = (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); minArity = 1; repr = "CELL"}
let errorType = { inputs = [SimpleType TypeEnum.Error]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "ERROR.TYPE"}
let info = { inputs = [SimpleType TypeEnum.Str]; output = (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Str])); minArity = 1; repr = "INFO"}
let isBlank = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Bool; minArity = 1; repr = "ISBLANK"}
let isErr = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Bool; minArity = 1; repr = "ISERR"}
let isError = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Bool; minArity = 1; repr = "ISERROR"}
let isEven = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Bool; minArity = 1; repr = "ISEVEN"}
let isFormula = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Bool; minArity = 1; repr = "ISFORMULA"}
let isLogical = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Bool; minArity = 1; repr = "ISLOGICAL"}
let isNa = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Bool; minArity = 1; repr = "ISNA"}
let isNontext = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Bool; minArity = 1; repr = "ISNONTEXT"}
let isNumber = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Bool; minArity = 1; repr = "ISNUMBER"}
let isOdd = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Bool; minArity = 1; repr = "ISODD"}
let isRef = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Bool; minArity = 1; repr = "ISREF"}
let isText = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Bool; minArity = 1; repr = "ISTEXT"}
let n = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "N"}
let na = { inputs = []; output = SimpleType TypeEnum.Error; minArity = 0; repr = "NA"}
let sheet = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "SHEET"}
let sheets = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "SHEETS"}
let type' = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "TYPE"}

// Logical
let false' = { inputs = []; output = SimpleType TypeEnum.Bool; minArity = 0; repr = "FALSE" }
let not = { inputs = [SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.Bool; minArity = 1; repr = "NOT" }
let true' = { inputs = []; output = SimpleType TypeEnum.Bool; minArity = 0; repr = "TRUE" }

// Lookup and Reference
let address = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Str; minArity = 2; repr = "ADDRESS" }
let column = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Numeric; minArity = 0; repr = "COLUMN" }
let columns = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "COLUMNS" }
let filter = { inputs = [SimpleType TypeEnum.General; SimpleType TypeEnum.Bool; SimpleType TypeEnum.General]; output = SimpleType TypeEnum.General; minArity = 2; repr = "FILTER" }
let formulaText = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Bool; minArity = 1; repr = "FORMULATEXT" }
// Not parseable due to need for pivot table
// let getPivotData = { inputs = []; output = (); repr = "GETPIVOTDATA" }
let hlookup = { inputs = [SimpleType TypeEnum.General; SimpleType TypeEnum.General; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.General; minArity = 3; repr = "HLOOKUP" }
let hyperlink = { inputs = [SimpleType TypeEnum.Str; SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Str; minArity = 1; repr = "HYPERLINK" }
let index = { inputs = [SimpleType TypeEnum.General; SimpleType TypeEnum.General; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.General; minArity = 2; repr = "INDEX" }
let indirect = { inputs = [SimpleType TypeEnum.Str; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.General; minArity = 1; repr = "INDIRECT" }
let lookup = { inputs = [SimpleType TypeEnum.General; SimpleType TypeEnum.General; SimpleType TypeEnum.General]; output = SimpleType TypeEnum.General; minArity = 2; repr = "LOOKUP" }
let match' = { inputs = [SimpleType TypeEnum.General; SimpleType TypeEnum.General; SimpleType TypeEnum.Numeric]; output = (ComplexType (Set [TypeEnum.Numeric; TypeEnum.Error])); minArity = 2; repr = "MATCH" }
let offset = { inputs = [SimpleType TypeEnum.General; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.General; minArity = 3; repr = "OFFSET" }
let row = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Numeric; minArity = 0; repr = "ROW" }
let rows = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "ROWS" }
// Not parseable within this app's framework, refers to a COM process and to a server
// let rtd = { inputs = []; output = (); repr = "RTD" }
let sort = { inputs = [SimpleType TypeEnum.General; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.General; minArity = 1; repr = "SORT" }
let sortBy = { inputs = [SimpleType TypeEnum.General; SimpleType TypeEnum.General; SimpleType TypeEnum.Numeric ] @ List.concat [ for i in 1..127 -> ignore i; [SimpleType TypeEnum.General; SimpleType TypeEnum.Numeric] ]; output = SimpleType TypeEnum.General; minArity = 2; repr = "SORTBY" }
let transpose = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.General; minArity = 1; repr = "TRANSPOSE" }
let unique = { inputs = [SimpleType TypeEnum.General; SimpleType TypeEnum.Bool; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.General; minArity = 1; repr = "UNIQUE" }
let vlookup = { inputs = [SimpleType TypeEnum.General; SimpleType TypeEnum.General; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.General; minArity = 3; repr = "VLOOKUP" }
let xlookup = { inputs = [SimpleType TypeEnum.General; SimpleType TypeEnum.General; SimpleType TypeEnum.General; SimpleType TypeEnum.General; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.General; minArity = 3; repr = "XLOOKUP" }
let xmatch = { inputs = [SimpleType TypeEnum.General; SimpleType TypeEnum.General; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "XMATCH" }

// Math and Trig
let abs = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "ABS" }
let aggregate = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric] @ [ for i in 1..253 -> ignore i; SimpleType TypeEnum.General ]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "AGGREGATE" }
let acos = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "ACOS" }
let acosh = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "ACOSH" }
let acot = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "ACOT" }
let acoth = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "ACOTH" }
let arabic = { inputs = [SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "ARABIC" }
let asin = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "ASIN" }
let asinh = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "ASINH" }
let atan = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "ATAN" }
let atan2 = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "ATAN2" }
let atanh = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "ATANH" }
let base' = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Str; minArity = 2; repr = "BASE" }
let ceiling = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "CEILING" }
let ceilingMath = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "CEILING.MATH" }
let ceilingPrecise = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "CEILING.PRECISE" }
let combin = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "COMBIN" }
let combinA = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "COMBINA" }
let cos = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "COS" }
let cosh = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "COSH" }
let cot = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "COT" }
let coth = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "COTH" }
let csc = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "CSC" }
let csch = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "CSCH" }
let degrees = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "DEGREES" }
let even = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "EVEN" }
let exp = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "EXP" }
let fact = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "FACT" }
let factDouble = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "FACTDOUBLE" }
let floor = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "FLOOR" }
let floorMath = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "FLOOR.MATH" }
let floorPrecise = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "FLOOR.PRECISE" }
let gcd = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "GCD" }
let int' = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "INT" }
let isoCeiling = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "ISO.CEILING" }
let lcm = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "LCM" }
let ln = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "LN" }
let log = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "LOG" }
let log10 = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "LOG10" }
let mDeterm = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "MDETERM" }
let mInverse = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "MINVERSE" }
let mMult = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "MMULT" }
let mod' = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "MOD" }
let mRound = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "MROUND" }
let mUnit = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "MUNIT" }
let odd = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "ODD" }
let pi = { inputs = []; output = SimpleType TypeEnum.Numeric; minArity = 0; repr = "PI" }
let power = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "POWER" }
let quotient = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "QUOTIENT" }
let radians = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "RADIANS" }
let rand = { inputs = []; output = SimpleType TypeEnum.Numeric; minArity = 0; repr = "RAND" }
let randArray = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.Numeric; minArity = 0; repr = "RANDARRAY" }
let randBetween = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "RANDBETWEEN" }
let roman = { inputs = [SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "ROMAN" }
let round = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "ROUND" }
let roundDown = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "ROUNDDOWN" }
let roundUp = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "ROUNDUP" }
let sec = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "SEC" }
let sech = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "SECH" }
let seriesSum = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "SERIESSUM" }
let sequence = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "SEQUENCE" }
let sign = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "SIGN" }
let sin = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "SIN" }
let sinh = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "SINH" }
let sqrt = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "SQRT" }
let sqrtPi = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "SQRTPI" }
let subTotal = { inputs =  SimpleType TypeEnum.Numeric :: [ for i in 1..254 -> ignore i; SimpleType TypeEnum.General ]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "SUBTOTAL" }
let sumIf = { inputs = [SimpleType TypeEnum.General; SimpleType TypeEnum.General; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "SUMIF" }
let sumIfs = { inputs = SimpleType TypeEnum.Numeric :: [ for i in 1..254 -> ignore i; SimpleType TypeEnum.General ]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "SUMIFS" }
let sumX2My2 = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "SUMX2MY2" }
let sumX2Py2 = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "SUMX2PY2" }
let sumXMy2 = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "SUMXMY2" }
let tan = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "TAN" }
let tanh = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "TANH" }
let trunc = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "TRUNC" }

// Statistical
let averageIf = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "AVERAGEIF"}
let averageIfs = { inputs = SimpleType TypeEnum.Numeric :: [ for i in 1..254 -> ignore i; SimpleType TypeEnum.General ]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "AVERAGEIFS" }
let betaDist = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "BETA.DIST"}
let betaInv = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "BETA.INV"}
let binomDist = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "BINOM.DIST"}
let binomDistRange = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "BINOM.DIST.RANGE"}
let binomInv = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "BINOM.INV"}
let chiSqDist = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "CHISQ.DIST"}
let chiSqDistRt = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "CHISQ.DIST.RT"}
let chiSqInv = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "CHISQ.INV"}
let chiSqInvRt = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "CHISQ.INV.RT"}
let chiSqTest = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "CHISQ.TEST"}
let confidenceNorm = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "CONFIDENCE.NORM"}
let confidenceT = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "CONFIDENCE.T"}
let correl = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "CORREL"}
let countBlank = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "COUNTBLANK"}
let countIf = { inputs = [SimpleType TypeEnum.General; SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "COUNTIF"}
let countIfs = { inputs = [ for i in 1..254 -> ignore i; SimpleType TypeEnum.General ]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "COUNTIFS" }
let covarianceP = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "COVARIANCE.P"}
let covarianceS = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "COVARIANCE.S"}
let exponDist = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "EXPON.DIST"}
let fDist = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "F.DIST"}
let fDistRt = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "F.DIST.RT"}
let fInv = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "F.INV"}
let fInvRt = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "F.INV.RT"}
let fTest = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "F.TEST"}
let fisher = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "FISHER"}
let fisherinv = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "FISHERINV"}
let forecast = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "FORECAST"}
let forecastEts = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "FORECAST.ETS"}
let forecastEtsConfInt = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "FORECAST.ETS.CONFINT"}
let forecastEtsSeasonality = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "FORECAST.ETS.SEASONALITY"}
let forecastEtsStat = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "FORECAST.ETS.STAT"}
let forecastLinear = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "FORECAST.LINEAR"}
let frequency = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "FREQUENCY"}
let gamma = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "GAMMA"}
let gammaDist = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "GAMMA.DIST"}
let gammaInv = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "GAMMA.INV"}
let gammaLn = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "GAMMALN"}
let gammaLnPrecise = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "GAMMALN.PRECISE"}
let gauss = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "GAUSS"}
let growth = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "GROWTH"}
let hypgeomDist = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "HYPGEOM.DIST"}
let intercept = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "INTERCEPT"}
let large = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "LARGE"}
let linest = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "LINEST"}
let logest = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "LOGEST"}
let logNormDist = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "LOGNORM.DIST"}
let logNormInv = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "LOGNORM.INV"}
let maxIfs = { inputs = SimpleType TypeEnum.Numeric :: [ for i in 1..254 -> ignore i; SimpleType TypeEnum.General ]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "MAXIFS" }
let minIfs = { inputs = SimpleType TypeEnum.Numeric :: [ for i in 1..254 -> ignore i; SimpleType TypeEnum.General ]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "MINIFS" }
let negBinomDist = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "NEGBINOM.DIST"}
let normDist = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "NORM.DIST"}
let normInv = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "NORM.INV"}
let normSDist = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "NORM.S.DIST"}
let normSInv = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "NORM.S.INV"}
let pearson = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "PEARSON"}
let percentileExc = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "PERCENTILE.EXC"}
let percentileInc = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "PERCENTILE.INC"}
let percentrankExc = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "PERCENTRANK.EXC"}
let percentrankInc = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "PERCENTRANK.INC"}
let permut = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "PERMUT"}
let permutationa = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "PERMUTATIONA"}
let phi = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "PHI"}
let poissonDist = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "POISSON.DIST"}
let prob = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "PROB"}
let quartileInc = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "QUARTILE.INC"}
let quartileExc = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "QUARTILE.EXC"}
let rankAvg = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "RANK.AVG"}
let rankEq = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "RANK.EQ"}
let rsq = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "RSQ"}
let slope = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "SLOPE"}
let small = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "SMALL"}
let standardize = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "STANDARDIZE"}
let steyx = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "STEYX"}
let tDist = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 3; repr = "T.DIST"}
let tDist2t = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "T.DIST.2T"}
let tDistRt = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "T.DIST.RT"}
let tInv = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "T.INV"}
let tInv2t = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "T.INV.2T"}
let tTest = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "T.TEST"}
let trend = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "TREND"}
let trimmean = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "TRIMMEAN"}
let weibullDist = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.Numeric; minArity = 4; repr = "WEIBULL.DIST"}
let zTest = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "Z.TEST"}

// Text
let arrayToText = { inputs = [SimpleType TypeEnum.General; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Str; minArity = 1; repr = "ARRAYTOTEXT" }
let asc = { inputs = [SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Str; minArity = 1; repr = "ASC" }
let bahtText = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Str; minArity = 1; repr = "BAHTTEXT" }
let char = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Str; minArity = 1; repr = "CHAR" }
let clean = { inputs = [SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Str; minArity = 1; repr = "CLEAN" }
let code = { inputs = [SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "CODE" }
let dbcs = { inputs = [SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Str; minArity = 1; repr = "DBCS" }
let dollar = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Str; minArity = 1; repr = "DOLLAR" }
let exact = { inputs = [SimpleType TypeEnum.Str; SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Bool; minArity = 2; repr = "EXACT" }
let find = { inputs = [SimpleType TypeEnum.Str; SimpleType TypeEnum.Str; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "FIND" }
let findB = { inputs = [SimpleType TypeEnum.Str; SimpleType TypeEnum.Str; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 2; repr = "FINDB" }
let fixed' = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Bool]; output = SimpleType TypeEnum.Str; minArity = 1; repr = "FIXED" }
let left = { inputs = [SimpleType TypeEnum.Str; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Str; minArity = 2; repr = "LEFT" }
let leftB = { inputs = [SimpleType TypeEnum.Str; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Str; minArity = 2; repr = "LEFTB" }
let len = { inputs = [SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "LEN" }
let lenB = { inputs = [SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "LENB" }
let lower = { inputs = [SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Str; minArity = 1; repr = "LOWER" }
let mid = { inputs = [SimpleType TypeEnum.Str; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Str; minArity = 3; repr = "MID" }
let midB = { inputs = [SimpleType TypeEnum.Str; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Str; minArity = 3; repr = "MIDB" }
let numberValue = { inputs = [SimpleType TypeEnum.Str; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "NUMBERVALUE" }
let phonetic = { inputs = [SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Str; minArity = 1; repr = "PHONETIC" }
let proper = { inputs = [SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Str; minArity = 1; repr = "PROPER" }
let replace = { inputs = [SimpleType TypeEnum.Str; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Str; minArity = 4; repr = "REPLACE" }
let replaceB = { inputs = [SimpleType TypeEnum.Str; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Str; minArity = 4; repr = "REPLACEB" }
let rept = { inputs = [SimpleType TypeEnum.Str; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Str; minArity = 2; repr = "REPT" }
let right = { inputs = [SimpleType TypeEnum.Str; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Str; minArity = 1; repr = "RIGHT" }
let rightB = { inputs = [SimpleType TypeEnum.Str; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Str; minArity = 1; repr = "RIGHTB" }
let search = { inputs = [SimpleType TypeEnum.Str; SimpleType TypeEnum.Str; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Str; minArity = 2; repr = "SEARCH" }
let searchB = { inputs = [SimpleType TypeEnum.Str; SimpleType TypeEnum.Str; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Str; minArity = 2; repr = "SEARCHB" }
let substitute = { inputs = [SimpleType TypeEnum.Str; SimpleType TypeEnum.Str; SimpleType TypeEnum.Str; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Str; minArity = 3; repr = "SUBSTITUTE" }
let t = { inputs = [SimpleType TypeEnum.General]; output = SimpleType TypeEnum.Str; minArity = 1; repr = "T" }
let text = { inputs = [SimpleType TypeEnum.Numeric; SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Str; minArity = 2; repr = "TEXT" }
let textJoin = { inputs = [SimpleType TypeEnum.Str; SimpleType TypeEnum.Bool; ] @ [ for i in 1..252 -> ignore i; SimpleType TypeEnum.Str ]; output = SimpleType TypeEnum.Str; minArity = 3; repr = "TEXTJOIN" }
let trim = { inputs = [SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Str; minArity = 1; repr = "TRIM" }
let uniChar = { inputs = [SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Str; minArity = 1; repr = "UNICHAR" }
let uniCode = { inputs = [SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "UNICODE" }
let upper = { inputs = [SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Str; minArity = 1; repr = "UPPER" }
let value = { inputs = [SimpleType TypeEnum.Str]; output = SimpleType TypeEnum.Numeric; minArity = 1; repr = "VALUE" }
let valueToText = { inputs = [SimpleType TypeEnum.General; SimpleType TypeEnum.Numeric]; output = SimpleType TypeEnum.Str; minArity = 1; repr = "VALUETOTEXT" }

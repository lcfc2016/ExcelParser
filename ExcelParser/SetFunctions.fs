module SetFunctions

open Types


// Engineering
let imProduct = { input = ComplexType(Set[ TypeEnum.Numeric; TypeEnum.Str ]); output = ComplexType(Set[ TypeEnum.Numeric; TypeEnum.Str ]); repr = "IMPRODUCT"}
let imSum = { input = ComplexType(Set[ TypeEnum.Numeric; TypeEnum.Str ]); output = ComplexType(Set[ TypeEnum.Numeric; TypeEnum.Str ]); repr = "IMSUM"}

// Logical
let and' = { input = ComplexType(Set[ TypeEnum.Bool; TypeEnum.Numeric ]); output = SimpleType TypeEnum.Bool; repr = "AND" }
let or' = { input = ComplexType(Set[ TypeEnum.Bool; TypeEnum.Numeric ]); output = SimpleType TypeEnum.Bool; repr = "OR" }
let xor = { input = ComplexType(Set[ TypeEnum.Bool; TypeEnum.Numeric ]); output = SimpleType TypeEnum.Bool; repr = "XOR" }

// Lookup and Reference
let areas = { input = SimpleType TypeEnum.General; output = SimpleType TypeEnum.Numeric; repr = "AREAS" }

// Math and Trig
let multinomial = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "MULTINOMIAL" }
let product = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "PRODUCT" }
let sum = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "SUM" }
let sumProduct = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "SUMPRODUCT" }
let sumSq = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "SUMSQ" }

// Statistical
let avedev = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "AVEDEV"}
let average = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "AVERAGE" }
let averagea = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "AVERAGEA"}
let count = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "COUNT" }
let countA = { input = SimpleType TypeEnum.General; output = SimpleType TypeEnum.Numeric; repr = "COUNTA" }
let devSq = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "DEVSQ"}
let kurt = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "KURT"}
let geoMean = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "GEOMEAN"}
let harMean = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "HARMEAN"}
let max = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "MAX" }
let maxA = { input = SimpleType TypeEnum.General; output = SimpleType TypeEnum.General; repr = "MAXA" }
let median = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "MEDIAN" }
let min = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "MIN" }
let minA = { input = SimpleType TypeEnum.General; output = SimpleType TypeEnum.General; repr = "MINA" }
let modeMult = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "MODE.MULT" }
let modeSngl = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "MODE.SNGL" }
let skew = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "SKEW" }
let skewP = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "SKEW.P" }
let stDevP = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "STDEV.P" }
let stDevS = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "STDEV.S" }
let stDevA = { input = SimpleType TypeEnum.General; output = SimpleType TypeEnum.General; repr = "STDEVA" }
let stDevPA = { input = SimpleType TypeEnum.General; output = SimpleType TypeEnum.General; repr = "STDEVPA" }
let varP = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "VAR.P" }
let varS = { input = SimpleType TypeEnum.Numeric; output = SimpleType TypeEnum.Numeric; repr = "VAR.S" }
let varA = { input = SimpleType TypeEnum.General; output = SimpleType TypeEnum.Numeric; repr = "VARA" }
let varPA = { input = SimpleType TypeEnum.General; output = SimpleType TypeEnum.Numeric; repr = "VARPA" }

// Text
let concat = { input = SimpleType TypeEnum.Str; output = SimpleType TypeEnum.Str; repr = "CONCAT" }
let concatenate = { input = SimpleType TypeEnum.Str; output = SimpleType TypeEnum.Str; repr = "CONCATENATE" }

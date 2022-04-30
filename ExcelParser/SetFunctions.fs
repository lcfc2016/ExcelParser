module SetFunctions

open System
open Types


// Engineering

// Financial

// Logical
let and' = { input = ComplexType(Set[ TypeEnum.Bool; TypeEnum.Numeric]); output = (SimpleType TypeEnum.Bool); repr = "AND" }
let or' = { input = ComplexType(Set[ TypeEnum.Bool; TypeEnum.Numeric]); output = (SimpleType TypeEnum.Bool); repr = "OR" }
let xor = { input = ComplexType(Set[ TypeEnum.Bool; TypeEnum.Numeric]); output = (SimpleType TypeEnum.Bool); repr = "XOR" }

// Lookup and Reference
let areas = { input = (SimpleType TypeEnum.General); output = (SimpleType TypeEnum.Numeric); repr = "AREAS" }

// Math and Trig
let multinomial = { input = (SimpleType TypeEnum.Numeric); output = (SimpleType TypeEnum.Numeric); repr = "MULTINOMIAL" }
let product = { input = (SimpleType TypeEnum.Numeric); output = (SimpleType TypeEnum.Numeric); repr = "PRODUCT" }
let sum = { input = (SimpleType TypeEnum.Numeric); output = (SimpleType TypeEnum.Numeric); repr = "SUM" }

// Statistical
let average = { input = (SimpleType TypeEnum.Numeric); output = (SimpleType TypeEnum.Numeric); repr = "AVERAGE" }
let count = { input = (SimpleType TypeEnum.Numeric); output = (SimpleType TypeEnum.Numeric); repr = "COUNT" }
let counta = { input = (SimpleType TypeEnum.General); output = (SimpleType TypeEnum.Numeric); repr = "COUNTA" }
let max = { input = (SimpleType TypeEnum.Numeric); output = (SimpleType TypeEnum.Numeric); repr = "MAX" }
let min = { input = (SimpleType TypeEnum.Numeric); output = (SimpleType TypeEnum.Numeric); repr = "MIN" }

// Text
let concat = { input = (SimpleType TypeEnum.Str); output = (SimpleType TypeEnum.Str); repr = "CONCAT" }
let concatenate = { input = (SimpleType TypeEnum.Str); output = (SimpleType TypeEnum.Str); repr = "CONCATENATE" }

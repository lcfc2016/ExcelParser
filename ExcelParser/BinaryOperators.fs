module BinaryOperators


open Types

let add = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "+" }
let sub = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "-" }
let mult = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "*" }
let div = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "/" }
let expt = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 2; repr = "^" }
let binConcat = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Str); minArity = 2; repr = "&" }
let gt = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); minArity = 2; repr = ">" }
let lt = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); minArity = 2; repr = "<" }
let gte = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); minArity = 2; repr = ">=" }
let lte = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); minArity = 2; repr = "<=" }
let equality = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); minArity = 2; repr = "=" }
let inequality = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); minArity = 2; repr = "<>" }
module BinaryOperators

open System
open System.Text.RegularExpressions

open Types

let add = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "+" }
let sub = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "-" }
let mult = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "*" }
let div = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "/" }
let expt = { inputs = [(SimpleType TypeEnum.Numeric); (SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "^" }
let binConcat = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Str); repr = "&" }
let gt = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); repr = ">" }
let lt = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); repr = "<" }
let gte = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); repr = ">=" }
let lte = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); repr = "<=" }
let equality = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); repr = "=" }
let inequality = { inputs = [(SimpleType TypeEnum.General); (SimpleType TypeEnum.General)]; output = (SimpleType TypeEnum.Bool); repr = "<>" }
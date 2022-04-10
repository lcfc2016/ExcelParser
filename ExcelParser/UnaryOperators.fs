module UnaryOperators

open System
open Types

let negative = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "-" }
let positive = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "+" }

// Postfix operators
let percentage = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); repr = "%" }
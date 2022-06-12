module UnaryOperators

open Types

let negative = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "-" }
let positive = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "+" }

// Postfix operators
let percentage = { inputs = [(SimpleType TypeEnum.Numeric)]; output = (SimpleType TypeEnum.Numeric); minArity = 1; repr = "%" }
module GenericFunctions

open Types


// Logical
let if' = { inputs = [SimpleType TypeEnum.Bool]; minimumOutputs = 1; maximumOutputs = 2; repr = "IF" }
let ifError = { inputs = []; minimumOutputs = 2; maximumOutputs = 2; repr = "IFERROR" }
let ifNA = { inputs = []; minimumOutputs = 2; maximumOutputs = 2; repr = "IFNA" }

// Lookup & Reference
let choose = { inputs = [SimpleType TypeEnum.Numeric]; minimumOutputs = 1; maximumOutputs = 254; repr = "CHOOSE" }
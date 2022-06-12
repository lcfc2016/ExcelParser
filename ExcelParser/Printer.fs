module Printer

open System
open Types


let rec printParsedCell expr indentLevel =
    let indent = String.replicate indentLevel "  "
    match expr with
    | Leaf leaf ->
        match leaf with
        | Reference name ->
            printfn "%s%s" indent name
        | Constant (value, xlType) -> printfn "%s%s (%s)" indent value (xlType.print())
        | Range (minRow, maxRow, minCol, maxCol) -> printfn "%s%s%d:%s%d" indent minCol minRow maxCol maxRow
        | Sheet (sheet, reference) ->
            printfn "%s%s -> %s" indent sheet (match reference with
                                              | Reference name -> name
                                              | Constant (value, xlType) -> value
                                              | Range(minRow, maxRow, minCol, maxCol) ->
                                                sprintf "%s%d:%s%d" minCol minRow maxCol maxRow
                                              | _ -> invalidOp "Invalid sheet reference chaining detected")
    | Node node ->
        match node with
        | Unary (unary, arg) ->
            printfn "%s%s" indent unary.repr
            printParsedCell arg (indentLevel + 1)
        | Binary (op, left, right) ->
             printfn "%s%s" indent op.repr
             printParsedCell left (indentLevel + 1)
             printParsedCell right (indentLevel + 1)
        | Func (f, args) ->
            printfn "%s%s" indent f.repr
            Seq.iter (fun a -> printParsedCell a (indentLevel + 1)) args
        | SetFunc (f, args) ->
            printfn "%s%s" indent f.repr
            Seq.iter (fun a -> printParsedCell a (indentLevel + 1)) args
        | GenericFunc (f, args) ->
            printfn "%s%s" indent f.repr
            Seq.iter (fun a -> printParsedCell a (indentLevel + 1)) args
        | SwitchFunc args ->
            printfn "%sSWITCH" indent
            Seq.iter (fun a -> printParsedCell a (indentLevel + 1)) args
        | IfsFunc args ->
            printfn "%sIFS" indent
            Seq.iter (fun a -> printParsedCell a (indentLevel + 1)) args
    | Values values ->
        printfn "%s{%s}" indent (
            String.Join(", ", List.map (fun v -> match v with
                                                 | Constant (value, xlType) -> value
                                                 | _ -> invalidOp "Unexpected reference in literal array")
                                        values))
    | Union union ->
        printfn "%sUNION" indent
        Seq.iter (fun a -> printParsedCell a (indentLevel + 1)) union

let run cellName expr =
    printfn "%s=" cellName
    printParsedCell expr 1

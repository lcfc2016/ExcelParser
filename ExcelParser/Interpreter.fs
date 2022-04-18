module Interpreter

open System
open System.Collections.Generic
open Types


// Globals
let mutable cellLookup = Map<String, Map<String, ParsedCell>>[]
let mutable checkedCells = new Dictionary<String, Dictionary<String, XLType>>()
let mutable errorBuffer = new Queue<(String * String)>()

let typeJoin (types: list<XLType>) =
    String.concat ", " (List.map (fun (input: XLType) -> input.print()) types)

let condenseTypes (types: Set<XLType>) =
    let convertSimpleTypesToSets x =
        match x with
        | SimpleType x -> Set.empty.Add(x)
        | ComplexType x -> x
    let typeSet = (Set.unionMany (Set.map convertSimpleTypesToSets types))
    if Set.contains TypeEnum.General typeSet
    then SimpleType (TypeEnum.General)
    else if Set.count typeSet > 1
         then ComplexType (typeSet)
         else SimpleType (Set.minElement typeSet)

let constructError errorType f actual =
    match errorType with
    | TypeStatus.Mismatch ->
        match f with
        | FixedArity f -> f.repr + " expected " + f.inputList() + " got " + typeJoin actual
        | Generic f -> f.repr + " expected " + f.inputList() + " got " + typeJoin actual
        | Variadic f -> f.repr + " expected " + f.input.print() + " got " + typeJoin actual
    | TypeStatus.PartialHandling ->
        match f with
        | FixedArity f -> f.repr + " expected " + f.inputList() + " got " + typeJoin actual
        | Generic f -> f.repr + " expected " + f.inputList() + " got " + typeJoin actual
        | Variadic f -> f.repr + " expected " + f.input.print() + " got " + typeJoin actual
    | _ -> invalidOp "Match or unhandled error type passed to error handling"

let addError sheet cell errorType f actual =
    errorBuffer.Enqueue(sheet + ": " + cell, (constructError errorType f actual))

let checkTypes expected actual =
    match expected with
    | SimpleType expected ->
        if expected = TypeEnum.General
        then TypeStatus.Match
        else match actual with
             | SimpleType actual ->
                 if expected = actual
                 then TypeStatus.Match
                 else TypeStatus.Mismatch
             | ComplexType actual -> TypeStatus.PartialHandling
    | ComplexType expected ->
        match actual with
        | SimpleType actual ->
            if Set.contains actual expected
            then TypeStatus.Match
            else TypeStatus.Mismatch
        | ComplexType actual ->
            let intersection = Set.intersect expected actual
            if intersection.Count = (max expected.Count actual.Count)
            then TypeStatus.Match
            else if intersection.Count > 0
                    then TypeStatus.PartialHandling
                    else TypeStatus.Mismatch

let rec walkAST (sheet: String) (cell: String) expr : XLType =
    match expr with
    | Leaf leaf ->
        match leaf with
        | Reference name -> fetchTypeOrParseTypes sheet name
        | Constant (value, xlType) -> xlType
        | Range (minRow, maxRow, minCol, maxCol) ->
            let activeCells = Map.filter
                                (fun name (cell: ParsedCell) ->
                                    cell.row >= minRow
                                    && cell.row <= maxRow
                                    && cell.column >= minCol
                                    && cell.column <= maxCol)
                                (cellLookup.GetValueOrDefault(sheet))
            let returnedTypes = Set.map (fun name -> fetchTypeOrParseTypes sheet name) (Set (Seq.map fst (Map.toSeq activeCells)))
            condenseTypes returnedTypes
        | Sheet (sheet, reference) ->
            match reference with
            | Reference name -> fetchTypeOrParseTypes sheet name
            | Range (minRow, maxRow, minCol, maxCol) ->
                let activeCells = Map.filter
                                    (fun name (cell: ParsedCell) ->
                                        cell.row >= minRow
                                        && cell.row <= maxRow
                                        && cell.column >= minCol
                                        && cell.column <= maxCol)
                                    (cellLookup.GetValueOrDefault(sheet))
                let returnedTypes = Set.map (fun name -> fetchTypeOrParseTypes sheet name) (Set (Seq.map fst (Map.toSeq activeCells)))
                condenseTypes returnedTypes
            | _-> invalidOp ("Cross-sheet reference in cell " + cell + " not followed by cell or range")
    | Node node ->
        match node with
        | Unary (unary, arg) ->
            let actual = (walkAST sheet cell arg)
            let typeStatus = checkTypes unary.inputs.Head actual
            if typeStatus <> TypeStatus.Match
            then addError sheet cell typeStatus (FixedArity unary) [actual]
            unary.output
        | Binary (op, left, right) ->
            let actualLeft = (walkAST sheet cell left)
            let actualRight = (walkAST sheet cell right)
            let leftOpTypeStatus = checkTypes op.inputs.Head actualLeft
            let rightOpTypeStatus = checkTypes op.inputs.Head actualLeft
            if leftOpTypeStatus <> TypeStatus.Match || rightOpTypeStatus <> TypeStatus.Match
            then addError sheet cell (max leftOpTypeStatus rightOpTypeStatus) (FixedArity op) [actualLeft; actualRight]
            op.output
        | Func (f, args) ->
            let actualTypes = List.map (fun t -> walkAST sheet cell t) args
            let typeStatuses = List.map2 (fun e a -> checkTypes e a) f.inputs actualTypes
            if List.length args > 0 && List.max typeStatuses > TypeStatus.Match
            then addError sheet cell (List.max typeStatuses) (FixedArity f) actualTypes
            f.output
        | SetFunc (f, args) ->
            let actualTypes = List.map (fun t -> walkAST sheet cell t) args
            let typeStatuses = List.map (fun a -> checkTypes f.input a) actualTypes
            if List.max typeStatuses > TypeStatus.Match
            then addError sheet cell (List.max typeStatuses) (Variadic f) [(condenseTypes (Set actualTypes))]
            f.output
        | GenericFunc (f, args) ->
            let inputs, outputs = List.splitAt f.inputs.Length args
            let inputTypes = List.map (fun t -> walkAST sheet cell t) inputs
            let typeStatuses = List.map2 (fun e a -> checkTypes e a) f.inputs inputTypes
            if List.max typeStatuses > TypeStatus.Match
            then addError sheet cell (List.max typeStatuses) (Generic f) inputTypes
            condenseTypes (Set.map (fun t -> walkAST sheet cell t) (Set outputs))
        | CaseStatement cases ->
            (SimpleType TypeEnum.General) // TODO
    | Values values ->
        condenseTypes (Set.map (fun v -> match v with
                                         | Constant (value, xlType) -> xlType
                                         | _ -> invalidOp "Unexpected reference in literal array")
                                (Set.ofList values))

and fetchTypeOrParseTypes sheet cell =
    checkedCells.GetValueOrDefault(sheet).GetValueOrDefault(
        cell,
        walkAST sheet cell (cellLookup.GetValueOrDefault(sheet).GetValueOrDefault(cell).ast)
    )

let typeCheckSheet sheet (cellMap: Map<String, ParsedCell>) =
    Map.map (fun address (cell: ParsedCell) -> (fetchTypeOrParseTypes sheet address)) cellMap

let run (astMap: Map<String, Map<String, ParsedCell>>) =
    // Initialise global AST lookup and dictionary of known cell types
    cellLookup <- astMap
    Map.iter (fun sheet contents -> checkedCells.Add(sheet, new Dictionary<String, XLType>())) astMap
    // Type check
    ignore (Map.map (fun sheet contents -> typeCheckSheet sheet contents) astMap)
    errorBuffer
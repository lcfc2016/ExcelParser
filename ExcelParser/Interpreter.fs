module Interpreter

open System
open System.Collections.Generic
open Types


// Globals
let mutable cellLookup = Map<String, Map<String, ParsedCell>>[]
let mutable checkedCells = new Dictionary<String, Dictionary<String, XLType>>()
let mutable errorBuffer = new Queue<Error>()

let typeJoin (types: list<XLType>) =
    String.concat "," (List.map (fun (input: XLType) -> input.print()) types)

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

let getFunctionDetails f =
    match f with
    | FixedArity f -> (f.repr, f.inputList())
    | Generic f -> (f.repr, f.inputList())
    | Variadic f -> (f.repr, f.input.print())
    | _ -> ("SWITCH", "")

let addError sheet cell errorType f actual =
    let funcDetails = getFunctionDetails f
    errorBuffer.Enqueue({
        sheet = sheet;
        cell = cell;
        f = (fst funcDetails);
        expected = (snd funcDetails);
        actual = typeJoin actual;
        errorType = errorType.ToString();
        errorMessage = ""
    })

let checkTypes expected actual =
    match expected with
    | SimpleType expected ->
        if expected = TypeEnum.General
        then TypeStatus.Match
        else match actual with
             | SimpleType actual ->
                // Debatable as to whether the latter clause (and line 61) to accept general for actual or not is the correct choice, add as strict flag?
                 if expected = actual || actual = TypeEnum.General
                 then TypeStatus.Match
                 else TypeStatus.Mismatch
             | ComplexType actual -> TypeStatus.PartialHandling
    | ComplexType expected ->
        match actual with
        | SimpleType actual ->
            if Set.contains actual expected || actual = TypeEnum.General
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
                                    match cell with
                                    | Success cell ->
                                        cell.row >= minRow
                                        && cell.row <= maxRow
                                        && cell.column >= minCol
                                        && cell.column <= maxCol
                                    | Failure f -> false)
                                (cellLookup.GetValueOrDefault(sheet))
            let returnedTypes = Set.map (fun name -> fetchTypeOrParseTypes sheet name) (Set (Seq.map fst (Map.toSeq activeCells)))
            if returnedTypes.IsEmpty
            then SimpleType(TypeEnum.General)
            else condenseTypes returnedTypes
        | Sheet (sheetRef, reference) ->
            if cellLookup.ContainsKey(sheetRef)
            then
                match reference with
                | Reference name -> fetchTypeOrParseTypes sheetRef name
                | Range (minRow, maxRow, minCol, maxCol) ->
                    let activeCells = Map.filter
                                        (fun name (cell: ParsedCell) ->
                                            match cell with
                                            | Success cell ->
                                                cell.row >= minRow
                                                && cell.row <= maxRow
                                                && cell.column >= minCol
                                                && cell.column <= maxCol
                                            | Failure f -> false)
                                        (cellLookup.GetValueOrDefault(sheetRef))
                    let returnedTypes = Set.map (fun name -> fetchTypeOrParseTypes sheetRef name) (Set (Seq.map fst (Map.toSeq activeCells)))
                    if returnedTypes.IsEmpty
                    then SimpleType(TypeEnum.General)
                    else condenseTypes returnedTypes
                | _-> invalidOp ("Cross-sheet reference in cell " + cell + " not followed by cell or range")
            else
                errorBuffer.Enqueue({
                    sheet = sheet;
                    cell = cell;
                    f = "";
                    expected = "";
                    actual = "";
                    errorType = "parseError";
                    errorMessage = "Invalid sheet reference: " + sheetRef
                })
                SimpleType (TypeEnum.General)
    | Node node ->
        let getType = walkAST sheet cell
        match node with
        | Unary (unary, arg) ->
            let actual = getType arg
            let typeStatus = checkTypes unary.inputs.Head actual
            if typeStatus <> TypeStatus.Match
            then addError sheet cell typeStatus (FixedArity unary) [actual]
            unary.output
        | Binary (op, left, right) ->
            let actualLeft = getType left
            let actualRight = getType right
            let leftOpTypeStatus = checkTypes op.inputs.Head actualLeft
            let rightOpTypeStatus = checkTypes op.inputs.Head actualLeft
            if leftOpTypeStatus <> TypeStatus.Match || rightOpTypeStatus <> TypeStatus.Match
            then addError sheet cell (max leftOpTypeStatus rightOpTypeStatus) (FixedArity op) [actualLeft; actualRight]
            op.output
        | Func (f, args) ->
            let actualTypes = List.map getType args
            let typeStatuses = List.map2 (fun e a -> checkTypes e a) (List.take(actualTypes.Length) f.inputs) actualTypes
            if List.length args > 0 && List.max typeStatuses > TypeStatus.Match
            then addError sheet cell (List.max typeStatuses) (FixedArity f) actualTypes
            f.output
        | SetFunc (f, args) ->
            let actualTypes = List.map getType args
            let typeStatuses = List.map (fun a -> checkTypes f.input a) actualTypes
            if List.max typeStatuses > TypeStatus.Match
            then addError sheet cell (List.max typeStatuses) (Variadic f) [(condenseTypes (Set actualTypes))]
            f.output
        | GenericFunc (f, args) ->
            let inputs, outputs = List.splitAt f.inputs.Length args
            let inputTypes = List.map getType inputs
            let typeStatuses = List.map2 (fun e a -> checkTypes e a) f.inputs inputTypes
            if (not typeStatuses.IsEmpty) && List.max typeStatuses > TypeStatus.Match
            then addError sheet cell (List.max typeStatuses) (Generic f) inputTypes
            condenseTypes (Set.map getType (Set outputs))
        | SwitchFunc args ->
            let inputType = getType args.Head
            let rec recur remaining outTypes =
                match remaining with
                | [] -> condenseTypes (Set.ofList outTypes)
                | [x] -> recur [] ((getType x) :: outTypes)
                | x::xs ->
                    let thisInputType = getType x
                    let typeCheck = checkTypes inputType thisInputType
                    if typeCheck > TypeStatus.Match
                    then errorBuffer.Enqueue({
                        sheet = sheet;
                        cell = cell;
                        f = "SWITCH";
                        expected = inputType.print();
                        actual = thisInputType.print();
                        errorType = typeCheck.ToString();
                        errorMessage = ""
                    })
                    recur xs.Tail ((getType (xs.Head)) :: outTypes)
            recur args.Tail []
        | IfsFunc args ->
            let inputType = (SimpleType TypeEnum.Bool)
            let rec recur remaining outTypes =
                match remaining with
                | [] -> condenseTypes (Set.ofList outTypes)
                | x::xs ->
                    let thisInputType = getType x
                    let typeCheck = checkTypes inputType thisInputType
                    if typeCheck > TypeStatus.Match
                    then errorBuffer.Enqueue({
                        sheet = sheet;
                        cell = cell;
                        f = "IFS";
                        expected = inputType.print();
                        actual = thisInputType.print();
                        errorType = typeCheck.ToString();
                        errorMessage = ""
                    })
                    recur xs.Tail ((getType (xs.Head)) :: outTypes)
            recur args []
    | Values values ->
        condenseTypes (Set.map (fun v -> match v with
                                         | Constant (value, xlType) -> xlType
                                         | _ -> invalidOp "Unexpected reference in literal array")
                                (Set.ofList values))
    | Union union ->
        condenseTypes (Set.map (fun v -> walkAST sheet cell v) (Set.ofList union))

and fetchTypeOrParseTypes sheet cell =
    if cellLookup.ContainsKey(sheet)
    then
        if checkedCells.GetValueOrDefault(sheet).ContainsKey(cell)
        then checkedCells.GetValueOrDefault(sheet).GetValueOrDefault(cell)
        else
            let foundType = (
                if cellLookup.GetValueOrDefault(sheet).ContainsKey(cell)
                then
                    walkAST sheet cell (
                        match cellLookup.GetValueOrDefault(sheet).GetValueOrDefault(cell) with
                        | Success s -> s.ast
                        | Failure f -> Leaf ( Constant ( f, SimpleType (TypeEnum.General) ) )
                    )
                else SimpleType (TypeEnum.General)
            )
            checkedCells.GetValueOrDefault(sheet).Add(cell, foundType)
            foundType
    else
        errorBuffer.Enqueue({
            sheet = sheet;
            cell = cell;
            f = "";
            expected = "";
            actual = "";
            errorType = "parseError";
            errorMessage = "Invalid sheet reference: " + sheet
        })
        SimpleType (TypeEnum.General)

let typeCheckSheet sheet (cellMap: Map<String, ParsedCell>) =
    Map.map (fun address (cell: ParsedCell) ->
                match cell with
                | Success s -> (fetchTypeOrParseTypes sheet address)
                | Failure f ->
                    errorBuffer.Enqueue({
                        sheet = sheet;
                        cell = address;
                        f = "";
                        expected = "";
                        actual = "";
                        errorType = "parseError";
                        errorMessage = f
                    })
                    SimpleType (TypeEnum.General))
            cellMap

let run (astMap: Map<String, Map<String, ParsedCell>>) =
    // Initialise errorFormat, global AST lookup and dictionary of known cell types
    cellLookup <- astMap
    Map.iter (fun sheet contents -> checkedCells.Add(sheet, new Dictionary<String, XLType>())) astMap
    // Type check
    ignore (Map.map (fun sheet contents -> typeCheckSheet sheet contents) astMap)
    errorBuffer
module Interpreter

open System
open System.Collections.Generic
open Types


// Globals
let mutable cellLookup = Map<String, Map<String, ParsedCell>>[]
let mutable checkedCells = new Dictionary<String, Dictionary<String, XLType>>()
let mutable errorBuffer = new Queue<Error>()

let radix26 (str: string) =
    str.ToCharArray()
    |> List.ofArray
    |> List.rev
    |> List.mapi (fun i c -> (float ((int c) - 64)) * (26.0 ** (float i)))
    |> List.reduce (fun x y -> x + y)
    |> int

let typeJoin (types: list<XLType>) =
    String.concat "," (List.map (fun (input: XLType) -> input.print()) types)

let condenseTypes (types: Set<XLType>) reduceToGeneral =
    let convertSimpleTypesToSets x =
        match x with
        | SimpleType x -> Set.empty.Add(x)
        | ComplexType x -> x
    let typeSet = (Set.unionMany (Set.map convertSimpleTypesToSets types))
    if Set.contains TypeEnum.General typeSet && reduceToGeneral
    then SimpleType (TypeEnum.General)
    else if Set.count typeSet > 1
         then ComplexType (typeSet)
         else SimpleType (Set.minElement typeSet)

let getFunctionDetails f (actual: list<XLType>) =
    match f with
    | FixedArity f -> (f.repr, f.inputList(actual.Length))
    | Generic f -> (f.repr, f.inputList())
    | Variadic f -> (f.repr, f.input.print())
    | _ -> ("SWITCH", "")

let addError sheet cell errorType f (actual: list<XLType>) =
    let funcDetails = getFunctionDetails f actual
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

let rec walkAST (sheet: String) (cell: String) (visited: Set<string>) expr : XLType =
    match expr with
    | Leaf leaf ->
        match leaf with
        | Reference name ->
            let loc = sheet + "!" + name
            if visited.Contains loc then invalidOp ("Circular reference detected: " + loc)
            fetchTypeOrParseTypes sheet name (visited.Add loc)
        | Constant (value, xlType) -> xlType
        | Range (minRow, maxRow, minCol, maxCol) ->
            let activeCells = Map.filter
                                (fun name (cell: ParsedCell) ->
                                    match cell with
                                    | Success cell ->
                                        let colRadix26 = radix26 cell.column
                                        cell.row >= minRow
                                        && cell.row <= maxRow
                                        && colRadix26 >= radix26 minCol
                                        && colRadix26 <= radix26 maxCol
                                    | Failure f -> false)
                                (cellLookup.GetValueOrDefault(sheet))
            let returnedTypes = Set.map (fun name ->
                let loc = sheet + "!" + name
                if visited.Contains loc then invalidOp ("Circular reference detected: " + loc)
                fetchTypeOrParseTypes sheet name (visited.Add loc)) (Set (Seq.map fst (Map.toSeq activeCells)))
            if returnedTypes.IsEmpty
            then SimpleType(TypeEnum.General)
            else condenseTypes returnedTypes true
        | Sheet (sheetRef, reference) ->
            if cellLookup.ContainsKey(sheetRef)
            then
                match reference with
                | Reference name ->
                    let loc = sheetRef + "!" + name
                    if visited.Contains loc then invalidOp ("Circular reference detected: " + loc)
                    fetchTypeOrParseTypes sheetRef name (visited.Add loc)
                | Range (minRow, maxRow, minCol, maxCol) ->
                    let activeCells = Map.filter
                                        (fun name (cell: ParsedCell) ->
                                            match cell with
                                            | Success cell ->
                                                let colRadix26 = radix26 cell.column
                                                cell.row >= minRow
                                                && cell.row <= maxRow
                                                && colRadix26 >= radix26 minCol
                                                && colRadix26 <= radix26 maxCol
                                            | Failure f -> false)
                                        (cellLookup.GetValueOrDefault(sheetRef))
                    let returnedTypes = Set.map (fun name ->
                        let loc = sheetRef + "!" + name
                        if visited.Contains loc then invalidOp ("Circular reference detected: " + loc)
                        fetchTypeOrParseTypes sheetRef name (visited.Add loc)) (Set (Seq.map fst (Map.toSeq activeCells)))
                    if returnedTypes.IsEmpty
                    then SimpleType(TypeEnum.General)
                    else condenseTypes returnedTypes true
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
        let getType = walkAST sheet cell visited
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
            then addError sheet cell (List.max typeStatuses) (Variadic f) [(condenseTypes (Set actualTypes) false)]
            f.output
        | GenericFunc (f, args) ->
            let inputs, outputs = List.splitAt f.inputs.Length args
            let inputTypes = List.map getType inputs
            let typeStatuses = List.map2 (fun e a -> checkTypes e a) f.inputs inputTypes
            if (not typeStatuses.IsEmpty) && List.max typeStatuses > TypeStatus.Match
            then addError sheet cell (List.max typeStatuses) (Generic f) inputTypes
            condenseTypes (Set.map getType (Set outputs)) true
        | SwitchFunc args ->
            let inputType = getType args.Head
            let rec recur remaining outTypes =
                match remaining with
                | [] -> condenseTypes (Set.ofList outTypes) true
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
                | [] -> condenseTypes (Set.ofList outTypes) true
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
                      true
    | Union union ->
        condenseTypes (Set.map (fun v -> walkAST sheet cell visited v) (Set.ofList union)) true

and fetchTypeOrParseTypes sheet cell (visited: Set<string>) =
    if cellLookup.ContainsKey(sheet)
    then
        if checkedCells.GetValueOrDefault(sheet).ContainsKey(cell)
        then checkedCells.GetValueOrDefault(sheet).GetValueOrDefault(cell)
        else
            let foundType = (
                if cellLookup.GetValueOrDefault(sheet).ContainsKey(cell)
                then
                    walkAST sheet cell visited (
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
                | Success s -> (fetchTypeOrParseTypes sheet address Set.empty)
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
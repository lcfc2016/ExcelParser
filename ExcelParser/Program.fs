open System
open System.IO
open System.Collections.Generic
open Types


let createAST input isFormula namedRanges =
    Tokeniser.run input isFormula namedRanges |> Parser.run

// DEBUG FUNCTIONS

let debugParseASTs filename cellAddress =
    // Debug function, fetch specific cell's AST
    let namedRanges = XLSXReader.getNamedRanges filename
    printfn "%s" filename
    Map.iter (fun name contents ->
               printfn "%s" name
               ignore [ for (cell: UnparsedCell) in contents ->
                        if cell.address = cellAddress then Printer.run cell.address (createAST cell.value cell.isFormula namedRanges)])
             (XLSXReader.xlsxReader(filename))

let debugNamedRanges filename =
    // Dump named range input in readable format
    let namedRanges = XLSXReader.getNamedRanges filename
    for range in namedRanges do
        printfn "%s -> %s" range.Key (range.Value.ranges.ToString())

let typeCheckWithoutNamedRanges filename =
    // As per parseAndTypeCheck below (i.e. default behaviour) but without named ranges
    let astMap = Map.map (fun name contents ->
        Map [ for (cell: UnparsedCell) in contents ->
               ( cell.address,
                   try
                       Success ({ column = cell.column; row = cell.row; ast = (createAST cell.value cell.isFormula Map.empty) })
                   with
                       | :? InvalidOperationException as ex -> Failure (ex.Message))])
                        (XLSXReader.xlsxReader(filename))
    Interpreter.run astMap

// OUTPUT FUNCTIONS

let parseAndPrintASTs filename =
    // -p option
    let namedRanges = XLSXReader.getNamedRanges filename
    printfn "%s" filename
    Map.iter (fun name contents ->
               printfn "%s" name
               ignore [ for (cell: UnparsedCell) in contents ->
                           try
                               Printer.run cell.address (createAST cell.value cell.isFormula namedRanges)
                           with
                               | :? InvalidOperationException as ex -> printfn "%s -> Unable to parse: %s" cell.address ex.Message ])
             (XLSXReader.xlsxReader(filename))

let printTypeOutput (errors: Queue<Error>) =
    for error in errors do
        printfn "%s, %s, %s -> Expected %s, Got %s, %s, %s" error.sheet error.cell error.f error.expected error.actual error.errorType error.errorMessage

let parseAndTypeCheck filename =
    // Fetch named ranges, then tokenise and parse, any failures are handled and then the ASTs passed to the type checker
    let namedRanges = XLSXReader.getNamedRanges filename
    let astMap = Map.map (fun name contents ->
        Map [ for (cell: UnparsedCell) in contents ->
                ( cell.address,
                    try
                        Success ({ column = cell.column; row = cell.row; ast = (createAST cell.value cell.isFormula namedRanges) })
                    with
                        | :? InvalidOperationException as ex -> Failure (ex.Message))])
                    (XLSXReader.xlsxReader(filename))
    Interpreter.run astMap

let typeCheckAndPrint filename =
    // Default output
    printfn "%s" filename
    try
        parseAndTypeCheck filename |> printTypeOutput
    with
        | :? InvalidOperationException as ex ->
            printfn "Failed to type check %s: %s" filename ex.Message

let errorsToCSVFormat filename (errors: Queue<Error>) =
    Seq.map(fun (error: Error) ->
        sprintf "\"%s\",\"%s\",\"%s\",\"%s\",\"%s\",\"%s\",\"%s\",\"%s\"" filename error.sheet error.cell error.f error.expected error.actual error.errorType error.errorMessage) errors

let rec generateOutputName (filename: string) attempt =
    // Checks if previous output exists, increments attempt number and tries again if so
    let output = Path.GetDirectoryName(filename) + @"\" + "output_" + Path.GetFileNameWithoutExtension(filename) + (if attempt < 1 then "" else "_" + attempt.ToString()) + ".csv"
    if File.Exists output
    then generateOutputName filename (attempt + 1)
    else output

let typeCheckingToCSV (filename: string) =
    // -c option, parses and type checks, then formats the errors to CSV before outputting. Prints a status to STDOUT
    printf "Starting: %s, " filename
    try
        let errors = parseAndTypeCheck filename
        if errors.Count > 0
        then
            printfn "errors"
            use outFile = new StreamWriter(generateOutputName filename 0)
            outFile.WriteLine("file,sheet,cell,function,expected,actual,error_type,error_message")
            errors |> (errorsToCSVFormat filename) |> Seq.iter outFile.WriteLine
        else
            printfn "finished"
    with
        | ex ->
            printfn "failed: %s" ex.Message

[<EntryPoint>]
let main argv =
#if DEBUG
    let testFile = @"C:\Users\bjs73\Documents\MSc\IRP\test_sheet_1.xlsx"
    let files = [@"C:\Users\bjs73\Documents\MSc\IRP\test_sheet_1.xlsx"; @"C:\Users\bjs73\Documents\wedding_guests.xlsx"]
    List.iter typeCheckAndPrint files
    //let code = @"-3^4+1"
    //ignore (createAST code true Map.empty|> (Printer.run "A1"))
    //debugNamedRanges testFile
    //debugParseASTs testFile "W10"
    //printTypeOutput Interpreter.errorBuffer
    //ignore (XLSXReader.testXLRead testFile)
    //typeCheckWithoutNamedRanges testFile |> printTypeOutput
    //parseAndPrintASTs testFile
    //typeCheckAndPrint testFile
    //typeCheckingToCSV testFile
#else
    // Check argument provided, if not or -h, advise. If lead arg is a flag, check that files follow, and take appropriate
    // action. If no flag use default type-check and print behaviour. If bad flag advise
    if argv.Length < 1 || argv.[0] = "-h"
    then printfn "Please provide xlsx files to parse and type check. Flag -p enables AST print mode, -c CSV output mode"
    elif argv.[0].[0] = '-'
    then
        let files = List.tail (List.ofArray argv)
        if argv.Length < 2
        then printfn "Please provide xlsx files"
        elif argv.[0] = "-p" then List.iter parseAndPrintASTs files
        elif argv.[0] = "-c" then List.iter typeCheckingToCSV files
        else printfn "%s is not a valid option, expecting '-c' for CSV output, or '-p' for printout of AST" argv.[0]
    else
        List.iter typeCheckAndPrint (List.ofArray argv)
#endif
    0 // return an integer exit code
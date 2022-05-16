// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.IO
open System.Collections.Generic
open Types


let createAST input isFormula namedRanges =
    Tokeniser.run input isFormula namedRanges |> Parser.run

let parseAndPrintASTs filename =
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

let debugParseASTs filename cellAddress =
    let namedRanges = XLSXReader.getNamedRanges filename
    printfn "%s" filename
    Map.iter (fun name contents ->
               printfn "%s" name
               ignore [ for (cell: UnparsedCell) in contents ->
                        if cell.address = cellAddress then Printer.run cell.address (createAST cell.value cell.isFormula namedRanges)])
             (XLSXReader.xlsxReader(filename))

let debugNamedRanges filename =
    let namedRanges = XLSXReader.getNamedRanges filename
    for range in namedRanges do
        printfn "%s -> %s" range.Key (range.Value.ranges.ToString())

let printTypeOutput (errors: Queue<Error>) =
    for error in errors do
        printfn "%s, %s, %s -> Expected %s, Got %s, %s, %s" error.sheet error.cell error.f error.expected error.actual error.errorType error.errorMessage

let parseAndTypeCheck filename =
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
    printfn "%s" filename
    try
        parseAndTypeCheck filename |> printTypeOutput
    with
        | :? InvalidOperationException as ex ->
            printfn "Failed to type check %s: %s" filename ex.Message

let errorsToCSVFormat filename (errors: Queue<Error>) =
    Seq.map(fun (error: Error) ->
        // String.Join(",", filename, error.sheet, error.cell, error.f, error.expected, error.actual, error.errorType, error.errorMessage)) errors
        sprintf "\"%s\",\"%s\",\"%s\",\"%s\",\"%s\",\"%s\",\"%s\",\"%s\"" filename error.sheet error.cell error.f error.expected error.actual error.errorType error.errorMessage) errors

let rec generateOutputName (filename: string) attempt =
    let output = Path.GetDirectoryName(filename) + @"\" + "output_" + Path.GetFileNameWithoutExtension(filename) + (if attempt < 1 then "" else "_" + attempt.ToString()) + ".csv"
    if File.Exists output
    then generateOutputName filename (attempt + 1)
    else output

let typeCheckingToCSV (filename: string) =
    printfn "Starting: %s" filename
    try
        let errors = parseAndTypeCheck filename
        if errors.Count > 0
        then
            printfn "Errors found: %s" filename
            use outFile = new StreamWriter(generateOutputName filename 0)
            outFile.WriteLine("file,sheet,cell,function,expected,actual,error_type,error_message")
            errors |> (errorsToCSVFormat filename) |> Seq.iter outFile.WriteLine
    with
        | ex ->
            printfn "Error %s: %s" filename ex.Message
    printfn "Finished: %s" filename

[<EntryPoint>]
let main argv =
#if DEBUG
    let testFile = @"C:/Users/bjs73/Documents/MSc/IRP/final_dataset/info1/Student006_1FAULTS_FAULTVERSION2.xlsx"
    //let testFile = @"C:\Users\bjs73\Documents\MSc\IRP\test_sheet_1.xlsx"
    let code = "-E92*C76"
    //debugNamedRanges testFile
    //debugParseASTs testFile
    //createAST code true Map.empty |> (Printer.run "A1")
    //ignore (XLSXReader.testXLRead testFile)
    //parseAndPrintASTs testFile
    typeCheckAndPrint testFile
    //typeCheckingToCSV testFile
#else
    if argv.Length < 1
    then printfn "Please provide xlsx files to parse and type check. Flag -p enables AST print mode, -c CSV output mode"
    elif argv.[0].[0] = '-'
    then
        let files = List.tail (List.ofArray argv)
        //let files = List.filter(fun filename -> not (XLSXReader.isProtected filename)) (List.tail (List.ofArray argv))
        if argv.Length < 2
        then printfn "Please provide xlsx files"
        elif argv.[0] = "-p" then List.iter parseAndPrintASTs files
        elif argv.[0] = "-c" then List.iter typeCheckingToCSV files
        else printfn "%s is not a valid option, expecting '-c' for CSV output, or '-p' for printout of AST" argv.[0]
    else
        List.iter typeCheckAndPrint (List.ofArray argv)
#endif
    0 // return an integer exit code
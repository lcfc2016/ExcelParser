// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open System.IO
open System.Collections.Generic
open Types


let createAST input isFormula =
    (input, isFormula) ||> Tokeniser.run |> Parser.run

let parseAndPrintASTs filename =
     Map.iter (fun name contents ->
                printfn "%s" name
                ignore [ for (cell: UnparsedCell) in contents ->
                            try
                                Printer.run cell.address (createAST cell.value cell.isFormula)
                            with
                                | :? InvalidOperationException as ex -> printfn "%s -> Unable to parse: %s" cell.address ex.Message ])
              (XLSXReader.xlsxReader(filename))

let printTypeOutput (errors: Queue<Error>) =
    for error in errors do
        printfn "%s, %s, %s -> Expected %s, Got %s, %s" error.sheet error.cell error.f error.expected error.actual error.errorType

let parseAndTypeCheck filename =
    let astMap = Map.map (fun name contents ->
                             Map [ for (cell: UnparsedCell) in contents ->
                                    ( cell.address,
                                        try
                                            Success ({ column = cell.column; row = cell.row; ast = (createAST cell.value cell.isFormula) })
                                        with
                                            | :? InvalidOperationException as ex -> Failure (ex.Message))])
                        (XLSXReader.xlsxReader(filename))
    Interpreter.run astMap

let typeCheckAndPrint filename =
    parseAndTypeCheck filename |> printTypeOutput

let errorsToCSVFormat filename (errors: Queue<Error>) =
    Seq.map(fun error ->
        // String.Join(",", filename, error.sheet, error.cell, error.f, error.expected, error.actual, error.errorType, error.errorMessage)) errors
        sprintf "\"%s\",\"%s\",\"%s\",\"%s\",\"%s\",\"%s\",\"%s\",\"%s\"" filename error.sheet error.cell error.f error.expected error.actual error.errorType error.errorMessage) errors

let rec generateOutputName (filename: string) attempt =
    let output = Path.GetDirectoryName(filename) + @"\" + "output_" + Path.GetFileNameWithoutExtension(filename) + (if attempt < 1 then "" else "_" + attempt.ToString()) + ".csv"
    if File.Exists output
    then generateOutputName filename (attempt + 1)
    else output

let typeCheckingToCSV (filename: string) =
    let errors = parseAndTypeCheck filename
    if errors.Count > 0
    then
        use outFile = new StreamWriter(generateOutputName filename 0)
        outFile.WriteLine("file,sheet,cell,function,expected,actual,error_type,error_message")
        errors |> (errorsToCSVFormat filename) |> Seq.iter outFile.WriteLine

[<EntryPoint>]
let main argv =
#if DEBUG
    let testFile = "C:/Users/bjs73/documents/MSC/IRP/test_sheet_1.xlsx"
    //ignore (XLSXReader.testXLRead testFile)
    // parseAndPrintASTs testFile
    //typeCheckAndPrint testFile
    typeCheckingToCSV testFile
#else
    if argv.Length < 1
    then printfn "Please provide xlsx files to parse and type check. Flag -p enables AST print mode, -c CSV output mode"
    elif argv.[0].[0] = '-'
    then if argv.Length < 2
         then printfn "Please provide xlsx files"
         elif argv.[0] = "-p" then List.iter parseAndPrintASTs (List.ofArray argv)
         elif argv.[0] = "-c" then List.iter typeCheckingToCSV (List.ofArray argv)
         else printfn "%s is not a valid option, expecting '-c' for CSV output, or '-p' for printout of AST" argv.[0]
    else List.iter typeCheckAndPrint (List.ofArray argv)
#endif
    0 // return an integer exit code
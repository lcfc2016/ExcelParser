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
                           Printer.run cell.address (createAST cell.value cell.isFormula)])
              (XLSXReader.xlsxReader(filename))

let printTypeOutput (errors: Queue<Error>) =
    for (sheet, cell, f, expected, actual, error) in errors do
        printfn "%s, %s, %s -> Expected %s, Got %s, %s error" sheet cell f expected actual error

let outputToCSV filename (errors: Queue<Error>) =
    Seq.map(fun (sheet, cell, f, expected, actual, error) ->
        String.Join(",", filename, sheet, cell, f, expected, actual, error)) errors

let parseAndTypeCheck filename =
    let astMap = Map.map (fun name contents ->
                             Map [ for (cell: UnparsedCell) in contents ->
                                    (cell.address, {
                                        column = cell.column;
                                        row = cell.row;
                                        ast = (createAST cell.value cell.isFormula)
                                    })])
                        (XLSXReader.xlsxReader(filename))
    Interpreter.run astMap

[<EntryPoint>]
let main argv =
#if DEBUG
    // let code = "=abs(neg(log(1000, 10) + 3 ^ 2)) = 7"
    let testFile = "C:/Users/bjs73/documents/MSC/IRP/test_sheet_1.xlsx"
    let outputFile = "C:/Users/bjs73/documents/MSC/IRP/output_test.csv"
    //ignore (XLSXReader.testXLRead testFile)
    // parseAndPrintASTs testFile
    // parseAndTypeCheck testFile |> printTypeOutput
    use outFile = new StreamWriter(outputFile)
    outFile.WriteLine("file,sheet,cell,function,expected,actual,actual,error_type")
    for file in [testFile] do
        parseAndTypeCheck testFile |> (outputToCSV file) |> Seq.iter outFile.WriteLine
#else
    if argv.Length < 1
    then printfn "Please provide xlsx files to parse and type check. Flag -p enables AST print mode"
    else if argv.[0] = "-p"
         then if argv.Length < 2
              then printfn "Please provide xlsx files to parse and type check"
              else List.iter parseAndPrintASTs (List.ofArray argv)
         else List.iter parseAndTypeCheck (List.ofArray argv)
#endif
    0 // return an integer exit code
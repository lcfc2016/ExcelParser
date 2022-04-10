// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Types

let textFile = "C:/Users/bjs73/documents/MSC/IRP/test_sheet_1.xlsx"

let createAST input isFormula =
    (input, isFormula) ||> Tokeniser.run |> Parser.run

let parseAndPrintASTs () =
     Map.iter (fun name contents ->
                printfn "%s" name
                ignore [ for (cell: UnparsedCell) in contents ->
                           Printer.run cell.address (createAST cell.value cell.isFormula)])
              (XLSXReader.xlsxReader(textFile))

let parseAndTypeCheck () =
    let astMap = Map.map (fun name contents ->
                             Map [ for (cell: UnparsedCell) in contents ->
                                    (cell.address, {
                                        column = cell.column;
                                        row = cell.row;
                                        ast = (createAST cell.value cell.isFormula)
                                    })])
                        (XLSXReader.xlsxReader(textFile))
    let errors = Interpreter.run astMap
    for error in errors do
        printfn "%s = %s" (fst error) (snd error)

[<EntryPoint>]
let main argv =
    // let code = "=abs(neg(log(1000, 10) + 3 ^ 2)) = 7"
    // XLSXReader.testXLRead textFile
    // parseAndPrintASTs ()
    parseAndTypeCheck ()
    0 // return an integer exit code
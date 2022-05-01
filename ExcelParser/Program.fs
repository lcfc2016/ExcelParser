// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Types


let createAST input isFormula =
    (input, isFormula) ||> Tokeniser.run |> Parser.run

let parseAndPrintASTs filename =
     Map.iter (fun name contents ->
                printfn "%s" name
                ignore [ for (cell: UnparsedCell) in contents ->
                           Printer.run cell.address (createAST cell.value cell.isFormula)])
              (XLSXReader.xlsxReader(filename))

let parseAndTypeCheck filename =
    let astMap = Map.map (fun name contents ->
                             Map [ for (cell: UnparsedCell) in contents ->
                                    (cell.address, {
                                        column = cell.column;
                                        row = cell.row;
                                        ast = (createAST cell.value cell.isFormula)
                                    })])
                        (XLSXReader.xlsxReader(filename))
    let errors = Interpreter.run astMap
    for error in errors do
        printfn "%s = %s" (fst error) (snd error)

[<EntryPoint>]
let main argv =
#if DEBUG
    // let code = "=abs(neg(log(1000, 10) + 3 ^ 2)) = 7"
    let testFile = "C:/Users/bjs73/documents/MSC/IRP/test_sheet_1.xlsx"
    //ignore (XLSXReader.testXLRead testFile)
    parseAndPrintASTs testFile
    // parseAndTypeCheck testFile
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
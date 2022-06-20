module XLSXReader

open System
open System.IO
open ClosedXML.Excel
open Types

let getNamedRanges (filename: String) =
    // Attempts to retrieve the named ranges from the file, ClosedXML and Excel occasionally error,
    // report if so. Otherwise map over ranges, discarding multi-sheet ranges as unresolvable.
    // Then return the parsed named ranges
    try
        use workbook = new XLWorkbook(filename)
        let validRanges =
            List.filter (fun (range: IXLNamedRange) ->
                            let sheetNames = Set.ofList [ for subRange in range.Ranges -> subRange.Worksheet.Name ]
                            sheetNames.Count = 1 )
                            [ for range in workbook.NamedRanges.ValidNamedRanges() -> range ]
        Map [ for range in validRanges ->
                let sheet = [ for subRange in range.Ranges -> subRange.Worksheet.Name ].Head
                ( range.Name.ToLower(), { sheet = sheet + "!"; ranges = [ for subRange in range.Ranges -> subRange.RangeAddress.ToString() ] } ) ]
    with
        | :? FileNotFoundException as ex -> invalidOp(filename + " not found")
        | :? IndexOutOfRangeException as ex -> invalidOp("ClosedXML named range read error")
        | ex -> invalidOp("Error occurred reading file")

let readSheet (xlSheet: IXLWorksheet) =
    [ for cell in xlSheet.CellsUsed() -> {
            address = cell.Address.ToString(XLReferenceStyle.A1);
            column = cell.WorksheetColumn().ColumnLetter();
            row = cell.WorksheetRow().RowNumber();
            // Below has to strip curly braces due to a quirk in ClosedXML formula parsing
            value = if cell.HasFormula then cell.FormulaA1.TrimStart('{').TrimEnd('}') else cell.GetString();
            isFormula = cell.HasFormula
    }]

let xlsxReader (filename: string) =
    try
        use workbook = new XLWorkbook(filename)
        Map [ for sheet in workbook.Worksheets -> sheet.Name, readSheet sheet ]
    with
        | ex -> invalidOp("Failed to read " + filename)

let testXLRead testWB =
    let parsedWB = xlsxReader(testWB)
    Map.map (fun name sheet ->
        printfn "%s" name
        Seq.iter (fun cell -> printfn "%O" cell) sheet) parsedWB

module XLSXReader

open System
open ClosedXML.Excel
open System.Collections.Generic
open System.Text.RegularExpressions
open Types

let getNamedRanges (filename: String) =
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
        | :? IndexOutOfRangeException as ex -> invalidOp("ClosedXML named range read error")
        | ex -> invalidOp("Error occurred reading named ranges")

let readSheet (xlSheet: IXLWorksheet) =
    [ for cell in xlSheet.CellsUsed() -> {
            address = cell.Address.ToString(XLReferenceStyle.A1);
            column = cell.WorksheetColumn().ColumnLetter();
            row = cell.WorksheetRow().RowNumber();
            // Below has to strip curly braces due to weirdness in ClosedXML formula parsing
            value = if cell.HasFormula then cell.FormulaA1.TrimStart('{').TrimEnd('}') else cell.GetString();
            isFormula = cell.HasFormula
    }]

let normaliseSheetName (sheetName: String) =
    sheetName
    // if Regex.IsMatch(sheetName, @"\s")
    // then "'" + sheetName + "'"
    // else sheetName

let xlsxReader (filename: string) =
    try
        use workbook = new XLWorkbook(filename)
        Map [ for sheet in workbook.Worksheets -> (normaliseSheetName sheet.Name, readSheet sheet) ]
    with
        | ex -> invalidOp("Failed to read " + filename)

let isProtected (filename: string) =
    use workbook = new XLWorkbook(filename)
    workbook.IsPasswordProtected// || workbook.IsProtected

let testXLRead testWB =
    let parsedWB = xlsxReader(testWB)
    Map.map (fun name sheet ->
        printfn "%s" name
        Seq.iter (fun cell -> printfn "%O" cell) sheet) parsedWB

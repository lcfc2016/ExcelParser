module XLSXReader

open System
open ClosedXML.Excel
open System.Collections.Generic
open System.Text.RegularExpressions
open Types

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
    use workbook = new XLWorkbook(filename)
    Map [ for sheet in workbook.Worksheets -> (normaliseSheetName sheet.Name, readSheet sheet) ]

let isProtected (filename: string) =
    use workbook = new XLWorkbook(filename)
    workbook.IsPasswordProtected// || workbook.IsProtected

let testXLRead testWB =
    let parsedWB = xlsxReader(testWB)
    Map.map (fun name sheet ->
        printfn "%s" name
        Seq.iter (fun cell -> printfn "%O" cell) sheet) parsedWB

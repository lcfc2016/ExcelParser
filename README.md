# ExcelParser

This project is for a type checking parser for the Excel formula language. It takes xlsx files as input and outputs type mismatches, partial type matches, and parse errors, either to STDOUT or as CSVs. It also can produce the AST for the formula language. It's complete across a large number of features, but misses out a few functions and syntactic constructs. Better documentation to follow in future.

It was developed as a VS2019 project and is probably best downloaded as such. Relies on the ClosedXML library for reading XLSX files.

USAGE

The prototype is built to be ran from the command line, it can be invoked as per any command line executable once downloaded and compiled

The parser expects the name(s) of Excel files to be provided as command line arguments, and will produce output for each supplied. If no files are provided or the flag '-h' is supplied as the first argument, the parser will output simple usage notes.

Three modes are available and can be invoked as follows:

- Print type checks in readable format to STDOUT: default mode, provide only the file names to parse
- Print parsed ASTs without type checking to STDOUT: provide '-p' as a flag prior to any file names
- Output type checks to CSVs, print progress to STDOUT: provide '-c' as a flag prior to any file names. In the analysis the progress output was redirected to txt files as a log, i.e. in the '-c' example below, ' > log_file.txt' was appended. Note that as designed the parser does not overwrite existing CSVs, instead it first tries to produce a csv of the format 'output_filename.csv', if said file already exists it appends '_1' to the file name, i.e. 'output_filename_1.csv', if that exists it increases the numeric suffix by 1 and tries again, repeating this process until it finds an unused file name.

Example usage:

> ExcelParser.exe test_sheet_1.xlsx
<PRINTS ANY TYPE AND PARSE ISSUES FOUND>

> ExcelParser.exe -p test_sheet_1.xlsx
<PRINTS THE PARSED ASTs FOR EACH NON-EMPTY CELL>

> ExcelParser.exe -c test_sheet_1.xlsx
<OUTPUTS THE TYPE CHECKING TO A CSV AND PRINTS THE STATUS TO STDOUT>

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using Villamos.Adatszerkezet;


namespace Villamos
{
    public static partial class MyClosedXML_Excel
    {



        public static void AlkalmazFerdeVonalak(string path, List<Beállítás_Ferde> beállítások)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(path, true))
            {
                WorkbookPart workbookPart = document.WorkbookPart;
                WorkbookStylesPart stylesPart = workbookPart.WorkbookStylesPart;

                if (stylesPart == null)
                {
                    stylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
                    stylesPart.Stylesheet = new Stylesheet(
                        new Fonts(new Font()),
                        new Fills(new Fill()),
                        new Borders(new Border()),
                        new CellFormats(new CellFormat())
                    );
                }

                Stylesheet stylesheet = stylesPart.Stylesheet;

                if (stylesheet.Borders == null)
                    stylesheet.Borders = new Borders(new Border());

                if (stylesheet.CellFormats == null)
                    stylesheet.CellFormats = new CellFormats(new CellFormat());

                foreach (var beállítás in beállítások)
                {
                    var sheet = workbookPart.Workbook.Descendants<Sheet>()
                        .FirstOrDefault(s => s.Name == beállítás.Munkalap);

                    if (sheet == null)
                        continue;

                    WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                    SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                    var cellRefs = GetCellReferences(beállítás.Terület);

                    foreach (var cellRef in cellRefs)
                    {
                        ApplyDiagonalToCell(sheetData, cellRef, stylesheet, beállítás.Jobb);
                    }

                    worksheetPart.Worksheet.Save();
                }

                stylesPart.Stylesheet.Save();
            }
        }

        private static void ApplyDiagonalToCell(
            SheetData sheetData,
            string cellRef,
            Stylesheet stylesheet,
            bool jobb)
        {
            GetCellCoordinates(cellRef, out int col, out int rowIndex);

            Row row = sheetData.Elements<Row>().FirstOrDefault(r => r.RowIndex == rowIndex);
            if (row == null)
            {
                row = new Row() { RowIndex = (uint)rowIndex };
                sheetData.Append(row);
            }

            Cell cell = row.Elements<Cell>().FirstOrDefault(c => c.CellReference == cellRef);
            if (cell == null)
            {
                cell = new Cell() { CellReference = cellRef };
                row.Append(cell);
            }

            uint originalStyleIndex = cell.StyleIndex ?? 0;
            CellFormat originalFormat = (CellFormat)stylesheet.CellFormats.ElementAt((int)originalStyleIndex);

            Border newBorder = CloneBorder(originalFormat, stylesheet);

            newBorder.DiagonalUp = jobb;
            newBorder.DiagonalDown = !jobb;

            // ✅ FONTOS: diagonális vonal stílusa
            if (newBorder.DiagonalBorder == null)
                newBorder.DiagonalBorder = new DiagonalBorder();

            newBorder.DiagonalBorder.Style = BorderStyleValues.Thin;

            uint newBorderId = (uint)stylesheet.Borders.Count;
            stylesheet.Borders.Append(newBorder);

            uint newFormatId = (uint)stylesheet.CellFormats.Count;

            CellFormat newFormat = new CellFormat(originalFormat.OuterXml)
            {
                BorderId = newBorderId,
                ApplyBorder = true
            };

            stylesheet.CellFormats.Append(newFormat);

            cell.StyleIndex = newFormatId;
        }

        private static Border CloneBorder(CellFormat format, Stylesheet stylesheet)
        {
            uint borderId = format.BorderId ?? 0;
            Border original = (Border)stylesheet.Borders.ElementAt((int)borderId);

            // Klónozás
            var left = (LeftBorder)(original.LeftBorder?.CloneNode(true) ?? new LeftBorder());
            var right = (RightBorder)(original.RightBorder?.CloneNode(true) ?? new RightBorder());
            var top = (TopBorder)(original.TopBorder?.CloneNode(true) ?? new TopBorder());
            var bottom = (BottomBorder)(original.BottomBorder?.CloneNode(true) ?? new BottomBorder());
            var diagonal = (DiagonalBorder)(original.DiagonalBorder?.CloneNode(true) ?? new DiagonalBorder());

            //// ✅ Itt tudod felülírni az értékeket
            //left.Style = BorderStyleValues.Thin;
            //left.Color = new Color() { Rgb = "000000" };
            //// piros
            //// Példa: top border vékony fekete
            //top.Style = BorderStyleValues.Thin;
            //top.Color = new Color() { Rgb = "000000" };

            return new Border(left, right, top, bottom, diagonal);
        }

        private static List<string> GetCellReferences(string range)
        {
            var result = new List<string>();

            var parts = range.Split(':');
            if (parts.Length == 1)
            {
                result.Add(parts[0].ToUpper());
                return result;
            }

            var start = parts[0].ToUpper();
            var end = parts[1].ToUpper();

            GetCellCoordinates(start, out int startCol, out int startRow);
            GetCellCoordinates(end, out int endCol, out int endRow);

            for (int col = startCol; col <= endCol; col++)
            {
                for (int row = startRow; row <= endRow; row++)
                {
                    result.Add(ColumnNumberToName(col) + row);
                }
            }

            return result;
        }

        private static void GetCellCoordinates(string cellRef, out int col, out int row)
        {
            string colPart = new string(cellRef.TakeWhile(char.IsLetter).ToArray());
            string rowPart = new string(cellRef.SkipWhile(char.IsLetter).ToArray());

            col = ColumnNameToNumber(colPart);
            row = int.Parse(rowPart);
        }

        private static int ColumnNameToNumber(string columnName)
        {
            int sum = 0;
            foreach (char c in columnName)
            {
                sum *= 26;
                sum += (c - 'A' + 1);
            }
            return sum;
        }

        private static string ColumnNumberToName(int columnNumber)
        {
            string columnName = "";
            while (columnNumber > 0)
            {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;
            }
            return columnName;
        }

    }




}





using DocumentFormat.OpenXml;
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
        public static void FerdeVonalAlkalmaz(string fájlnév, List<Beállítás_Ferde> Beállítások)
        {
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fájlnév, true))
            {
                WorkbookPart workbookPart = doc.WorkbookPart;
                List<Sheet> sheets = workbookPart.Workbook.Sheets?.Elements<Sheet>().ToList();

                if (sheets == null) return;

                for (int i = 0; i < sheets.Count; i++)
                {
                    Sheet sheet = sheets[i];
                    string lapNév = sheet?.Name?.Value;
                    List<Beállítás_Ferde> beállítások = Beállítások.Where(y => y.Munkalap.Trim() == lapNév.Trim()).ToList();
                    foreach (Beállítás_Ferde beállítás in beállítások)
                    {
                        var wsPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                        var stylesPart = workbookPart.WorkbookStylesPart;
                        if (stylesPart?.Stylesheet == null) throw new InvalidOperationException("Nincs stílusdefiníció.");

                        Stylesheet stylesheet = stylesPart.Stylesheet;

                        Borders borders = stylesheet.Borders;
                        if (borders == null || (borders.Count?.Value ?? 0) == 0) throw new InvalidOperationException("Nincs border definíció.");


                        // === 1. Meghatározzuk a célcellákat (egyesített cellák figyelembevételével) ===
                        var mergeCells = wsPart.Worksheet.Elements<MergeCells>().FirstOrDefault();
                        var mergedMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                        if (mergeCells != null)
                        {
                            foreach (var mc in mergeCells.Elements<MergeCell>())
                            {
                                if (string.IsNullOrEmpty(mc.Reference?.Value)) continue;
                                var cells = ExpandRange(mc.Reference.Value).ToList();
                                if (cells.Count > 0)
                                {
                                    string tl = cells[0];
                                    foreach (string c in cells) mergedMap[c] = tl;
                                }
                            }
                        }

                        var targetCells = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        foreach (string cellRef in ExpandRange(beállítás.Terület))
                        {
                            targetCells.Add(mergedMap.TryGetValue(cellRef, out var tl) ? tl : cellRef);
                        }

                        // === 2. Minden célcella stílusának border-jét módosítjuk ===
                        foreach (string cellRef in targetCells)
                        {
                            var cell = GetCell(wsPart, cellRef);
                            if (cell?.StyleIndex == null) continue;

                            uint styleIndex = cell.StyleIndex.Value;
                            if (styleIndex >= (stylesheet.CellFormats?.Count?.Value ?? 0)) continue;

                            var cellFormat = stylesheet.CellFormats.ElementAt((int)styleIndex) as CellFormat;
                            if (cellFormat?.BorderId == null) continue;

                            uint borderId = cellFormat.BorderId.Value;
                            if (borderId >= (borders.Count?.Value ?? 0)) continue;

                            var border = borders.ElementAt((int)borderId) as Border;
                            if (border == null) continue;

                            // === 3. Itt történik a varázslat: bekapcsoljuk az átlós szegélyt ===
                            if (beállítás.Jobb)
                                border.DiagonalDown = true;
                            else
                                border.DiagonalUp = true;

                            border.DiagonalBorder.Style.Value = BorderStyleValues.Thin;
                            border.DiagonalBorder.Color = new Color { Rgb = HexBinaryValue.FromString("000000") };
                        }

                        // Mentés
                        stylesheet.Save();
                        wsPart.Worksheet.Save();
                    }

                }

            }
        }

        // Cell keresése (már korábban is volt)
        private static Cell GetCell(WorksheetPart worksheetPart, string cellReference)
        {
            var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();
            string rowPart = string.Concat(cellReference.SkipWhile(char.IsLetter));
            if (!uint.TryParse(rowPart, out uint rowId)) return null;

            var row = sheetData.Elements<Row>().FirstOrDefault(r => r.RowIndex == rowId);
            return row?.Elements<Cell>().FirstOrDefault(c => c.CellReference == cellReference);
        }

        // Tartomány kibontása (már korábban is volt)
        public static IEnumerable<string> ExpandRange(string range)
        {
            if (string.IsNullOrEmpty(range)) yield break;
            if (!range.Contains(':'))
            {
                yield return range;
                yield break;
            }
            var parts = range.Split(':');
            (int c1, uint r1) = ParseRef(parts[0]);
            (int c2, uint r2) = ParseRef(parts[1]);
            int minC = Math.Min(c1, c2), maxC = Math.Max(c1, c2);
            uint minR = Math.Min(r1, r2), maxR = Math.Max(r1, r2);
            for (uint r = minR; r <= maxR; r++)
                for (int c = minC; c <= maxC; c++)
                    yield return GetColName(c) + r;
        }

        private static (int, uint) ParseRef(string r)
        {
            var l = string.Concat(r.TakeWhile(char.IsLetter)).ToUpper();
            var d = string.Concat(r.Skip(l.Length));
            uint row = uint.Parse(d);
            int col = 0;
            foreach (char ch in l) col = col * 26 + (ch - 'A' + 1);
            return (col, row);
        }

        private static string GetColName(int c)
        {
            string s = "";
            while (c > 0) { c--; s = (char)('A' + (c % 26)) + s; c /= 26; }
            return s;
        }
    }
}

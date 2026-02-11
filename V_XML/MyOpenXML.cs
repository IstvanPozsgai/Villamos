using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Villamos
{
    public static class MyOpenXML
    {

        public static void GenerateFromTemplate(string templatePath, string outputPath, Dictionary<string, string> cellValues)
        {
            // 1. Sablon lemásolása (képek, drawing, rels megmarad!)
            File.Copy(templatePath, outputPath, overwrite: true);

            // 2. OpenXML megnyitása
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(outputPath, true))
            {
                WorkbookPart wbPart = doc.WorkbookPart;

                // 3. Megkeressük az "Adatok" nevű munkalapot
                Sheet adatokSheet = wbPart.Workbook.Descendants<Sheet>()
                    .FirstOrDefault(s => s.Name == "Adatok");

                if (adatokSheet == null)
                    throw new Exception("Az 'Adatok' munkalap nem található a sablonban!");

                WorksheetPart wsPart = (WorksheetPart)(wbPart.GetPartById(adatokSheet.Id));

                SheetData sheetData = wsPart.Worksheet.GetFirstChild<SheetData>();

                // 4. Csak az Adatok lap celláit írjuk
                foreach (var kvp in cellValues)
                {
                    string cellRef = kvp.Key;   // pl. "B4"
                    string value = kvp.Value;   // pl. "AB0027"

                    WriteCell(sheetData, cellRef, value);
                }

                var calcProps = wbPart.Workbook.Elements<CalculationProperties>().FirstOrDefault();
                if (calcProps == null)
                {
                    calcProps = new CalculationProperties();
                    wbPart.Workbook.Append(calcProps);
                }

                // Automatikus számítás beállítása (string attribútumként)
                calcProps.SetAttribute(new OpenXmlAttribute("calculationMode", null, "auto"));
                calcProps.SetAttribute(new OpenXmlAttribute("fullCalcOnLoad", null, "1"));

                wbPart.Workbook.Save();


            }
        }

        private static void WriteCell(SheetData sheetData, string cellRef, string value)
        {
            // Sor index kinyerése
            uint rowIndex = uint.Parse(new string(cellRef.Where(char.IsDigit).ToArray()));

            // Sor megkeresése vagy létrehozása
            Row row = sheetData.Elements<Row>().FirstOrDefault(r => r.RowIndex == rowIndex);
            if (row == null)
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // Cella megkeresése vagy létrehozása
            Cell cell = row.Elements<Cell>().FirstOrDefault(c => c.CellReference == cellRef);
            if (cell == null)
            {
                cell = new Cell() { CellReference = cellRef };
                row.Append(cell);
            }

            // Régi tartalom törlése
            cell.RemoveAllChildren();

            // InlineString használata (legbiztonságosabb)
            cell.DataType = CellValues.InlineString;
            cell.InlineString = new InlineString(new Text(value));
        }

    }

}

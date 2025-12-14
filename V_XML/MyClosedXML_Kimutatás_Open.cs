using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Linq;

namespace Villamos
{
    public static partial class MyClosedXML_Excel
    {

        public static void Kimutatás_Pivot_Subtotal_AlTipus(string fájlnév, string munkalap)
        {

            // 1. Pivot tábla megnyitása
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fájlnév, true))
            {
                var wbPart = doc.WorkbookPart;

                // Kimutatás lap keresése
                var sheet = wbPart.Workbook.Descendants<Sheet>()
                    .FirstOrDefault(s => s.Name == munkalap);
                if (sheet == null) return;

                var wsPart = (WorksheetPart)wbPart.GetPartById(sheet.Id);
                var pivotTablePart = wsPart.PivotTableParts.FirstOrDefault();
                if (pivotTablePart == null) return;

                var pivotDef = pivotTablePart.PivotTableDefinition;
                var rowFields = pivotDef.GetFirstChild<RowFields>();
                if (rowFields == null || !rowFields.Any()) return;

                // Első mező = AlTípus
                var alTipusField = rowFields.Elements<Field>().First();

                // 1. Ismétlődő címke engedélyezése → showAll="1"
                alTipusField.SetAttribute(new DocumentFormat.OpenXml.OpenXmlAttribute(
                    "showAll",
                    null,
                    "1"
                ));

                // 2. Részösszeg bekapcsolása → subtotalTop="1"
                alTipusField.SetAttribute(new DocumentFormat.OpenXml.OpenXmlAttribute(
                    "subtotalTop",
                    null,
                    "1"
                ));
            }
        }
    }
}
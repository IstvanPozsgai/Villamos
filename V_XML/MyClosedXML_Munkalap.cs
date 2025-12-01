using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace Villamos
{
    public static partial class MyClosedXML_Excel
    {
        public static void Tábla_Rögzítés(string munkalapnév, int sor)
        {
            FagyasztandóSorok[munkalapnév] = sor;
        }

        public static void Szűrés(string munkalapNév, string oszloptól, string oszlopig, int sorig, int sortól = 1)
        {
            try
            {
                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapNév);

                // Tartomány létrehozása: pl. "A1:D100"
                string kezdőCella = $"{oszloptól}{sortól}";
                string utolsóCella = $"{oszlopig}{sorig}";
                IXLRange tartomány = munkalap.Range(kezdőCella, utolsóCella);

                // AutoFilter bekapcsolása a tartományra
                tartomány.SetAutoFilter();
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Szűrés(munkalap: {munkalapNév}, oszloptól: {oszloptól}, oszlopig: {oszlopig}, sorig: {sorig}, sortól: {sortól}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\nA hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void AlkalmazFagyasztást(string fájlnév, Dictionary<string, int> fagyasztások)
        {
            using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fájlnév, isEditable: true))
            {
                WorkbookPart workbookPart = doc.WorkbookPart;
                List<Sheet> sheets = workbookPart.Workbook.Sheets?.Elements<Sheet>().ToList();

                if (sheets == null) return;

                foreach (Sheet sheet in sheets)
                {
                    string lapNév = sheet.Name?.Value;
                    if (string.IsNullOrEmpty(lapNév) || !fagyasztások.TryGetValue(lapNév, out int sorSzám) || sorSzám <= 0)
                        continue;

                    WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                    Worksheet worksheet = worksheetPart.Worksheet;

                    SheetViews sheetViews = worksheet.GetFirstChild<SheetViews>();
                    if (sheetViews == null)
                    {
                        sheetViews = new SheetViews();
                        worksheet.InsertAt(sheetViews, 0);
                    }

                    SheetView sheetView = sheetViews.Elements<SheetView>().FirstOrDefault();
                    if (sheetView == null)
                    {
                        sheetView = new SheetView { WorkbookViewId = 0 };
                        sheetViews.Append(sheetView);
                    }
                    else
                    {
                        if (sheetView.WorkbookViewId == null) sheetView.WorkbookViewId = 0;
                    }

                    sheetView.Pane = new Pane
                    {
                        VerticalSplit = sorSzám,
                        TopLeftCell = new StringValue($"A{sorSzám + 1}"),
                        ActivePane = PaneValues.BottomLeft,
                        State = PaneStateValues.Frozen
                    };



                    sheetView.AppendChild(new Selection
                    {
                        ActiveCell = new StringValue($"A{sorSzám + 1}"),
                        SequenceOfReferences = new ListValue<StringValue>
                        {
                            Items = { new StringValue($"A{sorSzám + 1}") }
                        }
                        // Pane property is optional and not required here
                    });

                    worksheetPart.Worksheet.Save();
                }
            }
        }

        public static void Munkalap_átnevezés(string régi, string új)
        {
            try
            {
                //  munkalap lekérése  név alapján)
                IXLWorksheet worksheet = xlWorkBook.Worksheet(régi);
                // Átnevezés
                worksheet.Name = új;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Munkalap_átnevezés(régi {régi}, új {új}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Munkalap_Új(string munkalap)
        {
            try
            {
                // Új munkalap hozzáadása
                IXLWorksheet munkalapObj = xlWorkBook.Worksheets.Add(munkalap);
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Új_munkalap(munkalap {munkalap}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Munkalap_aktív(string munkalap)
        {
            try
            {
                xlWorkSheet = xlWorkBook.Worksheet(munkalap);
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Munkalap_aktív(munkalap {munkalap}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///  Kép_beillesztés
        /// </summary>
        /// <param name="munkalap"></param>
        /// <param name="mit"></param>
        /// <param name="hely"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Magas"></param>
        /// <param name="Széles"></param>
        public static void Kép_beillesztés(string munkalapnév, String mit, string hely, int X, int Y, double Wszázalék, double HSzázalék)
        {
            try
            {
                // Kép hozzáadása
                using (var imageStream = File.OpenRead(hely))
                {
                    IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapnév);
                    munkalap.AddPicture(hely)
                            .WithPlacement(ClosedXML.Excel.Drawings.XLPicturePlacement.FreeFloating)
                            .MoveTo(X, Y)          // X és Y: pixelben vagy pontban (ClosedXML alapértelmezetten EMU-t használ, de van pont / pixel konverzió)
                            .ScaleWidth(Wszázalék)
                            .ScaleHeight(HSzázalék); // ClosedXML nem teszi lehetővé közvetlen pixelméret megadását — relatív skálázás szükséges
                }
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Kép_beillesztés(munkalap {munkalapnév}, mit {mit}, hely {hely}, X {X}, Y {Y}, Wszázalék {Wszázalék}), HSzázalék {HSzázalék}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

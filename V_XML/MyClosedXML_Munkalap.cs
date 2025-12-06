using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
        /// <summary>
        /// Belső hiperhivatkozás (Link) beillesztése egy adott cellába, amely egy másik munkalapra mutat.
        /// </summary>
        /// <param name="munkalap">Annak a munkalapnak a neve, ahová a linket tesszük.</param>
        /// <param name="hova">A cella címe (pl. "A1"), ahová a link kerül.</param>
        /// <param name="hivatkozottlap">A cél munkalap neve, amire a link mutatni fog (kattintáskor ide ugrik).</param>
        public static void Link_beillesztés(String munkalap, string hova, string hivatkozottlap)
        {
            try
            {
                IXLWorksheet worksheet = xlWorkBook.Worksheet(munkalap);
                IXLCell cella = worksheet.Cell(hova);

                string keplet = $"=HYPERLINK(\"#'{hivatkozottlap}'!A1\", \"'{hivatkozottlap}'\")";

                cella.FormulaA1 = keplet;
                cella.Style.Font.FontColor = XLColor.FromTheme(XLThemeColor.Hyperlink);
                cella.Style.Font.Underline = XLFontUnderlineValues.Single;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Link_beillesztés hiba", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\nA hiba naplózásra került.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Kimutatás (Pivot Table) készítése ClosedXML segítségével.
        /// Ez a FŐ metódus, amely kezeli az összesítési módokat is.
        /// </summary>
        public static void Kimutatás_Fő(string munkalap_adat, string balfelső, string jobbalsó,
                                        string kimutatás_Munkalap, string Kimutatás_cella, string Kimutatás_név,
                                        List<string> összesítNév, List<string> Összesítés_módja,
                                        List<string> sorNév, List<string> oszlopNév, List<string> SzűrőNév)
        {
            try
            {
                IXLWorksheet Adatok_lap = xlWorkBook.Worksheet(munkalap_adat);

                IXLWorksheet Kimutatás_lap;
                try { Kimutatás_lap = xlWorkBook.Worksheet(kimutatás_Munkalap); }
                catch { Kimutatás_lap = xlWorkBook.Worksheets.Add(kimutatás_Munkalap); }

                IXLRange AdatRange = Adatok_lap.Range(balfelső, jobbalsó);

                IXLCell celCella = Kimutatás_lap.Cell(Kimutatás_cella);
                IXLPivotTable pivotTable = Kimutatás_lap.PivotTables.Add(Kimutatás_név, celCella, AdatRange);

                // Sorok
                if (sorNév != null && sorNév.Count > 0)
                    foreach (string nev in sorNév)
                        pivotTable.RowLabels.Add(nev);

                // Oszlopok
                if (oszlopNév != null && oszlopNév.Count > 0)
                    foreach (string nev in oszlopNév)
                        pivotTable.ColumnLabels.Add(nev);

                // Szűrők
                if (SzűrőNév != null && SzűrőNév.Count > 0)
                    foreach (string nev in SzűrőNév)
                        pivotTable.ReportFilters.Add(nev);

                // Értékek
                if (összesítNév != null && összesítNév.Count > 0)
                {
                    for (int i = 0; i < összesítNév.Count; i++)
                    {
                        string mezoNev = összesítNév[i];
                        string mod = (Összesítés_módja != null && i < Összesítés_módja.Count) ? Összesítés_módja[i] : "xlSum";

                        IXLPivotValue valueField = pivotTable.Values.Add(mezoNev);

                        switch (mod)
                        {
                            case "xlCount": // Darabszám
                                valueField.SummaryFormula = XLPivotSummary.Count;
                                valueField.CustomName = mezoNev + " Összeg";
                                break;

                            case "xlSum": // Összeg
                            default:
                                valueField.SummaryFormula = XLPivotSummary.Sum;
                                valueField.CustomName = mezoNev + " db";
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Kimutatás_Fő \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\nA hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        /// <summary>
         /// KOMPATIBILITÁSI TÚLTERHELÉS (Overload)
         /// Ez teszi lehetővé, hogy a régi kódod (ami 11 paramétert használ és nincs benne az összesítés módja)
         /// továbbra is működjön.
         /// </summary>
        public static void Kimutatás_Fő(
            string munkalap_extra, 
            string munkalap_adat,  
            string balfelső,
            string jobbalsó,
            string kimutatás_Munkalap,
            string Kimutatás_cella,
            string Kimutatás_név,
            List<string> összesítNév,
            List<string> sorNév,
            List<string> oszlopNév,
            List<string> SzűrőNév)
        {

            List<string> alapertelmezettModok = new List<string>();

            if (összesítNév != null)
                for (int i = 0; i < összesítNév.Count; i++)
                    alapertelmezettModok.Add("xlSum");

            Kimutatás_Fő(
                munkalap_adat,
                balfelső,
                jobbalsó,
                kimutatás_Munkalap,
                Kimutatás_cella,
                Kimutatás_név,
                összesítNév,
                alapertelmezettModok,
                sorNév,
                oszlopNév,
                SzűrőNév
            );
        }

        /// <summary>
        /// Automata szűrés kikapcsolása (ClosedXML)
        /// </summary>
        /// <param name="munkalap">A munkalap neve</param>
        public static void SzűrésKi(string munkalap)
        {
            try
            {
                IXLWorksheet worksheet = xlWorkBook.Worksheet(munkalap);

                worksheet.AutoFilter.Clear();
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"SzűrésKi(munkalap {munkalap}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static long Munkalap(System.Data.DataTable Tábla, int sor, string munkalap)
        {
            try
            {
                // Munkalap kiválasztása
                IXLWorksheet worksheet = xlWorkBook.Worksheet(munkalap);

                // A ClosedXML InsertData funkciója csak az adatokat teszi be, a fejlécet külön kell kezelni,
                // ha nem "Táblázatként" (InsertTable), hanem sima adatként (InsertData) illesztjük be.
                for (int j = 0; j < Tábla.Columns.Count; j++)
                    worksheet.Cell(sor, j + 1).Value = Tábla.Columns[j].ColumnName;

                // Ha van adat a táblában, akkor a fejléc alá (sor + 1) beillesztjük egyben.
                if (Tábla.Rows.Count > 0)
                    worksheet.Cell(sor + 1, 1).InsertData(Tábla);

                // Visszatérünk a sorok számával (ahogy az eredeti kód tette)
                long utolsó_sor = Tábla.Rows.Count;
                return utolsó_sor;
            }
            catch (Exception ex)
            {
                System.Diagnostics.StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Munkalap feltöltés (munkalap: {munkalap}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                System.Windows.Forms.MessageBox.Show(ex.Message + "\n\nA hiba naplózásra került.", "A program hibára futott", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return 0;
            }
        }

        /// <summary>
        /// Képletek másolása formázás nélkül (ClosedXML)
        /// </summary>
        /// <param name="munkalap">Munkalap neve</param>
        /// <param name="honnan">Forrás tartomány (pl. "A1:A10")</param>
        /// <param name="hova">Cél tartomány bal felső cellája (pl. "B1")</param>
        public static void Képlet_másol(string munkalap, string honnan, string hova)
        {
            try
            {
                IXLWorksheet worksheet = xlWorkBook.Worksheet(munkalap);

                IXLRange sourceRange = worksheet.Range(honnan);

                IXLCell targetStartCell = worksheet.Range(hova).FirstCell();

                int startRow = sourceRange.RangeAddress.FirstAddress.RowNumber;
                int startCol = sourceRange.RangeAddress.FirstAddress.ColumnNumber;

                foreach (IXLCell sourceCell in sourceRange.Cells())
                {
                    int rowOffset = sourceCell.Address.RowNumber - startRow;
                    int colOffset = sourceCell.Address.ColumnNumber - startCol;

                    IXLCell targetCell = worksheet.Cell(targetStartCell.Address.RowNumber + rowOffset,
                                                        targetStartCell.Address.ColumnNumber + colOffset);

                    if (sourceCell.HasFormula)
                        targetCell.FormulaR1C1 = sourceCell.FormulaR1C1;
                    else
                        targetCell.Value = sourceCell.Value;
                }
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Képlet_másol(munkalap {munkalap}, honnan {honnan}, hova {hova}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);

                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;


namespace Villamos
{
    public static partial class MyClosedXML_Excel
    {
        private static XLWorkbook xlWorkBook;
        private static IXLWorksheet xlWorkSheet;
        private static readonly Dictionary<string, int> FagyasztandóSorok = new Dictionary<string, int>();
        private static readonly Dictionary<string, Beállítás_Nyomtatás> NyomtatásiBeállítások = new Dictionary<string, Beállítás_Nyomtatás>();

        public static int sor;
        public static int oszlop;


        private const double MmToInch = 1.0 / 25.4;

        public static void ExcelMegnyitás(string hely)
        {
            try
            {
                xlWorkBook = new XLWorkbook(hely);
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"ExcelMegnyitás(hely: {hely}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(
                    $"Nem sikerült megnyitni az Excel-fájlt:\n{hely}\n\nHiba: {ex.Message}",
                    "Hiba az Excel megnyitásakor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                // Bezárás: most már csak a munkafüzetet kell felszabadítani
                ExcelBezárás();
            }
        }

        public static void ExcelMentés(string fájlnév)
        {
            try
            {
                if (string.IsNullOrEmpty(fájlnév))
                {
                    throw new InvalidOperationException("A munkafüzet fájlútvonala nem lett beállítva. Használjon ExcelMentésMásként() vagy állítson be fájlnevet.");
                }

                xlWorkBook.SaveAs(fájlnév);
                // Utólagos OpenXml módosítás – CSAK ha van fagyasztás
                if (FagyasztandóSorok.Count > 0)
                {
                    AlkalmazFagyasztást(fájlnév, FagyasztandóSorok);
                    FagyasztandóSorok.Clear(); // nem kötelező, de tiszta állapot
                }


                // Ha van nyomtatási beállítás, alkalmazzuk OpenXml-mel
                if (NyomtatásiBeállítások.Count > 0)
                {
                    AlkalmazNyomtatásiBeállításokat(fájlnév, NyomtatásiBeállítások);
                    NyomtatásiBeállítások.Clear(); // Opcionális
                }
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"ExcelMentés \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ExcelBezárás()
        {
            try
            {
                // Munkafüzet eldobása – a ClosedXML nem igényel explicit "Close"-t,
                // de ha volt korábbi mentés, azt már el kellett végezni.
                xlWorkBook?.Dispose(); // Dispose felszabadítja a belső erőforrásokat (pl. stream-eket)
                xlWorkBook = null;
                xlWorkSheet = null;

                // Nincs szükség GC.Collect() vagy Marshal.ReleaseComObject() hívásokra
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"ExcelBezárás \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        // JAVÍTANDÓ:

        /// <summary>
        /// Elindítjuk az Excel készítést egy üres munkafüzettel
        /// </summary>
        public static void ExcelLétrehozás(bool teszt = false)
        {
            try
            {
                // Üres munkafüzet létrehozása memóriában
                xlWorkBook = new XLWorkbook();
                xlWorkSheet = xlWorkBook.Worksheets.Add("Munka1"); // Alapértelmezett lapnév

                // Ha teszt módban vagyunk, elmentjük ideiglenes fájlba és megnyitjuk
                if (teszt)
                {
                    string tempFile = System.IO.Path.GetTempFileName().Replace(".tmp", ".xlsx");
                    xlWorkBook.SaveAs(tempFile);
                    Process.Start(new ProcessStartInfo(tempFile) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"ExcelLétrehozás \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        /// <summary>
        /// Kiírja a szöveget a megfelelő cellába
        /// </summary>
        /// <param name="mit">szöveg</param>
        /// <param name="hova">szöveg</param>
        public static void Kiir(string mit, string hova)
        {
            try
            {
                // Érték beírása a megadott cellába vagy tartományba
                xlWorkSheet.Range(hova).Value = mit;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Kiir(mit {mit}, hova {hova})\n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Egyesíti a kiválasztott területet
        /// </summary>
        /// <param name="mit">szöveg</param>
        public static void Egyesít(string munkalap, string mit)
        {
            try
            {
                // Munkalap lekérése név alapján
                IXLWorksheet munkalapObj = xlWorkBook.Worksheet(munkalap);

                // Tartomány egyesítése
                IXLRange tartomany = munkalapObj.Range(mit);
                tartomany.Merge();

                // Igazítás beállítása: vízszintesen és függőlegesen középre
                tartomany.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                tartomany.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Egyesít(munkalap {munkalap}, mit {mit}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }


        /// <summary>
        /// A munkalapot, úgy mozgatja, hogy a kívánt cella rajta legyen a képernyőn.
        /// </summary>
        /// <param name="munkalap"></param>
        /// <param name="mit"></param>
        public static void Aktív_Cella(string munkalap, string mit)
        {
            try
            {
                // Beállítja az aktív munkalapot ÉS az aktív cellát a fájlban
                SetActiveSheetAndCell(xlWorkBook, munkalap, mit);
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Aktív_Cella(munkalap {munkalap}, mit {mit}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public static void SetActiveSheetAndCell(XLWorkbook workbook, string worksheetName, string cellAddress)
        {
            // 1. Először állítsd be a munkalapot aktívnak a ClosedXML-ben
            workbook.Worksheet(worksheetName).Select();

            // 2. Mentés memóriába (nem fájlba)
            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                stream.Position = 0;

                // 3. OpenXML-mel módosítjuk a fájl tartalmát
                using (var spreadsheet = SpreadsheetDocument.Open(stream, true))
                {
                    var workbookPart = spreadsheet.WorkbookPart;
                    var sheets = workbookPart.Workbook.Sheets;

                    // Munkalap azonosítása
                    uint sheetId = 0;
                    string relId = null;
                    uint index = 0;

                    foreach (var sheet in sheets.Elements<Sheet>())
                    {
                        if (sheet.Name == worksheetName)
                        {
                            sheetId = sheet.SheetId.Value;
                            relId = sheet.Id;
                            break;
                        }
                        index++;
                    }

                    // Állítsd be az aktív fület (0-alapú index!)
                    workbookPart.Workbook.BookViews = new BookViews(
                        new WorkbookView
                        {
                            ActiveTab = (UInt32)index
                        });

                    // Munkalap tartalom módosítása
                    var worksheetPart = (WorksheetPart)workbookPart.GetPartById(relId);
                    var sheetViews = worksheetPart.Worksheet.GetFirstChild<SheetViews>()
                                         ?? worksheetPart.Worksheet.InsertAt(new SheetViews(), 0);

                    var sheetView = sheetViews.GetFirstChild<SheetView>()
                                    ?? sheetViews.AppendChild(new SheetView());

                    // Távolítsd el a korábbi kijelöléseket
                    var selections = sheetView.GetFirstChild<Selection>();
                    if (selections != null)
                        selections.Remove();

                    // Állítsd be az új aktív cellát és kijelölést
                    sheetView.TabSelected = true;
                    sheetView.AppendChild(new Selection
                    {
                        ActiveCell = cellAddress,
                        SequenceOfReferences = new ListValue<StringValue> { InnerText = cellAddress }
                    });

                    // Mentsd el a változtatásokat
                    worksheetPart.Worksheet.Save();
                    workbookPart.Workbook.Save();
                }

                // 4. Írd vissza a módosított tartalmat a munkafüzetbe (ha szükséges további módosítás)
                stream.Position = 0;
                // Ha nem akarsz tovább dolgozni a munkafüzettel, akkor elegendő elmenteni a stream-et fájlba:
                // File.WriteAllBytes("fájl.xlsx", stream.ToArray());
            }
        }

        /// <summary>
        /// szöveg függőleges helyzetét lehet megadni
        /// </summary>
        /// <param name="mit"></param>
        /// <param name="irány">felső/alsó/közép</param>
        public static void Igazít_függőleges(string mit, string irány)
        {
            try
            {
                var tartomány = xlWorkSheet.Range(mit);
                XLAlignmentVerticalValues igazítás;

                switch (irány)
                {
                    case "felső":
                        igazítás = XLAlignmentVerticalValues.Top;
                        break;
                    case "alsó":
                        igazítás = XLAlignmentVerticalValues.Bottom;
                        break;
                    default:
                        igazítás = XLAlignmentVerticalValues.Center;
                        break;
                }

                tartomány.Style.Alignment.Vertical = igazítás;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Igazít_függőleges(mit {mit}, irány {irány}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///  A szöveg helyzetét lehet meghatározni a cellában bal és jobb a kötött név minden egyéb középre kerül.
        /// </summary>
        /// <param name="mit"></param>
        /// <param name="irány">bal/jobb/közép</param>
        public static void Igazít_vízszintes(string mit, string irány)
        {
            try
            {
                var tartomány = xlWorkSheet.Range(mit);
                XLAlignmentHorizontalValues igazítás;
                switch (irány)
                {
                    case "bal":
                        igazítás = XLAlignmentHorizontalValues.Left;
                        break;
                    case "jobb":
                        igazítás = XLAlignmentHorizontalValues.Right;
                        break;
                    default:
                        igazítás = XLAlignmentHorizontalValues.Center;
                        break;
                }
;

                tartomány.Style.Alignment.Horizontal = igazítás;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Igazít_vízszintes(mit {mit}, irány {irány}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public static void Sortörésseltöbbsorba_egyesített(string mit)
        {
            try
            {
                var tartomány = xlWorkSheet.Range(mit);

                // Egyesítés
                tartomány.Merge();

                // Sortörés (többsoros szöveg)
                tartomány.Style.Alignment.WrapText = true;

                // Vízszintes igazítás: General (alapértelmezett – elegendő, ha nem állítunk mást)
                tartomány.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.General;

                // Függőleges igazítás: középre
                tartomány.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Sortörésseltöbbsorba(mit {mit}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }





    }
}

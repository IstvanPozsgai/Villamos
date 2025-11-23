using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private static readonly List<Beállítás_Ferde> FerdeVonalak = new List<Beállítás_Ferde>();

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

                // Utólaf OpenXml segítségével behúzzuk a ferde vonalat.
                if (FerdeVonalak.Count > 0)
                {
                    FerdeVonalAlkalmaz(fájlnév, FerdeVonalak);
                    FerdeVonalak.Clear();
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

        public static void ExcelLétrehozás(string munkalap = "Munka1")
        {
            try
            {
                // Üres munkafüzet létrehozása memóriában
                xlWorkBook = new XLWorkbook();
                xlWorkSheet = xlWorkBook.Worksheets.Add(munkalap); // Alapértelmezett lapnév
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"ExcelLétrehozás \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


    }
}

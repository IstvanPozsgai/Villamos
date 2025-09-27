using Microsoft.Office.Interop.Excel;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MyExcel = Microsoft.Office.Interop.Excel;

namespace Villamos
{
    public partial class Excel_Új
    {
        public int sor;
        public int oszlop;
        public MyExcel.Application xlApp;
        public MyExcel.Workbook xlWorkBook;
        public MyExcel.Worksheet xlWorkSheet;
        public _Workbook _xlWorkBook;
        public _Worksheet _xlWorkSheet;

        public object misValue = System.Reflection.Missing.Value;


        /// <summary>
        /// Elindítjuk az Excel készítést egy üres munkafüzettel
        /// </summary>
        public void ExcelLétrehozás(bool teszt = false)
        {
            try
            {
                //elindítjuk az alkalmazást. létrehozzuk a fájlt és a munkalapot.
                xlApp = new MyExcel.Application
                {
                    Visible = teszt
                };
                xlApp.DisplayAlerts = false;
                // xlApp.Interactive = false;  Nem szabad bekapcsolni mert akkor nem működik kivétel HRESULT-értéke: 0x800AC472 dob.
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (MyExcel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
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
        /// Bezárja az excel táblát memória ürítéssel
        /// </summary>
        /// <param name="obj"></param>
        private static void ReleaseObject(object obj)
        {
            try
            {   // becsukjuk az excelt.
                if (obj != null)
                {
                    try
                    {
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                        obj = null;
                    }
                    catch (Exception)
                    {
                        obj = null;
                    }
                    finally
                    {
                        GC.Collect();
                    }
                }
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"ReleaseObject \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Excel táblát elmentjük a megadott néven
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="fájlnév"></param>
        public void ExcelMentés(string fájlnév)
        {
            try
            {
                xlApp.DisplayAlerts = false;
                xlWorkBook.SaveAs(fájlnév, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"ExcelMentés(fájlnév {fájlnév}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\nA hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Excel mentés ugyan azon a néven.
        /// </summary>
        public void ExcelMentés()
        {
            try
            {
                xlApp.DisplayAlerts = false;
                xlWorkBook.Save();
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"ExcelMentés \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void ExcelBezárás()
        {
            try
            {
                if (xlWorkBook != null)
                {
                    xlWorkBook.Close(SaveChanges: false); // vagy true, ha kell
                    Marshal.ReleaseComObject(xlWorkBook);
                    xlWorkBook = null;
                }

                if (xlApp != null)
                {
                    xlApp.Quit();
                    Marshal.ReleaseComObject(xlApp);
                    xlApp = null;
                }

                // Ha van munkalap referenciád, azt is szabadítsd fel
                if (xlWorkSheet != null)
                {
                    Marshal.ReleaseComObject(xlWorkSheet);
                    xlWorkSheet = null;
                }

                // Kritikus: GC hívások
                GC.Collect();
                GC.WaitForPendingFinalizers();

            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"ExcelBezárás \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Kiírja a szöveget a megfelelő cellába
        /// </summary>
        /// <param name="mit">szöveg</param>
        /// <param name="hova">szöveg</param>
        public void Kiir(string mit, string hova)
        {
            try
            {
                Range Cella = xlApp.Application.Range[hova];
                Cella.Value = mit;
                Marshal.ReleaseComObject(Cella);
                Cella = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Kiir(mit {mit}, hova {hova})\n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Munkalap_betű(string név, int méret)
        {
            try
            {
                Range Cellák = xlApp.Application.Cells;
                Cellák.VerticalAlignment = XlVAlign.xlVAlignCenter;
                Cellák.Font.Name = név;
                Cellák.Font.Size = méret;
                Cellák.Font.Strikethrough = false;
                Cellák.Font.Superscript = false;
                Cellák.Font.Subscript = false;
                Cellák.Font.OutlineFont = false;
                Cellák.Font.Shadow = false;
                Cellák.Font.Underline = XlUnderlineStyle.xlUnderlineStyleNone;
                Cellák.Font.ThemeColor = XlThemeColor.xlThemeColorLight1;
                Cellák.Font.TintAndShade = 0;
                Cellák.Font.ThemeFont = XlThemeFont.xlThemeFontNone;

                Marshal.ReleaseComObject(Cellák);
                Cellák = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Munkalap_betű(név: {név}, méret: {méret}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Megadott oszlop szélesség beállítása az oszlopnál
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="oszlop">string oszlopnév</param>
        /// <param name="szélesség">double szélesség, ha nincs megadva akkor automatikus</param>
        public void Oszlopszélesség(string munkalap, string oszlop, double szélesség = -1)
        {
            try
            {
                //Oszlop szélesség beállítás
                MyExcel.Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                Range Táblaterület = Munkalap.Range[oszlop];
                if (szélesség > 0)
                    Táblaterület.Columns.ColumnWidth = szélesség;
                else
                    Táblaterület.Columns.EntireColumn.AutoFit();
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Oszlopszélesség(munkalap: {munkalap}, oszlop: {oszlop}, szélesség: {szélesség} \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// A munkalapot, úgy mozgatja, hogy a kívánt cella rajta legyen a képernyőn.
        /// </summary>
        /// <param name="munkalap"></param>
        /// <param name="mit"></param>
        public void Aktív_Cella(string munkalap, string mit)
        {
            try
            {
                MyExcel.Worksheet Munkalap = (MyExcel.Worksheet)xlWorkBook.Worksheets[munkalap];
                xlWorkBook.Activate();
                Munkalap.Activate(); // Activate() stabilabb, mint Select()
                Range range = Munkalap.get_Range(mit, mit);
                range.Select();
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Aktív_Cella(munkalap {munkalap}, mit {mit}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Egyesíti a kiválasztott területet
        /// </summary>
        /// <param name="mit">szöveg</param>
        public void Egyesít(string munkalap, string mit)
        {
            try
            {
                MyExcel.Worksheet Munkalap = (MyExcel.Worksheet)xlWorkBook.Worksheets[munkalap];
                Range Táblaterület = Munkalap.Range[mit];
                Táblaterület.Merge();
                Táblaterület.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                Táblaterület.VerticalAlignment = XlVAlign.xlVAlignCenter;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Egyesít(munkalap {munkalap}, mit {mit}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

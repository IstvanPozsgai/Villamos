using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static System.IO.File;
using DT = System.Data;
using MyExcel = Microsoft.Office.Interop.Excel;


namespace Villamos
{
    public static partial class Module_Excel
    {
        public static int sor;
        public static int oszlop;
        public static MyExcel.Application xlApp;
        public static MyExcel.Workbook xlWorkBook;
        public static MyExcel.Worksheet xlWorkSheet;
        public static MyExcel._Workbook _xlWorkBook;
        public static MyExcel._Worksheet _xlWorkSheet;

        public static object misValue = System.Reflection.Missing.Value;


        /// <summary>
        /// Elindítjuk az Excel készítést egy üres munkafüzettel
        /// </summary>
        public static void ExcelLétrehozás(bool teszt = false)
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
                HibaNapló.Log(ex.Message, "ExcelLétrehozás", ex.StackTrace, ex.Source, ex.HResult);
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
                HibaNapló.Log(ex.Message, "ReleaseObject", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Excel táblát elmentjük a megadott néven
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="fájlnév"></param>
        public static void ExcelMentés(string fájlnév)
        {
            try
            {
                xlApp.DisplayAlerts = false;
                xlWorkBook.SaveAs(fájlnév, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"ExcelMentés(fájlnév {fájlnév})", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\nA hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Excel mentés ugyan azon a néven.
        /// </summary>
        public static void ExcelMentés()
        {
            try
            {
                xlApp.DisplayAlerts = false;
                xlWorkBook.Save();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "ExcelMentés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void ExcelBezárás()
        {
            try
            {
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
           //     Marshal.ReleaseComObject(xlWorkBook);
                ReleaseObject(xlWorkSheet);
                ReleaseObject(xlWorkBook);
                ReleaseObject(xlApp);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "ExcelBezárás", ex.StackTrace, ex.Source, ex.HResult);
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
                MyExcel.Range Cella = Module_Excel.xlApp.Application.Range[hova];
                Cella.Value = mit;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"Kiir(mit {mit}, hova {hova})", ex.StackTrace, ex.Source, ex.HResult);
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
                MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
                switch (irány)
                {
                    case "bal":
                        Táblaterület.HorizontalAlignment = Constants.xlLeft;
                        break;
                    case "jobb":
                        Táblaterület.HorizontalAlignment = Constants.xlRight;
                        break;
                    default:
                        Táblaterület.HorizontalAlignment = Constants.xlCenter;
                        break;
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"Igazít_vízszintes(mit {mit}, irány {irány})", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
                switch (irány)
                {
                    case "felső":
                        Táblaterület.VerticalAlignment = Constants.xlTop;
                        break;
                    case "alsó":
                        Táblaterület.VerticalAlignment = Constants.xlBottom;
                        break;
                    default:
                        Táblaterület.VerticalAlignment = Constants.xlCenter;
                        break;
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"Igazít_függőleges(mit {mit}, irány {irány})", ex.StackTrace, ex.Source, ex.HResult);
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
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                Range Táblaterület = Munkalap.Range[mit];
                Táblaterület.Merge();
                Táblaterület.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                Táblaterület.VerticalAlignment = XlVAlign.xlVAlignCenter;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"Egyesít(munkalap {munkalap}, mit {mit})", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Háttérszín beállítása
        /// </summary>
        /// <param name="mit">szöveg</param>
        /// <param name="szín">dupla</param>
        public static void Háttérszín(string mit, int szín)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
                if (szín < 0 || szín > 16777215)
                    Táblaterület.Interior.Color = Color.White;
                else
                    Táblaterület.Interior.Color = szín;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"Háttérszín(mit {mit}, szín {szín})", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void Háttérszín(string mit, Color színe)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
                Táblaterület.Interior.Color = System.Drawing.ColorTranslator.ToOle(színe);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"Háttérszín(mit {mit}, színe {színe})", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //itt tartok
        public static void CellaNincsHáttér(string mit)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Interior.Pattern = Constants.xlNone;
            Táblaterület.Interior.TintAndShade = 0;
            Táblaterület.Interior.PatternTintAndShade = 0;

        }
        /// <summary>
        /// Sötét háttérhez világos betűt állít be
        /// </summary>
        /// <param name="mit">szöveg</param>
        /// <param name="szín">dupla</param>
        public static void Háttérszíninverz(string mit, double szín)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Interior.Color = szín;

            Táblaterület.Font.ThemeColor = Microsoft.Office.Interop.Excel.XlThemeColor.xlThemeColorDark1;
            Táblaterület.Font.TintAndShade = 0;
        }

        public static void Háttérszíninverz(string mit, Color színe)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Interior.Color = System.Drawing.ColorTranslator.ToOle(színe);

            Táblaterület.Font.ThemeColor = Microsoft.Office.Interop.Excel.XlThemeColor.xlThemeColorDark1;
            Táblaterület.Font.TintAndShade = 0;
        }


        /// <summary>
        /// A munkalapot, úgy mozgatja, hogy a kívánt cella rajta legyen a képernyőn.
        /// </summary>
        /// <param name="munkalap"></param>
        /// <param name="mit"></param>
        public static void Aktív_Cella(string munkalap, string mit)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            Munkalap.Select();

            MyExcel.Range range = Munkalap.get_Range(mit, mit);
            range.Select();
        }




        /// <summary>
        /// A cellába beírt szöveg olvasási irányát lehet beállítani
        /// </summary>
        /// <param name="munkalap">munkalap neve</param>
        /// <param name="mit">cella helyzete</param>
        /// <param name="mennyit">-90 bal- 0 vízszintes- 90 jobb</param>
        public static void SzövegIrány(string munkalap, string mit, double mennyit)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range Táblaterület = Munkalap.Range[mit];
            Táblaterület.Orientation = mennyit;
        }






        /// <summary>
        /// A cella tartalmát sortöréssel több sorba írja
        /// </summary>
        /// <param name="mit"></param>
        /// 

        public static void Sortörésseltöbbsorba(string mit)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Cells.Select();

            Táblaterület.HorizontalAlignment = Constants.xlGeneral;
            Táblaterület.VerticalAlignment = Constants.xlCenter;
            Táblaterület.WrapText = true;
            Táblaterület.Orientation = 0;
            Táblaterület.AddIndent = false;
            Táblaterület.IndentLevel = 0;
            Táblaterület.ShrinkToFit = false;
            Táblaterület.MergeCells = false;
        }


        public static void Sortörésseltöbbsorba(string mit, bool egyesített)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Cells.Select();

            Táblaterület.HorizontalAlignment = Constants.xlGeneral;
            Táblaterület.VerticalAlignment = Constants.xlCenter;
            Táblaterület.WrapText = true;
            Táblaterület.Orientation = 0;
            Táblaterület.AddIndent = false;
            Táblaterület.IndentLevel = 0;
            Táblaterület.ShrinkToFit = false;
            Táblaterület.MergeCells = egyesített;
        }

        public static void Sortörésseltöbbsorba_egyesített(string mit)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Cells.Select();

            Táblaterület.HorizontalAlignment = Constants.xlGeneral;
            Táblaterület.VerticalAlignment = Constants.xlCenter;
            Táblaterület.WrapText = true;
            Táblaterület.Orientation = 0;
            Táblaterület.AddIndent = false;
            Táblaterület.IndentLevel = 0;
            Táblaterület.ShrinkToFit = false;
            Táblaterület.MergeCells = true;
        }

        /// <summary>
        /// Megnyitja az excel, html lapot
        /// </summary>
        /// <param name="Fájlhelye"></param>
        public static void Megnyitás(string Fájlhelye)
        {
            if (!Exists(Fájlhelye)) return;
            Process.Start(Fájlhelye);
        }


        public static void ExcelMegnyitás(string hely, bool látszik = false)
        {
            xlApp = new MyExcel.Application();
            xlApp.Visible = látszik;
            xlWorkBook = xlApp.Workbooks.Open(hely);
        }


        public static void Értékmásol(string munkalap, string honnan, string hova)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range területhonnan = Munkalap.Range[honnan];
            MyExcel.Range területhova = Munkalap.Range[hova];

            területhonnan.Select();
            területhonnan.Copy();
            területhova.Select();
            területhova.PasteSpecial(Paste: XlPasteType.xlPasteValues, Operation: XlPasteSpecialOperation.xlPasteSpecialOperationNone, SkipBlanks: false, Transpose: false);
        }


        public static void Képlet_másol(string munkalap, string honnan, string hova)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range területhonnan = Munkalap.Range[honnan];
            MyExcel.Range területhova = Munkalap.Range[hova];

            területhonnan.Select();
            területhonnan.Copy();
            területhova.Select();
            területhova.PasteSpecial(Paste: XlPasteType.xlPasteFormulas, Operation: XlPasteSpecialOperation.xlPasteSpecialOperationNone, SkipBlanks: false, Transpose: false);
        }

        public static string Beolvas(string honnan)
        {
            string válasz = "_";
            MyExcel.Range Cella = Module_Excel.xlApp.Application.Range[honnan];

            if (Cella.Value != null)
                válasz = Cella.Value.ToStrTrim();

            return válasz;
        }

        public static void Kicsinyít(string mit)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.ShrinkToFit = true;
        }

        public static DateTime BeolvasDátum(string honnan)
        {
            DateTime válasz = new DateTime(1900, 1, 1);
            MyExcel.Range Cella = Module_Excel.xlApp.Application.Range[honnan];
            if (Cella.Value == null)
            {
                válasz = new DateTime(1900, 1, 1);
            }
            else if (!int.TryParse(Cella.Value.ToString(), out int result))
            {
                válasz = Convert.ToDateTime(Cella.Value);
            }
            else
            {
                válasz = Convert.ToDateTime(Cella.Value);
            }
            return válasz;
        }

        public static DateTime Beolvasidő(string honnan)
        {
            DateTime válasz = new DateTime(1900, 1, 1, 0, 0, 0);
            MyExcel.Range Cella = Module_Excel.xlApp.Application.Range[honnan];

            if (Cella.Value == null)
            {
                válasz = new DateTime(1900, 1, 1, 0, 0, 0);
            }
            else if (decimal.TryParse(Cella.Value.ToString(), out decimal ideig))
            {
                int óra, perc, másodperc;
                decimal órad, percd, másodpercd;

                órad = ideig * 24;
                óra = ((int)órad);
                órad = órad - Convert.ToDecimal(óra);

                percd = órad * 60;
                perc = (int)percd;
                percd = percd - Convert.ToDecimal(perc);

                másodpercd = percd * 60;
                másodperc = (int)másodpercd;

                válasz = new DateTime(1900, 1, 1, óra, perc, másodperc);
            }
            else if (Cella.Value.ToString().Contains(":"))
            {
                string[] darab = Cella.Value.ToString().Split(':');
                int óra = int.Parse(darab[0]);
                int perc = int.Parse(darab[1]);
                int másodperc;
                if (darab.Length > 2)
                    másodperc = int.Parse(darab[2]);
                else
                    másodperc = 0;


                válasz = new DateTime(1900, 1, 1, óra, perc, másodperc);
            }
            return válasz;
        }

        public static void Kép_beillesztés(string munkalap, String mit, string hely)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];

            xlWorkSheet.Shapes.AddPicture(hely, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, 50, 30, 420, 175);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="munkalap"></param>
        /// <param name="mit"></param>
        /// <param name="hely"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Magas"></param>
        /// <param name="Széles"></param>
        public static void Kép_beillesztés(string munkalap, String mit, string hely, int X, int Y, int Magas, int Széles)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];

            xlWorkSheet.Shapes.AddPicture(hely, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, X, Y, Széles, Magas);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hova">cella hivatkozás</param>
        /// <param name="hivatkozottlap">munkalap neve amire mutat</param>
        public static void Link_beillesztés(String munkalap, string hova, string hivatkozottlap)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range Táblaterület = Munkalap.Range[hova];
            Táblaterület.Hyperlinks.Add(Anchor: Táblaterület, Address: "", SubAddress: "'" + hivatkozottlap + "'!A1", TextToDisplay: "'" + hivatkozottlap + "'");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="munkalap_adat">Az a munkalap melyen vannak az adatok</param>
        /// <param name="balfelső">A kimutatás alaptáblájának bal felső cellaneve</param>
        /// <param name="jobbalsó">>A kimutatás alaptáblájának jobb alsó cellaneve</param>
        /// <param name="kimutatás_Munkalap">Az a munkalap amelyikre tesszük a kimutatást</param>
        /// <param name="Kimutatás_cella">Kimutatás bal felső cellája</param>
        /// <param name="Kimutatás_név">Kimutatás neve</param>
        /// <param name="összesítNév">Azon adatok listája amit összesítünk</param>
        /// <param name="sorNév">Sorokban szereplő értékek listája</param>
        /// <param name="oszlopNév">Oszlopokban szereplő értékek listája</param>
        /// <param name="SzűrőNév">Szűrő Nevek listája</param>
        /// <param name="SzűrőÉrték">Szűrt értékek</param>
        public static void Kimutatás_Fő(string munkalap_adat, string balfelső, string jobbalsó, string kimutatás_Munkalap, string Kimutatás_cella, string Kimutatás_név
            , List<string> összesítNév, List<string> sorNév, List<string> oszlopNév, List<string> SzűrőNév)
        {

            MyExcel.Worksheet Adatok_lap = (Worksheet)xlWorkBook.Worksheets[munkalap_adat];
            MyExcel.Worksheet Kimutatás_lap = (Worksheet)xlWorkBook.Worksheets[kimutatás_Munkalap];

            MyExcel.Range AdatRange = Adatok_lap.Range[balfelső, jobbalsó];

            PivotCaches pivotCaches = xlWorkBook.PivotCaches();
            MyExcel.Range pivotData = Adatok_lap.Range[balfelső, jobbalsó];

            MyExcel.PivotCache pivotCache = pivotCaches.Create(XlPivotTableSourceType.xlDatabase, pivotData);
            MyExcel.PivotTable pivotTable = pivotCache.CreatePivotTable(Kimutatás_lap.Range[Kimutatás_cella], Kimutatás_név);

            //Táblázatban megjelenő érték
            if (összesítNév.Count > 0)
            {
                for (int i = 0; i < összesítNév.Count; i++)
                {

                    PivotField salesField = (PivotField)pivotTable.PivotFields(összesítNév[i]);
                    salesField.Orientation = XlPivotFieldOrientation.xlDataField;
                    salesField.Function = XlConsolidationFunction.xlSum;
                    salesField.Name = összesítNév[i] + " db";
                }
            }
            //Sor adatok
            if (sorNév.Count > 0)
            {
                for (int i = 0; i < sorNév.Count; i++)
                {
                    PivotField colorsRowsField = (PivotField)pivotTable.PivotFields(sorNév[i]);
                    colorsRowsField.Orientation = XlPivotFieldOrientation.xlRowField;
                }
            }

            //oszlopok 
            if (oszlopNév.Count > 0)
            {
                for (int i = 0; i < oszlopNév.Count; i++)
                {
                    PivotField regionField = (PivotField)pivotTable.PivotFields(oszlopNév[i]);
                    regionField.Orientation = XlPivotFieldOrientation.xlColumnField;
                }
            }

            //Szűrő mezők
            if (SzűrőNév.Count > 0)
            {
                for (int i = 0; i < SzűrőNév.Count; i++)
                {
                    PivotField datefield = (PivotField)pivotTable.PivotFields(SzűrőNév[i]);
                    datefield.Orientation = XlPivotFieldOrientation.xlPageField;
                    datefield.EnableMultiplePageItems = true;
                }
            }
            //Szűrés egy napra
            //    datefield.CurrentPage = SzűrőÉrték;
        }


        public static void Kimutatás_Fő(string munkalap_adat, string balfelső, string jobbalsó, string kimutatás_Munkalap, string Kimutatás_cella, string Kimutatás_név,
            List<string> összesítNév, List<string> Összesítés_módja, List<string> sorNév, List<string> oszlopNév, List<string> SzűrőNév)
        {
            MyExcel.Worksheet Adatok_lap = (Worksheet)xlWorkBook.Worksheets[munkalap_adat];
            MyExcel.Worksheet Kimutatás_lap = (Worksheet)xlWorkBook.Worksheets[kimutatás_Munkalap];

            MyExcel.Range AdatRange = Adatok_lap.Range[balfelső, jobbalsó];

            PivotCaches pivotCaches = xlWorkBook.PivotCaches();
            MyExcel.Range pivotData = Adatok_lap.Range[balfelső, jobbalsó];

            MyExcel.PivotCache pivotCache = pivotCaches.Create(XlPivotTableSourceType.xlDatabase, pivotData);
            MyExcel.PivotTable pivotTable = pivotCache.CreatePivotTable(Kimutatás_lap.Range[Kimutatás_cella], Kimutatás_név);

            //Táblázatban megjelenő érték
            if (összesítNév.Count > 0)
            {
                for (int i = 0; i < összesítNév.Count; i++)
                {

                    PivotField salesField = (PivotField)pivotTable.PivotFields(összesítNév[i]);
                    salesField.Orientation = XlPivotFieldOrientation.xlDataField;
                    switch (Összesítés_módja[i])
                    {

                        case "xlSum":
                            salesField.Function = XlConsolidationFunction.xlSum;
                            salesField.Name = összesítNév[i] + " db";
                            break;

                        case "xlCount":
                            salesField.Function = XlConsolidationFunction.xlCount;
                            salesField.Name = összesítNév[i] + " Összeg";
                            break;

                        default:
                            break;
                    }


                }
            }
            //oszlopok 
            if (oszlopNév.Count > 0)
            {
                for (int i = 0; i < oszlopNév.Count; i++)
                {
                    PivotField regionField = (PivotField)pivotTable.PivotFields(oszlopNév[i]);
                    regionField.Orientation = XlPivotFieldOrientation.xlColumnField;
                    regionField.Position = i + 1;
                }
            }

            //Sor adatok
            if (sorNév.Count > 0)
            {
                for (int i = 0; i < sorNév.Count; i++)
                {
                    PivotField colorsRowsField = (PivotField)pivotTable.PivotFields(sorNév[i]);
                    colorsRowsField.Orientation = XlPivotFieldOrientation.xlRowField;
                }
            }

            //Szűrő mezők
            if (SzűrőNév.Count > 0)
            {
                for (int i = 0; i < SzűrőNév.Count; i++)
                {
                    PivotField datefield = (PivotField)pivotTable.PivotFields(SzűrőNév[i]);
                    datefield.Orientation = XlPivotFieldOrientation.xlPageField;
                    datefield.EnableMultiplePageItems = true;
                }
            }
        }

        public static void Nyomtatás(string munkalap, int kezdőoldal, int példányszám)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];

            Munkalap.PrintOutEx(kezdőoldal, misValue, példányszám, false);

        }




        /// <summary>
        /// Munkalapot a jelzett helyen és sornál két részre osztja
        /// </summary>
        /// <param name="munkalap">munkalap neve</param>
        /// <param name="mit">Cella jelölés ahol osztani akarunk</param>
        /// <param name="sor">a sornak a neve ahol osztani akarunk</param>
        public static void Nyom_Oszt(string munkalap, string mit, int sor, int oldaltörés = 1)
        {

            xlApp.ActiveWindow.View = XlWindowView.xlPageBreakPreview;
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range Táblaterület = Munkalap.Range[mit];
            Munkalap.HPageBreaks.Add(Munkalap.Cells[sor, oldaltörés]);
        }


        public static void Diagram(string munkalap, int felsőx, int felsőy, int alsóx, int alsóy, string táblafelső, string táblaalsó)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];

            MyExcel.Range crange;
            MyExcel.ChartObjects cb = (MyExcel.ChartObjects)Munkalap.ChartObjects(Type.Missing);
            MyExcel.ChartObject cbc = (MyExcel.ChartObject)cb.Add(felsőx, felsőy, alsóx, alsóy);
            MyExcel.Chart cp = cbc.Chart;

            crange = Munkalap.get_Range(táblafelső, táblaalsó);
            cp.SetSourceData(crange, misValue);
            cp.ChartType = MyExcel.XlChartType.xlPie;
            cp.ApplyLayout(1);

        }

        //Elkopó
        public static long Munkalap(DT.DataTable Tábla, int sor, string munkalap)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            Munkalap.Select();

            //Fejléc
            for (int j = 0; j < Tábla.Columns.Count; j++)
            {
                Munkalap.Cells[sor, j + 1] = Tábla.Columns[j].ColumnName.ToString();
            }


            for (int i = 0; i < Tábla.Rows.Count; i++)
            {
                for (int j = 0; j < Tábla.Columns.Count; j++)
                {
                    Munkalap.Cells[i + sor + 1, j + 1] = Tábla.Rows[i].ItemArray[j];
                }
            }

            long utolsó_sor = Tábla.Rows.Count;
            return utolsó_sor;
        }

        public static int Tábla_Író(string hely, string jelszó, string szöveg, int sor, string munkalap)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            Munkalap.Select();

            DataGridView dataGridView1 = new DataGridView();
            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";

            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                OleDbDataAdapter Adapter = new OleDbDataAdapter(szöveg, Kapcsolat);

                DataSet Tábla = new DataSet();

                Adapter.Fill(Tábla);
                //Fejléc
                for (int j = 0; j < Tábla.Tables[0].Columns.Count; j++)
                {
                    Munkalap.Cells[sor, j + 1] = Tábla.Tables[0].Columns[j].ColumnName.ToString();
                }


                for (int i = 0; i < Tábla.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < Tábla.Tables[0].Columns.Count; j++)
                    {
                        Munkalap.Cells[i + sor + 1, j + 1] = Tábla.Tables[0].Rows[i].ItemArray[j];
                    }
                }

                int utolsó_sor = Tábla.Tables[0].Rows.Count;
                return utolsó_sor;
            }

        }

    }
}

using Microsoft.Office.Interop.Excel;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MyExcel = Microsoft.Office.Interop.Excel;

namespace Villamos
{
    public static partial class Module_Excel
    {
        const double MmToInch = 1.0 / 25.4;

        // JAVÍTANDÓ:copilot
        /// <summary>
        /// Nyomtatási területet 
        /// </summary>
        /// <param name="munkalap"></param>
        /// <param name="terület"></param>
        /// <param name="sorismétlődés"> "" vagy "$1:$1"</param>
        /// /// <param name="oszlopisnétlődés">"" vagy "$A:$B"</param>
        /// <param name="álló">Álló esetén true, fekvó esetén false</param>
        /// A
        public static void NyomtatásiTerület_részletes(string munkalap, string terület,
                                                       string sorismétlődés = "", string oszlopisnétlődés = "",
                                                       bool álló = true, string oldalszéles = "1", string oldalmagas = "")
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                Munkalap.Select();

                PageSetup Táblaterület = (MyExcel.PageSetup)Munkalap.PageSetup;

                Táblaterület.PrintTitleRows = sorismétlődés;
                Táblaterület.PrintTitleColumns = oszlopisnétlődés;

                Táblaterület.PrintArea = terület;
                {
                    Táblaterület.LeftHeader = "";
                    Táblaterület.CenterHeader = Program.PostásNév.Trim();
                    Táblaterület.RightHeader = DateTime.Now.ToString("yyyy.MM.dd hh:mm");
                    Táblaterület.LeftFooter = "";
                    Táblaterület.CenterFooter = @"&P/&N";
                    Táblaterület.RightFooter = "";
                    Táblaterület.LeftMargin = xlApp.InchesToPoints(15 * MmToInch);
                    Táblaterület.RightMargin = xlApp.InchesToPoints(15 * MmToInch);
                    Táblaterület.TopMargin = xlApp.InchesToPoints(20 * MmToInch);
                    Táblaterület.BottomMargin = xlApp.InchesToPoints(20 * MmToInch);
                    Táblaterület.HeaderMargin = xlApp.InchesToPoints(13 * MmToInch);
                    Táblaterület.FooterMargin = xlApp.InchesToPoints(13 * MmToInch);
                    Táblaterület.PrintHeadings = false;
                    Táblaterület.PrintGridlines = false;
                    Táblaterület.PrintComments = MyExcel.XlPrintLocation.xlPrintNoComments;
                    Táblaterület.CenterHorizontally = false;
                    // JAVÍTANDÓ:
                    //    Táblaterület.CenterVertically = false;
                    if (álló)
                        Táblaterület.Orientation = MyExcel.XlPageOrientation.xlPortrait;
                    else
                        Táblaterület.Orientation = MyExcel.XlPageOrientation.xlLandscape;

                    Táblaterület.Draft = false;
                    Táblaterület.PaperSize = MyExcel.XlPaperSize.xlPaperA4;
                    Táblaterület.Order = MyExcel.XlOrder.xlDownThenOver;
                    Táblaterület.BlackAndWhite = false;
                    Táblaterület.Zoom = false;
                    if (int.TryParse(oldalszéles, out int széles))
                        Táblaterület.FitToPagesWide = széles;
                    else
                        Táblaterület.FitToPagesWide = false;
                    if (int.TryParse(oldalmagas, out int magas))
                        Táblaterület.FitToPagesTall = magas;
                    else
                        Táblaterület.FitToPagesTall = false;
                    Táblaterület.PrintErrors = MyExcel.XlPrintErrors.xlPrintErrorsDisplayed;
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (ex.Message.Trim() == "PageSetup osztály LeftHeader tulajdonsága nem állítható be")
                    MessageBox.Show("Alapértelmezett nyomtató be van állítva?", "Nyomtató beállítási hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    HibaNapló.Log(ex.Message, "NyomtatásiTerület_részletes", ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new Exception("NyomtatásiTerület_részletes hiba");
                }
            }
        }

        /// <summary>
        ///         
        /// </summary>
        /// <param name="munkalap"></param>
        /// <param name="terület"></param>
        /// <param name="sorismétlődés"></param>
        /// <param name="oszlopisnétlődés"></param>
        /// <param name="fejbal"></param>
        /// <param name="fejközép"></param>
        /// <param name="fejjobb"></param>
        /// <param name="fénykép"></param>
        /// B
        public static void NyomtatásiTerület_részletes(string munkalap, string terület,
                                                       string sorismétlődés, string oszlopisnétlődés,
                                                       string fejbal, string fejközép, string fejjobb,
                                                       string fénykép)
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                Munkalap.Select();

                PageSetup Táblaterület = (MyExcel.PageSetup)Munkalap.PageSetup;

                Táblaterület.PrintTitleRows = sorismétlődés;
                Táblaterület.PrintTitleColumns = oszlopisnétlődés;

                Táblaterület.LeftHeaderPicture.Filename = fénykép;

                Táblaterület.PrintArea = terület;
                {
                    Táblaterület.LeftHeader = fejbal;
                    Táblaterület.CenterHeader = fejközép;
                    Táblaterület.RightHeader = fejjobb;
                    Táblaterület.LeftFooter = "";
                    Táblaterület.CenterFooter = @"&P/&N";
                    Táblaterület.RightFooter = "";
                    Táblaterület.LeftMargin = xlApp.InchesToPoints(15 * MmToInch);
                    Táblaterület.RightMargin = xlApp.InchesToPoints(15 * MmToInch);
                    Táblaterület.TopMargin = xlApp.InchesToPoints(20 * MmToInch);
                    Táblaterület.BottomMargin = xlApp.InchesToPoints(20 * MmToInch);
                    Táblaterület.HeaderMargin = xlApp.InchesToPoints(13 * MmToInch);
                    Táblaterület.FooterMargin = xlApp.InchesToPoints(13 * MmToInch);
                    Táblaterület.PrintHeadings = false;
                    Táblaterület.PrintGridlines = false;
                    Táblaterület.PrintComments = MyExcel.XlPrintLocation.xlPrintNoComments;
                    Táblaterület.CenterHorizontally = false;
                    Táblaterület.CenterVertically = false;
                    Táblaterület.Orientation = MyExcel.XlPageOrientation.xlPortrait;
                    Táblaterület.Draft = false;
                    Táblaterület.PaperSize = MyExcel.XlPaperSize.xlPaperA4;
                    Táblaterület.Order = MyExcel.XlOrder.xlDownThenOver;
                    Táblaterület.BlackAndWhite = false;
                    Táblaterület.Zoom = false;
                    Táblaterület.FitToPagesWide = 1;
                    Táblaterület.FitToPagesTall = false;
                    Táblaterület.PrintErrors = MyExcel.XlPrintErrors.xlPrintErrorsDisplayed;
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "NyomtatásiTerület_részletes", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Karbantartási munkalap nyomtatási terület beállítása
        /// </summary>
        /// <param name="munkalap"></param>
        /// <param name="terület"></param>
        /// <param name="sorismétlődés"></param>
        /// <param name="lábbal"></param>
        /// <param name="lábközép"></param>
        /// <param name="lábjobb"></param>
        /// C
        public static void NyomtatásiTerület_részletes(string munkalap, string terület,
                                                       string sorismétlődés,
                                                       string lábbal, string lábközép, string lábjobb)
        {
            try
            {

                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                PageSetup Táblaterület = Munkalap.PageSetup;
                Táblaterület.PrintTitleRows = sorismétlődés;
                Táblaterület.PrintArea = terület;
                Táblaterület.LeftFooter = lábbal;
                Táblaterület.CenterFooter = lábközép;
                Táblaterület.RightFooter = lábjobb;
                Táblaterület.LeftMargin = xlApp.InchesToPoints(6 * MmToInch); //6 mm
                Táblaterület.RightMargin = xlApp.InchesToPoints(6 * MmToInch);
                Táblaterület.TopMargin = xlApp.InchesToPoints(9 * MmToInch);     // ≈ 9 mm
                Táblaterület.BottomMargin = xlApp.InchesToPoints(14 * MmToInch);      // ≈ 14 mm
                Táblaterület.HeaderMargin = xlApp.InchesToPoints(8 * MmToInch);
                Táblaterület.FooterMargin = xlApp.InchesToPoints(8 * MmToInch);
                Táblaterület.PrintHeadings = false;
                Táblaterület.PrintGridlines = false;
                Táblaterület.PrintComments = MyExcel.XlPrintLocation.xlPrintNoComments;
                Táblaterület.CenterHorizontally = true;
                Táblaterület.Orientation = MyExcel.XlPageOrientation.xlPortrait;
                Táblaterület.Draft = false;
                Táblaterület.PaperSize = MyExcel.XlPaperSize.xlPaperA4;
                Táblaterület.Order = MyExcel.XlOrder.xlDownThenOver;
                Táblaterület.BlackAndWhite = false;
                Táblaterület.Zoom = false;
                Táblaterület.FitToPagesWide = 1;
                Táblaterület.FitToPagesTall = false;
                Táblaterület.PrintErrors = MyExcel.XlPrintErrors.xlPrintErrorsDisplayed;
            }
            catch (System.Runtime.InteropServices.COMException comEx)
            {
                // Tipikus nyomtatóhiba: nincs alapértelmezett nyomtató
                if (comEx.HResult == unchecked((int)0x800A03EC))
                {
                    MessageBox.Show("Alapértelmezett nyomtató be van állítva?", "Nyomtató beállítási hiba",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    HibaNapló.Log(comEx.Message, "NyomtatásiTerület_részletes", comEx.StackTrace, comEx.Source, comEx.HResult);
                    MessageBox.Show($"{comEx.Message}\n\nA hiba naplózásra került.", "Hiba történt",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "NyomtatásiTerület_részletes", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="munkalap">Munkalap neve</param>
        /// <param name="terület"> "" vagy "$1:$1"</param>
        /// <param name="balMargó">LeftMargin 18 cm formátumban</param>
        /// <param name="jobbMargó">RightMargin 18 cm formátumban</param>
        /// <param name="alsóMargó">BottomMargin 15 cm formátumban</param>
        /// <param name="felsőMargó">TopMargin 19 cm formátumban</param>
        /// <param name="fejlécMéret">HeaderMargin 8 formátumban</param>
        /// <param name="LáblécMéret">FooterMargin 8 formátumban</param>
        /// <param name="oldalszéles">"szám" vagy false</param>
        /// <param name="oldalmagas">"szám" vagy false</param>
        /// D
        public static void NyomtatásiTerület_részletes(string munkalap, string terület,
            int balMargó = 18,
            int jobbMargó = 18,
            int alsóMargó = 15,
            int felsőMargó = 19,
            int fejlécMéret = 8,
            int LáblécMéret = 8,
            string oldalszéles = "1",
            string oldalmagas = "1")
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                Munkalap.Select();

                PageSetup Táblaterület = (MyExcel.PageSetup)Munkalap.PageSetup;

                Táblaterület.PrintArea = terület;
                {
                    Táblaterület.LeftMargin = xlApp.InchesToPoints(balMargó * MmToInch);
                    Táblaterület.RightMargin = xlApp.InchesToPoints(jobbMargó * MmToInch);
                    Táblaterület.TopMargin = xlApp.InchesToPoints(felsőMargó * MmToInch);
                    Táblaterület.BottomMargin = xlApp.InchesToPoints(alsóMargó * MmToInch);
                    Táblaterület.HeaderMargin = xlApp.InchesToPoints(fejlécMéret * MmToInch);
                    Táblaterület.FooterMargin = xlApp.InchesToPoints(LáblécMéret * MmToInch);
                    Táblaterület.PrintHeadings = false;
                    Táblaterület.PrintGridlines = false;
                    Táblaterület.PrintComments = MyExcel.XlPrintLocation.xlPrintNoComments;
                    Táblaterület.Orientation = MyExcel.XlPageOrientation.xlPortrait;
                    Táblaterület.Draft = false;
                    Táblaterület.PaperSize = MyExcel.XlPaperSize.xlPaperA4;
                    Táblaterület.Order = MyExcel.XlOrder.xlDownThenOver;
                    Táblaterület.BlackAndWhite = false;
                    Táblaterület.Zoom = false;
                    if (int.TryParse(oldalszéles, out int széles))
                        Táblaterület.FitToPagesWide = széles;
                    else
                        Táblaterület.FitToPagesWide = false;
                    if (int.TryParse(oldalmagas, out int magas))
                        Táblaterület.FitToPagesTall = magas;
                    else
                        Táblaterület.FitToPagesTall = false;

                    Táblaterület.PrintErrors = MyExcel.XlPrintErrors.xlPrintErrorsDisplayed;
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (ex.Message.Trim() == "PageSetup osztály LeftHeader tulajdonsága nem állítható be")
                    MessageBox.Show("Alapértelmezett nyomtató be van állítva?", "Nyomtató beállítási hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    HibaNapló.Log(ex.Message, "NyomtatásiTerület_részletes", ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="munkalap"></param>
        /// <param name="terület"></param>
        /// <param name="sorismétlődés"></param>
        /// <param name="oszlopisnétlődés"></param>
        /// <param name="fejbal"></param>
        /// <param name="fejközép"></param>
        /// <param name="fejjobb"></param>
        /// <param name="lábbal"></param>
        /// <param name="lábközép"></param>
        /// <param name="lábjobb"></param>
        /// <param name="fénykép"></param>
        /// <param name="balMargó"></param>
        /// <param name="jobbMargó"></param>
        /// <param name="alsóMargó"></param>
        /// <param name="felsőMargó"></param>
        /// <param name="fejlécMéret"></param>
        /// <param name="LáblécMéret"></param>
        /// <param name="vízszintes"></param>
        /// <param name="függőleges"></param>
        /// <param name="Elrendezés"></param>
        /// E
        public static void NyomtatásiTerület_részletes(string munkalap, string terület, string sorismétlődés, string oszlopisnétlődés,
                    string fejbal, string fejközép, string fejjobb, string lábbal, string lábközép, string lábjobb, string fénykép, int balMargó, int jobbMargó,
                    int alsóMargó, int felsőMargó, int fejlécMéret, int LáblécMéret, bool vízszintes, bool függőleges, string Elrendezés)
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                Munkalap.Select();

                PageSetup Táblaterület = (MyExcel.PageSetup)Munkalap.PageSetup;

                Táblaterület.PrintTitleRows = sorismétlődés;
                Táblaterület.PrintTitleColumns = oszlopisnétlődés;

                Táblaterület.LeftHeaderPicture.Filename = fénykép;

                Táblaterület.PrintArea = terület;
                {
                    Táblaterület.LeftHeader = fejbal;
                    Táblaterület.CenterHeader = fejközép;
                    Táblaterület.RightHeader = fejjobb;
                    Táblaterület.LeftFooter = lábbal;
                    Táblaterület.CenterFooter = lábközép;
                    Táblaterület.RightFooter = lábjobb;
                    Táblaterület.LeftMargin = xlApp.InchesToPoints(balMargó * MmToInch);
                    Táblaterület.RightMargin = xlApp.InchesToPoints(jobbMargó * MmToInch);
                    Táblaterület.TopMargin = xlApp.InchesToPoints(felsőMargó * MmToInch);
                    Táblaterület.BottomMargin = xlApp.InchesToPoints(alsóMargó * MmToInch);
                    Táblaterület.HeaderMargin = xlApp.InchesToPoints(fejlécMéret * MmToInch);
                    Táblaterület.FooterMargin = xlApp.InchesToPoints(LáblécMéret * MmToInch);
                    Táblaterület.PrintHeadings = false;
                    Táblaterület.PrintGridlines = false;
                    Táblaterület.PrintComments = MyExcel.XlPrintLocation.xlPrintNoComments;
                    Táblaterület.CenterHorizontally = vízszintes;
                    Táblaterület.CenterVertically = függőleges;
                    if (Elrendezés != "Álló")
                        Táblaterület.Orientation = XlPageOrientation.xlLandscape;
                    else
                        Táblaterület.Orientation = XlPageOrientation.xlPortrait;
                    Táblaterület.Draft = false;
                    Táblaterület.PaperSize = MyExcel.XlPaperSize.xlPaperA4;
                    Táblaterület.Order = MyExcel.XlOrder.xlDownThenOver;
                    Táblaterület.BlackAndWhite = false;
                    Táblaterület.Zoom = false;
                    Táblaterület.FitToPagesWide = 1;
                    Táblaterület.FitToPagesTall = false;
                    Táblaterület.PrintErrors = MyExcel.XlPrintErrors.xlPrintErrorsDisplayed;
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (ex.Message.Trim() == "PageSetup osztály LeftHeader tulajdonsága nem állítható be")
                    MessageBox.Show("Alapértelmezett nyomtató be van állítva?", "Nyomtató beállítási hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    HibaNapló.Log(ex.Message, "NyomtatásiTerület_részletes", ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="munkalap   ">munkalap   </param>
        /// <param name="terület    ">terület    </param>
        /// <param name="balMargó   ">balMargó   </param>
        /// <param name="jobbMargó  ">jobbMargó  </param>
        /// <param name="alsóMargó  ">alsóMargó  </param>
        /// <param name="felsőMargó ">felsőMargó </param>
        /// <param name="fejlécMéret">fejlécMéret</param>
        /// <param name="LáblécMéret">LáblécMéret</param>
        /// <param name="oldalszéles">oldalszéles</param>
        /// <param name="oldalmagas ">oldalmagas </param>
        /// <param name="álló       ">álló       </param>
        /// <param name="papírméret ">papírméret </param>
        /// <param name="víz_közép  ">víz_közép  </param>
        /// <param name="Függ_közép ">Függ_közép </param>
        /// F
        public static void NyomtatásiTerület_részletes(string munkalap, string terület,
            int balMargó, int jobbMargó,
            int alsóMargó, int felsőMargó,
            int fejlécMéret, int LáblécMéret,
            string oldalszéles = "1", string oldalmagas = "1",
            bool álló = true, string papírméret = "A4",
            bool víz_közép = true, bool Függ_közép = true)
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                Munkalap.Select();

                PageSetup Táblaterület = (MyExcel.PageSetup)Munkalap.PageSetup;

                Táblaterület.PrintArea = terület;

                Táblaterület.LeftMargin = xlApp.InchesToPoints(balMargó * MmToInch);
                Táblaterület.RightMargin = xlApp.InchesToPoints(jobbMargó * MmToInch);
                Táblaterület.TopMargin = xlApp.InchesToPoints(felsőMargó * MmToInch);
                Táblaterület.BottomMargin = xlApp.InchesToPoints(alsóMargó * MmToInch);
                Táblaterület.HeaderMargin = xlApp.InchesToPoints(fejlécMéret * MmToInch);
                Táblaterület.FooterMargin = xlApp.InchesToPoints(LáblécMéret * MmToInch);
                Táblaterület.PrintHeadings = false;
                Táblaterület.PrintGridlines = false;

                Táblaterület.PrintComments = MyExcel.XlPrintLocation.xlPrintNoComments;
                if (álló)
                    Táblaterület.Orientation = MyExcel.XlPageOrientation.xlPortrait;
                else
                    Táblaterület.Orientation = MyExcel.XlPageOrientation.xlLandscape;

                Táblaterület.Draft = false;

                Táblaterület.CenterHorizontally = víz_közép;
                Táblaterület.CenterVertically = Függ_közép;

                if (papírméret == "A4")
                    Táblaterület.PaperSize = MyExcel.XlPaperSize.xlPaperA4;
                else
                    Táblaterület.PaperSize = MyExcel.XlPaperSize.xlPaperA3;

                Táblaterület.Order = MyExcel.XlOrder.xlDownThenOver;
                Táblaterület.BlackAndWhite = false;
                Táblaterület.Zoom = false;
                if (int.TryParse(oldalszéles, out int széles))
                    Táblaterület.FitToPagesWide = széles;
                else
                    Táblaterület.FitToPagesWide = false;
                if (int.TryParse(oldalmagas, out int magas))
                    Táblaterület.FitToPagesTall = magas;
                else
                    Táblaterület.FitToPagesTall = false;

                Táblaterület.PrintErrors = MyExcel.XlPrintErrors.xlPrintErrorsDisplayed;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (ex.Message.Trim() == "PageSetup osztály LeftHeader tulajdonsága nem állítható be")
                    MessageBox.Show("Alapértelmezett nyomtató be van állítva?", "Nyomtató beállítási hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    HibaNapló.Log(ex.Message, "NyomtatásiTerület_részletes", ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Teljes változat
        /// </summary>
        /// <param name="munkalap"></param>
        /// <param name="terület"></param>
        /// <param name="sorismétlődés"></param>
        /// <param name="oszlopisnétlődés"></param>
        /// <param name="fejbal"></param>
        /// <param name="fejközép"></param>
        /// <param name="fejjobb"></param>
        /// <param name="lábbal"></param>
        /// <param name="lábközép"></param>
        /// <param name="lábjobb"></param>
        /// <param name="fénykép"></param>
        /// <param name="balMargó"></param>
        /// <param name="jobbMargó"></param>
        /// <param name="alsóMargó"></param>
        /// <param name="felsőMargó"></param>
        /// <param name="fejlécMéret"></param>
        /// <param name="LáblécMéret"></param>
        /// <param name="vízszintes"></param>
        /// <param name="függőleges"></param>
        /// <param name="oldalszéles"></param>
        /// <param name="oldalmagas"></param>
        /// G
        public static void NyomtatásiTerület_részletes(string munkalap, string terület, string sorismétlődés, string oszlopisnétlődés,
                string fejbal, string fejközép, string fejjobb, string lábbal, string lábközép, string lábjobb, string fénykép, int balMargó, int jobbMargó,
                int alsóMargó, int felsőMargó, int fejlécMéret, int LáblécMéret, bool vízszintes, bool függőleges, string oldalszéles, string oldalmagas,
                bool álló = true, string papírméret = "A4")
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                Munkalap.Select();

                PageSetup Táblaterület = (MyExcel.PageSetup)Munkalap.PageSetup;

                Táblaterület.PrintTitleRows = sorismétlődés;
                Táblaterület.PrintTitleColumns = oszlopisnétlődés;

                Táblaterület.LeftHeaderPicture.Filename = fénykép;

                Táblaterület.PrintArea = terület;
                {
                    Táblaterület.LeftHeader = fejbal;
                    Táblaterület.CenterHeader = fejközép;
                    Táblaterület.RightHeader = fejjobb;
                    Táblaterület.LeftFooter = lábbal;
                    Táblaterület.CenterFooter = lábközép;
                    Táblaterület.RightFooter = lábjobb;
                    Táblaterület.LeftMargin = xlApp.InchesToPoints(balMargó * MmToInch);
                    Táblaterület.RightMargin = xlApp.InchesToPoints(jobbMargó * MmToInch);
                    Táblaterület.TopMargin = xlApp.InchesToPoints(felsőMargó * MmToInch);
                    Táblaterület.BottomMargin = xlApp.InchesToPoints(alsóMargó * MmToInch);
                    Táblaterület.HeaderMargin = xlApp.InchesToPoints(fejlécMéret * MmToInch);
                    Táblaterület.FooterMargin = xlApp.InchesToPoints(LáblécMéret * MmToInch);
                    Táblaterület.PrintHeadings = false;
                    Táblaterület.PrintGridlines = false;
                    Táblaterület.PrintComments = MyExcel.XlPrintLocation.xlPrintNoComments;
                    Táblaterület.CenterHorizontally = vízszintes;
                    Táblaterület.CenterVertically = függőleges;
                    Táblaterület.Orientation = MyExcel.XlPageOrientation.xlPortrait;
                    Táblaterület.Draft = false;
                    Táblaterület.PaperSize = MyExcel.XlPaperSize.xlPaperA4;
                    Táblaterület.Order = MyExcel.XlOrder.xlDownThenOver;
                    Táblaterület.BlackAndWhite = false;
                    Táblaterület.Zoom = false;
                    if (int.TryParse(oldalszéles, out int széles))
                        Táblaterület.FitToPagesWide = széles;
                    else
                        Táblaterület.FitToPagesWide = false;

                    if (int.TryParse(oldalmagas, out int magas))
                        Táblaterület.FitToPagesTall = magas;
                    else
                        Táblaterület.FitToPagesTall = false;

                    if (álló)
                        Táblaterület.Orientation = MyExcel.XlPageOrientation.xlPortrait;
                    else
                        Táblaterület.Orientation = MyExcel.XlPageOrientation.xlLandscape;

                    if (papírméret == "A4")
                        Táblaterület.PaperSize = MyExcel.XlPaperSize.xlPaperA4;
                    else
                        Táblaterület.PaperSize = MyExcel.XlPaperSize.xlPaperA3;
                }

                Táblaterület.PrintErrors = MyExcel.XlPrintErrors.xlPrintErrorsDisplayed;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (ex.Message.Trim() == "PageSetup osztály LeftHeader tulajdonsága nem állítható be")
                    MessageBox.Show("Alapértelmezett nyomtató be van állítva?", "Nyomtató beállítási hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    HibaNapló.Log(ex.Message, "NyomtatásiTerület_részletes", ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        public static void Nyomtatás(string munkalap, int kezdőoldal, int példányszám)
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                Munkalap.PrintOutEx(kezdőoldal, misValue, példányszám, false);
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Nyomtatás(munkalap {munkalap}, kezdőoldal {kezdőoldal}, példányszám {példányszám}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Munkalapot a jelzett helyen és sornál két részre osztja
        /// </summary>
        /// <param name="munkalap">munkalap neve</param>
        /// <param name="mit">Cella jelölés ahol osztani akarunk</param>
        /// <param name="sor">a sornak a neve ahol osztani akarunk</param>
        public static void Nyom_Oszt(string munkalap, string mit, int sor, int oldaltörés = 1)
        {
            try
            {
                xlApp.ActiveWindow.View = XlWindowView.xlPageBreakPreview;
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                MyExcel.Range Táblaterület = Munkalap.Range[mit];
                Munkalap.HPageBreaks.Add(Munkalap.Cells[sor, oldaltörés]);

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Nyom_Oszt(munkalap {munkalap}, mit {mit}, sor {sor}, oldaltörés {oldaltörés}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

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
        /// <summary>
        /// Kijelölt területet rácsoz
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="Kijelöltterület">szöveg kijelölt terület A1:V4 formában</param>

        public static void Rácsoz(string Kijelöltterület)
        {
            MyExcel.Range táblaterület = null;
            try
            {
                // Explicit munkalap használata (ajánlott, de most marad az xlApp is)
                Worksheet ws = (MyExcel.Worksheet)Module_Excel.xlWorkBook.ActiveSheet;
                táblaterület = ws.get_Range(Kijelöltterület);

                // Belső szegélyek: vékony
                táblaterület.Borders[MyExcel.XlBordersIndex.xlInsideHorizontal].LineStyle = MyExcel.XlLineStyle.xlContinuous;
                táblaterület.Borders[MyExcel.XlBordersIndex.xlInsideHorizontal].Weight = MyExcel.XlBorderWeight.xlThin;

                táblaterület.Borders[MyExcel.XlBordersIndex.xlInsideVertical].LineStyle = MyExcel.XlLineStyle.xlContinuous;
                táblaterület.Borders[MyExcel.XlBordersIndex.xlInsideVertical].Weight = MyExcel.XlBorderWeight.xlThin;

                // Külső szegélyek: VASTAG (közepes)
                táblaterület.Borders[MyExcel.XlBordersIndex.xlEdgeLeft].LineStyle = MyExcel.XlLineStyle.xlContinuous;
                táblaterület.Borders[MyExcel.XlBordersIndex.xlEdgeLeft].Weight = MyExcel.XlBorderWeight.xlMedium;

                táblaterület.Borders[MyExcel.XlBordersIndex.xlEdgeRight].LineStyle = MyExcel.XlLineStyle.xlContinuous;
                táblaterület.Borders[MyExcel.XlBordersIndex.xlEdgeRight].Weight = MyExcel.XlBorderWeight.xlMedium;

                táblaterület.Borders[MyExcel.XlBordersIndex.xlEdgeTop].LineStyle = MyExcel.XlLineStyle.xlContinuous;
                táblaterület.Borders[MyExcel.XlBordersIndex.xlEdgeTop].Weight = MyExcel.XlBorderWeight.xlMedium;

                táblaterület.Borders[MyExcel.XlBordersIndex.xlEdgeBottom].LineStyle = MyExcel.XlLineStyle.xlContinuous;
                táblaterület.Borders[MyExcel.XlBordersIndex.xlEdgeBottom].Weight = MyExcel.XlBorderWeight.xlMedium;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Rácsoz(Kijelöltterület: \"{Kijelöltterület}\") \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (táblaterület != null) Marshal.ReleaseComObject(táblaterület);
            }
        }

        /// <summary>
        /// Vastagkeretet készít a kijelölt területre
        /// </summary>
        /// <param name="Kijelöltterület">szöveg</param>
        public static void Vastagkeret(string Kijelöltterület)
        {
            MyExcel.Range Táblaterület = null;
            try
            {
                Táblaterület = Module_Excel.xlApp.get_Range(Kijelöltterület);
                //Táblaterület.BorderAround(MyExcel.XlLineStyle.xlContinuous,
                //         MyExcel.XlBorderWeight.xlMedium,
                //         MyExcel.XlColorIndex.xlColorIndexAutomatic,
                //         MyExcel.XlColorIndex.xlColorIndexAutomatic);
                // Külön-külön állítjuk be a szegélyeket
                Borders borders = Táblaterület.Borders;

                // Bal szegély
                borders[MyExcel.XlBordersIndex.xlEdgeLeft].LineStyle = MyExcel.XlLineStyle.xlContinuous;
                borders[MyExcel.XlBordersIndex.xlEdgeLeft].Weight = MyExcel.XlBorderWeight.xlMedium;

                // Jobb szegély
                borders[MyExcel.XlBordersIndex.xlEdgeRight].LineStyle = MyExcel.XlLineStyle.xlContinuous;
                borders[MyExcel.XlBordersIndex.xlEdgeRight].Weight = MyExcel.XlBorderWeight.xlMedium;

                // Felső szegély
                borders[MyExcel.XlBordersIndex.xlEdgeTop].LineStyle = MyExcel.XlLineStyle.xlContinuous;
                borders[MyExcel.XlBordersIndex.xlEdgeTop].Weight = MyExcel.XlBorderWeight.xlMedium;

                // Alsó szegély ← ez a kritikus!
                borders[MyExcel.XlBordersIndex.xlEdgeBottom].LineStyle = MyExcel.XlLineStyle.xlContinuous;
                borders[MyExcel.XlBordersIndex.xlEdgeBottom].Weight = MyExcel.XlBorderWeight.xlMedium;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Vastagkeret(Kijelöltterület {Kijelöltterület}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Táblaterület != null)
                {
                    Marshal.ReleaseComObject(Táblaterület);
                }
            }
        }

        /// <summary>
        /// Vékonykeretet készít a kijelölt területre
        /// </summary>
        /// <param name="mit">azöveg</param>
        public static void Vékonykeret(string Kijelöltterület)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.get_Range(Kijelöltterület);
                Táblaterület.BorderAround(MyExcel.XlLineStyle.xlContinuous,
                         MyExcel.XlBorderWeight.xlThin,
                         MyExcel.XlColorIndex.xlColorIndexAutomatic);

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Vékonykeret(Kijelöltterület {Kijelöltterület}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void VékonyFelső(string Kijelöltterület)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.get_Range(Kijelöltterület);
                Táblaterület.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;
                Táblaterület.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
                Táblaterület.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlLineStyleNone;
                Táblaterület.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                Táblaterület.Borders[XlBordersIndex.xlEdgeTop].ColorIndex = 0;
                Táblaterület.Borders[XlBordersIndex.xlEdgeTop].TintAndShade = 0;
                Táblaterület.Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlThin;

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"VékonyFelső(Kijelöltterület {Kijelöltterület}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void VastagFelső(string Kijelöltterület)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.get_Range(Kijelöltterület);
                Táblaterület.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;
                Táblaterület.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
                Táblaterület.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlLineStyleNone;
                Táblaterület.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                Táblaterület.Borders[XlBordersIndex.xlEdgeTop].ColorIndex = 0;
                Táblaterület.Borders[XlBordersIndex.xlEdgeTop].TintAndShade = 0;
                Táblaterület.Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlMedium;

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"VastagFelső(Kijelöltterület {Kijelöltterület}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void Pontvonal(string Kijelöltterület)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.get_Range(Kijelöltterület);
                Táblaterület.Borders.Item[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                Táblaterület.Borders[XlBordersIndex.xlEdgeTop].ColorIndex = XlColorIndex.xlColorIndexAutomatic;
                Táblaterület.Borders.Item[XlBordersIndex.xlEdgeTop].TintAndShade = 0;
                Táblaterület.Borders.Item[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlHairline;

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Pontvonal(Kijelöltterület {Kijelöltterület}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void Aláírásvonal(string Kijelöltterület)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.get_Range(Kijelöltterület);

                // Átlós és oldalsó szegélyek kikapcsolása
                Táblaterület.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlLineStyleNone;
                Táblaterület.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = XlLineStyle.xlLineStyleNone;
                Táblaterület.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlLineStyleNone;
                Táblaterület.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlLineStyleNone;
                Táblaterület.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlLineStyleNone;
                Táblaterület.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlLineStyleNone;
                Táblaterület.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlLineStyleNone;

                // Felső szegély: szaggatott aláírásvonal
                Border felső = Táblaterület.Borders[XlBordersIndex.xlEdgeTop];
                felső.LineStyle = XlLineStyle.xlDash;
                felső.ColorIndex = XlColorIndex.xlColorIndexAutomatic;
                felső.TintAndShade = 0;
                felső.Weight = XlBorderWeight.xlThin;

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Aláírásvonal(Kijelöltterület: {Kijelöltterület}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void Keret(string Kijelöltterület, bool jobb, bool bal, bool alsó, bool felső)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.get_Range(Kijelöltterület);

                if (jobb)
                {
                    Border jobbSzegely = Táblaterület.Borders[XlBordersIndex.xlEdgeRight];
                    jobbSzegely.LineStyle = XlLineStyle.xlContinuous;
                    jobbSzegely.ColorIndex = XlColorIndex.xlColorIndexAutomatic;
                    jobbSzegely.TintAndShade = 0;
                    jobbSzegely.Weight = XlBorderWeight.xlThin;
                }

                if (bal)
                {
                    Border balSzegely = Táblaterület.Borders[XlBordersIndex.xlEdgeLeft];
                    balSzegely.LineStyle = XlLineStyle.xlContinuous;
                    balSzegely.ColorIndex = XlColorIndex.xlColorIndexAutomatic;
                    balSzegely.TintAndShade = 0;
                    balSzegely.Weight = XlBorderWeight.xlThin;
                }

                if (alsó)
                {
                    Border alsoSzegely = Táblaterület.Borders[XlBordersIndex.xlEdgeBottom];
                    alsoSzegely.LineStyle = XlLineStyle.xlContinuous;
                    alsoSzegely.ColorIndex = XlColorIndex.xlColorIndexAutomatic;
                    alsoSzegely.TintAndShade = 0;
                    alsoSzegely.Weight = XlBorderWeight.xlThin;
                }

                if (felső)
                {
                    Border felsoSzegely = Táblaterület.Borders[XlBordersIndex.xlEdgeTop];
                    felsoSzegely.LineStyle = XlLineStyle.xlContinuous;
                    felsoSzegely.ColorIndex = XlColorIndex.xlColorIndexAutomatic;
                    felsoSzegely.TintAndShade = 0;
                    felsoSzegely.Weight = XlBorderWeight.xlThin;
                }

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Keret(Kijelöltterület: {Kijelöltterület}, jobb: {jobb}, bal: {bal}, alsó: {alsó}, felső: {felső}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void FerdeVonal(string Kijelöltterület)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.get_Range(Kijelöltterület);

                Border ferde = Táblaterület.Borders[XlBordersIndex.xlDiagonalDown];
                ferde.LineStyle = XlLineStyle.xlContinuous;
                ferde.ColorIndex = XlColorIndex.xlColorIndexAutomatic;
                ferde.TintAndShade = 0;
                ferde.Weight = XlBorderWeight.xlThin;

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"FerdeVonal(Kijelöltterület: {Kijelöltterület}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

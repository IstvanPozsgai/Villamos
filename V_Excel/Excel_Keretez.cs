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
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.get_Range(Kijelöltterület);

                XlBordersIndex[] szegélyek = new[]
                {
                    XlBordersIndex.xlEdgeLeft,
                    XlBordersIndex.xlEdgeRight,
                    XlBordersIndex.xlEdgeTop,
                    XlBordersIndex.xlEdgeBottom,
                    XlBordersIndex.xlInsideHorizontal,
                    XlBordersIndex.xlInsideVertical
               };

                foreach (XlBordersIndex index in szegélyek)
                {
                    Táblaterület.Borders[index].LineStyle = XlLineStyle.xlContinuous;
                    Táblaterület.Borders[index].Weight = XlBorderWeight.xlThin;
                }

                Táblaterület.BorderAround(
                    LineStyle: XlLineStyle.xlContinuous,
                    Weight: XlBorderWeight.xlMedium,
                    ColorIndex: XlColorIndex.xlColorIndexAutomatic
                );

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Rácsoz(Kijelöltterület: \"{Kijelöltterület}\") \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Vastagkeretet készít a kijelölt területre
        /// </summary>
        /// <param name="Kijelöltterület">szöveg</param>
        public static void Vastagkeret(string Kijelöltterület)
        {
            try
            {
                MyExcel.Range Táblaterület = Module_Excel.xlApp.get_Range(Kijelöltterület);
                Táblaterület.BorderAround(MyExcel.XlLineStyle.xlContinuous,
                         MyExcel.XlBorderWeight.xlMedium,
                         MyExcel.XlColorIndex.xlColorIndexAutomatic,
                         MyExcel.XlColorIndex.xlColorIndexAutomatic);

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Vastagkeret(Kijelöltterület {Kijelöltterület}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

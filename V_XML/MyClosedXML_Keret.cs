using ClosedXML.Excel;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Villamos
{
    public static partial class MyClosedXML_Excel
    {
        public static void Rácsoz(string munkalapnév, string Kijelöltterület)
        {
            try
            {
                // Tartomány lekérése az aktuális (aktív) munkalapon
                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapnév);
                IXLRange tartomány = xlWorkSheet.Range(Kijelöltterület);

                // === Külső szegélyek: MEDIUM ===
                tartomány.Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                tartomány.Style.Border.RightBorder = XLBorderStyleValues.Medium;
                tartomány.Style.Border.TopBorder = XLBorderStyleValues.Medium;
                tartomány.Style.Border.BottomBorder = XLBorderStyleValues.Medium;

                // Belső rács: vékony (mind vízszintes, mind függőleges)
                tartomány.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
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
        public static void Vastagkeret(string munkalapnév, string Kijelöltterület)
        {
            try
            {
                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapnév);
                IXLRange tartomány = xlWorkSheet.Range(Kijelöltterület);

                // Bal szegély
                tartomány.Style.Border.LeftBorder = XLBorderStyleValues.Medium;

                // Jobb szegély
                tartomány.Style.Border.RightBorder = XLBorderStyleValues.Medium;

                // Felső szegély
                tartomány.Style.Border.TopBorder = XLBorderStyleValues.Medium;

                // Alsó szegély ← ez a kritikus!
                tartomány.Style.Border.BottomBorder = XLBorderStyleValues.Medium;

                tartomány.Style.Border.InsideBorder = XLBorderStyleValues.None;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Vastagkeret(Kijelöltterület {Kijelöltterület}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Aláírásvonal(string Kijelöltterület)
        {
            try
            {
                IXLRange tartomány = xlWorkSheet.Range(Kijelöltterület);

                // Bal szegély
                tartomány.Style.Border.LeftBorder = XLBorderStyleValues.None;

                // Jobb szegély
                tartomány.Style.Border.RightBorder = XLBorderStyleValues.None;

                // Felső szegély
                tartomány.Style.Border.TopBorder = XLBorderStyleValues.Dashed;

                // Alsó szegély ← ez a kritikus!
                tartomány.Style.Border.BottomBorder = XLBorderStyleValues.None;

            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Aláírásvonal(Kijelöltterület: {Kijelöltterület}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Kijelöltterület"></param>
        /// <param name="jobb">jobb felső</param>
        public static void FerdeVonal(string Kijelöltterület, bool jobb = true)
        {
            try
            {
                IXLRange tartomány = xlWorkSheet.Range(Kijelöltterület);
                if (jobb)
                    tartomány.Style.Border.DiagonalDown = true;
                else
                    tartomány.Style.Border.DiagonalUp = true;
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

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
                IXLRange tartomany = munkalap.Range(Kijelöltterület);

                // === Külső szegélyek: MEDIUM ===
                tartomany.Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                tartomany.Style.Border.RightBorder = XLBorderStyleValues.Medium;
                tartomany.Style.Border.TopBorder = XLBorderStyleValues.Medium;
                tartomany.Style.Border.BottomBorder = XLBorderStyleValues.Medium;

                // Belső rács: vékony (mind vízszintes, mind függőleges)
                tartomany.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Rácsoz(Kijelöltterület: \"{Kijelöltterület}\") \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        // JAVÍTANDÓ:
        public static void Rácsoz(string Kijelöltterület)
        {
            try
            {
                // Tartomány lekérése az aktuális (aktív) munkalapon

                var tartomany = xlWorkSheet.Range(Kijelöltterület);

                // === Külső szegélyek: MEDIUM ===
                tartomany.Style.Border.LeftBorder = XLBorderStyleValues.Medium;
                tartomany.Style.Border.RightBorder = XLBorderStyleValues.Medium;
                tartomany.Style.Border.TopBorder = XLBorderStyleValues.Medium;
                tartomany.Style.Border.BottomBorder = XLBorderStyleValues.Medium;

                // Belső rács: vékony (mind vízszintes, mind függőleges)
                tartomany.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
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
                var tartomány = xlWorkSheet.Range(Kijelöltterület);

                // Bal szegély
                tartomány.Style.Border.LeftBorder = XLBorderStyleValues.Medium;

                // Jobb szegély
                tartomány.Style.Border.RightBorder = XLBorderStyleValues.Medium;

                // Felső szegély
                tartomány.Style.Border.TopBorder = XLBorderStyleValues.Medium;

                // Alsó szegély ← ez a kritikus!
                tartomány.Style.Border.BottomBorder = XLBorderStyleValues.Medium;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Vastagkeret(Kijelöltterület {Kijelöltterület}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}

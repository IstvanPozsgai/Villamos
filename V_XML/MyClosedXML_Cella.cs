using ClosedXML.Excel;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Villamos
{

    public static partial class MyClosedXML_Excel
    {
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
        ///  A szöveg helyzetét lehet meghatározni a cellában bal és jobb a kötött név minden egyéb középre kerül.
        /// </summary>
        /// <param name="mit"></param>
        /// <param name="irány">bal/jobb/közép</param>
        public static void Igazít_vízszintes(string munkalap, string mit, string irány)
        {
            try
            {
                // Munkalap lekérése név alapján
                IXLWorksheet munkalapObj = xlWorkBook.Worksheet(munkalap);

                // Tartomány egyesítése
                IXLRange tartomany = munkalapObj.Range(mit);

                XLAlignmentHorizontalValues alignment;
                if (irány.Trim() == "bal")
                    alignment = XLAlignmentHorizontalValues.Left;
                else if (irány.Trim() == "jobb")
                    alignment = XLAlignmentHorizontalValues.Right;
                else
                    alignment = XLAlignmentHorizontalValues.Center;
                tartomany.Style.Alignment.Horizontal = alignment;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Igazít_vízszintes(mit {mit}, irány {irány}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// szöveg függőleges helyzetét lehet megadni
        /// </summary>
        /// <param name="mit"></param>
        /// <param name="irány">felső/alsó/közép</param>
        public static void Igazít_függőleges(string munkalap, string mit, string irány)
        {
            try
            {
                // Munkalap lekérése név alapján
                IXLWorksheet munkalapObj = xlWorkBook.Worksheet(munkalap);

                // Tartomány egyesítése
                IXLRange tartomany = munkalapObj.Range(mit);

                XLAlignmentVerticalValues alignment;

                if (irány.Trim() == "felső")
                    alignment = XLAlignmentVerticalValues.Top;
                else if (irány.Trim() == "alsó")
                    alignment = XLAlignmentVerticalValues.Bottom;
                else
                    alignment = XLAlignmentVerticalValues.Center;
                tartomany.Style.Alignment.Vertical = alignment;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Igazít_függőleges(mit {mit}, irány {irány}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

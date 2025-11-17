using ClosedXML.Excel;
using System;
using System.Diagnostics;
using System.Windows.Forms;


namespace Villamos
{

    public static partial class MyClosedXML_Excel
    {
        public static void Betű(string munkalapnév, string mit, System.Drawing.Color színe)
        {
            try
            {
                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapnév);
                IXLRange tartomány = munkalap.Range(mit);
                tartomány.Style.Font.FontColor = XLColor.FromColor(színe);
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Betű(mit: {mit}, szín: {színe.Name}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\nA hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Betű(string munkalapnév, string mit, bool aláhúzott, bool dőlt, bool vastag)
        {
            try
            {
                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapnév);
                IXLRange tartomány = munkalap.Range(mit);

                // Aláhúzás
                //         tartomány.Style.Font.Underline = XLUnderlineValues.Single;

                // Dőlt és félkövér
                tartomány.Style.Font.Italic = dőlt;
                tartomány.Style.Font.Bold = vastag;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Betű(mit: {mit}, aláhúzott: {aláhúzott}, dőlt: {dőlt}, vastag: {vastag}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }








        // JAVÍTANDÓ:
        /// <summary>
        /// Betű típusát és méretét állítja be a munkalapon
        /// </summary>
        /// <param name="név"></param>
        /// <param name="méret"></param>
        public static void Munkalap_betű(string név, int méret)
        {
            try
            {
                // Feltételezzük, hogy xlWorkSheet az aktuális/aktív munkalap
                IXLWorksheet munkalap = xlWorkSheet;

                // Alapértelmezett stílus beállítása a munkalap szintjén
                munkalap.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                munkalap.Style.Font.FontName = név;
                munkalap.Style.Font.FontSize = méret;
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
        /// Betűméretet lehet beállítani
        /// </summary>
        /// <param name="mit">szöveg</param>
        /// <param name="méret">egész</param>
        public static void Betű(string mit, int méret)
        {
            try
            {
                // Feltételezzük, hogy xlWorkSheet az aktuális/aktív munkalap
                var tartomány = xlWorkSheet.Range(mit);

                // Betűméret beállítása
                tartomány.Style.Font.FontSize = méret;

            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Betű(mit: \"{mit}\", méret: {méret}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public static void Betű(string mit, bool aláhúzott, bool dőlt, bool vastag)
        {
            try
            {

                var tartomány = xlWorkSheet.Range(mit);

                // Aláhúzás
                //         tartomány.Style.Font.Underline = XLUnderlineValues.Single;

                // Dőlt és félkövér
                tartomány.Style.Font.Italic = dőlt;
                tartomány.Style.Font.Bold = vastag;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Betű(mit: {mit}, aláhúzott: {aláhúzott}, dőlt: {dőlt}, vastag: {vastag}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}

using ClosedXML.Excel;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Villamos
{
    public static partial class MyClosedXML_Excel
    {
        /// <summary>
        /// Megadott oszlop szélesség beállítása az oszlopnál
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="oszlop">string oszlopnév</param>
        /// <param name="szélesség">double szélesség, ha nincs megadva akkor automatikus</param>

        public static void Oszlopszélesség(string munkalapNév, string oszlopTartomány, double szélesség = -1)
        {
            try
            {
                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapNév);

                // Ellenőrizzük, hogy tartomány-e (tartalmaz-e ":")
                if (oszlopTartomány.Contains(':'))
                {
                    // Példa: "A:K" → szétválasztjuk
                    string[] részek = oszlopTartomány.Split(':');
                    if (részek.Length != 2) throw new ArgumentException($"Érvénytelen oszloptartomány: {oszlopTartomány}");

                    string kezdőOszlop = részek[0].Trim();
                    string végOszlop = részek[1].Trim();

                    // Konvertáljuk oszlopszámokká
                    int kezdőIndex = XLHelper.GetColumnNumberFromLetter(kezdőOszlop);
                    int végIndex = XLHelper.GetColumnNumberFromLetter(végOszlop);

                    if (kezdőIndex <= 0 || végIndex <= 0 || kezdőIndex > végIndex) throw new ArgumentException($"Érvénytelen oszloptartomány: {oszlopTartomány}");

                    // Végigmegyünk az oszlopokon
                    for (int i = kezdőIndex; i <= végIndex; i++)
                    {
                        OszlopotÁllít(i, munkalap, szélesség);
                    }
                }
                else
                {
                    // Egyetlen oszlop
                    int kezdőIndex = XLHelper.GetColumnNumberFromLetter(oszlopTartomány);
                    OszlopotÁllít(kezdőIndex, munkalap, szélesség);
                }
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Oszlopszélesség(munkalap: {munkalapNév}, oszlop: {oszlopTartomány}, szélesség: {szélesség}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\nA hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void OszlopotÁllít(int i, IXLWorksheet munkalap, double szélesség = -1)
        {
            if (szélesség > 0)
                munkalap.Column(i).Width = szélesség;
            else
            {
                IXLColumn oszlop = munkalap.Column(i);
                oszlop.AdjustToContents();
                if (oszlop.Width > 80) oszlop.Width = 80;
            }

        }

        public static void Háttérszín(string munkalapnév, string mit, System.Drawing.Color színe)
        {
            try
            {
                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapnév);
                IXLRange tartomány = munkalap.Range(mit);
                tartomány.Style.Fill.BackgroundColor = XLColor.FromColor(színe);
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Háttérszín(mit {mit}, színe {színe}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\nA hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        // JAVÍTANDÓ:

        /// <summary>
        /// Sormagasságot lehet beállítani
        /// </summary>
        /// <param name="mit">szöveg</param>
        /// <param name="mekkora">egész, ha -1 akkor automatikus sormagasságot akarunk beállítani</param>
        /// 
        public static void Sormagasság(string mit, int mekkora)
        {
            try
            {
                var tartomány = xlWorkSheet.Range(mit);

                if (mekkora > 0)
                {
                    // Fix sor magasság beállítása (Excel egységekben)
                    foreach (var sor in tartomány.Rows())
                    {
                        // IXLRangeRow-nak nincs Height property-je, de van WorksheetRow() metódusa,
                        // ami IXLRow-t ad vissza, azon már van Height property.
                        sor.WorksheetRow().Height = mekkora;
                    }
                }
                else
                {
                    // AutoFit NEM támogatott ClosedXML-ben.
                    // Alternatíva: alapértelmezett magasság (pl. 15), vagy semmi.
                    // Itt választhatod, hogy mit szeretnél:
                    //
                    // Opció 1: alapértelmezett magasság
                    // foreach (var sor in tartomány.Rows()) { sor.WorksheetRow().Height = 15; }
                    //
                    // Opció 2: nem csinálunk semmit (marad az alapértelmezett)
                    // (ez a legbiztonságosabb, mert az Excel alapértelmezetten jól méretez)
                }
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Sormagasság(mit: {mit}, mekkora: {mekkora}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

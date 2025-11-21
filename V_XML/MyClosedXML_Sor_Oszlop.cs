using ClosedXML.Excel;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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

        public static void Sortörésseltöbbsorba(string munkalapnév, string mit, bool egyesített = false)
        {
            try
            {
                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapnév);
                IXLRange range = munkalap.Range(mit);
                if (egyesített) range.Merge();
                range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.General;
                range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                range.Style.Alignment.WrapText = true;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Sortörésseltöbbsorba(mit {mit}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                if (ex.HResult == -2146777998)
                {
                    MessageBox.Show(ex.Message, "A program figyelmet igényel", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static void Sormagasság(string munkalapnév, string mit, int mekkora)
        {
            try
            {
                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapnév);

                // Tartomány lekérése – pl. "A5" vagy "B2:D10"
                IXLRange tartomány = munkalap.Range(mit);

                // Az érintett sorok számai
             var érintettSorok = Enumerable.Range(tartomány.FirstRow().RowNumber(),
                                                    tartomány.RowCount()).ToList();
                if (mekkora > 0)
                {
                    // Fix sor magasság beállítása (pontban – Excel-ben 1 egység = 1 pont)
                    foreach (int sor in érintettSorok)
                    {
                        munkalap.Row(sor).Height = mekkora;
                    }
                }
                else
                {
                    // ⚠️ ClosedXML NEM támogatja natívan az AutoFit sor magasságot!
                    // De van mód arra, hogy "jó közelítéssel" automatikusan méretezzünk:
                    // 1. Távolítsuk el a fix magasságot (állítsuk "alapértelmezett" értékre)
                    // 2. Bekapcsoljuk a WrapText-et (ha többsoros szöveg van)
                    // 3. Megbízunk abban, hogy az Excel majd AutoFit-tel nyitja meg

                    foreach (int sor in érintettSorok)
                    {
                        // Alapértelmezett sor magasság visszaállítása (~15 pont)
                        munkalap.Row(sor).Height = 15.0;

                        // Opcionális: ha a cellákban sortörés van, az Excel AutoFit-tel fog működni
                        // (ezt már korábban beállíthattad a stílusban)
                    }
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

using ClosedXML.Excel;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using Villamos.V_Adatszerkezet;



namespace Villamos
{

    public static partial class MyClosedXML_Excel
    {
        public static void Betű(string munkalapnév, string mit, Beállítás_Betű beállítás)
        {
            try
            {
                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapnév);
                IXLRange tartomány = munkalap.Range(mit);
                tartomány.Style.Font.FontColor = XLColor.FromColor(beállítás.Szín);
                tartomány.Style.Font.Italic = beállítás.Dőlt;
                tartomány.Style.Font.Bold = beállítás.Vastag;
                if (beállítás.Aláhúzott) tartomány.Style.Font.Underline = XLFontUnderlineValues.Single;                   //nincs próbálva
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Betű(mit: {mit})  Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\nA hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Munkalap_betű(string munkalapnév, string név, int méret)
        {
            try
            {

                IXLWorksheet munkalap = xlWorkBook.Worksheet(munkalapnév);
                // Stílus beállítása a munkalap alapértelmezett stílusára (ez éri el a teljes munkalapot)
                munkalap.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                munkalap.Style.Font.SetFontName(név);
                munkalap.Style.Font.FontSize = méret;
                munkalap.Style.Font.Strikethrough = false;
                munkalap.Style.Font.VerticalAlignment = XLFontVerticalTextAlignmentValues.Baseline; // nem felső/alsó index
                munkalap.Style.Font.Underline = XLFontUnderlineValues.None;
                // ClosedXML nem támogatja közvetlenül ThemeColor, TintAndShade, OutlineFont, Shadow stb. ugyanúgy,
                // mint az Interop – ezeket általában nem kell külön visszaállítani, mert alapértelmezés szerint nincsenek bekapcsolva.

            }
            catch (Exception ex)
            {
                var hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Munkalap_betű(név: {név}, méret: {méret}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\nA hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

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
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Betű(mit: {mit})  Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\nA hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

using ClosedXML.Excel;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Villamos
{
    public static partial class MyClosedXML_Excel
    {
        public static string Beolvas(string munkalapnév, string honnan)
        {
            string válasz = "_";
            try
            {
                IXLWorksheet lap = xlWorkBook.Worksheet(munkalapnév);
                válasz = lap.Cell(honnan).Value.ToStrTrim();
                if(válasz.Trim ()=="") válasz = "_";
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Beolvas(honnan {honnan}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return válasz;
        }

        public static void Értékmásol(string munkalapnév, string honnan, string hova)
        {
            try
            {
                IXLWorksheet lap = xlWorkBook.Worksheet(munkalapnév);
                IXLRange forrás = lap.Range(honnan);
                IXLCell célKezdo = lap.Cell(hova); // Működik akár "D1", akár "D1:E2" – de csak az első cellát használjuk

                int startRow = célKezdo.Address.RowNumber;
                int startCol = célKezdo.Address.ColumnNumber;

                int forrásElsoSor = forrás.FirstRow().RowNumber();
                int forrásElsoOszlop = forrás.FirstColumn().ColumnNumber();

                foreach (var forrásCella in forrás.Cells())
                {
                    int sorEltolás = forrásCella.Address.RowNumber - forrásElsoSor;
                    int oszlopEltolás = forrásCella.Address.ColumnNumber - forrásElsoOszlop;

                    lap.Cell(startRow + sorEltolás, startCol + oszlopEltolás)
                        .Value = forrásCella.Value; // Csak érték – képlet nem másolódik
                }
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Értékmásol(munkalap {munkalapnév}, honnan {honnan}, hova {hova}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

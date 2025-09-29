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
        /// A táblázatot rögzíti a beállított sornak megfelelően
        /// </summary>
        /// <param name="sor">sor</param>
        public static void Tábla_Rögzítés(int sor)
        {
            try
            {
                xlApp.ActiveWindow.SplitColumn = 0;
                xlApp.ActiveWindow.SplitRow = sor;
                xlApp.ActiveWindow.FreezePanes = true;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Tábla_Rögzítés(sor {sor}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// automata szűrést kikapcsolja
        /// </summary>
        /// <param name="munkalap"></param>
        public static void SzűrésKi(string munkalap)
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                Munkalap.Activate();
                Munkalap.AutoFilterMode = false;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"SzűrésKi(munkalap {munkalap}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// A küldött névvel beszúr utolsó lapnak egy munkalapot
        /// </summary>
        /// <param name="név"></param>
        public static void Új_munkalap(string munkalap)
        {
            try
            {
                //Munakalap hozzáadás
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets.Add();
                // munkalap átnevezéséhez 
                Munkalap.Name = munkalap;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Új_munkalap(munkalap {munkalap}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Egy munkalapot átnevez és aktívvá teszi
        /// </summary>
        /// <param name="régi"></param>
        /// <param name="új"></param>
        public static void Munkalap_átnevezés(string régi, string új)
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[régi];
                Munkalap.Name = új;
                Munkalap.Select();
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Munkalap_átnevezés(régi {régi}, új {új}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Kijelöljük a munkalapot
        /// </summary>
        /// <param name="név"></param>
        public static void Munkalap_aktív(string munkalap)
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                Munkalap.Activate();
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Munkalap_aktív(munkalap {munkalap}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void Szűrés(string munkalap, string oszloptól, string oszlopig, int sorig, int sortól = 1)
        {
            try
            {
                Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
                string kezdőCella = oszloptól + sortól;
                string utolsóCella = oszlopig + sorig;
                MyExcel.Range Táblaterület = Munkalap.get_Range(kezdőCella, utolsóCella);
                object result = Táblaterület.AutoFilter(sortól);

                Marshal.ReleaseComObject(Táblaterület);
                Táblaterület = null;
            }
            catch (Exception ex)
            {
                StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
                string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
                HibaNapló.Log(ex.Message, $"Szűrés(munkalap: {munkalap}, oszloptól: {oszloptól}, oszlopig: {oszlopig}, sorig: {sorig},sortól: {sortól} ) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

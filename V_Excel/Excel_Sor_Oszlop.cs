using System;
using System.Windows.Forms;
using MyExcel = Microsoft.Office.Interop.Excel;

namespace Villamos
{
    public static partial class Module_Excel
    {
        /// <summary>
        /// Munkalapon a jelölt sor elé beszúr meghatározott sorokat.
        /// </summary>
        /// <param name="munkalap">munkalap neve</param>
        /// <param name="sor">a sorszám ahova kell beszúrni</param>
        /// <param name="beszúrás">beszúrandó sorok száma</param>
        public static void SorBeszúrás(string munkalap, int sor, int beszúrás)
        {
            try
            {
                MyExcel.Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];

                // Ellenőrizzük, hogy a munkalap nem null-e
                if (Munkalap == null) throw new ArgumentNullException(nameof(Munkalap), "A munkalap nem lehet null.");

                // Ellenőrizzük, hogy a sorindex érvényes-e
                if (sor <= 0) throw new ArgumentOutOfRangeException(nameof(sor), "A sorindexnek 1-nél nagyobbnak kell lennie.");

                // Beszúrunk egy sort az adott pozícióba
                for (int i = 0; i < beszúrás; i++)
                {
                    MyExcel.Range row = (MyExcel.Range)Munkalap.Rows[sor];
                    row.Insert(MyExcel.XlInsertShiftDirection.xlShiftDown);
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"SorBeszúrás(munkalap: {munkalap}, munkalap: {sor}, munkalap: {beszúrás})", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


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
                MyExcel.Range Táblaterület = Module_Excel.xlApp.get_Range(mit);
                if (mekkora > 0)
                    Táblaterület.RowHeight = mekkora;
                else
                    Táblaterület.EntireRow.AutoFit();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"Sormagasság(mit: {mit}, mekkora: {(mekkora.HasValue ? mekkora.Value.ToString() : "AutoFit")})", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

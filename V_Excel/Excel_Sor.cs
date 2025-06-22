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
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "SorBeszúrás", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sormagasságot lehet beállítani
        /// </summary>
        /// <param name="mit">szöveg</param>
        /// <param name="mekkora">egész</param>
        /// 

        public static void Sormagasság(string mit, int mekkora)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.RowHeight = mekkora;
        }

        /// <summary>
        /// Automata sormagasság beállítása
        /// </summary>
        /// <param name="mit"></param>
        public static void Sormagasság(string mit)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.EntireRow.AutoFit();
        }
    }
}

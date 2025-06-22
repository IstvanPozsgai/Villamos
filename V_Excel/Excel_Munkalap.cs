using Microsoft.Office.Interop.Excel;
using DT = System.Data;
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
            xlApp.ActiveWindow.SplitColumn = 0;
            xlApp.ActiveWindow.SplitRow = sor;
            xlApp.ActiveWindow.FreezePanes = true;
        }


        /// <summary>
        /// automata szűrést kikapcsolja
        /// </summary>
        /// <param name="munkalap"></param>
        public static void SzűrésKi(string munkalap)
        {

            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            Munkalap.AutoFilterMode = false;
        }


        //Elkopó
        public static long Munkalap(DT.DataTable Tábla, int sor, string munkalap)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            Munkalap.Select();

            //Fejléc
            for (int j = 0; j < Tábla.Columns.Count; j++)
            {
                Munkalap.Cells[sor, j + 1] = Tábla.Columns[j].ColumnName.ToString();
            }


            for (int i = 0; i < Tábla.Rows.Count; i++)
            {
                for (int j = 0; j < Tábla.Columns.Count; j++)
                {
                    Munkalap.Cells[i + sor + 1, j + 1] = Tábla.Rows[i].ItemArray[j];
                }
            }

            long utolsó_sor = Tábla.Rows.Count;
            return utolsó_sor;
        }
    }
}

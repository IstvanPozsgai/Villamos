using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
using MyExcel = Microsoft.Office.Interop.Excel;

namespace Villamos
{
    public static partial class Module_Excel
    {

        /// <summary>
        /// Datagridviewból készít Excel táblát
        /// </summary>
        /// <param name="fájlexc">Excel tábla mentési helye</param>
        /// <param name="tábla">Átadott táblázat</param>
        /// <param name="Elsőoszlop">Az első oszlopot kell-e törölni, mert van sor fejléc</param>>
        public static void EXCELtábla(string fájlexc, DataGridView tábla, bool Elsőoszlop)
        {

            MyExcel.Range MyRange;
            Module_Excel.ExcelLétrehozás();


            // fejléc kiírása
            int oszlopíró = 1;

            for (oszlop = 0; oszlop < tábla.ColumnCount; oszlop++)
            {
                if (tábla.Columns[oszlop].Visible)
                {
                    oszlopíró += 1;
                    xlWorkSheet.Cells[1, oszlopíró] = tábla.Columns[oszlop].HeaderText;
                }
            }

            // mindet kijelöl datagrindviewben a fejléc nem másolódik
            tábla.SelectAll();
            // kitörötljük a vágólapot
            Clipboard.Clear();
            // másoljuk a kijelölt elemeket
            Clipboard.SetDataObject(tábla.GetClipboardContent());

            //Beillesztjük az értékeket
            if (Elsőoszlop)
            {// ha van jelölő akkor ideírjuk

                MyRange = xlWorkSheet.get_Range("a2");
            }
            else
            {// ha nincs sorjelölő akkor ide
                MyRange = xlWorkSheet.get_Range("b2");
            }

            MyRange.PasteSpecial(XlPasteType.xlPasteAll, XlPasteSpecialOperation.xlPasteSpecialOperationNone);

            // tábla kijelölését töröljük
            tábla.ClearSelection();

            // az első oszlop akkor kitöröljük

            oszlopíró -= 1;
            OszlopTörlés("A:A");


            //Utolsó oszlop és sor adatok
            oszlop = oszlopíró;
            sor = tábla.RowCount;

            // Kiszínezzük
            MyRange = xlWorkSheet.get_Range("A1", Oszlopnév(oszlop) + "1");
            MyRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

            // rácsozás
            Rácsoz("A1:" + Oszlopnév(oszlop) + (sor + 1).ToString());

            //Automata Oszlop szélesség beállítás
            Oszlopszélesség("Munka1", "A:" + Oszlopnév(oszlop));

            //Vastag betű
            MyExcel.Range Táblaterület = xlWorkSheet.Range["A1:" + Oszlopnév(oszlop) + "1"];
            Táblaterület.Font.Bold = true;
            Táblaterület.Interior.Color = Color.Yellow;

            //Rögzítjük a fejlécet
            xlApp.Range["A2"].Select();
            xlApp.ActiveWindow.SplitColumn = 0;
            xlApp.ActiveWindow.SplitRow = 1;
            xlApp.ActiveWindow.FreezePanes = true;

            //szűrést felteszük
            Szűrés("Munka1", 1, oszlop, 1);


            //Nyomtatási terület kijelülése
            NyomtatásiTerület_részletes("Munka1", "A1:" + Oszlopnév(oszlop) + (sor + 1).ToString(), "$1:$1", "", true);

            //Beállunk az A1 cellába
            xlApp.Range["A1"].Select();

            ExcelMentés(fájlexc);

            Module_Excel.ExcelBezárás();
        }

        /// <summary>
        /// Sor jelölő nincs akkor ==> false
        /// Színez true átmásolja a színezést is
        /// </summary>
        /// <param name="fájlexc"></param>
        /// <param name="tábla"></param>
        /// <param name="Elsőoszlop"></param>
        /// <param name="Színez"></param>
        public static void EXCELtábla(string fájlexc, DataGridView tábla, bool Elsőoszlop, bool Színez)
        {

            MyExcel.Range MyRange;
            Module_Excel.ExcelLétrehozás();


            // fejléc kiírása
            int oszlopíró = 1;

            for (oszlop = 0; oszlop < tábla.ColumnCount; oszlop++)
            {
                if (tábla.Columns[oszlop].Visible)
                {
                    oszlopíró += 1;
                    xlWorkSheet.Cells[1, oszlopíró] = tábla.Columns[oszlop].HeaderText;
                }
            }

            // mindet kijelöl datagrindviewben a fejléc nem másolódik
            tábla.SelectAll();
            // kitörötljük a vágólapot
            Clipboard.Clear();
            // másoljuk a kijelölt elemeket
            Clipboard.SetDataObject(tábla.GetClipboardContent());

            //Beillesztjük az értékeket
            if (Elsőoszlop)
            {// ha van jelölő akkor ideírjuk

                MyRange = xlWorkSheet.get_Range("a2");
            }
            else
            {// ha nincs sorjelölő akkor ide
                MyRange = xlWorkSheet.get_Range("b2");
            }

            MyRange.PasteSpecial(XlPasteType.xlPasteAll, XlPasteSpecialOperation.xlPasteSpecialOperationNone);

            // tábla kijelölését töröljük
            tábla.ClearSelection();

            // az első oszlop akkor kitöröljük

            oszlopíró -= 1;
            OszlopTörlés("A:A");


            //Utolsó oszlop és sor adatok
            oszlop = oszlopíró;
            sor = tábla.RowCount;

            // Kiszínezzük
            MyRange = xlWorkSheet.get_Range("A1", Oszlopnév(oszlop) + "1");
            MyRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

            // rácsozás
            Rácsoz("A1:" + Oszlopnév(oszlop) + (sor + 1).ToString());

            //Automata Oszlop szélesség beállítás
            Oszlopszélesség("Munka1", "A:" + Oszlopnév(oszlop));

            //Vastag betű
            MyExcel.Range Táblaterület = xlWorkSheet.Range["A1:" + Oszlopnév(oszlop) + "1"];
            Táblaterület.Font.Bold = true;
            Táblaterület.Interior.Color = Color.Yellow;

            //Rögzítjük a fejlécet
            xlApp.Range["A2"].Select();
            xlApp.ActiveWindow.SplitColumn = 0;
            xlApp.ActiveWindow.SplitRow = 1;
            xlApp.ActiveWindow.FreezePanes = true;

            //szűrést felteszük
            Szűrés("Munka1", 1, oszlop, 1);

            //Színezzük a tartalmat
            sor = 2;
            oszlop = 1;
            Color Háttér;
            for (int i = 0; i < tábla.Rows.Count; i++)
            {
                for (int j = 0; j < tábla.Columns.Count; j++)
                {
                    Háttér = tábla.Rows[i].Cells[j].Style.BackColor;
                    if (Háttér.Name == "0")
                        Háttér = Color.White;

                    if (j < tábla.Columns.Count - 2)
                        Háttérszín(Oszlopnév(oszlop + j) + (sor + i).ToString(), Háttér);

                    if (tábla.Rows[i].Cells[j].Value != null)
                    {

                        if (j == tábla.Columns.Count - 1)
                        {

                            Háttérszín("A" + (sor + i).ToString(), Color.FromArgb(50, 165, 67));
                        }
                    }
                }
            }



            //Nyomtatási terület kijelülése
            NyomtatásiTerület_részletes("Munka1", "A1:" + Oszlopnév(oszlop) + (sor + 1).ToString(), "$1:$1", "", true);

            //Beállunk az A1 cellába
            xlApp.Range["A1"].Select();

            ExcelMentés(fájlexc);

            Module_Excel.ExcelBezárás();
        }
        /// <summary>
        /// A megadott munkalapra elkészítit az átküldött adatoknak megfelelő munkalapot
        /// </summary>
        /// <param name="munkalap"></param>
        /// <param name="fájlexc"></param>
        /// <param name="tábla"></param>
        /// <param name="Elsőoszlop"></param>
        public static void EXCELtábla(string munkalap, string fájlexc, DataGridView tábla, bool Elsőoszlop)
        {
            MyExcel.Range MyRange;

            // fejléc kiírása
            int oszlopíró = 1;

            for (oszlop = 0; oszlop < tábla.ColumnCount; oszlop++)
            {
                if (tábla.Columns[oszlop].Visible)
                {
                    oszlopíró += 1;
                    xlWorkSheet.Cells[1, oszlopíró] = tábla.Columns[oszlop].HeaderText;
                }
            }

            // mindet kijelöl datagrindviewben a fejléc nem másolódik
            tábla.SelectAll();
            // kitörötljük a vágólapot
            Clipboard.Clear();
            // másoljuk a kijelölt elemeket
            Clipboard.SetDataObject(tábla.GetClipboardContent());

            //Beillesztjük az értékeket
            if (Elsőoszlop)
            {// ha van jelölő akkor ideírjuk
                MyRange = xlWorkSheet.get_Range("a2");
            }
            else
            {// ha nincs sorjelölő akkor ide
                MyRange = xlWorkSheet.get_Range("b2");
            }

            MyRange.PasteSpecial(XlPasteType.xlPasteAll, XlPasteSpecialOperation.xlPasteSpecialOperationNone);

            // tábla kijelölését töröljük
            tábla.ClearSelection();

            // az első oszlop akkor kitöröljük

            oszlopíró -= 1;
            OszlopTörlés("A:A");


            //Utolsó oszlop és sor adatok
            oszlop = oszlopíró;
            sor = tábla.RowCount;

            // Kiszínezzük
            MyRange = xlWorkSheet.get_Range("A1", Oszlopnév(oszlop) + "1");
            MyRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

            // rácsozás
            Rácsoz("A1:" + Oszlopnév(oszlop) + (sor + 1).ToString());

            //Automata Oszlop szélesség beállítás
            Oszlopszélesség(munkalap, "A:" + Oszlopnév(oszlop));

            //Vastag betű
            MyExcel.Range Táblaterület = xlWorkSheet.Range["A1:" + Oszlopnév(oszlop) + "1"];
            Táblaterület.Font.Bold = true;
            Táblaterület.Interior.Color = Color.Yellow;

            //Rögzítjük a fejlécet
            xlApp.Range["A2"].Select();
            xlApp.ActiveWindow.SplitColumn = 0;
            xlApp.ActiveWindow.SplitRow = 1;
            xlApp.ActiveWindow.FreezePanes = true;

            //szűrést felteszük
            Szűrés(munkalap, 1, oszlop, 1);


            //Nyomtatási terület kijelülése
            NyomtatásiTerület_részletes(munkalap, "A1:" + Oszlopnév(oszlop) + (sor + 1).ToString(), "$1:$1", "", true);

            //Beállunk az A1 cellába
            xlApp.Range["A1"].Select();
        }



        /// <summary>
        /// Ez a változat közvetlenül adatbázisból írja ki az adatokat
        /// Gondoskodni kell külön szálon a holtart működéséről
        /// </summary>
        /// <param name="hely"> Adatbázis elérhetősége</param>
        /// <param name="jelszó">Adatbázis jelszó</param>
        /// <param name="szöveg">Adatbázis sql</param>
        /// <param name="fájlexc">Excel mentési helye és fájlneve</param>
        public static void EXCELtábla(string hely, string jelszó, string szöveg, string fájlexc)
        {

            MyExcel.Range MyRange;
            Module_Excel.ExcelLétrehozás();

            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";

            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                OleDbDataAdapter Adapter = new OleDbDataAdapter(szöveg, Kapcsolat);

                DataSet Tábla = new DataSet();

                Adapter.Fill(Tábla);
                //Fejléc
                for (int j = 0; j < Tábla.Tables[0].Columns.Count; j++)
                {
                    xlWorkSheet.Cells[1, j + 1] = Tábla.Tables[0].Columns[j].ColumnName.ToString();
                }


                for (int i = 0; i < Tábla.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < Tábla.Tables[0].Columns.Count; j++)
                    {
                        xlWorkSheet.Cells[i + 2, j + 1] = Tábla.Tables[0].Rows[i].ItemArray[j];
                    }
                }

                //Utolsó oszlop és sor adatok
                oszlop = Tábla.Tables[0].Columns.Count;
                sor = Tábla.Tables[0].Rows.Count;
            }
            // Kiszínezzük
            MyRange = xlWorkSheet.get_Range("A1", Oszlopnév(oszlop) + "1");
            MyRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

            // rácsozás
            Rácsoz("A1:" + Oszlopnév(oszlop) + (sor + 1).ToString());

            //Automata Oszlop szélesség beállítás
            Oszlopszélesség("Munka1", "A:" + Oszlopnév(oszlop));

            //Vastag betű
            MyExcel.Range Táblaterület = xlWorkSheet.Range["A1:" + Oszlopnév(oszlop) + "1"];
            Táblaterület.Font.Bold = true;
            Táblaterület.Interior.Color = Color.Yellow;

            //Rögzítjük a fejlécet
            xlApp.Range["A2"].Select();
            xlApp.ActiveWindow.SplitColumn = 0;
            xlApp.ActiveWindow.SplitRow = 1;
            xlApp.ActiveWindow.FreezePanes = true;

            //szűrést felteszük
            Szűrés("Munka1", 1, oszlop, 1);


            //Nyomtatási terület kijelülése
            NyomtatásiTerület_részletes("Munka1", "A1:" + Oszlopnév(oszlop) + (sor + 1).ToString(), "$1:$1", "", true);

            //Beállunk az A1 cellába
            xlApp.Range["A1"].Select();

            ExcelMentés(fájlexc);

            Module_Excel.ExcelBezárás();
        }


        /// <summary>
        /// Ez a változat közvetlenül adatbázisból írja ki az adatokat
        /// Gondoskodni kell külön szálon a holtart működéséről
        /// </summary>
        /// <param name="hely"> Adatbázis elérhetősége</param>
        /// <param name="jelszó">Adatbázis jelszó</param>
        /// <param name="szöveg">Adatbázis sql</param>
        public static int EXCELtábla(string hely, string jelszó, string szöveg)
        {

            MyExcel.Range MyRange;
            Module_Excel.ExcelLétrehozás();

            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";

            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                OleDbDataAdapter Adapter = new OleDbDataAdapter(szöveg, Kapcsolat);

                DataSet Tábla = new DataSet();

                Adapter.Fill(Tábla);
                //Fejléc
                for (int j = 0; j < Tábla.Tables[0].Columns.Count; j++)
                {
                    xlWorkSheet.Cells[1, j + 1] = Tábla.Tables[0].Columns[j].ColumnName.ToString();
                }


                for (int i = 0; i < Tábla.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < Tábla.Tables[0].Columns.Count; j++)
                    {
                        xlWorkSheet.Cells[i + 2, j + 1] = Tábla.Tables[0].Rows[i].ItemArray[j];
                    }
                }

                //Utolsó oszlop és sor adatok
                oszlop = Tábla.Tables[0].Columns.Count;
                sor = Tábla.Tables[0].Rows.Count;
            }
            // Kiszínezzük
            MyRange = xlWorkSheet.get_Range("A1", Oszlopnév(oszlop) + "1");
            MyRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

            // rácsozás
            Rácsoz("A1:" + Oszlopnév(oszlop) + (sor + 1).ToString());

            //Automata Oszlop szélesség beállítás
            Oszlopszélesség("Munka1", "A:" + Oszlopnév(oszlop));

            //Vastag betű
            MyExcel.Range Táblaterület = xlWorkSheet.Range["A1:" + Oszlopnév(oszlop) + "1"];
            Táblaterület.Font.Bold = true;
            Táblaterület.Interior.Color = Color.Yellow;

            //Rögzítjük a fejlécet
            xlApp.Range["A2"].Select();
            xlApp.ActiveWindow.SplitColumn = 0;
            xlApp.ActiveWindow.SplitRow = 1;
            xlApp.ActiveWindow.FreezePanes = true;

            //szűrést felteszük
            Szűrés("Munka1", 1, oszlop, 1);


            //Nyomtatási terület kijelülése
            NyomtatásiTerület_részletes("Munka1", "A1:" + Oszlopnév(oszlop) + (sor + 1).ToString(), "$1:$1", "", true);

            //Beállunk az A1 cellába
            xlApp.Range["A1"].Select();

            return sor;
        }


        /// <summary>
        /// Adatbázist kiírja Excelbe, de nem menti el, hogy lehessen tovább folytatni.
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="szöveg"></param>
        public static void EXCEL_tábla(string hely, string jelszó, string szöveg)
        {

            MyExcel.Range MyRange;
            Module_Excel.ExcelLétrehozás();

            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";

            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                OleDbDataAdapter Adapter = new OleDbDataAdapter(szöveg, Kapcsolat);

                DataSet Tábla = new DataSet();

                Adapter.Fill(Tábla);
                //Fejléc
                for (int j = 0; j < Tábla.Tables[0].Columns.Count; j++)
                {
                    xlWorkSheet.Cells[1, j + 1] = Tábla.Tables[0].Columns[j].ColumnName.ToString();
                }


                for (int i = 0; i < Tábla.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < Tábla.Tables[0].Columns.Count; j++)
                    {
                        xlWorkSheet.Cells[i + 2, j + 1] = Tábla.Tables[0].Rows[i].ItemArray[j];
                    }
                }

                //Utolsó oszlop és sor adatok
                oszlop = Tábla.Tables[0].Columns.Count;
                sor = Tábla.Tables[0].Rows.Count;
            }
            // Kiszínezzük
            MyRange = xlWorkSheet.get_Range("A1", Oszlopnév(oszlop) + "1");
            MyRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

            // rácsozás
            Rácsoz("A1:" + Oszlopnév(oszlop) + (sor + 1).ToString());

            //Automata Oszlop szélesség beállítás
            Oszlopszélesség("Munka1", "A:" + Oszlopnév(oszlop));

            //Vastag betű
            MyExcel.Range Táblaterület = xlWorkSheet.Range["A1:" + Oszlopnév(oszlop) + "1"];
            Táblaterület.Font.Bold = true;
            Táblaterület.Interior.Color = Color.Yellow;

            //Rögzítjük a fejlécet
            xlApp.Range["A2"].Select();
            xlApp.ActiveWindow.SplitColumn = 0;
            xlApp.ActiveWindow.SplitRow = 1;
            xlApp.ActiveWindow.FreezePanes = true;

            //szűrést felteszük
            Szűrés("Munka1", 1, oszlop, 1);


            //Nyomtatási terület kijelülése
            NyomtatásiTerület_részletes("Munka1", "A1:" + Oszlopnév(oszlop) + (sor + 1).ToString(), "$1:$1", "", true);

            //Beállunk az A1 cellába
            xlApp.Range["A1"].Select();
        }

        /// <summary>
        /// Ez a változat közvetlenül Adattáblából írja ki az adatokat
        /// </summary>
        /// <param name="hely"> Adatbázis elérhetősége</param>
        /// <param name="jelszó">Adatbázis jelszó</param>
        /// <param name="szöveg">Adatbázis sql</param>
        public static void EXCELtábla(DataTable Tábla, string fájlexc, List<string> Elemek = null)
        {
            MyExcel.Range MyRange;
            Module_Excel.ExcelLétrehozás();

            //Fejléc
            int oszlop = 1;
            for (int j = 0; j < Tábla.Columns.Count; j++)
            {
                if (Elemek != null && Elemek.Count > 0 && Elemek.Contains(Tábla.Columns[j].ColumnName))
                {
                    xlWorkSheet.Cells[1, oszlop] = Tábla.Columns[j].ColumnName.ToString();
                    oszlop++;
                }

                if (Elemek == null)
                {
                    xlWorkSheet.Cells[1, oszlop] = Tábla.Columns[j].ColumnName.ToString();
                    oszlop++;
                }

            }


            for (int i = 0; i < Tábla.Rows.Count; i++)
            {
                oszlop = 1;
                for (int j = 0; j < Tábla.Columns.Count; j++)
                {
                    if (Elemek != null && Elemek.Count > 0 && Elemek.Contains(Tábla.Columns[j].ColumnName))
                    {
                        xlWorkSheet.Cells[i + 2, oszlop] = Tábla.Rows[i].ItemArray[j];
                        oszlop++;
                    }
                    if (Elemek == null)
                    {
                        xlWorkSheet.Cells[i + 2, oszlop] = Tábla.Rows[i].ItemArray[j];
                        oszlop++;
                    }
                }
            }

            //Utolsó oszlop és sor adatok
            oszlop--;
            sor = Tábla.Rows.Count;

            // Kiszínezzük
            MyRange = xlWorkSheet.get_Range("A1", Oszlopnév(oszlop) + "1");
            MyRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);

            // rácsozás
            Rácsoz("A1:" + Oszlopnév(oszlop) + (sor + 1).ToString());

            //Automata Oszlop szélesség beállítás
            Oszlopszélesség("Munka1", "A:" + Oszlopnév(oszlop));

            //Vastag betű
            MyExcel.Range Táblaterület = xlWorkSheet.Range["A1:" + Oszlopnév(oszlop) + "1"];
            Táblaterület.Font.Bold = true;
            Táblaterület.Interior.Color = Color.Yellow;

            //Rögzítjük a fejlécet
            xlApp.Range["A2"].Select();
            xlApp.ActiveWindow.SplitColumn = 0;
            xlApp.ActiveWindow.SplitRow = 1;
            xlApp.ActiveWindow.FreezePanes = true;

            //szűrést felteszük
            Szűrés("Munka1", 1, oszlop, 1);


            //Nyomtatási terület kijelülése
            NyomtatásiTerület_részletes("Munka1", $"A1:{Oszlopnév(oszlop)}{sor + 1}", "$1:$1", "", true);

            //Beállunk az A1 cellába
            xlApp.Range["A1"].Select();

            ExcelMentés(fájlexc);

            Module_Excel.ExcelBezárás();
        }
    }
}

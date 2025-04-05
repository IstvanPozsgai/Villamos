using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using static System.IO.File;
using MyExcel = Microsoft.Office.Interop.Excel;



namespace Villamos
{

    public static class Module_Excel
    {
        public static int sor;
        public static int oszlop;
        public static MyExcel.Application xlApp;
        public static MyExcel.Workbook xlWorkBook;
        public static MyExcel.Worksheet xlWorkSheet;
        public static MyExcel._Workbook _xlWorkBook;
        public static MyExcel._Worksheet _xlWorkSheet;

        public static object misValue = System.Reflection.Missing.Value;


        /// <summary>
        /// Elindítjuk az Excel készítést egy üres munkafüzettel
        /// </summary>
        public static void ExcelLétrehozás(bool teszt = false)
        {
            //elindítjuk az alkalmazást. létrehozzuk a fájlt és a munkalapot.
            xlApp = new MyExcel.Application
            {
                Visible = teszt
            };
            Module_Excel.xlWorkBook = xlApp.Workbooks.Add(misValue);
            Module_Excel.xlWorkSheet = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets.get_Item(1);

        }



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
        public static void EXCELtábla(System.Data.DataTable Tábla, string fájlexc)
        {

            MyExcel.Range MyRange;
            Module_Excel.ExcelLétrehozás();

            //Fejléc
            for (int j = 0; j < Tábla.Columns.Count; j++)
            {
                xlWorkSheet.Cells[1, j + 1] = Tábla.Columns[j].ColumnName.ToString();
            }


            for (int i = 0; i < Tábla.Rows.Count; i++)
            {
                for (int j = 0; j < Tábla.Columns.Count; j++)
                {
                    xlWorkSheet.Cells[i + 2, j + 1] = Tábla.Rows[i].ItemArray[j];
                }
            }

            //Utolsó oszlop és sor adatok
            oszlop = Tábla.Columns.Count;
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



        /// <summary>
        /// 
        /// </summary>
        /// <param name="mit">terület</param>
        /// <param name="sor">sor</param>
        public static void Tábla_Rögzítés(string mit, int sor)
        {
            MyExcel.Range Táblaterület = xlWorkSheet.Range[mit];
            xlApp.ActiveWindow.SplitColumn = 0;
            xlApp.ActiveWindow.SplitRow = sor;
            xlApp.ActiveWindow.FreezePanes = true;
        }


        public static int Tábla_Író(string hely, string jelszó, string szöveg, int sor, string munkalap)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            Munkalap.Select();

            DataGridView dataGridView1 = new DataGridView();
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
                    Munkalap.Cells[sor, j + 1] = Tábla.Tables[0].Columns[j].ColumnName.ToString();
                }


                for (int i = 0; i < Tábla.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < Tábla.Tables[0].Columns.Count; j++)
                    {
                        Munkalap.Cells[i + sor + 1, j + 1] = Tábla.Tables[0].Rows[i].ItemArray[j];
                    }
                }

                int utolsó_sor = Tábla.Tables[0].Rows.Count;
                return utolsó_sor;
            }

        }


        /// <summary>
        /// Bezárja az excel táblát memória ürítéssel
        /// </summary>
        /// <param name="obj"></param>
        private static void ReleaseObject(object obj)
        {
            // becsukjuk az excelt.
            if (obj != null)
            {
                try
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                    obj = null;
                }
                catch (Exception)
                {
                    obj = null;
                }
                finally
                {
                    GC.Collect();
                }
            }
        }


        /// <summary>
        /// Excel táblát elmentjük a megadott néven
        /// </summary>
        /// <param name="wb"></param>
        /// <param name="fájlnév"></param>
        public static void ExcelMentés(string fájlnév)
        {
            xlApp.DisplayAlerts = false;
            xlWorkBook.SaveAs(fájlnév, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue,
                   Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
        }

        /// <summary>
        /// Excel mentés ugyan azon a néven.
        /// </summary>
        public static void ExcelMentés()
        {
            xlApp.DisplayAlerts = false;
            xlWorkBook.Save();

        }

        public static void ExcelBezárás()
        {
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            ReleaseObject(xlApp);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlWorkSheet);
        }



        /// <summary>
        /// Kijelölt területet rácsoz
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="Kijelöltterület">szöveg kijelölt terület A1:V4 formában</param>
        public static void Rácsoz(string Kijelöltterület)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[Kijelöltterület];
            Táblaterület.Borders.LineStyle = XlLineStyle.xlContinuous;
            Táblaterület.Borders.Weight = MyExcel.XlBorderWeight.xlThin;
            Táblaterület.BorderAround(MyExcel.XlLineStyle.xlContinuous,
                                        MyExcel.XlBorderWeight.xlMedium,
                                        MyExcel.XlColorIndex.xlColorIndexAutomatic,
                                        MyExcel.XlColorIndex.xlColorIndexAutomatic);
        }


        /// <summary>
        /// Megadott oszlop szélesség beállítása az oszlopnál
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="oszlop">string oszlopnév</param>
        /// <param name="szélesség">double szélesség</param>
        public static void Oszlopszélesség(string munkalap, string oszlop, int szélesség)
        {
            //Oszlop szélesség beállítás
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];

            MyExcel.Range Táblaterület = Munkalap.Range[oszlop];
            Táblaterület.Columns.Select();
            Táblaterület.Columns.ColumnWidth = szélesség;

        }

        /// <summary>
        /// Ebben a változatban automatikus az oszlop szélesség
        /// </summary>
        /// <param name="munkalap"></param>
        /// <param name="oszlop"></param>
        public static void Oszlopszélesség(string munkalap, string oszlop)
        {
            //Oszlop szélesség beállítás
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range Táblaterület = Munkalap.Range[oszlop];
            Táblaterület.Columns.EntireColumn.AutoFit();
        }


        /// <summary>
        /// Törli az oszlopot
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="oszlop">formában kell megadni "A:A" </param>
        public static void OszlopTörlés(string oszlop)
        {
            MyExcel.Range Táblaterület = xlWorkSheet.Range[oszlop];
            Táblaterület.Delete(Microsoft.Office.Interop.Excel.XlDirection.xlToLeft);
        }

        /// <summary>
        /// Elrejti az oszlopot
        /// </summary>
        /// <param name="oszlop"></param>
        public static void OszlopRejtés(string munkalap, string oszlop)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range Táblaterület = Munkalap.Range[oszlop];
            Táblaterület.EntireColumn.Hidden = true;

        }


        /// <summary>
        /// Kiírja a szöveget a megfelelő cellába
        /// </summary>
        /// <param name="mit">szöveg</param>
        /// <param name="hova">szöveg</param>
        public static void Kiir(string mit, string hova)
        {

            MyExcel.Range Cella = Module_Excel.xlApp.Application.Range[hova];
            Cella.Value = mit;
        }

        /// <summary>
        ///  A szöveg helyzetét lehet meghatározni a cellában bal és jobb a kötött név minden egyéb középre kerül.
        /// </summary>
        /// <param name="mit"></param>
        /// <param name="irány">bal/jobb/közép</param>
        public static void Igazít_vízszintes(string mit, string irány)
        {

            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            switch (irány)
            {
                case "bal":
                    Táblaterület.HorizontalAlignment = Constants.xlLeft;
                    break;

                case "jobb":
                    Táblaterület.HorizontalAlignment = Constants.xlRight;
                    break;
                default:
                    Táblaterület.HorizontalAlignment = Constants.xlCenter;
                    break;
            }
        }
        /// <summary>
        /// szöveg függőleges helyzetét lehet megadni
        /// </summary>
        /// <param name="mit"></param>
        /// <param name="irány">felső/alsó/közép</param>

        public static void Igazít_függőleges(string mit, string irány)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            switch (irány)
            {
                case "felső":
                    Táblaterület.VerticalAlignment = Constants.xlTop;
                    break;

                case "alsó":
                    Táblaterület.VerticalAlignment = Constants.xlBottom;
                    break;
                default:
                    Táblaterület.VerticalAlignment = Constants.xlCenter;
                    break;
            }
        }

        /// <summary>
        /// Vastagkeretet készít a kijelölt területre
        /// </summary>
        /// <param name="mit">szöveg</param>
        public static void Vastagkeret(string mit)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.BorderAround(MyExcel.XlLineStyle.xlContinuous,
                     MyExcel.XlBorderWeight.xlMedium,
                     MyExcel.XlColorIndex.xlColorIndexAutomatic,
                     MyExcel.XlColorIndex.xlColorIndexAutomatic);
        }




        /// <summary>
        /// Vékonykeretet készít a kijelölt területre
        /// </summary>
        /// <param name="mit">azöveg</param>
        public static void Vékonykeret(string mit)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.BorderAround(MyExcel.XlLineStyle.xlContinuous,
                     MyExcel.XlBorderWeight.xlThin,
                     MyExcel.XlColorIndex.xlColorIndexAutomatic,
                     MyExcel.XlColorIndex.xlColorIndexAutomatic);
        }

        public static void VékonyFelső(string mit)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];

            Táblaterület.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = Constants.xlNone;
            Táblaterület.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = Constants.xlNone;
            Táblaterület.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = Constants.xlNone;
            Táblaterület.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
            Táblaterület.Borders[XlBordersIndex.xlEdgeTop].ColorIndex = 0;
            Táblaterület.Borders[XlBordersIndex.xlEdgeTop].TintAndShade = 0;
            Táblaterület.Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlThin;
        }

        public static void VastagFelső(string mit)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];

            Táblaterület.Borders[XlBordersIndex.xlDiagonalDown].LineStyle = Constants.xlNone;
            Táblaterület.Borders[XlBordersIndex.xlDiagonalUp].LineStyle = Constants.xlNone;
            Táblaterület.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = Constants.xlNone;
            Táblaterület.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
            Táblaterület.Borders[XlBordersIndex.xlEdgeTop].ColorIndex = 0;
            Táblaterület.Borders[XlBordersIndex.xlEdgeTop].TintAndShade = 0;
            Táblaterület.Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlMedium;
        }

        /// <summary>
        /// Egyesíti a kiválasztott területet
        /// </summary>
        /// <param name="mit">szöveg</param>
        public static void Egyesít(string munkalap, string mit)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];

            MyExcel.Range Táblaterület = Munkalap.Range[mit];
            Táblaterület.Select();
            Táblaterület.HorizontalAlignment = Constants.xlCenter;
            Táblaterület.VerticalAlignment = Constants.xlCenter;
            Táblaterület.WrapText = false;
            Táblaterület.Orientation = 0;
            Táblaterület.AddIndent = false;
            Táblaterület.IndentLevel = 0;
            Táblaterület.ShrinkToFit = false;
            Táblaterület.MergeCells = false;
            Táblaterület.Merge();
        }


        /// <summary>
        /// Betűméretet lehet beállítani
        /// </summary>
        /// <param name="mit">szöveg</param>
        /// <param name="méret">egész</param>
        public static void Betű(string mit, int méret)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Font.Size = méret;
            Táblaterület.Font.Strikethrough = false;
            Táblaterület.Font.Superscript = false;
            Táblaterület.Font.Subscript = false;
            Táblaterület.Font.OutlineFont = false;
            Táblaterület.Font.Shadow = false;
            Táblaterület.Font.Underline = Microsoft.Office.Interop.Excel.XlUnderlineStyle.xlUnderlineStyleNone;
        }

        public static void Betű(string mit, Color színe)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Font.Color = színe;
            //     Táblaterület.Font.ThemeColor = színe;
            //Táblaterület.Font.TintAndShade = 0;
        }


        /// <summary>
        /// Betű formátumát lehet állítani
        /// </summary>
        /// <param name="mit"></param>
        /// <param name="aláhúzott"></param>
        /// <param name="dőlt"></param>
        /// <param name="vastag"></param>
        public static void Betű(string mit, bool aláhúzott, bool dőlt, bool vastag)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Font.Underline = aláhúzott;
            Táblaterület.Font.Italic = dőlt;
            Táblaterület.Font.Bold = vastag;
        }

        /// <summary>
        /// A Betű stílusából egyet lehet kiválasztani, annak a formátum maszkját kell elküldeni.
        /// üres string ha nem akarjuk kihasználni.
        /// </summary>
        /// <param name="mit"></param>
        /// <param name="stílus"></param>
        /// <param name="formátum"></param>
        public static void Betű(string mit, string stílus = "", string formátum = "")
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            if (stílus.Trim() != "")
                Táblaterület.Style = stílus;
            if (formátum.Trim() != "")
                Táblaterület.NumberFormat = formátum;
        }

        /// <summary>
        /// Cellán belüli szöveg formázásokat láncba kell megadni, a lánc minden elemére vonatkozóan
        /// </summary>
        /// <param name="mit"></param>
        /// <param name="aláhúzott"></param>
        /// <param name="dőlt"></param>
        /// <param name="vastag"></param>
        /// <param name="kezdet"></param>
        /// <param name="hossz"></param>
        public static void Cella_Betű(string mit, bool aláhúzott, bool dőlt, bool vastag, int kezdet, int hossz)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Characters[kezdet, hossz].Font.Underline = aláhúzott;
            Táblaterület.Characters[kezdet, hossz].Font.Italic = dőlt;
            Táblaterület.Characters[kezdet, hossz].Font.Bold = vastag;
        }

        public static void Cella_Betű(string mit, bool aláhúzott, bool dőlt, bool vastag, int kezdet, int hossz, Color szín, string betű = "Arial", int méret = 12)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Characters[kezdet, hossz].Font.Underline = aláhúzott;
            Táblaterület.Characters[kezdet, hossz].Font.Italic = dőlt;
            Táblaterület.Characters[kezdet, hossz].Font.Bold = vastag;
            Táblaterület.Characters[kezdet, hossz].Font.Color = szín;
            Táblaterület.Characters[kezdet, hossz].Font.FontStyle = betű;
            Táblaterület.Characters[kezdet, hossz].Font.Size = méret;
        }

        /// <summary>
        /// Háttérszín beállítása
        /// </summary>
        /// <param name="mit">szöveg</param>
        /// <param name="szín">dupla</param>
        public static void Háttérszín(string mit, double szín)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Interior.Color = szín;
        }

        public static void Háttérszín(string mit, Color színe)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Interior.Color = System.Drawing.ColorTranslator.ToOle(színe);

        }

        public static void CellaNincsHáttér(string mit)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Interior.Pattern = Constants.xlNone;
            Táblaterület.Interior.TintAndShade = 0;
            Táblaterület.Interior.PatternTintAndShade = 0;

        }
        /// <summary>
        /// Sötét háttérhez világos betűt állít be
        /// </summary>
        /// <param name="mit">szöveg</param>
        /// <param name="szín">dupla</param>
        public static void Háttérszíninverz(string mit, double szín)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Interior.Color = szín;

            Táblaterület.Font.ThemeColor = Microsoft.Office.Interop.Excel.XlThemeColor.xlThemeColorDark1;
            Táblaterület.Font.TintAndShade = 0;
        }

        public static void Háttérszíninverz(string mit, Color színe)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Interior.Color = System.Drawing.ColorTranslator.ToOle(színe);

            Táblaterület.Font.ThemeColor = Microsoft.Office.Interop.Excel.XlThemeColor.xlThemeColorDark1;
            Táblaterület.Font.TintAndShade = 0;
        }

        /// <summary>
        /// Nyomtatási területet 
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="terület"></param>
        /// <param name="sorismétlődés"> "" vagy "$1:$1"</param>
        /// /// <param name="oszlopisnétlődés">"" vagy "$A:$B"</param>
        /// <param name="álló">Álló esetén true, fekvó esetén false</param>
        public static void NyomtatásiTerület_részletes(string munkalap, string terület, string sorismétlődés, string oszlopisnétlődés, bool álló)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            Munkalap.Select();

            PageSetup Táblaterület = (MyExcel.PageSetup)Munkalap.PageSetup;

            Táblaterület.PrintTitleRows = sorismétlődés;
            Táblaterület.PrintTitleColumns = oszlopisnétlődés;

            Táblaterület.PrintArea = terület;
            {
                Táblaterület.LeftHeader = "";
                Táblaterület.CenterHeader = Program.PostásNév.Trim();
                Táblaterület.RightHeader = DateTime.Now.ToString("yyyy.MM.dd hh:mm");
                Táblaterület.LeftFooter = "";
                Táblaterület.CenterFooter = @"&P/&N";
                Táblaterület.RightFooter = "";
                Táblaterület.LeftMargin = xlApp.InchesToPoints(0.590551181102362d);
                Táblaterület.RightMargin = xlApp.InchesToPoints(0.590551181102362d);
                Táblaterület.TopMargin = xlApp.InchesToPoints(0.78740157480315d);
                Táblaterület.BottomMargin = xlApp.InchesToPoints(0.78740157480315d);
                Táblaterület.HeaderMargin = xlApp.InchesToPoints(0.511811023622047d);
                Táblaterület.FooterMargin = xlApp.InchesToPoints(0.511811023622047d);
                Táblaterület.PrintHeadings = false;
                Táblaterület.PrintGridlines = false;
                Táblaterület.PrintComments = Microsoft.Office.Interop.Excel.XlPrintLocation.xlPrintNoComments;
                Táblaterület.CenterHorizontally = false;
                Táblaterület.CenterVertically = false;
                if (álló)
                    Táblaterület.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
                else
                    Táblaterület.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

                Táblaterület.Draft = false;
                Táblaterület.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
                Táblaterület.Order = Microsoft.Office.Interop.Excel.XlOrder.xlDownThenOver;
                Táblaterület.BlackAndWhite = false;
                Táblaterület.Zoom = false;
                Táblaterület.FitToPagesWide = 1;
                Táblaterület.FitToPagesTall = false;
                Táblaterület.PrintErrors = Microsoft.Office.Interop.Excel.XlPrintErrors.xlPrintErrorsDisplayed;
            }
        }

        public static void NyomtatásiTerület_részletes(string munkalap, string terület, string sorismétlődés, string oszlopisnétlődés,
            string fejbal, string fejközép, string fejjobb, string fénykép)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            Munkalap.Select();

            PageSetup Táblaterület = (MyExcel.PageSetup)Munkalap.PageSetup;

            Táblaterület.PrintTitleRows = sorismétlődés;
            Táblaterület.PrintTitleColumns = oszlopisnétlődés;

            Táblaterület.LeftHeaderPicture.Filename = fénykép;

            Táblaterület.PrintArea = terület;
            {
                Táblaterület.LeftHeader = fejbal;
                Táblaterület.CenterHeader = fejközép;
                Táblaterület.RightHeader = fejjobb;
                Táblaterület.LeftFooter = "";
                Táblaterület.CenterFooter = @"&P/&N";
                Táblaterület.RightFooter = "";
                Táblaterület.LeftMargin = xlApp.InchesToPoints(0.590551181102362d);
                Táblaterület.RightMargin = xlApp.InchesToPoints(0.590551181102362d);
                Táblaterület.TopMargin = xlApp.InchesToPoints(0.78740157480315d);
                Táblaterület.BottomMargin = xlApp.InchesToPoints(0.78740157480315d);
                Táblaterület.HeaderMargin = xlApp.InchesToPoints(0.511811023622047d);
                Táblaterület.FooterMargin = xlApp.InchesToPoints(0.511811023622047d);
                Táblaterület.PrintHeadings = false;
                Táblaterület.PrintGridlines = false;
                Táblaterület.PrintComments = Microsoft.Office.Interop.Excel.XlPrintLocation.xlPrintNoComments;
                Táblaterület.CenterHorizontally = false;
                Táblaterület.CenterVertically = false;
                Táblaterület.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
                Táblaterület.Draft = false;
                Táblaterület.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
                Táblaterület.Order = Microsoft.Office.Interop.Excel.XlOrder.xlDownThenOver;
                Táblaterület.BlackAndWhite = false;
                Táblaterület.Zoom = false;
                Táblaterület.FitToPagesWide = 1;
                Táblaterület.FitToPagesTall = false;
                Táblaterület.PrintErrors = Microsoft.Office.Interop.Excel.XlPrintErrors.xlPrintErrorsDisplayed;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="munkalap"></param>
        /// <param name="terület"></param>
        /// <param name="sorismétlődés"></param>
        /// <param name="oszlopisnétlődés"></param>
        /// <param name="fejbal"></param>
        /// <param name="fejközép"></param>
        /// <param name="fejjobb"></param>
        /// <param name="lábbal"></param>
        /// <param name="lábközép"></param>
        /// <param name="lábjobb"></param>
        /// <param name="fénykép"></param>
        /// <param name="balMargó"></param>
        /// <param name="jobbMargó"></param>
        /// <param name="alsóMargó"></param>
        /// <param name="felsőMargó"></param>
        /// <param name="fejlécMéret"></param>
        /// <param name="LáblécMéret"></param>
        /// <param name="vízszintes"></param>
        /// <param name="függőleges"></param>
        public static void NyomtatásiTerület_részletes(string munkalap, string terület, string sorismétlődés, string oszlopisnétlődés,
                        string fejbal, string fejközép, string fejjobb, string lábbal, string lábközép, string lábjobb, string fénykép, double balMargó, double jobbMargó,
                        double alsóMargó, double felsőMargó, double fejlécMéret, double LáblécMéret, bool vízszintes, bool függőleges)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            Munkalap.Select();

            PageSetup Táblaterület = (MyExcel.PageSetup)Munkalap.PageSetup;

            Táblaterület.PrintTitleRows = sorismétlődés;
            Táblaterület.PrintTitleColumns = oszlopisnétlődés;

            Táblaterület.LeftHeaderPicture.Filename = fénykép;

            Táblaterület.PrintArea = terület;
            {
                Táblaterület.LeftHeader = fejbal;
                Táblaterület.CenterHeader = fejközép;
                Táblaterület.RightHeader = fejjobb;
                Táblaterület.LeftFooter = lábbal;
                Táblaterület.CenterFooter = lábközép;
                Táblaterület.RightFooter = lábjobb;
                Táblaterület.LeftMargin = xlApp.InchesToPoints(balMargó);
                Táblaterület.RightMargin = xlApp.InchesToPoints(jobbMargó);
                Táblaterület.TopMargin = xlApp.InchesToPoints(felsőMargó);
                Táblaterület.BottomMargin = xlApp.InchesToPoints(alsóMargó);
                Táblaterület.HeaderMargin = xlApp.InchesToPoints(fejlécMéret);
                Táblaterület.FooterMargin = xlApp.InchesToPoints(LáblécMéret);
                Táblaterület.PrintHeadings = false;
                Táblaterület.PrintGridlines = false;
                Táblaterület.PrintComments = Microsoft.Office.Interop.Excel.XlPrintLocation.xlPrintNoComments;
                Táblaterület.CenterHorizontally = vízszintes;
                Táblaterület.CenterVertically = függőleges;
                Táblaterület.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
                Táblaterület.Draft = false;
                Táblaterület.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
                Táblaterület.Order = Microsoft.Office.Interop.Excel.XlOrder.xlDownThenOver;
                Táblaterület.BlackAndWhite = false;
                Táblaterület.Zoom = false;
                Táblaterület.FitToPagesWide = 1;
                Táblaterület.FitToPagesTall = false;
                Táblaterület.PrintErrors = Microsoft.Office.Interop.Excel.XlPrintErrors.xlPrintErrorsDisplayed;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="munkalap">Munkalap neve</param>
        /// <param name="terület"> "" vagy "$1:$1"</param>
        /// <param name="balMargó">LeftMargin 0.708661417322835d formátumban</param>
        /// <param name="jobbMargó">RightMargin 0.708661417322835d formátumban</param>
        /// <param name="alsóMargó">BottomMargin 0.708661417322835d formátumban</param>
        /// <param name="felsőMargó">TopMargin 0.708661417322835d formátumban</param>
        /// <param name="fejlécMéret">HeaderMargin 0.708661417322835d formátumban</param>
        /// <param name="LáblécMéret">FooterMargin 0.708661417322835d formátumban</param>
        /// <param name="oldalszéles">"szám" vagy false</param>
        /// <param name="oldalmagas">"szám" vagy false</param>
        public static void NyomtatásiTerület_részletes(string munkalap, string terület,
            double balMargó = 0.708661417322835d,
            double jobbMargó = 0.708661417322835d,
            double alsóMargó = 0.590551181102362d,
            double felsőMargó = 0.748031496062992d,
            double fejlécMéret = 0.31496062992126d,
            double LáblécMéret = 0.31496062992126d,
            string oldalszéles = "1",
            string oldalmagas = "1")
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            Munkalap.Select();

            PageSetup Táblaterület = (MyExcel.PageSetup)Munkalap.PageSetup;

            Táblaterület.PrintArea = terület;
            {
                Táblaterület.LeftMargin = xlApp.InchesToPoints(balMargó);
                Táblaterület.RightMargin = xlApp.InchesToPoints(jobbMargó);
                Táblaterület.TopMargin = xlApp.InchesToPoints(felsőMargó);
                Táblaterület.BottomMargin = xlApp.InchesToPoints(alsóMargó);
                Táblaterület.HeaderMargin = xlApp.InchesToPoints(fejlécMéret);
                Táblaterület.FooterMargin = xlApp.InchesToPoints(LáblécMéret);
                Táblaterület.PrintHeadings = false;
                Táblaterület.PrintGridlines = false;
                Táblaterület.PrintComments = Microsoft.Office.Interop.Excel.XlPrintLocation.xlPrintNoComments;
                Táblaterület.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
                Táblaterület.Draft = false;
                Táblaterület.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
                Táblaterület.Order = Microsoft.Office.Interop.Excel.XlOrder.xlDownThenOver;
                Táblaterület.BlackAndWhite = false;
                Táblaterület.Zoom = false;
                if (int.TryParse(oldalszéles, out int széles))
                    Táblaterület.FitToPagesWide = széles;
                else
                    Táblaterület.FitToPagesWide = false;
                if (int.TryParse(oldalmagas, out int magas))
                    Táblaterület.FitToPagesTall = magas;
                else
                    Táblaterület.FitToPagesTall = false;

                Táblaterület.PrintErrors = Microsoft.Office.Interop.Excel.XlPrintErrors.xlPrintErrorsDisplayed;
            }
        }

        public static void NyomtatásiTerület_részletes(string munkalap, string terület, string sorismétlődés, string oszlopisnétlődés,
                    string fejbal, string fejközép, string fejjobb, string lábbal, string lábközép, string lábjobb, string fénykép, double balMargó, double jobbMargó,
                    double alsóMargó, double felsőMargó, double fejlécMéret, double LáblécMéret, bool vízszintes, bool függőleges, string Elrendezés)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            Munkalap.Select();

            PageSetup Táblaterület = (MyExcel.PageSetup)Munkalap.PageSetup;

            Táblaterület.PrintTitleRows = sorismétlődés;
            Táblaterület.PrintTitleColumns = oszlopisnétlődés;

            Táblaterület.LeftHeaderPicture.Filename = fénykép;

            Táblaterület.PrintArea = terület;
            {
                Táblaterület.LeftHeader = fejbal;
                Táblaterület.CenterHeader = fejközép;
                Táblaterület.RightHeader = fejjobb;
                Táblaterület.LeftFooter = lábbal;
                Táblaterület.CenterFooter = lábközép;
                Táblaterület.RightFooter = lábjobb;
                Táblaterület.LeftMargin = xlApp.InchesToPoints(balMargó);
                Táblaterület.RightMargin = xlApp.InchesToPoints(jobbMargó);
                Táblaterület.TopMargin = xlApp.InchesToPoints(felsőMargó);
                Táblaterület.BottomMargin = xlApp.InchesToPoints(alsóMargó);
                Táblaterület.HeaderMargin = xlApp.InchesToPoints(fejlécMéret);
                Táblaterület.FooterMargin = xlApp.InchesToPoints(LáblécMéret);
                Táblaterület.PrintHeadings = false;
                Táblaterület.PrintGridlines = false;
                Táblaterület.PrintComments = Microsoft.Office.Interop.Excel.XlPrintLocation.xlPrintNoComments;
                Táblaterület.CenterHorizontally = vízszintes;
                Táblaterület.CenterVertically = függőleges;
                if (Elrendezés != "Álló")
                    Táblaterület.Orientation = XlPageOrientation.xlLandscape;
                else
                    Táblaterület.Orientation = XlPageOrientation.xlPortrait;
                Táblaterület.Draft = false;
                Táblaterület.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
                Táblaterület.Order = Microsoft.Office.Interop.Excel.XlOrder.xlDownThenOver;
                Táblaterület.BlackAndWhite = false;
                Táblaterület.Zoom = false;
                Táblaterület.FitToPagesWide = 1;
                Táblaterület.FitToPagesTall = false;
                Táblaterület.PrintErrors = Microsoft.Office.Interop.Excel.XlPrintErrors.xlPrintErrorsDisplayed;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="munkalap   ">munkalap   </param>
        /// <param name="terület    ">terület    </param>
        /// <param name="balMargó   ">balMargó   </param>
        /// <param name="jobbMargó  ">jobbMargó  </param>
        /// <param name="alsóMargó  ">alsóMargó  </param>
        /// <param name="felsőMargó ">felsőMargó </param>
        /// <param name="fejlécMéret">fejlécMéret</param>
        /// <param name="LáblécMéret">LáblécMéret</param>
        /// <param name="oldalszéles">oldalszéles</param>
        /// <param name="oldalmagas ">oldalmagas </param>
        /// <param name="álló       ">álló       </param>
        /// <param name="papírméret ">papírméret </param>
        /// <param name="víz_közép  ">víz_közép  </param>
        /// <param name="Függ_közép ">Függ_közép </param>
        public static void NyomtatásiTerület_részletes(string munkalap, string terület,
            double balMargó, double jobbMargó,
            double alsóMargó, double felsőMargó,
            double fejlécMéret, double LáblécMéret,
            string oldalszéles = "1", string oldalmagas = "1",
            bool álló = true, string papírméret = "A4",
            bool víz_közép = true, bool Függ_közép = true)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            Munkalap.Select();

            PageSetup Táblaterület = (MyExcel.PageSetup)Munkalap.PageSetup;

            Táblaterület.PrintArea = terület;
            {
                Táblaterület.LeftMargin = xlApp.InchesToPoints(balMargó);
                Táblaterület.RightMargin = xlApp.InchesToPoints(jobbMargó);
                Táblaterület.TopMargin = xlApp.InchesToPoints(felsőMargó);
                Táblaterület.BottomMargin = xlApp.InchesToPoints(alsóMargó);
                Táblaterület.HeaderMargin = xlApp.InchesToPoints(fejlécMéret);
                Táblaterület.FooterMargin = xlApp.InchesToPoints(LáblécMéret);
                Táblaterület.PrintHeadings = false;
                Táblaterület.PrintGridlines = false;

                Táblaterület.PrintComments = Microsoft.Office.Interop.Excel.XlPrintLocation.xlPrintNoComments;
                if (álló)
                    Táblaterület.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
                else
                    Táblaterület.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

                Táblaterület.Draft = false;

                Táblaterület.CenterHorizontally = víz_közép;
                Táblaterület.CenterVertically = Függ_közép;

                if (papírméret == "A4")
                    Táblaterület.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
                else
                    Táblaterület.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA3;

                Táblaterület.Order = Microsoft.Office.Interop.Excel.XlOrder.xlDownThenOver;
                Táblaterület.BlackAndWhite = false;
                Táblaterület.Zoom = false;
                if (int.TryParse(oldalszéles, out int széles))
                    Táblaterület.FitToPagesWide = széles;
                else
                    Táblaterület.FitToPagesWide = false;
                if (int.TryParse(oldalmagas, out int magas))
                    Táblaterület.FitToPagesTall = magas;
                else
                    Táblaterület.FitToPagesTall = false;

                Táblaterület.PrintErrors = Microsoft.Office.Interop.Excel.XlPrintErrors.xlPrintErrorsDisplayed;
            }
        }

        /// <summary>
        /// Teljes változat
        /// </summary>
        /// <param name="munkalap"></param>
        /// <param name="terület"></param>
        /// <param name="sorismétlődés"></param>
        /// <param name="oszlopisnétlődés"></param>
        /// <param name="fejbal"></param>
        /// <param name="fejközép"></param>
        /// <param name="fejjobb"></param>
        /// <param name="lábbal"></param>
        /// <param name="lábközép"></param>
        /// <param name="lábjobb"></param>
        /// <param name="fénykép"></param>
        /// <param name="balMargó"></param>
        /// <param name="jobbMargó"></param>
        /// <param name="alsóMargó"></param>
        /// <param name="felsőMargó"></param>
        /// <param name="fejlécMéret"></param>
        /// <param name="LáblécMéret"></param>
        /// <param name="vízszintes"></param>
        /// <param name="függőleges"></param>
        /// <param name="oldalszéles"></param>
        /// <param name="oldalmagas"></param>
        public static void NyomtatásiTerület_részletes(string munkalap, string terület, string sorismétlődés, string oszlopisnétlődés,
                string fejbal, string fejközép, string fejjobb, string lábbal, string lábközép, string lábjobb, string fénykép, double balMargó, double jobbMargó,
                double alsóMargó, double felsőMargó, double fejlécMéret, double LáblécMéret, bool vízszintes, bool függőleges, string oldalszéles, string oldalmagas,
                bool álló = true, string papírméret = "A4")
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            Munkalap.Select();

            PageSetup Táblaterület = (MyExcel.PageSetup)Munkalap.PageSetup;

            Táblaterület.PrintTitleRows = sorismétlődés;
            Táblaterület.PrintTitleColumns = oszlopisnétlődés;

            Táblaterület.LeftHeaderPicture.Filename = fénykép;

            Táblaterület.PrintArea = terület;
            {
                Táblaterület.LeftHeader = fejbal;
                Táblaterület.CenterHeader = fejközép;
                Táblaterület.RightHeader = fejjobb;
                Táblaterület.LeftFooter = lábbal;
                Táblaterület.CenterFooter = lábközép;
                Táblaterület.RightFooter = lábjobb;
                Táblaterület.LeftMargin = xlApp.InchesToPoints(balMargó);
                Táblaterület.RightMargin = xlApp.InchesToPoints(jobbMargó);
                Táblaterület.TopMargin = xlApp.InchesToPoints(felsőMargó);
                Táblaterület.BottomMargin = xlApp.InchesToPoints(alsóMargó);
                Táblaterület.HeaderMargin = xlApp.InchesToPoints(fejlécMéret);
                Táblaterület.FooterMargin = xlApp.InchesToPoints(LáblécMéret);
                Táblaterület.PrintHeadings = false;
                Táblaterület.PrintGridlines = false;
                Táblaterület.PrintComments = Microsoft.Office.Interop.Excel.XlPrintLocation.xlPrintNoComments;
                Táblaterület.CenterHorizontally = vízszintes;
                Táblaterület.CenterVertically = függőleges;
                Táblaterület.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
                Táblaterület.Draft = false;
                Táblaterület.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
                Táblaterület.Order = Microsoft.Office.Interop.Excel.XlOrder.xlDownThenOver;
                Táblaterület.BlackAndWhite = false;
                Táblaterület.Zoom = false;
                if (int.TryParse(oldalszéles, out int széles))
                    Táblaterület.FitToPagesWide = széles;
                else
                    Táblaterület.FitToPagesWide = false;

                if (int.TryParse(oldalmagas, out int magas))
                    Táblaterület.FitToPagesTall = magas;
                else
                    Táblaterület.FitToPagesTall = false;

                if (álló)
                    Táblaterület.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
                else
                    Táblaterület.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

                if (papírméret == "A4")
                    Táblaterület.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
                else
                    Táblaterület.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA3;
            }

            Táblaterület.PrintErrors = Microsoft.Office.Interop.Excel.XlPrintErrors.xlPrintErrorsDisplayed;
        }



        /// <summary>
        /// A küldött névvel beszúr utolsó lapnak egy munkalapot
        /// </summary>
        /// <param name="név"></param>
        public static void Új_munkalap(string név)
        {
            //Munakalap hozzáadás
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets.Add();
            // munkalap átnevezéséhez 
            Munkalap.Name = név;
        }


        /// <summary>
        /// Egy munkalapot átnevez és aktívvá teszi
        /// </summary>
        /// <param name="régi"></param>
        /// <param name="új"></param>
        public static void Munkalap_átnevezés(string régi, string új)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[régi];
            Munkalap.Name = új;
            Munkalap.Select();
        }


        /// <summary>
        /// Kijelöljük a munkalapot
        /// </summary>
        /// <param name="név"></param>
        public static void Munkalap_aktív(string név)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[név];
            Munkalap.Select();
        }



        /// <summary>
        /// Betű típusát és méretét állítja be a munkalapon
        /// </summary>
        /// <param name="név"></param>
        /// <param name="méret"></param>
        public static void Munkalap_betű(string név, int méret)
        {
            MyExcel.Range Cellák = Module_Excel.xlApp.Application.Cells;
            Cellák.VerticalAlignment = Constants.xlCenter;
            Cellák.Font.Name = név;
            Cellák.Font.Size = méret;
            Cellák.Font.Strikethrough = false;
            Cellák.Font.Superscript = false;
            Cellák.Font.Subscript = false;
            Cellák.Font.OutlineFont = false;
            Cellák.Font.Shadow = false;
            Cellák.Font.Underline = Microsoft.Office.Interop.Excel.XlUnderlineStyle.xlUnderlineStyleNone;
            Cellák.Font.ThemeColor = Microsoft.Office.Interop.Excel.XlThemeColor.xlThemeColorLight1;
            Cellák.Font.TintAndShade = 0;
            Cellák.Font.ThemeFont = Microsoft.Office.Interop.Excel.XlThemeFont.xlThemeFontNone;
        }
        /// <summary>
        /// A munkalapot, úgy mozgatja, hogy a kívánt cella rajta legyen a képernyőn.
        /// </summary>
        /// <param name="munkalap"></param>
        /// <param name="mit"></param>
        public static void Aktív_Cella(string munkalap, string mit)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            Munkalap.Select();

            MyExcel.Range range = Munkalap.get_Range(mit, mit);
            range.Select();
        }


        /// <summary>
        /// Sormagasságot lehet beállítani
        /// </summary>
        /// <param name="mit">szöveg</param>
        /// <param name="mekkora">egész</param>
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

        /// <summary>
        /// A cellába beírt szöveg olvasási irányát lehet beállítani
        /// </summary>
        /// <param name="munkalap">munkalap neve</param>
        /// <param name="mit">cella helyzete</param>
        /// <param name="mennyit">-90 bal- 0 vízszintes- 90 jobb</param>
        public static void SzövegIrány(string munkalap, string mit, double mennyit)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range Táblaterület = Munkalap.Range[mit];
            Táblaterület.Orientation = mennyit;
        }



        /// <summary>
        /// Oszlop sorszámát átalakítja az oszlop jelölő betűvé 3 db Betűhosszt tud számolni ZZZ
        /// </summary>
        /// <param name="sorszám">Int adunnk át</param>
        /// <returns></returns>
        public static string Oszlopnév(int sorszám)
        {
            string ideig;
            if (sorszám < 27)
            {
                ideig = Convert.ToString(Convert.ToChar(sorszám + 64));
            }
            //Ha maradék nélkül lehet osztani
            else if (sorszám < 703)
            {
                int elsőbetűszáma = ((sorszám - 1) / 26) + 64;
                int maradék = (sorszám - 1) % 26;
                int másodikbetűszáma = maradék + 65;
                Char betű1 = Convert.ToChar(elsőbetűszáma);
                Char betű2 = Convert.ToChar(másodikbetűszáma);
                ideig = Convert.ToString(betű1) + Convert.ToString(betű2);
            }
            else
            {
                int betűszáma1 = ((sorszám) / 676) + 64;
                int maradék1 = sorszám % 676;
                int betűszáma2 = ((maradék1) / 26) + 64;
                int maradék2 = maradék1 % 26;
                int betűszáma3 = maradék2 + 64;
                Char betű1 = Convert.ToChar(betűszáma1);
                Char betű2 = Convert.ToChar(betűszáma2);
                Char betű3 = Convert.ToChar(betűszáma3);
                ideig = Convert.ToString(betű1) + Convert.ToString(betű2) + Convert.ToString(betű3);
            }
            return ideig.Replace('@', 'A');
        }


        /// <summary>
        /// A cella tartalmát sortöréssel több sorba írja
        /// </summary>
        /// <param name="mit"></param>
        /// 

        public static void Sortörésseltöbbsorba(string mit)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Cells.Select();

            Táblaterület.HorizontalAlignment = Constants.xlGeneral;
            Táblaterület.VerticalAlignment = Constants.xlCenter;
            Táblaterület.WrapText = true;
            Táblaterület.Orientation = 0;
            Táblaterület.AddIndent = false;
            Táblaterület.IndentLevel = 0;
            Táblaterület.ShrinkToFit = false;
            Táblaterület.MergeCells = false;
        }


        public static void Sortörésseltöbbsorba(string mit, bool egyesített)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Cells.Select();

            Táblaterület.HorizontalAlignment = Constants.xlGeneral;
            Táblaterület.VerticalAlignment = Constants.xlCenter;
            Táblaterület.WrapText = true;
            Táblaterület.Orientation = 0;
            Táblaterület.AddIndent = false;
            Táblaterület.IndentLevel = 0;
            Táblaterület.ShrinkToFit = false;
            Táblaterület.MergeCells = egyesített;
        }

        public static void Sortörésseltöbbsorba_egyesített(string mit)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.Cells.Select();

            Táblaterület.HorizontalAlignment = Constants.xlGeneral;
            Táblaterület.VerticalAlignment = Constants.xlCenter;
            Táblaterület.WrapText = true;
            Táblaterület.Orientation = 0;
            Táblaterület.AddIndent = false;
            Táblaterület.IndentLevel = 0;
            Táblaterület.ShrinkToFit = false;
            Táblaterület.MergeCells = true;
        }

        public static void Szűrés(string munkalap, int oszloptól, int oszlopig, int sor)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range myRange = Munkalap.Range[Oszlopnév(oszloptól) + ":" + Oszlopnév(oszlopig)];
#pragma warning disable IDE0059 // Unnecessary assignment of a value
            object result = myRange.AutoFilter(sor);
#pragma warning restore IDE0059 // Unnecessary assignment of a value
        }


        public static void Szűrés(string munkalap, string oszloptól, string oszlopig, int sor)
        {

            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range myRange = Munkalap.Range[oszloptól + ":" + oszlopig];
#pragma warning disable IDE0059 // Unnecessary assignment of a value
            object result = myRange.AutoFilter(sor);
#pragma warning restore IDE0059 // Unnecessary assignment of a value
        }

        public static void Szűrés(string munkalap, string mit, int sor)
        {

            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range myRange = Munkalap.Range[mit];
            object result = myRange.AutoFilter(sor);
        }


        public static void Pontvonal(string mit)
        {

            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];

            Táblaterület.Borders.Item[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
            Táblaterület.Borders.Item[XlBordersIndex.xlEdgeTop].ColorIndex = Constants.xlAutomatic;
            Táblaterület.Borders.Item[XlBordersIndex.xlEdgeTop].TintAndShade = 0;
            Táblaterület.Borders.Item[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlHairline;
        }

        /// <summary>
        /// Megnyitja az excel, html lapot
        /// </summary>
        /// <param name="Fájlhelye"></param>
        public static void Megnyitás(string Fájlhelye)
        {
            if (!Exists(Fájlhelye)) return;
            Process.Start(Fájlhelye);
        }


        public static void ExcelMegnyitás(string hely)
        {
            xlApp = new MyExcel.Application();
            //  xlApp.Visible = true;
            xlWorkBook = xlApp.Workbooks.Open(hely);
        }

        public static void Értékmásol(string hol)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[hol];
            Táblaterület.Range[hol].Cells.Select();
            Táblaterület.Copy();
            Táblaterület.PasteSpecial(Paste: XlPasteType.xlPasteValues, Operation: XlPasteSpecialOperation.xlPasteSpecialOperationNone, SkipBlanks: false, Transpose: false);
        }

        public static void Értékmásol(string munkalap, string honnan, string hova)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range területhonnan = Munkalap.Range[honnan];
            MyExcel.Range területhova = Munkalap.Range[hova];

            területhonnan.Select();
            területhonnan.Copy();
            területhova.Select();
            területhova.PasteSpecial(Paste: XlPasteType.xlPasteValues, Operation: XlPasteSpecialOperation.xlPasteSpecialOperationNone, SkipBlanks: false, Transpose: false);
        }


        public static void Képlet_másol(string munkalap, string honnan, string hova)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range területhonnan = Munkalap.Range[honnan];
            MyExcel.Range területhova = Munkalap.Range[hova];

            területhonnan.Select();
            területhonnan.Copy();
            területhova.Select();
            területhova.PasteSpecial(Paste: XlPasteType.xlPasteFormulas, Operation: XlPasteSpecialOperation.xlPasteSpecialOperationNone, SkipBlanks: false, Transpose: false);
        }

        public static string Beolvas(string honnan)
        {
            string válasz = "_";
            MyExcel.Range Cella = Module_Excel.xlApp.Application.Range[honnan];

            if (Cella.Value != null)
                válasz = Cella.Value.ToStrTrim();

            return válasz;
        }

        public static void Kicsinyít(string mit)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];
            Táblaterület.ShrinkToFit = true;
        }

        public static DateTime BeolvasDátum(string honnan)
        {
            DateTime válasz = new DateTime(1900, 1, 1);
            MyExcel.Range Cella = Module_Excel.xlApp.Application.Range[honnan];
            try
            {

                if (Cella.Value == null)
                {
                    válasz = new DateTime(1900, 1, 1);
                }
                else if (!int.TryParse(Cella.Value.ToString(), out int result))
                {
                    válasz = Convert.ToDateTime(Cella.Value);
                }
                else
                {
                    válasz = Convert.ToDateTime(Cella.Value);
                }

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"{Cella.Value}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return válasz;
        }

        public static DateTime Beolvasidő(string honnan)
        {
            DateTime válasz = new DateTime(1900, 1, 1, 0, 0, 0);
            MyExcel.Range Cella = Module_Excel.xlApp.Application.Range[honnan];

            if (Cella.Value == null)
            {
                válasz = new DateTime(1900, 1, 1, 0, 0, 0);
            }
            else if (decimal.TryParse(Cella.Value.ToString(), out decimal ideig))
            {
                int óra, perc, másodperc;
                decimal órad, percd, másodpercd;

                órad = ideig * 24;
                óra = ((int)órad);
                órad = órad - Convert.ToDecimal(óra);

                percd = órad * 60;
                perc = (int)percd;
                percd = percd - Convert.ToDecimal(perc);

                másodpercd = percd * 60;
                másodperc = (int)másodpercd;

                válasz = new DateTime(1900, 1, 1, óra, perc, másodperc);
            }
            else if (Cella.Value.ToString().Contains(":"))
            {
                string[] darab = Cella.Value.ToString().Split(':');
                int óra = int.Parse(darab[0]);
                int perc = int.Parse(darab[1]);
                int másodperc;
                if (darab.Length > 2)
                    másodperc = int.Parse(darab[2]);
                else
                    másodperc = 0;


                válasz = new DateTime(1900, 1, 1, óra, perc, másodperc);
            }
            return válasz;
        }

        public static void Aláírásvonal(string mit)

        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];

            Táblaterület.Borders.Item[XlBordersIndex.xlDiagonalDown].LineStyle = Constants.xlNone;
            Táblaterület.Borders.Item[XlBordersIndex.xlDiagonalUp].LineStyle = Constants.xlNone;
            Táblaterület.Borders.Item[XlBordersIndex.xlEdgeLeft].LineStyle = Constants.xlNone;

            Táblaterület.Borders.Item[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlDash;
            Táblaterület.Borders.Item[XlBordersIndex.xlEdgeTop].ColorIndex = Constants.xlAutomatic;
            Táblaterület.Borders.Item[XlBordersIndex.xlEdgeTop].TintAndShade = 0;
            Táblaterület.Borders.Item[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlThin;

            Táblaterület.Borders.Item[XlBordersIndex.xlEdgeBottom].LineStyle = Constants.xlNone;
            Táblaterület.Borders.Item[XlBordersIndex.xlEdgeRight].LineStyle = Constants.xlNone;
            Táblaterület.Borders.Item[XlBordersIndex.xlInsideVertical].LineStyle = Constants.xlNone;
            Táblaterület.Borders.Item[XlBordersIndex.xlInsideHorizontal].LineStyle = Constants.xlNone;
        }

        public static void Sorbarednezés(int oszlopszám, string terület)
        {
            MyExcel.Range Táblaterület = (MyExcel.Range)Module_Excel.xlApp.Application.Range[terület];

            Táblaterület.Sort(Táblaterület.Columns[oszlopszám], XlSortOrder.xlAscending,
                                misValue, misValue, XlSortOrder.xlAscending,
                                misValue, XlSortOrder.xlAscending,
                                XlYesNoGuess.xlGuess, misValue, misValue, XlSortOrientation.xlSortColumns, XlSortMethod.xlPinYin, XlSortDataOption.xlSortNormal);
        }

        public static void ConvertToTxt(string excel, string csv)
        {

            xlApp = new MyExcel.Application();
            // //xlApp.Visible = true;
            xlWorkBook = xlApp.Workbooks.Open(excel);

            //xlApp.DisplayAlerts = false;
            xlWorkBook.SaveAs(csv, Microsoft.Office.Interop.Excel.XlFileFormat.xlTextWindows, misValue, misValue, misValue, XlSaveAsAccessMode.xlNoChange,
                   Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);

            ExcelBezárás();
        }

        public static int Utolsósor(string munkalap)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range Range = Munkalap.UsedRange;
            int maxRow = Range.Rows.Count;
            return maxRow;
        }

        public static int Utolsóoszlop(string munkalap)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range RangeX = Munkalap.UsedRange;
            int maxColumn = RangeX.Columns.Count;
            return maxColumn;
        }


        public static void Keret(string mit, bool jobb, bool bal, bool alsó, bool felső)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];

            if (jobb)
            {
                Táblaterület.Borders.Item[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
                Táblaterület.Borders.Item[XlBordersIndex.xlEdgeRight].ColorIndex = Constants.xlAutomatic;
                Táblaterület.Borders.Item[XlBordersIndex.xlEdgeRight].TintAndShade = 0;
                Táblaterület.Borders.Item[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
            }

            if (bal)
            {
                Táblaterület.Borders.Item[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
                Táblaterület.Borders.Item[XlBordersIndex.xlEdgeLeft].ColorIndex = Constants.xlAutomatic;
                Táblaterület.Borders.Item[XlBordersIndex.xlEdgeLeft].TintAndShade = 0;
                Táblaterület.Borders.Item[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThin;
            }

            if (alsó)
            {
                Táblaterület.Borders.Item[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
                Táblaterület.Borders.Item[XlBordersIndex.xlEdgeBottom].ColorIndex = Constants.xlAutomatic;
                Táblaterület.Borders.Item[XlBordersIndex.xlEdgeBottom].TintAndShade = 0;
                Táblaterület.Borders.Item[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin;
            }

            if (felső)
            {
                Táblaterület.Borders.Item[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
                Táblaterület.Borders.Item[XlBordersIndex.xlEdgeTop].ColorIndex = Constants.xlAutomatic;
                Táblaterület.Borders.Item[XlBordersIndex.xlEdgeTop].TintAndShade = 0;
                Táblaterület.Borders.Item[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlThin;
            }
        }


        public static void Kép_beillesztés(string munkalap, String mit, string hely)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];

            xlWorkSheet.Shapes.AddPicture(hely, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, 50, 30, 420, 175);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="munkalap"></param>
        /// <param name="mit"></param>
        /// <param name="hely"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Magas"></param>
        /// <param name="Széles"></param>
        public static void Kép_beillesztés(string munkalap, String mit, string hely, int X, int Y, int Magas, int Széles)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];

            xlWorkSheet.Shapes.AddPicture(hely, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, X, Y, Széles, Magas);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hova">cella hivatkozás</param>
        /// <param name="hivatkozottlap">munkalap neve amire mutat</param>
        public static void Link_beillesztés(String munkalap, string hova, string hivatkozottlap)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range Táblaterület = Munkalap.Range[hova];
            Táblaterület.Hyperlinks.Add(Anchor: Táblaterület, Address: "", SubAddress: "'" + hivatkozottlap + "'!A1", TextToDisplay: "'" + hivatkozottlap + "'");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="munkalap_adat">Az a munkalap melyen vannak az adatok</param>
        /// <param name="balfelső">A kimutatás alaptáblájának bal felső cellaneve</param>
        /// <param name="jobbalsó">>A kimutatás alaptáblájának jobb alsó cellaneve</param>
        /// <param name="kimutatás_Munkalap">Az a munkalap amelyikre tesszük a kimutatást</param>
        /// <param name="Kimutatás_cella">Kimutatás bal felső cellája</param>
        /// <param name="Kimutatás_név">Kimutatás neve</param>
        /// <param name="összesítNév">Azon adatok listája amit összesítünk</param>
        /// <param name="sorNév">Sorokban szereplő értékek listája</param>
        /// <param name="oszlopNév">Oszlopokban szereplő értékek listája</param>
        /// <param name="SzűrőNév">Szűrő Nevek listája</param>
        /// <param name="SzűrőÉrték">Szűrt értékek</param>
        public static void Kimutatás_Fő(string munkalap_adat, string balfelső, string jobbalsó, string kimutatás_Munkalap, string Kimutatás_cella, string Kimutatás_név
            , List<string> összesítNév, List<string> sorNév, List<string> oszlopNév, List<string> SzűrőNév)
        {

            MyExcel.Worksheet Adatok_lap = (Worksheet)xlWorkBook.Worksheets[munkalap_adat];
            MyExcel.Worksheet Kimutatás_lap = (Worksheet)xlWorkBook.Worksheets[kimutatás_Munkalap];

            MyExcel.Range AdatRange = Adatok_lap.Range[balfelső, jobbalsó];

            PivotCaches pivotCaches = xlWorkBook.PivotCaches();
            MyExcel.Range pivotData = Adatok_lap.Range[balfelső, jobbalsó];

            MyExcel.PivotCache pivotCache = pivotCaches.Create(XlPivotTableSourceType.xlDatabase, pivotData);
            MyExcel.PivotTable pivotTable = pivotCache.CreatePivotTable(Kimutatás_lap.Range[Kimutatás_cella], Kimutatás_név);

            //Táblázatban megjelenő érték
            if (összesítNév.Count > 0)
            {
                for (int i = 0; i < összesítNév.Count; i++)
                {

                    PivotField salesField = (PivotField)pivotTable.PivotFields(összesítNév[i]);
                    salesField.Orientation = XlPivotFieldOrientation.xlDataField;
                    salesField.Function = XlConsolidationFunction.xlSum;
                    salesField.Name = összesítNév[i] + " db";
                }
            }
            //Sor adatok
            if (sorNév.Count > 0)
            {
                for (int i = 0; i < sorNév.Count; i++)
                {
                    PivotField colorsRowsField = (PivotField)pivotTable.PivotFields(sorNév[i]);
                    colorsRowsField.Orientation = XlPivotFieldOrientation.xlRowField;
                }
            }

            //oszlopok 
            if (oszlopNév.Count > 0)
            {
                for (int i = 0; i < oszlopNév.Count; i++)
                {
                    PivotField regionField = (PivotField)pivotTable.PivotFields(oszlopNév[i]);
                    regionField.Orientation = XlPivotFieldOrientation.xlColumnField;
                }
            }

            //Szűrő mezők
            if (SzűrőNév.Count > 0)
            {
                for (int i = 0; i < SzűrőNév.Count; i++)
                {
                    PivotField datefield = (PivotField)pivotTable.PivotFields(SzűrőNév[i]);
                    datefield.Orientation = XlPivotFieldOrientation.xlPageField;
                    datefield.EnableMultiplePageItems = true;
                }
            }
            //Szűrés egy napra
            //    datefield.CurrentPage = SzűrőÉrték;
        }


        public static void Kimutatás_Fő(string munkalap_adat, string balfelső, string jobbalsó, string kimutatás_Munkalap, string Kimutatás_cella, string Kimutatás_név
    , List<string> összesítNév, List<string> Összesítés_módja, List<string> sorNév, List<string> oszlopNév, List<string> SzűrőNév)
        {
            try
            {
                MyExcel.Worksheet Adatok_lap = (Worksheet)xlWorkBook.Worksheets[munkalap_adat];
                MyExcel.Worksheet Kimutatás_lap = (Worksheet)xlWorkBook.Worksheets[kimutatás_Munkalap];

                MyExcel.Range AdatRange = Adatok_lap.Range[balfelső, jobbalsó];

                PivotCaches pivotCaches = xlWorkBook.PivotCaches();
                MyExcel.Range pivotData = Adatok_lap.Range[balfelső, jobbalsó];

                MyExcel.PivotCache pivotCache = pivotCaches.Create(XlPivotTableSourceType.xlDatabase, pivotData);
                MyExcel.PivotTable pivotTable = pivotCache.CreatePivotTable(Kimutatás_lap.Range[Kimutatás_cella], Kimutatás_név);

                //Táblázatban megjelenő érték
                if (összesítNév.Count > 0)
                {
                    for (int i = 0; i < összesítNév.Count; i++)
                    {

                        PivotField salesField = (PivotField)pivotTable.PivotFields(összesítNév[i]);
                        salesField.Orientation = XlPivotFieldOrientation.xlDataField;
                        switch (Összesítés_módja[i])
                        {

                            case "xlSum":
                                salesField.Function = XlConsolidationFunction.xlSum;
                                salesField.Name = összesítNév[i] + " db";
                                break;

                            case "xlCount":
                                salesField.Function = XlConsolidationFunction.xlCount;
                                salesField.Name = összesítNév[i] + " Összeg";
                                break;

                            default:
                                break;
                        }


                    }
                }
                //oszlopok 
                if (oszlopNév.Count > 0)
                {
                    for (int i = 0; i < oszlopNév.Count; i++)
                    {
                        PivotField regionField = (PivotField)pivotTable.PivotFields(oszlopNév[i]);
                        regionField.Orientation = XlPivotFieldOrientation.xlColumnField;
                        regionField.Position = i + 1;
                    }
                }

                //Sor adatok
                if (sorNév.Count > 0)
                {
                    for (int i = 0; i < sorNév.Count; i++)
                    {
                        PivotField colorsRowsField = (PivotField)pivotTable.PivotFields(sorNév[i]);
                        colorsRowsField.Orientation = XlPivotFieldOrientation.xlRowField;
                    }
                }



                //Szűrő mezők
                if (SzűrőNév.Count > 0)
                {
                    for (int i = 0; i < SzűrőNév.Count; i++)
                    {
                        PivotField datefield = (PivotField)pivotTable.PivotFields(SzűrőNév[i]);
                        datefield.Orientation = XlPivotFieldOrientation.xlPageField;
                        datefield.EnableMultiplePageItems = true;
                    }
                }
                //Szűrés egy napra
                //    datefield.CurrentPage = SzűrőÉrték;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Kimutatás_Fő", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Nyomtatás(string munkalap, int kezdőoldal, int példányszám)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];

            Munkalap.PrintOutEx(kezdőoldal, misValue, példányszám, false);

        }

        public static void Törlés(string munkalap, string mit)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range Táblaterület = Munkalap.Range[mit];
            Táblaterület.ClearContents();
        }

        public static void FerdeVonal(string mit)
        {
            MyExcel.Range Táblaterület = Module_Excel.xlApp.Application.Range[mit];

            Táblaterület.Borders.Item[XlBordersIndex.xlDiagonalDown].LineStyle = XlLineStyle.xlContinuous;
            Táblaterület.Borders.Item[XlBordersIndex.xlDiagonalDown].ColorIndex = Constants.xlAutomatic;
            Táblaterület.Borders.Item[XlBordersIndex.xlDiagonalDown].TintAndShade = 0;
            Táblaterület.Borders.Item[XlBordersIndex.xlDiagonalDown].Weight = XlBorderWeight.xlThin;
        }

        /// <summary>
        /// Munkalapot a jelzett helyen és sornál két részre osztja
        /// </summary>
        /// <param name="munkalap">munkalap neve</param>
        /// <param name="mit">Cella jelölés ahol osztani akarunk</param>
        /// <param name="sor">a sornak a neve ahol osztani akarunk</param>
        public static void Nyom_Oszt(string munkalap, string mit, int sor, int oldaltörés = 1)
        {

            xlApp.ActiveWindow.View = XlWindowView.xlPageBreakPreview;
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            MyExcel.Range Táblaterület = Munkalap.Range[mit];
            Munkalap.HPageBreaks.Add(Munkalap.Cells[sor, oldaltörés]);
        }

        public static void Kép_beillesztés(string munkalap, string hova, string fájl, float bal, float teteje, float széles, float magas)
        {
            Worksheet Munka_lap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];
            Munka_lap.Shapes.AddPicture(fájl, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, bal, teteje, széles, magas);

        }

        public static void Diagram(string munkalap, int felsőx, int felsőy, int alsóx, int alsóy, string táblafelső, string táblaalsó)
        {
            Worksheet Munkalap = (MyExcel.Worksheet)Module_Excel.xlWorkBook.Worksheets[munkalap];

            MyExcel.Range crange;
            MyExcel.ChartObjects cb = (MyExcel.ChartObjects)Munkalap.ChartObjects(Type.Missing);
            MyExcel.ChartObject cbc = (MyExcel.ChartObject)cb.Add(felsőx, felsőy, alsóx, alsóy);
            MyExcel.Chart cp = cbc.Chart;

            crange = Munkalap.get_Range(táblafelső, táblaalsó);
            cp.SetSourceData(crange, misValue);
            cp.ChartType = MyExcel.XlChartType.xlPie;
            cp.ApplyLayout(1);

        }



        // Beosztás Excel tábla
        /*
        public static void BeoTábla(string fájlexc, object tábla, DateTime Dátum)
        {

            // ha üres a tábla akkor kilép
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectLessEqual(tábla.Rows.Count, 1, false)))
                return;


            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            // formázáshoz
            xlWorkBook = MyExcel.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)MyExcel.Worksheets.Item(1);
            // xlWorkSheet = CType(xlWorkBook.Worksheets.Item(1), Excel.Worksheet)
            // *********************** Táblázat munkalap ********************************

            MyExcel.Sheets("Munka1").Select();
            MyExcel.Sheets("Munka1").Name = "Beosztás";
            MyExcel.Sheets.Add(After: MyExcel.Sheets(MyExcel.Sheets.Count));
            MyExcel.Sheets("Munka2").Name = "Részletes";
            MyExcel.Sheets("Beosztás").Select();

            int sorelőző = 1;
            xlWorkSheet.Cells(sorelőző, 1).Value = Strings.Format(Dátum, "yyyy.MM.") + " havi beosztás";

            sorelőző = 3;
            // fejléc kiírása
            var loopTo = Convert.ToInt32(Operators.SubtractObject(tábla.ColumnCount, 1));
            for (oszlop = 0; oszlop <= loopTo; oszlop++)
                xlWorkSheet.Cells(sorelőző, oszlop + 1).Value = tábla.Columns(oszlop).HeaderText;
            sorelőző += 1;
            // tartalom kiírása
            var loopTo1 = Convert.ToInt32(Operators.SubtractObject(tábla.RowCount, 1));
            for (sor = 0; sor <= loopTo1; sor++)
            {
                var loopTo2 = Convert.ToInt32(Operators.SubtractObject(tábla.ColumnCount, 1));
                for (oszlop = 0; oszlop <= loopTo2; oszlop++)
                {
                    xlWorkSheet.Cells(sorelőző + sor, oszlop + 1).Value = tábla.Rows(sor).Cells(oszlop).Value;
                    xlWorkSheet.Cells(2, 53).Value = oszlop + 1;
                }
                xlWorkSheet.Cells(1, 53).Value = sorelőző + sor;
            }


            // megformázzuk
            MyExcel.Range("A3:" + Oszlopnév(Convert.ToInt32(Beolvas("BA2"))) + "3").Select();
            {
                var withBlock = MyExcel.Selection.Interior;
                withBlock.ColorIndex = 6;
            }

            // rácsozás
            Rácsoz("A3:" + Oszlopnév(Convert.ToInt32(Beolvas("BA2"))) + Beolvas("ba1"));

            // Oszlopok beállítása
            MyExcel.Columns("A:A").Select();
            MyExcel.Selection.ColumnWidth = 30;
            MyExcel.Columns("B:B").Select();
            MyExcel.Selection.ColumnWidth = 7;
            MyExcel.Columns("C:C").Select();
            MyExcel.Selection.ColumnWidth = 7;
            MyExcel.Columns("D:" + Oszlopnév(Convert.ToInt32(Beolvas("BA2")) - 1)).Select();
            MyExcel.Selection.ColumnWidth = 5;
            MyExcel.Columns(Oszlopnév(Convert.ToInt32(Beolvas("BA2"))) + ":" + Oszlopnév(Convert.ToInt32(Beolvas("BA2")))).Select();
            MyExcel.Selection.ColumnWidth = 10;




            MyExcel.Selection.Font.Bold = true;
            MyExcel.Cells.Select();

            {
                var withBlock1 = MyExcel.Selection.Font;
                withBlock1.Name = "Arial";
                withBlock1.Size = 12;
                withBlock1.Strikethrough = false;
                withBlock1.Superscript = false;
                withBlock1.Subscript = false;
                withBlock1.OutlineFont = false;
                withBlock1.Shadow = false;
                withBlock1.Underline = Microsoft.Office.Interop.Excel.XlUnderlineStyle.xlUnderlineStyleNone;
                withBlock1.ColorIndex = Microsoft.Office.Interop.Excel.Constants.xlAutomatic;
            }


            // fejléc
            MyExcel.Range("A3:" + Oszlopnév(Convert.ToInt32(Beolvas("BA2"))) + "3").Select();
            {
                var withBlock2 = MyExcel.Selection.Interior;
                withBlock2.ColorIndex = 15;
                withBlock2.Pattern = Microsoft.Office.Interop.Excel.Constants.xlSolid;
            }
            MyExcel.Selection.Font.Bold = true;

            // szo v más színnel
            var loopTo3 = Convert.ToInt32(Beolvas("BA2"));
            for (i = 4; i <= loopTo3; i++)
            {
                switch (tábla.Rows((object)0).Cells((object)(i - 1)).Style.BackColor)
                {
                    case var @case when Operators.ConditionalCompareObjectEqual(@case, Color.FromArgb(186, 176, 165), false):
                        {
                            MyExcel.Range(Oszlopnév(i) + "3:" + Oszlopnév(i) + Beolvas("BA1")).Select();
                            MyExcel.Selection.Interior.Color = 8372031;
                            break;
                        }

                    case var case1 when Operators.ConditionalCompareObjectEqual(case1, Color.FromArgb(228, 189, 141), false):
                        {
                            MyExcel.Range(Oszlopnév(i) + "3:" + Oszlopnév(i) + Beolvas("BA1")).Select();
                            MyExcel.Selection.Interior.Color = 7171583;
                            break;
                        }
                    case var case2 when Operators.ConditionalCompareObjectEqual(case2, Color.FromArgb(244, 95, 95), false):
                        {
                            MyExcel.Range(Oszlopnév(i) + "3:" + Oszlopnév(i) + Beolvas("BA1")).Select();
                            MyExcel.Selection.Interior.Color = 7171583;
                            break;
                        }
                }


            }


            // nyomtatási terület
            MyExcel.ActiveSheet.PageSetup.PrintArea = "$A$1:$" + Oszlopnév(Convert.ToInt32(Beolvas("BA2"))) + "$" + (Conversions.ToDouble(Beolvas("ba1")) + 1d).ToString();
            {
                var withBlock3 = MyExcel.ActiveSheet.PageSetup;
                withBlock3.LeftHeader = "";
                withBlock3.CenterHeader = Strings.Trim(Konvertáló.My.MyProject.Forms.AblakFőoldal.Panels1.Text);
                withBlock3.RightHeader = Strings.Format(DateTime.Now, "yyyy.MM.dd hh:mm");
                withBlock3.LeftFooter = "";
                withBlock3.CenterFooter = "";
                withBlock3.RightFooter = "";
                withBlock3.LeftMargin = MyExcel.InchesToPoints(0.590551181102362d);
                withBlock3.RightMargin = MyExcel.InchesToPoints(0.590551181102362d);
                withBlock3.TopMargin = MyExcel.InchesToPoints(0.78740157480315d);
                withBlock3.BottomMargin = MyExcel.InchesToPoints(0.590551181102362d);
                withBlock3.HeaderMargin = MyExcel.InchesToPoints(0.511811023622047d);
                withBlock3.FooterMargin = MyExcel.InchesToPoints(0.511811023622047d);
                withBlock3.PrintHeadings = false;
                withBlock3.PrintGridlines = false;
                withBlock3.PrintComments = Microsoft.Office.Interop.Excel.XlPrintLocation.xlPrintNoComments;
                withBlock3.CenterHorizontally = false;
                withBlock3.CenterVertically = false;
                withBlock3.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
                withBlock3.Draft = false;
                withBlock3.PaperSize = Microsoft.Office.Interop.Excel.XlPaperSize.xlPaperA4;
                withBlock3.FirstPageNumber = Microsoft.Office.Interop.Excel.Constants.xlAutomatic;
                withBlock3.Order = Microsoft.Office.Interop.Excel.XlOrder.xlDownThenOver;
                withBlock3.BlackAndWhite = false;
                withBlock3.Zoom = false;
                withBlock3.FitToPagesWide = 1;
                withBlock3.FitToPagesTall = false;
                withBlock3.PrintErrors = Microsoft.Office.Interop.Excel.XlPrintErrors.xlPrintErrorsDisplayed;
            }
            // MyExcel.ActiveWindow.WindowState = Microsoft.Office.Interop.Excel.XlWindowState.xlMaximized
            MyExcel.Range("A1").Select();

            xlWorkBook.SaveAs(fájlexc, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);

            // az excel tábla bezárása
            MyExcel.Workbooks.Close();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(MyExcel.Workbooks);
            // alkalmazás leállítása
            // MyExcel.Quit()
            // System.Runtime.InteropServices.Marshal.ReleaseComObject(MyExcel)
            // '    MyExcel = Nothing
            GC.Collect();


        }
        */

    }
}

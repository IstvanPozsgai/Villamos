using ArrayToExcel;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using DataTable = System.Data.DataTable;
using MyExcel = Microsoft.Office.Interop.Excel;

namespace Villamos
{
    public static partial class Module_Excel
    {
        /// <summary>
        /// Ez a változat közvetlenül Adattáblából írja ki az adatokat
        /// és az ArrayToExcel könyvtárat használja
        /// </summary>
        /// <param name="Tábla"></param>
        /// <param name="fájl"></param>
        /// <param name="Elemek"></param>
        public static void DataTableToExcel(string fájl, DataTable Tábla, DataGridView TáblaGrid = null)
        {
            try
            {
                byte[] excel = Tábla.ToExcel(a => a.SheetName("Munka1"));
                File.WriteAllBytes(fájl, excel);

                ///Megnyitjuk az Excel Formázásra
                ExcelMegnyitás(fájl);

                ////Utolsó oszlop és sor adatok
                oszlop = Tábla.Columns.Count;
                sor = Tábla.Rows.Count;

                Háttérszín($"A1:{Oszlopnév(oszlop)}1", System.Drawing.Color.Yellow); //Sárga háttér
                Betű($"A1:{Oszlopnév(oszlop)}1", System.Drawing.Color.Black);  //Fekete betű
                Betű($"A1:{Oszlopnév(oszlop)}1", false, false, true); //vastag betű

                Rácsoz($"A1:{Oszlopnév(oszlop)}{sor + 1}"); // rácsozás
                Oszlopszélesség("Munka1", $"A:{Oszlopnév(oszlop)}");     //Automata Oszlop szélesség beállítás

                if (TáblaGrid != null) Színezés(TáblaGrid);

                Tábla_Rögzítés(1);  //Rögzítjük a fejlécet
                Szűrés("Munka1", 1, oszlop, 1);    //szűrést felteszük

                //Nyomtatási terület kijelülése
                NyomtatásiTerület_részletes("Munka1", $"A1:{Oszlopnév(oszlop)}{sor + 1}", "$1:$1", "", true);

                //Beállunk az A1 cellába
                xlApp.Range["A1"].Select();

                ExcelMentés();
                ExcelBezárás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "ExcelTábla", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Kiszinézi a DataGridView adatai szerint az Excel tábla adat sorait
        /// csak azokat a cellákat színezi ami nem fehér
        /// Nagy táblázatoknál sokáig tart
        /// </summary>
        /// <param name="TáblaDat"></param>
        private static void Színezés(DataGridView TáblaDat)
        {
            try
            {
                int sor = 2;
                int oszlop = 1;
                Color Háttér;
                for (int i = 0; i < TáblaDat.Rows.Count; i++)
                {
                    for (int j = 0; j < TáblaDat.Columns.Count; j++)
                    {
                        Háttér = TáblaDat.Rows[i].Cells[j].Style.BackColor;
                        if (Háttér.Name == "0") Háttér = Color.White;
                        if (Háttér != Color.White) Háttérszín(Oszlopnév(oszlop + j) + (sor + i).ToString(), Háttér);
                    }
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Színezés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// DataGridView adatai alapján Excel táblát készít
        /// </summary>
        /// <param name="fájl">Mentési név</param>
        /// <param name="Tábla">DataGridView tábla</param>
        /// <param name="színes">DataGridView tábla színezését át akarjuk vinni az Excel táblába</param>
        public static void DataGridViewToExcel(string fájl, DataGridView Tábla, bool színes = false)
        {
            try
            {
                DataTable ÚjTábla = new DataTable();
                foreach (DataGridViewColumn oszlop in Tábla.Columns)
                {
                    if (oszlop.Visible)
                    {
                        ÚjTábla.Columns.Add(oszlop.HeaderText);
                    }
                }
                foreach (DataGridViewRow sor in Tábla.Rows)
                {
                    DataRow ÚjSor = ÚjTábla.NewRow();
                    for (int i = 0; i < Tábla.Columns.Count; i++)
                    {
                        if (Tábla.Columns[i].Visible)
                        {
                            ÚjSor[i] = sor.Cells[i].Value;
                        }
                    }
                    ÚjTábla.Rows.Add(ÚjSor);
                }
                if (színes)
                    DataTableToExcel(fájl, ÚjTábla, Tábla);
                else
                    DataTableToExcel(fájl, ÚjTábla);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "DataGridViewToExcel", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Elkoptatni

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

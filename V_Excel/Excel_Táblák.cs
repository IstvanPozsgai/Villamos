using ArrayToExcel;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using DataTable = System.Data.DataTable;

namespace Villamos
{
    public static partial class Module_Excel
    {
        // JAVÍTANDÓ:

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
                Szűrés("Munka1", "A", Oszlopnév(oszlop), sor + 1);    //szűrést felteszük

                //Nyomtatási terület kijelülése
                NyomtatásiTerület_részletes("Munka1", $"A1:{Oszlopnév(oszlop)}{sor + 1}", "$1:$1", "", true);

                //Beállunk az A1 cellába
                xlApp.Range["A1"].Select();

                ExcelMentés();
                ExcelBezárás();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"DataTableToExcel(fájl: {fájl})", ex.StackTrace, ex.Source, ex.HResult);
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
                int sor = 2;   // 1 sor a fejléc ami sárga
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
    }
}

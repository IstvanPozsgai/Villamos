using ArrayToExcel;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using DataTable = System.Data.DataTable;

namespace Villamos
{
    public static partial class MyClosedXML_Excel
    {
        //Táblaműveleteket gyűjti


        /// <summary>
        /// Ez a változat közvetlenül Adattáblából írja ki az adatokat
        /// és az ArrayToExcel könyvtárat használja
        /// </summary>
        /// <param name="Tábla"></param>
        /// <param name="fájl"></param>
        /// <param name="Elemek"></param>
        /// 

        public static void DataTableToXML(string fájl, DataTable Tábla, DataGridView TáblaGrid = null)
        {
            try
            {
                string munkalapnév = "Munka1";
                byte[] excel = Tábla.ToExcel(a => a.SheetName(munkalapnév));
                File.WriteAllBytes(fájl, excel);

                ///Megnyitjuk az Excel Formázásra
                ExcelMegnyitás(fájl);

                ////Utolsó oszlop és sor adatok
                oszlop = Tábla.Columns.Count;
                sor = Tábla.Rows.Count;

                Háttérszín(munkalapnév, $"A1:{Module_Excel.Oszlopnév(oszlop)}1", Color.Yellow); //Sárga háttér
                Betű(munkalapnév, $"A1:{Module_Excel.Oszlopnév(oszlop)}1", Color.Black);  //Fekete betű
                Betű(munkalapnév, $"A1:{Module_Excel.Oszlopnév(oszlop)}1", false, false, true); //vastag betű

                Rácsoz(munkalapnév, $"A1:{Module_Excel.Oszlopnév(oszlop)}{sor + 1}"); // rácsozás
                Oszlopszélesség(munkalapnév, $"A:{Module_Excel.Oszlopnév(oszlop)}");     //Automata Oszlop szélesség beállítás

                //     if (TáblaGrid != null) Színezés(TáblaGrid);

                Tábla_Rögzítés(munkalapnév, 1);  //Rögzítjük a fejlécet
                Szűrés(munkalapnév, "A", Module_Excel.Oszlopnév(oszlop), sor + 1);    //szűrést felteszük

                //Nyomtatási terület kijelülése
                NyomtatásiBeállítás NyBeállítás = new NyomtatásiBeállítás
                {
                    NyomtatásiTerület = $"A1:{Module_Excel.Oszlopnév(oszlop)}{sor + 1}",
                    Munkalap = munkalapnév,
                    IsmétlődőSorok = "$1:$1",
                    Álló = false,
                    LapSzéles = 1,
                    LáblécKözép = "&P/&N",
                    FejlécKözép = Program.PostásNév?.Trim() ?? "",
                    FejlécJobb = DateTime.Now.ToString("yyyy.MM.dd HH:mm")
                };
                NyomtatásiTerület_részletes(munkalapnév, NyBeállítás);

                ExcelMentés(fájl);
                ExcelBezárás();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"DataTableToExcel(fájl: {fájl})", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// DataGridView adatai alapján Excel táblát készít
        /// </summary>
        /// <param name="fájl">Mentési név</param>
        /// <param name="Tábla">DataGridView tábla</param>
        /// <param name="színes">DataGridView tábla színezését át akarjuk vinni az Excel táblába</param>
        public static void DataGridViewToXML(string fájl, DataGridView Tábla, bool színes = false)
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
                    DataTableToXML(fájl, ÚjTábla, Tábla);
                else
                    DataTableToXML(fájl, ÚjTábla);
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

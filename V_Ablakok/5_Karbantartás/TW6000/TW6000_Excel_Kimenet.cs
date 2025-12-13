using System.Drawing;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.V_Ablakok._5_Karbantartás.TW6000
{
    public class TW6000_Excel_Kimenet
    {
        readonly string munkalap = "Munka1";
        readonly Beállítás_Betű BeBetű = new Beállítás_Betű() { Méret = 12 };

        public void Excel_Kimenet(string fájlexc, DataGridView Táblaütemezés)
        {
            MyX.ExcelLétrehozás(munkalap);
            // megnyitjuk és kitöltjük az excel táblát

            MyX.Munkalap_betű(munkalap, BeBetű);

            // fejléc kiírása
            for (int oszlop = 0; oszlop < Táblaütemezés.ColumnCount; oszlop++)
            {
                MyX.Kiir(Táblaütemezés.Columns[oszlop].HeaderText, MyF.Oszlopnév(oszlop + 1) + "1");
                MyX.Háttérszín(munkalap, MyF.Oszlopnév(oszlop + 1) + "1", Color.Yellow);
                //Holtart.Lép();
            }

            // tartalom kiírása
            for (int sor = 0; sor < Táblaütemezés.RowCount; sor++)
            {
                Color ideigsor = Táblaütemezés.Rows[sor].DefaultCellStyle.BackColor;
                if (ideigsor.Name == "0") ideigsor = Color.White;
                MyX.Háttérszín(munkalap, $"A{sor + 2}:{MyF.Oszlopnév(Táblaütemezés.ColumnCount - 2)}{sor + 2}", ideigsor);

                int utolsoOszlopIndex = Táblaütemezés.ColumnCount - 1;
                int utolsoElottiOszlopIndex = Táblaütemezés.ColumnCount - 2;

                for (int oszlop = 0; oszlop < Táblaütemezés.ColumnCount; oszlop++)
                {
                    if (Táblaütemezés.Rows[sor].Cells[oszlop].Value != null)
                    {
                        bool utolsoKetOszlop = (oszlop == utolsoOszlopIndex || oszlop == utolsoElottiOszlopIndex);

                        if (utolsoKetOszlop)
                        {
                            MyX.Kiir($"#SZÁME#{Táblaütemezés.Rows[sor].Cells[oszlop].Value}", MyF.Oszlopnév(oszlop + 1) + (sor + 2).ToString());
                        }
                        else
                        {
                            MyX.Kiir(Táblaütemezés.Rows[sor].Cells[oszlop].Value.ToStrTrim(), MyF.Oszlopnév(oszlop + 1) + (sor + 2).ToString());
                        }


                        Color ideig = Táblaütemezés.Rows[sor].Cells[oszlop].Style.BackColor;
                        if (ideig.Name != "0")
                            MyX.Háttérszín(munkalap, MyF.Oszlopnév(oszlop + 1) + (sor + 2).ToString(), ideig);
                    }
                }
            }
            // megformázzuk
            int utolsóSor = Táblaütemezés.RowCount + 1;
            string utolsóOszlop = MyF.Oszlopnév(Táblaütemezés.ColumnCount);
            MyX.Rácsoz(munkalap, $"A2:B{utolsóSor}" );
            MyX.Rácsoz(munkalap, $"A1:B1" );
            string OszlopEleje = "C";
            for (int oszlop = 4; oszlop < Táblaütemezés.ColumnCount; oszlop++)
            {
                if (MyX.Beolvas(munkalap, MyF.Oszlopnév(oszlop)+"1") != "_")
                {
                    string OszlopVége = MyF.Oszlopnév(oszlop-1);
                    MyX.Rácsoz(munkalap, $"{OszlopEleje}2:{OszlopVége}{utolsóSor}");
                    MyX.Rácsoz(munkalap, $"{OszlopEleje}1:{OszlopVége}1");
                    OszlopEleje = MyF.Oszlopnév(oszlop );                   
                }
            }
            MyX.Rácsoz(munkalap, $"{OszlopEleje}2:{utolsóOszlop}{utolsóSor}");
            MyX.Rácsoz(munkalap, $"{OszlopEleje}1:{utolsóOszlop}1");

            MyX.Oszlopszélesség(munkalap, $"A:{utolsóOszlop}");

            Beállítás_Nyomtatás NyomtatásBeállít = new Beállítás_Nyomtatás()
            {
                Munkalap = munkalap,
                NyomtatásiTerület = $"A1:{utolsóOszlop}{utolsóSor}",
                BalMargó = 15,
                JobbMargó = 15,
                AlsóMargó = 20,
                FelsőMargó = 15,
                FejlécMéret = 13,
                LáblécMéret = 13,
                LapSzéles = 1,
                LapMagas = 1,
                Álló = false,
                VízKözép = true,
                FüggKözép = true
            };

            MyX.NyomtatásiTerület_részletes(munkalap, NyomtatásBeállít);

            // bezárjuk az Excel-t
            MyX.ExcelMentés(fájlexc);
            MyX.ExcelBezárás();

            MyF.Megnyitás(fájlexc);
        }
    }
}

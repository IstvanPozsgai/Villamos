using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;
using Villamos.Adatszerkezet;

namespace Villamos.V_Ablakok._5_Karbantartás.CAF_Ütemezés
{
    public class CAF_Elo_Havi_Excel
    {
   readonly      Beállítás_Betű BeBetű = new Beállítás_Betű { Név = "Calibri", Méret = 11 };
        public void Elo_havi_excel_keszit(string fájlexc, Kezelő_CAF_Szinezés KézSzín, DataGridView Tábla_elő, Action eloterveListazasExcelhezNegat)
        {
            // megnyitjuk az excelt
            string munkalap = "Munka1";

            MyX.ExcelLétrehozás();
            // *********************************
            // * Tartalom kezdete              *
            // *********************************
            MyX.Munkalap_betű(munkalap, BeBetű);

            DateTime ideigdátum;
            DateTime előzőHónap = new DateTime(1900, 1, 1);
            int szombat = 255;
            int vasárnap = 255;

            List<Adat_CAF_Szinezés> AdatokSzín = KézSzín.Lista_Adatok();
            Adat_CAF_Szinezés Szín = AdatokSzín.Where(a => a.Telephely == Tábla_elő.Rows[Tábla_elő.RowCount - 5].Cells[3].Value.ToStrTrim()).FirstOrDefault();
            if (Szín != null)
            {
                szombat = (int)Szín.Színszombat;
                vasárnap = (int)Szín.SzínVasárnap;
            }

            // Kiírjuk a dátumokat
            //Holtart.Be();
            for (int i = 0; i <= Tábla_elő.Rows.Count - 6; i++)
            {
                ideigdátum = DateTime.Parse(Tábla_elő.Rows[i].Cells[0].Value.ToString());
                MyX.Kiir($"#SZÁME#{ideigdátum:dd}", MyF.Oszlopnév(i + 2) + "2");
                MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(i + 2) + ":" + MyF.Oszlopnév(i + 2), 3);

                if (Tábla_elő.Rows[i].DefaultCellStyle.BackColor == Color.Beige) // Pihenőnap
                     MyX.Háttérszín(munkalap,MyF.Oszlopnév(i + 2) + "2:" + MyF.Oszlopnév(i + 2) + Tábla_elő.ColumnCount.ToString(), Color.FromArgb(szombat));

                if (Tábla_elő.Rows[i].DefaultCellStyle.BackColor == Color.BurlyWood)                    // vasárnap
                     MyX.Háttérszín(munkalap,MyF.Oszlopnév(i + 2) + "2:" + MyF.Oszlopnév(i + 2) + Tábla_elő.ColumnCount.ToString(), Color.FromArgb(vasárnap));

                if (Tábla_elő.Rows[i].DefaultCellStyle.BackColor == Color.IndianRed)                  // ünnep
                     MyX.Háttérszín(munkalap,MyF.Oszlopnév(i + 2) + "2:" + MyF.Oszlopnév(i + 2) + Tábla_elő.ColumnCount.ToString(), Color.FromArgb(vasárnap));

                //Holtart.Lép();
            }

            előzőHónap = DateTime.Parse(Tábla_elő.Rows[0].Cells[0].Value.ToString());
            int blokkeleje = 2;
            // hónap nevek kiírása

            for (int iii = 0; iii < Tábla_elő.Rows.Count - 6; iii++)
            {
                if (előzőHónap.ToString("yyyy MMM") != DateTime.Parse(Tábla_elő.Rows[iii].Cells[0].Value.ToString()).ToString("yyyy MMM"))
                {
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(blokkeleje) + "1:" + MyF.Oszlopnév(iii + 1) + "1");
                    MyX.Kiir(előzőHónap.ToString("yyyy MMM"), MyF.Oszlopnév(blokkeleje) + "1");
                    előzőHónap = DateTime.Parse(Tábla_elő.Rows[iii].Cells[0].Value.ToString());
                    blokkeleje = iii + 2;
                }
            }
            // utolsó hónap
            DateTime iidát = DateTime.Parse(Tábla_elő.Rows[Tábla_elő.Rows.Count - 6].Cells[0].Value.ToString());
            if (előzőHónap.ToString("yyyy MMM") == DateTime.Parse(Tábla_elő.Rows[Tábla_elő.Rows.Count - 6].Cells[0].Value.ToString()).ToString("yyyy MMM"))
            {
                MyX.Egyesít(munkalap, MyF.Oszlopnév(blokkeleje) + "1:" + MyF.Oszlopnév(Tábla_elő.Rows.Count - 4) + "1");
                MyX.Kiir(előzőHónap.ToString("yyyy MMM"), MyF.Oszlopnév(blokkeleje) + "1");
            }
            //Holtart.Lép();


            // kiírjuk  a pályaszámokat
            int sor = 3;
            int sormax;
            string pályaszám = "";
            int k = 0;

            MyX.Oszlopszélesség(munkalap, "a:a", 9);

            for (int ii = 3; ii < Tábla_elő.ColumnCount; ii++)
            {
                MyX.Kiir($"#SZÁME#{Tábla_elő.Columns[ii].HeaderText}", $"a{sor}");
                int PSZszín = 255;
                int PSZgarszín = 255;

                Szín = AdatokSzín.Where(a => a.Telephely == Tábla_elő.Rows[Tábla_elő.RowCount - 5].Cells[ii].Value.ToStrTrim()).FirstOrDefault();
                if (Szín != null)
                {
                    PSZszín = (int)Szín.SzínPsz;
                    PSZgarszín = (int)Szín.SzínPSZgar;
                }

                if (Tábla_elő.Rows[Tábla_elő.RowCount - 4].Cells[ii].Value.ToString().Trim() == "1")
                     MyX.Háttérszín(munkalap,"a" + sor.ToString(), Color.FromArgb(PSZgarszín));
                else
                     MyX.Háttérszín(munkalap,"a" + sor.ToString(), Color.FromArgb(PSZszín));

                sor += 1;
                //Holtart.Lép();
            }
            sormax = sor;


            // feltöltjük a vizsgálatokat

            for (k = 3; k <= sormax; k++)
            {
                pályaszám = MyX.Beolvas(munkalap,"a" + k.ToString()).Trim();
                for (int j = 1; j < Tábla_elő.Columns.Count; j++)
                {
                    // ha a két pályaszám egyezik
                    if (pályaszám.Trim() == Tábla_elő.Columns[j].HeaderText.Trim())
                    {
                        int isszín = 255;
                        int istűrésszín = 255;
                        int Pszín = 255;

                        Szín = AdatokSzín.Where(a => a.Telephely == Tábla_elő.Rows[Tábla_elő.RowCount - 5].Cells[j].Value.ToStrTrim()).FirstOrDefault();
                        if (Szín != null)
                        {
                            isszín = (int)Szín.SzínIS;
                            istűrésszín = (int)Szín.SzínIStűrés;
                            Pszín = (int)Szín.SzínP;

                        }
                        for (int i = 0; i < Tábla_elő.Rows.Count - 6; i++)
                        {
                            if (Tábla_elő.Rows[i].Cells[j].Value != null)
                            {
                                string szöveg = Tábla_elő.Rows[i].Cells[j].Value.ToString().Trim();
                                // ha a napi adatok között van vizsgálat akkor kiírjuk
                                if (szöveg != "")
                                {
                                    // ************
                                    // IS előterv 
                                    // ************
                                    if (szöveg == "/")
                                         MyX.Háttérszín(munkalap,MyF.Oszlopnév(i + 2) + k.ToString(), Color.FromArgb(istűrésszín));

                                    if (szöveg.Contains("IS") == true)
                                         MyX.Háttérszín(munkalap,MyF.Oszlopnév(i + 2) + k.ToString(), Color.FromArgb(isszín));

                                    if (szöveg.Contains("P") == true)
                                         MyX.Háttérszín(munkalap,MyF.Oszlopnév(i + 2) + k.ToString(), Color.FromArgb(Pszín));
                                }

                                if (szöveg != "/")
                                    MyX.Kiir(szöveg, MyF.Oszlopnév(i + 2) + k.ToString());
                            }
                        }
                    }
                }
            }
            //Holtart.Lép();

            int előzővége = 3;
            // beírjuk a képleteket

            for (k = 3; k <= sormax; k++)
            {
                pályaszám = MyX.Beolvas(munkalap,"a" + k.ToString()).Trim();
                if (pályaszám.Trim() == "_")
                {
                    for (int i = 0; i <= Tábla_elő.Rows.Count - 6; i++)
                        MyX.Kiir($"#KÉPLET#=COUNTIF(R[-{(k - előzővége)}]C:R[-1]C,\"*IS*\")+COUNTIF(R[-{(k - előzővége)}]C:R[-1]C,\"*P*\")", MyF.Oszlopnév(i + 2) + k.ToString());
                    k += 1;
                    előzővége = k + 1;
                }
                //Holtart.Lép();
            }
            // Berácsozzuk
            MyX.Vastagkeret(munkalap,"a1:" + MyF.Oszlopnév(Tábla_elő.Rows.Count - 4) + k.ToString());
            MyX.Rácsoz(munkalap,"a1:" + MyF.Oszlopnév(Tábla_elő.Rows.Count - 4) + k.ToString());

            // *********************************
            // * Tartalom vége                 *
            // *********************************

            // * Kiegészítő adatok eleje       *
            // *********************************
            eloterveListazasExcelhezNegat?.Invoke();      


            // feltöltjük a vizsgálatokat
            for (k = 3; k < Tábla_elő.ColumnCount; k++)
            {
                // beolvassuk az Excel táblából a pályaszámot

                for (int j = 1; j < Tábla_elő.Columns.Count; j++)
                {
                    pályaszám = MyX.Beolvas(munkalap,"a" + k.ToString()).Trim();
                    // ha a két pályaszám egyezik
                    if (pályaszám.Trim() == Tábla_elő.Columns[j].HeaderText.Trim())
                    {
                        // a színeket betöltjük
                        int Szín_E_v = 255;
                        int Szín_dollár_v = 255;
                        int Szín_Kukac_v = 255;
                        int Szín_Hasteg_v = 255;
                        int Szín_jog_v = 255;
                        int Szín_nagyobb_v = 255;

                        Szín = AdatokSzín.Where(a => a.Telephely == Tábla_elő.Rows[Tábla_elő.RowCount - 1].Cells[j].Value.ToStrTrim()).FirstOrDefault();
                        if (Szín != null)
                        {
                            Szín_E_v = (int)Szín.Szín_E;
                            Szín_dollár_v = (int)Szín.Szín_dollár;
                            Szín_Kukac_v = (int)Szín.Szín_Kukac;
                            Szín_Hasteg_v = (int)Szín.Szín_Hasteg;
                            Szín_jog_v = (int)Szín.Szín_jog;
                            Szín_nagyobb_v = (int)Szín.Szín_nagyobb;
                        }

                        // végig megyünk cellánként és ha van tartalma akkor kiírjuk, illetve színezzük
                        for (int i = 0; i < Tábla_elő.Rows.Count; i++)
                        {
                            if (Tábla_elő.Rows[i].Cells[j].Value != null)
                            {
                                if (Tábla_elő.Rows[i].Cells[j].Value.ToString().Trim() != "")
                                {

                                    switch (Tábla_elő.Rows[i].Cells[j].Value.ToString().Trim().Substring(0, 1))
                                    {
                                        case "E":
                                            {
                                                 MyX.Háttérszín(munkalap,MyF.Oszlopnév(i + 2) + k.ToString(), Color.FromArgb(Szín_E_v));
                                                break;
                                            }
                                        case "e":
                                            {
                                                 MyX.Háttérszín(munkalap,MyF.Oszlopnév(i + 2) + k.ToString(), Color.FromArgb(Szín_E_v));
                                                break;
                                            }
                                        case "$":
                                            {
                                                 MyX.Háttérszín(munkalap,MyF.Oszlopnév(i + 2) + k.ToString(), Color.FromArgb(Szín_dollár_v));
                                                break;
                                            }

                                        case "@":
                                            {
                                                 MyX.Háttérszín(munkalap,MyF.Oszlopnév(i + 2) + k.ToString(), Color.FromArgb(Szín_Kukac_v));
                                                break;
                                            }

                                        case "#":
                                            {
                                                 MyX.Háttérszín(munkalap,MyF.Oszlopnév(i + 2) + k.ToString(), Color.FromArgb(Szín_Hasteg_v));
                                                break;
                                            }

                                        case "§":
                                            {
                                                 MyX.Háttérszín(munkalap,MyF.Oszlopnév(i + 2) + k.ToString(), Color.FromArgb(Szín_jog_v));
                                                break;
                                            }

                                        case ">":
                                            {
                                                 MyX.Háttérszín(munkalap,MyF.Oszlopnév(i + 2) + k.ToString(), Color.FromArgb(Szín_nagyobb_v));
                                                break;
                                            }

                                    }
                                    string szöveg = Tábla_elő.Rows[i].Cells[j].Value.ToString().Trim();
                                    MyX.Kiir(szöveg, MyF.Oszlopnév(i + 2) + k.ToString());
                                }
                            }
                        }
                    }
                }
                //Holtart.Lép();
            }

            // bezárjuk az Excel-t
            MyX.Aktív_Cella(munkalap, "A1");
            MyX.ExcelMentés(fájlexc);
            MyX.ExcelBezárás();
        }
    }
}

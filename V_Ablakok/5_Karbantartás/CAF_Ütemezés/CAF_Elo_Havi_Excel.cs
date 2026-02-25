using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using MyColor = Villamos.V_MindenEgyéb.Kezelő_Szín;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;


namespace Villamos.V_Ablakok._5_Karbantartás.CAF_Ütemezés
{
    public class CAF_Elo_Havi_Excel
    {
        Szín_kódolás Szín;

        readonly Beállítás_Betű BeBetű = new Beállítás_Betű { Név = "Calibri", Méret = 11 };
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
            Szín_kódolás szombat = MyColor.Szín_váltó(255);
            Szín_kódolás vasárnap = MyColor.Szín_váltó(255);

            List<Adat_CAF_Szinezés> AdatokSzín = KézSzín.Lista_Adatok();
            Adat_CAF_Szinezés Szín = AdatokSzín.Where(a => a.Telephely == Tábla_elő.Rows[Tábla_elő.RowCount - 5].Cells[3].Value.ToStrTrim()).FirstOrDefault();
            if (Szín != null)
            {
                szombat = MyColor.Szín_váltó((long)Szín.Színszombat);
                vasárnap = MyColor.Szín_váltó((long)Szín.SzínVasárnap);
            }

            // Kiírjuk a dátumokat

            for (int i = 0; i <= Tábla_elő.Rows.Count - 6; i++)
            {
                ideigdátum = DateTime.Parse(Tábla_elő.Rows[i].Cells[0].Value.ToString());
                MyX.Kiir($"#SZÁME#{ideigdátum:dd}", MyF.Oszlopnév(i + 2) + "2");
                MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(i + 2) + ":" + MyF.Oszlopnév(i + 2), 3);

                if (Tábla_elő.Rows[i].DefaultCellStyle.BackColor == Color.Beige) // Pihenőnap
                    MyX.Háttérszín(munkalap, MyF.Oszlopnév(i + 2) + "2:" + MyF.Oszlopnév(i + 2) + Tábla_elő.ColumnCount.ToString(), Color.FromArgb(szombat.Piros, szombat.Zöld, szombat.Kék));

                if (Tábla_elő.Rows[i].DefaultCellStyle.BackColor == Color.BurlyWood) // vasárnap
                    MyX.Háttérszín(munkalap, MyF.Oszlopnév(i + 2) + "2:" + MyF.Oszlopnév(i + 2) + Tábla_elő.ColumnCount.ToString(), Color.FromArgb(vasárnap.Piros, vasárnap.Zöld, vasárnap.Kék));

                if (Tábla_elő.Rows[i].DefaultCellStyle.BackColor == Color.IndianRed) // ünnep
                    MyX.Háttérszín(munkalap, MyF.Oszlopnév(i + 2) + "2:" + MyF.Oszlopnév(i + 2) + Tábla_elő.ColumnCount.ToString(), Color.FromArgb(vasárnap.Piros, vasárnap.Zöld, vasárnap.Kék));
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

            // kiírjuk  a pályaszámokat
            int sor = 3;
            int sormax;
            string pályaszám = "";
            int k = 0;

            MyX.Oszlopszélesség(munkalap, "a:a", 9);

            for (int ii = 3; ii < Tábla_elő.ColumnCount; ii++)
            {
                MyX.Kiir($"#SZÁME#{Tábla_elő.Columns[ii].HeaderText}", $"a{sor}");
                Szín_kódolás PSZszín = MyColor.Szín_váltó(255);
                Szín_kódolás PSZgarszín = MyColor.Szín_váltó(255);

                Szín = AdatokSzín.Where(a => a.Telephely == Tábla_elő.Rows[Tábla_elő.RowCount - 5].Cells[ii].Value.ToStrTrim()).FirstOrDefault();
                if (Szín != null)
                {
                    PSZszín = MyColor.Szín_váltó((long)Szín.SzínPsz);
                    PSZgarszín = MyColor.Szín_váltó((long)Szín.SzínPSZgar);
                }

                if (Tábla_elő.Rows[Tábla_elő.RowCount - 4].Cells[ii].Value.ToStrTrim() == "1")
                    MyX.Háttérszín(munkalap, "a" + sor.ToString(), Color.FromArgb(PSZgarszín.Piros, PSZgarszín.Zöld, PSZgarszín.Kék));
                else
                    MyX.Háttérszín(munkalap, "a" + sor.ToString(), Color.FromArgb(PSZszín.Piros, PSZszín.Zöld, PSZszín.Kék));

                sor += 1;
            }
            sormax = sor;


            // feltöltjük a vizsgálatokat

            for (k = 3; k <= sormax; k++)
            {
                pályaszám = MyX.Beolvas(munkalap, "a" + k.ToString()).Trim();
                for (int j = 1; j < Tábla_elő.Columns.Count; j++)
                {
                    // ha a két pályaszám egyezik
                    if (pályaszám.Trim() == Tábla_elő.Columns[j].HeaderText.Trim())
                    {

                        Szín_kódolás isszín = MyColor.Szín_váltó(255);
                        Szín_kódolás istűrésszín = MyColor.Szín_váltó(255);
                        Szín_kódolás Pszín = MyColor.Szín_váltó(255);


                        Szín = AdatokSzín.Where(a => a.Telephely == Tábla_elő.Rows[Tábla_elő.RowCount - 5].Cells[j].Value.ToStrTrim()).FirstOrDefault();
                        if (Szín != null)
                        {

                            isszín = MyColor.Szín_váltó((long)Szín.SzínIS);
                            istűrésszín = MyColor.Szín_váltó((int)Szín.SzínIStűrés);
                            Pszín = MyColor.Szín_váltó((int)Szín.SzínP);

                        }
                        for (int i = 0; i < Tábla_elő.Rows.Count - 6; i++)
                        {
                            if (Tábla_elő.Rows[i].Cells[j].Value != null)
                            {
                                string szöveg = Tábla_elő.Rows[i].Cells[j].Value.ToStrTrim();
                                // ha a napi adatok között van vizsgálat akkor kiírjuk
                                if (szöveg != "")
                                {
                                    // ************
                                    // IS előterv 
                                    // ************
                                    if (szöveg == "/")
                                        MyX.Háttérszín(munkalap, MyF.Oszlopnév(i + 2) + k.ToString(), Color.FromArgb(istűrésszín.Piros, istűrésszín.Zöld, istűrésszín.Kék));

                                    if (szöveg.Contains("IS") == true)
                                        MyX.Háttérszín(munkalap, MyF.Oszlopnév(i + 2) + k.ToString(), Color.FromArgb(isszín.Piros, isszín.Zöld, isszín.Kék));

                                    if (szöveg.Contains("P") == true)
                                        MyX.Háttérszín(munkalap, MyF.Oszlopnév(i + 2) + k.ToString(), Color.FromArgb(Pszín.Piros, Pszín.Zöld, Pszín.Kék));
                                }

                                if (szöveg != "/")
                                    MyX.Kiir(szöveg, MyF.Oszlopnév(i + 2) + k.ToString());
                            }
                        }
                    }
                }
            }

            int előzővége = 3;
            // beírjuk a képleteket

            for (k = 3; k <= sormax; k++)
            {
                pályaszám = MyX.Beolvas(munkalap, "a" + k.ToString()).Trim();
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
            MyX.Vastagkeret(munkalap, "a1:" + MyF.Oszlopnév(Tábla_elő.Rows.Count - 4) + k.ToString());
            MyX.Rácsoz(munkalap, "a1:" + MyF.Oszlopnév(Tábla_elő.Rows.Count - 4) + k.ToString());

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
                    pályaszám = MyX.Beolvas(munkalap, "a" + k.ToString()).Trim();
                    // ha a két pályaszám egyezik
                    if (pályaszám.Trim() == Tábla_elő.Columns[j].HeaderText.Trim())
                    {
                        // a színeket betöltjük
                        Szín_kódolás Szín_E_v = MyColor.Szín_váltó(255);
                        Szín_kódolás Szín_dollár_v = MyColor.Szín_váltó(255);
                        Szín_kódolás Szín_Kukac_v = MyColor.Szín_váltó(255);
                        Szín_kódolás Szín_Hasteg_v = MyColor.Szín_váltó(255);
                        Szín_kódolás Szín_jog_v = MyColor.Szín_váltó(255);
                        Szín_kódolás Szín_nagyobb_v = MyColor.Szín_váltó(255);

                        Szín = AdatokSzín.Where(a => a.Telephely == Tábla_elő.Rows[Tábla_elő.RowCount - 1].Cells[j].Value.ToStrTrim()).FirstOrDefault();
                        if (Szín != null)
                        {
                            Szín_E_v = MyColor.Szín_váltó((long)Szín.Szín_E);
                            Szín_dollár_v = MyColor.Szín_váltó((long)Szín.Szín_dollár);
                            Szín_Kukac_v = MyColor.Szín_váltó((long)Szín.Szín_Kukac);
                            Szín_Hasteg_v = MyColor.Szín_váltó((long)Szín.Szín_Hasteg);
                            Szín_jog_v = MyColor.Szín_váltó((long)Szín.Szín_jog);
                            Szín_nagyobb_v = MyColor.Szín_váltó((long)Szín.Szín_nagyobb);
                        }

                        // végig megyünk cellánként és ha van tartalma akkor kiírjuk, illetve színezzük
                        for (int i = 0; i < Tábla_elő.Rows.Count; i++)
                        {
                            if (Tábla_elő.Rows[i].Cells[j].Value != null)
                            {
                                if (Tábla_elő.Rows[i].Cells[j].Value.ToStrTrim() != "")
                                {

                                    switch (Tábla_elő.Rows[i].Cells[j].Value.ToStrTrim().Substring(0, 1))
                                    {
                                        case "E":
                                            {
                                                MyX.Háttérszín(munkalap, MyF.Oszlopnév(i + 2) + k.ToString(), Color.FromArgb(Szín_E_v.Piros, Szín_E_v.Zöld, Szín_E_v.Kék));  // Szín_E_v
                                                break;
                                            }
                                        case "e":
                                            {
                                                MyX.Háttérszín(munkalap, MyF.Oszlopnév(i + 2) + k.ToString(), Color.FromArgb(Szín_E_v.Piros, Szín_E_v.Zöld, Szín_E_v.Kék));  // Szín_E_v
                                                break;
                                            }
                                        case "$":
                                            {
                                                MyX.Háttérszín(munkalap, MyF.Oszlopnév(i + 2) + k.ToString(), Color.FromArgb(Szín_dollár_v.Piros, Szín_dollár_v.Zöld, Szín_dollár_v.Kék));  // Szín_dollár_v
                                                break;
                                            }

                                        case "@":
                                            {
                                                MyX.Háttérszín(munkalap, MyF.Oszlopnév(i + 2) + k.ToString(), Color.FromArgb(Szín_Kukac_v.Piros, Szín_Kukac_v.Zöld, Szín_Kukac_v.Kék));   // Szín_Kukac_v
                                                break;
                                            }

                                        case "#":
                                            {
                                                MyX.Háttérszín(munkalap, MyF.Oszlopnév(i + 2) + k.ToString(), Color.FromArgb(Szín_Hasteg_v.Piros, Szín_Hasteg_v.Zöld, Szín_Hasteg_v.Kék));  // Szín_Hasteg_v
                                                break;
                                            }

                                        case "§":
                                            {
                                                MyX.Háttérszín(munkalap, MyF.Oszlopnév(i + 2) + k.ToString(), Color.FromArgb(Szín_jog_v.Piros, Szín_jog_v.Zöld, Szín_jog_v.Kék));  // Szín_jog_v
                                                break;
                                            }

                                        case ">":
                                            {
                                                MyX.Háttérszín(munkalap, MyF.Oszlopnév(i + 2) + k.ToString(), Color.FromArgb(Szín_nagyobb_v.Piros, Szín_nagyobb_v.Zöld, Szín_nagyobb_v.Kék)); //Szín_nagyobb_v
                                                break;
                                            }

                                    }
                                    string szöveg = Tábla_elő.Rows[i].Cells[j].Value.ToStrTrim();
                                    MyX.Kiir(szöveg, MyF.Oszlopnév(i + 2) + k.ToString());
                                }
                            }
                        }
                    }
                }
            }

            // bezárjuk az Excel-t
            MyX.Aktív_Cella(munkalap, "A1");
            MyX.ExcelMentés(fájlexc);
            MyX.ExcelBezárás();
        }
    }
}

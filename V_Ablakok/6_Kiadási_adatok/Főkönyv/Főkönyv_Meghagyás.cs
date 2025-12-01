using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos_Nyomtatványok
{
    public class Főkönyv_Meghagyás
    {
        // JAVÍTANDÓ:
        public void Főkönyv_Meghagyáskészítés(string fájlexc, string Cmbtelephely, DateTime Dátum, string papírméret, string papírelrendezés)
        {
            try
            {
                MyE.ExcelLétrehozás();
                string munkalap = "Munka1";

                // egész tábla betűméret
                MyE.Munkalap_betű("Calibri", 11);

                // oszlop szélesség
                MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(1) + ":" + MyE.Oszlopnév(30), 6);
                MyE.Oszlopszélesség(munkalap, "d:d", 2);
                MyE.Oszlopszélesség(munkalap, "h:h", 2);
                MyE.Oszlopszélesség(munkalap, "l:l", 2);
                MyE.Oszlopszélesség(munkalap, "r:r", 10);
                // vastag keret
                MyE.Vastagkeret("a1:ad1");

                MyE.Betű("e1", 18);
                MyE.Betű("t1", 18);
                MyE.Betű("e1", false, false, true);
                MyE.Betű("t1", false, false, true);
                MyE.Kiir(DateTime.Today.ToString("yyyy.MM.dd dddd"), "e1");
                MyE.Egyesít(munkalap, "e1:k1");
                MyE.Kiir(DateTime.Today.ToString("yyyy.MM.dd dddd"), "t1");
                MyE.Egyesít(munkalap, "t1:z1");
                MyE.Kiir("Csatolások:", "q20");
                MyE.Betű("q20", false, false, true);
                MyE.Kiir("Szétcsatolások:", "y20");
                MyE.Betű("y20", false, false, true);
                //
                // frissítjük a táblát
                // elkészítjük a formanyomtatványt
                Főkönyv_Funkciók.Napiállók(Cmbtelephely.Trim());
                // kiirjuk a V2-t
                string jelszó = "pozsgaii";
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\villamos\Új_napihiba.mdb";
                string szöveg = "SELECT * FROM hiba order by azonosító asc";

                Kezelő_Nap_Hiba KNH_kéz = new Kezelő_Nap_Hiba();
                List<Adat_Nap_Hiba> Adatok = KNH_kéz.Lista_adatok(hely, jelszó, szöveg);

                int sor = 2;
                int oszlop = 17;
                Kiirja_Karb_("V3", Adatok, sor, oszlop);

                sor += 2;
                Kiirja_Karb_("V2", Adatok, sor, oszlop);

                sor += 2;
                Kiirja_Karb_("V1", Adatok, sor, oszlop);


                sor += 2;
                Kiirja_Karb_("E3", Adatok, sor, oszlop);


                // vizsgálatra maradjon benn


                // csoportosításhoz alaphelyzetbe állítjuk a váltózókat
                List<string> csoportpsz = new List<string>();


                foreach (Adat_Nap_Hiba rekord in Adatok)
                {
                    if (rekord.Üzemképtelen.ToUpper().Contains("E3") || rekord.Üzemképtelen.ToUpper().Contains("V1"))
                    {
                        csoportpsz.Add(rekord.Azonosító.Trim());
                    }
                }

                // benn maradók csoportba kiirása
                oszlop = 20;
                sor += 2;
                MyE.Kiir("Vizsgálatra maradjon:", MyE.Oszlopnév(17) + $"{sor}");
                MyE.Betű(MyE.Oszlopnév(17) + $"{sor}", false, false, true);

                for (int j = 0; j < csoportpsz.Count; j++)
                {

                    if (csoportpsz[j].Trim() != "")
                    {
                        MyE.Kiir(csoportpsz[j].Trim(), MyE.Oszlopnév(oszlop) + $"{sor}");

                        oszlop += 1;
                        if (oszlop > 28)
                        {
                            oszlop = 21;
                            sor += 1;
                        }
                    }

                }
                // mosás bennmarad
                sor += 2;

                // csoportosításhoz alaphelyzetbe állítjuk a váltózókat
                csoportpsz.Clear();


                szöveg = "SELECT * FROM hiba order by azonosító asc";
                Adatok = KNH_kéz.Lista_adatok(hely, jelszó, szöveg);


                foreach (Adat_Nap_Hiba rekord in Adatok)
                {
                    if (rekord.Üzemképtelen.ToUpper().Contains("MOSÓ"))
                    {
                        csoportpsz.Add(rekord.Azonosító.Trim());
                    }
                }


                // a mosók kiirása
                MyE.Kiir("Mosásra maradjon:", MyE.Oszlopnév(17) + $"{sor}");
                MyE.Betű(MyE.Oszlopnév(17) + $"{sor}", false, false, true);

                oszlop = 20;
                for (int j = 0; j < csoportpsz.Count; j++)
                {
                    if (csoportpsz[j].Trim() != "")
                    {
                        MyE.Kiir(csoportpsz[j], MyE.Oszlopnév(oszlop) + $"{sor}");
                        oszlop += 1;
                        if (oszlop > 28)
                        {
                            oszlop = 19;
                            sor += 1;
                        }
                    }

                }
                // mosás beálló
                // csoportosításhoz alaphelyzetbe állítjuk a váltózókat
                csoportpsz.Clear();

                foreach (Adat_Nap_Hiba rekord in Adatok)
                {
                    if (rekord.Beálló.ToUpper().Contains("MOSÓ"))
                    {
                        csoportpsz.Add(rekord.Azonosító.Trim());
                    }
                }

                // a mosók kiirása
                sor += 2;
                MyE.Kiir("Mosás:", MyE.Oszlopnév(17) + $"{sor}");
                MyE.Betű(MyE.Oszlopnév(17) + $"{sor}", false, false, true);

                oszlop = 19;
                for (int j = 0; j < csoportpsz.Count; j++)
                {

                    if (csoportpsz[j].Trim() != "")
                    {
                        MyE.Kiir(csoportpsz[j], MyE.Oszlopnév(oszlop) + $"{sor}");
                        oszlop += 1;
                        if (oszlop > 28)
                        {
                            oszlop = 19;
                            sor += 1;
                        }
                    }

                }

                // hibák
                oszlop = 1;
                int sorúj = 2;
                MyE.Kiir("Hibák:", MyE.Oszlopnév(1) + sorúj.ToString());
                MyE.Betű(MyE.Oszlopnév(1) + sorúj.ToString(), false, false, true);
                sorúj += 1;


                string szöveg1;
                foreach (Adat_Nap_Hiba rekord in Adatok)
                {

                    sorúj += 1;
                    if (rekord.Státus == 3)
                    {
                        MyE.Kiir("*" + rekord.Azonosító.Trim(), MyE.Oszlopnév(1) + sorúj.ToString());
                    }
                    else
                    {
                        MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(1) + sorúj.ToString());
                    }
                    if (rekord.Státus == 4)
                    {
                        MyE.Betű(MyE.Oszlopnév(1) + sorúj.ToString(), true, false, true);
                    }
                    MyE.Kiir(rekord.Típus.Trim(), MyE.Oszlopnév(2) + sorúj.ToString());
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(2) + sorúj.ToString() + ":" + MyE.Oszlopnév(3) + sorúj.ToString());
                    szöveg1 = "";
                    if (rekord.Üzemképtelen.Trim() != "_")
                    {
                        szöveg1 = rekord.Üzemképtelen.Trim();
                    }
                    if (rekord.Beálló.Trim() != "_")
                    {
                        szöveg1 += "+" + rekord.Beálló.Trim();
                    }
                    if (rekord.Üzemképeshiba.Trim() != "_")
                    {
                        szöveg1 += "+" + rekord.Üzemképeshiba.Trim();
                    }
                    szöveg1 = szöveg1.Length > 100 ? szöveg1.Substring(0, 100) : szöveg1;
                    MyE.Kiir(szöveg1, MyE.Oszlopnév(4) + sorúj.ToString());
                }



                // összecsatolások
                // megnézzük, hogy van-e adott szerelvény napló
                hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\hibanapló\" + Dátum.ToString("yyyyMM") + "szerelvény.mdb";

                // ha van akkor kiirjuk
                if (System.IO.File.Exists(hely))
                {
                    oszlop = 17;

                    szöveg = "SELECT * FROM szerelvénytáblanapló where [szerelvényhossz]>0 and [mikor]> # " + Dátum.ToString("MM-dd-yyyy") + " #";
                    szöveg += " order by mikor";

                    long szerelvény = 0;
                    sor = 22;
                    Kezelő_Szerelvény_Napló KSZN_kéz = new Kezelő_Szerelvény_Napló();
                    List<Adat_Szerelvény_Napló> SzAdatok = KSZN_kéz.Lista_Adatok(hely, jelszó, szöveg);
                    if (SzAdatok != null)
                    {
                        foreach (Adat_Szerelvény_Napló rekord in SzAdatok)
                        {
                            if (rekord.Kocsi2.Trim() != "0")
                            {
                                // ha a szerelvény id nem egyezik akkor sort emel
                                if (szerelvény != rekord.ID && szerelvény != 0)
                                    sor++;
                                if (rekord.Kocsi1.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi1, MyE.Oszlopnév(oszlop) + $"{sor}");
                                if (rekord.Kocsi2.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi2, MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                                if (rekord.Kocsi3.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi3, MyE.Oszlopnév(oszlop + 2) + $"{sor}");
                                if (rekord.Kocsi4.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi4, MyE.Oszlopnév(oszlop + 3) + $"{sor}");
                                if (rekord.Kocsi5.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi5, MyE.Oszlopnév(oszlop + 4) + $"{sor}");
                                if (rekord.Kocsi6.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi6, MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                                szerelvény = rekord.ID;
                            }
                        }
                    }

                    // Szétcsatolások
                    oszlop = 25;
                    sor = 22;

                    szöveg = "SELECT * FROM szerelvénytáblanapló where [szerelvényhossz]=0 and [mikor]> # " + Dátum.ToString("MM-dd-yyyy") + " #";
                    szöveg += " order by mikor";

                    SzAdatok = KSZN_kéz.Lista_Adatok(hely, jelszó, szöveg);
                    if (SzAdatok != null)
                    {
                        foreach (Adat_Szerelvény_Napló rekord in SzAdatok)
                        {
                            // ha a második kocsi van akkor kírja a 0-kat
                            if (rekord.Kocsi2.Trim() != "0")
                            {
                                if (rekord.Kocsi1.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi1.Trim(), MyE.Oszlopnév(oszlop) + $"{sor}");
                                if (rekord.Kocsi2.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi2.Trim(), MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                                if (rekord.Kocsi3.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi3.Trim(), MyE.Oszlopnév(oszlop + 2) + $"{sor}");
                                if (rekord.Kocsi4.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi4.Trim(), MyE.Oszlopnév(oszlop + 3) + $"{sor}");
                                if (rekord.Kocsi5.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi5.Trim(), MyE.Oszlopnév(oszlop + 4) + $"{sor}");
                                if (rekord.Kocsi6.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi6.Trim(), MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                                sor += 1;
                            }
                        }
                    }
                }
                if (sorúj >= sor)
                    sor = sorúj;
                // vastag keret
                MyE.Vastagkeret("a1:ad" + $"{sor}");
                // nyomtatási beállítások
                bool papírelrendez;
                if (papírelrendezés == "--")
                    papírelrendez = false;
                else if (papírelrendezés == "Álló")
                    papírelrendez = true;
                else
                    papírelrendez = false;
                if (papírméret == "--") papírméret = "A3";

                MyE.NyomtatásiTerület_részletes(munkalap, "a1:ad" + $"{sor}", 5, 5, 5, 5,
                    5, 5, "1", "1", papírelrendez, papírméret, true, true);

                // bezárjuk az Excel-t
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                MyE.Megnyitás(fájlexc);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);

            }
        }
        void Kiirja_Karb_(string Karbantartás, List<Adat_Nap_Hiba> Adatok, int sor, int oszlop)
        {
            if (Adatok.Count > 0)
            {
                MyE.Kiir(Karbantartás, MyE.Oszlopnév(oszlop) + $"{sor}");
                MyE.Betű(MyE.Oszlopnév(oszlop) + $"{sor}", false, false, true);
                foreach (Adat_Nap_Hiba rekord in Adatok)
                {
                    // kiirjuk a v3-t
                    if (rekord.Üzemképtelen.ToUpper().Contains(Karbantartás))
                    {
                        oszlop += 1;
                        if (oszlop == 31)
                        {
                            oszlop = 18;
                            sor += 1;
                        }
                        MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(oszlop) + $"{sor}");
                        MyE.Betű(MyE.Oszlopnév(oszlop) + $"{sor}", true, false, true);
                    }
                    // kiirjuk a v3-t
                    if (rekord.Beálló.ToUpper().Contains(Karbantartás))
                    {
                        oszlop += 1;
                        if (oszlop == 31)
                        {
                            oszlop = 18;
                            sor += 1;
                        }
                        MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(oszlop) + $"{sor}");
                    }
                }
            }
        }
    }
}

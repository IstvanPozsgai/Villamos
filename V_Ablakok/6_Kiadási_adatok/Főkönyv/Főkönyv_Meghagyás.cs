using System;
using System.Collections.Generic;
using System.Linq;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.Villamos_Nyomtatványok
{
    public class Főkönyv_Meghagyás
    {
        readonly Kezelő_Nap_Hiba KNH_kéz = new Kezelő_Nap_Hiba();
        readonly Kezelő_Szerelvény_Napló KSZN_kéz = new Kezelő_Szerelvény_Napló();

        readonly Beállítás_Betű BeBetű = new Beállítás_Betű { Méret = 11, Név = "Calibri" };
        readonly Beállítás_Betű BeBetűAV = new Beállítás_Betű { Méret = 11, Név = "Calibri", Vastag = true, Aláhúzott = true };
        readonly Beállítás_Betű BeBetűV = new Beállítás_Betű { Méret = 11, Név = "Calibri", Vastag = true };
        readonly Beállítás_Betű BeBetű18V = new Beállítás_Betű { Méret = 18, Név = "Calibri", Vastag = true };

        readonly string munkalap = "Munka1";
        public void Főkönyv_Meghagyáskészítés(string fájlexc, string Cmbtelephely, DateTime Dátum, string papírméret, string papírelrendezés)
        {
            try
            {
                MyX.ExcelLétrehozás(munkalap);
                // egész tábla betűméret
                MyX.Munkalap_betű(munkalap, BeBetű);

                // oszlop szélesség
                MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(1) + ":" + MyF.Oszlopnév(30), 6);
                MyX.Oszlopszélesség(munkalap, "d:d", 2);
                MyX.Oszlopszélesség(munkalap, "h:h", 2);
                MyX.Oszlopszélesség(munkalap, "l:l", 2);
                MyX.Oszlopszélesség(munkalap, "r:r", 10);
                // vastag keret
                MyX.Vastagkeret(munkalap, "a1:ad1");


                MyX.Betű(munkalap, "e1", BeBetű18V);
                MyX.Betű(munkalap, "t1", BeBetű18V);
                MyX.Kiir(DateTime.Today.ToString("yyyy.MM.dd dddd"), "e1");
                MyX.Egyesít(munkalap, "e1:k1");
                MyX.Kiir(DateTime.Today.ToString("yyyy.MM.dd dddd"), "t1");
                MyX.Egyesít(munkalap, "t1:z1");
                MyX.Kiir("Csatolások:", "q20");
                MyX.Betű(munkalap, "q20", BeBetűV);
                MyX.Kiir("Szétcsatolások:", "y20");
                MyX.Betű(munkalap, "y20", BeBetűV);
                //
                // frissítjük a táblát
                // elkészítjük a formanyomtatványt
                Főkönyv_Funkciók.Napiállók(Cmbtelephely.Trim());
                // kiirjuk a V2-t
                List<Adat_Nap_Hiba> Adatok = KNH_kéz.Lista_Adatok(Cmbtelephely.Trim());
                Adatok = Adatok.OrderBy(y => y.Azonosító).ToList();
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
                MyX.Kiir("Vizsgálatra maradjon:", MyF.Oszlopnév(17) + $"{sor}");
                MyX.Betű(munkalap, MyF.Oszlopnév(17) + $"{sor}", BeBetűV);

                for (int j = 0; j < csoportpsz.Count; j++)
                {

                    if (csoportpsz[j].Trim() != "")
                    {
                        MyX.Kiir(csoportpsz[j].Trim(), MyF.Oszlopnév(oszlop) + $"{sor}");

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

                foreach (Adat_Nap_Hiba rekord in Adatok)
                {
                    if (rekord.Üzemképtelen.ToUpper().Contains("MOSÓ"))
                    {
                        csoportpsz.Add(rekord.Azonosító.Trim());
                    }
                }


                // a mosók kiirása
                MyX.Kiir("Mosásra maradjon:", MyF.Oszlopnév(17) + $"{sor}");
                MyX.Betű(munkalap, MyF.Oszlopnév(17) + $"{sor}", BeBetűV);

                oszlop = 20;
                for (int j = 0; j < csoportpsz.Count; j++)
                {
                    if (csoportpsz[j].Trim() != "")
                    {
                        MyX.Kiir(csoportpsz[j], MyF.Oszlopnév(oszlop) + $"{sor}");
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
                MyX.Kiir("Mosás:", MyF.Oszlopnév(17) + $"{sor}");
                MyX.Betű(munkalap, MyF.Oszlopnév(17) + $"{sor}", BeBetűV);

                oszlop = 19;
                for (int j = 0; j < csoportpsz.Count; j++)
                {

                    if (csoportpsz[j].Trim() != "")
                    {
                        MyX.Kiir(csoportpsz[j], MyF.Oszlopnév(oszlop) + $"{sor}");
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
                MyX.Kiir("Hibák:", MyF.Oszlopnév(1) + sorúj.ToString());
                MyX.Betű(munkalap, MyF.Oszlopnév(1) + sorúj.ToString(), BeBetűV);
                sorúj += 1;


                string szöveg1;
                foreach (Adat_Nap_Hiba rekord in Adatok)
                {

                    sorúj += 1;
                    if (rekord.Státus == 3)
                    {
                        MyX.Kiir("*" + rekord.Azonosító.Trim(), MyF.Oszlopnév(1) + sorúj.ToString());
                    }
                    else
                    {
                        MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(1) + sorúj.ToString());
                    }
                    if (rekord.Státus == 4)
                    {
                        MyX.Betű(munkalap, MyF.Oszlopnév(1) + sorúj.ToString(), BeBetűAV);
                    }
                    MyX.Kiir(rekord.Típus.Trim(), MyF.Oszlopnév(2) + sorúj.ToString());
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(2) + sorúj.ToString() + ":" + MyF.Oszlopnév(3) + sorúj.ToString());
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
                    MyX.Kiir(szöveg1, MyF.Oszlopnév(4) + sorúj.ToString());
                }



                // összecsatolások
                // megnézzük, hogy van-e adott szerelvény napló

                List<Adat_Szerelvény_Napló> SzAdatokÖ = KSZN_kéz.Lista_Adatok(Cmbtelephely.Trim(), Dátum);

                // ha van akkor kiirjuk
                if (SzAdatokÖ != null)
                {
                    List<Adat_Szerelvény_Napló> SzAdatok = (from a in SzAdatokÖ
                                                            where a.Szerelvényhossz > 0 &&
                                                            a.Mikor > Dátum
                                                            orderby a.Mikor
                                                            select a).ToList();
                    oszlop = 17;
                    long szerelvény = 0;
                    sor = 22;

                    foreach (Adat_Szerelvény_Napló rekord in SzAdatok)
                    {
                        if (rekord.Kocsi2.Trim() != "0")
                        {
                            // ha a szerelvény id nem egyezik akkor sort emel
                            if (szerelvény != rekord.ID && szerelvény != 0)
                                sor++;
                            if (rekord.Kocsi1.Trim() != "0")
                                MyX.Kiir(rekord.Kocsi1, MyF.Oszlopnév(oszlop) + $"{sor}");
                            if (rekord.Kocsi2.Trim() != "0")
                                MyX.Kiir(rekord.Kocsi2, MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                            if (rekord.Kocsi3.Trim() != "0")
                                MyX.Kiir(rekord.Kocsi3, MyF.Oszlopnév(oszlop + 2) + $"{sor}");
                            if (rekord.Kocsi4.Trim() != "0")
                                MyX.Kiir(rekord.Kocsi4, MyF.Oszlopnév(oszlop + 3) + $"{sor}");
                            if (rekord.Kocsi5.Trim() != "0")
                                MyX.Kiir(rekord.Kocsi5, MyF.Oszlopnév(oszlop + 4) + $"{sor}");
                            if (rekord.Kocsi6.Trim() != "0")
                                MyX.Kiir(rekord.Kocsi6, MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                            szerelvény = rekord.ID;
                        }
                    }


                    // Szétcsatolások
                    oszlop = 25;
                    sor = 22;
                    SzAdatok = (from a in SzAdatokÖ
                                where a.Szerelvényhossz == 0 &&
                                a.Mikor > Dátum
                                orderby a.Mikor
                                select a).ToList();
                    if (SzAdatok != null)
                    {
                        foreach (Adat_Szerelvény_Napló rekord in SzAdatok)
                        {
                            // ha a második kocsi van akkor kírja a 0-kat
                            if (rekord.Kocsi2.Trim() != "0")
                            {
                                if (rekord.Kocsi1.Trim() != "0")
                                    MyX.Kiir(rekord.Kocsi1.Trim(), MyF.Oszlopnév(oszlop) + $"{sor}");
                                if (rekord.Kocsi2.Trim() != "0")
                                    MyX.Kiir(rekord.Kocsi2.Trim(), MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                                if (rekord.Kocsi3.Trim() != "0")
                                    MyX.Kiir(rekord.Kocsi3.Trim(), MyF.Oszlopnév(oszlop + 2) + $"{sor}");
                                if (rekord.Kocsi4.Trim() != "0")
                                    MyX.Kiir(rekord.Kocsi4.Trim(), MyF.Oszlopnév(oszlop + 3) + $"{sor}");
                                if (rekord.Kocsi5.Trim() != "0")
                                    MyX.Kiir(rekord.Kocsi5.Trim(), MyF.Oszlopnév(oszlop + 4) + $"{sor}");
                                if (rekord.Kocsi6.Trim() != "0")
                                    MyX.Kiir(rekord.Kocsi6.Trim(), MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                                sor += 1;
                            }
                        }
                    }
                }
                if (sorúj >= sor)
                    sor = sorúj;
                // vastag keret
                MyX.Vastagkeret(munkalap, "a2:ad" + $"{sor}");
                // nyomtatási beállítások
                bool papírelrendez = true;
                if (papírelrendezés != "Álló") papírelrendez = false;

                if (papírméret == "--") papírméret = "A3";
                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = "a1:ad" + $"{sor}",
                    Álló = papírelrendez,
                    Papírméret = papírméret,
                    VízKözép = true,
                    FüggKözép = true,
                    LapMagas = 1,
                    LapSzéles = 1,
                    BalMargó = 5,
                    JobbMargó = 5,
                    FelsőMargó = 5,
                    AlsóMargó = 5,
                    LáblécMéret = 5,
                    FejlécMéret = 5
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);
                // bezárjuk az Excel-t
                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();

                MyF.Megnyitás(fájlexc);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);

            }
        }
        private void Kiirja_Karb_(string Karbantartás, List<Adat_Nap_Hiba> Adatok, int sor, int oszlop)
        {
            if (Adatok.Count > 0)
            {
                MyX.Kiir(Karbantartás, MyF.Oszlopnév(oszlop) + $"{sor}");
                MyX.Betű(munkalap, MyF.Oszlopnév(oszlop) + $"{sor}", BeBetűV);
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
                        MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(oszlop) + $"{sor}");
                        MyX.Betű(munkalap, MyF.Oszlopnév(oszlop) + $"{sor}", BeBetűAV);
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
                        MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(oszlop) + $"{sor}");
                    }
                }
            }
        }
    }
}

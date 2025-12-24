using System;
using System.Collections.Generic;
using System.Linq;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.Villamos_Nyomtatványok
{
    public class Főkönyv_Háromnapos
    {
        readonly Kezelő_jármű_hiba KézJárműHiba = new Kezelő_jármű_hiba();
        readonly Kezelő_Jármű2 KézJármű = new Kezelő_Jármű2();
        readonly Beállítás_Betű BeBetű = new Beállítás_Betű();
        readonly Beállítás_Betű BeBetűV = new Beállítás_Betű { Vastag = true };
        readonly Beállítás_Betű BeBetűD = new Beállítás_Betű { Dőlt = true };

        public void Három_Nyomtatvány(string fájlneve, string Cmbtelephely, string papírméret, string papírelrendezés)
        {
            MyX.ExcelLétrehozás("Hétfő-Csütörtök");
            MyX.Munkalap_Új("Kedd-Péntek");
            MyX.Munkalap_Új("Szerda-Szombat");

            string[] mit = { "Hétfő-Csütörtök", "Kedd-Péntek", "Szerda-Szombat" };

            // kiírjuk a kocsikat

            List<Adat_Jármű_hiba> AdatokHiba = KézJárműHiba.Lista_Adatok(Cmbtelephely.Trim());
            List<Adat_Jármű_2> AdatokHárom = KézJármű.Lista_Adatok(Cmbtelephely);

            for (int j = 0; j < 3; j++)
            {
                List<Adat_Jármű_2> AdatokSzűrt = (from a in AdatokHárom
                                                  where a.Haromnapos == j + 1
                                                  orderby a.Azonosító ascending
                                                  select a).ToList();
                string munkalap = mit[j];
                MyX.Munkalap_aktív(munkalap);
                MyX.Munkalap_betű(munkalap, BeBetű);

                int sor = 1;
                int oszlop = 1;
                int i = 1;

                foreach (Adat_Jármű_2 rekord in AdatokSzűrt)
                {
                    if (sor == 1)
                    {
                        // elkészítjük a fejlécet
                        MyX.Kiir("psz", MyF.Oszlopnév(oszlop) + $"{sor}");
                        MyX.Kiir("Hiba", MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                        MyX.Kiir("Nappal", MyF.Oszlopnév(oszlop + 2) + $"{sor}");
                        MyX.Kiir("Éjszaka", MyF.Oszlopnév(oszlop + 3) + $"{sor}");
                        sor += 1;
                    }
                    MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(oszlop) + $"{sor}");
                    MyX.Betű(munkalap, MyF.Oszlopnév(oszlop) + $"{sor}", BeBetűD);
                    List<Adat_Jármű_hiba> ElemekHiba = (from a in AdatokHiba
                                                        where a.Azonosító == rekord.Azonosító
                                                        orderby a.Korlát descending
                                                        select a).ToList();
                    string üzemképtelen = "";
                    string beálló = "";
                    if (ElemekHiba != null && ElemekHiba.Count > 0)
                    {
                        foreach (Adat_Jármű_hiba Elem in ElemekHiba)
                        {
                            if (Elem.Korlát == 4) üzemképtelen += Elem.Hibaleírása;
                            if (Elem.Korlát == 3) beálló += Elem.Hibaleírása;
                        }

                        if (üzemképtelen.Trim() != "_")
                        {
                            // ha üzemképtelen
                            if (üzemképtelen.Length > 20)
                                MyX.Kiir(üzemképtelen.Substring(0, 20), MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                            else
                                MyX.Kiir(üzemképtelen, MyF.Oszlopnév(oszlop + 1) + $"{sor}");

                            MyX.Betű(munkalap, MyF.Oszlopnév(oszlop + 1) + $"{sor}", BeBetűV);
                        }
                        else if (beálló.Trim() != "_")
                        {
                            // ha beálló


                            if (beálló.Length > 20)
                                MyX.Kiir(beálló.Substring(0, 20), MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                            else
                                MyX.Kiir(beálló, MyF.Oszlopnév(oszlop + 1) + $"{sor}");

                            MyX.Betű(munkalap, MyF.Oszlopnév(oszlop + 1) + $"{sor}", BeBetűD);
                        }
                    }
                    sor += 1;
                    i += 1;

                    if (sor == 27)
                    {
                        sor = 1;
                        oszlop += 4;
                    }
                }

                //Formázzukalapokat

                for (int ii = 1; ii < oszlop + 3; ii += 4)
                {
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(ii) + "1:" + MyF.Oszlopnév(ii + 3) + "26");
                    MyX.Betű(munkalap, MyF.Oszlopnév(ii) + "1:" + MyF.Oszlopnév(ii) + "26", BeBetűD);
                }
                for (int ii = 1; ii < oszlop + 3; ii += 4)
                {
                    MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(ii) + ":" + MyF.Oszlopnév(ii + 3), 10);
                    MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(ii + 1) + ":" + MyF.Oszlopnév(ii + 1), 25);
                    MyX.Igazít_vízszintes(munkalap, $"{MyF.Oszlopnév(ii)}1:{MyF.Oszlopnév(ii)}26", "közép");
                }
                MyX.Sormagasság(munkalap, "1:26", 25);
                MyX.Betű(munkalap, $"A1:{MyF.Oszlopnév(oszlop + 3)}1", BeBetűV);
                // nyomtatási terület
                bool álló = true;
                if (papírelrendezés == "Fekvő") álló = false;
                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:{MyF.Oszlopnév(oszlop + 3)}26",
                    FejlécBal = mit[j],
                    FejlécKözép = "E2 vizsgálati",
                    FejlécJobb = DateTime.Today.ToString("yyyy.MM.dd"),
                    LáblécBal = "........................................................" + '\n' + "nappalos aláírás",
                    LáblécJobb = "........................................................" + '\n' + "éjszakás aláírás",
                    LapSzéles = 1,
                    LapMagas = 1,
                    FelsőMargó = 15,
                    AlsóMargó = 25,
                    BalMargó = 18,
                    JobbMargó = 18,
                    FejlécMéret = 8,
                    LáblécMéret = 8,
                    VízKözép = true,
                    FüggKözép = true,
                    Álló = álló,
                    Papírméret = papírméret
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);
            }
            // átnevezzük a lapokat
            MyX.ExcelMentés(fájlneve);
            MyX.ExcelBezárás();
            MyF.Megnyitás(fájlneve);
        }
    }
}

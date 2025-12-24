using System;
using System.Collections.Generic;
using System.Linq;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.Villamos_Nyomtatványok
{
    public class Főkönyv_Jegykezelő
    {
        readonly Beállítás_Betű BeBetű = new Beállítás_Betű { Méret = 20 };
        public void Jegykezelő(string fájlneve, string Cmbtelephely, List<Adat_Jármű> AdatokJármű, List<Adat_Főkönyv_Nap> AdatokFőkönyvNap, DateTime Dátum, List<string> AdatokTakarításTípus, List<Adat_Jármű_Vendég> AdatokFőVendég)
        {
            string munkalap = "Munka1";
            MyX.ExcelLétrehozás();
            MyX.Munkalap_betű(munkalap, BeBetű);

            // első sor állítva
            MyX.SzövegIrány(munkalap, "5:5", 90);
            MyX.Sormagasság(munkalap, "5:5", 150);

            DateTime napszak = new DateTime(1900, 1, 1);

            int sor = 5;
            int oszlop = 1;
            int i = 1;
            int oszlopismét = 1;

            foreach (Adat_Jármű rekord in AdatokJármű)
            {
                string Típuskell = (from a in AdatokTakarításTípus
                                    where a == rekord.Típus
                                    select a).FirstOrDefault();
                if (Típuskell != null && Típuskell.Trim() != "")
                {
                    if (sor == 5)
                    {
                        //  elkészítjük a fejlécet
                        MyX.Kiir("Psz", MyF.Oszlopnév(oszlop) + $"{sor}");
                        MyX.Kiir("Típus", MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                        MyX.Kiir("Ellenőrizve", MyF.Oszlopnév(oszlop + 2) + $"{sor}");
                        MyX.Kiir("Eltömítve", MyF.Oszlopnév(oszlop + 3) + $"{sor}");
                        MyX.Kiir("Készülék csere", MyF.Oszlopnév(oszlop + 4) + $"{sor}");
                        MyX.Kiir("Futár hiba", MyF.Oszlopnév(oszlop + 5) + $"{sor}");
                        MyX.Kiir("Festékszalag", MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                        sor += 1;
                    }
                    //  kiírjuk a pályaszámot, típust
                    MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(oszlop) + $"{sor}");
                    MyX.Kiir(rekord.Típus.Trim(), MyF.Oszlopnév(oszlop + 1) + $"{sor}");

                    Adat_Főkönyv_Nap ElemNap = (from a in AdatokFőkönyvNap
                                                where a.Azonosító == rekord.Azonosító
                                                select a).FirstOrDefault();

                    if (ElemNap != null)
                    {
                        if (ElemNap.Tervindulás.ToShortDateString() == napszak.ToShortDateString())
                        {
                            MyX.Egyesít(munkalap, MyF.Oszlopnév(oszlop + 2) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                            MyX.Kiir("Benn volt", MyF.Oszlopnév(oszlop + 2) + $"{sor}");
                        }
                    }
                    Adat_Jármű_Vendég VendégAdat = (from a in AdatokFőVendég
                                                    where a.Azonosító == rekord.Azonosító
                                                    select a).FirstOrDefault();

                    if (VendégAdat != null)
                    {
                        if (Cmbtelephely.Trim() != VendégAdat.KiadóTelephely)
                        {
                            MyX.Egyesít(munkalap, MyF.Oszlopnév(oszlop + 2) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                            MyX.Kiir(VendégAdat.KiadóTelephely, MyF.Oszlopnév(oszlop + 2) + $"{sor}");
                        }
                    }
                    sor += 1;
                    i += 1;

                    if (sor == 56)
                    {
                        sor = 5;
                        oszlop += 7;
                        oszlopismét += 1;
                    }
                }
            }

            // összes oszlopszélesség 5
            MyX.Oszlopszélesség(munkalap, "a:" + MyF.Oszlopnév(oszlopismét * 7), 5);
            for (int j = 0; j < oszlopismét; j++)
            {
                //  beállítjuk az oszlop psz szélességeket
                MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(1 + j * 7) + ":" + MyF.Oszlopnév(1 + j * 7), 16);
                MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(1 + j * 7 + 1) + ":" + MyF.Oszlopnév(1 + j * 7 + 1), 16);
                //  rácsozzuk
                MyX.Rácsoz(munkalap, MyF.Oszlopnév(1 + j * 7) + "5:" + MyF.Oszlopnév(7 + j * 7) + "55");

            }

            MyX.Kiir("Éjszakai jegyellenőrzés", "A3");
            MyX.Kiir($"{Dátum:yyyy.MM} hó {Dátum:dd} nap {Dátum:dddd}", "a1");

            MyX.Egyesít(munkalap, "a60:f60");
            MyX.Aláírásvonal(munkalap, "A60:F60");
            MyX.Kiir("Váltós csoportvezető", "A60");

            // nyomtatási terület
            Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
            {
                Munkalap = munkalap,
                NyomtatásiTerület = $"A1:{MyF.Oszlopnév(7 + (oszlopismét - 1) * 7)}61",
                LapSzéles = 1,
                LapMagas = 1,
                FelsőMargó = 19,
                AlsóMargó = 15,
                BalMargó = 18,
                JobbMargó = 18,
                FejlécMéret = 8,
                LáblécMéret = 8,
            };
            MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);
            MyX.ExcelMentés(fájlneve);
            MyX.ExcelBezárás();

            MyF.Megnyitás(fájlneve);
        }
    }
}

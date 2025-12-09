using System;
using System.Collections.Generic;
using System.Linq;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.Villamos_Nyomtatványok
{
    public class Főkönyv_Takarítás
    {

        readonly Beállítás_Betű BeBetű = new Beállítás_Betű { Név = "Arial", Méret = 20 };
        readonly Beállítás_Betű BeBetű14 = new Beállítás_Betű { Név = "Arial", Méret = 14 };
        readonly Beállítás_Betű BeBetűC12 = new Beállítás_Betű { Név = "Calibri", Méret = 12 };
        readonly Beállítás_Betű BeBetűC16 = new Beállítás_Betű { Név = "Calibri", Méret = 16 };

        string munkalap = "";
        public void Takarítás_Excel(string fájlexc, string Cmbtelephely, DateTime Dátum, string napszak, List<string> AdatokTakarításTípus,
                                 List<Adat_Jármű> AdatokJármű,
                                 List<Adat_Főkönyv_Nap> AdatokFőkönyvNap,
                                 List<Adat_Jármű_Vendég> AdatokFőVendég,
                                 List<Adat_Főkönyv_ZSER> AdatokFőkönyvZSER)
        {
            munkalap = "Takarítás";
            MyX.ExcelLétrehozás(munkalap);

            // létrehozzunk annyi lapfület amennyi típusm kell
            if (AdatokTakarításTípus != null && AdatokTakarításTípus.Count > 0)
            {

                MyX.Munkalap_Új("Nappalos Igazoló");
                MyX.Munkalap_Új("Összes_állományi");
                MyX.Munkalap_Új("J1_J2_J3");
                MyX.Munkalap_Új("J4_J5_J6");

                // munkalapok létrehozása
                foreach (string rekordkieg in AdatokTakarításTípus)
                {
                    MyX.Munkalap_Új(rekordkieg.Trim());
                    MyX.Munkalap_Új(rekordkieg.Trim() + "_Üres");
                }

            }
            Összevont("J1", Dátum, napszak, AdatokJármű, AdatokFőkönyvNap, AdatokTakarításTípus, AdatokFőVendég);
            Összevont("J4", Dátum, napszak, AdatokJármű, AdatokFőkönyvNap, AdatokTakarításTípus, AdatokFőVendég);
            Söpréslapok(napszak, Dátum, Cmbtelephely, AdatokTakarításTípus, AdatokJármű, AdatokFőkönyvNap, AdatokFőVendég);
            Üreslapok(Dátum, napszak, AdatokTakarításTípus, AdatokJármű);
            EstiBeállók(Dátum, napszak, AdatokFőkönyvZSER);
            Összes_takarítás_kocsi(Cmbtelephely, Dátum, napszak, AdatokTakarításTípus, AdatokJármű, AdatokFőkönyvNap, AdatokFőVendég);
            Takarítás_igazoló(Cmbtelephely, Dátum);


            // bezárjuk az Excel-t
            MyX.Munkalap_aktív("Takarítás");
            MyX.ExcelMentés(fájlexc);
            MyX.ExcelBezárás();
            MyF.Megnyitás(fájlexc);
        }

        private void Söpréslapok(string napszak, DateTime Dátum, string Cmbtelephely, List<string> AdatokTakarításTípus,
                                 List<Adat_Jármű> AdatokJármű,
                                 List<Adat_Főkönyv_Nap> AdatokFőkönyvNap,
                                 List<Adat_Jármű_Vendég> AdatokFőVendég)
        {

            // ******************************
            // *  Söprés lapok              *
            // ******************************
            int sor, oszlop, oszlopismét;

            foreach (string rekordkieg in AdatokTakarításTípus)
            {
                munkalap = rekordkieg.Trim();
                MyX.Munkalap_aktív(rekordkieg.Trim());
                MyX.Munkalap_betű(munkalap, BeBetű);
                sor = 5;
                oszlop = 1;
                oszlopismét = 1;

                List<Adat_Jármű> AdatokJárműSzűrt = (from a in AdatokJármű
                                                     where a.Típus == rekordkieg
                                                     orderby a.Azonosító ascending
                                                     select a).ToList();

                foreach (Adat_Jármű rekord in AdatokJárműSzűrt)
                {
                    if (sor == 5)
                    {
                        // elkészítjük a fejlécet
                        MyX.Kiir("Psz", MyF.Oszlopnév(oszlop) + $"{sor}");
                        MyX.Kiir("Kijelölve", MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                        MyX.Kiir("Megfelelő", MyF.Oszlopnév(oszlop + 2) + $"{sor}");
                        MyX.Kiir("Nem Megfelelő", MyF.Oszlopnév(oszlop + 3) + $"{sor}");
                        MyX.Kiir("Graffiti (m2)", MyF.Oszlopnév(oszlop + 4) + $"{sor}");
                        MyX.Kiir("Eseti (m2)", MyF.Oszlopnév(oszlop + 5) + $"{sor}");
                        MyX.Kiir("Fertőtlenítés (m2)", MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                        sor += 1;
                    }
                    MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(oszlop) + $"{sor}");

                    DateTime IdeigDátum = new DateTime(Dátum.Year, Dátum.Month, Dátum.Day, 14, 0, 0);
                    DateTime NullaDátum = new DateTime(1900, 1, 1, 0, 0, 0);
                    Adat_Főkönyv_Nap ElemNap;
                    if (napszak.Trim() == "de")
                        ElemNap = (from a in AdatokFőkönyvNap
                                   where a.Azonosító == rekord.Azonosító &&
                                   a.Tervérkezés < IdeigDátum &&
                                   a.Tervérkezés != NullaDátum
                                   select a).FirstOrDefault();
                    else
                        ElemNap = (from a in AdatokFőkönyvNap
                                   where a.Azonosító == rekord.Azonosító &&
                                   a.Tervérkezés >= IdeigDátum
                                   select a).FirstOrDefault();

                    if (ElemNap != null) MyX.Kiir("X", MyF.Oszlopnév(oszlop + 1) + $"{sor}");


                    Adat_Jármű_Vendég VendégAdat = (from a in AdatokFőVendég
                                                    where a.Azonosító == rekord.Azonosító
                                                    select a).FirstOrDefault();

                    if (VendégAdat != null)
                    {
                        if (Cmbtelephely.Trim() != VendégAdat.KiadóTelephely)
                        {
                            MyX.Kiir("", MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                            MyX.Egyesít(rekordkieg.Trim(), MyF.Oszlopnév(oszlop + 2) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                            MyX.Kiir(VendégAdat.KiadóTelephely, MyF.Oszlopnév(oszlop + 2) + $"{sor}");
                        }
                    }

                    sor += 1;
                    if (sor == 46)
                    {
                        sor = 5;
                        oszlop += 8;
                        oszlopismét += 1;
                    }
                }
                // első sor állítva
                MyX.SzövegIrány(rekordkieg.Trim(), $"A5:{MyF.Oszlopnév(oszlopismét * 8)}5", 90);
                MyX.Sormagasság(munkalap, "5:5", 175);

                // összes oszlopszélesség 7
                MyX.Oszlopszélesség(rekordkieg.Trim(), "a:" + MyF.Oszlopnév(oszlopismét * 8), 6);

                for (int j = 0; j < oszlopismét; j++)
                {
                    // beállítjuk az oszlop psz szélességeket
                    MyX.Oszlopszélesség(rekordkieg.Trim(), MyF.Oszlopnév(1 + j * 8) + ":" + MyF.Oszlopnév(1 + j * 8), 10);

                    // rácsozzuk
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1 + j * 8) + "5:" + MyF.Oszlopnév(7 + j * 8) + "5");
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1 + j * 8) + "5:" + MyF.Oszlopnév(7 + j * 8) + "46");
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1 + j * 8) + "46:" + MyF.Oszlopnév(7 + j * 8) + "46");

                    MyX.Kiir("#KÉPLET#=COUNTIF(R[-40]C:R[-1]C,\"X\")", MyF.Oszlopnév(2 + j * 8) + "46");
                }
                MyX.Sormagasság(munkalap, "46:47", 30);
                MyX.Vastagkeret(munkalap, MyF.Oszlopnév(1 + (oszlopismét - 1) * 8) + "47:" + MyF.Oszlopnév(7 + (oszlopismét - 1) * 8) + "47");
                MyX.Kiir("Össz", "A46");
                MyX.Kiir("Össz", MyF.Oszlopnév(1 + (oszlopismét - 1) * 8) + "47");
                MyX.Kiir("#KÉPLET#=SUM(R[-1])", MyF.Oszlopnév(2 + (oszlopismét - 1) * 8) + "47");


                if (oszlopismét < 3) oszlopismét = 3;
                Beállítás_Nyomtatás BeNYom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:{MyF.Oszlopnév(7 + (oszlopismét - 1) * 8)}65",
                    LapMagas = 1,
                    LapSzéles = 1,
                    BalMargó = 10,
                    JobbMargó = 10,
                    AlsóMargó = 15,
                    FelsőMargó = 19,
                    LáblécMéret = 8,
                    FejlécMéret = 8
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNYom);
                if (napszak == "de")
                    MyX.Kiir(rekordkieg.Trim() + "    J1     takarítás Nappal", "a3");
                else
                    MyX.Kiir(rekordkieg.Trim() + "    J1     takarítás ÉJSZAKA", "a3");



                MyX.Kiir("Előírt létszám:  …… fő, megjelent :……. Fő", "A49");
                MyX.Kiir("Cégjelzéses munkaruhát nem viselt : …….. Fő", "a51");

                MyX.Egyesít(rekordkieg.Trim(), "a53:p53");
                MyX.Kiir("A megrendelő a napi munkatervet átadta: ……….óra……….perckor. ", "A53");
                MyX.Egyesít(rekordkieg.Trim(), "a58:e58");
                MyX.Egyesít(rekordkieg.Trim(), "g58:k58");
                MyX.Kiir("BKV ZRT.", "a58");
                MyX.Kiir("Vállalkozó", "g58");
                MyX.Pontvonal(munkalap, "a58:e58");
                MyX.Pontvonal(munkalap, "g58:k58");

                MyX.Egyesít(rekordkieg.Trim(), "a59:p59");
                MyX.Kiir("A vállalkozó a napi munkafeladatok elvégzését lejelentette:", "A59");
                MyX.Egyesít(rekordkieg.Trim(), "a61:p61");
                MyX.Kiir("  ……….óra……….perckor.", "a61");


                MyX.Egyesít(rekordkieg.Trim(), "a65:e65");
                MyX.Egyesít(rekordkieg.Trim(), "g65:k65");
                MyX.Kiir("BKV ZRT.", "a65");
                MyX.Kiir("Vállalkozó", "g65");
                MyX.Pontvonal(munkalap, "a65:e65");
                MyX.Pontvonal(munkalap, "g65:k65");

                MyX.Kiir(Dátum.ToString("yyyy.MM") + " hó " + Dátum.ToString("dd") + " nap", "a1");
            }
        }

        private void Üreslapok(DateTime Dátum, string napszak, List<string> AdatokTakarításTípus,
                                 List<Adat_Jármű> AdatokJármű)
        {
            // ******************************
            // *  Üres lapok                *
            // ******************************
            int sor, oszlop, oszlopismét;

            foreach (string rekordkieg in AdatokTakarításTípus)
            {
                munkalap = rekordkieg.Trim() + "_Üres";
                MyX.Munkalap_aktív(munkalap);
                // minden cella
                MyX.Munkalap_betű(munkalap, BeBetű);
                sor = 5;
                oszlop = 1;
                oszlopismét = 1;

                List<Adat_Jármű> AdatokJárműSzűrt = (from a in AdatokJármű
                                                     where a.Típus == rekordkieg
                                                     orderby a.Azonosító ascending
                                                     select a).ToList();

                foreach (Adat_Jármű rekord in AdatokJárműSzűrt)
                {
                    if (sor == 5)
                    {
                        // elkészítjük a fejlécet
                        MyX.Kiir("Psz", MyF.Oszlopnév(oszlop) + $"{sor}");
                        MyX.Kiir("Kijelölve", MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                        MyX.Kiir("Megfelelő", MyF.Oszlopnév(oszlop + 2) + $"{sor}");
                        MyX.Kiir("Nem Megfelelő", MyF.Oszlopnév(oszlop + 3) + $"{sor}");
                        MyX.Kiir("Graffiti (m2)", MyF.Oszlopnév(oszlop + 4) + $"{sor}");
                        MyX.Kiir("Eseti (m2)", MyF.Oszlopnév(oszlop + 5) + $"{sor}");
                        MyX.Kiir("Fertőtlenítés (m2)", MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                        sor += 1;
                    }
                    MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(oszlop) + $"{sor}");
                    sor += 1;

                    if (sor == 46)
                    {
                        sor = 5;
                        oszlop += 8;
                        oszlopismét += 1;
                    }
                }


                // első sor állítva
                MyX.SzövegIrány(munkalap, $"A5:{MyF.Oszlopnév(oszlopismét * 8)}5", 90);
                MyX.Sormagasság(munkalap, "5:5", 175);

                // összes oszlopszélesség 7
                MyX.Oszlopszélesség(munkalap, "a:" + MyF.Oszlopnév(oszlopismét * 7), 6);

                for (int j = 0; j < oszlopismét; j++)
                {
                    // beállítjuk az oszlop psz szélességeket
                    MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(1 + j * 8) + ":" + MyF.Oszlopnév(1 + j * 8), 10);

                    // rácsozzuk
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1 + j * 8) + "5:" + MyF.Oszlopnév(7 + j * 8) + "6");
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1 + j * 8) + "6:" + MyF.Oszlopnév(7 + j * 8) + "45");
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1 + j * 8) + "46:" + MyF.Oszlopnév(7 + j * 8) + "46");
                }
                MyX.Sormagasság(munkalap, "46:47", 30);
                MyX.Vastagkeret(munkalap, MyF.Oszlopnév(1 + (oszlopismét - 1) * 8) + "47:" + MyF.Oszlopnév(7 + (oszlopismét - 1) * 8) + "47");
                MyX.Kiir("Össz", "A46");
                MyX.Kiir("Össz", MyF.Oszlopnév(1 + (oszlopismét - 1) * 8) + "47");

                if (oszlopismét < 3)
                    oszlopismét = 3;
                Beállítás_Nyomtatás BeNYom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:{MyF.Oszlopnév(7 + (oszlopismét - 1) * 8)}65",
                    LapMagas = 1,
                    LapSzéles = 1,
                    BalMargó = 10,
                    JobbMargó = 10,
                    AlsóMargó = 15,
                    FelsőMargó = 19,
                    LáblécMéret = 8,
                    FejlécMéret = 8
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNYom);
                if (napszak == "de")
                    MyX.Kiir(rekordkieg.Trim() + "    J1     takarítás Nappal", "a3");
                else
                    MyX.Kiir(rekordkieg.Trim() + "    J1     takarítás ÉJSZAKA", "a3");

                MyX.Kiir("Előírt létszám:  …… fő, megjelent :……. Fő", "A49");
                MyX.Kiir("Cégjelzéses munkaruhát nem viselt : …….. Fő", "a51");

                MyX.Egyesít(munkalap, "A53:P53");
                MyX.Kiir("A megrendelő a napi munkatervet átadta: ……….óra……….perckor. ", "A53");
                MyX.Egyesít(munkalap, "a58:e58");
                MyX.Egyesít(munkalap, "g58:k58");
                MyX.Kiir("BKV ZRT.", "a58");
                MyX.Kiir("Vállalkozó", "g58");
                MyX.Pontvonal(munkalap, "a58:e58");
                MyX.Pontvonal(munkalap, "g58:k58");

                MyX.Egyesít(munkalap, "a59:p59");
                MyX.Kiir("A vállalkozó a napi munkafeladatok elvégzését lejelentette:", "A59");
                MyX.Egyesít(munkalap, "a61:p61");
                MyX.Kiir("  ……….óra……….perckor.", "a61");


                MyX.Egyesít(munkalap, "a65:e65");
                MyX.Egyesít(munkalap, "g65:k65");
                MyX.Kiir("BKV ZRT.", "a65");
                MyX.Kiir("Vállalkozó", "g65");
                MyX.Pontvonal(munkalap, "a65:e65");
                MyX.Pontvonal(munkalap, "g65:k65");

                MyX.Kiir(Dátum.ToString("yyyy.MM") + " hó " + Dátum.ToString("dd") + " nap", "a1");
            }
        }

        private void EstiBeállók(DateTime Dátum, string napszak, List<Adat_Főkönyv_ZSER> AdatokFőkönyvZSER)
        {

            // ******************************
            // *  ESti beállók              *
            // ******************************

            int sor;
            munkalap = "Takarítás";
            MyX.Munkalap_aktív(munkalap);
            // minden cella
            MyX.Munkalap_betű(munkalap, BeBetű14);

            sor = 2;

            if (napszak == "de")
                MyX.Kiir(Dátum.ToString("yyyy.MM.dd") + " Nappali söprés", "a1");
            else
                MyX.Kiir(Dátum.ToString("yyyy.MM.dd") + " Esti söprés", "a1");

            foreach (Adat_Főkönyv_ZSER rekord in AdatokFőkönyvZSER)
            {
                if (napszak == "de")
                {
                    if (rekord.Tervérkezés < new DateTime(Dátum.Year, Dátum.Month, Dátum.Day, 14, 0, 0) && rekord.Tervérkezés > new DateTime(Dátum.Year, Dátum.Month, Dátum.Day, 0, 0, 0))
                    {
                        sor += 1;
                        MyX.Kiir(rekord.Viszonylat.Trim(), MyF.Oszlopnév(1) + $"{sor}");
                        if (rekord.Kocsi1.Trim() != "_")
                            MyX.Kiir(rekord.Kocsi1.Trim(), MyF.Oszlopnév(2) + $"{sor}");
                        if (rekord.Kocsi2.Trim() != "_")
                            MyX.Kiir(rekord.Kocsi2.Trim(), MyF.Oszlopnév(3) + $"{sor}");
                        if (rekord.Kocsi3.Trim() != "_")
                            MyX.Kiir(rekord.Kocsi3.Trim(), MyF.Oszlopnév(4) + $"{sor}");
                        if (rekord.Kocsi4.Trim() != "_")
                            MyX.Kiir(rekord.Kocsi4.Trim(), MyF.Oszlopnév(5) + $"{sor}");
                        if (rekord.Kocsi5.Trim() != "_")
                            MyX.Kiir(rekord.Kocsi5.Trim(), MyF.Oszlopnév(6) + $"{sor}");
                        if (rekord.Kocsi6.Trim() != "_")
                            MyX.Kiir(rekord.Kocsi6.Trim(), MyF.Oszlopnév(7) + $"{sor}");
                        MyX.Kiir(rekord.Tervérkezés.ToString(), MyF.Oszlopnév(8) + $"{sor}");
                    }
                }

                else if (rekord.Tervérkezés > new DateTime(Dátum.Year, Dátum.Month, Dátum.Day, 14, 0, 0))
                {
                    sor += 1;
                    MyX.Kiir(rekord.Viszonylat.Trim(), MyF.Oszlopnév(1) + $"{sor}");
                    if (rekord.Kocsi1.Trim() != "_")
                        MyX.Kiir(rekord.Kocsi1.Trim(), MyF.Oszlopnév(2) + $"{sor}");
                    if (rekord.Kocsi2.Trim() != "_")
                        MyX.Kiir(rekord.Kocsi2.Trim(), MyF.Oszlopnév(3) + $"{sor}");
                    if (rekord.Kocsi3.Trim() != "_")
                        MyX.Kiir(rekord.Kocsi3.Trim(), MyF.Oszlopnév(4) + $"{sor}");
                    if (rekord.Kocsi4.Trim() != "_")
                        MyX.Kiir(rekord.Kocsi4.Trim(), MyF.Oszlopnév(5) + $"{sor}");
                    if (rekord.Kocsi5.Trim() != "_")
                        MyX.Kiir(rekord.Kocsi5.Trim(), MyF.Oszlopnév(6) + $"{sor}");
                    if (rekord.Kocsi6.Trim() != "_")
                        MyX.Kiir(rekord.Kocsi6.Trim(), MyF.Oszlopnév(7) + $"{sor}");
                    MyX.Kiir(rekord.Tervérkezés.ToString(), MyF.Oszlopnév(8) + $"{sor}");
                }

            }
            MyX.Oszlopszélesség(munkalap, "H:H");
            Beállítás_Nyomtatás BeNYom = new Beállítás_Nyomtatás
            {
                Munkalap = munkalap,
                NyomtatásiTerület = $"A1:H{sor}",
                LapSzéles = 1
            };
            MyX.NyomtatásiTerület_részletes(munkalap, BeNYom);

            MyX.Kiir(Dátum.ToString("yyyy.MM") + " hó " + Dátum.ToString("dd") + " nap", "a1");
        }

        private void Takarítás_igazoló(string Cmbtelephely, DateTime Dátum)
        {
            munkalap = "Nappalos Igazoló";
            MyX.Munkalap_aktív(munkalap);

            // Betűméret
            MyX.Munkalap_betű(munkalap, BeBetűC12);

            // létszám adatok
            MyX.Oszlopszélesség(munkalap, "a:a", 6);
            MyX.Oszlopszélesség(munkalap, "b:c", 15);
            MyX.Oszlopszélesség(munkalap, "d:i", 10);
            MyX.Oszlopszélesség(munkalap, "i:i", 13);
            MyX.Egyesít(munkalap, "a1:b1");
            MyX.Egyesít(munkalap, "a2:b2");
            MyX.Egyesít(munkalap, "a3:b3");
            MyX.Kiir("Előírt létszám [Fő]: ", "a1");
            MyX.Kiir("Megjelent [Fő]:", "a2");
            MyX.Kiir("Munkaruhát viselt  [Fő]:", "a3");
            MyX.Rácsoz(munkalap, "a1:c3");

            Kezelő_Jármű_Takarítás_Vezénylés KJTV_kéz = new Kezelő_Jármű_Takarítás_Vezénylés();
            List<Adat_Jármű_Takarítás_Vezénylés> Adatok = KJTV_kéz.Lista_Adatok(Cmbtelephely.Trim(), DateTime.Now.Year);
            Adatok = (from a in Adatok
                      where a.Státus != 9
                      && a.Dátum.ToShortDateString() == Dátum.ToShortDateString()
                      orderby a.Takarítási_fajta, a.Szerelvényszám, a.Azonosító
                      select a).ToList();

            string takarítási_fajta = "";
            int sor = 5;
            int eleje = 5;
            int vége = 5;

            foreach (Adat_Jármű_Takarítás_Vezénylés rekord in Adatok)
            {

                if (takarítási_fajta.Trim() == rekord.Takarítási_fajta.Trim())
                {
                    // ha azonos akkor kiírja a pályaszámot
                    sor += 1;
                    MyX.Kiir(rekord.Azonosító.Trim(), "a" + $"{sor}");
                    MyX.Sormagasság(munkalap, sor.ToString() + ":" + $"{sor}", 25);
                }
                else
                {
                    // fejlécet készít és befejezi az előző táblát
                    if (sor != 5)
                    {
                        vége = sor + 3;
                        MyX.Sormagasság(munkalap, sor.ToString() + ":" + (sor + 3).ToString(), 25);
                        sor += 4;
                    }
                    if (eleje == 5 & vége == 5)
                    {
                    }
                    // első alkalommal nem fejezi be az előző táblázatot
                    else
                    {
                        // befejezi az előző táblát
                        MyX.Rácsoz(munkalap, $"a{eleje}:i{eleje}");
                        MyX.Rácsoz(munkalap, $"a{eleje + 1}:i{vége}");
                    }

                    takarítási_fajta = rekord.Takarítási_fajta.Trim();
                    MyX.Betű(munkalap, $"a{sor}", BeBetűC16);

                    MyX.Kiir(takarítási_fajta.Trim(), "a" + $"{sor}");
                    sor += 1;
                    eleje = sor;


                    // fejléc
                    MyX.Sormagasság(munkalap, sor.ToString() + ":" + $"{sor}", 48);
                    MyX.Kiir("Jármű biztosításának ideje", "b" + $"{sor}");
                    MyX.Kiir("Takarítás befejezésének ideje", "c" + $"{sor}");

                    MyX.Kiir("Megfelelt", "d" + $"{sor}");
                    MyX.Kiir("Nem Megfelelt", "e" + $"{sor}");
                    MyX.Kiir("Pót határidő", "f" + $"{sor}");
                    MyX.Kiir("Megfelelt", "g" + $"{sor}");
                    MyX.Kiir("Nem Megfelelt", "h" + $"{sor}");
                    MyX.Kiir("Igazolta", "i" + $"{sor}");
                    MyX.Sortörésseltöbbsorba(munkalap, $"A{sor}:I{sor}");
                    // első kocsi
                    sor += 1;
                    MyX.Kiir(rekord.Azonosító.Trim(), "a" + $"{sor}");
                    MyX.Sormagasság(munkalap, sor.ToString() + ":" + $"{sor}", 25);
                }
            }


            // befejezi az előző tábláta  

            vége = sor + 3;
            MyX.Sormagasság(munkalap, sor.ToString() + ":" + (sor + 3).ToString(), 25);
            MyX.Rácsoz(munkalap, $"a{eleje}:i{eleje}");
            MyX.Rácsoz(munkalap, $"a{eleje + 1}:i{vége}");

            // Aláírás lábléc
            sor += 5;
            MyX.Egyesít(munkalap, "a" + $"{sor}" + ":g" + $"{sor}");
            MyX.Kiir("A megrendelő a napi munkatervet átadta: ……….óra……….perckor. ", "A" + $"{sor}");
            sor += 5;
            MyX.Egyesít(munkalap, "a" + $"{sor}" + ":d" + $"{sor}");
            MyX.Egyesít(munkalap, "f" + $"{sor}" + ":i" + $"{sor}");
            MyX.Kiir("BKV ZRT.", "a" + $"{sor}");
            MyX.Kiir("Vállalkozó", "f" + $"{sor}");
            MyX.Pontvonal(munkalap, "a" + $"{sor}");
            MyX.Pontvonal(munkalap, "f" + $"{sor}");

            sor += 2;
            MyX.Egyesít(munkalap, "a" + $"{sor}" + ":g" + $"{sor}");
            MyX.Kiir("A vállalkozó a napi munkafeladatok elvégzését lejelentette:  ……….óra……….perckor.", "a" + $"{sor}");

            sor += 5;
            MyX.Egyesít(munkalap, "a" + $"{sor}" + ":d" + $"{sor}");
            MyX.Egyesít(munkalap, "f" + $"{sor}" + ":i" + $"{sor}");
            MyX.Kiir("BKV ZRT.", "a" + $"{sor}");
            MyX.Kiir("Vállalkozó", "f" + $"{sor}");
            MyX.Pontvonal(munkalap, "a" + $"{sor}");
            MyX.Pontvonal(munkalap, "f" + $"{sor}");

            vége = sor;

            // nyomtatási beállítások
            Beállítás_Nyomtatás BeNYom = new Beállítás_Nyomtatás
            {
                Munkalap = munkalap,
                NyomtatásiTerület = $"A1:I{vége}",
                LapSzéles = 1,
                BalMargó = 18,
                JobbMargó = 18,
                AlsóMargó = 19,
                FelsőMargó = 19,
                LáblécMéret = 8,
                FejlécMéret = 8,
                FejlécJobb = Cmbtelephely.Trim(),
                FejlécKözép = "Jármű takarítás igazolólap Nappal ",
                FejlécBal = Dátum.ToString("yyyy.MM.dd"),
                LáblécBal = "........................................\nTakarítást végző    \n",
                LáblécJobb = "........................................\n                    BKV Zrt",
                VízKözép = true
            };
            MyX.NyomtatásiTerület_részletes(munkalap, BeNYom);
        }

        private void Összes_takarítás_kocsi(string Cmbtelephely, DateTime Dátum, string napszak, List<string> AdatokTakarításTípus, List<Adat_Jármű> AdatokJármű,
            List<Adat_Főkönyv_Nap> AdatokFőkönyvNap, List<Adat_Jármű_Vendég> AdatokFőVendég)
        {
            munkalap = "Összes_állományi";
            MyX.Munkalap_aktív(munkalap);

            int sor;
            int oszlop;
            int oszlopismét;
            int blokkeleje;

            sor = 5;
            oszlop = 1;
            oszlopismét = 1;

            // elkészítjük a fejlécet
            MyX.Kiir("Psz", MyF.Oszlopnév(oszlop) + 5.ToString());
            MyX.Kiir("Kijelölve", MyF.Oszlopnév(oszlop + 1) + 5.ToString());
            MyX.Kiir("Megfelelő", MyF.Oszlopnév(oszlop + 2) + 5.ToString());
            MyX.Kiir("Nem Megfelelő", MyF.Oszlopnév(oszlop + 3) + 5.ToString());
            MyX.Kiir("Graffiti (m2)", MyF.Oszlopnév(oszlop + 4) + 5.ToString());
            MyX.Kiir("Eseti (m2)", MyF.Oszlopnév(oszlop + 5) + 5.ToString());
            MyX.Kiir("Fertőtlenítés (m2)", MyF.Oszlopnév(oszlop + 6) + 5.ToString());
            sor += 1;

            foreach (string rekordkieg in AdatokTakarításTípus)
            {
                // elkészítjük a fejlécet
                MyX.Kiir("Psz", MyF.Oszlopnév(oszlop) + 5.ToString());
                MyX.Kiir("Kijelölve", MyF.Oszlopnév(oszlop + 1) + 5.ToString());
                MyX.Kiir("Megfelelő", MyF.Oszlopnév(oszlop + 2) + 5.ToString());
                MyX.Kiir("Nem Megfelelő", MyF.Oszlopnév(oszlop + 3) + 5.ToString());
                MyX.Kiir("Graffiti (m2)", MyF.Oszlopnév(oszlop + 4) + 5.ToString());
                MyX.Kiir("Eseti (m2)", MyF.Oszlopnév(oszlop + 5) + 5.ToString());
                MyX.Kiir("Fertőtlenítés (m2)", MyF.Oszlopnév(oszlop + 6) + 5.ToString());

                MyX.Egyesít(munkalap, MyF.Oszlopnév(oszlop + 1) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                MyX.Kiir(rekordkieg.Trim(), MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                sor += 1;
                blokkeleje = sor;

                List<Adat_Jármű> AdatokJárműSzűrt = (from a in AdatokJármű
                                                     where a.Típus == rekordkieg
                                                     orderby a.Azonosító ascending
                                                     select a).ToList();

                foreach (Adat_Jármű rekord in AdatokJárműSzűrt)
                {

                    if (sor == 5)
                    {
                        // elkészítjük a fejlécet
                        MyX.Kiir("Psz", MyF.Oszlopnév(oszlop) + $"{sor}");
                        MyX.Kiir("Kijelölve", MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                        MyX.Kiir("Megfelelő", MyF.Oszlopnév(oszlop + 2) + $"{sor}");
                        MyX.Kiir("Nem Megfelelő", MyF.Oszlopnév(oszlop + 3) + $"{sor}");
                        MyX.Kiir("Graffiti (m2)", MyF.Oszlopnév(oszlop + 4) + $"{sor}");
                        MyX.Kiir("Eseti (m2)", MyF.Oszlopnév(oszlop + 5) + $"{sor}");
                        MyX.Kiir("Fertőtlenítés (m2)", MyF.Oszlopnév(oszlop + 6) + $"{sor}");

                        sor += 1;
                    }

                    MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(oszlop) + $"{sor}");

                    DateTime IdeigDátum = new DateTime(Dátum.Year, Dátum.Month, Dátum.Day, 14, 0, 0);
                    DateTime NullaDátum = new DateTime(1900, 1, 1, 0, 0, 0);
                    Adat_Főkönyv_Nap ElemNap;
                    if (napszak.Trim() == "de")
                        ElemNap = (from a in AdatokFőkönyvNap
                                   where a.Azonosító == rekord.Azonosító &&
                                   a.Tervérkezés < IdeigDátum &&
                                   a.Tervérkezés != NullaDátum
                                   select a).FirstOrDefault();
                    else
                        ElemNap = (from a in AdatokFőkönyvNap
                                   where a.Azonosító == rekord.Azonosító &&
                                   a.Tervérkezés >= IdeigDátum
                                   select a).FirstOrDefault();
                    if (ElemNap != null) MyX.Kiir("X", MyF.Oszlopnév(oszlop + 1) + $"{sor}");

                    Adat_Jármű_Vendég VendégAdat = (from a in AdatokFőVendég
                                                    where a.Azonosító == rekord.Azonosító
                                                    select a).FirstOrDefault();

                    if (VendégAdat != null)
                    {
                        if (Cmbtelephely.Trim() != VendégAdat.KiadóTelephely)
                        {
                           MyX.Kiir("", MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                            MyX.Egyesít(munkalap, MyF.Oszlopnév(oszlop + 2) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                            MyX.Kiir(VendégAdat.KiadóTelephely, MyF.Oszlopnév(oszlop + 2) + $"{sor}");
                        }
                    }

                    sor += 1;
                    if (sor >= 46)
                    {
                        MyX.Kiir("Össz", MyF.Oszlopnév(oszlop) + $"{sor}");
                        MyX.Kiir("#KÉPLET#=COUNTIF(R[-" + Math.Abs(sor - blokkeleje).ToString() + "]C:R[-1]C,\"X\")", MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                        MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlop) + blokkeleje.ToString() + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                        MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlop) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");

                        sor = 5;
                        blokkeleje = 6;
                        oszlop += 7;
                        oszlopismét += 1;
                    }
                }


                // ha vége a típusnak akkor összesítünk
                if (sor >= 43)
                {
                    sor = 46;
                    MyX.Kiir("Össz", MyF.Oszlopnév(oszlop) + $"{sor}");
                    MyX.Kiir("#KÉPLET#=COUNTIF(R[-" + Math.Abs(sor - blokkeleje).ToString() + "]C:R[-1]C,\"X\")", MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlop) + blokkeleje.ToString() + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");

                    sor = 6;
                    blokkeleje = 6;
                    oszlop += 7;
                    oszlopismét += 1;
                }
                else
                {
                    sor += 3;
                    MyX.Kiir("Össz", MyF.Oszlopnév(oszlop) + $"{sor}");
                    MyX.Kiir("#KÉPLET#=COUNTIF(R[-" + Math.Abs(sor - blokkeleje).ToString() + "]C:R[-1]C,\"X\")", MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlop) + blokkeleje.ToString() + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                    sor += 1;
                }
            }



            // **************************************************************
            // ha van olyan jármű ami másik telephelyről jött, akkor kiírjuk
            // '**************************************************************

            List<Adat_Jármű_Vendég> VAdatok = (from a in AdatokFőVendég
                                               where a.KiadóTelephely == Cmbtelephely.Trim()
                                               orderby a.Típus, a.Azonosító
                                               select a).ToList();

            if (sor >= 43)
            {
                sor = 6;
                blokkeleje = 6;
                oszlop += 7;
                oszlopismét += 1;
            }
            else
            {
                blokkeleje = sor;
                sor += 1;
            }
            string előzőtípus = "";


            foreach (Adat_Jármű_Vendég rekord in VAdatok)
            {
                if (sor == 5)
                {
                    // elkészítjük a fejlécet
                    MyX.Kiir("Psz", MyF.Oszlopnév(oszlop) + 5.ToString());
                    MyX.Kiir("Kijelölve", MyF.Oszlopnév(oszlop + 1) + 5.ToString());
                    MyX.Kiir("Megfelelő", MyF.Oszlopnév(oszlop + 2) + 5.ToString());
                    MyX.Kiir("Nem Megfelelő", MyF.Oszlopnév(oszlop + 3) + 5.ToString());
                    MyX.Kiir("Graffiti (m2)", MyF.Oszlopnév(oszlop + 4) + 5.ToString());
                    MyX.Kiir("Eseti (m2)", MyF.Oszlopnév(oszlop + 5) + 5.ToString());
                    MyX.Kiir("Fertőtlenítés (m2)", MyF.Oszlopnév(oszlop + 6) + 5.ToString());
                    sor += 1;
                }
                if (előzőtípus.Trim() != rekord.Típus.Trim())
                {

                    MyX.Egyesít(munkalap, MyF.Oszlopnév(oszlop + 2) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                    MyX.Kiir(rekord.Típus.Trim(), MyF.Oszlopnév(oszlop + 2) + $"{sor}");
                    előzőtípus = rekord.Típus.Trim();
                    blokkeleje = sor;
                    sor += 1;
                }
                MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(oszlop) + $"{sor}");

                sor += 1;

                if (sor == 46)
                {
                    MyX.Kiir("Össz", MyF.Oszlopnév(oszlop) + $"{sor}");
                    MyX.Kiir("#KÉPLET#=COUNTIF(R[-" + Math.Abs(sor - blokkeleje).ToString() + "]C:R[-1]C,\"X\")", MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlop) + blokkeleje.ToString() + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                    sor = 6;
                    blokkeleje = 6;
                    oszlop += 7;
                    oszlopismét += 1;
                }
            }

            if (sor > 45)
            {
                MyX.Kiir("Össz", MyF.Oszlopnév(oszlop) + $"{sor}");
                MyX.Kiir("#KÉPLET#=COUNTIF(R[-" + Math.Abs(sor - blokkeleje).ToString() + "]C:R[-1]C,\"X\")", MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlop) + blokkeleje.ToString() + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                sor = 5;
                blokkeleje = 6;
                oszlop += 7;
                oszlopismét += 1;
            }
            else
            {
                sor += 3;
            }

            MyX.Kiir("Össz", MyF.Oszlopnév(oszlop) + $"{sor}");
            MyX.Kiir("#KÉPLET#=COUNTIF(R[-" + Math.Abs(sor - blokkeleje).ToString() + "]C:R[-1]C,\"X\")", MyF.Oszlopnév(oszlop + 1) + $"{sor}");
            MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlop) + blokkeleje.ToString() + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlop) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");

            // **************************************************************
            // ha van olyan jármű ami másik telephelyről jött, akkor kiírjuk vége
            // **************************************************************

            // Maradék rácsozás
            if (sor < 46)
            {
                MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlop) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + "46");
            }
            MyX.Munkalap_betű(munkalap, BeBetű);


            // első sor állítva
            MyX.SzövegIrány(munkalap, $"A5:{MyF.Oszlopnév(oszlopismét * 8)}5", 90);
            MyX.Sormagasság(munkalap, "5:5", 175);

            // összes oszlopszélesség 6
            MyX.Oszlopszélesség(munkalap, "a:" + MyF.Oszlopnév(oszlopismét * 7), 6);

            for (int j = 0; j < oszlopismét; j++)
            {
                // beállítjuk az oszlop psz szélességeket
                MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(1 + j * 7) + ":" + MyF.Oszlopnév(1 + j * 7), 15);

                // rácsozzuk
                MyX.Rácsoz(munkalap, MyF.Oszlopnév(1 + j * 7) + "5:" + MyF.Oszlopnév(7 + j * 7) + "5");
            }

            if (oszlopismét < 3)
                oszlopismét = 3;
            Beállítás_Nyomtatás BeNYom = new Beállítás_Nyomtatás
            {
                Munkalap = munkalap,
                NyomtatásiTerület = $"A1:{MyF.Oszlopnév(7 + (oszlopismét - 1) * 8)}65",
                LapMagas = 1,
                LapSzéles = 1,
                BalMargó = 10,
                JobbMargó = 10,
                AlsóMargó = 15,
                FelsőMargó = 19,
                LáblécMéret = 8,
                FejlécMéret = 8
            };
            MyX.NyomtatásiTerület_részletes(munkalap, BeNYom);

            if (napszak == "de")
                MyX.Kiir("J1 takarítás NAPPAL", "a3");
            else
                MyX.Kiir("J1 takarítás ÉJSZAKA", "a3");

            MyX.Kiir(Dátum.ToString("yyyy.MM") + " hó " + Dátum.ToString("dd") + " nap", "a1");

            MyX.Kiir("Előírt létszám:  …… fő, megjelent :……. Fő", "A49");
            MyX.Kiir("Cégjelzéses munkaruhát nem viselt : …….. Fő", "a51");

            MyX.Egyesít(munkalap, "a53:p53");
            MyX.Kiir("A megrendelő a napi munkatervet átadta: ……….óra……….perckor. ", "A53");
            MyX.Egyesít(munkalap, "a58:e58");
            MyX.Egyesít(munkalap, "g58:k58");
            MyX.Kiir("BKV ZRT.", "a58");
            MyX.Kiir("Vállalkozó", "g58");
            MyX.Pontvonal(munkalap, "A58:E58");
            MyX.Pontvonal(munkalap, "G58:K58");


            MyX.Egyesít(munkalap, "a59:p59");
            MyX.Kiir("A vállalkozó a napi munkafeladatok elvégzését lejelentette:", "A59");
            MyX.Egyesít(munkalap, "a61:p61");
            MyX.Kiir("  ……….óra……….perckor.", "a61");

            MyX.Egyesít(munkalap, "a65:e65");
            MyX.Egyesít(munkalap, "g65:k65");
            MyX.Kiir("BKV ZRT.", "a65");
            MyX.Kiir("Vállalkozó", "g65");
            MyX.Pontvonal(munkalap, "A65:E65");
            MyX.Pontvonal(munkalap, "G65:K65");

            MyX.Kiir(Dátum.ToString("yyyy.MM") + " hó " + Dátum.ToString("dd") + " nap", "a1");
        }

        private void Összevont(string tétel, DateTime Dátum, string napszak,
            List<Adat_Jármű> AdatokJármű, List<Adat_Főkönyv_Nap> AdatokFőkönyvNap, List<string> AdatokTakarításTípus, List<Adat_Jármű_Vendég> AdatokFőVendég)
        {

            if (tétel.Trim() == "J1")
            {
                munkalap = "J1_J2_J3";
                MyX.Munkalap_aktív(munkalap);

                MyX.Kiir("J1 takarítás Nappal", "A4");
                MyX.Kiir("J2 takarítás Nappal", "H4");
                MyX.Kiir("J3 takarítás Nappal", "O4");
            }
            else
            {
                munkalap = "J4_J5_J6";
                MyX.Munkalap_aktív(munkalap);

                MyX.Kiir("J4 takarítás Nappal", "A4");
                MyX.Kiir("J5 takarítás Nappal", "H4");
                MyX.Kiir("J6 takarítás Nappal", "O4");
            }


            // minden cella

            MyX.Sormagasság(munkalap, "5:5", 175);

            MyX.Oszlopszélesség(munkalap, "A:A", 10);
            MyX.Oszlopszélesség(munkalap, "H:H", 10);
            MyX.Oszlopszélesség(munkalap, "O:O", 10);
            MyX.Oszlopszélesség(munkalap, "B:G", 6);
            MyX.Oszlopszélesség(munkalap, "I:N", 6);
            MyX.Oszlopszélesség(munkalap, "P:U", 6);


            MyX.Egyesít(munkalap, "a4:g4");
            MyX.Egyesít(munkalap, "h4:n4");
            MyX.Egyesít(munkalap, "o4:u4");
            MyX.Vastagkeret(munkalap, "a4:g4");
            MyX.Vastagkeret(munkalap, "h4:n4");
            MyX.Vastagkeret(munkalap, "o4:u4");
            int sor, oszlop;

            sor = 5;
            oszlop = 1;
            for (int szorzó = 0; szorzó <= 2; szorzó++)
            {
                // elkészítjük a fejlécet
                MyX.Kiir("Psz", MyF.Oszlopnév(oszlop + 7 * szorzó) + $"{sor}");
                MyX.Kiir("Kijelölve", MyF.Oszlopnév(oszlop + 7 * szorzó + 1) + $"{sor}");
                MyX.Kiir("Megfelelő", MyF.Oszlopnév(oszlop + 7 * szorzó + 2) + $"{sor}");
                MyX.Kiir("Nem Megfelelő", MyF.Oszlopnév(oszlop + 7 * szorzó + 3) + $"{sor}");
                MyX.Kiir("Graffiti (m2)", MyF.Oszlopnév(oszlop + 7 * szorzó + 4) + $"{sor}");
                MyX.Kiir("Eseti (m2)", MyF.Oszlopnév(oszlop + 7 * szorzó + 5) + $"{sor}");
                MyX.Kiir("Fertőtlenítés (m2)", MyF.Oszlopnév(oszlop + 7 * szorzó + 6) + $"{sor}");

            }
            string előzőtípus = "";

            foreach (string rekordkieg in AdatokTakarításTípus)
            {

                if (előzőtípus.Trim() != rekordkieg.Trim())
                {
                    // ha vége a típusnak, akkor kiírjuk az összesen sort
                    if (!string.IsNullOrEmpty(előzőtípus.Trim()))
                    {
                        // az utolsó után 3 üres sor
                        sor += 4;

                        MyX.Kiir("Össz", "a" + $"{sor}");
                        MyX.Kiir("Össz", "h" + $"{sor}");
                        MyX.Kiir("Össz", "o" + $"{sor}");
                        MyX.Kiir("1", "AA" + $"{sor}");
                    }

                    sor += 1;
                    MyX.Egyesít(munkalap, "a" + $"{sor}" + ":g" + $"{sor}");
                    MyX.Egyesít(munkalap, "h" + $"{sor}" + ":n" + $"{sor}");
                    MyX.Egyesít(munkalap, "o" + $"{sor}" + ":u" + $"{sor}");

                    MyX.Kiir(rekordkieg.Trim(), "a" + $"{sor}");
                    MyX.Kiir(rekordkieg.Trim(), "h" + $"{sor}");
                    MyX.Kiir(rekordkieg.Trim(), "o" + $"{sor}");

                    MyX.Kiir("1", "AA" + $"{sor}");
                    előzőtípus = rekordkieg.Trim();
                }

                List<Adat_Jármű> AdatokJárműSzűrt = (from a in AdatokJármű
                                                     where a.Típus == rekordkieg
                                                     orderby a.Azonosító ascending
                                                     select a).ToList();

                foreach (Adat_Jármű rekord in AdatokJárműSzűrt)
                {
                    sor += 1;

                    MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(oszlop) + $"{sor}");
                    MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(oszlop + 7) + $"{sor}");
                    MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(oszlop + 14) + $"{sor}");
                    MyX.Kiir("0", "AA" + $"{sor}");

                    DateTime IdeigDátum = new DateTime(Dátum.Year, Dátum.Month, Dátum.Day, 14, 0, 0);
                    DateTime NullaDátum = new DateTime(1900, 1, 1, 0, 0, 0);
                    Adat_Főkönyv_Nap ElemNap;
                    if (napszak.Trim() == "de")
                        ElemNap = (from a in AdatokFőkönyvNap
                                   where a.Azonosító == rekord.Azonosító &&
                                   a.Tervérkezés < IdeigDátum &&
                                   a.Tervérkezés != NullaDátum
                                   select a).FirstOrDefault();
                    else
                        ElemNap = (from a in AdatokFőkönyvNap
                                   where a.Azonosító == rekord.Azonosító &&
                                   a.Tervérkezés >= IdeigDátum
                                   select a).FirstOrDefault();
                    // Ha volt forgalomban akkor csak a J1-hez írja be
                    if (ElemNap != null && tétel.Trim() == "J1") MyX.Kiir("X", MyF.Oszlopnév(oszlop + 1) + $"{sor}");

                    Adat_Jármű_Vendég VendégAdat = (from a in AdatokFőVendég
                                                    where a.Azonosító == rekord.Azonosító
                                                    select a).FirstOrDefault();

                    if (VendégAdat != null)
                    {
                        MyX.Kiir("", MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                        MyX.Egyesít(munkalap, MyF.Oszlopnév(oszlop + 2) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                        MyX.Kiir(VendégAdat.KiadóTelephely, MyF.Oszlopnév(oszlop + 2) + $"{sor}");
                    }
                }
            }

            sor += 1;
            MyX.Kiir("Össz", "a" + $"{sor}");
            MyX.Kiir("Össz", "h" + $"{sor}");
            MyX.Kiir("Össz", "o" + $"{sor}");
            MyX.Kiir("1", "AA" + $"{sor}");
            // ****************************
            // formázás
            // ****************************
            MyX.Munkalap_betű(munkalap, BeBetű);

            // első sor állítva
            MyX.SzövegIrány(munkalap, $"A5:U5", 90);

            MyX.Rácsoz(munkalap, "A5:A" + $"{sor}");
            MyX.Rácsoz(munkalap, "A5:G" + $"{sor}");
            MyX.Rácsoz(munkalap, "H5:N" + $"{sor}");
            MyX.Rácsoz(munkalap, "O5:U" + $"{sor}");


            for (int j = 5; j < sor; j++)
            {
                if (MyX.Beolvas(munkalap, "AA" + j.ToString()) == "1")
                {
                    MyX.Vastagkeret(munkalap, "A" + j.ToString() + ":G" + j.ToString());
                    MyX.Vastagkeret(munkalap, "H" + j.ToString() + ":N" + j.ToString());
                    MyX.Vastagkeret(munkalap, "O" + j.ToString() + ":U" + j.ToString());
                }
            }
            //kitöröljük a segédoszlopot
            MyX.OszlopTörlés(munkalap, "AA");
            Beállítás_Nyomtatás BeNYom = new Beállítás_Nyomtatás
            {
                Munkalap = munkalap,
                NyomtatásiTerület = $"A1:U{sor}",
                LapSzéles = 1,
                BalMargó = 10,
                JobbMargó = 10,
                AlsóMargó = 15,
                FelsőMargó = 19,
                LáblécMéret = 8,
                FejlécMéret = 8,
                IsmétlődőSorok = "$1:$5"
            };
            MyX.NyomtatásiTerület_részletes(munkalap, BeNYom);

            sor += 2;
            MyX.Kiir("Előírt létszám:  …… fő, megjelent :……. Fő", "A" + $"{sor}");
            sor += 2;
            MyX.Kiir("Cégjelzéses munkaruhát nem viselt : …….. Fő", "a" + $"{sor}");
            sor += 2;
            MyX.Egyesít(munkalap, "a" + $"{sor}" + ":U" + $"{sor}");
            MyX.Kiir("A megrendelő a napi munkatervet átadta: ……….óra……….perckor. ", "A" + $"{sor}");
            sor += 5;
            MyX.Egyesít(munkalap, "a" + $"{sor}" + ":e" + $"{sor}");
            MyX.Egyesít(munkalap, "g" + $"{sor}" + ":k" + $"{sor}");

            MyX.Kiir("BKV ZRT.", "a" + $"{sor}");
            MyX.Kiir("Vállalkozó", "g" + $"{sor}");
            MyX.Pontvonal(munkalap, "a" + $"{sor}" + ":E" + $"{sor}");
            MyX.Pontvonal(munkalap, "g" + $"{sor}" + ":K" + $"{sor}");
            sor += 2;
            MyX.Egyesít(munkalap, "a" + $"{sor}" + ":u" + $"{sor}");
            MyX.Kiir("A vállalkozó a napi munkafeladatok elvégzését lejelentette:  ……….óra……….perckor.", "A" + $"{sor}");

            sor += 5;
            MyX.Egyesít(munkalap, "a" + $"{sor}" + ":e" + $"{sor}");
            MyX.Egyesít(munkalap, "g" + $"{sor}" + ":k" + $"{sor}");
            MyX.Kiir("BKV ZRT.", "a" + $"{sor}");
            MyX.Kiir("Vállalkozó", "g" + $"{sor}");
            MyX.Pontvonal(munkalap, "a" + $"{sor}" + ":E" + $"{sor}");
            MyX.Pontvonal(munkalap, "g" + $"{sor}" + ":K" + $"{sor}");

            if (napszak.Trim() == "de")
                MyX.Kiir("Takarítás NAPPAL", "O1");
            else
                MyX.Kiir("Takarítás ÉJSZAKA", "O1");

            MyX.Kiir(Dátum.ToString("yyyy.MM") + " hó " + Dátum.ToString("dd") + " nap", "a1");
        }
    }
}

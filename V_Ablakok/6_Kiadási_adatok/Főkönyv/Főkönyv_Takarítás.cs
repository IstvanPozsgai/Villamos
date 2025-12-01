using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.Villamos_Nyomtatványok
{
    public class Főkönyv_Takarítás
    {
        public void Takarítás_Excel(string fájlexc, string Cmbtelephely, DateTime Dátum, string napszak, List<string> AdatokTakarításTípus,
                                 List<Adat_Jármű> AdatokJármű,
                                 List<Adat_Főkönyv_Nap> AdatokFőkönyvNap,
                                 List<Adat_Jármű_Vendég> AdatokFőVendég,
                                 List<Adat_Főkönyv_ZSER> AdatokFőkönyvZSER)
        {
            MyX.ExcelLétrehozás();

            // létrehozzunk annyi lapfület amennyi típusm kell
            if (AdatokTakarításTípus != null && AdatokTakarításTípus.Count > 0)
            {
                MyX.Munkalap_átnevezés("Munka1", "Takarítás");
                MyX.Új_munkalap("Nappalos Igazoló");
                MyX.Új_munkalap("Összes_állományi");
                MyX.Új_munkalap("J1_J2_J3");
                MyX.Új_munkalap("J4_J5_J6");

                // munkalapok létrehozása
                foreach (string rekordkieg in AdatokTakarításTípus)
                {
                    MyX.Új_munkalap(rekordkieg.Trim());
                    MyX.Új_munkalap(rekordkieg.Trim() + "_Üres");
                }

            }

            Söpréslapok(napszak, Dátum, Cmbtelephely, AdatokTakarításTípus, AdatokJármű, AdatokFőkönyvNap, AdatokFőVendég);
            Üreslapok(Dátum, napszak, AdatokTakarításTípus, AdatokJármű);
            EstiBeállók(Dátum, napszak, AdatokFőkönyvZSER);
            Összes_takarítás_kocsi(Cmbtelephely, Dátum, napszak, AdatokTakarításTípus, AdatokJármű, AdatokFőkönyvNap, AdatokFőVendég);
            Takarítás_igazoló(Cmbtelephely, Dátum);
            Összevont("J1", Dátum, napszak, AdatokJármű, AdatokFőkönyvNap, AdatokTakarításTípus, AdatokFőVendég);
            Összevont("J4", Dátum, napszak, AdatokJármű, AdatokFőkönyvNap, AdatokTakarításTípus, AdatokFőVendég);

            // bezárjuk az Excel-t
            MyX.Munkalap_aktív("Takarítás");
            MyX.Aktív_Cella("Takarítás", "A1");

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
                MyX.Munkalap_aktív(rekordkieg.Trim());
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


                // minden cella
                MyX.Munkalap_betű("Arial", 20);

                // első sor állítva
                MyX.SzövegIrány(rekordkieg.Trim(), "5:5", 90);
                MyX.Sormagasság("5:5", 175);

                // összes oszlopszélesség 7
                MyX.Oszlopszélesség(rekordkieg.Trim(), "a:" + MyF.Oszlopnév(oszlopismét * 7), 6);

                for (int j = 0; j < oszlopismét; j++)
                {
                    // beállítjuk az oszlop psz szélességeket
                    MyX.Oszlopszélesség(rekordkieg.Trim(), MyF.Oszlopnév(1 + j * 8) + ":" + MyF.Oszlopnév(1 + j * 8), 10);

                    // rácsozzuk
                    MyX.Rácsoz(MyF.Oszlopnév(1 + j * 8) + "5:" + MyF.Oszlopnév(7 + j * 8) + "46");
                    MyX.Vastagkeret(MyF.Oszlopnév(1 + j * 8) + "5:" + MyF.Oszlopnév(7 + j * 8) + "5");
                    MyX.Vastagkeret(MyF.Oszlopnév(1 + j * 8) + "46:" + MyF.Oszlopnév(7 + j * 8) + "46");
                    MyX.Kiir("=COUNTA(R[-40]C:R[-1]C)", MyF.Oszlopnév(2 + j * 8) + "46");
                }
                MyX.Sormagasság("46:47", 30);
                MyX.Vastagkeret(MyF.Oszlopnév(1 + (oszlopismét - 1) * 8) + "47:" + MyF.Oszlopnév(7 + (oszlopismét - 1) * 8) + "47");
                MyX.Kiir("Össz", "A46");
                MyX.Kiir("Össz", MyF.Oszlopnév(1 + (oszlopismét - 1) * 8) + "47");
                MyX.Kiir("=SUM(R[-1])", MyF.Oszlopnév(2 + (oszlopismét - 1) * 8) + "47");


                if (oszlopismét < 3)
                    oszlopismét = 3;

                MyX.NyomtatásiTerület_részletes(rekordkieg.Trim(), "A1:" + MyF.Oszlopnév(7 + (oszlopismét - 1) * 8) + "65", 10, 10,
                    15, 19, 8, 8, "1", "1", true, "A4", false, false);

                if (napszak == "de")
                    MyX.Kiir(rekordkieg.Trim() + "    ©J1     takarítás Nappal", "a3");
                else
                    MyX.Kiir(rekordkieg.Trim() + "    ©J1     takarítás ÉJSZAKA", "a3");



                MyX.Kiir("Előírt létszám:  …… fő, megjelent :……. Fő", "A49");
                MyX.Kiir("Cégjelzéses munkaruhát nem viselt : …….. Fő", "a51");

                MyX.Egyesít(rekordkieg.Trim(), "a53:p53");
                MyX.Kiir("A megrendelő a napi munkatervet átadta: ……….óra……….perckor. ", "A53");
                MyX.Egyesít(rekordkieg.Trim(), "a58:e58");
                MyX.Egyesít(rekordkieg.Trim(), "g58:k58");
                MyX.Kiir("BKV ZRT.", "a58");
                MyX.Kiir("Vállalkozó", "g58");
                MyX.Pontvonal("a58:e58");
                MyX.Pontvonal("g58:k58");

                MyX.Egyesít(rekordkieg.Trim(), "a59:p59");
                MyX.Kiir("A vállalkozó a napi munkafeladatok elvégzését lejelentette:", "A59");
                MyX.Egyesít(rekordkieg.Trim(), "a61:p61");
                MyX.Kiir("  ……….óra……….perckor.", "a61");


                MyX.Egyesít(rekordkieg.Trim(), "a65:e65");
                MyX.Egyesít(rekordkieg.Trim(), "g65:k65");
                MyX.Kiir("BKV ZRT.", "a65");
                MyX.Kiir("Vállalkozó", "g65");
                MyX.Pontvonal("a65:e65");
                MyX.Pontvonal("g65:k65");

                MyX.Kiir(Dátum.ToString("yyyy.MM") + " hó " + Dátum.ToString("dd") + " nap", "a1");
                MyX.Aktív_Cella(rekordkieg.Trim(), "A1");
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
                string munkalap = rekordkieg.Trim() + "_Üres";
                MyX.Munkalap_aktív(rekordkieg.Trim() + "_Üres");

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

                // minden cella
                MyX.Munkalap_betű("Arial", 20);

                // első sor állítva
                MyX.SzövegIrány(munkalap, "5:5", 90);
                MyX.Sormagasság("5:5", 175);

                // összes oszlopszélesség 7
                MyX.Oszlopszélesség(munkalap, "a:" + MyF.Oszlopnév(oszlopismét * 7), 6);

                for (int j = 0; j < oszlopismét; j++)
                {
                    // beállítjuk az oszlop psz szélességeket
                    MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(1 + j * 8) + ":" + MyF.Oszlopnév(1 + j * 8), 10);

                    // rácsozzuk
                    MyX.Rácsoz(MyF.Oszlopnév(1 + j * 8) + "5:" + MyF.Oszlopnév(7 + j * 8) + "46");
                    MyX.Vastagkeret(MyF.Oszlopnév(1 + j * 8) + "5:" + MyF.Oszlopnév(7 + j * 8) + "5");
                    MyX.Vastagkeret(MyF.Oszlopnév(1 + j * 8) + "46:" + MyF.Oszlopnév(7 + j * 8) + "46");

                }
                MyX.Sormagasság("46:47", 30);
                MyX.Vastagkeret(MyF.Oszlopnév(1 + (oszlopismét - 1) * 8) + "47:" + MyF.Oszlopnév(7 + (oszlopismét - 1) * 8) + "47");
                MyX.Kiir("Össz", "A46");
                MyX.Kiir("Össz", MyF.Oszlopnév(1 + (oszlopismét - 1) * 8) + "47");

                if (oszlopismét < 3)
                    oszlopismét = 3;
                MyX.NyomtatásiTerület_részletes(munkalap, "A1:" + MyF.Oszlopnév(7 + (oszlopismét - 1) * 8) + "65", 10, 10,
                                15, 19, 8, 8, "1", "1", true, "A4", false, false);

                if (napszak == "de")
                    MyX.Kiir(rekordkieg.Trim() + "    ©J1     takarítás Nappal", "a3");
                else
                    MyX.Kiir(rekordkieg.Trim() + "    ©J1     takarítás ÉJSZAKA", "a3");



                MyX.Kiir("Előírt létszám:  …… fő, megjelent :……. Fő", "A49");
                MyX.Kiir("Cégjelzéses munkaruhát nem viselt : …….. Fő", "a51");

                MyX.Egyesít(munkalap, "A53:P53");
                MyX.Kiir("A megrendelő a napi munkatervet átadta: ……….óra……….perckor. ", "A53");
                MyX.Egyesít(munkalap, "a58:e58");
                MyX.Egyesít(munkalap, "g58:k58");
                MyX.Kiir("BKV ZRT.", "a58");
                MyX.Kiir("Vállalkozó", "g58");
                MyX.Pontvonal("a58:e58");
                MyX.Pontvonal("g58:k58");

                MyX.Egyesít(munkalap, "a59:p59");
                MyX.Kiir("A vállalkozó a napi munkafeladatok elvégzését lejelentette:", "A59");
                MyX.Egyesít(munkalap, "a61:p61");
                MyX.Kiir("  ……….óra……….perckor.", "a61");


                MyX.Egyesít(munkalap, "a65:e65");
                MyX.Egyesít(munkalap, "g65:k65");
                MyX.Kiir("BKV ZRT.", "a65");
                MyX.Kiir("Vállalkozó", "g65");
                MyX.Pontvonal("a65:e65");
                MyX.Pontvonal("g65:k65");

                MyX.Kiir(Dátum.ToString("yyyy.MM") + " hó " + Dátum.ToString("dd") + " nap", "a1");
                MyX.Aktív_Cella(munkalap, "A1");
            }
        }

        private void EstiBeállók(DateTime Dátum, string napszak, List<Adat_Főkönyv_ZSER> AdatokFőkönyvZSER)
        {

            // ******************************
            // *  ESti beállók              *
            // ******************************

            int sor;
            string munkalap = "Takarítás";
            MyX.Munkalap_aktív(munkalap);
            // minden cella
            MyX.Munkalap_betű("Arial", 14);

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
            MyX.NyomtatásiTerület_részletes("Takarítás", "A1:H" + $"{sor}", "", "", true);

            MyX.Kiir(Dátum.ToString("yyyy.MM") + " hó " + Dátum.ToString("dd") + " nap", "a1");
            MyX.Aktív_Cella("Takarítás", "A1");
        }

        private void Takarítás_igazoló(string Cmbtelephely, DateTime Dátum)
        {
            string munkalap = "Nappalos Igazoló";
            MyX.Munkalap_aktív(munkalap);

            // Betűméret
            MyX.Munkalap_betű("Calibri", 12);

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
            MyX.Rácsoz("a1:c3");

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
                    MyX.Sormagasság(sor.ToString() + ":" + $"{sor}", 25);
                }
                else
                {
                    // fejlécet készít és befejezi az előző táblát
                    if (sor != 5)
                    {
                        vége = sor + 3;
                        MyX.Sormagasság(sor.ToString() + ":" + (sor + 3).ToString(), 25);
                        sor += 4;
                    }
                    if (eleje == 5 & vége == 5)
                    {
                    }
                    // első alkalommal nem fejezi be az előző táblázatot
                    else
                    {
                        // befejezi az előző táblát
                        MyX.Rácsoz("a" + eleje.ToString() + ":i" + vége.ToString());
                        MyX.Vastagkeret("a" + eleje.ToString() + ":i" + vége.ToString());
                        MyX.Vastagkeret("a" + (eleje + 1).ToString() + ":i" + vége.ToString());
                    }

                    takarítási_fajta = rekord.Takarítási_fajta.Trim();
                    MyX.Betű("a" + $"{sor}", 16);

                    MyX.Kiir(takarítási_fajta.Trim(), "a" + $"{sor}");
                    sor += 1;
                    eleje = sor;


                    // fejléc
                    MyX.Sormagasság(sor.ToString() + ":" + $"{sor}", 48);
                    MyX.Kiir("Jármű biztosításának ideje", "b" + $"{sor}");
                    MyX.Kiir("Takarítás befejezésének ideje", "c" + $"{sor}");

                    MyX.Kiir("Megfelelt", "d" + $"{sor}");
                    MyX.Kiir("Nem Megfelelt", "e" + $"{sor}");
                    MyX.Kiir("Pót határidő", "f" + $"{sor}");
                    MyX.Kiir("Megfelelt", "g" + $"{sor}");
                    MyX.Kiir("Nem Megfelelt", "h" + $"{sor}");
                    MyX.Kiir("Igazolta", "i" + $"{sor}");
                    MyX.Sortörésseltöbbsorba(sor.ToString() + ":" + $"{sor}");
                    // első kocsi
                    sor += 1;
                    MyX.Kiir(rekord.Azonosító.Trim(), "a" + $"{sor}");
                    MyX.Sormagasság(sor.ToString() + ":" + $"{sor}", 25);
                }
            }


            // befejezi az előző tábláta  

            vége = sor + 3;
            MyX.Sormagasság(sor.ToString() + ":" + (sor + 3).ToString(), 25);
            MyX.Rácsoz("a" + eleje.ToString() + ":i" + vége.ToString());
            MyX.Vastagkeret("a" + eleje.ToString() + ":i" + vége.ToString());
            MyX.Vastagkeret("a" + (eleje + 1).ToString() + ":i" + vége.ToString());


            // Aláírás lábléc
            sor += 5;
            MyX.Egyesít(munkalap, "a" + $"{sor}" + ":g" + $"{sor}");
            MyX.Kiir("A megrendelő a napi munkatervet átadta: ……….óra……….perckor. ", "A" + $"{sor}");
            sor += 5;
            MyX.Egyesít(munkalap, "a" + $"{sor}" + ":d" + $"{sor}");
            MyX.Egyesít(munkalap, "f" + $"{sor}" + ":i" + $"{sor}");
            MyX.Kiir("BKV ZRT.", "a" + $"{sor}");
            MyX.Kiir("Vállalkozó", "f" + $"{sor}");
            MyX.Pontvonal("a" + $"{sor}");
            MyX.Pontvonal("f" + $"{sor}");

            sor += 2;
            MyX.Egyesít(munkalap, "a" + $"{sor}" + ":g" + $"{sor}");
            MyX.Kiir("A vállalkozó a napi munkafeladatok elvégzését lejelentette:  ……….óra……….perckor.", "a" + $"{sor}");

            sor += 5;
            MyX.Egyesít(munkalap, "a" + $"{sor}" + ":d" + $"{sor}");
            MyX.Egyesít(munkalap, "f" + $"{sor}" + ":i" + $"{sor}");
            MyX.Kiir("BKV ZRT.", "a" + $"{sor}");
            MyX.Kiir("Vállalkozó", "f" + $"{sor}");
            MyX.Pontvonal("a" + $"{sor}");
            MyX.Pontvonal("f" + $"{sor}");

            vége = sor;

            // nyomtatási beállítások
            MyX.NyomtatásiTerület_részletes(munkalap, "A1:I" + vége.ToString(), "$6:$6", "", Cmbtelephely.Trim(), "©Jármű takarítás igazolólap Nappal ",
                Dátum.ToString("yyyy.MM.dd"), "........................................\n                    BKV Zrt", "",
                "........................................\nTakarítást végző    \n", "", 18, 18,
                19, 19, 8, 8, true, false, "1", "false", true, "A4");


            MyX.Aktív_Cella(munkalap, "A1");
        }

        private void Összes_takarítás_kocsi(string Cmbtelephely, DateTime Dátum, string napszak, List<string> AdatokTakarításTípus, List<Adat_Jármű> AdatokJármű,
            List<Adat_Főkönyv_Nap> AdatokFőkönyvNap, List<Adat_Jármű_Vendég> AdatokFőVendég)
        {
            string munkalap = "Összes_állományi";
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
                MyX.Vastagkeret(MyF.Oszlopnév(oszlop) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                MyX.Vastagkeret(MyF.Oszlopnév(oszlop + 1) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
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
                        MyX.Kiir("=COUNTA(R[-" + Math.Abs(sor - blokkeleje).ToString() + "]C:R[-1]C)", MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                        MyX.Rácsoz(MyF.Oszlopnév(oszlop) + blokkeleje.ToString() + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                        MyX.Vastagkeret(MyF.Oszlopnév(oszlop) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");

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
                    MyX.Kiir("=COUNTA(R[-" + Math.Abs(sor - blokkeleje).ToString() + "]C:R[-1]C)", MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                    MyX.Rácsoz(MyF.Oszlopnév(oszlop) + blokkeleje.ToString() + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                    MyX.Vastagkeret(MyF.Oszlopnév(oszlop) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                    sor = 6;
                    blokkeleje = 6;
                    oszlop += 7;
                    oszlopismét += 1;
                }
                else
                {
                    sor += 3;
                    MyX.Kiir("Össz", MyF.Oszlopnév(oszlop) + $"{sor}");
                    MyX.Kiir("=COUNTA(R[-" + Math.Abs(sor - blokkeleje).ToString() + "]C:R[-1]C)", MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                    MyX.Rácsoz(MyF.Oszlopnév(oszlop) + blokkeleje.ToString() + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                    MyX.Vastagkeret(MyF.Oszlopnév(oszlop) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
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
                    MyX.Vastagkeret(MyF.Oszlopnév(oszlop) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                    MyX.Vastagkeret(MyF.Oszlopnév(oszlop + 1) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                    előzőtípus = rekord.Típus.Trim();
                    blokkeleje = sor;
                    sor += 1;
                }
                MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(oszlop) + $"{sor}");

                sor += 1;

                if (sor == 46)
                {
                    MyX.Kiir("Össz", MyF.Oszlopnév(oszlop) + $"{sor}");
                    MyX.Kiir("=COUNTA(R[-" + Math.Abs(sor - blokkeleje).ToString() + "]C:R[-1]C)", MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                    MyX.Rácsoz(MyF.Oszlopnév(oszlop) + blokkeleje.ToString() + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                    MyX.Vastagkeret(MyF.Oszlopnév(oszlop) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");

                    sor = 6;
                    blokkeleje = 6;
                    oszlop += 7;
                    oszlopismét += 1;
                }
            }

            if (sor > 45)
            {
                MyX.Kiir("Össz", MyF.Oszlopnév(oszlop) + $"{sor}");
                MyX.Kiir("=COUNTA(R[-" + Math.Abs(sor - blokkeleje).ToString() + "]C:R[-1]C)", MyF.Oszlopnév(oszlop + 1) + $"{sor}");
                MyX.Rácsoz(MyF.Oszlopnév(oszlop) + blokkeleje.ToString() + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
                MyX.Vastagkeret(MyF.Oszlopnév(oszlop) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
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
            MyX.Kiir("=COUNTA(R[-" + Math.Abs(sor - blokkeleje).ToString() + "]C:R[-1]C)", MyF.Oszlopnév(oszlop + 1) + $"{sor}");
            MyX.Rácsoz(MyF.Oszlopnév(oszlop) + blokkeleje.ToString() + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");
            MyX.Vastagkeret(MyF.Oszlopnév(oszlop) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + $"{sor}");

            // **************************************************************
            // ha van olyan jármű ami másik telephelyről jött, akkor kiírjuk vége
            // **************************************************************

            // Maradék rácsozás
            if (sor < 46)
            {
                MyX.Rácsoz(MyF.Oszlopnév(oszlop) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + "46");
                MyX.Vastagkeret(MyF.Oszlopnév(oszlop) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop + 6) + "46");
            }
            MyX.Munkalap_betű("Arial", 20);


            // első sor állítva
            MyX.SzövegIrány(munkalap, "5:5", 90);
            MyX.Sormagasság("5:5", 175);

            // összes oszlopszélesség 6
            MyX.Oszlopszélesség(munkalap, "a:" + MyF.Oszlopnév(oszlopismét * 7), 6);

            for (int j = 0; j < oszlopismét; j++)
            {
                // beállítjuk az oszlop psz szélességeket
                MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(1 + j * 7) + ":" + MyF.Oszlopnév(1 + j * 7), 15);

                // rácsozzuk
                MyX.Rácsoz(MyF.Oszlopnév(1 + j * 7) + "5:" + MyF.Oszlopnév(7 + j * 7) + "5");
                MyX.Vastagkeret(MyF.Oszlopnév(1 + j * 7) + "5:" + MyF.Oszlopnév(7 + j * 7) + "5");
            }

            if (oszlopismét < 3)
                oszlopismét = 3;
            MyX.NyomtatásiTerület_részletes(munkalap, "A1:" + MyF.Oszlopnév(7 + (oszlopismét - 1) * 7) + "65", 10, 10,
              15, 8, 8, 8, "1", "1");

            if (napszak == "de")
                MyX.Kiir("©J1 takarítás NAPPAL", "a3");
            else
                MyX.Kiir("©J1 takarítás ÉJSZAKA", "a3");

            MyX.Kiir(Dátum.ToString("yyyy.MM") + " hó " + Dátum.ToString("dd") + " nap", "a1");

            MyX.Kiir("Előírt létszám:  …… fő, megjelent :……. Fő", "A49");
            MyX.Kiir("Cégjelzéses munkaruhát nem viselt : …….. Fő", "a51");

            MyX.Egyesít(munkalap, "a53:p53");
            MyX.Kiir("A megrendelő a napi munkatervet átadta: ……….óra……….perckor. ", "A53");
            MyX.Egyesít(munkalap, "a58:e58");
            MyX.Egyesít(munkalap, "g58:k58");
            MyX.Kiir("BKV ZRT.", "a58");
            MyX.Kiir("Vállalkozó", "g58");
            MyX.Pontvonal("A58:E58");
            MyX.Pontvonal("G58:K58");


            MyX.Egyesít(munkalap, "a59:p59");
            MyX.Kiir("A vállalkozó a napi munkafeladatok elvégzését lejelentette:", "A59");
            MyX.Egyesít(munkalap, "a61:p61");
            MyX.Kiir("  ……….óra……….perckor.", "a61");

            MyX.Egyesít(munkalap, "a65:e65");
            MyX.Egyesít(munkalap, "g65:k65");
            MyX.Kiir("BKV ZRT.", "a65");
            MyX.Kiir("Vállalkozó", "g65");
            MyX.Pontvonal("A65:E65");
            MyX.Pontvonal("G65:K65");

            MyX.Kiir(Dátum.ToString("yyyy.MM") + " hó " + Dátum.ToString("dd") + " nap", "a1");
            MyX.Aktív_Cella(munkalap, "A1");

        }

        private void Összevont(string tétel, DateTime Dátum, string napszak,
            List<Adat_Jármű> AdatokJármű, List<Adat_Főkönyv_Nap> AdatokFőkönyvNap, List<string> AdatokTakarításTípus, List<Adat_Jármű_Vendég> AdatokFőVendég)
        {
            string munkalap;
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

            MyX.Sormagasság("5:5", 175);


            // Oszlopszélesség("A:A", 10)
            MyX.Oszlopszélesség(munkalap, "A:A", 10);
            MyX.Oszlopszélesség(munkalap, "H:H", 10);
            MyX.Oszlopszélesség(munkalap, "O:O", 10);
            MyX.Oszlopszélesség(munkalap, "B:G", 6);
            MyX.Oszlopszélesség(munkalap, "I:N", 6);
            MyX.Oszlopszélesség(munkalap, "P:U", 6);


            MyX.Egyesít(munkalap, "a4:g4");
            MyX.Egyesít(munkalap, "h4:n4");
            MyX.Egyesít(munkalap, "o4:u4");
            MyX.Vastagkeret("a4:g4");
            MyX.Vastagkeret("h4:n4");
            MyX.Vastagkeret("o4:u4");
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
            MyX.Munkalap_betű("Arial", 20);

            // első sor állítva
            MyX.SzövegIrány(munkalap, "5:5", 90);

            MyX.Rácsoz("A5:A" + $"{sor}");
            MyX.Vastagkeret("A5:A" + $"{sor}");
            MyX.Rácsoz("A5:G" + $"{sor}");
            MyX.Vastagkeret("A5:G" + $"{sor}");

            MyX.Rácsoz("H5:N" + $"{sor}");
            MyX.Vastagkeret("H5:N" + $"{sor}");

            MyX.Rácsoz("O5:U" + $"{sor}");
            MyX.Vastagkeret("O5:U" + $"{sor}");

            for (int j = 5; j < sor; j++)
            {
                if (MyX.Beolvas("AA" + j.ToString()) == "1")
                {
                    MyX.Vastagkeret("A" + j.ToString() + ":G" + j.ToString());
                    MyX.Vastagkeret("H" + j.ToString() + ":N" + j.ToString());
                    MyX.Vastagkeret("O" + j.ToString() + ":U" + j.ToString());
                }
            }

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
            MyX.Pontvonal("a" + $"{sor}" + ":E" + $"{sor}");
            MyX.Pontvonal("g" + $"{sor}" + ":K" + $"{sor}");
            sor += 2;
            MyX.Egyesít(munkalap, "a" + $"{sor}" + ":u" + $"{sor}");
            MyX.Kiir("A vállalkozó a napi munkafeladatok elvégzését lejelentette:  ……….óra……….perckor.", "A" + $"{sor}");

            sor += 5;
            MyX.Egyesít(munkalap, "a" + $"{sor}" + ":e" + $"{sor}");
            MyX.Egyesít(munkalap, "g" + $"{sor}" + ":k" + $"{sor}");
            MyX.Kiir("BKV ZRT.", "a" + $"{sor}");
            MyX.Kiir("Vállalkozó", "g" + $"{sor}");
            MyX.Pontvonal("a" + $"{sor}" + ":E" + $"{sor}");
            MyX.Pontvonal("g" + $"{sor}" + ":K" + $"{sor}");

            MyX.NyomtatásiTerület_részletes(munkalap,
                                             "A1:U" + $"{sor}",
                                             "$1:$5",
                                             "",
                                             "", "", "", "", "", "", "",
                                             10, 10,
                                             15, 19,
                                             8, 8,
                                             true, false,
                                             "1", "false", true, "A4");

            if (napszak.Trim() == "de")
                MyX.Kiir("©Takarítás NAPPAL", "o1");
            else
                MyX.Kiir("©Takarítás ÉJSZAKA", "O1");

            MyX.Kiir(Dátum.ToString("yyyy.MM") + " hó " + Dátum.ToString("dd") + " nap", "a1");

            MyX.Aktív_Cella(munkalap, "A1");
        }
    }
}

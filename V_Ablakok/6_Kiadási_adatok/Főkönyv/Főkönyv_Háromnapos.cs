using System;
using System.Collections.Generic;
using System.Linq;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;

namespace Villamos.Villamos_Nyomtatványok
{
    public class Főkönyv_Háromnapos
    {
        readonly Kezelő_jármű_hiba KézJárműHiba = new Kezelő_jármű_hiba();

        public void Három_Nyomtatvány(string fájlneve, string Cmbtelephely, string papírméret, string papírelrendezés)
        {

            MyE.ExcelLétrehozás();

            MyE.Új_munkalap("Munka2");
            MyE.Új_munkalap("Munka3");

            string[] mit = { "Hétfő-Csütörtök", "Kedd-Péntek", "Szerda-Szombat" };

            // kiírjuk a kocsikat

            List<Adat_Jármű_hiba> AdatokHiba = KézJárműHiba.Lista_Adatok(Cmbtelephely.Trim());

            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\villamos\villamos2.mdb";
            string jelszó = "pozsgaii";
            string szöveg = $"SELECT * FROM állománytábla";

            Kezelő_Jármű2 KézJármű = new Kezelő_Jármű2();
            List<Adat_Jármű_2> AdatokHárom = KézJármű.Lista_Adatok(hely, jelszó, szöveg);

            for (int j = 1; j <= 3; j++)
            {
                List<Adat_Jármű_2> AdatokSzűrt = (from a in AdatokHárom
                                                  where a.Haromnapos == j
                                                  orderby a.Azonosító ascending
                                                  select a).ToList();
                string munkalap = "Munka" + j;
                MyE.Munkalap_aktív(munkalap);
                MyE.Munkalap_betű("Arial", 12);

                int sor = 1;
                int oszlop = 1;
                int i = 1;

                foreach (Adat_Jármű_2 rekord in AdatokSzűrt)
                {
                    if (sor == 1)
                    {
                        // elkészítjük a fejlécet
                        MyE.Kiir("psz", MyE.Oszlopnév(oszlop) + $"{sor}");
                        MyE.Kiir("Hiba", MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                        MyE.Kiir("Nappal", MyE.Oszlopnév(oszlop + 2) + $"{sor}");
                        MyE.Kiir("Éjszaka", MyE.Oszlopnév(oszlop + 3) + $"{sor}");
                        sor += 1;
                    }
                    MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(oszlop) + $"{sor}");
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
                                MyE.Kiir(üzemképtelen.Substring(0, 20), MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                            else
                                MyE.Kiir(üzemképtelen, MyE.Oszlopnév(oszlop + 1) + $"{sor}");

                            MyE.Betű(MyE.Oszlopnév(oszlop + 1) + $"{sor}", false, false, true);
                        }
                        else if (beálló.Trim() != "_")
                        {
                            // ha beálló


                            if (beálló.Length > 20)
                                MyE.Kiir(beálló.Substring(0, 20), MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                            else
                                MyE.Kiir(beálló, MyE.Oszlopnév(oszlop + 1) + $"{sor}");

                            MyE.Betű(MyE.Oszlopnév(oszlop + 1) + $"{sor}", false, true, false);
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
                    MyE.Rácsoz(MyE.Oszlopnév(ii) + "1:" + MyE.Oszlopnév(ii + 3) + "26");
                    MyE.Betű(MyE.Oszlopnév(ii) + "1:" + MyE.Oszlopnév(ii) + "26", false, true, true);
                    MyE.Vastagkeret(MyE.Oszlopnév(ii) + "1:" + MyE.Oszlopnév(ii) + "26");
                    MyE.Vastagkeret(MyE.Oszlopnév(ii) + "1:" + MyE.Oszlopnév(ii + 3) + "1");
                    MyE.Vastagkeret(MyE.Oszlopnév(ii) + "1");

                }
                for (int ii = 1; ii < oszlop + 3; ii += 4)
                {
                    MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(ii) + ":" + MyE.Oszlopnév(ii + 3), 10);
                    MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(ii + 1) + ":" + MyE.Oszlopnév(ii + 1), 25);
                    MyE.Igazít_vízszintes(MyE.Oszlopnév(ii) + ":" + MyE.Oszlopnév(ii), "közép");
                }
                MyE.Sormagasság("1:26", 25);
                MyE.Betű("1:1", false, false, true);
                // nyomtatási terület
                MyE.NyomtatásiTerület_részletes(munkalap, "A1:" + MyE.Oszlopnév(oszlop + 3) + "26", "", "",
                    "&\"-,Félkövér\"&16" + mit[j - 1],
                    "&\"-,Félkövér\"&16©E2 vizsgálati",
                    "&\"Arial,Félkövér\"&16 " + DateTime.Today.ToString("yyyy.MM.dd"),
                    "&\"Arial,Normál\"&14........................................................" + '\n' + "nappalos aláírás",
                    "",
                    "&\"Arial,Normál\"&14........................................................" + '\n' + "éjszakás aláírás", "",
                    18, 18, 25, 15, 8, 8, true, true, "1", "1",
                    papírelrendezés != "Fekvő", papírméret);
                MyE.Aktív_Cella(munkalap, "A1");
            }
            // átnevezzük a lapokat

            MyE.Munkalap_átnevezés("Munka1", mit[0]);
            MyE.Munkalap_átnevezés("Munka2", mit[1]);
            MyE.Munkalap_átnevezés("Munka3", mit[2]);

            MyE.Munkalap_aktív("Hétfő-Csütörtök");

            MyE.ExcelMentés(fájlneve);
            MyE.ExcelBezárás();
            MyE.Megnyitás(fájlneve);
        }
    }
}

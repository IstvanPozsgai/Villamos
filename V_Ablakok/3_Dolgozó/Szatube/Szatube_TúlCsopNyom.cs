using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.V_Ablakok._3_Dolgozó.Szatube
{
    public class Szatube_TúlCsopNyom
    {
        readonly Beállítás_Betű BeBetuSzazados = new Beállítás_Betű() { Formátum = "0.00", Név = "Calibri" };
        readonly Beállítás_Betű BeBetuCalibri12 = new Beállítás_Betű() { Név = "Calibri", Méret = 12 };
        readonly List<string> NyomtatásiFájlok = new List<string>();
        public void TúlCsopNyomtat(Kezelő_Kiegészítő_Jelenlétiív KézJelenléti, Kezelő_Kiegészítő_főkönyvtábla KézFő, Kezelő_Dolgozó_Alap KézDolgAlap,
            string CmbTelephely, string fájlexc, DataGridView Tábla, bool Töröl)
        {
            string munkalap = "Munka1";
            MyX.ExcelLétrehozás(munkalap);
            // ************************
            // excel tábla érdemi része
            // ************************

            MyX.Munkalap_betű(munkalap, BeBetuCalibri12);

            MyX.Oszlopszélesség(munkalap, "A:A", 4);
            MyX.Oszlopszélesség(munkalap, "b:b", 30);
            MyX.Oszlopszélesség(munkalap, "c:c", 10);
            MyX.Oszlopszélesség(munkalap, "d:d", 30);
            MyX.Oszlopszélesség(munkalap, "e:f", 17);
            MyX.Oszlopszélesség(munkalap, "g:g", 10);
            MyX.Oszlopszélesség(munkalap, "h:h", 25);
            MyX.Oszlopszélesség(munkalap, "i:i", 30);
            MyX.Oszlopszélesség(munkalap, "j:j", 20);
            MyX.Oszlopszélesség(munkalap, "k:k", 25);
            // kiírjuk a szervezeteket
            MyX.Egyesít(munkalap, "i1:k1");
            MyX.Egyesít(munkalap, "i2:k2");
            MyX.Egyesít(munkalap, "i3:k3");
            List<Adat_Kiegészítő_Jelenlétiív> Adatok = KézJelenléti.Lista_Adatok(CmbTelephely);
            Adatok = Adatok.Where(a => a.Id > 1).OrderBy(a => a.Id).ToList();

            List<Adat_Kiegészítő_főkönyvtábla> FőkönyAdatok = KézFő.Lista_Adatok(CmbTelephely);

            int P = 0;
            foreach (Adat_Kiegészítő_Jelenlétiív rekord in Adatok)
            {
                if (P == 2) MyX.Kiir(rekord.Szervezet.Trim(), "i1");
                if (P == 3) MyX.Kiir(rekord.Szervezet.Trim(), "i2");
                if (P == 4) MyX.Kiir(rekord.Szervezet.Trim(), "i3");
                P++;
            }

            List<Adat_Dolgozó_Alap> DolgAdatok = KézDolgAlap.Lista_Adatok(CmbTelephely);

            // vastag vonal
            MyX.VastagFelső(munkalap, "A5:K5");

            // logó beszúrása
            MyX.Kép_beillesztés(munkalap, "A1", Application.StartupPath + @"\Főmérnökség\adatok\BKV.png", 5, 5, 0.6901, 0.788);

            MyX.Egyesít(munkalap, "a7:k7");
            MyX.Kiir("Rendkívüli munka elrendelő lap (csoportos)", "a7");
            // fejléc elkészítése
            MyX.Sortörésseltöbbsorba(munkalap, "A9:K9");
            MyX.Kiir("S.sz.", "A9");
            MyX.Kiir("Név", "B9");
            MyX.Kiir("Azonosító", "C9");
            MyX.Kiir("Munkakör", "D9");
            MyX.Kiir("A rendkívüli\nmunkavégzés \nidőpontja \n(dátum, óra)", "E9");
            MyX.Kiir("A rendkívüli \nmunkavégzés vége \n(dátum, óra)", "F9");
            MyX.Kiir("Időtartam\n(órában)", "G9");
            MyX.Kiir("Rendkívüli munka fajtája", "H9");
            MyX.Kiir("Az elvégzendő munka leírása, indoka.", "I9");
            MyX.Kiir("Megváltás módja", "J9");
            MyX.Kiir("Munkavállaló " + '\n' + "aláírása", "K9");
            //MyX.Betű("a9:k9");
            MyX.Rácsoz(munkalap, "a9:k9");
         
            int sor = 10;

            DateTime eleje;
            DateTime vége;

            for (int i = 0; i < Tábla.Rows.Count; i++)
            {
                //Holtart.Lép();
                if (Tábla.Rows[i].Selected)
                {
                    // sor formázása
                    MyX.Sormagasság(munkalap, $"{sor}:{sor}", 45);

                    // adatok kiírsa
                    MyX.Kiir($"#SZÁME#{Tábla.Rows[i].Cells[0].Value}", "a" + sor);
                    MyX.Igazít_vízszintes(munkalap, $"A{sor}", "bal");
                    MyX.Kiir(Tábla.Rows[i].Cells[2].Value.ToStrTrim(), "b" + sor);
                    MyX.Kiir($"#SZÁME#{Tábla.Rows[i].Cells[1].Value}", "c" + sor);
                    MyX.Igazít_vízszintes(munkalap, $"C{sor}", "bal");
                    string Munkakör = (from a in DolgAdatok
                                       where a.Dolgozószám.Trim() == Tábla.Rows[i].Cells[1].Value.ToStrTrim()
                                       select a.Munkakör).FirstOrDefault();

                    if (Munkakör != null) MyX.Kiir(Munkakör, "d" + sor);
                    MyX.Sortörésseltöbbsorba(munkalap, $"d{sor}");

                    eleje = DateTime.Parse(Tábla.Rows[i].Cells[7].Value.ToStrTrim());
                    string válasz = Tábla.Rows[i].Cells[3].Value.ToStrTrim() + " " + Tábla.Rows[i].Cells[7].Value.ToStrTrim();
                    MyX.Kiir(válasz, "e" + sor);
                    vége = DateTime.Parse(Tábla.Rows[i].Cells[8].Value.ToStrTrim());
                    if (eleje < vége)
                    {
                        // nappal
                        válasz = Tábla.Rows[i].Cells[3].Value.ToStrTrim() + " " + Tábla.Rows[i].Cells[8].Value.ToStrTrim();
                        MyX.Kiir(válasz, "f" + sor);
                    }
                    else
                    {
                        // éjszaka
                        válasz = Tábla.Rows[i].Cells[3].Value.ToStrTrim() + " " + Tábla.Rows[i].Cells[8].Value.ToStrTrim();
                        MyX.Kiir(válasz, "f" + sor);
                    }


                    MyX.Kiir($"#KÉPLET#={Tábla.Rows[i].Cells[4].Value}/60", "g" + sor);
                    MyX.Betű(munkalap, "g" + sor, BeBetuSzazados);
                    MyX.Igazít_vízszintes(munkalap, $"G{sor}", "bal");

                    válasz = Tábla.Rows[i].Cells[5].Value.ToStrTrim();
                    MyX.Sortörésseltöbbsorba(munkalap, $"i{sor}");
                    if (válasz.Contains("&T"))
                    {
                        válasz = válasz.Substring(2, válasz.Length - 2).Trim();
                        MyX.Kiir(válasz, "i" + sor);
                        MyX.Kiir("50% bérpótlék", "j" + sor);
                        MyX.Kiir("Túlóra", "h" + sor);
                    }
                    else if (válasz.Contains("&EB"))
                    {
                        válasz = válasz.Substring(3, válasz.Length - 3).Trim();
                        MyX.Kiir(válasz, "i" + sor);
                        MyX.Kiir("100% bérpótlék", "j" + sor);
                        MyX.Kiir("Elvont pihenő", "h" + sor);
                    }
                    else if (válasz.Contains("&EP"))
                    {
                        válasz = válasz.Substring(3, válasz.Length - 3).Trim();
                        MyX.Kiir(válasz, "i" + sor);
                        MyX.Kiir("100% bérpótlék", "j" + sor);
                        MyX.Kiir("Elvont pihenő", "h" + sor);
                    }
                    else if (válasz.Contains("&V"))
                    {
                        válasz = válasz.Substring(2, válasz.Length - 2).Trim();
                        MyX.Kiir(válasz, "i" + sor);
                        MyX.Kiir("50% bérpótlék", "j" + sor);
                        MyX.Kiir("visszaadott pihenő", "h" + sor);
                    }
                    sor++;
                }
            }
            MyX.Rácsoz(munkalap, $"a10:k{sor}");
  
            // dátum
            sor += 1;
            MyX.Kiir("Dátum: " + DateTime.Now.ToString("yyyy.MM.dd"), "a" + sor);
            sor += 1;
            MyX.Sormagasság(munkalap, sor + ":" + sor, 45);
            sor += 1;
            MyX.Egyesít(munkalap, "a" + sor + ":b" + sor);
            MyX.Egyesít(munkalap, "d" + sor + ":e" + sor);
            MyX.Egyesít(munkalap, "g" + sor + ":h" + sor);
            MyX.Egyesít(munkalap, "j" + sor + ":k" + sor);
            MyX.Kiir("Kiállította, ellenőrizte:", "a" + sor);
            MyX.Kiir("Túlórát elrendelte:", "d" + sor);
            MyX.Kiir("Túlmunka végrehajtását igazolja:", "g" + sor);
            MyX.Kiir("Túlmunka végzés kifiztetését engedélyezte:", "j" + sor);
            MyX.Aláírásvonal(munkalap, "a" + sor + ":b" + sor);
            MyX.Aláírásvonal(munkalap, "d" + sor + ":e" + sor);
            MyX.Aláírásvonal(munkalap, "g" + sor + ":h" + sor);
            MyX.Aláírásvonal(munkalap, "j" + sor + ":k" + sor);
            sor += 1;
            MyX.Egyesít(munkalap, "a" + sor + ":b" + sor);
            MyX.Egyesít(munkalap, "d" + sor + ":e" + sor);
            MyX.Egyesít(munkalap, "g" + sor + ":h" + sor);
            MyX.Egyesít(munkalap, "j" + sor + ":k" + sor);


            string Név = (from a in FőkönyAdatok
                          where a.Id == 2
                          select a.Név).FirstOrDefault();

            // aláíró név

            MyX.Kiir(Név, "d" + sor);
            MyX.Kiir(Név, "g" + sor);

            Név = (from a in FőkönyAdatok
                   where a.Id == 3
                   select a.Név).FirstOrDefault();
            MyX.Kiir(Név, "J" + sor);


            // beosztás
            sor += 1;
            MyX.Egyesít(munkalap, "a" + sor + ":b" + sor);
            MyX.Egyesít(munkalap, "d" + sor + ":e" + sor);
            MyX.Egyesít(munkalap, "g" + sor + ":h" + sor);
            MyX.Egyesít(munkalap, "j" + sor + ":k" + sor);
            string Beosztás = (from a in FőkönyAdatok
                               where a.Id == 2
                               select a.Beosztás).FirstOrDefault();

            MyX.Kiir(Beosztás, "d" + sor);
            MyX.Kiir(Beosztás, "g" + sor);
            Beosztás = (from a in FőkönyAdatok
                        where a.Id == 3
                        select a.Beosztás).FirstOrDefault();

            MyX.Kiir(Beosztás, "j" + sor);

            // ****************************
            // excel tábla érdemi rész vége
            // ****************************
            Beállítás_Nyomtatás beallitas_tulora = new Beállítás_Nyomtatás
            {
                Munkalap = munkalap,
                NyomtatásiTerület = $"A1:K{sor}",
                Álló = false,
                LapSzéles = 1,
                FejlécKözép = Program.PostásNév,
                FejlécJobb = DateTime.Now.ToString("yyyy.MM.dd HH:mm"),
                LáblécKözép = "&P/&N"
            };
            MyX.NyomtatásiTerület_részletes(munkalap, beallitas_tulora);
            MyX.ExcelMentés(fájlexc);
            NyomtatásiFájlok.Add(fájlexc);
            MyX.ExcelBezárás();
            MyF.ExcelNyomtatás(NyomtatásiFájlok, !Töröl);

        }
    }
}

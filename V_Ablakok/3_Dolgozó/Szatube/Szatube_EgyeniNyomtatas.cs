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
    public class Szatube_EgyeniNyomtatas
    {
        readonly Beállítás_Betű BeBetuCalibri10 = new Beállítás_Betű() { Név = "Calibri", Méret = 10 };
      readonly  List<string> NyomtatásiFájlok = new List<string>();
        public void EgyeniNyomtatas( string CmbTelephely, int Adat_Évek, Kezelő_Kiegészítő_Jelenlétiív KézJelenléti, Kezelő_Kiegészítő_főkönyvtábla KézFő, Kezelő_Dolgozó_Alap KézDolgAlap, Kezelő_Szatube_Túlóra KézTúlóra, DataGridView Tábla)
        {
            string válasz;
            string munkalap = "Munka1";
            string Logo = Application.StartupPath + @"\Főmérnökség\adatok\BKV.png";
            NyomtatásiFájlok.Clear();

            for (int i = 0; i < Tábla.SelectedRows.Count; i++)
            {
                DateTime eleje;
                DateTime vége;

                MyX.ExcelLétrehozás(munkalap);

                // ************************
                // excel tábla érdemi része
                // ************************
                MyX.Munkalap_betű(munkalap, BeBetuCalibri10);

                MyX.Oszlopszélesség(munkalap, "A:A", 19);
                MyX.Oszlopszélesség(munkalap, "b:g", 7);
                MyX.Oszlopszélesség(munkalap, "h:i", 1);
                MyX.Oszlopszélesség(munkalap, "j:j", 19);
                MyX.Oszlopszélesség(munkalap, "k:p", 7);

                // kiírjuk a szervezeteket
                MyX.Egyesít(munkalap, "d1:g1");
                MyX.Egyesít(munkalap, "m1:p1");
                MyX.Egyesít(munkalap, "d2:g2");
                MyX.Egyesít(munkalap, "m2:p2");
                MyX.Egyesít(munkalap, "d3:g3");
                MyX.Egyesít(munkalap, "m3:p3");

                List<Adat_Kiegészítő_Jelenlétiív> Adatok = KézJelenléti.Lista_Adatok(CmbTelephely);
                Adatok = Adatok.Where(a => a.Id > 1).OrderBy(a => a.Id).ToList();

                List<Adat_Kiegészítő_főkönyvtábla> FőkönyAdatok = KézFő.Lista_Adatok(CmbTelephely);

                foreach (Adat_Kiegészítő_Jelenlétiív rekord in Adatok)
                {
                    switch (rekord.Id)
                    {
                        case 2:
                            MyX.Kiir(rekord.Szervezet, "d1");
                            MyX.Kiir(rekord.Szervezet, "m1");
                            break;
                        case 3:
                            MyX.Kiir(rekord.Szervezet, "d2");
                            MyX.Kiir(rekord.Szervezet, "m2");
                            break;
                        case 4:
                            MyX.Kiir(rekord.Szervezet, "d3");
                            MyX.Kiir(rekord.Szervezet, "m3");
                            break;

                    }
                }

                // logók beszúrása
                MyX.Kép_beillesztés(munkalap, "A1", Logo, 5, 5, 0.55208, 0.6304);
                MyX.Kép_beillesztés(munkalap, "A1", Logo, 490, 5, 0.55208, 0.6304);

                // tábla fejléc
                MyX.Egyesít(munkalap, "a5:g5");
                MyX.Kiir("Rendkívüli munka elrendelő lap (egyéni)", "a5");
                MyX.Egyesít(munkalap, "j5:p5");
                MyX.Kiir("Rendkívüli munka elrendelő lap (egyéni)", "j5");
                MyX.Rácsoz(munkalap, "a5:g5");
                MyX.Rácsoz(munkalap, "j5:p5");
                // táblázat rajzolás
                MyX.Egyesít(munkalap, "a6:b7");
                MyX.Egyesít(munkalap, "j6:k7");

                MyX.Kiir("Név, HR azonosító,munakör:", "a6");
                MyX.Kiir("Név, HR azonosító,munakör:", "j6");
                MyX.Egyesít(munkalap, "c6:g6");
                MyX.Egyesít(munkalap, "c7:g7");
                MyX.Egyesít(munkalap, "l6:p6");
                MyX.Egyesít(munkalap, "l7:p7");


                MyX.Egyesít(munkalap, "a8:b9");
                MyX.Egyesít(munkalap, "j8:k9");
                MyX.Kiir("A rendkívüli munkavégzés indoka:", "a8");
                MyX.Kiir("A rendkívüli munkavégzés indoka:", "j8");
                MyX.Egyesít(munkalap, "c8:g9");
                MyX.Egyesít(munkalap, "l8:p9");

                MyX.Egyesít(munkalap, "a10:b11");
                MyX.Egyesít(munkalap, "j10:k11");
                MyX.Kiir("Rendkívüli munka fajtája :", "a10");
                MyX.Kiir("Rendkívüli munka fajtája :", "j10");
                MyX.Egyesít(munkalap, "c10:g11");
                MyX.Egyesít(munkalap, "l10:p11");

                MyX.Egyesít(munkalap, "a12:b13");
                MyX.Egyesít(munkalap, "j12:k13");
                MyX.Sortörésseltöbbsorba(munkalap, "a12:b13", true);
                MyX.Sortörésseltöbbsorba(munkalap, "j12:k13", true);
                MyX.Kiir("A rendkívüli munkavégzés időpontja:", "a12");
                MyX.Kiir("A rendkívüli munkavégzés időpontja:", "j12");
                MyX.Egyesít(munkalap, "c12:g12");
                MyX.Egyesít(munkalap, "c13:g13");
                MyX.Egyesít(munkalap, "l12:p12");
                MyX.Egyesít(munkalap, "l13:p13");

                MyX.Egyesít(munkalap, "a14:b15");
                MyX.Egyesít(munkalap, "j14:k15");
                MyX.Kiir("Időtartama:", "a14");
                MyX.Kiir("Időtartama:", "j14");
                MyX.Egyesít(munkalap, "c14:g15");
                MyX.Egyesít(munkalap, "l14:p15");

                MyX.Egyesít(munkalap, "a16:b17");
                MyX.Egyesít(munkalap, "j16:k17");
                MyX.Sortörésseltöbbsorba(munkalap, "a16:b17", true);
                MyX.Sortörésseltöbbsorba(munkalap, "j16:k17", true);
                MyX.Kiir("Elvont pihenőnap esetén a megváltás módja:", "a16");
                MyX.Kiir("Elvont pihenőnap esetén a megváltás módja:", "j16");
                MyX.Egyesít(munkalap, "c16:g17");
                MyX.Egyesít(munkalap, "l16:p17");
                MyX.Sortörésseltöbbsorba(munkalap, "C16:G17", true);
                MyX.Sortörésseltöbbsorba(munkalap, "l16:p17", true);

                MyX.Rácsoz(munkalap, "a6:g17");
                MyX.Rácsoz(munkalap, "a6:g17");
                MyX.Rácsoz(munkalap, "j6:p17");
                MyX.Rácsoz(munkalap, "j6:p17");

                MyX.Sormagasság(munkalap, "A6:B17", 15);

                // dátum kiírása
                MyX.Kiir("Budapest, " + DateTime.Now.ToString("yyyy. MMMM. dd"), "a18");
                MyX.Kiir("Budapest, " + DateTime.Now.ToString("yyyy. MMMM. dd"), "j18");

                MyX.Egyesít(munkalap, "d19:g19");
                MyX.Egyesít(munkalap, "m19:p19");
                MyX.Kiir("Rendkívüli munkavégzést elrendelte:", "d19");
                MyX.Kiir("Rendkívüli munkavégzést elrendelte:", "m19");

                for (int ii = 20; ii < 31; ii++)
                {
                    MyX.Egyesít(munkalap, $"a{ii}:b{ii}");
                    MyX.Egyesít(munkalap, $"d{ii}:g{ii}");
                    MyX.Egyesít(munkalap, $"j{ii}:k{ii}");
                    MyX.Egyesít(munkalap, $"m{ii}:p{ii}");
                }

                MyX.Kiir("Kiállította, ellenőrizte", "a21");
                MyX.Kiir("Kiállította, ellenőrizte", "j21");
                MyX.Kiir("Átvettem:", "a23");
                MyX.Kiir("Átvettem:", "j23");
                MyX.Kiir("Végrehajtás igazolása:", "d23");
                MyX.Kiir("Végrehajtás igazolása:", "m23");
                MyX.Kiir("munkavállaló aláírása", "a25");
                MyX.Kiir("munkavállaló aláírása", "j25");
                MyX.Kiir("A kifizetést engedélyezem:", "a28");
                MyX.Kiir("A kifizetést engedélyezem:", "j28");

                MyX.Sormagasság(munkalap, "20:20", 35);
                MyX.Sormagasság(munkalap, "24:24", 35);
                MyX.Sormagasság(munkalap, "27:28", 35);

                MyX.Aláírásvonal(munkalap, "a21:b21");
                MyX.Aláírásvonal(munkalap, "d21:g21");
                MyX.Aláírásvonal(munkalap, "j21:k21");
                MyX.Aláírásvonal(munkalap, "m21:p21");
                MyX.Aláírásvonal(munkalap, "a25:b25");
                MyX.Aláírásvonal(munkalap, "d25:g25");
                MyX.Aláírásvonal(munkalap, "j25:k25");
                MyX.Aláírásvonal(munkalap, "m25:p25");
                MyX.Aláírásvonal(munkalap, "d29:g29");
                MyX.Aláírásvonal(munkalap, "m29:p29");

                string Beosztás = (from a in FőkönyAdatok
                                   where a.Id == 2
                                   select a.Beosztás).FirstOrDefault();
                string Név = (from a in FőkönyAdatok
                              where a.Id == 2
                              select a.Név).FirstOrDefault();
                if (Név != null)
                {
                    MyX.Kiir(Név, "d21");
                    MyX.Kiir(Név, "m21");
                    MyX.Kiir(Név, "d25");
                    MyX.Kiir(Név, "m25");
                }
                if (Beosztás != null)
                {
                    MyX.Kiir(Beosztás, "d22");
                    MyX.Kiir(Beosztás, "m22");
                    MyX.Kiir(Beosztás, "d26");
                    MyX.Kiir(Beosztás, "m26");
                }
                Beosztás = (from a in FőkönyAdatok
                            where a.Id == 3
                            select a.Beosztás).FirstOrDefault();
                Név = (from a in FőkönyAdatok
                       where a.Id == 3
                       select a.Név).FirstOrDefault();
                if (Név != null)
                {
                    MyX.Kiir(Név, "d29");
                    MyX.Kiir(Név, "m29");
                }
                if (Beosztás != null)
                {
                    MyX.Kiir(Beosztás, "d30");
                    MyX.Kiir(Beosztás, "m30");
                }
                Beállítás_Nyomtatás beallitas_egyeni = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:P30",
                    Álló = false,
                    LapSzéles = 1,
                    LapMagas = 1
                };
                MyX.NyomtatásiTerület_részletes(munkalap, beallitas_egyeni);

                List<Adat_Dolgozó_Alap> DolgAdatok = KézDolgAlap.Lista_Adatok(CmbTelephely);

                MyX.Kiir("Sorszám: " + Tábla.SelectedRows[i].Cells[0].Value.ToStrTrim(), "a4");
                MyX.Kiir("Sorszám: " + Tábla.SelectedRows[i].Cells[0].Value.ToStrTrim(), "j4");

                MyX.Kiir(Tábla.SelectedRows[i].Cells[2].Value.ToStrTrim() + "(" + Tábla.SelectedRows[i].Cells[1].Value.ToStrTrim() + ")", "c6");
                MyX.Kiir(Tábla.SelectedRows[i].Cells[2].Value.ToStrTrim() + "(" + Tábla.SelectedRows[i].Cells[1].Value.ToStrTrim() + ")", "l6");

                válasz = (from a in DolgAdatok
                          where a.Dolgozószám.Trim() == Tábla.SelectedRows[i].Cells[1].Value.ToStrTrim()
                          select a.Munkakör.Trim()).FirstOrDefault();

                if (válasz != null)
                {
                    MyX.Kiir(válasz, "c7");
                    MyX.Kiir(válasz, "l7");
                }

                válasz = Tábla.SelectedRows[i].Cells[5].Value.ToStrTrim();
                if (válasz.Contains("&T"))
                {
                    válasz = válasz.Substring(2, válasz.Length - 2).Trim();
                    MyX.Kiir(válasz, "c8");
                    MyX.Kiir(válasz, "l8");
                    MyX.Kiir("Túlóra", "c10");
                    MyX.Kiir("Túlóra", "l10");
                    MyX.Kiir("-", "c16");
                    MyX.Kiir("-", "l16");
                }
                else if (válasz.Contains("&EB"))
                {
                    válasz = válasz.Substring(3, válasz.Length - 3).Trim();
                    MyX.Kiir(válasz, "c8");
                    MyX.Kiir(válasz, "l8");
                    MyX.Kiir("Elvont pihenő", "c10");
                    MyX.Kiir("Elvont pihenő", "l10");
                    MyX.Kiir("100 % bérpótlék", "c16");
                    MyX.Kiir("100 % bérpótlék", "l16");
                }

                else if (válasz.Contains("&EP"))
                {
                    válasz = válasz.Substring(3, válasz.Length - 3).Trim();
                    MyX.Kiir(válasz, "c8");
                    MyX.Kiir(válasz, "l8");
                    MyX.Kiir("Elvont pihenő", "c10");
                    MyX.Kiir("Elvont pihenő", "l10");
                    MyX.Kiir("100 % bérpótlék", "c16");
                    MyX.Kiir("100 % bérpótlék", "l16");
                }

                else if (válasz.Contains("&V"))
                {
                    válasz = válasz.Substring(2, válasz.Length - 2).Trim();
                    MyX.Kiir(válasz, "c8");
                    MyX.Kiir(válasz, "l8");
                    MyX.Kiir("visszaadott pihenő", "c10");
                    MyX.Kiir("visszaadott pihenő", "l10");
                    MyX.Kiir("-", "c16");
                    MyX.Kiir("-", "l16");

                }

                MyX.Sortörésseltöbbsorba(munkalap, "C8", true);
                MyX.Sortörésseltöbbsorba(munkalap, "l8", true);

                eleje = DateTime.Parse(Tábla.SelectedRows[i].Cells[7].Value.ToString());
                vége = DateTime.Parse(Tábla.SelectedRows[i].Cells[8].Value.ToString());
                válasz = Tábla.SelectedRows[i].Cells[3].Value.ToStrTrim() + " nap " + Tábla.SelectedRows[i].Cells[7].Value.ToStrTrim() + " -tól";
                MyX.Kiir(válasz, "c12");
                MyX.Kiir(válasz, "l12");

                if (eleje < vége)
                {
                    // nappal
                    válasz = Tábla.SelectedRows[i].Cells[3].Value.ToStrTrim() + " nap " + Tábla.SelectedRows[i].Cells[8].Value.ToStrTrim() + " -ig";
                    MyX.Kiir(válasz, "c13");
                    MyX.Kiir(válasz, "l13");
                }
                else
                {
                    // éjszaka
                    válasz = Tábla.SelectedRows[i].Cells[3].Value.ToStrTrim() + " nap " + Tábla.SelectedRows[i].Cells[8].Value.ToStrTrim() + " -ig";
                    MyX.Kiir(válasz, "c13");
                    MyX.Kiir(válasz, "l13");
                }

                MyX.Kiir(Math.Round((double.Parse(Tábla.SelectedRows[i].Cells[4].Value.ToString()) / 60d), 2) + " óra", "c14");
                MyX.Kiir(Math.Round((double.Parse(Tábla.SelectedRows[i].Cells[4].Value.ToString()) / 60d), 2) + " óra", "l14");

                string könyvtár = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string fájlnév = $"Túlóra_{Program.PostásNév}_{Tábla.SelectedRows[i].Cells[1].Value.ToStrTrim()}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                string MentésiFájl = $@"{könyvtár}\{fájlnév}";
                MyX.ExcelMentés(MentésiFájl);
                NyomtatásiFájlok.Add(MentésiFájl);
                MyX.ExcelBezárás();
            }
            // a státusokat átállítja
            List<double> Sorszámok = new List<double>();
            for (int i = 0; i < Tábla.SelectedRows.Count; i++)
                Sorszámok.Add(Tábla.SelectedRows[i].Cells[0].Value.ToÉrt_Double());

            KézTúlóra.Státus(CmbTelephely, Adat_Évek, Sorszámok, 1);
            // ****************************
            // excel tábla érdemi rész vége
            // ****************************
            MyF.ExcelNyomtatás(NyomtatásiFájlok);
        }
    }
}

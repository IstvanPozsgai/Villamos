using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.Villamos_Ablakok.Kerékeszterga
{

    public class Kerékeszterga_Excel
    {
        public string Fájl { get; private set; }
        public DateTime Dátum { get; private set; }

        int NormaIdő = 0;

        readonly Kezelő_Kerék_Eszterga_Igény KézIgény = new Kezelő_Kerék_Eszterga_Igény();
        readonly Kezelő_Váltós_Naptár KéZNaptár = new Kezelő_Váltós_Naptár();
        readonly Kezelő_Kerék_Eszterga_Esztergályos KézEszter = new Kezelő_Kerék_Eszterga_Esztergályos();
        readonly Kezelő_Dolgozó_Beosztás_Új KézBeo = new Kezelő_Dolgozó_Beosztás_Új();
        readonly Kezelő_Kerék_Eszterga_Naptár KézEsztNaptár = new Kezelő_Kerék_Eszterga_Naptár();

        readonly Beállítás_Betű BeBetu = new Beállítás_Betű() { };
        readonly Beállítás_Betű BeBetuVastag = new Beállítás_Betű() { Vastag = true };
        readonly Beállítás_Betű BeBetuSzazalek = new Beállítás_Betű() { Formátum = "0%" };

        public Kerékeszterga_Excel(string fájl, DateTime dátum)
        {
            Fájl = fájl;
            Dátum = dátum;
        }

        public void Excel_alaptábla()
        {
            MyX.ExcelLétrehozás();
            Beosztás();
            Elvégzett();
            Gépidő();
            Várakozók();
            MyX.ExcelMentés(Fájl);
            MyX.ExcelBezárás();
        }

        private void Várakozók()
        {
            try
            {
                string munkalap = "Esztergára_Várók";
                MyX.Munkalap_átnevezés("Munka1", munkalap);
                MyX.Munkalap_aktív(munkalap);
                int sor = 1;
                for (int ii = -1; ii < 1; ii++)
                {
                    //fejléc elkészítése
                    MyX.Kiir("Prioritás", "A" + sor);
                    MyX.Kiir("Igénylés ideje", "B" + sor);
                    MyX.Kiir("Pályaszám(ok)", "C" + sor);
                    MyX.Kiir("Telephely", "D" + sor);
                    MyX.Kiir("Típus", "E" + sor);
                    MyX.Kiir("Státus", "F" + sor);
                    MyX.Kiir("Megjegyzés", "G" + sor);

                    List<Adat_Kerék_Eszterga_Igény> Adatok = KézIgény.Lista_Adatok(DateTime.Today.AddYears(ii).Year);
                    Adatok = (from a in Adatok
                              where a.Státus < 7
                              orderby a.Prioritás descending, a.Rögzítés_dátum
                              select a).ToList();
                    foreach (Adat_Kerék_Eszterga_Igény rekord in Adatok)
                    {
                        sor++;
                        MyX.Kiir(rekord.Prioritás.ToString(), "A" + sor);
                        MyX.Kiir(rekord.Rögzítés_dátum.ToString(), "B" + sor);
                        MyX.Kiir(rekord.Pályaszám.Trim(), "C" + sor);
                        MyX.Kiir(rekord.Telephely.Trim(), "D" + sor);
                        MyX.Kiir(rekord.Típus.Trim(), "E" + sor);
                        switch (rekord.Státus)
                        {
                            case 0:
                                MyX.Kiir("Igény", "F" + sor);
                                break;
                            case 2:
                                MyX.Kiir("Ütemezett", "F" + sor);
                                break;
                            case 7:
                                MyX.Kiir("Elkészült", "F" + sor);
                                break;
                            case 9:
                                MyX.Kiir("Törölt", "F" + sor);
                                break;
                        }
                        MyX.Kiir(rekord.Megjegyzés.Trim(), "G" + sor);
                    }

                }
                MyX.Rácsoz(munkalap, "A1:G" + sor);
                MyX.Vastagkeret(munkalap, "A1:G" + sor);
                MyX.Betű(munkalap, "A1:G1", BeBetuVastag);
                MyX.Háttérszín(munkalap, "A1:G1", System.Drawing.Color.Yellow);
                MyX.Szűrés(munkalap, "A", "G", 1);
                MyX.Oszlopszélesség(munkalap, "A:G");
                //Munkalap, terület, sorismétlődés, oszlopismétlődés, álló
                Beállítás_Nyomtatás beallitas_fejlec = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:G{sor}",
                    IsmétlődőSorok = "1:1",
                    IsmétlődőOszlopok = "",
                    Álló = true
                };
                MyX.NyomtatásiTerület_részletes(munkalap, beallitas_fejlec);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Várakozók", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Beosztás()
        {
            try
            {

                string munkalap = "Beosztás";
                MyX.Munkalap_Új(munkalap);
                MyX.Munkalap_aktív(munkalap);
                MyX.Kiir("Hr Azonosító", "A1");
                MyX.Kiir("Név", "B1");
                DateTime Hételső = MyF.Hét_elsőnapja(Dátum);
                DateTime ideig = MyF.Hét_elsőnapja(Dátum);
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(Dátum);

                //Alap adatok kiírása
                int sor = 1;
                int oszlop = 3;
                for (int i = 0; i < 7; i++)
                {
                    MyX.Kiir(ideig.AddDays(i).ToString("dd"), MyF.Oszlopnév(oszlop + i) + sor);

                }

                //Dolgozói beosztás kiírása
                List<Adat_Dolgozó_Beosztás_Új> Adatok = Adat_BEO_Csoport(Dátum);
                string előzőDolg = "";
                foreach (Adat_Dolgozó_Beosztás_Új rekord in Adatok)
                {
                    if (előzőDolg.Trim() != rekord.Dolgozószám.Trim())
                    {
                        sor++;
                        előzőDolg = rekord.Dolgozószám.Trim();
                    }
                    TimeSpan ideig1 = rekord.Nap - Hételső;
                    int oszlopa = ideig1.Days + 3;
                    MyX.Kiir(rekord.Beosztáskód.Trim(), MyF.Oszlopnév(oszlopa) + sor);
                    MyX.Kiir(rekord.Dolgozószám.Trim(), "A" + sor);
                    MyX.Kiir(Dolgozó_név(rekord.Dolgozószám).ToString(), "B" + sor);
                }

                // hétvége és ünnepnap színezés
                List<Adat_Váltós_Naptár> AdatNaptár = KéZNaptár.Lista_Adatok(Dátum.Year, "");
                AdatNaptár = (from a in AdatNaptár
                              where a.Dátum >= Hételső
                              && a.Dátum <= Hétutolsó
                              orderby a.Dátum
                              select a).ToList();

                foreach (Adat_Váltós_Naptár Elem in AdatNaptár)
                {
                    TimeSpan ideig1 = Elem.Dátum - Hételső;
                    int oszlopa = ideig1.Days + 3;
                    switch (Elem.Nap.Trim())
                    {
                        case "P":
                            MyX.Háttérszín(munkalap, MyF.Oszlopnév(oszlopa) + "1:" + MyF.Oszlopnév(oszlopa) + sor, System.Drawing.Color.Green);
                            break;
                        case "V":
                            MyX.Háttérszín(munkalap, MyF.Oszlopnév(oszlopa) + "1:" + MyF.Oszlopnév(oszlopa) + sor, System.Drawing.Color.Red);
                            break;
                        case "Ü":
                            MyX.Háttérszín(munkalap, MyF.Oszlopnév(oszlopa) + "1:" + MyF.Oszlopnév(oszlopa) + sor, System.Drawing.Color.Red);
                            break;
                    }
                }
                MyX.Rácsoz(munkalap, "A1:I" + sor);
                MyX.Vastagkeret(munkalap, "A1:I" + sor);
                MyX.Oszlopszélesség(munkalap, "A:B");
                MyX.Oszlopszélesség(munkalap, "C:I", 5);
                Beállítás_Nyomtatás beallitas_dolgozoi = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:I{sor}",
                    IsmétlődőSorok = "1:1",
                    IsmétlődőOszlopok = "",
                    Álló = true
                };
                MyX.NyomtatásiTerület_részletes(munkalap, beallitas_dolgozoi);

            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Beosztás", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<Adat_Dolgozó_Beosztás_Új> Adat_BEO_Csoport(DateTime KüldDátum)
        {
            List<Adat_Dolgozó_Beosztás_Új> CsoportAdatok = new List<Adat_Dolgozó_Beosztás_Új>();
            List<Adat_Kerék_Eszterga_Esztergályos> Csoport = KézEszter.Lista_Adatok().OrderBy(a => a.Dolgozószám).ToList();

            foreach (Adat_Kerék_Eszterga_Esztergályos Elem in Csoport)
            {
                List<Adat_Dolgozó_Beosztás_Új> SzemélyBEO = Adat_BEO_személy(Dátum, Elem.Dolgozószám.Trim());
                CsoportAdatok.AddRange(SzemélyBEO);
            }
            return CsoportAdatok;
        }

        public List<Adat_Dolgozó_Beosztás_Új> Adat_BEO_személy(DateTime KüldDátum, string dolgozószám)
        {
            DateTime Hételső = MyF.Hét_elsőnapja(KüldDátum);
            DateTime Hétutolsó = MyF.Hét_Utolsónapja(KüldDátum);

            List<Adat_Dolgozó_Beosztás_Új> Adatok = KézBeo.Lista_Adatok("Baross", Hételső);
            //következő hónap adatait hozzáadjuk
            if (Hételső.Month != Hétutolsó.Month)
            {
                //Másik Hónap
                List<Adat_Dolgozó_Beosztás_Új> Ideig = KézBeo.Lista_Adatok("Baross", Hétutolsó);
                Adatok.AddRange(Ideig);
            }
            Adatok = (from a in Adatok
                      where a.Dolgozószám == dolgozószám.Trim()
                      && a.Nap >= Hételső
                      && a.Nap <= Hétutolsó
                      orderby a.Nap
                      select a).ToList();

            return Adatok;
        }

        public string Dolgozó_név(string dolgozószám)
        {
            string válasz = "";
            List<Adat_Kerék_Eszterga_Esztergályos> Adatok = KézEszter.Lista_Adatok();

            Adat_Kerék_Eszterga_Esztergályos Elem = (from a in Adatok
                                                     where a.Dolgozószám == dolgozószám
                                                     select a).FirstOrDefault();
            if (Elem != null) válasz = Elem.Dolgozónév.Trim();

            return válasz;
        }

        public void Elvégzett()
        {
            try
            {
                DateTime Hételső = MyF.Hét_elsőnapja(Dátum);
                DateTime IdeigDát = Hételső;
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(Dátum);

                List<Adat_Kerék_Eszterga_Naptár> Adatok = KézEsztNaptár.Lista_Adatok(Dátum.Year);
                Adatok = (from a in Adatok
                          where a.Idő >= Hételső
                          && a.Idő <= Hétutolsó
                          orderby a.Pályaszám
                          select a).ToList();

                string munkalap = "Elvégzett";
                MyX.Munkalap_Új(munkalap);
                MyX.Munkalap_aktív(munkalap);


                MyX.Oszlopszélesség(munkalap, "F:F", 70);
                int sor = 1;
                string előző = "";
                string megjegyzés = "";
                int darab = 0;
                foreach (Adat_Kerék_Eszterga_Naptár rekord in Adatok)
                {
                    if (rekord.Pályaszám.Trim() != "_")
                    {
                        if (előző.Trim() != rekord.Pályaszám.Trim())
                        {
                            előző = rekord.Pályaszám.Trim();
                            darab = 0;
                            megjegyzés = "";
                            MyX.Sortörésseltöbbsorba(munkalap, "F" + sor);
                            sor++;
                        }
                        darab++;
                        if (rekord.Megjegyzés.Trim() != "" && !megjegyzés.Contains(rekord.Megjegyzés.Trim()))
                            megjegyzés += rekord.Megjegyzés.Trim() + "-";
                        MyX.Kiir(rekord.Idő.ToString("yyyy.MM.dd"), "A" + sor);
                        MyX.Kiir(rekord.Pályaszám, "B" + sor);
                        MyX.Kiir((darab * 30).ToString(), "E" + sor);
                        MyX.Kiir(megjegyzés, "F" + sor);
                    }
                }

                //Megkeressük a telephelyet és a Norma időt
                List<Adat_Kerék_Eszterga_Igény> AdatokIgény = KézIgény.Lista_Adatok(Dátum.Year);

                for (int i = 2; i <= sor; i++)
                {
                    string Beolvasott = MyX.Beolvas(munkalap, "B" + i);
                    string[] darabol = Beolvasott.Split('=');

                    Adat_Kerék_Eszterga_Igény EgyIgény = AdatokIgény.Where(a => a.Pályaszám == darabol[0].Trim()).FirstOrDefault();
                    if (EgyIgény != null)
                    {
                        NormaIdő += EgyIgény.Norma;
                        MyX.Kiir(EgyIgény.Norma.ToString(), "D" + i);
                        MyX.Kiir(EgyIgény.Telephely, "C" + i);
                        switch (EgyIgény.Státus)
                        {
                            case 0:
                                MyX.Kiir("Igényelt", "G" + i);
                                break;
                            case 2:
                                MyX.Kiir("Ütemezett", "G" + i);
                                break;
                            case 7:
                                MyX.Kiir("Elkészült", "G" + i);
                                break;
                            case 9:
                                MyX.Kiir("Törölt", "G" + i);
                                break;
                            default:
                                MyX.Kiir("Nem értékelhető", "G" + i);
                                break;
                        }
                    }
                }

                MyX.Kiir("Dátum", "A1");
                MyX.Kiir("Pályaszám(ok)", "B1");
                MyX.Kiir("Telephely", "C1");
                MyX.Kiir("Normaidő", "D1");
                MyX.Kiir("Felhasznált gépidő", "E1");
                MyX.Kiir("Indoklás", "F1");
                MyX.Kiir("Igények ?", "G1");
                MyX.Oszlopszélesség(munkalap, "A:E");
                MyX.Oszlopszélesség(munkalap, "G:G");
                MyX.Rácsoz(munkalap, "A1:G" + sor);
                MyX.Vastagkeret(munkalap, "A1:G" + sor);

                MyX.Háttérszín(munkalap, "A1:G1", System.Drawing.Color.Yellow);
                MyX.Szűrés(munkalap, "A", "G", 1);
                Beállítás_Nyomtatás beallitas_elvegzett = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:G{sor}",
                    IsmétlődőSorok = "1:1",
                    IsmétlődőOszlopok = "",
                    Álló = true
                };
                MyX.NyomtatásiTerület_részletes(munkalap, beallitas_elvegzett);

            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Gépidő()
        {
            try
            {
                DateTime Hételső = MyF.Hét_elsőnapja(Dátum);
                DateTime IdeigDát = Hételső;
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(Dátum);

                List<Adat_Kerék_Eszterga_Naptár> Adatok = KézEsztNaptár.Lista_Adatok(Dátum.Year);
                Adatok = (from a in Adatok
                          where a.Idő >= Hételső
                          && a.Idő <= Hétutolsó
                          orderby a.Pályaszám
                          select a).ToList();

                string munkalap = "GépIdő";
                MyX.Munkalap_Új(munkalap);
                MyX.Munkalap_aktív(munkalap);
                MyX.Kiir("Tevékenység", "A1");
                MyX.Kiir("Gépidő", "B1");
                MyX.Kiir("%-os megoszlás", "C1");

                MyX.Kiir("Hatékonyság", "m1");
                MyX.Kiir("", "l1");
                MyX.Kiir("Norma idő [perc]", "l2");
                MyX.Kiir("Tervezési idő [perc]", "l3");
                MyX.Kiir(NormaIdő.ToString(), "m2");

                int sor = 1;
                string előző = "";

                int darab = 0;
                string tevékenység = "";
                int összesen = 0;
                foreach (Adat_Kerék_Eszterga_Naptár rekord in Adatok)
                {
                    if (rekord.Pályaszám.Trim() != "_")
                    {
                        tevékenység = rekord.Pályaszám.Contains("=") ? "Esztergálás" : rekord.Pályaszám.Trim();
                        if (előző.Trim() != tevékenység)
                        {
                            előző = tevékenység;
                            darab = 0;
                            sor++;
                        }
                        darab++;
                        összesen++;

                        MyX.Kiir(tevékenység, "A" + sor);
                        if (tevékenység == "Esztergálás") MyX.Kiir((darab * 30).ToString(), "m3");

                        MyX.Kiir((darab * 30).ToString(), "B" + sor);
                    }
                }
                összesen = összesen == 0 ? 1 : összesen * 30; //ne osszunk váletlenül sem nullával
                for (int i = 2; i <= sor; i++)
                {
                    MyX.Kiir($"#KÉPLET#=RC[-1]/{összesen}", "C" + i);
                    MyX.Betű(munkalap, "C" + i, BeBetuSzazalek);
                }

                MyX.Diagram_Beallit(munkalap, 10, 150, "A1", "B" + sor, "Gépidő");
                MyX.Rácsoz(munkalap, "A1:C" + sor);
                MyX.Vastagkeret(munkalap, "A1:C" + sor);
                MyX.Oszlopszélesség(munkalap, "A:C");
                MyX.Háttérszín(munkalap, "A1:C1", System.Drawing.Color.Yellow);
                MyX.Szűrés(munkalap, "A", "C", 1);

                //kis tábla
                MyX.Rácsoz(munkalap, "l1:m3");
                MyX.Vastagkeret(munkalap, "l1:m3");
                MyX.Oszlopszélesség(munkalap, "l:m");
                MyX.Háttérszín(munkalap, "l1:m1", System.Drawing.Color.Yellow);
                MyX.Diagram_Beallit(munkalap, 600, 150, "l1", "m3");

                Beállítás_Nyomtatás beallitas_gepido = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:Q{sor}",
                    IsmétlődőSorok = "1:1",
                    IsmétlődőOszlopok = "",
                    Álló = true
                };
                MyX.NyomtatásiTerület_részletes(munkalap, beallitas_gepido);

            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

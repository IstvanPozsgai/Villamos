using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Kezelők;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok.Kerékeszterga
{

    public class Kerékeszterga_Excel
    {
        public string Fájl { get; private set; }
        public string Gyökér { get; private set; }
        public DateTime Dátum { get; private set; }

        int NormaIdő = 0;
        public Kerékeszterga_Excel(string fájl, string gyökér, DateTime dátum)
        {
            Fájl = fájl;
            Dátum = dátum;
            Gyökér = gyökér;

        }

        public void Excel_alaptábla()
        {
            MyE.ExcelLétrehozás();
            Beosztás();
            Elvégzett();
            Gépidő();
            Várakozók();
            MyE.ExcelMentés(Fájl);
            MyE.ExcelBezárás();
        }

        void Várakozók()
        {
            try
            {
                string munkalap = "Esztergára_Várók";
                MyE.Munkalap_átnevezés("Munka1", munkalap);
                int sor = 1;
                for (int ii = -1; ii < 1; ii++)
                {
                    string hely = Gyökér + $@"\Főmérnökség\Adatok\Kerékeszterga\{DateTime.Today.AddYears(ii).Year}_Igény.mdb";
                    string jelszó = "RónaiSándor";
                    string szöveg;


                    //fejléc elkészítése
                    MyE.Kiir("Prioritás", "A" + sor);
                    MyE.Kiir("Igénylés ideje", "B" + sor);
                    MyE.Kiir("Pályaszám(ok)", "C" + sor);
                    MyE.Kiir("Telephely", "D" + sor);
                    MyE.Kiir("Típus", "E" + sor);
                    MyE.Kiir("Státus", "F" + sor);
                    MyE.Kiir("Megjegyzés", "G" + sor);


                    if (File.Exists(hely))
                    {
                        szöveg = "SELECT * FROM Igény WHERE státus<7  ORDER BY Prioritás desc, Rögzítés_dátum  ";
                        Kezelő_Kerék_Eszterga_Igény kéz = new Kezelő_Kerék_Eszterga_Igény();
                        List<Adat_Kerék_Eszterga_Igény> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                        foreach (Adat_Kerék_Eszterga_Igény rekord in Adatok)
                        {
                            sor++;
                            MyE.Kiir(rekord.Prioritás.ToString(), "A" + sor);
                            MyE.Kiir(rekord.Rögzítés_dátum.ToString(), "B" + sor);
                            MyE.Kiir(rekord.Pályaszám.Trim(), "C" + sor);
                            MyE.Kiir(rekord.Telephely.Trim(), "D" + sor);
                            MyE.Kiir(rekord.Típus.Trim(), "E" + sor);
                            switch (rekord.Státus)
                            {
                                case 0:
                                    MyE.Kiir("Igény", "F" + sor);
                                    break;
                                case 2:
                                    MyE.Kiir("Ütemezett", "F" + sor);
                                    break;
                                case 7:
                                    MyE.Kiir("Elkészült", "F" + sor);
                                    break;
                                case 9:
                                    MyE.Kiir("Törölt", "F" + sor);
                                    break;
                            }
                            MyE.Kiir(rekord.Megjegyzés.Trim(), "G" + sor);
                        }
                    }
                }
                MyE.Rácsoz("A1:G" + sor);
                MyE.Vastagkeret("A1:G" + sor);
                MyE.Betű("A1:G1", false, false, true);
                MyE.Háttérszín("A1:G1", System.Drawing.Color.Yellow);
                MyE.Szűrés(munkalap, "A", "G", 1);
                MyE.Oszlopszélesség(munkalap, "A:G");
                MyE.NyomtatásiTerület_részletes(munkalap, "A1:G" + sor, "1:1", "", true);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Várakozók", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void Beosztás()
        {
            try
            {

                string munkalap = "Beosztás";
                MyE.Új_munkalap(munkalap);
                MyE.Kiir("Hr Azonosító", "A1");
                MyE.Kiir("Név", "B1");
                DateTime Hételső = MyF.Hét_elsőnapja(Dátum);
                DateTime ideig = MyF.Hét_elsőnapja(Dátum);
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(Dátum);

                //Alap adatok kiírása
                int sor = 1;
                int oszlop = 3;
                for (int i = 0; i < 7; i++)
                {
                    MyE.Kiir(ideig.AddDays(i).ToString("dd"), MyE.Oszlopnév(oszlop + i) + sor);

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
                    MyE.Kiir(rekord.Beosztáskód.Trim(), MyE.Oszlopnév(oszlopa) + sor);
                    MyE.Kiir(rekord.Dolgozószám.Trim(), "A" + sor);
                    MyE.Kiir(Dolgozó_név(rekord.Dolgozószám).ToString(), "B" + sor);
                }

                // hétvége és ünnepnap színezés
                string hely = Gyökér + @"\Főmérnökség\adatok\" + Dátum.Year.ToString() + @"\munkaidőnaptár.mdb";
                string jelszó = "katalin";
                string szöveg = $"SELECT * FROM naptár WHERE dátum>=#{Hételső:MM-dd-yyyy}# AND dátum<=#{Hétutolsó:MM-dd-yyyy}# ORDER BY Dátum";
                Kezelő_Váltós_Naptár KéZNaptár = new Kezelő_Váltós_Naptár();
                List<Adat_Váltós_Naptár> AdatNaptár = KéZNaptár.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Váltós_Naptár Elem in AdatNaptár)
                {
                    TimeSpan ideig1 = Elem.Dátum - Hételső;
                    int oszlopa = ideig1.Days + 3;
                    switch (Elem.Nap.Trim())
                    {
                        case "P":
                            MyE.Háttérszín(MyE.Oszlopnév(oszlopa) + "1:" + MyE.Oszlopnév(oszlopa) + sor, System.Drawing.Color.Green);
                            break;
                        case "V":
                            MyE.Háttérszín(MyE.Oszlopnév(oszlopa) + "1:" + MyE.Oszlopnév(oszlopa) + sor, System.Drawing.Color.Red);
                            break;
                        case "Ü":
                            MyE.Háttérszín(MyE.Oszlopnév(oszlopa) + "1:" + MyE.Oszlopnév(oszlopa) + sor, System.Drawing.Color.Red);
                            break;
                    }
                }
                MyE.Rácsoz("A1:I" + sor);
                MyE.Vastagkeret("A1:I" + sor);
                MyE.Oszlopszélesség(munkalap, "A:B");
                MyE.Oszlopszélesség(munkalap, "C:I", 5);
                MyE.NyomtatásiTerület_részletes(munkalap, "A1:I" + sor, "1:1", "", true);
                MyE.Aktív_Cella(munkalap, "A1");
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

            string hely = Gyökér + $@"\Főmérnökség\Adatok\Kerékeszterga\Törzs.mdb";
            string jelszó = "RónaiSándor";
            string szöveg = $"SELECT * FROM Esztergályos ORDER BY dolgozószám";

            Kezelő_Kerék_Eszterga_Esztergályos Kéz = new Kezelő_Kerék_Eszterga_Esztergályos();
            List<Adat_Kerék_Eszterga_Esztergályos> Csoport = Kéz.Lista_Adatok(hely, jelszó, szöveg);

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

            string hely = Gyökér + $@"\Baross\Adatok\Beosztás\{Hételső.Year}\EsztBeosztás{Hételső:yyyyMM}.mdb";
            string jelszó = "kiskakas";
            string szöveg = $"SELECT * FROM Beosztás WHERE Dolgozószám='{dolgozószám.Trim()}' AND Nap>=#{Hételső:MM-dd-yyyy}# AND Nap<=#{Hétutolsó:MM-dd-yyyy}#";
            szöveg += " ORDER BY Nap";

            Kezelő_Dolgozó_Beosztás_Új kéz = new Kezelő_Dolgozó_Beosztás_Új();
            List<Adat_Dolgozó_Beosztás_Új> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);


            //következő hónap adatait hozzáadjuk
            if (Hételső.Month != Hétutolsó.Month)
            {
                //Másik Hónap
                hely = Gyökér + $@"\Baross\Adatok\Beosztás\{Hétutolsó.Year}\EsztBeosztás{Hétutolsó:yyyyMM}.mdb";
                List<Adat_Dolgozó_Beosztás_Új> Ideig = kéz.Lista_Adatok(hely, jelszó, szöveg);
                Adatok.AddRange(Ideig);

            }
            return Adatok;
        }

        public string Dolgozó_név(string dolgozószám)
        {
            string hely = Gyökér + $@"\Főmérnökség\Adatok\Kerékeszterga\Törzs.mdb";
            string jelszó = "RónaiSándor";
            string szöveg = $"SELECT * FROM Esztergályos WHERE dolgozószám='{dolgozószám}'";
            Kezelő_Kerék_Eszterga_Esztergályos Kéz = new Kezelő_Kerék_Eszterga_Esztergályos();
            Adat_Kerék_Eszterga_Esztergályos Elem = Kéz.Egy_Adat(hely, jelszó, szöveg);

            return Elem.Dolgozónév.Trim();
        }

        void Elvégzett()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{Dátum.Year}_Esztergálás.mdb";
                string jelszó = "RónaiSándor";
                DateTime Hételső = MyF.Hét_elsőnapja(Dátum);
                DateTime IdeigDát = Hételső;
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(Dátum);

                string szöveg = $"SELECT * FROM Naptár WHERE Naptár.Idő>=#{Hételső:MM-dd-yyyy}# ";
                szöveg += $" And Naptár.Idő<=#{Hétutolsó:MM-dd-yyyy}# ORDER BY pályaszám";

                Kezelő_Kerék_Eszterga_Naptár kéz = new Kezelő_Kerék_Eszterga_Naptár();
                List<Adat_Kerék_Eszterga_Naptár> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                string munkalap = "Elvégzett";
                MyE.Új_munkalap(munkalap);


                MyE.Oszlopszélesség(munkalap, "F:F", 70);
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
                            MyE.Sortörésseltöbbsorba("F" + sor);
                            sor++;
                        }
                        darab++;
                        if (rekord.Megjegyzés.Trim() != "" && !megjegyzés.Contains(rekord.Megjegyzés.Trim()))
                            megjegyzés += rekord.Megjegyzés.Trim() + "-";
                        MyE.Kiir(rekord.Idő.ToString("yyyy.MM.dd"), "A" + sor);
                        MyE.Kiir(rekord.Pályaszám, "B" + sor);
                        MyE.Kiir((darab * 30).ToString(), "E" + sor);
                        MyE.Kiir(megjegyzés, "F" + sor);
                    }
                }


                //Megkeressük a telephelyet és a Norma időt
                Kezelő_Kerék_Eszterga_Igény kézIgény = new Kezelő_Kerék_Eszterga_Igény();
                hely = Gyökér + $@"\Főmérnökség\Adatok\Kerékeszterga\{Dátum.Year}_Igény.mdb";
                Adat_Kerék_Eszterga_Igény AdatokIgény;

                for (int i = 2; i <= sor; i++)
                {
                    string Beolvasott = MyE.Beolvas("B" + i);
                    string[] darabol = Beolvasott.Split('=');
                    szöveg = $"SELECT * FROM Igény WHERE pályaszám='{darabol[0].Trim()}' ";
                    AdatokIgény = kézIgény.Egy_Adat(hely, jelszó, szöveg);
                    if (AdatokIgény != null)
                    {
                        NormaIdő += AdatokIgény.Norma;
                        MyE.Kiir(AdatokIgény.Norma.ToString(), "D" + i);
                        MyE.Kiir(AdatokIgény.Telephely, "C" + i);
                        switch (AdatokIgény.Státus)
                        {
                            case 0:
                                MyE.Kiir("Igényelt", "G" + i);
                                break;
                            case 2:
                                MyE.Kiir("Ütemezett", "G" + i);
                                break;
                            case 7:
                                MyE.Kiir("Elkészült", "G" + i);
                                break;
                            case 9:
                                MyE.Kiir("Törölt", "G" + i);
                                break;
                            default:
                                MyE.Kiir("Nem értékelhető", "G" + i);
                                break;
                        }



                    }
                }

                MyE.Kiir("Dátum", "A1");
                MyE.Kiir("Pályaszám(ok)", "B1");
                MyE.Kiir("Telephely", "C1");
                MyE.Kiir("Normaidő", "D1");
                MyE.Kiir("Felhasznált gépidő", "E1");
                MyE.Kiir("Indoklás", "F1");
                MyE.Kiir("Igények ?", "G1");
                MyE.Oszlopszélesség(munkalap, "A:E");
                MyE.Oszlopszélesség(munkalap, "G:G");
                MyE.Rácsoz("A1:G" + sor);
                MyE.Vastagkeret("A1:G" + sor);

                MyE.Háttérszín("A1:G1", System.Drawing.Color.Yellow);
                MyE.Szűrés(munkalap, "A", "G", 1);
                MyE.NyomtatásiTerület_részletes(munkalap, "A1:G" + sor, "1:1", "", true);
                MyE.Aktív_Cella(munkalap, "A1");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void Gépidő()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{Dátum.Year}_Esztergálás.mdb";
                string jelszó = "RónaiSándor";
                DateTime Hételső = MyF.Hét_elsőnapja(Dátum);
                DateTime IdeigDát = Hételső;
                DateTime Hétutolsó = MyF.Hét_Utolsónapja(Dátum);

                string szöveg = $"SELECT * FROM Naptár WHERE Naptár.Idő>=#{Hételső:MM-dd-yyyy}# ";
                szöveg += $" And Naptár.Idő<=#{Hétutolsó:MM-dd-yyyy}# ORDER BY pályaszám";

                Kezelő_Kerék_Eszterga_Naptár kéz = new Kezelő_Kerék_Eszterga_Naptár();
                List<Adat_Kerék_Eszterga_Naptár> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                string munkalap = "GépIdő";
                MyE.Új_munkalap(munkalap);
                MyE.Kiir("Tevékenység", "A1");
                MyE.Kiir("Gépidő", "B1");
                MyE.Kiir("%-os megoszlás", "C1");

                MyE.Kiir("Hatékonyság", "m1");
                MyE.Kiir("", "l1");
                MyE.Kiir("Norma idő [perc]", "l2");
                MyE.Kiir("Tervezési idő [perc]", "l3");
                MyE.Kiir(NormaIdő.ToString(), "m2");

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

                        MyE.Kiir(tevékenység, "A" + sor);
                        if (tevékenység == "Esztergálás") MyE.Kiir((darab * 30).ToString(), "m3");

                        MyE.Kiir((darab * 30).ToString(), "B" + sor);
                    }
                }
                összesen = összesen == 0 ? 1 : összesen * 30; //ne osszunk váletlenül sem nullával
                for (int i = 2; i <= sor; i++)
                {
                    MyE.Kiir($"=RC[-1]/{összesen}", "C" + i);
                    MyE.Betű("C" + i, "Percent", "");
                }

                MyE.Diagram(munkalap, 10, 150, 500, 500, "A1", "B" + sor);
                MyE.Rácsoz("A1:C" + sor);
                MyE.Vastagkeret("A1:C" + sor);
                MyE.Oszlopszélesség(munkalap, "A:C");
                MyE.Háttérszín("A1:C1", System.Drawing.Color.Yellow);
                MyE.Szűrés(munkalap, "A", "C", 1);

                //kis tábla
                MyE.Rácsoz("l1:m3");
                MyE.Vastagkeret("l1:m3");
                MyE.Oszlopszélesség(munkalap, "l:m");
                MyE.Háttérszín("l1:m1", System.Drawing.Color.Yellow);
                MyE.Diagram(munkalap, 600, 150, 500, 500, "l1", "m3");


                MyE.NyomtatásiTerület_részletes(munkalap, "A1:Q" + sor, "1:1", "", true);
                MyE.Aktív_Cella(munkalap, "A1");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

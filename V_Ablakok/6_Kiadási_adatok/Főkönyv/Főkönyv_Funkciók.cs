using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public static class Főkönyv_Funkciók
    {
        readonly static Kezelő_Főkönyv_Zser_Km KézFőZserKm = new Kezelő_Főkönyv_Zser_Km();
        readonly static Kezelő_Főkönyv_ZSER KézFőZser = new Kezelő_Főkönyv_ZSER();
        readonly static Kezelő_Főkönyv_Nap KézFőkönyvNap = new Kezelő_Főkönyv_Nap();
        readonly static Kezelő_Nap_Hiba KézHibaÚj = new Kezelő_Nap_Hiba();
        readonly static Kezelő_jármű_hiba KézJárműHiba = new Kezelő_jármű_hiba();
        readonly static Kezelő_Jármű_Javításiátfutástábla KézXnapos = new Kezelő_Jármű_Javításiátfutástábla();
        readonly static Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly static Kezelő_Kiadás_Összesítő KézKiadÖ = new Kezelő_Kiadás_Összesítő();
        readonly static Kezelő_Főkönyv_Személyzet Kéz_Személy = new Kezelő_Főkönyv_Személyzet();
        readonly static Kezelő_Főkönyv_Típuscsere Kéz_Típus = new Kezelő_Főkönyv_Típuscsere();

        public static void Napiállók(string Telephely)
        {
            try
            {
                // kitöröljük az előzményt
                KézHibaÚj.Törlés(Telephely);
                List<Adat_Jármű_hiba> AdatokJármű = KézJárműHiba.Lista_Adatok(Telephely);
                AdatokJármű = (from a in AdatokJármű
                               orderby a.Azonosító, a.Korlát descending
                               select a).ToList();

                string beálló = "_";
                string üzemképtelen = "_";
                string Üzemképes = "_";
                string azonosító = "";
                string típus = "";
                long korlát = 0;

                List<Adat_Nap_Hiba> AdatokGy = new List<Adat_Nap_Hiba>();
                Adat_Nap_Hiba ADAT;
                foreach (Adat_Jármű_hiba rekord in AdatokJármű)
                {
                    if (azonosító.Trim() == "")
                    {
                        azonosító = rekord.Azonosító;
                        típus = rekord.Típus;
                        korlát = rekord.Korlát;
                    }
                    if (azonosító != rekord.Azonosító)
                    {
                        // rögzítjük az előzőt
                        ADAT = new Adat_Nap_Hiba(azonosító, DateTime.Today, beálló, üzemképtelen, Üzemképes, típus, korlát);
                        AdatokGy.Add(ADAT);

                        beálló = "_";
                        üzemképtelen = "_";
                        Üzemképes = "_";
                        azonosító = rekord.Azonosító;
                        típus = rekord.Típus;
                        korlát = rekord.Korlát;
                    }
                    if (korlát < rekord.Korlát) korlát = rekord.Korlát;
                    switch (rekord.Korlát)
                    {
                        case 1:
                            {
                                if (Üzemképes == "_")
                                    Üzemképes = rekord.Hibaleírása;
                                else
                                    Üzemképes += "+" + rekord.Hibaleírása;
                                break;
                            }
                        case 2:
                            {
                                if (beálló == "_")
                                    beálló = rekord.Hibaleírása;
                                else
                                    beálló += "+" + rekord.Hibaleírása;
                                break;
                            }
                        case 3:
                            {
                                if (beálló == "_")
                                    beálló = rekord.Hibaleírása;
                                else
                                    beálló += "+" + rekord.Hibaleírása;
                                break;
                            }
                        case 4:
                            {
                                if (üzemképtelen == "_")
                                    üzemképtelen = rekord.Hibaleírása;
                                else
                                    üzemképtelen += "+" + rekord.Hibaleírása;
                                break;
                            }
                    }
                }

                // rögzítjük az utolsót
                ADAT = new Adat_Nap_Hiba(azonosító, DateTime.Today, beálló, üzemképtelen, Üzemképes, típus, korlát);
                AdatokGy.Add(ADAT);

                KézHibaÚj.Rögzítés(Telephely, AdatokGy);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Napi állók feltöltése", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void SUBnapihibagöngyölés(string Telephely)
        {
            try
            {
                // napi állók xnapos tábla
                List<Adat_Jármű_Javításiátfutástábla> AdatokXnapos = KézXnapos.Lista_Adatok(Telephely.Trim());
                List<Adat_Jármű_hiba> AdatokHiba = KézJárműHiba.Lista_Adatok(Telephely.Trim());

                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok(Telephely.Trim());
                AdatokJármű = (from a in AdatokJármű
                               where a.Státus == 4
                               select a).ToList();

                // először az új elemeket rögzítése
                List<Adat_Jármű_Javításiátfutástábla> AdatokGyM = new List<Adat_Jármű_Javításiátfutástábla>();
                List<Adat_Jármű_Javításiátfutástábla> AdatokGyR = new List<Adat_Jármű_Javításiátfutástábla>();
                if (AdatokJármű.Count >= 1)
                {
                    foreach (Adat_Jármű rekord in AdatokJármű)
                    {
                        Adat_Jármű_Javításiátfutástábla ElemXnapos = (from a in AdatokXnapos
                                                                      where a.Azonosító == rekord.Azonosító
                                                                      select a).FirstOrDefault();
                        // ha nincs ilyen pályaszám akkor rögzítjük
                        if (ElemXnapos == null)
                        {
                            Adat_Jármű_Javításiátfutástábla ADATR = new Adat_Jármű_Javításiátfutástábla(
                                                    rekord.Miótaáll,
                                                    new DateTime(1900, 1, 1),
                                                    rekord.Azonosító.Trim(),
                                                    "?");
                            AdatokGyR.Add(ADATR);
                        }

                        // rögzítjük/módosítjuk a hibákat
                        List<Adat_Jármű_hiba> HAdatok = (from a in AdatokHiba
                                                         where a.Korlát == 4 && a.Azonosító == rekord.Azonosító
                                                         select a).ToList();

                        string hibaleírása = "";
                        foreach (Adat_Jármű_hiba rekordhiba in HAdatok)
                            hibaleírása += rekordhiba.Hibaleírása.Trim() + "-";

                        if (hibaleírása.Trim() == "") hibaleírása = "?";
                        Adat_Jármű_Javításiátfutástábla ADATM = new Adat_Jármű_Javításiátfutástábla(
                                            rekord.Azonosító.Trim(),
                                            hibaleírása);
                        AdatokGyM.Add(ADATM);

                    }
                    if (AdatokGyR.Count > 0) KézXnapos.Rögzítés(Telephely.Trim(), AdatokGyR);
                    if (AdatokGyM.Count > 0) KézXnapos.Módosítás(Telephely.Trim(), AdatokGyM);
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "SUBnapihibagöngyölés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void SUBNapielkészültek(DateTime Dátum, string Telephely)
        {
            try
            {   // az új megálló kocsikat rögzíti az MyAba és frissíti a hiba leírás szöveget
                // xnapos tábla
                string helyelk = $@"{Application.StartupPath}\{Telephely.Trim()}\adatok\hibanapló\Elkészült{Dátum.Year}.mdb";
                if (!File.Exists(helyelk)) Adatbázis_Létrehozás.Javításiátfutástábla(helyelk);

                List<Adat_Jármű_Javításiátfutástábla> AdatokXnapos = KézXnapos.Lista_Adatok(Telephely.Trim());
                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok(Telephely.Trim());
                AdatokJármű = (from a in AdatokJármű
                               where a.Státus == 4
                               select a).ToList();

                if (AdatokXnapos.Count >= 1)
                {
                    List<Adat_Jármű_Javításiátfutástábla> AdatokGyR = new List<Adat_Jármű_Javításiátfutástábla>();
                    List<string> AdatokGyT = new List<string>();
                    foreach (Adat_Jármű_Javításiátfutástábla rekord in AdatokXnapos)
                    {
                        // ha a státusa megváltozott akkor elkészült
                        Adat_Jármű ElemJármű = (from a in AdatokJármű
                                                where a.Státus == 4 && a.Azonosító == rekord.Azonosító
                                                select a).FirstOrDefault();

                        if (ElemJármű == null)
                        {
                            // ha elkészült akkor átírjuk az éves táblázatba
                            Adat_Jármű_Javításiátfutástábla ADAT = new Adat_Jármű_Javításiátfutástábla(
                                                        rekord.Kezdődátum,
                                                        DateTime.Today,
                                                        rekord.Azonosító,
                                                        rekord.Hibaleírása);
                            AdatokGyR.Add(ADAT);
                            //Kitöröljük a Napiból
                            AdatokGyT.Add(rekord.Azonosító);
                        }

                    }
                    if (AdatokGyR.Count > 0) KézXnapos.Rögzítés(Telephely, DateTime.Today.Year, AdatokGyR);
                    if (AdatokGyT.Count > 0) KézXnapos.Törlés(Telephely, AdatokGyT);
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "SUBNapielkészültek", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //
        public static void Napiadatokmentése(string Napszak, DateTime Dátum, string Telephely)
        {
            string hely = $@"{Application.StartupPath}\{Telephely.Trim()}\adatok\főkönyv\{Dátum:yyyy}\nap\{Dátum:yyyyMMdd}{Napszak}nap.mdb";
            string jelszó = "lilaakác";


            // ha nem létezik a fájl akkor kilép
            if (!File.Exists(hely)) return;
            string helykiadás = $@"{Application.StartupPath}\{Telephely.Trim()}\adatok\főkönyv\kiadás{Dátum:yyyy}.mdb";
            string jelszókiadás = "plédke";
            string szöveg1 = $@"SELECT * FROM tábla where dátum=#{Dátum:MM-dd-yyyy}# and napszak='{Napszak.Trim()}'";

            List<Adat_Kiadás_összesítő> AdatokKiadás = KézKiadÖ.Lista_adatok(helykiadás, jelszókiadás, szöveg1);

            int eforgalomban = 0;
            int etartalék = 0;
            int ekocsiszíni = 0;
            int efélreállítás = 0;
            int efőjavítás = 0;
            int eszemélyzet = 0;
            string etípus = "";
            string szöveg;


            // ha van ilyen adat akkor kitöröljük az összes típust
            if (AdatokKiadás != null && AdatokKiadás.Count > 0)
            {
                szöveg = $@"DELETE FROM tábla where dátum=#{Dátum:MM-dd-yyyy}# and napszak='{Napszak.Trim()}'";
                MyA.ABtörlés(helykiadás, jelszókiadás, szöveg);
            }

            szöveg = "SELECT * FROM adattábla order by típus";

            List<Adat_Főkönyv_Nap> Adatok = KézFőkönyvNap.Lista_adatok(hely, jelszó, szöveg);

            foreach (Adat_Főkönyv_Nap rekord in Adatok)
            {
                if (etípus.Trim() == "") etípus = rekord.Típus.Trim();
                if (etípus.Trim() != rekord.Típus.Trim())
                {
                    // ha különböző akkor rögzíti a fájlba
                    szöveg = "INSERT INTO tábla (dátum, napszak, típus, forgalomban, tartalék, kocsiszíni, félreállítás, főjavítás, személyzet) VALUES (";
                    szöveg += "'" + Dátum.ToString("yyyy.MM.dd") + "', ";
                    szöveg += "'" + Napszak.Trim() + "', ";
                    szöveg += "'" + etípus.Trim() + "', ";
                    szöveg += eforgalomban.ToString() + ", ";
                    szöveg += etartalék.ToString() + ", ";
                    szöveg += ekocsiszíni.ToString() + ", ";
                    szöveg += efélreállítás.ToString() + ", ";
                    szöveg += efőjavítás.ToString() + ", ";
                    szöveg += eszemélyzet.ToString() + ") ";
                    MyA.ABMódosítás(helykiadás, jelszókiadás, szöveg);

                    // rögzítés után lenullázzuk a számlálókat
                    eforgalomban = 0;
                    etartalék = 0;
                    ekocsiszíni = 0;
                    efélreállítás = 0;
                    efőjavítás = 0;
                    eszemélyzet = 0;
                    etípus = rekord.Típus.Trim();
                }
                if (etípus.Trim() == rekord.Típus.Trim())
                {
                    // ha ugyanaz akkor összesítjük elemenként

                    // megvizsgáljuk a kocsit
                    if (rekord.Napszak.Trim() == "DE" || rekord.Napszak.Trim() == "DU")
                    {
                        if (rekord.Megjegyzés.ToUpper().Trim().Substring(0, 1) == "S")
                            eszemélyzet += 1;
                        else if (rekord.Napszak.Trim() == Napszak.ToUpper())
                        {
                            // ha forgalomban volt
                            eforgalomban += 1;
                        }
                    }
                    // ha nem volt forgalomban és nem áll akkor tartalék
                    else if (rekord.Státus != 4)
                    {
                        etartalék += 1;
                    }
                    // ha nem félreállított vagy nem főjavítás soros, akkor kocsiszíni
                    else if (!(rekord.Hibaleírása.Contains("#") || rekord.Hibaleírása.Contains("&")))
                    {
                        ekocsiszíni += 1;
                    }
                    // félreállítás
                    else if (rekord.Hibaleírása.Contains("#"))
                    {
                        efőjavítás += 1;
                    }
                    else
                    {
                        efélreállítás += 1;
                    }
                }
            }
            // ha már nincs több akkor rögzíti az utolsó típus adatokat
            szöveg = "INSERT INTO tábla (dátum, napszak, típus, forgalomban, tartalék, kocsiszíni, félreállítás, főjavítás, személyzet) VALUES (";
            szöveg += "'" + Dátum.ToString("yyyy.MM.dd") + "', ";
            szöveg += "'" + Napszak.Trim() + "', ";
            szöveg += "'" + etípus.Trim() + "', ";
            szöveg += eforgalomban.ToString() + ", ";
            szöveg += etartalék.ToString() + ", ";
            szöveg += ekocsiszíni.ToString() + ", ";
            szöveg += efélreállítás.ToString() + ", ";
            szöveg += efőjavítás.ToString() + ", ";
            szöveg += eszemélyzet.ToString() + ") ";
            MyA.ABMódosítás(helykiadás, jelszókiadás, szöveg);
        }
        //
        public static void Napitipuscsere(string Napszak, DateTime Dátum, string Telephely)
        {
            List<Adat_Főkönyv_Nap> AdatokNap = KézFőkönyvNap.Lista_Adatok(Telephely, Dátum, Napszak);


            string helyzser = $@"{Application.StartupPath}\{Telephely.Trim()}\adatok\főkönyv\{Dátum:yyyy}\zser\zser{Dátum:yyyyMMdd}{Napszak}.mdb";
            if (!File.Exists(helyzser)) return;
            string helykieg = $@"{Application.StartupPath}\{Telephely.Trim()}\adatok\segéd\Kiegészítő.mdb";
            Kezelő_Telep_Kieg_Fortetípus KézKiegTipus = new Kezelő_Telep_Kieg_Fortetípus();
            List<Adat_Telep_Kieg_Fortetípus> AdatokKiegTipus = KézKiegTipus.Lista_Adatok(Telephely.Trim());

            List<Adat_FőKönyv_Típuscsere> AdatokTípus = Kéz_Típus.Lista_Adatok(Telephely.Trim(), Dátum.Year);
            bool vane = AdatokTípus.Any(t => t.Dátum == Dátum && t.Napszak.Trim() == Napszak.Trim());
            if (vane) Kéz_Típus.Törlés(Telephely, Dátum.Year, Napszak, Dátum);    // Adott napi adatokat kitöröljük

            string jelszó = "lilaakác";
            string szöveg = "SELECT * FROM zseltábla ORDER BY  viszonylat,forgalmiszám,tervindulás";
            Kezelő_Főkönyv_ZSER KFZS_kéz = new Kezelő_Főkönyv_ZSER();
            List<Adat_Főkönyv_ZSER> Adatok = KFZS_kéz.Lista_adatok(helyzser, jelszó, szöveg);

            string jótípus;
            string jótípusalap;
            // végig nézzük a zser adatait

            foreach (Adat_Főkönyv_ZSER rekord in Adatok)
            {
                // ha reggeli,vagy esti  csak akkor rögzíti
                if (rekord.Napszak.Trim() == "DE" || rekord.Napszak.Trim() == "DU")
                {
                    // ha megtalálta akkor megkeressük, hogy milyen típust kellett volna kiadni a forgalmi számba
                    Adat_Telep_Kieg_Fortetípus ElemTípus = (from a in AdatokKiegTipus
                                                            where a.Ftípus.ToUpper() == rekord.Szerelvénytípus.Trim().ToUpper()
                                                            select a).FirstOrDefault();
                    jótípus = "_";
                    if (ElemTípus != null) jótípus = ElemTípus.Típus.ToUpper();

                    // leellenőrizzük a pályaszámokat
                    for (int i = 1; i <= 6; i++)
                    {
                        string ideigpsz = "";
                        switch (i)
                        {
                            case 1:
                                ideigpsz = rekord.Kocsi1.Trim();
                                break;
                            case 2:
                                ideigpsz = rekord.Kocsi2.Trim();
                                break;
                            case 3:
                                ideigpsz = rekord.Kocsi3.Trim();
                                break;
                            case 4:
                                ideigpsz = rekord.Kocsi4.Trim();
                                break;
                            case 5:
                                ideigpsz = rekord.Kocsi5.Trim();
                                break;
                            case 6:
                                ideigpsz = rekord.Kocsi6.Trim();
                                break;
                        }
                        // csak a pályaszámokat ellenőrizzük le
                        if (ideigpsz.Trim() != "_")
                        {
                            Adat_Főkönyv_Nap ElemNap = (from a in AdatokNap
                                                        where a.Azonosító == ideigpsz.Trim()
                                                        select a).FirstOrDefault();
                            jótípusalap = "_";
                            if (ElemNap != null) jótípusalap = ElemNap.Típus.ToUpper();

                            if (jótípus.Trim() != jótípusalap.Trim())
                            {
                                szöveg = "INSERT INTO típuscseretábla (dátum, napszak, típuselőírt, típuskiadott, viszonylat, forgalmiszám, tervindulás, azonosító, kocsi ) VALUES (";
                                szöveg += "'" + Dátum.ToString("yyyy.MM.dd") + "', ";
                                szöveg += "'" + Napszak.Trim() + "', ";
                                szöveg += "'" + jótípus.Trim() + "', ";
                                szöveg += "'" + jótípusalap.Trim() + "', ";
                                szöveg += "'" + rekord.Viszonylat.Trim() + "', ";
                                szöveg += "'" + rekord.Forgalmiszám.Trim() + "', ";
                                szöveg += "'" + rekord.Tervindulás.ToString() + "', ";
                                szöveg += "'" + ideigpsz.Trim() + "', ";
                                szöveg += "'" + "kocsi" + i.ToString() + "') ";
                                string helytípus = $@"{Application.StartupPath}\{Telephely.Trim()}\adatok\főkönyv\típuscsere{Dátum:yyyy}.mdb";
                                string jelszótípus = "plédke";
                                MyA.ABMódosítás(helytípus, jelszótípus, szöveg);
                            }
                        }
                    }
                }
            }
        }

        public static void Napiszemélyzet(string Napszak, DateTime Dátum, string Telephely)
        {
            try
            {
                List<Adat_Főkönyv_Személyzet> Adatok_Személy = Kéz_Személy.Lista_adatok(Telephely.Trim(), Dátum.Year);
                bool vane = Adatok_Személy.Any(t => t.Dátum == Dátum && t.Napszak.Trim() == Napszak.Trim());
                if (vane) Kéz_Személy.Törlés(Telephely, Dátum.Year, Napszak, Dátum);    // Adott napi adatokat kitöröljük

                List<Adat_Főkönyv_Nap> Adatok = KézFőkönyvNap.Lista_Adatok(Telephely, Dátum, Napszak);
                Adatok = (from a in Adatok
                          where a.Megjegyzés.ToUpper().Substring(0, 1) == "S"
                          orderby a.Típus
                          select a).ToList();

                foreach (Adat_Főkönyv_Nap rekord in Adatok)
                {
                    Adat_Főkönyv_Személyzet ADAT = new Adat_Főkönyv_Személyzet(
                                           Dátum,
                                           Napszak,
                                           rekord.Típus.Trim(),
                                           rekord.Viszonylat.Trim(),
                                           rekord.Forgalmiszám.Trim(),
                                           rekord.Tervindulás,
                                           rekord.Azonosító.Trim());
                    Kéz_Személy.Rögzítés(Telephely, Dátum.Year, ADAT);
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Napiszemélyzet", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Napitöbblet(string Napszak, DateTime Dátum, string Telephely)
        {
            try
            {
                List<Adat_Főkönyv_Nap> Adatok = KézFőkönyvNap.Lista_Adatok(Telephely, Dátum, Napszak);
                Adatok = (from a in Adatok
                          where a.Megjegyzés.ToUpper().Substring(0, 1) == "T"
                          orderby a.Típus
                          select a).ToList();

                foreach (Adat_Főkönyv_Nap rekord in Adatok)
                {
                    Adat_FőKönyv_Típuscsere ADAT = new Adat_FőKönyv_Típuscsere(
                                           Dátum,
                                           Napszak,
                                           "Többlet kiadás",
                                           rekord.Típus.Trim(),
                                           rekord.Viszonylat.Trim(),
                                           rekord.Forgalmiszám.Trim(),
                                           rekord.Tervindulás,
                                           rekord.Azonosító.Trim(),
                                           "kocsi1");
                    Kéz_Típus.Rögzítés(Telephely, Dátum.Year, ADAT);
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Napitöbblet", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //
        public static void ZSER_Betöltés(string hely, string ExcelFájl, DateTime Dátum, string Telephely, long kiadási_korr = 0, long érkezési_korr = 0)
        {
            try
            {
                // megnyitjuk a beolvasandó táblát
                MyE.ExcelMegnyitás(ExcelFájl);

                // leellenőrizzük, hogy az adat nap egyezik-e
                if (MyE.BeolvasDátum("d2").ToString("yyyyMMdd") != Dátum.ToString("yyyyMMdd"))
                {
                    // ha nem egyezik akkor
                    MyE.ExcelBezárás();
                    throw new HibásBevittAdat("A betölteni kívánt adatok nem egyeznek meg a beállított nappal !");
                }

                string munkalap = "Sheet1";
                // megnézzük, hogy hány sorból áll a tábla
                int utolsó = MyE.Utolsósor(munkalap);
                Km_adatok_beolvasása(utolsó, kiadási_korr, érkezési_korr, Dátum, Telephely);

                // megnézzük, hogy hány sorból áll a tábla
                int i = 1;

                string jelszó = "lilaakác";
                string szöveg;
                if (utolsó > 1)
                {
                    i = 2;
                    List<string> szövegGy = new List<string>();
                    while (utolsó + 1 != i)
                    {
                        szöveg = "INSERT INTO ZSELtábla (viszonylat, forgalmiszám, tervindulás, tényindulás, tervérkezés, tényérkezés, státus, ";
                        szöveg += " szerelvénytípus, kocsikszáma, megjegyzés, kocsi1, kocsi2, kocsi3, kocsi4, kocsi5, kocsi6, ellenőrző, napszak)  VALUES (";
                        szöveg += $"'{MyE.Beolvas($"b{i}")}', ";
                        szöveg += $"'{MyE.Beolvas($"c{i}")}', ";

                        DateTime idegignap = MyE.BeolvasDátum($"D{i}");
                        DateTime idegigóra = MyE.Beolvasidő($"E{i}");
                        DateTime ideigdátum = new DateTime(idegignap.Year, idegignap.Month, idegignap.Day, idegigóra.Hour, idegigóra.Minute, idegigóra.Second);
                        ideigdátum = ideigdátum.AddMinutes(kiadási_korr);
                        szöveg += $"'{ideigdátum:yyyy.MM.dd HH:mm:ss}', ";

                        idegignap = MyE.BeolvasDátum($"F{i}");
                        idegigóra = MyE.Beolvasidő($"G{i}");
                        ideigdátum = new DateTime(idegignap.Year, idegignap.Month, idegignap.Day, idegigóra.Hour, idegigóra.Minute, idegigóra.Second);
                        ideigdátum = ideigdátum.AddMinutes(kiadási_korr);
                        szöveg += $"'{ideigdátum:yyyy.MM.dd HH:mm:ss}', ";

                        idegignap = MyE.BeolvasDátum($"H{i}");
                        idegigóra = MyE.Beolvasidő($"I{i}");
                        ideigdátum = new DateTime(idegignap.Year, idegignap.Month, idegignap.Day, idegigóra.Hour, idegigóra.Minute, idegigóra.Second);
                        ideigdátum = ideigdátum.AddMinutes(érkezési_korr);
                        szöveg += $"'{ideigdátum:yyyy.MM.dd HH:mm:ss}', ";

                        idegignap = MyE.BeolvasDátum($"J{i}");
                        idegigóra = MyE.Beolvasidő($"K{i}");
                        ideigdátum = new DateTime(idegignap.Year, idegignap.Month, idegignap.Day, idegigóra.Hour, idegigóra.Minute, idegigóra.Second);
                        ideigdátum = ideigdátum.AddMinutes(érkezési_korr);
                        szöveg += $"'{ideigdátum:yyyy.MM.dd HH:mm:ss}', ";

                        szöveg += $"'{MyF.Szöveg_Tisztítás(MyE.Beolvas($"l{i}"), 0, 10)}', ";
                        szöveg += $"'{MyE.Beolvas($"m{i}")}', ";
                        szöveg += $"{MyE.Beolvas($"o{i}")}, ";
                        szöveg += $"'{MyF.Szöveg_Tisztítás(MyE.Beolvas($"r{i}"), 0, 20)}', ";

                        string ideig = MyE.Beolvas($"S{i}").Trim();
                        szöveg += $"'{Pályaszám_csorbítás(ideig.Trim())}', ";

                        ideig = MyE.Beolvas($"U{i}").Trim();
                        szöveg += $"'{Pályaszám_csorbítás(ideig.Trim())}', ";

                        ideig = MyE.Beolvas($"W{i}").Trim();
                        szöveg += $"'{Pályaszám_csorbítás(ideig.Trim())}', ";

                        ideig = MyE.Beolvas($"Y{i}").Trim();
                        szöveg += $"'{Pályaszám_csorbítás(ideig.Trim())}', ";

                        ideig = MyE.Beolvas($"AA{i}").Trim();
                        szöveg += $"'{Pályaszám_csorbítás(ideig.Trim())}', ";

                        ideig = MyE.Beolvas($"AC{i}").Trim();
                        szöveg += $"'{Pályaszám_csorbítás(ideig.Trim())}', ";

                        szöveg += "'_', '*' )";

                        szövegGy.Add(szöveg);
                        i++;

                    }
                    MyA.ABMódosítás(hely, jelszó, szövegGy);
                }
                // az excel tábla bezárása
                MyE.ExcelBezárás();

                // kitöröljük a betöltött fájlt
                if (File.Exists(ExcelFájl)) File.Delete(ExcelFájl);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                if (ex.HResult == -2147024860 || ex.HResult == -2147024864)
                {
                    MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    HibaNapló.Log(ex.Message, "ZSER_Betöltés", ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static void ZSER_Betöltés(string Telephely, DateTime Dátum, string Napszak, string ExcelFájl, long kiadási_korr = 0, long érkezési_korr = 0)
        {
            try
            {
                // megnyitjuk a beolvasandó táblát
                MyE.ExcelMegnyitás(ExcelFájl);

                // leellenőrizzük, hogy az adat nap egyezik-e
                if (MyE.BeolvasDátum("d2").ToString("yyyyMMdd") != Dátum.ToString("yyyyMMdd"))
                {
                    // ha nem egyezik akkor
                    MyE.ExcelBezárás();
                    throw new HibásBevittAdat("A betölteni kívánt adatok nem egyeznek meg a beállított nappal !");
                }

                string munkalap = "Sheet1";
                // megnézzük, hogy hány sorból áll a tábla
                int utolsó = MyE.Utolsósor(munkalap);
                Km_adatok_beolvasása(utolsó, kiadási_korr, érkezési_korr, Dátum, Telephely);

                // megnézzük, hogy hány sorból áll a tábla
                List<Adat_Főkönyv_ZSER> AdatokGy = new List<Adat_Főkönyv_ZSER>();
                for (int i = 2; i < utolsó; i++)
                {
                    string viszonylat = MyE.Beolvas($"b{i}");
                    string forgalmiszám = MyE.Beolvas($"c{i}");

                    DateTime idegignap = MyE.BeolvasDátum($"D{i}");
                    DateTime idegigóra = MyE.Beolvasidő($"E{i}");
                    DateTime ideigdátum = new DateTime(idegignap.Year, idegignap.Month, idegignap.Day, idegigóra.Hour, idegigóra.Minute, idegigóra.Second);
                    ideigdátum = ideigdátum.AddMinutes(kiadási_korr);
                    DateTime tervindulás = ideigdátum;

                    idegignap = MyE.BeolvasDátum($"F{i}");
                    idegigóra = MyE.Beolvasidő($"G{i}");
                    ideigdátum = new DateTime(idegignap.Year, idegignap.Month, idegignap.Day, idegigóra.Hour, idegigóra.Minute, idegigóra.Second);
                    ideigdátum = ideigdátum.AddMinutes(kiadási_korr);
                    DateTime tényindulás = ideigdátum;

                    idegignap = MyE.BeolvasDátum($"H{i}");
                    idegigóra = MyE.Beolvasidő($"I{i}");
                    ideigdátum = new DateTime(idegignap.Year, idegignap.Month, idegignap.Day, idegigóra.Hour, idegigóra.Minute, idegigóra.Second);
                    ideigdátum = ideigdátum.AddMinutes(érkezési_korr);
                    DateTime tervérkezés = ideigdátum;

                    idegignap = MyE.BeolvasDátum($"J{i}");
                    idegigóra = MyE.Beolvasidő($"K{i}");
                    ideigdátum = new DateTime(idegignap.Year, idegignap.Month, idegignap.Day, idegigóra.Hour, idegigóra.Minute, idegigóra.Second);
                    ideigdátum = ideigdátum.AddMinutes(érkezési_korr);
                    DateTime tényérkezés = ideigdátum;

                    string napszak = "*";
                    string szerelvénytípus = MyE.Beolvas($"m{i}");
                    long kocsikszáma = long.Parse(MyE.Beolvas($"o{i}"));
                    string megjegyzés = MyF.Szöveg_Tisztítás(MyE.Beolvas($"r{i}"), 0, 20);

                    string ideig = MyE.Beolvas($"S{i}").Trim();
                    string kocsi1 = Pályaszám_csorbítás(ideig.Trim());
                    ideig = MyE.Beolvas($"U{i}").Trim();
                    string kocsi2 = Pályaszám_csorbítás(ideig.Trim());
                    ideig = MyE.Beolvas($"W{i}").Trim();
                    string kocsi3 = Pályaszám_csorbítás(ideig.Trim());
                    ideig = MyE.Beolvas($"Y{i}").Trim();
                    string kocsi4 = Pályaszám_csorbítás(ideig.Trim());
                    ideig = MyE.Beolvas($"AA{i}").Trim();
                    string kocsi5 = Pályaszám_csorbítás(ideig.Trim());
                    ideig = MyE.Beolvas($"AC{i}").Trim();
                    string kocsi6 = Pályaszám_csorbítás(ideig.Trim());
                    string ellenőrző = "_";
                    string státus = MyF.Szöveg_Tisztítás(MyE.Beolvas($"l{i}"), 0, 10);


                    Adat_Főkönyv_ZSER ADAT = new Adat_Főkönyv_ZSER(
                                viszonylat,
                                forgalmiszám,
                                tervindulás,
                                tényindulás,
                                tervérkezés,
                                tényérkezés,
                                napszak,
                                szerelvénytípus,
                                kocsikszáma,
                                megjegyzés,
                                kocsi1,
                                kocsi2,
                                kocsi3,
                                kocsi4,
                                kocsi5,
                                kocsi6,
                                ellenőrző,
                                státus);
                    AdatokGy.Add(ADAT);
                }
                KézFőZser.Rögzítés(Telephely, Dátum, Napszak, AdatokGy);
                // az excel tábla bezárása
                MyE.ExcelBezárás();

                // kitöröljük a betöltött fájlt
                if (File.Exists(ExcelFájl)) File.Delete(ExcelFájl);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {

                if (ex.HResult == -2147024860 || ex.HResult == -2147024864)
                {
                    MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    HibaNapló.Log(ex.Message, "ZSER_Betöltés", ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private static string Pályaszám_csorbítás(string mit)
        {
            if (mit == null || mit.Trim() == "") mit = "_";
            if (mit != "_")
            {
                if (mit.Length > 10) mit.Substring(0, 10);
                mit = mit.Substring(1).Trim();
                if (mit.Length < 4)
                    mit = new string('0', 4 - mit.Length) + mit;
            }
            return mit;
        }

        private static void Km_adatok_beolvasása(int sormax, long kiadásikorr, long érkezésikorr, DateTime Dátum, string Telephely)
        {
            string szöveg = "";
            int oszlop = 0;
            int i = 0;
            try
            {
                List<Adat_Főkönyv_Zser_Km> Adatok = KézFőZserKm.Lista_adatok(Dátum.Year);

                List<Adat_Főkönyv_Zser_Km> Elemek = (from a in Adatok
                                                     where a.Telephely == Telephely.Trim() && a.Dátum.ToShortDateString() == Dátum.ToShortDateString()
                                                     select a).ToList();

                // leellenőrizzük, hogy van-e már erre a napra rögzítve adat ha van töröljük

                if (Elemek != null) KézFőZserKm.Törlés(Telephely.Trim(), Dátum);

                string[] oszlopok = new string[7];
                oszlopok[1] = "S";
                oszlopok[2] = "U";
                oszlopok[3] = "W";
                oszlopok[4] = "Y";
                oszlopok[5] = "AA";
                oszlopok[6] = "AC";

                // beolvassuk az excel tábla szükséges adatait

                List<Adat_Főkönyv_Zser_Km> AdatokGy = new List<Adat_Főkönyv_Zser_Km>();
                for (i = 2; i <= sormax; i++)
                {
                    DateTime idegignap = MyE.Beolvas("D" + i).ToÉrt_DaTeTime();
                    DateTime idegigóra = MyE.Beolvasidő("E" + i);
                    DateTime ideigdátum = new DateTime(idegignap.Year, idegignap.Month, idegignap.Day, idegigóra.Hour, idegigóra.Minute, idegigóra.Second);
                    DateTime tervindulás = ideigdátum.AddMinutes(kiadásikorr);

                    idegignap = MyE.Beolvas("F" + i).ToÉrt_DaTeTime();
                    idegigóra = MyE.Beolvasidő("G" + i);
                    ideigdátum = new DateTime(idegignap.Year, idegignap.Month, idegignap.Day, idegigóra.Hour, idegigóra.Minute, idegigóra.Second);
                    DateTime tényindulás = ideigdátum.AddMinutes(kiadásikorr);

                    idegignap = MyE.Beolvas("H" + i).ToÉrt_DaTeTime();
                    idegigóra = MyE.Beolvasidő("I" + i);
                    ideigdátum = new DateTime(idegignap.Year, idegignap.Month, idegignap.Day, idegigóra.Hour, idegigóra.Minute, idegigóra.Second);
                    DateTime tervérkezés = ideigdátum.AddMinutes(érkezésikorr);

                    idegignap = MyE.Beolvas("J" + i).ToÉrt_DaTeTime();
                    idegigóra = MyE.Beolvasidő("K" + i);
                    ideigdátum = new DateTime(idegignap.Year, idegignap.Month, idegignap.Day, idegigóra.Hour, idegigóra.Minute, idegigóra.Second);
                    DateTime tényérkezés = ideigdátum.AddMinutes(érkezésikorr);


                    string kms = MyE.Beolvas("ae" + i.ToString());
                    if (!int.TryParse(kms, out int km)) km = 0;

                    TimeSpan számhossz = tervérkezés - tervindulás;
                    TimeSpan menethossz = tényérkezés - tényindulás;

                    if (számhossz.TotalMinutes != menethossz.TotalMinutes && menethossz.TotalMinutes != 0)
                    {
                        //Ha nem a teljes számot járja le akkor kiszámoljuk a töredék km-t.
                        km = (int)((km * menethossz.TotalMinutes) / számhossz.TotalMinutes);
                    }


                    for (oszlop = 1; oszlop <= 6; oszlop++)
                    {

                        string szövegideig = MyE.Beolvas(oszlopok[oszlop] + i).Trim();
                        if (szövegideig != "")
                        {
                            string azonosító = "";
                            szövegideig = MyF.Szöveg_Tisztítás(szövegideig, 1, 4);
                            if (szövegideig.Trim().Length < 4)
                            {
                                //Fogaskerekű pályaszáma
                                string ideigpsz = new string('0', 4 - szövegideig.Trim().Length);
                                azonosító = ideigpsz + szövegideig.Trim();
                            }
                            else
                                azonosító = szövegideig.Trim();
                            Adat_Főkönyv_Zser_Km ADAT = new Adat_Főkönyv_Zser_Km(
                                                     azonosító.Trim(),
                                                     tervindulás,
                                                     km,
                                                     Telephely.Trim());
                            AdatokGy.Add(ADAT);
                        }
                    }
                }
                KézFőZserKm.Rögzítés(AdatokGy, Dátum.Year);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Km_adatok_beolvasása", ex.StackTrace, ex.Source, ex.HResult, $"{szöveg}\nOszlop:{oszlop}\nSor:{i}");
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception("MyA rögzítési hiba, az adotok rögzítése/módosítása nem történt meg.");
            }
        }
    }
}

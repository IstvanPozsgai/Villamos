using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public static class Főkönyv_Funkciók
    {
        readonly static Kezelő_Főkönyv_ZSER KézFőZser = new Kezelő_Főkönyv_ZSER();
        readonly static Kezelő_Főkönyv_Nap KézFőkönyvNap = new Kezelő_Főkönyv_Nap();
        readonly static Kezelő_Nap_Hiba KézHibaÚj = new Kezelő_Nap_Hiba();
        readonly static Kezelő_jármű_hiba KézJárműHiba = new Kezelő_jármű_hiba();
        readonly static Kezelő_Jármű_Javításiátfutástábla KézXnapos = new Kezelő_Jármű_Javításiátfutástábla();
        readonly static Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly static Kezelő_Kiadás_Összesítő KézKiadÖ = new Kezelő_Kiadás_Összesítő();
        readonly static Kezelő_Főkönyv_Személyzet Kéz_Személy = new Kezelő_Főkönyv_Személyzet();
        readonly static Kezelő_Főkönyv_Típuscsere Kéz_Típus = new Kezelő_Főkönyv_Típuscsere();
        readonly static Kezelő_Telep_Kieg_Fortetípus KézKiegTipus = new Kezelő_Telep_Kieg_Fortetípus();

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

                string beálló = "";
                string üzemképtelen = "";
                string Üzemképes = "";
                string azonosító = "";
                string típus = "";
                long korlát = 0;
                DateTime Dátum = new DateTime(1900, 1, 1);

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
                        ADAT = new Adat_Nap_Hiba(azonosító, Dátum, beálló, üzemképtelen, Üzemképes, típus, korlát);
                        AdatokGy.Add(ADAT);

                        beálló = "";
                        üzemképtelen = "";
                        Üzemképes = "";
                        azonosító = rekord.Azonosító;
                        típus = rekord.Típus;
                        korlát = rekord.Korlát;
                        Dátum = new DateTime(1900, 1, 1);
                    }
                    if (korlát < rekord.Korlát) korlát = rekord.Korlát;
                    switch (rekord.Korlát)
                    {
                        case 1:
                            {
                                if (Üzemképes.Trim() == "")
                                    Üzemképes = rekord.Hibaleírása;
                                else
                                    Üzemképes += "+" + rekord.Hibaleírása;
                                break;
                            }
                        case 2:
                            {
                                if (beálló.Trim() == "")
                                    beálló = rekord.Hibaleírása;
                                else
                                    beálló += "+" + rekord.Hibaleírása;
                                break;
                            }
                        case 3:
                            {
                                if (beálló.Trim() == "")
                                    beálló = rekord.Hibaleírása;
                                else
                                    beálló += "+" + rekord.Hibaleírása;
                                break;
                            }
                        case 4:
                            {
                                if (üzemképtelen.Trim() == "")
                                    üzemképtelen = rekord.Hibaleírása;
                                else
                                    üzemképtelen += "+" + rekord.Hibaleírása;
                                if (Dátum == new DateTime(1900, 1, 1) || Dátum >= rekord.Idő) Dátum = rekord.Idő;
                                break;
                            }
                    }
                }

                // rögzítjük az utolsót
                ADAT = new Adat_Nap_Hiba(azonosító, Dátum, beálló, üzemképtelen, Üzemképes, típus, korlát);
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

        public static void Napiadatokmentése(string Napszak, DateTime Dátum, string Telephely)
        {
            List<Adat_Kiadás_összesítő> AdatokKiadás = KézKiadÖ.Lista_Adatok(Telephely, Dátum.Year);
            AdatokKiadás = (from a in AdatokKiadás
                            where a.Dátum == Dátum && a.Napszak.Trim() == Napszak.Trim()
                            select a).ToList();

            // ha van ilyen adat akkor kitöröljük 
            if (AdatokKiadás != null && AdatokKiadás.Count > 0) KézKiadÖ.Törlés(Telephely, Dátum.Year, Dátum, Napszak);

            int eforgalomban = 0;
            int etartalék = 0;
            int ekocsiszíni = 0;
            int efélreállítás = 0;
            int efőjavítás = 0;
            int eszemélyzet = 0;
            string etípus = "";

            List<Adat_Főkönyv_Nap> Adatok = KézFőkönyvNap.Lista_Adatok(Telephely, Dátum, Napszak).OrderBy(a => a.Típus).ToList();

            foreach (Adat_Főkönyv_Nap rekord in Adatok)
            {
                if (etípus.Trim() == "") etípus = rekord.Típus.Trim();
                if (etípus.Trim() != rekord.Típus.Trim())
                {
                    // ha különböző akkor rögzíti a fájlba
                    Adat_Kiadás_összesítő ADAT = new Adat_Kiadás_összesítő(
                                          Dátum,
                                          Napszak.Trim(),
                                          etípus.Trim(),
                                          eforgalomban,
                                          etartalék,
                                          ekocsiszíni,
                                          efélreállítás,
                                          efőjavítás,
                                          eszemélyzet);
                    KézKiadÖ.Rögzítés(Telephely, Dátum.Year, ADAT);

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
            Adat_Kiadás_összesítő ADATv = new Adat_Kiadás_összesítő(
                                         Dátum,
                                         Napszak.Trim(),
                                         etípus.Trim(),
                                         eforgalomban,
                                         etartalék,
                                         ekocsiszíni,
                                         efélreállítás,
                                         efőjavítás,
                                         eszemélyzet);
            KézKiadÖ.Rögzítés(Telephely, Dátum.Year, ADATv);
        }

        public static void Napitipuscsere(string Napszak, DateTime Dátum, string Telephely)
        {
            List<Adat_Főkönyv_Nap> AdatokNap = KézFőkönyvNap.Lista_Adatok(Telephely, Dátum, Napszak);
            List<Adat_Főkönyv_ZSER> Adatok = KézFőZser.Lista_Adatok(Telephely.Trim(), Dátum, Napszak);
            List<Adat_Telep_Kieg_Fortetípus> AdatokKiegTipus = KézKiegTipus.Lista_Adatok(Telephely.Trim());
            List<Adat_FőKönyv_Típuscsere> AdatokTípus = Kéz_Típus.Lista_Adatok(Telephely.Trim(), Dátum.Year);

            bool vane = AdatokTípus.Any(t => t.Dátum == Dátum && t.Napszak.Trim() == Napszak.Trim());
            if (vane) Kéz_Típus.Törlés(Telephely, Dátum.Year, Napszak, Dátum);    // Adott napi adatokat kitöröljük

            string jótípus, jótípusalap;
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
                                Adat_FőKönyv_Típuscsere ADAT = new Adat_FőKönyv_Típuscsere(
                                                           Dátum,
                                                           Napszak.Trim(),
                                                           jótípus.Trim(),
                                                           jótípusalap.Trim(),
                                                           rekord.Viszonylat.Trim(),
                                                           rekord.Forgalmiszám.Trim(),
                                                           rekord.Tervindulás,
                                                           ideigpsz,
                                                           $"kocsi{i}");
                                Kéz_Típus.Rögzítés(Telephely.Trim(), Dátum.Year, ADAT);
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
                List<Adat_Főkönyv_Személyzet> Adatok_Személy = Kéz_Személy.Lista_Adatok(Telephely.Trim(), Dátum.Year);
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

        public static string Pályaszám_csorbítás(string mit)
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

        public static void FőadatEllenőrzése(string Telephely)
        {
            try
            {
                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok(Telephely.Trim());
                AdatokJármű = AdatokJármű.Where(a => a.Státus == 4).ToList();
                List<Adat_Jármű_hiba> AdatokHiba = KézJárműHiba.Lista_Adatok(Telephely.Trim());

                foreach (Adat_Jármű Adat in AdatokJármű)
                {
                    // ha a jármű státusza 4 akkor megnézzük, hogy van-e hiba
                    List<Adat_Jármű_hiba> AdatokGy = (from a in AdatokHiba
                                                      where a.Korlát == 4 && a.Azonosító == Adat.Azonosító
                                                      select a).ToList();
                    DateTime Miótaáll = new DateTime(1900, 1, 1, 0, 0, 0);
                    foreach (Adat_Jármű_hiba rekord in AdatokGy)
                        if (Miótaáll == new DateTime(1900, 1, 1, 0, 0, 0) || Miótaáll > rekord.Idő) Miótaáll = rekord.Idő;

                    if (Adat.Miótaáll == new DateTime(1900, 1, 1, 0, 0, 0) || Adat.Miótaáll > Miótaáll)
                        KézJármű.Módosítás_Dátum(Telephely, Adat.Azonosító, Miótaáll);
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "FőadatEllenőrzése", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

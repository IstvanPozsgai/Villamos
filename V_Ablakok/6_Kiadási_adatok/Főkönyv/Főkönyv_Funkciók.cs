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
        readonly static Kezelő_Nap_Hiba KézHibaÚj = new Kezelő_Nap_Hiba();
        readonly static Kezelő_jármű_hiba KézJárműHiba = new Kezelő_jármű_hiba();
        readonly static Kezelő_Jármű_Javításiátfutástábla KézXnapos = new Kezelő_Jármű_Javításiátfutástábla();
        readonly static Kezelő_Jármű KézJármű = new Kezelő_Jármű();

        public static void Napiállók(string telephely)
        {
            try
            {
                // kitöröljük az előzményt
                KézHibaÚj.Törlés(telephely);
                List<Adat_Jármű_hiba> AdatokJármű = KézJárműHiba.Lista_Adatok(telephely);
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

                KézHibaÚj.Rögzítés(telephely, AdatokGy);
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

        public static void SUBnapihibagöngyölés(string telephely)
        {
            try
            {
                // napi állók xnapos tábla
                List<Adat_Jármű_Javításiátfutástábla> AdatokXnapos = KézXnapos.Lista_Adatok(telephely.Trim());
                List<Adat_Jármű_hiba> AdatokHiba = KézJárműHiba.Lista_Adatok(telephely.Trim());

                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok(telephely.Trim());
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
                    if (AdatokGyR.Count > 0) KézXnapos.Rögzítés(telephely.Trim(), AdatokGyR);
                    if (AdatokGyM.Count > 0) KézXnapos.Módosítás(telephely.Trim(), AdatokGyM);
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void SUBNapielkészültek(DateTime Dátum, string telephely)
        {
            // az új megálló kocsikat rögzíti az MyAba és frissíti a hiba leírás szöveget
            // xnapos tábla
            string helyelk = $@"{Application.StartupPath}\{telephely.Trim()}\adatok\hibanapló\Elkészült{Dátum.Year}.mdb";
            if (!File.Exists(helyelk)) Adatbázis_Létrehozás.Javításiátfutástábla(helyelk);

            string hely = $@"{Application.StartupPath}\{telephely.Trim()}\adatok\hibanapló\Napi.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Javításiátfutástábla(hely);
            string jelszó = "plédke";
            string szöveg = "SELECT * FROM xnapostábla ";

            List<Adat_Jármű_Javításiátfutástábla> AdatokXnapos = KézXnapos.Lista_adatok(hely, jelszó, szöveg);


            string helyvill = $@"{Application.StartupPath}\{telephely.Trim()}\adatok\villamos\villamos.mdb";
            string jelszóvill = "pozsgaii";
            szöveg = "SELECT * FROM állománytábla where státus=4 ";

            List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok(helyvill, jelszóvill, szöveg);



            if (AdatokXnapos.Count >= 1)
            {
                List<string> SzövegGy = new List<string>();
                List<string> SzövegGyT = new List<string>();
                foreach (Adat_Jármű_Javításiátfutástábla rekord in AdatokXnapos)
                {
                    // ha a státusa megváltozott akkor elkészült
                    Adat_Jármű ElemJármű = (from a in AdatokJármű
                                            where a.Státus == 4 && a.Azonosító == rekord.Azonosító
                                            select a).FirstOrDefault();

                    if (ElemJármű == null)
                    {
                        // ha elkészült akkor átírjuk az éves táblázatba
                        szöveg = "INSERT INTO xnapostábla (azonosító, kezdődátum, végdátum, hibaleírása) VALUES (";
                        szöveg += $"'{rekord.Azonosító.Trim()}', ";
                        szöveg += $"'{rekord.Kezdődátum:yyyy.MM.dd}', ";
                        szöveg += $"'{DateTime.Today:yyyy.MM.dd}', ";
                        szöveg += $"'{rekord.Hibaleírása.Trim()}' )";
                        SzövegGy.Add(szöveg);

                        // kitöröljük a napi táblából a elkészülteket
                        szöveg = $"DELETE FROM xnapostábla WHERE azonosító='{rekord.Azonosító.Trim()}'";

                        SzövegGyT.Add(szöveg);
                    }

                }
                if (SzövegGy.Count > 0) MyA.ABMódosítás(helyelk, jelszó, SzövegGy);
                if (SzövegGyT.Count > 0) MyA.ABtörlés(hely, jelszó, SzövegGyT);
            }
        }


        public static void Napiadatokmentése(string KÜLDreggel, DateTime KÜLDdátum, string Küldtelephely)
        {
            string hely = $@"{Application.StartupPath}\{Küldtelephely.Trim()}\adatok\főkönyv\{KÜLDdátum:yyyy}\nap\{KÜLDdátum:yyyyMMdd}{KÜLDreggel}nap.mdb";
            string jelszó = "lilaakác";


            // ha nem létezik a fájl akkor kilép
            if (!File.Exists(hely)) return;
            string helykiadás = $@"{Application.StartupPath}\{Küldtelephely.Trim()}\adatok\főkönyv\kiadás{KÜLDdátum:yyyy}.mdb";
            string jelszókiadás = "plédke";
            string szöveg1 = $@"SELECT * FROM tábla where dátum=#{KÜLDdátum:MM-dd-yyyy}# and napszak='{KÜLDreggel.Trim()}'";
            Kezelő_Kiadás_Összesítő KézKiadÖ = new Kezelő_Kiadás_Összesítő();
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
                szöveg = $@"DELETE FROM tábla where dátum=#{KÜLDdátum:MM-dd-yyyy}# and napszak='{KÜLDreggel.Trim()}'";
                MyA.ABtörlés(helykiadás, jelszókiadás, szöveg);
            }

            szöveg = "SELECT * FROM adattábla order by típus";
            Kezelő_Főkönyv_Nap KFN_kéz = new Kezelő_Főkönyv_Nap();
            List<Adat_Főkönyv_Nap> Adatok = KFN_kéz.Lista_adatok(hely, jelszó, szöveg);

            foreach (Adat_Főkönyv_Nap rekord in Adatok)
            {
                if (etípus.Trim() == "") etípus = rekord.Típus.Trim();
                if (etípus.Trim() != rekord.Típus.Trim())
                {
                    // ha különböző akkor rögzíti a fájlba
                    szöveg = "INSERT INTO tábla (dátum, napszak, típus, forgalomban, tartalék, kocsiszíni, félreállítás, főjavítás, személyzet) VALUES (";
                    szöveg += "'" + KÜLDdátum.ToString("yyyy.MM.dd") + "', ";
                    szöveg += "'" + KÜLDreggel.Trim() + "', ";
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
                        else if (rekord.Napszak.Trim() == KÜLDreggel.ToUpper())
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
            szöveg += "'" + KÜLDdátum.ToString("yyyy.MM.dd") + "', ";
            szöveg += "'" + KÜLDreggel.Trim() + "', ";
            szöveg += "'" + etípus.Trim() + "', ";
            szöveg += eforgalomban.ToString() + ", ";
            szöveg += etartalék.ToString() + ", ";
            szöveg += ekocsiszíni.ToString() + ", ";
            szöveg += efélreállítás.ToString() + ", ";
            szöveg += efőjavítás.ToString() + ", ";
            szöveg += eszemélyzet.ToString() + ") ";
            MyA.ABMódosítás(helykiadás, jelszókiadás, szöveg);
        }


        public static void Napitipuscsere(string KÜLDreggel, DateTime KÜLDdátum, string Küldtelephely)
        {
            string hely = $@"{Application.StartupPath}\{Küldtelephely.Trim()}\adatok\főkönyv\{KÜLDdátum:yyyy}\nap\{KÜLDdátum:yyyyMMdd}{KÜLDreggel}nap.mdb";
            if (!File.Exists(hely)) return;
            string jelszó = "lilaakác";
            string szöveg = $"SELECT * FROM adattábla";
            Kezelő_Főkönyv_Nap KézNap = new Kezelő_Főkönyv_Nap();
            List<Adat_Főkönyv_Nap> AdatokNap = KézNap.Lista_adatok(hely, jelszó, szöveg);

            string helyzser = $@"{Application.StartupPath}\{Küldtelephely.Trim()}\adatok\főkönyv\{KÜLDdátum:yyyy}\zser\zser{KÜLDdátum:yyyyMMdd}{KÜLDreggel}.mdb";
            if (!File.Exists(helyzser)) return;

            string helykieg = $@"{Application.StartupPath}\{Küldtelephely.Trim()}\adatok\segéd\Kiegészítő.mdb";
            Kezelő_Telep_Kieg_Fortetípus KézKiegTipus = new Kezelő_Telep_Kieg_Fortetípus();
            List<Adat_Telep_Kieg_Fortetípus> AdatokKiegTipus = KézKiegTipus.Lista_Adatok(Küldtelephely.Trim());



            string helytípus = $@"{Application.StartupPath}\{Küldtelephely.Trim()}\adatok\főkönyv\típuscsere{KÜLDdátum:yyyy}.mdb";
            string jelszótípus = "plédke";
            szöveg = $@"SELECT * FROM típuscseretábla where dátum=#{KÜLDdátum:MM-dd-yyyy}# and napszak='{KÜLDreggel.Trim()}'";
            Kezelő_Főkönyv_Típuscsere KézTípus = new Kezelő_Főkönyv_Típuscsere();
            List<Adat_FőKönyv_Típuscsere> AdatokTípus = KézTípus.Lista_adatok(helytípus, jelszótípus, szöveg);

            // adott napi és napszaki típuscseréket töröljük
            if (AdatokTípus != null && AdatokTípus.Count > 0)
            {
                szöveg = $@"DELETE FROM típuscseretábla where dátum=#{KÜLDdátum:MM-dd-yyyy}# and napszak='{KÜLDreggel.Trim()}'";
                MyA.ABtörlés(helytípus, jelszótípus, szöveg);
            }

            szöveg = "SELECT * FROM zseltábla ORDER BY  viszonylat,forgalmiszám,tervindulás";
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
                                szöveg += "'" + KÜLDdátum.ToString("yyyy.MM.dd") + "', ";
                                szöveg += "'" + KÜLDreggel.Trim() + "', ";
                                szöveg += "'" + jótípus.Trim() + "', ";
                                szöveg += "'" + jótípusalap.Trim() + "', ";
                                szöveg += "'" + rekord.Viszonylat.Trim() + "', ";
                                szöveg += "'" + rekord.Forgalmiszám.Trim() + "', ";
                                szöveg += "'" + rekord.Tervindulás.ToString() + "', ";
                                szöveg += "'" + ideigpsz.Trim() + "', ";
                                szöveg += "'" + "kocsi" + i.ToString() + "') ";
                                MyA.ABMódosítás(helytípus, jelszótípus, szöveg);
                            }
                        }
                    }
                }
            }
        }


        public static void Napiszemélyzet(string KÜLDreggel, DateTime KÜLDdátum, string Küldtelephely)
        {
            string hely = $@"{Application.StartupPath}\{Küldtelephely.Trim()}\adatok\főkönyv\{KÜLDdátum.Year}\nap\{KÜLDdátum:yyyyMMdd}{KÜLDreggel}nap.mdb";

            if (!File.Exists(hely)) return;
            // ha nem létezik a fájl akkor kilép
            string jelszó = "lilaakác";

            string jelszószemélyzet = "plédke";
            string helyszemélyzet = $@"{Application.StartupPath}\{Küldtelephely.Trim()}\adatok\főkönyv\személyzet" + KÜLDdátum.ToString("yyyy") + ".mdb";

            string szöveg = "SELECT * FROM tábla";
            Kezelő_Főkönyv_Személyzet Kéz_Személy = new Kezelő_Főkönyv_Személyzet();
            List<Adat_Főkönyv_Személyzet> Adatok_Személy = Kéz_Személy.Lista_adatok(helyszemélyzet, jelszószemélyzet, szöveg);
            bool vane = Adatok_Személy.Any(t => t.Dátum.Date == KÜLDdátum.Date && t.Napszak.Trim() == KÜLDreggel.Trim());

            if (vane)
            {
                // Adott napi adatokat kitöröljük
                szöveg = "DELETE FROM tábla where dátum=#" + KÜLDdátum.ToString("MM-dd-yyyy") + "#";
                szöveg += " and napszak='" + KÜLDreggel.Trim() + "'";
                MyA.ABtörlés(helyszemélyzet, jelszószemélyzet, szöveg);
            }
            szöveg = "SELECT * FROM adattábla WHERE megjegyzés<>'_'  and viszonylat<>'-'  order by típus";

            Kezelő_Főkönyv_Nap KFN_kéz = new Kezelő_Főkönyv_Nap();
            List<Adat_Főkönyv_Nap> Adatok = KFN_kéz.Lista_adatok(hely, jelszó, szöveg);

            foreach (Adat_Főkönyv_Nap rekord in Adatok)
            {
                if (rekord.Megjegyzés.ToUpper().Substring(0, 1) == "S") // ha s betűvel kezdődik
                {
                    szöveg = "INSERT INTO tábla (dátum, napszak, típus, viszonylat, forgalmiszám, tervindulás, azonosító) VALUES (";
                    szöveg += "'" + KÜLDdátum.ToString("yyyy.MM.dd") + "', ";
                    szöveg += "'" + KÜLDreggel.Trim() + "', ";
                    szöveg += "'" + rekord.Típus.Trim() + "', ";
                    szöveg += "'" + rekord.Viszonylat.Trim() + "', ";
                    szöveg += "'" + rekord.Forgalmiszám.Trim() + "', ";
                    szöveg += "'" + rekord.Tervindulás.ToString() + "', ";
                    szöveg += "'" + rekord.Azonosító.Trim() + "') ";
                    MyA.ABMódosítás(helyszemélyzet, jelszószemélyzet, szöveg);
                }
            }

        }


        public static void Napitöbblet(string KÜLDreggel, DateTime KÜLDdátum, string Küldtelephely)
        {

            string hely = $@"{Application.StartupPath}\{Küldtelephely.Trim()}\adatok\főkönyv\{KÜLDdátum:yyyy}\nap\{KÜLDdátum:yyyyMMdd}{KÜLDreggel}nap.mdb";
            if (!File.Exists(hely))
                return;

            string jelszó = "lilaakác";
            // ha nem létezik a fájl akkor kilép
            string helytípus = $@"{Application.StartupPath}\{Küldtelephely.Trim()}\adatok\főkönyv\típuscsere" + KÜLDdátum.ToString("yyyy") + ".mdb";
            string jelszótípus = "plédke";

            //   string szöveg = "select * from típuscseretábla where dátum=#" + KÜLDdátum.ToString("MM-dd-yyyy") + "#  and napszak='" + KÜLDreggel.Trim() + "'";

            string szöveg = "SELECT * FROM adattábla WHERE megjegyzés<>'_' AND  viszonylat<>'-' order by típus";


            Kezelő_Főkönyv_Nap KFN_kéz = new Kezelő_Főkönyv_Nap();
            List<Adat_Főkönyv_Nap> Adatok = KFN_kéz.Lista_adatok(hely, jelszó, szöveg);

            foreach (Adat_Főkönyv_Nap rekord in Adatok)
            {
                if (rekord.Megjegyzés.ToUpper().Substring(0, 1) == "T")// ha t betűvel kezdődik
                {

                    szöveg = "INSERT INTO típuscseretábla (dátum, napszak, típuselőírt, típuskiadott, viszonylat, forgalmiszám, tervindulás, azonosító, kocsi) VALUES (";
                    szöveg += "'" + KÜLDdátum.ToString("yyyy.MM.dd") + "', ";
                    szöveg += "'" + KÜLDreggel.Trim() + "', ";
                    szöveg += "'Többlet kiadás', ";
                    szöveg += "'" + rekord.Típus.Trim() + "', ";
                    szöveg += "'" + rekord.Viszonylat.Trim() + "', ";
                    szöveg += "'" + rekord.Forgalmiszám.Trim() + "', ";
                    szöveg += "'" + rekord.Tervindulás.ToString() + "', ";
                    szöveg += "'" + rekord.Azonosító.Trim() + "', ";
                    szöveg += "'" + "kocsi1" + "') ";
                    MyA.ABMódosítás(helytípus, jelszótípus, szöveg);
                }
            }
        }


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

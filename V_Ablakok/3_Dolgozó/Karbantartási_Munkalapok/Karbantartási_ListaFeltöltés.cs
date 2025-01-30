using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos_Ablakok._3_Dolgozó.Karbantartási_Munkalapok
{
    public static class Karbantartási_ListaFeltöltés
    {

        public static List<Adat_Technológia_Rendelés> RendelésLista(string Telephely, DateTime Dátum)
        {
            Kezelő_Technológia_Rendelés KézRendelés = new Kezelő_Technológia_Rendelés();
            List<Adat_Technológia_Rendelés> AdatokRendelés = new List<Adat_Technológia_Rendelés>();
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely}\Adatok\\Munkalap\Rendelés.mdb";
                string jelszó = "Bezzegh";
                string szöveg = $"SELECT * FROM {Telephely} WHERE év={Dátum.Year} ORDER BY Technológia_típus, Karbantartási_fokozat";

                AdatokRendelés = KézRendelés.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "RendelésLista feltöltés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return AdatokRendelés;
        }


        public static List<Adat_technológia_Ciklus> KarbCiklusLista(string típus)
        {
            Kezelő_Technológia_Ciklus KézCiklus = new Kezelő_Technológia_Ciklus();
            List<Adat_technológia_Ciklus> AdatokCiklus = new List<Adat_technológia_Ciklus>();
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{típus}.mdb";
                string jelszó = "Bezzegh";
                string szöveg = "SELECT * FROM karbantartás ORDER BY sorszám";
                AdatokCiklus = KézCiklus.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "KarbCiklusLista feltöltés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return AdatokCiklus;
        }


        public static List<Adat_Technológia_TípusT> TípustáblaLista()
        {
            Kezelő_Technológia_TípusT KézTípusT = new Kezelő_Technológia_TípusT();
            List<Adat_Technológia_TípusT> AdatokTípusT = new List<Adat_Technológia_TípusT>();
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\technológia\technológia.mdb";
                string jelszó = "Bezzegh";
                string szöveg = "SELECT *  FROM Típus_tábla ORDER BY típus";
                AdatokTípusT = KézTípusT.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "TípustáblaLista feltöltés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return AdatokTípusT;
        }


        public static List<Adat_Technológia_TípusT> AlTípustáblaLista(string típus)
        {
            Kezelő_Technológia_TípusT KézTípusT = new Kezelő_Technológia_TípusT();
            List<Adat_Technológia_TípusT> AdatokTípusT = new List<Adat_Technológia_TípusT>();
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{típus}.mdb";
                string jelszó = "Bezzegh";
                string szöveg = "SELECT * FROM típus_tábla";
                AdatokTípusT = KézTípusT.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "TípustáblaLista feltöltés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return AdatokTípusT;
        }

        public static List<Adat_Technológia_Változat> VáltozatLista(string típus, string telephely)
        {
            Kezelő_Technológia_Változat KézVáltozat = new Kezelő_Technológia_Változat();
            List<Adat_Technológia_Változat> AdatokVáltozat = new List<Adat_Technológia_Változat>();
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{típus}.mdb";
                string jelszó = "Bezzegh";
                string szöveg = $"SELECT * FROM {telephely} ORDER BY technológia_Id ";
                AdatokVáltozat.Clear();
                AdatokVáltozat = KézVáltozat.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "VáltozatLista feltöltés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return AdatokVáltozat;
        }


        public static List<Adat_Technológia> TechnológiaLista(string típus)
        {
            Kezelő_Technológia KézTechnológia = new Kezelő_Technológia();
            List<Adat_Technológia> AdatokTechnológia = new List<Adat_Technológia>();
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{típus}.mdb";
                string jelszó = "Bezzegh";
                string szöveg = $"SELECT * FROM Technológia ";
                //    string szöveg = $"SELECT * FROM Technológia WHERE Érv_kezdete<=#{DateTime.Now:yyyy-MM-dd}# AND technológia.Érv_vége>=#{DateTime.Now:yyyy-MM-dd}# ";

                AdatokTechnológia = KézTechnológia.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "TechnológiaLista", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return AdatokTechnológia;
        }


        public static List<Adat_Technológia_Munkalap> Adatok_Egyesítése(List<Adat_Technológia> adatok_Tech, List<Adat_Technológia_Változat> adatok_telephely)
        {
            List<Adat_Technológia_Munkalap> Adatok = new List<Adat_Technológia_Munkalap>();
            try
            {
                Adat_Technológia_Munkalap Adat;
                //Üres lista
                foreach (Adat_Technológia Elem in adatok_Tech)
                {
                    //Üresek, hogy lehessen mindenkinek szűrni
                    string Karbantartási_fokozat = "";
                    string Változatnév = "";
                    string Végzi = "";
                    Adat = new Adat_Technológia_Munkalap(
                              Elem.ID,
                              Elem.Részegység,
                              Elem.Munka_utasítás_szám,
                              Elem.Utasítás_Cím,
                              Elem.Utasítás_leírás,
                              Elem.Paraméter,
                              Elem.Karb_ciklus_eleje.Sorszám,
                              Elem.Karb_ciklus_vége.Sorszám,
                              Elem.Érv_kezdete,
                              Elem.Érv_vége,
                              Elem.Szakmai_bontás,
                              Elem.Munkaterületi_bontás,
                              Elem.Altípus,
                              Elem.Kenés,
                              Karbantartási_fokozat,
                              Változatnév,
                              Végzi);
                    Adatok.Add(Adat);
                }
                foreach (Adat_Technológia Elem in adatok_Tech)
                {
                    List<Adat_Technológia_Változat> Szűrt = (from a in adatok_telephely
                                                             where a.Technológia_Id == Elem.ID
                                                             select a).ToList();
                    if (Szűrt != null)
                    {
                        foreach (Adat_Technológia_Változat Rész in Szűrt)
                        {
                            string Karbantartási_fokozat = Rész.Karbantartási_fokozat.Trim();
                            string Változatnév = Rész.Változatnév.Trim();
                            string Végzi = Rész.Végzi.Trim();
                            Adat = new Adat_Technológia_Munkalap(
                                  Elem.ID,
                                  Elem.Részegység,
                                  Elem.Munka_utasítás_szám,
                                  Elem.Utasítás_Cím,
                                  Elem.Utasítás_leírás,
                                  Elem.Paraméter,
                                  Elem.Karb_ciklus_eleje.Sorszám,
                                  Elem.Karb_ciklus_vége.Sorszám,
                                  Elem.Érv_kezdete,
                                  Elem.Érv_vége,
                                  Elem.Szakmai_bontás,
                                  Elem.Munkaterületi_bontás,
                                  Elem.Altípus,
                                  Elem.Kenés,
                                  Karbantartási_fokozat,
                                  Változatnév,
                                  Végzi);
                            Adatok.Add(Adat);
                        }
                    }
                }
                Adatok.OrderBy(a => a.ID);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Adatok_Egyesítése", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Adatok;
        }


        public static List<Adat_Dolgozó_Alap> DolgozóLista(string Telephely)
        {
            List<Adat_Dolgozó_Alap> AdatokDolgozó = new List<Adat_Dolgozó_Alap>();
            Kezelő_Dolgozó_Alap KézDolgozó = new Kezelő_Dolgozó_Alap();
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Dolgozók.mdb";
                if (!File.Exists(hely)) return AdatokDolgozó;
                string jelszó = "forgalmiutasítás";
                string szöveg = "SELECT * FROM Dolgozóadatok where kilépésiidő=#1/1/1900#   order by DolgozóNév asc";
                AdatokDolgozó = KézDolgozó.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "DologzóLista", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return AdatokDolgozó;
        }


        public static List<Adat_Kiegészítő_Csoportbeosztás> CsoportLista(string Telephely)
        {
            Kezelő_Kiegészítő_Csoportbeosztás Kéz = new Kezelő_Kiegészítő_Csoportbeosztás();
            List<Adat_Kiegészítő_Csoportbeosztás> AdatokCsoport = new List<Adat_Kiegészítő_Csoportbeosztás>();
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Segéd\kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM csoportbeosztás order by Sorszám";
                AdatokCsoport = Kéz.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "CsoportLista", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return AdatokCsoport;
        }


        public static List<Adat_Technológia_Kivételek> KivételekLista(string típus)
        {
            Kezelő_Technológia_Kivételek KézKivételek = new Kezelő_Technológia_Kivételek();
            List<Adat_Technológia_Kivételek> AdatokKivétel = new List<Adat_Technológia_Kivételek>();
            try
            {
                AdatokKivétel.Clear();
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{típus}.mdb";
                if (!File.Exists(hely)) return AdatokKivétel;
                string jelszó = "Bezzegh";
                string szöveg = szöveg = $"SELECT * FROM kivételek";
                
                AdatokKivétel = KézKivételek.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Kivételek Lista", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return AdatokKivétel;
        }


        public static List<string> T5C5_minden(string Telephely, List<Adat_Technológia_TípusT> Típus)
        {
            List<string> Adatok = new List<string>();
            Kezelő_Jármű Kéz = new Kezelő_Jármű();
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "Select * FROM Állománytábla WHERE  törölt = 0 ORDER BY azonosító";

                List<Adat_Jármű> IdeigAdatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);
                List<string> IdeigPsz;
                foreach (Adat_Technológia_TípusT rekord in Típus)
                {
                    IdeigPsz = (from a in IdeigAdatok
                                where a.Valóstípus == rekord.Típus && a.Üzem == Telephely
                                select a.Azonosító).ToList();
                    Adatok.AddRange(IdeigPsz);
                }
                Adatok.Sort();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "T5C5 Minden", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Adatok;
        }


        public static List<string> T5C5_E2(string Telephely, DateTime Dátum, List<string> Pályaszám_Adatok_típus)
        {
            Kezelő_Jármű2 KJAdat_2 = new Kezelő_Jármű2();
            List<string> Adatok = new List<string>();
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Villamos\Villamos2.mdb";
                string jelszó = "pozsgaii";
                //a dátum melyik nap
                int[] nap = { 1, 2, 3, 1, 2, 3, 0 };
                int hétnapja = (int)Dátum.DayOfWeek;

                if (hétnapja != 0)
                {
                    string szöveg = $"SELECT * FROM Állománytábla WHERE haromnapos={nap[hétnapja - 1]} ORDER BY azonosító";

                    List<Adat_Jármű_2> AdatokIdeig = KJAdat_2.Lista_Adatok(hely, jelszó, szöveg);
                    foreach (Adat_Jármű_2 item in AdatokIdeig)
                    {
                        //csak a típusnak megfelelő pályaszámokat írja ki
                        if (Pályaszám_Adatok_típus.Contains(item.Azonosító.Trim()))
                            Adatok.Add(item.Azonosító.Trim());
                    }
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "T5C5 E2 adatok", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Adatok;
        }

        public static List<string> T5C5_KarbFokozat(string Telephely, DateTime Dátum, string Fokozat, List<string> Pályaszám_Adatok_típus)
        {
            Kezelő_Vezénylés KVAdat = new Kezelő_Vezénylés();
            List<string> Adatok = new List<string>();
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely}\Adatok\főkönyv\futás\{Dátum:yyyy}\Vezénylés{Dátum:yyyy}.mdb";
                string jelszó = "tápijános";
                string szöveg = $"SELECT * FROM vezényléstábla WHERE Dátum=#{Dátum:yyyy-MM-dd}# AND törlés=0 AND Vizsgálat='{Fokozat}' ORDER BY azonosító";

                List<Adat_Vezénylés> AdatokJ = KVAdat.Lista_Adatok(hely, jelszó, szöveg);
                foreach (Adat_Vezénylés item in AdatokJ)
                {
                    //csak a típusnak megfelelő pályaszámokat írja ki
                    if (Pályaszám_Adatok_típus.Contains(item.Azonosító.Trim()))
                        Adatok.Add(item.Azonosító.Trim());
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "T5C5 E3 adatok", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Adatok;
        }


    }
}

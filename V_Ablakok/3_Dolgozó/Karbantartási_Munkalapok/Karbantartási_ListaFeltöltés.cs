using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
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
                AdatokRendelés = KézRendelés.Lista_Adatok(Telephely);
                AdatokRendelés = (from a in AdatokRendelés
                                  where a.Év == (long)Dátum.Year
                                  orderby a.Technológia_típus, a.Karbantartási_fokozat
                                  select a).ToList();
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
                AdatokCiklus = KézCiklus.Lista_Adatok(típus);
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
            Kezelő_Technológia_Típus KézTípus = new Kezelő_Technológia_Típus();
            List<Adat_Technológia_TípusT> AdatokTípusT = new List<Adat_Technológia_TípusT>();
            try
            {
                AdatokTípusT = KézTípus.Lista_Adatok();
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
                AdatokTípusT = KézTípusT.Lista_Adatok(típus);
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
                AdatokTechnológia = KézTechnológia.Lista_Adatok(típus);
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
                AdatokDolgozó = KézDolgozó.Lista_Adatok(Telephely).Where(a => a.Kilépésiidő.ToShortDateString() == new DateTime(1900, 1, 1).ToShortDateString()).OrderBy(a => a.DolgozóNév).ToList();
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
                AdatokCsoport = Kéz.Lista_Adatok(Telephely);
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
                AdatokKivétel = KézKivételek.Lista_Adatok(típus);
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
            Kezelő_Jármű KézJármű = new Kezelő_Jármű();
            try
            {
                List<Adat_Jármű> IdeigAdatok = KézJármű.Lista_Adatok("Főmérnökség").Where(a => a.Törölt == false).OrderBy(a => a.Azonosító).ToList();
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
            Kezelő_Jármű2 KézJármű2 = new Kezelő_Jármű2();
            List<string> Adatok = new List<string>();
            try
            {
                List<Adat_Jármű_2> AdatokIdeig = KézJármű2.Lista_Adatok(Telephely);
                //a dátum melyik nap
                int[] nap = { 1, 2, 3, 1, 2, 3, 0 };
                int hétnapja = (int)Dátum.DayOfWeek;

                if (hétnapja != 0)
                {
                    AdatokIdeig = AdatokIdeig.Where(a => a.Haromnapos == nap[hétnapja - 1]).OrderBy(a => a.Azonosító).ToList();
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
            Kezelő_Vezénylés KézVezénylés = new Kezelő_Vezénylés();
            List<string> Adatok = new List<string>();
            try
            {
                List<Adat_Vezénylés> AdatokJ = KézVezénylés.Lista_Adatok(Telephely, Dátum);
                AdatokJ = (from a in AdatokJ
                           where a.Dátum.ToShortDateString() == Dátum.ToShortDateString()
                           && a.Törlés == 0
                           && a.Vizsgálat == Fokozat
                           orderby a.Azonosító
                           select a).ToList();
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

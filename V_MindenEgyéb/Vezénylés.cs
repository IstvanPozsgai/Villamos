using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.V_MindenEgyéb
{
    public static class Vezénylés
    {
        readonly static Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly static Kezelő_Vezénylés KézVezénylés = new Kezelő_Vezénylés();
        readonly static Kezelő_jármű_hiba KézJárműHiba = new Kezelő_jármű_hiba();
        readonly static Kezelő_T5C5_Kmadatok KézKM = new Kezelő_T5C5_Kmadatok();

        public static void T5C5(string Telephely, DateTime Dátum)
        {
            try
            {
                List<Adat_Vezénylés> AdatokVezénylés = KézVezénylés.Lista_Adatok(Telephely, Dátum);
                AdatokVezénylés = (from a in AdatokVezénylés
                                   where a.Törlés == 0
                                   && a.Dátum == Dátum
                                   && a.Vizsgálatraütemez == 1
                                   && a.Törlés == 0
                                   orderby a.Azonosító
                                   select a).ToList();

                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok(Telephely); // Módosítjuk a jármű státuszát
                List<Adat_Jármű_hiba> AdatokHiba = KézJárműHiba.Lista_Adatok(Telephely);  // megnyitjuk a hibákat
                List<Adat_T5C5_Kmadatok> AdatokKM = KézKM.Lista_Adat();

                // ha van ütemezett kocsi
                foreach (Adat_Vezénylés rekordütemez in AdatokVezénylés)
                {
                    // hiba leírása összeállítása
                    string szöveg = "";
                    if (rekordütemez.Vizsgálat.Contains("V1"))
                    {
                        Adat_T5C5_Kmadatok EgyKm = (from a in AdatokKM
                                                    where a.Azonosító == rekordütemez.Azonosító
                                                    && a.Törölt == false
                                                    orderby a.Vizsgdátumk descending
                                                    select a).FirstOrDefault();
                        if (EgyKm != null)
                        {
                            if (EgyKm.KövV.Trim() != "")
                                szöveg += $"{EgyKm.KövV.Trim()}";
                            else
                                szöveg += "_";
                            if (EgyKm.KövV_sorszám != 0)
                                szöveg += $"-{EgyKm.KövV_sorszám}";
                            else
                                szöveg += "-0";
                        }
                    }
                    else
                        szöveg += $"{rekordütemez.Vizsgálat.Trim()} ";

                    szöveg += $"-{rekordütemez.Dátum:yyyy.MM.dd.}";

                    if (rekordütemez.Státus == 4)
                        szöveg += " Maradjon benn ";
                    else
                        szöveg += " Beálló ";

                    // Megnézzük, hogy volt-e már rögzítve ilyen szöveg
                    Adat_Jármű_hiba HibaElem = (from a in AdatokHiba
                                                where a.Azonosító == rekordütemez.Azonosító
                                                && a.Hibaleírása.Contains(szöveg.Trim())
                                                select a).FirstOrDefault();

                    bool KellRögzíteni = false;
                    // ha már volt ilyen szöveg rögzítve a pályaszámhoz akkor nem rögzítjük mégegyszer
                    if (HibaElem == null)
                    {
                        // hibák számát emeljük és státus állítjuk ha kell
                        Adat_Jármű ElemJármű = (from a in AdatokJármű
                                                where a.Azonosító == rekordütemez.Azonosító
                                                select a).FirstOrDefault();
                        if (ElemJármű != null)
                        {
                            KellRögzíteni = true;
                            long hibáksorszáma = ElemJármű.Hibák + 1;
                            long hiba = ElemJármű.Hibák + 1;
                            string típusa = ElemJármű.Típus;
                            long státus = ElemJármű.Státus;
                            long újstátus = 0;

                            if (státus != 4) // ha 4 státusa akkor nem kell módosítani.
                            {
                                // ha a következő napra ütemez
                                if (DateTime.Today.AddDays(1) >= Dátum)
                                {
                                    if (rekordütemez.Státus == 4)
                                        státus = 4;
                                    else
                                        státus = 3;

                                }
                                else if (státus < 4)
                                    státus = 3;
                            }
                            else
                            {
                                újstátus = 1;
                            }

                            // csak akkor módosítjuk a dátumot, ha nem áll
                            if (státus == 4 && újstátus == 0)
                            {
                                Adat_Jármű ADAT = new Adat_Jármű(
                                               rekordütemez.Azonosító.Trim(),
                                               hiba,
                                               státus,
                                               DateTime.Today);
                                KézJármű.Módosítás_Státus_Hiba_Dátum(Telephely, ADAT);
                            }
                            else
                            {
                                Adat_Jármű ADAT = new Adat_Jármű(
                                          rekordütemez.Azonosító.Trim(),
                                          hiba,
                                          státus);
                                KézJármű.Módosítás_Hiba_Státus(Telephely, ADAT);
                            }


                            // beírjuk a hibákat
                            Adat_Jármű_hiba AdatJármű;
                            if (KellRögzíteni == true)
                            {
                                if (DateTime.Today.AddDays(1) >= Dátum)
                                {
                                    AdatJármű = new Adat_Jármű_hiba(
                                                       Program.PostásNév.Trim(),
                                                       rekordütemez.Státus == 4 ? 4 : 3,
                                                       szöveg.Trim(),
                                                       DateTime.Now,
                                                       false,
                                                       típusa.Trim(),
                                                       rekordütemez.Azonosító.Trim(),
                                                       hibáksorszáma);
                                }
                                else
                                {
                                    AdatJármű = new Adat_Jármű_hiba(
                                                        Program.PostásNév.Trim(),
                                                        3,
                                                        szöveg.Trim(),
                                                        DateTime.Now,
                                                        false,
                                                        típusa.Trim(),
                                                        rekordütemez.Azonosító.Trim(),
                                                        hibáksorszáma);
                                }
                                KézJárműHiba.Rögzítés(Telephely, AdatJármű);

                            }
                        }
                    }
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Vezénylés T5C5", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

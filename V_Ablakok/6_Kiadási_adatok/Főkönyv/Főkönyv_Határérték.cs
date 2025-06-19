using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyO = Microsoft.Office.Interop.Outlook;

namespace Villamos.V_Ablakok._6_Kiadási_adatok.Főkönyv
{
    public static class Főkönyv_Határérték
    {
        readonly static Kezelő_Főkönyv_Zser_Km KézZser = new Kezelő_Főkönyv_Zser_Km();
        readonly static Kezelő_Ciklus KézCiklus = new Kezelő_Ciklus();
        readonly static Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly static Kezelő_jármű_hiba KézJárműHiba = new Kezelő_jármű_hiba();


        /// <summary>
        /// T5C5 és T5C5K2, SGP  járművek túllépésének ellenőrzése
        /// A napi zser feltöltését követően fut le ellenőrzi, hogy a km a ciklus felső határértékét meghaladja-e
        /// ha meghaladja akkor a járművet megállítja és e-mailt küld a címzetti körnek
        /// </summary>
        /// <param name="Adatok"></param>
        /// <param name="Telephely"></param>
        /// <returns></returns>
        public static bool T5C5_Túllépés(List<Adat_Főkönyv_Nap> Adatok, string Telephely, string Típus)
        {
            Kezelő_T5C5_Kmadatok KézVkm = new Kezelő_T5C5_Kmadatok(Típus);
            bool válasz = false;
            try
            {
                //Csak az üzemképes kocsikkal foglalkozunk
                Adatok = (from a in Adatok
                          where a.Státus != 4
                          && (a.Típus.Contains(Típus))
                          select a).ToList();

                List<Adat_T5C5_Kmadatok> AdatokVkm = KézVkm.Lista_Adatok();
                AdatokVkm = (from a in AdatokVkm
                             where a.Törölt == false
                             orderby a.Azonosító ascending, a.Vizsgdátumk descending
                             select a).ToList();
                List<Adat_Főkönyv_Zser_Km> AdatokZSER = KézZser.Lista_adatok(DateTime.Today.Year).OrderBy(a => a.Azonosító).ToList();
                List<Adat_Ciklus> AdatokCiklus = KézCiklus.Lista_Adatok(true);
                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok(Telephely);

                List<string> Eredmények = new List<string>();
                foreach (Adat_Főkönyv_Nap Adat in Adatok)
                {
                    Adat_Jármű EgyKocsi = AdatokJármű.FirstOrDefault(a => a.Azonosító == Adat.Azonosító);

                    Adat_T5C5_Kmadatok rekordszer = (from a in AdatokVkm
                                                     where a.Azonosító == Adat.Azonosító
                                                     select a).FirstOrDefault();
                    if (rekordszer != null)
                    {
                        List<Adat_Főkönyv_Zser_Km> KorNapikmLista = (from a in AdatokZSER
                                                                     where a.Azonosító == Adat.Azonosító && a.Dátum > rekordszer.KMUdátum
                                                                     select a).ToList();
                        Adat_Ciklus KisV = (from a in AdatokCiklus
                                            where a.Típus == rekordszer.Ciklusrend
                                            && a.Sorszám == rekordszer.KövV_sorszám
                                            select a).FirstOrDefault();

                        List<Adat_Ciklus> AdatokCiklusNagy = (from a in AdatokCiklus
                                                              where !a.Vizsgálatfok.Contains("V1")
                                                              && a.Típus == rekordszer.Ciklusrend
                                                              select a).ToList();

                        //km korrekció
                        long KorNapikm = 0;
                        if (KorNapikmLista != null)
                            KorNapikm = KorNapikmLista.Sum(a => a.Napikm);
                        //J javítás után át kell számolni a km-t
                        long KMUkm = 0;
                        if (rekordszer.Vizsgsorszám == 0)
                            KMUkm = rekordszer.KMUkm;
                        else
                            KMUkm = rekordszer.KMUkm - rekordszer.Vizsgkm;
                        // hol tartunk kmben?
                        long Vkm = KMUkm + KorNapikm;
                        long V23 = rekordszer.KMUkm + KorNapikm - rekordszer.V2V3Számláló;
                        string szöveg = "";
                        string Eredmény = "";
                        if (rekordszer.Vizsgsorszám == 0 && KisV.Felsőérték * AdatokCiklusNagy[1].Sorszám < V23)
                        {
                            // Ha J javítás volt és nincs visszaállítva még a km akkor a teljes kilométer szerint ellenőrizzük
                            //Nem lehet számolni vele, mert nagyban eltér a KMU km-tól vett különbségektől.
                        }
                        else
                        {
                            //V2 és V3 vizsgálatnál a km-t a ciklus alapján számoljuk
                            if (KisV.Felsőérték * AdatokCiklusNagy[1].Sorszám < V23)
                            {

                                Eredmény = $"A(z) {rekordszer.Azonosító} azonosítójú jármű V2/3 vizsgálat tűrésmezejét a {KisV.Felsőérték * AdatokCiklusNagy[1].Sorszám}" +
                                    $" km-t túllépte, az utolsó vizsgálat óta {V23} km futott, túllépés mértéke:{V23 - (KisV.Felsőérték * AdatokCiklusNagy[1].Sorszám)} km\n";
                                válasz = true;
                                szöveg = $"{rekordszer.KövV2}-{rekordszer.KövV2_sorszám}-{DateTime.Today:yyyy.MM.dd}";
                            }
                            //ebben az esetben V1-re vizsgálunk
                            else if (KisV.Felsőérték < Vkm)
                            {
                                Eredmény = $"A(z) {rekordszer.Azonosító} azonosítójú jármű V1 vizsgálat tűrésmezejét a {KisV.Felsőérték}" +
                                    $" km-t túllépte, az utolsó vizsgálat óta {Vkm} km futott, túllépés mértéke:{Vkm - KisV.Felsőérték} km\n";
                                válasz = true;
                                szöveg = $"{rekordszer.KövV}-{rekordszer.KövV_sorszám}-{DateTime.Today:yyyy.MM.dd}";
                            }

                            if (Eredmény.Trim() != "") Eredmények.Add(Eredmény);
                        }
                        if (szöveg.Trim() != "") Megállítjuk(rekordszer, szöveg, EgyKocsi, Telephely);
                    }
                }
                if (Eredmények.Count > 0) EmailKüldés(Eredmények, Telephely);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "T5C5_Túllépés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return válasz;
        }

        private static void Megállítjuk(Adat_T5C5_Kmadatok rekordszer, string szöveg, Adat_Jármű EgyKocsi, string Telephely)
        {
            try
            {
                //Beírjuk a hibát
                Adat_Jármű_hiba AdatJármű = new Adat_Jármű_hiba(
                                                "Program",
                                                4,
                                                szöveg.Trim(),
                                                DateTime.Now,
                                                false,
                                                EgyKocsi.Valóstípus.Trim(),
                                                rekordszer.Azonosító.Trim(),
                                                EgyKocsi.Hibáksorszáma + 1);
                KézJárműHiba.Rögzítés(Telephely, AdatJármű);

                // beírjuk a főtáblába is
                Adat_Jármű ADAT = new Adat_Jármű(
                                     rekordszer.Azonosító.Trim(),
                                     EgyKocsi.Hibáksorszáma + 1,
                                     4,
                                     DateTime.Now);
                KézJármű.Módosítás_Státus_Hiba_Dátum(Telephely, ADAT);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Megállítjuk", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void EmailKüldés(List<string> Eredmények, string Telephely)
        {
            try
            {
                Kezelő_Kiegészítő_Adatok_Terjesztés kéz = new Kezelő_Kiegészítő_Adatok_Terjesztés();
                List<Adat_Kiegészítő_Adatok_Terjesztés> Adatok = kéz.Lista_Adatok();

                string email = (from a in Adatok
                                where a.Id == 2
                                select a.Email).FirstOrDefault();
                if (email != null)
                {
                    MyO._Application _app = new MyO.Application();
                    MyO.MailItem mail = (MyO.MailItem)_app.CreateItem(MyO.OlItemType.olMailItem);
                    // címzett
                    mail.To = email;
                    // üzenet tárgya
                    mail.Subject = $"{Telephely} telephelyen ciklusrendben meghatározott tűrésmező túllépés történt";
                    // üzent szövege
                    string Html_szöveg = "<html><body>";
                    foreach (string Elem in Eredmények)
                        Html_szöveg += $"{Elem}<br>";

                    Html_szöveg += "</body></html>";
                    mail.HTMLBody = Html_szöveg;
                    mail.Importance = MyO.OlImportance.olImportanceNormal;
                    ((MyO._MailItem)mail).Send();

                    MessageBox.Show("E-mail üzenet el lett küldve", "Üzenet küldés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "EmailKüldés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.V_Ablakok._6_Kiadási_adatok.Főkönyv
{
    public static class Főkönyv_Határérték
    {
        readonly static Kezelő_T5C5_Kmadatok KézVkm = new Kezelő_T5C5_Kmadatok("T5C5");
        readonly static Kezelő_Főkönyv_Zser_Km KézZser = new Kezelő_Főkönyv_Zser_Km();
        readonly static Kezelő_Ciklus KézCiklus = new Kezelő_Ciklus();

        public static void T5C5_Túllépés(List<Adat_Főkönyv_Nap> Adatok)
        {
            try
            {
                List<Adat_T5C5_Kmadatok> AdatokVkm = KézVkm.Lista_Adatok();
                AdatokVkm = (from a in AdatokVkm
                             where a.Törölt == false
                             orderby a.Azonosító ascending, a.Vizsgdátumk descending
                             select a).ToList();
                List<Adat_Főkönyv_Zser_Km> AdatokZSER = KézZser.Lista_adatok(DateTime.Today.Year);
                List<Adat_Ciklus> AdatokCiklus = KézCiklus.Lista_Adatok(true);
                string Eredmény = "";
                foreach (Adat_Főkönyv_Nap Adat in Adatok)
                {
                    Adat_T5C5_Kmadatok rekordszer = (from a in AdatokVkm
                                                     where a.Azonosító == Adat.Azonosító
                                                     select a).FirstOrDefault();
                    List<Adat_Főkönyv_Zser_Km> KorNapikmLista = (from a in AdatokZSER
                                                                 where a.Azonosító == Adat.Azonosító && a.Dátum > rekordszer.KMUdátum
                                                                 select a).ToList();
                    Adat_Ciklus KisV = (from a in AdatokCiklus
                                        where a.Típus == rekordszer.Ciklusrend
                                        && a.Sorszám == rekordszer.KövV_sorszám
                                        select a).FirstOrDefault();

                    //Előző V2/V3-tól meg kellene határozni a km-t
                    Adat_Ciklus NagyV = (from a in AdatokCiklus
                                         where a.Típus == rekordszer.Ciklusrend
                                         && a.Sorszám == rekordszer.KövV_sorszám
                                         select a).FirstOrDefault();
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
                    long V23 = rekordszer.KMUkm - rekordszer.V2V3Számláló + KorNapikm;
                    //Követekező vizsgálat
                    if (rekordszer.KövV == rekordszer.KövV2)
                    {
                        //ebben az esetben V2/3-ra vizsgálunk
                    }
                    else
                    {
                        //ebben az esetben V1-re vizsgálunk
                        if (KisV.Felsőérték < Vkm)
                            Eredmény += $"A(z) {rekordszer.Azonosító} azonosítójú jármű V1 vizsgálat tűrésmezejét a {KisV.Felsőérték} km-t túllépte, az utolsó vizsgálat óta {Vkm} km futott, túllépés mértéke:{Vkm - KisV.Felsőérték} km\n";
                    }
                }
                if (Eredmény.Trim() == "") EmailKüldés(Eredmény);

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
        }

        private static void EmailKüldés(string Eredmény)
        {

            try
            {


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

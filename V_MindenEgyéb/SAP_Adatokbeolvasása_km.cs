using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.V_MindenEgyéb
{
    public static class SAP_Adatokbeolvasása_km
    {
        public static void Km_beolvasó(string fájlexcel, string típus)
        {
            try
            {
                DataTable Tábla = MyF.Excel_Tábla_Beolvas(fájlexcel);
                //Ellenőrzés
                if (!MyF.BetöltésHelyes("KM adatok", Tábla)) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt adatok formátuma ! ");

                // Beolvasni kívánt oszlopok
                Kezelő_Excel_Beolvasás KézBeolvasás = new Kezelő_Excel_Beolvasás();
                List<Adat_Excel_Beolvasás> oszlopnév = KézBeolvasás.Lista_Adatok();

                //Meghatározzuk a beolvasó tábla elnevezéseit 
                //Oszlopnevek beállítása
                string oszlopAzon = (from a in oszlopnév where a.Csoport == "KM adatok" && a.Státusz == false && a.Változónév == "azonosító" select a.Fejléc).FirstOrDefault();
                string oszlopHavi = (from a in oszlopnév where a.Csoport == "KM adatok" && a.Státusz == false && a.Változónév == "havikm" select a.Fejléc).FirstOrDefault();
                string oszlopKMUD = (from a in oszlopnév where a.Csoport == "KM adatok" && a.Státusz == false && a.Változónév == "kmudátum" select a.Fejléc).FirstOrDefault();
                string oszlop_KMU = (from a in oszlopnév where a.Csoport == "KM adatok" && a.Státusz == false && a.Változónév == "kmukm" select a.Fejléc).FirstOrDefault();
                string oszlop__FÚ = (from a in oszlopnév where a.Csoport == "KM adatok" && a.Státusz == false && a.Változónév == "jjavszám" select a.Fejléc).FirstOrDefault();
                string oszlopFúDá = (from a in oszlopnév where a.Csoport == "KM adatok" && a.Státusz == false && a.Változónév == "fúdátum" select a.Fejléc).FirstOrDefault();
                string oszlop__KM = (from a in oszlopnév where a.Csoport == "KM adatok" && a.Státusz == false && a.Változónév == "teljeskm" select a.Fejléc).FirstOrDefault();

                if (oszlopAzon == null || oszlopHavi == null || oszlopKMUD == null || oszlop_KMU == null || oszlop__FÚ == null || oszlopFúDá == null || oszlop__KM == null)
                    throw new HibásBevittAdat("Nincs helyesen beállítva a beolvasótábla! ");

                Kezelő_T5C5_Kmadatok Kéz = new Kezelő_T5C5_Kmadatok(típus);
                List<Adat_T5C5_Kmadatok> Adatok = Kéz.Lista_Adatok();



                // Első adattól végig pörgetjük a beolvasást addig amíg nem lesz üres
                List<Adat_T5C5_Kmadatok> AdatokGy = new List<Adat_T5C5_Kmadatok>();
                int sor = 2;
                foreach (DataRow Sor in Tábla.Rows)
                {
                    string azonosító = Sor[oszlopAzon].ToStrTrim();
                    long havikm = Sor[oszlopHavi].ToÉrt_Long();
                    DateTime kmudátum = Sor[oszlopKMUD].ToÉrt_DaTeTime();
                    long kmukm = Sor[oszlop_KMU].ToÉrt_Long();
                    long teljeskm = Sor[oszlop__KM].ToÉrt_Long();
                    long jjavszám = Sor[oszlop__FÚ].ToÉrt_Long();
                    DateTime fúdátum = Sor[oszlopFúDá].ToÉrt_DaTeTime();

                    azonosító = MyF.Szöveg_Tisztítás(azonosító, 1, 4);

                    if (azonosító.Trim() == "") break;
                    Adat_T5C5_Kmadatok Elem = (from a in Adatok
                                               where a.Azonosító == azonosító.Trim()
                                               && a.Törölt == false
                                               orderby a.Vizsgdátumk descending
                                               select a).FirstOrDefault();

                    if (Elem != null)
                    {
                        Adat_T5C5_Kmadatok ADAT = new Adat_T5C5_Kmadatok(
                                 Elem.ID,
                                 jjavszám,
                                 kmukm,
                                 kmudátum,
                                 havikm,
                                 fúdátum,
                                 teljeskm);
                        AdatokGy.Add(ADAT);
                    }
                    sor++;
                }
                Kéz.MódosításKm(AdatokGy);
                // kitöröljük a betöltött fájlt
                File.Delete(fájlexcel);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Km_beolvasó", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

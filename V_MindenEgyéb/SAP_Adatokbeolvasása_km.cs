using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
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



        /// <summary>
        /// Fogaskerekűre alakítva
        /// </summary>
        /// <param name="fájlexcel"></param>
        /// <param name="hely"></param>
        /// 
        public static void Km_beolvasóFogas(string fájlexcel)
        {
            try
            {
                DataTable Tábla = MyF.Excel_Tábla_Beolvas(fájlexcel);
                //Ellenőrzés
                if (!MyF.Betöltéshelyes("KM adatok", Tábla)) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt adatok formátuma ! ");

                MyE.ExcelMegnyitás(fájlexcel);
                string beopályaszám;

                Kezelő_T5C5_Kmadatok Kéz = new Kezelő_T5C5_Kmadatok("SGP");
                List<Adat_T5C5_Kmadatok> Adatok = Kéz.Lista_Adatok();

                // Első adattól végig pörgetjük a beolvasást addig amíg nem lesz üres
                List<Adat_T5C5_Kmadatok> AdatokGy = new List<Adat_T5C5_Kmadatok>();
                int sor = 2;
                while (MyE.Beolvas($"a{sor}") != "_")
                {
                    string beolvasott = MyE.Beolvas($"a{sor}");
                    beopályaszám = MyF.Szöveg_Tisztítás(beolvasott, 1, 4);

                    if (beopályaszám.Trim() == "") break;
                    Adat_T5C5_Kmadatok Elem = (from a in Adatok
                                               where a.Azonosító == beopályaszám.Trim()
                                               && a.Törölt == false
                                               orderby a.Vizsgdátumk descending
                                               select a).FirstOrDefault();

                    if (Elem != null)
                    {
                        Adat_T5C5_Kmadatok ADAT = new Adat_T5C5_Kmadatok(
                            Elem.ID,
                            MyE.Beolvas($"f{sor}").ToÉrt_Long(),
                            MyE.Beolvas($"d{sor}").ToÉrt_Long(),
                            MyE.BeolvasDátum($"c{sor}"),
                            MyE.Beolvas($"b{sor}") == "_" ? 0 : MyE.Beolvas($"b{sor}").ToÉrt_Long(),
                            MyE.BeolvasDátum($"g{sor}"),
                            MyE.Beolvas($"e{sor}").ToÉrt_Long());
                        AdatokGy.Add(ADAT);
                    }
                    sor++;
                }
                Kéz.MódosításKm(AdatokGy);
                // az excel tábla bezárása
                MyE.ExcelBezárás();
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



        public static void Km_beolvasóT5C5(string fájlexcel)
        {
            try
            {
                DateTime Eleje = DateTime.Now;
                DataTable Tábla = MyF.Excel_Tábla_Beolvas(fájlexcel);
                //Ellenőrzés
                if (!MyF.Betöltéshelyes("KM adatok", Tábla)) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt adatok formátuma ! ");

                string hely = Application.StartupPath + @"\Főmérnökség\Adatok\T5C5\Villamos4T5C5.mdb";
                string jelszó = "pocsaierzsi";
                // Első adattól végig pörgetjük a beolvasást addig amíg nem lesz üres

                string szöveg = "SELECT KMtábla.azonosító, KMtábla.vizsgdátumk, KMtábla.ID ";
                szöveg += " FROM  (SELECT KMtábla.azonosító, Max(KMtábla.vizsgdátumk) AS MaxOfvizsgdátumk FROM KMtábla WHERE törölt=False GROUP BY KMtábla.azonosító ORDER BY azonosító) AS Rész ";
                szöveg += " INNER JOIN KMtábla ON (Rész.MaxOfvizsgdátumk = KMtábla.vizsgdátumk) AND (Rész.azonosító = KMtábla.azonosító) ";
                szöveg += " WHERE törölt=False ORDER BY KMtábla.azonosító";

                Kezelő_T5C5_Kmadatok KézT5 = new Kezelő_T5C5_Kmadatok("T5C5");
                List<Adat_T5C5_Kmadatok> AdatokT5 = KézT5.Lista_Szűrt_Adat(hely, jelszó, szöveg);
                List<string> SzövegGy = new List<string>();

                foreach (Adat_BEOLVAS_KM rekord in Excel_Km_Beolvas(Tábla))
                {
                    long utolsórögzítés = (from a in AdatokT5
                                           where a.Azonosító.Trim() == rekord.Azonosító
                                           select a.ID).FirstOrDefault();
                    if (utolsórögzítés != 0)
                    {
                        szöveg = "UPDATE kmtábla SET ";
                        szöveg += $" KMUdátum='{rekord.KMUdátum:yyyy.MM.dd}', ";
                        szöveg += $" KMUkm={rekord.KMUkm}, ";
                        szöveg += $" havikm={rekord.Havikm}, ";
                        szöveg += $" Jjavszám={rekord.Jjavszám}, ";
                        szöveg += $" fudátum='{rekord.Fudátum:yyyy.MM.dd}', ";
                        szöveg += $" teljeskm={rekord.Teljeskm} ";
                        szöveg += $" WHERE [id]={utolsórögzítés}";
                        SzövegGy.Add(szöveg);
                    }
                }
                if (SzövegGy.Count > 0) MyA.ABMódosítás(hely, jelszó, SzövegGy);

                DateTime Vége = DateTime.Now;
                MessageBox.Show($"Az adatok beolvasása {Vége - Eleje} idő alatt megtörtént.", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);

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


        public static void Km_beolvasóICS(string fájlexcel)
        {
            try
            {
                DateTime Eleje = DateTime.Now;
                DataTable Tábla = MyF.Excel_Tábla_Beolvas(fájlexcel);
                //Ellenőrzés
                if (!MyF.Betöltéshelyes("KM adatok", Tábla)) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt adatok formátuma ! ");

                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\ICSKCSV\Villamos4ICS.mdb";
                string jelszó = "pocsaierzsi";
                // Első adattól végig pörgetjük a beolvasást addig amíg nem lesz üres

                string szöveg = "SELECT KMtábla.azonosító, KMtábla.vizsgdátumk, KMtábla.ID ";
                szöveg += " FROM  (SELECT KMtábla.azonosító, Max(KMtábla.vizsgdátumk) AS MaxOfvizsgdátumk FROM KMtábla WHERE törölt=False GROUP BY KMtábla.azonosító ORDER BY azonosító) AS Rész ";
                szöveg += " INNER JOIN KMtábla ON (Rész.MaxOfvizsgdátumk = KMtábla.vizsgdátumk) AND (Rész.azonosító = KMtábla.azonosító) ";
                szöveg += " WHERE törölt=False ORDER BY KMtábla.azonosító";

                Kezelő_T5C5_Kmadatok KézT5 = new Kezelő_T5C5_Kmadatok("T5C5");
                List<Adat_T5C5_Kmadatok> AdatokT5 = KézT5.Lista_Szűrt_Adat(hely, jelszó, szöveg);
                List<string> SzövegGy = new List<string>();

                foreach (Adat_BEOLVAS_KM rekord in Excel_Km_Beolvas(Tábla))
                {
                    long utolsórögzítés = (from a in AdatokT5
                                           where a.Azonosító.Trim() == rekord.Azonosító
                                           select a.ID).FirstOrDefault();
                    if (utolsórögzítés != 0)
                    {
                        szöveg = "UPDATE kmtábla SET ";
                        szöveg += $" KMUdátum='{rekord.KMUdátum:yyyy.MM.dd}', ";
                        szöveg += $" KMUkm={rekord.KMUkm}, ";
                        szöveg += $" havikm={rekord.Havikm}, ";
                        szöveg += $" Jjavszám={rekord.Jjavszám}, ";
                        szöveg += $" fudátum='{rekord.Fudátum:yyyy.MM.dd}', ";
                        szöveg += $" teljeskm={rekord.Teljeskm} ";
                        szöveg += $" WHERE [id]={utolsórögzítés}";
                        SzövegGy.Add(szöveg);
                    }
                }
                if (SzövegGy.Count > 0) MyA.ABMódosítás(hely, jelszó, SzövegGy);

                DateTime Vége = DateTime.Now;
                MessageBox.Show($"Az adatok beolvasása {Vége - Eleje} idő alatt megtörtént.", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);

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



        public static List<Adat_BEOLVAS_KM> Excel_Km_Beolvas(DataTable EgyTábla)
        {
            List<Adat_BEOLVAS_KM> Adatok = new List<Adat_BEOLVAS_KM>();
            if (EgyTábla != null)
            {
                for (int i = 0; i < EgyTábla.Rows.Count; i++)
                {
                    Adat_BEOLVAS_KM Adat = new Adat_BEOLVAS_KM(
                                    MyF.Szöveg_Tisztítás(EgyTábla.Rows[i]["Berendezés"].ToStrTrim(), 1, 4),
                                    EgyTábla.Rows[i]["Megtett KM (eltérés)"].ToÉrt_Long(),
                                    EgyTábla.Rows[i]["Intervallum KM dátum"].ToÉrt_DaTeTime(),
                                    EgyTábla.Rows[i]["KMU"].ToÉrt_Long(),
                                    EgyTábla.Rows[i]["KM"].ToÉrt_Long(),
                                    EgyTábla.Rows[i]["FÚ"].ToÉrt_Long(),
                                    EgyTábla.Rows[i]["FÚ dátuma"].ToÉrt_DaTeTime()
                                    );
                    Adatok.Add(Adat);
                }
            }
            return Adatok;
        }
    }
}

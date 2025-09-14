using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.V_Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.V_MindenEgyéb
{
    public static class SAP_Adatokbeolvasása
    {
        public static void Km_beolvasó(string fájlexcel, string típus)
        {
            try
            {
                DataTable Tábla = MyF.Excel_Tábla_Beolvas(fájlexcel);
                //Ellenőrzés
                if (!MyF.Betöltéshelyes("KM adatok", Tábla)) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt adatok formátuma ! ");

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

        public static void Kerék_beolvasó(string fájlexcel)
        {
            try
            {
                DataTable Tábla = MyF.Excel_Tábla_Beolvas(fájlexcel);
                //Ellenőrzés
                if (!MyF.Betöltéshelyes("Kerék", Tábla)) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt adatok formátuma ! ");

                // Beolvasni kívánt oszlopok
                Kezelő_Excel_Beolvasás KézBeolvasás = new Kezelő_Excel_Beolvasás();
                List<Adat_Excel_Beolvasás> oszlopnév = KézBeolvasás.Lista_Adatok();

                //Meghatározzuk a beolvasó tábla elnevezéseit 
                //Oszlopnevek beállítása
                string oszlopBerendezés = (from a in oszlopnév where a.Csoport == "Kerék" && a.Státusz == false && a.Változónév == "kerékberendezés" select a.Fejléc).FirstOrDefault();
                string oszlopMegnevezés = (from a in oszlopnév where a.Csoport == "Kerék" && a.Státusz == false && a.Változónév == "kerékmegnevezés" select a.Fejléc).FirstOrDefault();
                string oszlopGyártási = (from a in oszlopnév where a.Csoport == "Kerék" && a.Státusz == false && a.Változónév == "kerékgyártásiszám" select a.Fejléc).FirstOrDefault();
                string oszlopFölé = (from a in oszlopnév where a.Csoport == "Kerék" && a.Státusz == false && a.Változónév == "föléberendezés" select a.Fejléc).FirstOrDefault();
                string oszlopTétel = (from a in oszlopnév where a.Csoport == "Kerék" && a.Státusz == false && a.Változónév == "pozíció" select a.Fejléc).FirstOrDefault();
                string oszlopMódosít = (from a in oszlopnév where a.Csoport == "Kerék" && a.Státusz == false && a.Változónév == "Dátum" select a.Fejléc).FirstOrDefault();
                string oszlopFajta = (from a in oszlopnév where a.Csoport == "Kerék" && a.Státusz == false && a.Változónév == "objektumfajta" select a.Fejléc).FirstOrDefault();

                if (oszlopBerendezés == null || oszlopMegnevezés == null || oszlopGyártási == null || oszlopFölé == null || oszlopTétel == null || oszlopMódosít == null || oszlopFajta == null)
                    throw new HibásBevittAdat("Nincs helyesen beállítva a beolvasótábla! ");


                // Első adattól végig pörgetjük a beolvasást addig amíg nem lesz üres
                List<Adat_Kerék_Tábla> AdatokGy = new List<Adat_Kerék_Tábla>();
                int sor = 2;
                foreach (DataRow Sor in Tábla.Rows)
                {
                    string kerékberendezés = MyF.Szöveg_Tisztítás(Sor[oszlopBerendezés].ToStrTrim(), 0, 10);
                    string kerékmegnevezés = MyF.Szöveg_Tisztítás(Sor[oszlopMegnevezés].ToStrTrim(), 0, 255);
                    string kerékgyártásiszám = MyF.Szöveg_Tisztítás(Sor[oszlopGyártási].ToStrTrim(), 0, 30);
                    string föléberendezés = MyF.Szöveg_Tisztítás(Sor[oszlopFölé].ToStrTrim(), 0, 10).Replace(",", "");
                    string Azonosító = föléberendezés.Replace(",", "").Replace("V", "").Replace("F", "");
                    string objektumfajta = MyF.Szöveg_Tisztítás(Sor[oszlopFajta].ToStrTrim(), 0, 20);
                    string pozíció = MyF.Szöveg_Tisztítás(Sor[oszlopTétel].ToStrTrim(), 0, 10);
                    DateTime Dátum = Sor[oszlopMódosít].ToÉrt_DaTeTime();

                    Adat_Kerék_Tábla ADAT = new Adat_Kerék_Tábla(
                               kerékberendezés,
                               kerékmegnevezés,
                               kerékgyártásiszám,
                               föléberendezés,
                               Azonosító,
                               pozíció,
                               Dátum,
                               objektumfajta);
                    AdatokGy.Add(ADAT);

                    sor++;
                }
                Kezelő_Kerék_Tábla KézKerék = new Kezelő_Kerék_Tábla();
                if (AdatokGy.Count > 0) KézKerék.Osztályoz(AdatokGy);
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
        /// Menetkimaradáshoz szükséges adatok beolvasása az SAP Excel fájlból.
        /// </summary>
        /// <param name="Telephely"></param>
        /// <param name="Év"></param>
        /// <param name="fájlexcel"></param>
        /// <param name="felelősmunkahely"></param>
        /// <param name="üzem">alapértelmezés szerint üzemek és false esetén főmérnökség</param>
        public static void Menet_beolvasó(string Telephely, int Év, string fájlexcel, string felelősmunkahely, bool üzem = true)
        {
            try
            {
                Kezelő_Jármű KézJármű = new Kezelő_Jármű();
                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");
                DataTable Tábla = MyF.Excel_Tábla_Beolvas(fájlexcel);
                //Ellenőrzés
                if (!MyF.Betöltéshelyes("Menet", Tábla)) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt adatok formátuma ! ");

                // Beolvasni kívánt oszlopok
                Kezelő_Excel_Beolvasás KézBeolvasás = new Kezelő_Excel_Beolvasás();
                List<Adat_Excel_Beolvasás> oszlopnév = KézBeolvasás.Lista_Adatok();

                //Szolgálat adatok
                Kezelő_Kiegészítő_Szolgálattelepei KézSzolgTelep = new Kezelő_Kiegészítő_Szolgálattelepei();
                List<Adat_Kiegészítő_Szolgálattelepei> AdatokSzolg = KézSzolgTelep.Lista_Adatok();

                //Meghatározzuk a beolvasó tábla elnevezéseit 
                //Oszlopnevek beállítása
                string oszlopAzon = (from a in oszlopnév where a.Csoport == "Menet" && a.Státusz == false && a.Változónév == "azonosító" select a.Fejléc).FirstOrDefault();
                string oszlopVisz = (from a in oszlopnév where a.Csoport == "Menet" && a.Státusz == false && a.Változónév == "viszonylat" select a.Fejléc).FirstOrDefault();
                string oszlopJel = (from a in oszlopnév where a.Csoport == "Menet" && a.Státusz == false && a.Változónév == "Eseményjele" select a.Fejléc).FirstOrDefault();
                string oszlopIdő = (from a in oszlopnév where a.Csoport == "Menet" && a.Státusz == false && a.Változónév == "didő" select a.Fejléc).FirstOrDefault();
                string oszlopDátum = (from a in oszlopnév where a.Csoport == "Menet" && a.Státusz == false && a.Változónév == "ddátum" select a.Fejléc).FirstOrDefault();
                string oszlopMenet = (from a in oszlopnév where a.Csoport == "Menet" && a.Státusz == false && a.Változónév == "kimaradtmenet" select a.Fejléc).FirstOrDefault();
                string oszlopBeír = (from a in oszlopnév where a.Csoport == "Menet" && a.Státusz == false && a.Változónév == "jvbeírás" select a.Fejléc).FirstOrDefault();
                string oszlopjav = (from a in oszlopnév where a.Csoport == "Menet" && a.Státusz == false && a.Változónév == "javítás" select a.Fejléc).FirstOrDefault();
                string oszlopjelentés = (from a in oszlopnév where a.Csoport == "Menet" && a.Státusz == false && a.Változónév == "Jelentés" select a.Fejléc).FirstOrDefault();
                string oszloptétel = (from a in oszlopnév where a.Csoport == "Menet" && a.Státusz == false && a.Változónév == "tétel" select a.Fejléc).FirstOrDefault();
                string oszlopMunkahely = (from a in oszlopnév where a.Csoport == "Menet" && a.Státusz == false && a.Változónév == "munkahely" select a.Fejléc).FirstOrDefault();


                if (oszlopAzon == null) throw new HibásBevittAdat("Nincs helyesen beállítva az azonosító beolvasótábla! ");
                if (oszlopVisz == null) throw new HibásBevittAdat("Nincs helyesen beállítva a viszonylat beolvasótábla! ");
                if (oszlopJel == null) throw new HibásBevittAdat("Nincs helyesen beállítva az Eseményjele beolvasótábla! ");
                if (oszlopIdő == null) throw new HibásBevittAdat("Nincs helyesen beállítva a didő beolvasótábla! ");
                if (oszlopDátum == null) throw new HibásBevittAdat("Nincs helyesen beállítva a ddátum beolvasótábla! ");
                if (oszlopMenet == null) throw new HibásBevittAdat("Nincs helyesen beállítva a  kimaradtmenet beolvasótábla! ");
                if (oszlopBeír == null) throw new HibásBevittAdat("Nincs helyesen beállítva a jvbeírás beolvasótábla! ");
                if (oszlopjav == null) throw new HibásBevittAdat("Nincs helyesen beállítva a javítás beolvasótábla! ");
                if (oszlopjelentés == null) throw new HibásBevittAdat("Nincs helyesen beállítva a Jelentés beolvasótábla! ");
                if (oszloptétel == null) throw new HibásBevittAdat("Nincs helyesen beállítva a tétel beolvasótábla! ");
                if (oszlopMunkahely == null) throw new HibásBevittAdat("Nincs helyesen beállítva a munkahely beolvasótábla! ");

                Kezelő_Menetkimaradás Kéz = new Kezelő_Menetkimaradás();
                Kezelő_MenetKimaradás_Főmérnökség KézFőmérnök = new Kezelő_MenetKimaradás_Főmérnökség();
                List<Adat_Menetkimaradás> AdatokGy = new List<Adat_Menetkimaradás>();
                List<Adat_Menetkimaradás_Főmérnökség> AdatokFőGy = new List<Adat_Menetkimaradás_Főmérnökség>();
                foreach (DataRow Sor in Tábla.Rows)
                {
                    string azonosító = MyF.Szöveg_Tisztítás(Sor[oszlopAzon].ToStrTrim(), 1, 4);
                    string viszonylat = MyF.Szöveg_Tisztítás(Sor[oszlopVisz].ToStrTrim(), 0, 6);
                    string Típus = Milyen_típus(AdatokJármű, azonosító);
                    string Eseményjele = MyF.Szöveg_Tisztítás(Sor[oszlopJel].ToStrTrim(), 0, 1);
                    DateTime didő = Sor[oszlopIdő].ToÉrt_DaTeTime();
                    DateTime ddátum = Sor[oszlopDátum].ToÉrt_DaTeTime();
                    DateTime bekövetkezés = new DateTime(ddátum.Year, ddátum.Month, ddátum.Day, didő.Hour, didő.Minute, didő.Second);
                    int kimaradtmenet = Sor[oszlopMenet].ToÉrt_Int();
                    string jvbeírás = MyF.Szöveg_Tisztítás(Sor[oszlopBeír].ToStrTrim(), 0, 150);
                    string vmbeírás = "*";
                    string javítás = MyF.Szöveg_Tisztítás(Sor[oszlopjav].ToStrTrim(), 0, 150);
                    long Id = 0;
                    bool törölt = false;
                    string Jelentés = MyF.Szöveg_Tisztítás(Sor[oszlopjelentés].ToStrTrim(), 0, 20);
                    int tétel = Sor[oszloptétel].ToÉrt_Int();
                    string munkahely = MyF.Szöveg_Tisztítás(Sor[oszlopMunkahely].ToStrTrim(), 0, 20);
                    if (üzem)
                    {
                        if (felelősmunkahely.Trim().ToUpper() == munkahely.ToStrTrim().ToUpper())
                        {

                            Adat_Menetkimaradás Adat = new Adat_Menetkimaradás(
                                              viszonylat,
                                              azonosító,
                                              Típus,
                                              Eseményjele,
                                              bekövetkezés,
                                              kimaradtmenet,
                                              jvbeírás,
                                              vmbeírás,
                                              javítás,
                                              Id,
                                              törölt,
                                              Jelentés,
                                              tétel);
                            AdatokGy.Add(Adat);

                        }
                    }
                    else
                    {
                        string telephely = "_";
                        string szolgálat = "_";
                        Adat_Kiegészítő_Szolgálattelepei Lekérdezés = (from a in AdatokSzolg
                                                                       where a.Felelősmunkahely.Trim() == munkahely.Trim()
                                                                       select a).FirstOrDefault();
                        if (Lekérdezés != null)
                        {
                            telephely = Lekérdezés.Telephelynév;
                            szolgálat = Lekérdezés.Szolgálatnév;
                        }
                        Adat_Menetkimaradás_Főmérnökség AdatFő = new Adat_Menetkimaradás_Főmérnökség(
                                        viszonylat,
                                        azonosító,
                                        Típus,
                                        Eseményjele,
                                        bekövetkezés,
                                        kimaradtmenet,
                                        jvbeírás,
                                        vmbeírás,
                                        javítás,
                                        Id,
                                        törölt,
                                        Jelentés,
                                        tétel,
                                        telephely,
                                        szolgálat);
                        AdatokFőGy.Add(AdatFő);
                    }
                }

                if (AdatokGy.Count > 0) Kéz.Döntés(Telephely, Év, AdatokGy);
                if (AdatokFőGy.Count > 0) KézFőmérnök.Döntés(Év, AdatokFőGy);
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

        private static string Milyen_típus(List<Adat_Jármű> AdatokJármű, string azonosító)
        {
            string típus = "?";
            Adat_Jármű Elem = (from a in AdatokJármű
                               where a.Azonosító == azonosító.Trim()
                               select a).FirstOrDefault();
            if (Elem != null) típus = Elem.Valóstípus;
            return típus;
        }

        public static void Eszköz_Beolvasó(string fájlexcel, string Telephely)
        {
            try
            {
                DataTable Tábla = MyF.Excel_Tábla_Beolvas(fájlexcel);
                //Ellenőrzés
                if (!MyF.Betöltéshelyes("Eszköz", Tábla)) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt adatok formátuma ! ");

                // Beolvasni kívánt oszlopok
                Kezelő_Excel_Beolvasás KézBeolvasás = new Kezelő_Excel_Beolvasás();
                List<Adat_Excel_Beolvasás> oszlopnév = KézBeolvasás.Lista_Adatok();

                //Meghatározzuk a beolvasó tábla elnevezéseit 
                //Oszlopnevek beállítása
                string OszlopFelKtg = (from a in oszlopnév where a.Csoport == "Eszköz" && a.Státusz == false && a.Változónév == "Felelős_költséghely" select a.Fejléc).FirstOrDefault();
                string OszlopEszköz = (from a in oszlopnév where a.Csoport == "Eszköz" && a.Státusz == false && a.Változónév == "Eszköz" select a.Fejléc).FirstOrDefault();
                string OszlopAlszám = (from a in oszlopnév where a.Csoport == "Eszköz" && a.Státusz == false && a.Változónév == "Alszám" select a.Fejléc).FirstOrDefault();
                string Oszloplelszám = (from a in oszlopnév where a.Csoport == "Eszköz" && a.Státusz == false && a.Változónév == "Leltárszám" select a.Fejléc).FirstOrDefault();
                string OszlopMegn = (from a in oszlopnév where a.Csoport == "Eszköz" && a.Státusz == false && a.Változónév == "Megnevezés" select a.Fejléc).FirstOrDefault();
                string OszlopMFolyt = (from a in oszlopnév where a.Csoport == "Eszköz" && a.Státusz == false && a.Változónév == "Megnevezés_folyt" select a.Fejléc).FirstOrDefault();
                string OszlopGyártási = (from a in oszlopnév where a.Csoport == "Eszköz" && a.Státusz == false && a.Változónév == "Gyártási_szám" select a.Fejléc).FirstOrDefault();
                string OszlopAktDát = (from a in oszlopnév where a.Csoport == "Eszköz" && a.Státusz == false && a.Változónév == "Aktiválás_dátuma" select a.Fejléc).FirstOrDefault();
                string OszlopMenny = (from a in oszlopnév where a.Csoport == "Eszköz" && a.Státusz == false && a.Változónév == "Mennyiség" select a.Fejléc).FirstOrDefault();
                string OszlopBázisE = (from a in oszlopnév where a.Csoport == "Eszköz" && a.Státusz == false && a.Változónév == "Bázis_menny_egység" select a.Fejléc).FirstOrDefault();
                string OszlopDeaktDát = (from a in oszlopnév where a.Csoport == "Eszköz" && a.Státusz == false && a.Változónév == "Deaktiválás_dátuma" select a.Fejléc).FirstOrDefault();
                string OszlopTelep = (from a in oszlopnév where a.Csoport == "Eszköz" && a.Státusz == false && a.Változónév == "Telephely" select a.Fejléc).FirstOrDefault();
                string OszlopTMegnev = (from a in oszlopnév where a.Csoport == "Eszköz" && a.Státusz == false && a.Változónév == "Telephely_megnevezése" select a.Fejléc).FirstOrDefault();
                string OszlopHely = (from a in oszlopnév where a.Csoport == "Eszköz" && a.Státusz == false && a.Változónév == "Helyiség" select a.Fejléc).FirstOrDefault();
                string OszlopHMegnev = (from a in oszlopnév where a.Csoport == "Eszköz" && a.Státusz == false && a.Változónév == "Helyiség_megnevezés" select a.Fejléc).FirstOrDefault();
                string OszlopHR = (from a in oszlopnév where a.Csoport == "Eszköz" && a.Státusz == false && a.Változónév == "Szemügyi_törzsszám" select a.Fejléc).FirstOrDefault();
                string OszlopDolgNév = (from a in oszlopnév where a.Csoport == "Eszköz" && a.Státusz == false && a.Változónév == "Dolgozó_neve" select a.Fejléc).FirstOrDefault();
                string OszlopLelDát = (from a in oszlopnév where a.Csoport == "Eszköz" && a.Státusz == false && a.Változónév == "Leltár_dátuma" select a.Fejléc).FirstOrDefault();

                if (OszlopFelKtg == null ||
                    OszlopEszköz == null ||
                    OszlopAlszám == null ||
                    Oszloplelszám == null ||
                    OszlopMegn == null ||
                    OszlopMFolyt == null ||
                    OszlopGyártási == null ||
                    OszlopAktDát == null ||
                    OszlopMenny == null ||
                    OszlopBázisE == null ||
                    OszlopDeaktDát == null ||
                    OszlopTelep == null ||
                    OszlopTMegnev == null ||
                    OszlopHely == null ||
                    OszlopHMegnev == null ||
                    OszlopHR == null ||
                    OszlopDolgNév == null ||
                    OszlopLelDát == null) throw new HibásBevittAdat("Nincs helyesen beállítva a beolvasótábla! ");

                Kezelő_Eszköz Kéz = new Kezelő_Eszköz();
                List<Adat_Eszköz> Adatok = Kéz.Lista_Adatok(Telephely);
                // Első adattól végig pörgetjük a beolvasást addig amíg nem lesz üres
                List<Adat_Eszköz> AdatokGyR = new List<Adat_Eszköz>();
                List<Adat_Eszköz> AdatokGyM = new List<Adat_Eszköz>();
                int sor = 2;
                foreach (DataRow Sor in Tábla.Rows)
                {
                    string Eszköz = MyF.Szöveg_Tisztítás(Sor[OszlopEszköz].ToStrTrim());
                    if (Eszköz.Trim() != "")
                    {
                        string Alszám = MyF.Szöveg_Tisztítás(Sor[OszlopAlszám].ToStrTrim());
                        string Megnevezés = MyF.Szöveg_Tisztítás(Sor[OszlopMegn].ToStrTrim());
                        string Megnevezés_folyt = MyF.Szöveg_Tisztítás(Sor[OszlopMFolyt].ToStrTrim());
                        string Gyártási_szám = MyF.Szöveg_Tisztítás(Sor[OszlopGyártási].ToStrTrim());
                        string Leltárszám = MyF.Szöveg_Tisztítás(Sor[Oszloplelszám].ToStrTrim());
                        string Bázis_menny_egység = MyF.Szöveg_Tisztítás(Sor[OszlopBázisE].ToStrTrim());
                        string Szemügyi_törzsszám = MyF.Szöveg_Tisztítás(Sor[OszlopHR].ToStrTrim());
                        string Dolgozó_neve = MyF.Szöveg_Tisztítás(Sor[OszlopDolgNév].ToStrTrim());
                        string Felelős_költséghely = MyF.Szöveg_Tisztítás(Sor[OszlopFelKtg].ToStrTrim());
                        string telephely = MyF.Szöveg_Tisztítás(Sor[OszlopTelep].ToStrTrim());
                        string Telephely_megnevezése = MyF.Szöveg_Tisztítás(Sor[OszlopTMegnev].ToStrTrim());
                        string Helyiség = MyF.Szöveg_Tisztítás(Sor[OszlopHely].ToStrTrim());
                        string Helyiség_megnevezés = MyF.Szöveg_Tisztítás(Sor[OszlopHMegnev].ToStrTrim());

                        string Eszközosztály = "";
                        string Üzletág = "";
                        string Cím = "";
                        string Költséghely = "";
                        string Régi_leltárszám = "";
                        string Gyár = "";
                        string Leltári_költséghely = "";
                        string Vonalkód = "";
                        string Rendszám_pályaszám = "";

                        DateTime Leltár_dátuma = Sor[OszlopLelDát].ToÉrt_DaTeTime();
                        DateTime Aktiválás_dátuma = Sor[OszlopAktDát].ToÉrt_DaTeTime();
                        DateTime Deaktiválás_dátuma = Sor[OszlopDeaktDát].ToÉrt_DaTeTime();

                        DateTime Leltár_forduló_nap = new DateTime(1900, 1, 1);

                        double Mennyiség = Sor[OszlopMenny].ToÉrt_Double();

                        bool Vonalkódozható = false;

                        string Épület_Szerszám = "Nincs beállítva";
                        bool Épület_van = false;
                        bool Szerszám_van = false;
                        bool Státus = false;

                        Adat_Eszköz Elem = (from a in Adatok
                                            where a.Eszköz == Eszköz.Trim()
                                            select a).FirstOrDefault();

                        Adat_Eszköz ADAT = new Adat_Eszköz(
                             Eszköz,
                             Alszám,
                             Megnevezés,
                             Megnevezés_folyt,
                             Gyártási_szám,
                             Leltárszám,
                             Leltár_dátuma,
                             Mennyiség,
                             Bázis_menny_egység,
                             Aktiválás_dátuma,
                             telephely,
                             Telephely_megnevezése,
                             Helyiség,
                             Helyiség_megnevezés,
                             Gyár,
                             Leltári_költséghely,
                             Vonalkód,
                             Leltár_forduló_nap,
                             Szemügyi_törzsszám,
                             Dolgozó_neve,
                             Deaktiválás_dátuma,
                             Eszközosztály,
                             Üzletág,
                             Cím,
                             Költséghely,
                             Felelős_költséghely,
                             Régi_leltárszám,
                             Vonalkódozható,
                             Rendszám_pályaszám,
                             Épület_Szerszám,
                             Épület_van,
                             Szerszám_van,
                             Státus);

                        if (Elem != null)
                            AdatokGyM.Add(ADAT);
                        else
                            AdatokGyR.Add(ADAT);
                    }
                    sor++;
                }
                if (AdatokGyM.Count > 0) Kéz.Módosítás(Telephely, AdatokGyM);
                if (AdatokGyR.Count > 0) Kéz.Rögzítés(Telephely, AdatokGyR);

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

        public static void ZSER_Betöltés(string Telephely, DateTime Dátum, string Napszak, DataTable Tábla, long kiadási_korr = 0, long érkezési_korr = 0)
        {
            try
            {
                // Beolvasni kívánt oszlopok
                Kezelő_Excel_Beolvasás KézBeolvasás = new Kezelő_Excel_Beolvasás();
                List<Adat_Excel_Beolvasás> oszlopnév = KézBeolvasás.Lista_Adatok();

                //Meghatározzuk a beolvasó tábla elnevezéseit 
                //Oszlopnevek beállítása
                string OszlopVisz = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "viszonylat" select a.Fejléc).FirstOrDefault();
                string OszlopForg = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "forgalmiszám" select a.Fejléc).FirstOrDefault();
                string OszlopIndDát = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "IndDát" select a.Fejléc).FirstOrDefault();
                string OszlopIndIdő = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "IndÓra" select a.Fejléc).FirstOrDefault();
                string OszlopTényDát = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "TényDát" select a.Fejléc).FirstOrDefault();
                string OszlopTényIdő = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "TényÓra" select a.Fejléc).FirstOrDefault();
                string OszlopÉrkDát = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "ÉrkDát" select a.Fejléc).FirstOrDefault();
                string OszlopÉrkÓra = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "ÉrkÓra" select a.Fejléc).FirstOrDefault();
                string OszlopTÉrkDát = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "TÉrkDát" select a.Fejléc).FirstOrDefault();
                string OszlopTÉrkÓra = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "TÉrkÓra" select a.Fejléc).FirstOrDefault();
                string OszlopSzTípus = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "szerelvénytípus" select a.Fejléc).FirstOrDefault();
                string OszlopKocsikSZ = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "kocsikszáma" select a.Fejléc).FirstOrDefault();
                string OszlopMegj = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "megjegyzés" select a.Fejléc).FirstOrDefault();
                string OszlopKocsi1 = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "kocsi1" select a.Fejléc).FirstOrDefault();
                string OszlopKocsi2 = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "kocsi2" select a.Fejléc).FirstOrDefault();
                string OszlopKocsi3 = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "kocsi3" select a.Fejléc).FirstOrDefault();
                string OszlopKocsi4 = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "kocsi4" select a.Fejléc).FirstOrDefault();
                string OszlopKocsi5 = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "kocsi5" select a.Fejléc).FirstOrDefault();
                string OszlopKocsi6 = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "kocsi6" select a.Fejléc).FirstOrDefault();
                string OszlopStát = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "státus" select a.Fejléc).FirstOrDefault();

                if (OszlopVisz == null
                    || OszlopForg == null
                    || OszlopIndDát == null
                    || OszlopIndIdő == null
                    || OszlopTényDát == null
                    || OszlopTényIdő == null
                    || OszlopÉrkDát == null
                    || OszlopÉrkÓra == null
                    || OszlopTÉrkDát == null
                    || OszlopTÉrkÓra == null
                    || OszlopSzTípus == null
                    || OszlopKocsikSZ == null
                    || OszlopMegj == null
                    || OszlopKocsi1 == null
                    || OszlopKocsi2 == null
                    || OszlopKocsi3 == null
                    || OszlopKocsi4 == null
                    || OszlopKocsi5 == null
                    || OszlopKocsi6 == null
                    || OszlopStát == null) throw new HibásBevittAdat("Nem megfelelő formátumú a betölteni kívánt táblázat.");

                List<Adat_Főkönyv_ZSER> AdatokGy = new List<Adat_Főkönyv_ZSER>();
                Kezelő_Főkönyv_ZSER KézFőZser = new Kezelő_Főkönyv_ZSER();
                foreach (DataRow Sor in Tábla.Rows)
                {
                    string viszonylat = MyF.Szöveg_Tisztítás(Sor[OszlopVisz].ToStrTrim());
                    string forgalmiszám = MyF.Szöveg_Tisztítás(Sor[OszlopForg].ToStrTrim());

                    DateTime IndDát = Sor[OszlopIndDát].ToStrTrim().ToÉrt_DaTeTime();
                    DateTime IndÓra = Sor[OszlopIndIdő].ToStrTrim().ToÉrt_DaTeTime();
                    DateTime ideigdátum = new DateTime(IndDát.Year, IndDát.Month, IndDát.Day, IndÓra.Hour, IndÓra.Minute, IndÓra.Second);
                    ideigdátum = ideigdátum.AddMinutes(kiadási_korr);
                    DateTime tervindulás = ideigdátum;

                    DateTime TényDát = Sor[OszlopTényDát].ToStrTrim().ToÉrt_DaTeTime();
                    DateTime TényÓra = Sor[OszlopTényIdő].ToStrTrim().ToÉrt_DaTeTime();
                    ideigdátum = new DateTime(TényDát.Year, TényDát.Month, TényDát.Day, TényÓra.Hour, TényÓra.Minute, TényÓra.Second);
                    ideigdátum = ideigdátum.AddMinutes(kiadási_korr);
                    DateTime tényindulás = ideigdátum;

                    DateTime ÉrkDát = Sor[OszlopÉrkDát].ToStrTrim().ToÉrt_DaTeTime();
                    DateTime ÉrkÓra = Sor[OszlopÉrkÓra].ToStrTrim().ToÉrt_DaTeTime();
                    ideigdátum = new DateTime(ÉrkDát.Year, ÉrkDát.Month, ÉrkDát.Day, ÉrkÓra.Hour, ÉrkÓra.Minute, ÉrkÓra.Second);
                    ideigdátum = ideigdátum.AddMinutes(érkezési_korr);
                    DateTime tervérkezés = ideigdátum;

                    DateTime TÉrkDát = Sor[OszlopTÉrkDát].ToStrTrim().ToÉrt_DaTeTime();
                    DateTime TÉrkÓra = Sor[OszlopTÉrkÓra].ToStrTrim().ToÉrt_DaTeTime();
                    ideigdátum = new DateTime(TÉrkDát.Year, TÉrkDát.Month, TÉrkDát.Day, TÉrkÓra.Hour, TÉrkÓra.Minute, TÉrkÓra.Second);
                    ideigdátum = ideigdátum.AddMinutes(érkezési_korr);
                    DateTime tényérkezés = ideigdátum;

                    string napszak = "*";
                    string szerelvénytípus = MyF.Szöveg_Tisztítás(Sor[OszlopSzTípus].ToStrTrim());
                    long kocsikszáma = MyF.Szöveg_Tisztítás(Sor[OszlopKocsikSZ].ToStrTrim()).ToÉrt_Long();
                    string megjegyzés = MyF.Szöveg_Tisztítás(Sor[OszlopMegj].ToStrTrim(), 0, 20);

                    string kocsi1 = Főkönyv_Funkciók.Pályaszám_csorbítás(Sor[OszlopKocsi1].ToStrTrim());
                    string kocsi2 = Főkönyv_Funkciók.Pályaszám_csorbítás(Sor[OszlopKocsi2].ToStrTrim());
                    string kocsi3 = Főkönyv_Funkciók.Pályaszám_csorbítás(Sor[OszlopKocsi3].ToStrTrim());
                    string kocsi4 = Főkönyv_Funkciók.Pályaszám_csorbítás(Sor[OszlopKocsi4].ToStrTrim());
                    string kocsi5 = Főkönyv_Funkciók.Pályaszám_csorbítás(Sor[OszlopKocsi5].ToStrTrim());
                    string kocsi6 = Főkönyv_Funkciók.Pályaszám_csorbítás(Sor[OszlopKocsi6].ToStrTrim());
                    string ellenőrző = "_";
                    string státus = MyF.Szöveg_Tisztítás(Sor[OszlopStát].ToStrTrim(), 0, 10);

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
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "ZSER_Betöltés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public static void Km_adatok_beolvasása(DataTable Tábla, long kiadásikorr, long érkezésikorr, DateTime Dátum, string Telephely)
        {
            try
            {

                // Beolvasni kívánt oszlopok
                Kezelő_Excel_Beolvasás KézBeolvasás = new Kezelő_Excel_Beolvasás();
                List<Adat_Excel_Beolvasás> oszlopnév = KézBeolvasás.Lista_Adatok();


                Kezelő_Főkönyv_Zser_Km KézFőZserKm = new Kezelő_Főkönyv_Zser_Km();
                List<Adat_Főkönyv_Zser_Km> Adatok = KézFőZserKm.Lista_adatok(Dátum.Year);

                List<Adat_Főkönyv_Zser_Km> Elemek = (from a in Adatok
                                                     where a.Telephely == Telephely.Trim() && a.Dátum.ToShortDateString() == Dátum.ToShortDateString()
                                                     select a).ToList();

                // leellenőrizzük, hogy van-e már erre a napra rögzítve adat ha van töröljük
                if (Elemek != null) KézFőZserKm.Törlés(Telephely.Trim(), Dátum);

                string OszlopIndDát = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "IndDát" select a.Fejléc).FirstOrDefault();
                string OszlopIndIdő = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "IndÓra" select a.Fejléc).FirstOrDefault();
                string OszlopTényDát = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "TényDát" select a.Fejléc).FirstOrDefault();
                string OszlopTényIdő = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "TényÓra" select a.Fejléc).FirstOrDefault();
                string OszlopÉrkDát = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "ÉrkDát" select a.Fejléc).FirstOrDefault();
                string OszlopÉrkÓra = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "ÉrkÓra" select a.Fejléc).FirstOrDefault();
                string OszlopTÉrkDát = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "TÉrkDát" select a.Fejléc).FirstOrDefault();
                string OszlopTÉrkÓra = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "TÉrkÓra" select a.Fejléc).FirstOrDefault();
                string OszlopKocsi1 = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "kocsi1" select a.Fejléc).FirstOrDefault();
                string OszlopKocsi2 = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "kocsi2" select a.Fejléc).FirstOrDefault();
                string OszlopKocsi3 = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "kocsi3" select a.Fejléc).FirstOrDefault();
                string OszlopKocsi4 = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "kocsi4" select a.Fejléc).FirstOrDefault();
                string OszlopKocsi5 = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "kocsi5" select a.Fejléc).FirstOrDefault();
                string OszlopKocsi6 = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "kocsi6" select a.Fejléc).FirstOrDefault();
                string OszlopKm = (from a in oszlopnév where a.Csoport == "Zsel" && a.Státusz == false && a.Változónév == "km" select a.Fejléc).FirstOrDefault();

                if (OszlopIndDát == null
                            || OszlopIndIdő == null
                            || OszlopTényDát == null
                            || OszlopTényIdő == null
                            || OszlopÉrkDát == null
                            || OszlopÉrkÓra == null
                            || OszlopTÉrkDát == null
                            || OszlopTÉrkÓra == null
                            || OszlopKocsi1 == null
                            || OszlopKocsi2 == null
                            || OszlopKocsi3 == null
                            || OszlopKocsi4 == null
                            || OszlopKocsi5 == null
                            || OszlopKocsi6 == null
                            || OszlopKm == null) throw new HibásBevittAdat("Nem megfelelő formátumú a betölteni kívánt táblázat.");

                // beolvassuk az excel tábla szükséges adatait
                List<Adat_Főkönyv_Zser_Km> AdatokGy = new List<Adat_Főkönyv_Zser_Km>();
                foreach (DataRow Sor in Tábla.Rows)
                {
                    DateTime IndDát = Sor[OszlopIndDát].ToStrTrim().ToÉrt_DaTeTime();
                    DateTime IndÓra = Sor[OszlopIndIdő].ToStrTrim().ToÉrt_DaTeTime();
                    DateTime ideigdátum = new DateTime(IndDát.Year, IndDát.Month, IndDát.Day, IndÓra.Hour, IndÓra.Minute, IndÓra.Second);
                    ideigdátum = ideigdátum.AddMinutes(kiadásikorr);
                    DateTime tervindulás = ideigdátum;

                    DateTime TényDát = Sor[OszlopTényDát].ToStrTrim().ToÉrt_DaTeTime();
                    DateTime TényÓra = Sor[OszlopTényIdő].ToStrTrim().ToÉrt_DaTeTime();
                    ideigdátum = new DateTime(TényDát.Year, TényDát.Month, TényDát.Day, TényÓra.Hour, TényÓra.Minute, TényÓra.Second);
                    ideigdátum = ideigdátum.AddMinutes(kiadásikorr);
                    DateTime tényindulás = ideigdátum;

                    DateTime ÉrkDát = Sor[OszlopÉrkDát].ToStrTrim().ToÉrt_DaTeTime();
                    DateTime ÉrkÓra = Sor[OszlopÉrkÓra].ToStrTrim().ToÉrt_DaTeTime();
                    ideigdátum = new DateTime(ÉrkDát.Year, ÉrkDát.Month, ÉrkDát.Day, ÉrkÓra.Hour, ÉrkÓra.Minute, ÉrkÓra.Second);
                    ideigdátum = ideigdátum.AddMinutes(érkezésikorr);
                    DateTime tervérkezés = ideigdátum;

                    DateTime TÉrkDát = Sor[OszlopTÉrkDát].ToStrTrim().ToÉrt_DaTeTime();
                    DateTime TÉrkÓra = Sor[OszlopTÉrkÓra].ToStrTrim().ToÉrt_DaTeTime();
                    ideigdátum = new DateTime(TÉrkDát.Year, TÉrkDát.Month, TÉrkDát.Day, TÉrkÓra.Hour, TÉrkÓra.Minute, TÉrkÓra.Second);
                    ideigdátum = ideigdátum.AddMinutes(érkezésikorr);
                    DateTime tényérkezés = ideigdátum;

                    string kocsi1 = Főkönyv_Funkciók.Pályaszám_csorbítás(Sor[OszlopKocsi1].ToStrTrim());
                    string kocsi2 = Főkönyv_Funkciók.Pályaszám_csorbítás(Sor[OszlopKocsi2].ToStrTrim());
                    string kocsi3 = Főkönyv_Funkciók.Pályaszám_csorbítás(Sor[OszlopKocsi3].ToStrTrim());
                    string kocsi4 = Főkönyv_Funkciók.Pályaszám_csorbítás(Sor[OszlopKocsi4].ToStrTrim());
                    string kocsi5 = Főkönyv_Funkciók.Pályaszám_csorbítás(Sor[OszlopKocsi5].ToStrTrim());
                    string kocsi6 = Főkönyv_Funkciók.Pályaszám_csorbítás(Sor[OszlopKocsi6].ToStrTrim());

                    int km = Sor[OszlopKm].ToStrTrim().ToÉrt_Int();

                    TimeSpan számhossz = tervérkezés - tervindulás;
                    TimeSpan menethossz = tényérkezés - tényindulás;

                    if (számhossz.TotalMinutes != menethossz.TotalMinutes && menethossz.TotalMinutes != 0)
                    {
                        //Ha nem a teljes számot járja le akkor kiszámoljuk a töredék km-t.
                        km = (int)((km * menethossz.TotalMinutes) / számhossz.TotalMinutes);
                    }
                    if (kocsi1.Trim() != "_")
                    {
                        Adat_Főkönyv_Zser_Km ADAT = new Adat_Főkönyv_Zser_Km(kocsi1.Trim(), tervindulás, km, Telephely.Trim());
                        AdatokGy.Add(ADAT);
                    }
                    if (kocsi2.Trim() != "_")
                    {
                        Adat_Főkönyv_Zser_Km ADAT = new Adat_Főkönyv_Zser_Km(kocsi2.Trim(), tervindulás, km, Telephely.Trim());
                        AdatokGy.Add(ADAT);
                    }
                    if (kocsi3.Trim() != "_")
                    {
                        Adat_Főkönyv_Zser_Km ADAT = new Adat_Főkönyv_Zser_Km(kocsi3.Trim(), tervindulás, km, Telephely.Trim());
                        AdatokGy.Add(ADAT);
                    }
                    if (kocsi4.Trim() != "_")
                    {
                        Adat_Főkönyv_Zser_Km ADAT = new Adat_Főkönyv_Zser_Km(kocsi4.Trim(), tervindulás, km, Telephely.Trim());
                        AdatokGy.Add(ADAT);
                    }
                    if (kocsi5.Trim() != "_")
                    {
                        Adat_Főkönyv_Zser_Km ADAT = new Adat_Főkönyv_Zser_Km(kocsi5.Trim(), tervindulás, km, Telephely.Trim());
                        AdatokGy.Add(ADAT);
                    }
                    if (kocsi6.Trim() != "_")
                    {
                        Adat_Főkönyv_Zser_Km ADAT = new Adat_Főkönyv_Zser_Km(kocsi6.Trim(), tervindulás, km, Telephely.Trim());
                        AdatokGy.Add(ADAT);
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
                HibaNapló.Log(ex.Message, "Km_adatok_beolvasása", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception("MyA rögzítési hiba, az adotok rögzítése/módosítása nem történt meg.");
            }
        }

        public static void Raktár_beolvasó(string fájlexcel)
        {
            try
            {
                DataTable Tábla = MyF.Excel_Tábla_Beolvas(fájlexcel);
                //Ellenőrzés
                if (!MyF.Betöltéshelyes("AnyagVétel", Tábla)) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt adatok formátuma ! ");

                // Beolvasni kívánt oszlopok
                Kezelő_Excel_Beolvasás KézBeolvasás = new Kezelő_Excel_Beolvasás();
                List<Adat_Excel_Beolvasás> oszlopnév = KézBeolvasás.Lista_Adatok();

                //Meghatározzuk a beolvasó tábla elnevezéseit 
                //Oszlopnevek beállítása
                string oszlopCikk = (from a in oszlopnév where a.Csoport == "AnyagVétel" && a.Státusz == false && a.Változónév == "Cikkszám" select a.Fejléc).FirstOrDefault();
                string oszlopMeg = (from a in oszlopnév where a.Csoport == "AnyagVétel" && a.Státusz == false && a.Változónév == "Megnevezés" select a.Fejléc).FirstOrDefault();
                string oszlopRaktár = (from a in oszlopnév where a.Csoport == "AnyagVétel" && a.Státusz == false && a.Változónév == "Raktár" select a.Fejléc).FirstOrDefault();
                string oszlopMennyi = (from a in oszlopnév where a.Csoport == "AnyagVétel" && a.Státusz == false && a.Változónév == "Mennyi" select a.Fejléc).FirstOrDefault();
                string oszlopÁr = (from a in oszlopnév where a.Csoport == "AnyagVétel" && a.Státusz == false && a.Változónév == "Ár" select a.Fejléc).FirstOrDefault();
                string oszlopSarzs = (from a in oszlopnév where a.Csoport == "AnyagVétel" && a.Státusz == false && a.Változónév == "Sarzs" select a.Fejléc).FirstOrDefault();

                if (oszlopCikk == null
                  || oszlopMeg == null
                  || oszlopRaktár == null
                  || oszlopMennyi == null
                  || oszlopÁr == null
                  || oszlopSarzs == null) throw new HibásBevittAdat("Nincs helyesen beállítva a beolvasótábla! ");


                // Első adattól végig pörgetjük a beolvasást addig amíg nem lesz üres
                List<Adat_Anyagok> AdatokGy = new List<Adat_Anyagok>();
                List<Adat_Raktár> AdatokGyR = new List<Adat_Raktár>();
                int sor = 0;
                foreach (DataRow Sor in Tábla.Rows)
                {
                    //Beolvasott értékeke
                    string Cikkszám = MyF.Szöveg_Tisztítás(Sor[oszlopCikk].ToStrTrim(), 0, 20).TrimStart('0');
                    string Megnevezés = MyF.Szöveg_Tisztítás(Sor[oszlopMeg].ToStrTrim(), 0, 255);
                    string Raktár = MyF.Szöveg_Tisztítás(Sor[oszlopRaktár].ToStrTrim(), 0, 5);
                    double Mennyi = MyF.Szöveg_Tisztítás(Sor[oszlopMennyi].ToStrTrim()).Replace(".", "").ToÉrt_Double();
                    double Ár = MyF.Szöveg_Tisztítás(Sor[oszlopÁr].ToStrTrim()).Replace(".", "").ToÉrt_Double();
                    string Sarzs = MyF.Szöveg_Tisztítás(Sor[oszlopSarzs].ToStrTrim(), 0, 5);
                    double Árdb = 0;
                    if (Mennyi != 0) Árdb = Ár / Mennyi;

                    //Előállítjuk a cikktörzsnek megfelelő adatokat
                    Adat_Anyagok ADAT = new Adat_Anyagok(
                               Cikkszám,
                               Megnevezés,
                               "",
                               Sarzs,
                               Árdb);
                    AdatokGy.Add(ADAT);

                    //Módosítjuk a raktárkészletet
                    Adat_Raktár ADATR = new Adat_Raktár(
                                Cikkszám,
                                Sarzs,
                                Raktár,
                                Mennyi);
                    AdatokGyR.Add(ADATR);
                    sor++;
                }

                Kezelő_AnyagTörzs Kéz = new Kezelő_AnyagTörzs();
                if (AdatokGy.Count > 0) Kéz.Osztályoz(AdatokGy);

                Kezelő_Raktár KézRaktár = new Kezelő_Raktár();
                if (AdatokGyR.Count > 0) KézRaktár.Rögzítés(AdatokGyR);

                // kitöröljük a betöltött fájlt
                File.Delete(fájlexcel);
                MessageBox.Show($"Az adat konvertálás befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

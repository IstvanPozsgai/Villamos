using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
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
    }
}

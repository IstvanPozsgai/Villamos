using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos_Ablakok.CAF_Ütemezés
{
    static public class CAF_Közös_Eljárások
    {
        readonly static Kezelő_CAF_Adatok Kéz_Adatok = new Kezelő_CAF_Adatok();
        readonly static Kezelő_CAF_alap KézAlap = new Kezelő_CAF_alap();
        readonly static Kezelő_Ciklus Kéz_Ciklus = new Kezelő_Ciklus();

        readonly static List<Adat_Ciklus> Ciklus = Kéz_Ciklus.Lista_Adatok(true);

        public static Adat_CAF_Adatok Következő_Idő(List<Adat_Ciklus> Ciklus, Adat_CAF_Adatok Előző, Adat_CAF_alap Kocsi)
        {
            Adat_CAF_Adatok válasz = null;
            try
            {
                double Id = 0;
                string Azonosító = Kocsi.Azonosító.Trim();

                long Számláló = 0;
                int Idő_sorszám = 0;
                int Státus = 0;
                int KM_Sorszám = 0;
                int IDŐvKM = 1;
                DateTime Dátum = Kocsi.Vizsgdátum_nap;
                if (Előző != null)
                {
                    Számláló = Előző.Számláló;
                    Idő_sorszám = Előző.IDŐ_Sorszám;
                    KM_Sorszám = Előző.KM_Sorszám;
                    IDŐvKM = Előző.IDŐvKM;
                    Dátum = Előző.Dátum;
                }

                // ha az utolsó ütem akkor lenullázuk az értéket
                int következő;
                if (IDŐvKM == 2)
                {
                    következő = 1;
                }
                else
                {
                    if (Ciklus[Ciklus.Count - 1].Sorszám == Idő_sorszám)
                        következő = 1;
                    else
                        következő = Idő_sorszám + 1;
                }
                int IDŐ_Sorszám = következő;
                string Vizsgálat = Ciklus[következő - 1].Vizsgálatfok.Trim();

                int névleges_nap = (int)Ciklus[következő - 1].Névleges;
                Dátum = Dátum.AddDays(névleges_nap);
                DateTime Dátum_program = Dátum.AddDays(névleges_nap);
                IDŐvKM = 1;
                string Megjegyzés = "_";

                válasz = new Adat_CAF_Adatok(Id, Azonosító, Vizsgálat, Dátum, Dátum_program, Számláló, Státus, KM_Sorszám, IDŐ_Sorszám, IDŐvKM, Megjegyzés);
                return válasz;
            }

            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Utolsó_ütemezett", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return válasz;
            }
        }

        public static Adat_CAF_Adatok Következő_Km(List<Adat_Ciklus> Ciklus, Adat_CAF_Adatok Előző, Adat_CAF_alap Kocsi)
        {
            Adat_CAF_Adatok válasz = null;
            try
            {
                double Id = 0;
                string Azonosító = Kocsi.Azonosító.Trim();
                long Számláló = 0;
                int KM_Sorszám = 0;
                DateTime Dátum = Kocsi.Vizsgdátum_km;
                if (Előző != null)
                {
                    Számláló = Előző.Számláló;
                    KM_Sorszám = Előző.KM_Sorszám;
                    Dátum = Előző.Dátum;
                }



                int Státus = 0;
                int IDŐ_Sorszám = 1;

                // ha az utolsó ütem akkor lenullázuk az értéket
                int következő;
                if (Ciklus[Ciklus.Count - 1].Sorszám == KM_Sorszám)
                    következő = 1;
                else
                    következő = KM_Sorszám + 1;

                KM_Sorszám = következő;
                string Vizsgálat = Ciklus[következő - 1].Vizsgálatfok.Trim();

                int NapiKm = (int)(Kocsi.Havikm / 30);
                if (NapiKm == 0) NapiKm = 1;

                //Ha nincs számláló állás akkor a teljes ciklust kell tervezni.
                double Vizsgálat_Óta_km;
                if (Számláló == 0)
                    Vizsgálat_Óta_km = 0;
                else
                    Vizsgálat_Óta_km = Kocsi.KMUkm - Kocsi.Számláló;

                double futhatmég_Nap = (Ciklus[következő - 1].Felsőérték - Vizsgálat_Óta_km) / NapiKm;


                Dátum = Dátum.AddDays(futhatmég_Nap);
                DateTime Dátum_program = Dátum;
                int IDŐvKM = 2;
                string Megjegyzés = "";

                válasz = new Adat_CAF_Adatok(Id, Azonosító, Vizsgálat, Dátum, Dátum_program, Számláló, Státus, KM_Sorszám, IDŐ_Sorszám, IDŐvKM, Megjegyzés);
                return válasz;
            }

            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Utolsó_ütemezett", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return válasz;
            }


        }

        /// <summary>
        /// Az adott kocsi idő alapú ciklusrendjét beütemezi a két dátum között
        /// akkor áll le, ha végdátumot meghaladtuk, de az utolsó értéket még rögzíti
        /// </summary>
        /// <param name="pályaszám"></param>
        /// <param name="Elő_Dátumig"></param>
        /// <param name="Elő_Dátumtól"></param>
        /// <returns></returns>
        public static List<Adat_CAF_Adatok> IDŐ_EgyKocsi(string pályaszám, DateTime Elő_Dátumig, DateTime Elő_Dátumtól)
        {
            List<Adat_CAF_Adatok> Válasz = new List<Adat_CAF_Adatok>();
            try
            {
                bool vége = true;
                Adat_CAF_alap EgyCAF = KézAlap.Egy_Adat(pályaszám.Trim(), true);   //Jármű tulajdonsága
                List<Adat_Ciklus> Ciklus_Idő = Ciklus.Where(a => a.Típus == EgyCAF.Ciklusnap).OrderBy(a => a.Sorszám).ToList();
                Adat_CAF_Adatok Előző = Kéz_Adatok.Egy_Adat(pályaszám.Trim());    // utolsó ütemezett

                int Státus = 0;
                int IDŐvKM = 1;
                string Megjegyzés = "_";
                long Számláló = 0;
                int Idő_sorszám = 0;
                int KM_Sorszám = 0;
                DateTime Dátum = EgyCAF.Vizsgdátum_nap;
                if (Előző != null)
                {
                    Számláló = Előző.Számláló;
                    Idő_sorszám = Előző.IDŐ_Sorszám;
                    KM_Sorszám = Előző.KM_Sorszám;
                    Dátum = Előző.Dátum;
                    IDŐvKM = Előző.IDŐvKM;
                }


                if (Elő_Dátumtól != new DateTime(1900, 1, 1)) Dátum = Elő_Dátumtól;
                // ha km alapú az utolsó, vagy ha a maximálist elérte akkor 1
                // különben a ciklus következő elemét veszi
                int következő = 1;
                if (!(IDŐvKM == 1 && Ciklus_Idő[Ciklus_Idő.Count - 1].Sorszám == Idő_sorszám))
                    következő = Idő_sorszám + 1;


                while (vége == true)
                {
                    int IDŐ_Sorszám = következő;
                    string Vizsgálat = Ciklus_Idő[következő - 1].Vizsgálatfok.Trim();
                    long névleges_nap = Ciklus_Idő[következő - 1].Névleges;
                    Dátum = Dátum.AddDays(névleges_nap);

                    Adat_CAF_Adatok ADAT = new Adat_CAF_Adatok(0, pályaszám, Vizsgálat, Dátum, Dátum, Számláló, Státus, KM_Sorszám, IDŐ_Sorszám, 1, Megjegyzés);
                    Válasz.Add(ADAT);
                    // ha belefér az időbe akkor rögzítjük
                    if (Elő_Dátumig < ADAT.Dátum) vége = false;
                    következő++;
                    if (Ciklus_Idő.Count < következő) következő = 1;
                }

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "IDŐ_EgyKocsi", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Válasz;
        }

        public static List<Adat_CAF_Adatok> KM_EgyKocsi(string pályaszám, DateTime Elő_Dátumig)
        {
            List<Adat_CAF_Adatok> Válasz = new List<Adat_CAF_Adatok>();
            try
            {
                bool vége = true;
                Adat_CAF_alap EgyCAF = KézAlap.Egy_Adat(pályaszám.Trim(), true);//Jármű tulajdonsága
                List<Adat_Ciklus> Ciklus_Km = Ciklus.Where(a => a.Típus == EgyCAF.Cikluskm).OrderBy(a => a.Sorszám).ToList();
                Adat_CAF_Adatok Előző = Kéz_Adatok.Egy_Adat(pályaszám.Trim(), 2);

                double Vizsgálat_Óta_km;
                long Számláló = 0;
                int Km_sorszám = 0;
                DateTime Dátum = EgyCAF.Vizsgdátum_km;
                if (Előző != null)
                {
                    Számláló = Előző.Számláló;
                    Km_sorszám = Előző.KM_Sorszám;
                    Dátum = Előző.Dátum;
                }


                //Ha nincs számláló állás akkor a teljes ciklust kell tervezni.
                if (Számláló == 0)
                    Vizsgálat_Óta_km = 0;
                else
                    Vizsgálat_Óta_km = EgyCAF.KMUkm - EgyCAF.Számláló;
                int Státus = 0;
                int IDŐ_Sorszám = 1;
                int következő;
                int NapiKm = (int)(EgyCAF.Havikm / 30);
                if (NapiKm == 0) NapiKm = 1;
                int IDŐvKM = 2;
                string Megjegyzés = "_";
                // ha az utolsó ütem akkor lenullázuk az értéket
                if (Ciklus_Km[Ciklus_Km.Count - 1].Sorszám == Km_sorszám)
                    következő = 1;
                else
                    következő = Km_sorszám + 1;

                bool első = true;


                while (vége == true)
                {
                    int KM_Sorszám = következő;
                    string Vizsgálat = Ciklus_Km[következő - 1].Vizsgálatfok.Trim();
                    double futhatmég_Nap = 0;
                    if (első)
                    {
                        if (Ciklus_Km[következő - 1].Felsőérték > Vizsgálat_Óta_km) futhatmég_Nap = (Ciklus_Km[következő - 1].Felsőérték - Vizsgálat_Óta_km) / NapiKm;
                        első = false;
                    }
                    else
                        futhatmég_Nap = Ciklus_Km[következő - 1].Felsőérték / NapiKm;

                    Dátum = Dátum.AddDays(futhatmég_Nap);
                    Adat_CAF_Adatok ADAT = new Adat_CAF_Adatok(0, pályaszám, Vizsgálat, Dátum, Dátum, Számláló, Státus, KM_Sorszám, IDŐ_Sorszám, IDŐvKM, Megjegyzés);
                    Válasz.Add(ADAT);
                    // ha belefér az időbe akkor rögzítjük
                    if (Elő_Dátumig < ADAT.Dátum) vége = false;
                    következő++;
                    if (Ciklus_Km.Count < következő) következő = 1;
                }

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "KM_EgyKocsi", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Válasz;
        }

        public static void Idő_átütemezés(List<Adat_CAF_Adatok> Adatok, Adat_CAF_Adatok Adat, DateTime Dátumra, DateTime Dátumig)
        {
            try
            {
                // ha nem raktuk át másik napra akkor kilépünk
                if (Adat.Dátum == Dátumra) throw new HibásBevittAdat("Nem történt meg az átütemezés");

                if (Adat != null)
                {
                    // rögzítjük az új dátumra az adatot
                    // újat hoz létre
                    Adat_CAF_Adatok Új_adat = new Adat_CAF_Adatok(
                        Adat.Id,
                        Adat.Azonosító,
                        Adat.Vizsgálat,
                        Dátumra,
                        Adat.Dátum_program,
                        Adat.Számláló,
                        Adat.Státus,
                        Adat.KM_Sorszám,
                        Adat.IDŐ_Sorszám,
                        Adat.IDŐvKM,
                        "Átütemezés");
                    Kéz_Adatok.Döntés(Adatok, Új_adat);

                    // töröljük az új dátum utáni tervet

                    Adat_CAF_Adatok Elem = (from a in Adatok
                                            where a.Azonosító == Adat.Azonosító.Trim()
                                            && a.Dátum > Dátumra
                                            && a.Státus == 0
                                            select a).FirstOrDefault();
                    if (Elem != null) Kéz_Adatok.Törlés(Dátumra.AddDays(1), Adat.Azonosító.Trim());

                    // ütemezzük újra a kocsikat
                    // idő szerit
                    List<Adat_CAF_Adatok> IdőAdatok = IDŐ_EgyKocsi(Adat.Azonosító.Trim(), Dátumig, Dátumra);

                    // km szerint
                    List<Adat_CAF_Adatok> KMAdatok = KM_EgyKocsi(Adat.Azonosító.Trim(), Dátumig);
                    IdőAdatok.AddRange(KMAdatok);
                    Kéz_Adatok.Rögzítés(IdőAdatok);
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Idő_átütemezés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Km_átütemezés(List<Adat_CAF_Adatok> Adatok, Adat_CAF_Adatok Adat, DateTime Dátumra, DateTime Dátumig)
        {
            try
            {
                if (Adat.Dátum != Dátumra)
                {
                    if (Adat != null)
                    {
                        // töröltre állítjuk az aktuális sorszámot
                        Kéz_Adatok.Módosítás_Státus(Adat.Id, 9);

                        // ezen a napon ha van már idő alapú akkor töröljük
                        Adat_CAF_Adatok rekord = (from a in Adatok
                                                  where a.Dátum >= Dátumra
                                                  && a.Azonosító == Adat.Azonosító.Trim()
                                                  && a.Státus == 0
                                                  select a).FirstOrDefault();
                        if (rekord != null) Kéz_Adatok.Törlés(Dátumra, Adat.Azonosító.Trim());

                        // rögzítjük az új dátumra az adatot
                        Adat_CAF_Adatok Új_adat = new Adat_CAF_Adatok(
                          Adat.Id,
                          Adat.Azonosító,
                          Adat.Vizsgálat,
                          Dátumra,
                          Adat.Dátum_program,
                          Adat.Számláló,
                          0,
                          Adat.KM_Sorszám,
                          Adat.IDŐ_Sorszám,
                          Adat.IDŐvKM,
                          "Átütemezés");
                        Kéz_Adatok.Döntés(Adatok, Új_adat);
                    }
                }
                // ütemezzük újra a kocsikat
                // idő szerit
                List<Adat_CAF_Adatok> IdőAdatok = IDŐ_EgyKocsi(Adat.Azonosító.Trim(), Dátumig, new DateTime(1900, 1, 1));

                // km szerint
                List<Adat_CAF_Adatok> KMAdatok = KM_EgyKocsi(Adat.Azonosító.Trim(), Dátumig);
                IdőAdatok.AddRange(KMAdatok);
                Kéz_Adatok.Rögzítés(IdőAdatok);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Km_átütemezés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


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

                long Számláló = Előző.Számláló;
                int Státus = 0;
                int Idő_sorszám = Előző.IDŐ_Sorszám;
                int KM_Sorszám = Előző.KM_Sorszám;

                // ha az utolsó ütem akkor lenullázuk az értéket
                int következő;
                if (Előző.IDŐvKM == 2)
                {
                    következő = 1;
                }
                else
                {
                    if (Ciklus[Ciklus.Count - 1].Sorszám == Előző.IDŐ_Sorszám)
                        következő = 1;
                    else
                        következő = Előző.IDŐ_Sorszám + 1;
                }
                int IDŐ_Sorszám = következő;
                string Vizsgálat = Ciklus[következő - 1].Vizsgálatfok.Trim();

                int névleges_nap = (int)Ciklus[következő - 1].Névleges;
                DateTime Dátum = Előző.Dátum.AddDays(névleges_nap);
                DateTime Dátum_program = Előző.Dátum.AddDays(névleges_nap);
                int IDŐvKM = 1;
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

                long Számláló = Előző.Számláló;
                int Státus = 0;
                int IDŐ_Sorszám = 1;

                // ha az utolsó ütem akkor lenullázuk az értéket
                int következő;
                if (Ciklus[Ciklus.Count - 1].Sorszám == Előző.KM_Sorszám)
                    következő = 1;
                else
                    következő = Előző.KM_Sorszám + 1;

                int KM_Sorszám = következő;
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


                DateTime Dátum = Előző.Dátum.AddDays(futhatmég_Nap);
                DateTime Dátum_program = Előző.Dátum.AddDays(futhatmég_Nap);
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

        public static void IDŐ_Eltervező_EgyKocsi(string pályaszám, DateTime Elő_Dátumig)
        {
            try
            {
                bool vége = true;
                Adat_CAF_alap EgyCAF = KézAlap.Egy_Adat(pályaszám.Trim(), true);
                List<Adat_Ciklus> Ciklus_Idő = Ciklus.Where(a => a.Típus == EgyCAF.Ciklusnap).OrderBy(a => a.Sorszám).ToList();

                while (vége == true)
                {
                    //Jármű tulajdonsága
                    EgyCAF = KézAlap.Egy_Adat(pályaszám.Trim());
                    // utolsó ütemezett
                    Adat_CAF_Adatok Előző = Kéz_Adatok.Egy_Adat(pályaszám.Trim());
                    // következő idő szerinti
                    Adat_CAF_Adatok Adat = Következő_Idő(Ciklus_Idő, Előző, EgyCAF);

                    // ha belefér az időbe akkor rögzítjük
                    if (Elő_Dátumig >= Adat.Dátum)
                        Kéz_Adatok.Döntés(Adat);
                    else
                        vége = false;
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "IDŐ_Eltervező_EgyKocsi", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static List<Adat_CAF_Adatok> IDŐ_EgyKocsi(string pályaszám, DateTime Elő_Dátumig)
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
                long Számláló = Előző.Számláló;
                int Idő_sorszám = Előző.IDŐ_Sorszám;
                int KM_Sorszám = Előző.KM_Sorszám;
                DateTime Dátum = Előző.Dátum;
                // ha az utolsó ütem akkor lenullázuk az értéket
                int következő;
                if (Előző.IDŐvKM == 2)
                {
                    következő = 1;
                }
                else
                {
                    if (Ciklus_Idő[Ciklus_Idő.Count - 1].Sorszám == Előző.IDŐ_Sorszám)
                        következő = 1;
                    else
                        következő = Előző.IDŐ_Sorszám + 1;
                }

                while (vége == true)
                {
                    int IDŐ_Sorszám = következő;
                    string Vizsgálat = Ciklus_Idő[következő - 1].Vizsgálatfok.Trim();
                    long névleges_nap = Ciklus_Idő[következő - 1].Névleges;
                    Dátum = Dátum.AddDays(névleges_nap);

                    Adat_CAF_Adatok ADAT = new Adat_CAF_Adatok(0, pályaszám, Vizsgálat, Dátum, Dátum, Számláló, Státus, KM_Sorszám, IDŐ_Sorszám, IDŐvKM, Megjegyzés);
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

        public static void KM_Eltervező_EgyKocsi(string pályaszám, DateTime Elő_Dátumig)
        {
            try
            {
                bool vége = true;
                Adat_CAF_alap EgyCAF = KézAlap.Egy_Adat(pályaszám.Trim(), true);
                List<Adat_Ciklus> Ciklus_Km = Ciklus.Where(a => a.Típus == EgyCAF.Cikluskm).OrderBy(a => a.Sorszám).ToList();
                while (vége == true)
                {
                    //Jármű tulajdonsága
                    EgyCAF = KézAlap.Egy_Adat(pályaszám.Trim());
                    // utolsó ütemezett
                    Adat_CAF_Adatok Előző = Kéz_Adatok.Egy_Adat(pályaszám.Trim(), 2);
                    // következő km szerinti
                    Adat_CAF_Adatok Adat = Következő_Km(Ciklus_Km, Előző, EgyCAF);
                    // ha belefér az időbe akkor rögzítjük
                    if (Elő_Dátumig >= Adat.Dátum)
                        Kéz_Adatok.Döntés(Adat);
                    else
                        vége = false;
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "KM_Eltervező_EgyKocsi", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static List<Adat_CAF_Adatok> KM_EgyKocsi(string pályaszám, DateTime Elő_Dátumig)
        {
            List<Adat_CAF_Adatok> Válasz = new List<Adat_CAF_Adatok>();
            try
            {
                bool vége = true;
                Adat_CAF_alap EgyCAF = KézAlap.Egy_Adat(pályaszám.Trim(), true);//Jármű tulajdonsága
                List<Adat_Ciklus> Ciklus_Km = Ciklus.Where(a => a.Típus == EgyCAF.Cikluskm).OrderBy(a => a.Sorszám).ToList();
                Adat_CAF_Adatok Előző = Kéz_Adatok.Egy_Adat(pályaszám.Trim(), 2);     // utolsó ütemezett

                double Vizsgálat_Óta_km;
                long Számláló = Előző.Számláló;
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
                if (Ciklus_Km[Ciklus_Km.Count - 1].Sorszám == Előző.KM_Sorszám)
                    következő = 1;
                else
                    következő = Előző.KM_Sorszám + 1;
                DateTime Dátum = Előző.Dátum;
                while (vége == true)
                {
                    int KM_Sorszám = következő;
                    string Vizsgálat = Ciklus_Km[következő - 1].Vizsgálatfok.Trim();
                    double futhatmég_Nap = (Ciklus_Km[következő - 1].Felsőérték - Vizsgálat_Óta_km) / NapiKm;
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
    }
}


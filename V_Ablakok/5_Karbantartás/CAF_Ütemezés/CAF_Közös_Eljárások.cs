using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Kezelők;
using MyA = Adatbázis;

namespace Villamos.Villamos_Ablakok.CAF_Ütemezés
{
    static public class CAF_Közös_Eljárások
    {
        readonly static Kezelő_CAF_Adatok Kéz_Adatok = new Kezelő_CAF_Adatok();
        readonly static Kezelő_CAF_alap KézAlap = new Kezelő_CAF_alap();

        static public Adat_CAF_alap Villamos_tulajdonság(string pályaszám)
        {
            Adat_CAF_alap válasz = null;
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";
                string szöveg = $"SELECT * FROM alap WHERE azonosító='{pályaszám.Trim()}'";

                Adat_CAF_alap rekord = KézAlap.Egy_Adat(hely, jelszó, szöveg);
                válasz = rekord;
                return válasz;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Villamos_tulajdonság", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return válasz;
            }
        }

        public static Adat_CAF_Adatok Utolsó_ütemezett(string pályaszám, string időVSkm)
        {
            Adat_CAF_Adatok válasz = null;
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";
                string szöveg = $"SELECT * FROM adatok WHERE azonosító='{pályaszám.Trim()}' ";
                switch (időVSkm)
                {
                    case "km":
                        szöveg += " AND Idővkm=2 ";
                        break;
                }
                szöveg += " AND státus<9 ORDER BY dátum desc";


                Adat_CAF_Adatok Adat = Kéz_Adatok.Egy_Adat(hely, jelszó, szöveg);
                válasz = Adat;
                return válasz;
            }

            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Utolsó_ütemezett", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return válasz;
            }
        }


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


        public static double Köv_Sorszám(string hely, string jelszó)
        {
            string szöveg = "SELECT * FROM adatok ORDER BY id desc";
            List<Adat_CAF_Adatok> Adatok = Kéz_Adatok.Lista_Adatok(hely, jelszó, szöveg);
            double válasz = 1;
            if (Adatok.Count > 0) válasz = Adatok.Max (a=>a.Id )+1;
            return válasz;
        }

        public static void RögzítiMódosít(Adat_CAF_Adatok Adat, string hely, string jelszó)
        {
            try
            {
                double Sorszám;
    

                // ha nincs kitöltve az id, megkeressük a következő számot
                if (Adat.Id == 0)
                    Sorszám = Köv_Sorszám(hely, jelszó);
                else
                    Sorszám = Adat.Id;

                string szöveg = "SELECT * FROM adatok ORDER BY id desc";
                List<Adat_CAF_Adatok> Adatok = Kéz_Adatok.Lista_Adatok(hely, jelszó, szöveg);

                Adat_CAF_Adatok Elem = (from a in Adatok
                                        where a.Id == Sorszám
                                        select a).FirstOrDefault();

                if (Elem!=null)
                {
                    // Módosít
                    szöveg = "UPDATE adatok  SET ";
                    szöveg += "vizsgálat='" + Adat.Vizsgálat.Trim() + "', "; // vizsgálat
                    szöveg += "Dátum='" + Adat.Dátum.ToString("yyyy.MM.dd").Trim() + "', "; // Dátum
                    szöveg += "számláló=" + Adat.Számláló + ", "; // számláló
                    szöveg += "státus=" + Adat.Státus + ", "; // státus 
                    szöveg += "km_sorszám=" + Adat.KM_Sorszám + ", "; // km_sorszám
                    szöveg += "idő_sorszám=" + Adat.IDŐ_Sorszám + ", "; // idő_sorszám
                    szöveg += "megjegyzés='" + Adat.Megjegyzés.Trim() + "', "; // megjegyzés
                    szöveg += $"idővKM={Adat.IDŐvKM} ";
                    szöveg += " WHERE id=" + Sorszám;
                }
                else
                {
                    // újat hoz létre
                    szöveg = "INSERT INTO adatok (id, azonosító, vizsgálat, Dátum, számláló, státus, km_sorszám, idő_sorszám, idővKM, megjegyzés, Dátum_program) VALUES (";
                    szöveg += Sorszám + ", "; // id 
                    szöveg += "'" + Adat.Azonosító.Trim() + "', "; // azonosító
                    szöveg += "'" + Adat.Vizsgálat.Trim() + "', "; // vizsgálat
                    szöveg += " '" + Adat.Dátum.ToString("yyyy.MM.dd").Trim() + "', "; // Dátum
                    szöveg += Adat.Számláló + ", "; // számláló
                    szöveg += Adat.Státus + ", "; // státus 
                    szöveg += Adat.KM_Sorszám + ", "; // km_sorszám
                    szöveg += Adat.IDŐ_Sorszám + ", "; // idő_sorszám
                    szöveg += Adat.IDŐvKM + " , ";// idővKM
                    szöveg += "'" + Adat.Megjegyzés.Trim() + "', "; // megjegyzés
                    szöveg += " '" + Adat.Dátum_program.ToString("yyyy.MM.dd").Trim() + "') ";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "RögzítiMódosít", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void IDŐ_Eltervező_EgyKocsi(string pályaszám, DateTime Elő_Dátumig)
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";
                bool vége = true;
                string szöveg;

                Adat_CAF_Adatok AlapAdat;
                Adat_CAF_alap EgyCAF;
                List<Adat_Ciklus> Ciklus_Idő;
                Kezelő_Ciklus Kéz_Ciklus = new Kezelő_Ciklus();

                string helycik = Application.StartupPath + @"\Főmérnökség\adatok\ciklus.mdb";
                string jelszócik = "pocsaierzsi";


                szöveg = $"SELECT * FROM Alap WHERE azonosító='{pályaszám.Trim()}' AND törölt=false ";
                EgyCAF = KézAlap.Egy_Adat(hely, jelszó, szöveg);

                szöveg = $"SELECT * FROM adatok WHERE azonosító='{pályaszám.Trim()}' AND státus<9 ORDER BY dátum desc";
                AlapAdat = Kéz_Adatok.Egy_Adat(hely, jelszó, szöveg);

                szöveg = $"SELECT * FROM ciklusrendtábla WHERE  [törölt]='0' AND típus='{EgyCAF.Ciklusnap}' ORDER BY sorszám";
                Ciklus_Idő = Kéz_Ciklus.Lista_Adatok(helycik, jelszócik, szöveg);

                while (vége == true)
                {
                    //Jármű tulajdonsága
                    EgyCAF = Villamos_tulajdonság(pályaszám.Trim());
                    // utolsó ütemezett
                    Adat_CAF_Adatok Előző = Utolsó_ütemezett(pályaszám.Trim(), "");
                    // következő idő szerinti
                    Adat_CAF_Adatok Adat = Következő_Idő(Ciklus_Idő, Előző, EgyCAF);

                    // ha belefér az időbe akkor rögzítjük
                    if (Elő_Dátumig >= Adat.Dátum)
                        RögzítiMódosít(Adat, hely, jelszó);
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


        public static void KM_Eltervező_EgyKocsi(string pályaszám, DateTime Elő_Dátumig)
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";
                bool vége = true;
                string szöveg;



                Adat_CAF_Adatok AlapAdat;
                Adat_CAF_alap EgyCAF;

                string helycik = Application.StartupPath + @"\Főmérnökség\adatok\ciklus.mdb";
                string jelszócik = "pocsaierzsi";
                Kezelő_Ciklus Kéz_Ciklus = new Kezelő_Ciklus();
                List<Adat_Ciklus> Ciklus_Km;


                szöveg = $"SELECT * FROM Alap WHERE azonosító='{pályaszám.Trim()}' AND törölt=false ";
                EgyCAF = KézAlap.Egy_Adat(hely, jelszó, szöveg);

                szöveg = $"SELECT * FROM adatok WHERE azonosító='{pályaszám.Trim()}' AND státus<9 ORDER BY dátum desc";
                AlapAdat = Kéz_Adatok.Egy_Adat(hely, jelszó, szöveg);

                szöveg = $"SELECT * FROM ciklusrendtábla WHERE  [törölt]='0' AND típus='{EgyCAF.Cikluskm}' ORDER BY sorszám";
                Ciklus_Km = Kéz_Ciklus.Lista_Adatok(helycik, jelszócik, szöveg);

                while (vége == true)
                {
                    //Jármű tulajdonsága
                    EgyCAF = Villamos_tulajdonság(pályaszám.Trim());
                    // utolsó ütemezett
                    Adat_CAF_Adatok Előző = Utolsó_ütemezett(pályaszám.Trim(), "km");

                    // következő km szerinti
                    Adat_CAF_Adatok Adat = Következő_Km(Ciklus_Km, Előző, EgyCAF);
                    // ha belefér az időbe akkor rögzítjük
                    if (Elő_Dátumig >= Adat.Dátum)
                        RögzítiMódosít(Adat, hely, jelszó);
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

        public static void IDŐ_Átütemez(string hely, string jelszó, Adat_CAF_Adatok Adat, DateTime Dátumra, DateTime Elő_Dátumig)
        {
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
                RögzítiMódosít(Új_adat, hely, jelszó);

                // töröljük az új dátum utáni tervet
                string szöveg = $"SELECT * FROM adatok";
                List<Adat_CAF_Adatok> Adatok = Kéz_Adatok.Lista_Adatok(hely, jelszó, szöveg);
                Adat_CAF_Adatok Elem = (from a in Adatok
                                        where a.Azonosító == Adat.Azonosító.Trim()
                                        && a.Dátum > Dátumra
                                        && a.Státus == 0
                                        select a).FirstOrDefault();

                if (Elem != null)
                {
                    szöveg = $"DELETE  FROM adatok WHERE azonosító='{Adat.Azonosító.Trim()}' AND dátum>#";
                    szöveg += Dátumra.ToString("MM-dd-yyyy") + "# AND státus=0";
                    MyA.ABtörlés(hely, jelszó, szöveg);
                }
                // ütemezzük újra a kocsikat

                // idő szerit
                IDŐ_Eltervező_EgyKocsi(Adat.Azonosító.Trim(), Elő_Dátumig);

                // km szerint
                KM_Eltervező_EgyKocsi(Adat.Azonosító.Trim(), Elő_Dátumig);
            }

        }



    }
}


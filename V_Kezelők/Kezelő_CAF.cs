using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_CAF_alap
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
        readonly string jelszó = "CzabalayL";

        public Kezelő_CAF_alap()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.CAFtábla(hely.KönyvSzerk());
        }

        public List<Adat_CAF_alap> Lista_Adatok(bool Aktív = true)
        {
            string szöveg;
            if (Aktív)
                szöveg = "SELECT * FROM alap WHERE törölt=false ORDER BY azonosító";
            else
                szöveg = $"SELECT * FROM alap ORDER BY azonosító";

            List<Adat_CAF_alap> Adatok = new List<Adat_CAF_alap>();
            Adat_CAF_alap Adat;

            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";
            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                using (OleDbCommand Parancs = new OleDbCommand(szöveg, Kapcsolat))
                {
                    using (OleDbDataReader rekord = Parancs.ExecuteReader())
                    {
                        if (rekord.HasRows)
                        {
                            while (rekord.Read())
                            {
                                Adat = new Adat_CAF_alap(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Ciklusnap"].ToStrTrim(),
                                        rekord["Utolsó_Nap"].ToStrTrim(),
                                        rekord["Utolsó_Nap_sorszám"].ToÉrt_Long(),
                                        rekord["Végezte_nap"].ToStrTrim(),
                                        rekord["Vizsgdátum_nap"].ToÉrt_DaTeTime(),
                                        rekord["Cikluskm"].ToStrTrim(),
                                        rekord["Utolsó_Km"].ToStrTrim(),
                                        rekord["Utolsó_Km_sorszám"].ToÉrt_Long(),
                                        rekord["Végezte_km"].ToStrTrim(),
                                        rekord["Vizsgdátum_km"].ToÉrt_DaTeTime(),
                                        rekord["Számláló"].ToÉrt_Long(),
                                        rekord["havikm"].ToÉrt_Long(),
                                        rekord["KMUkm"].ToÉrt_Long(),
                                        rekord["KMUdátum"].ToÉrt_DaTeTime(),
                                        rekord["fudátum"].ToÉrt_DaTeTime(),
                                        rekord["Teljeskm"].ToÉrt_Long(),
                                        rekord["Típus"].ToStrTrim(),
                                        rekord["Garancia"].ToÉrt_Bool(),
                                        rekord["törölt"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_CAF_alap Egy_Adat(string Azonosító, bool Aktív = false)
        {
            Adat_CAF_alap Adat = null;
            try
            {
                List<Adat_CAF_alap> Adatok = Lista_Adatok(Aktív);
                if (Adatok.Count > 0) Adat = Adatok.FirstOrDefault(x => x.Azonosító == Azonosító);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Adat;
        }

        public List<Adat_CAF_alap> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_CAF_alap> Adatok = new List<Adat_CAF_alap>();
            Adat_CAF_alap Adat;

            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";
            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                using (OleDbCommand Parancs = new OleDbCommand(szöveg, Kapcsolat))
                {
                    using (OleDbDataReader rekord = Parancs.ExecuteReader())
                    {
                        if (rekord.HasRows)
                        {
                            while (rekord.Read())
                            {
                                Adat = new Adat_CAF_alap(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Ciklusnap"].ToStrTrim(),
                                        rekord["Utolsó_Nap"].ToStrTrim(),
                                        rekord["Utolsó_Nap_sorszám"].ToÉrt_Long(),
                                        rekord["Végezte_nap"].ToStrTrim(),
                                        rekord["Vizsgdátum_nap"].ToÉrt_DaTeTime(),
                                        rekord["Cikluskm"].ToStrTrim(),
                                        rekord["Utolsó_Km"].ToStrTrim(),
                                        rekord["Utolsó_Km_sorszám"].ToÉrt_Long(),
                                        rekord["Végezte_km"].ToStrTrim(),
                                        rekord["Vizsgdátum_km"].ToÉrt_DaTeTime(),
                                        rekord["Számláló"].ToÉrt_Long(),
                                        rekord["havikm"].ToÉrt_Long(),
                                        rekord["KMUkm"].ToÉrt_Long(),
                                        rekord["KMUdátum"].ToÉrt_DaTeTime(),
                                        rekord["fudátum"].ToÉrt_DaTeTime(),
                                        rekord["Teljeskm"].ToÉrt_Long(),
                                        rekord["Típus"].ToStrTrim(),
                                        rekord["Garancia"].ToÉrt_Bool(),
                                        rekord["törölt"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public void Rögzítés(Adat_CAF_alap Adat)
        {
            try
            {
                // új jármű
                string szöveg = "INSERT INTO alap (azonosító, Ciklusnap, Utolsó_Nap, Utolsó_Nap_sorszám, Végezte_nap, Vizsgdátum_nap, Cikluskm, Utolsó_Km,";
                szöveg += " Utolsó_Km_sorszám, Végezte_km, Vizsgdátum_km, számláló, havikm, KMUkm, KMUdátum, fudátum, Teljeskm, Garancia, törölt, típus ) VALUES (";
                szöveg += $"'{Adat.Azonosító}', "; // azonosító
                szöveg += $"'{Adat.Ciklusnap}', "; // Ciklusnap
                szöveg += $"'{Adat.Utolsó_Nap}', "; // Utolsó_Nap
                szöveg += $"{Adat.Utolsó_Nap_sorszám}, "; // Utolsó_Nap_sorszám
                szöveg += $"'{Adat.Végezte_nap}', "; // Végezte_nap
                szöveg += $"'{Adat.Vizsgdátum_nap:MM-dd-yyyy}', "; // Vizsgdátum_nap
                szöveg += $"'{Adat.Cikluskm}', "; // Cikluskm
                szöveg += $"'{Adat.Utolsó_Km}', ";  // Utolsó_Km
                szöveg += $"{Adat.Utolsó_Km_sorszám}, "; // Utolsó_Km_sorszám
                szöveg += $"'{Adat.Végezte_km}', ";  // Végezte_km
                szöveg += $"'{Adat.Vizsgdátum_km}', ";// Vizsgdátum_km
                szöveg += $"{Adat.Számláló}, "; // számláló,
                szöveg += $"{Adat.Havikm}, "; // havikm,
                szöveg += $"{Adat.KMUkm}, ";  // KMUkm
                szöveg += $"'{Adat.KMUdátum:MM-dd-yyyy}', "; // KMUdátum,
                szöveg += $"'{Adat.Fudátum:MM-dd-yyyy}', "; // fudátum
                szöveg += $"{Adat.Teljeskm}, "; // Teljeskm
                szöveg += $"{Adat.Garancia}, ";     // Garancia
                szöveg += $"{Adat.Törölt}, ";     // törölt
                szöveg += $"'{Adat.Típus}')";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosítás(Adat_CAF_alap Adat)
        {
            try
            {
                string szöveg = "UPDATE alap  SET ";
                szöveg += $"Ciklusnap='{Adat.Ciklusnap}', "; // Ciklusnap
                szöveg += $"Utolsó_Nap='{Adat.Utolsó_Nap}', "; // Utolsó_Nap
                szöveg += $"Utolsó_Nap_sorszám={Adat.Utolsó_Nap_sorszám}, "; // Utolsó_Nap_sorszám
                szöveg += $"Végezte_nap='{Adat.Végezte_nap}', "; // Végezte_nap
                szöveg += $"Vizsgdátum_nap='{Adat.Vizsgdátum_nap:MM-dd-yyyy}', "; // Vizsgdátum_nap
                szöveg += $"Cikluskm='{Adat.Cikluskm}', "; // Cikluskm
                szöveg += $"Utolsó_Km='{Adat.Utolsó_Km}', ";  // Utolsó_Km
                szöveg += $"Utolsó_Km_sorszám={Adat.Utolsó_Km_sorszám}, "; // Utolsó_Km_sorszám
                szöveg += $"Végezte_km='{Adat.Végezte_km}', "; // Végezte_km
                szöveg += $"Vizsgdátum_km='{Adat.Vizsgdátum_km:MM-dd-yyyy}', "; // Vizsgdátum_km
                szöveg += $"számláló={Adat.Számláló}, "; // számláló,
                szöveg += $"havikm={Adat.Havikm}, "; // havikm,
                szöveg += $"KMUkm={Adat.KMUkm}, ";  // KMUkm
                szöveg += $"KMUdátum='{Adat.KMUdátum:MM-dd-yyyy}', "; // KMUdátum,
                szöveg += $"fudátum='{Adat.Fudátum:MM-dd-yyyy}', ";  // fudátum
                szöveg += $"Teljeskm={Adat.Teljeskm}, "; // Teljeskm
                szöveg += $"Garancia={Adat.Garancia}, ";  // Garancia
                szöveg += $"törölt={Adat.Törölt}, ";// törölt
                szöveg += $"típus='{Adat.Típus} '"; // típus
                szöveg += $" WHERE azonosító='{Adat.Azonosító}'";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosítás_kmAdat(List<Adat_CAF_alap> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_CAF_alap Adat in Adatok)
                {
                    string szöveg = "UPDATE alap  SET ";
                    szöveg += $"havikm={Adat.Havikm}, "; // havikm,
                    szöveg += $"KMUkm={Adat.KMUkm}, ";  // KMUkm
                    szöveg += $"KMUdátum='{Adat.KMUdátum:MM-dd-yyyy}' "; // KMUdátum,
                    szöveg += $" WHERE azonosító='{Adat.Azonosító}'";
                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosítás_Kész_Idő(Adat_CAF_alap Adat)
        {
            try
            {
                string szöveg = "UPDATE alap  SET ";
                szöveg += $" Utolsó_nap='{Adat.Utolsó_Nap}', "; // vizsgálatot kapott
                szöveg += $" Utolsó_Nap_sorszám={Adat.Utolsó_Nap_sorszám}, ";  // vizsgálat sorszáma
                szöveg += $" Vizsgdátum_nap='{Adat.Vizsgdátum_nap:yyyy.MM.dd}', ";
                szöveg += $" Végezte_nap='{Adat.Végezte_nap}' ";
                szöveg += $" WHERE azonosító='{Adat.Azonosító}'";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosítás_Kész_Km(Adat_CAF_alap Adat)
        {
            try
            {
                string szöveg = "UPDATE alap  SET ";
                szöveg += $" Utolsó_km='{Adat.Utolsó_Km}', "; // vizsgálatot kapott
                szöveg += $" Utolsó_km_sorszám={Adat.Utolsó_Km_sorszám}, ";  // vizsgálat sorszáma
                szöveg += $" Vizsgdátum_km='{Adat.Vizsgdátum_km:yyyy.MM.dd}', ";
                szöveg += $" Végezte_km='{Adat.Végezte_km}', ";
                szöveg += $" számláló={Adat.Számláló}";
                szöveg += $" WHERE azonosító='{Adat.Azonosító}'";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


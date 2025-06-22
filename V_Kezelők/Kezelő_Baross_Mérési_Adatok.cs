using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Baross_Mérési_Adatok
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Baross_Mérés.mdb";
        readonly string jelszó = "RónaiSándor";
        readonly string táblanév = "mérés";

        public Kezelő_Baross_Mérési_Adatok()
        {
            if (!Exists(hely)) Adatbázis_Létrehozás.Kerék_Baross_Mérési_Adatok(hely.KönyvSzerk());
        }

        public List<Adat_Baross_Mérési_Adatok> Lista_Adatok()
        {
            List<Adat_Baross_Mérési_Adatok> Adatok = new List<Adat_Baross_Mérési_Adatok>();
            string szöveg = $"SELECT * FROM {táblanév}";

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
                                Adat_Baross_Mérési_Adatok Adat = new Adat_Baross_Mérési_Adatok(
                                        rekord["Dátum_1"].ToÉrt_DaTeTime(),
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Tulajdonos"].ToStrTrim(),
                                        rekord["kezelő"].ToStrTrim(),
                                        rekord["Profil"].ToStrTrim(),
                                        rekord["Profil_szám"].ToÉrt_Long(),
                                        rekord["Kerékpár_szám"].ToStrTrim(),
                                        rekord["Adat_1"].ToStrTrim(),
                                        rekord["Adat_2"].ToStrTrim(),
                                        rekord["Adat_3"].ToStrTrim(),
                                        rekord["Típus_Eszt"].ToStrTrim(),
                                        rekord["KMU"].ToÉrt_Long(),
                                        rekord["Pozíció_Eszt"].ToÉrt_Int(),
                                        rekord["Tengely_Aznosító"].ToStrTrim(),
                                        rekord["Adat_4"].ToStrTrim(),
                                        rekord["Dátum_2"].ToÉrt_DaTeTime(),
                                        rekord["Táv_Belső_Futó_K"].ToÉrt_Double(),
                                        rekord["Táv_Nyom_K"].ToÉrt_Double(),
                                        rekord["Delta_K"].ToÉrt_Double(),
                                        rekord["B_Átmérő_K"].ToÉrt_Double(),
                                        rekord["J_Átmérő_K"].ToÉrt_Double(),
                                        rekord["B_Axiális_K"].ToÉrt_Double(),
                                        rekord["J_Axiális_K"].ToÉrt_Double(),
                                        rekord["B_Radiális_K"].ToÉrt_Double(),
                                        rekord["J_Radiális_K"].ToÉrt_Double(),
                                        rekord["B_Nyom_Mag_K"].ToÉrt_Double(),
                                        rekord["J_Nyom_Mag_K"].ToÉrt_Double(),
                                        rekord["B_Nyom_Vast_K"].ToÉrt_Double(),
                                        rekord["J_nyom_Vast_K"].ToÉrt_Double(),
                                        rekord["B_Nyom_Vast_B_K"].ToÉrt_Double(),
                                        rekord["J_nyom_Vast_B_K"].ToÉrt_Double(),
                                        rekord["B_QR_K"].ToÉrt_Double(),
                                        rekord["J_QR_K"].ToÉrt_Double(),
                                        rekord["B_Profilhossz_K"].ToÉrt_Double(),
                                        rekord["J_Profilhossz_K"].ToÉrt_Double(),
                                        rekord["Dátum_3"].ToÉrt_DaTeTime(),
                                        rekord["Táv_Belső_Futó_Ú"].ToÉrt_Double(),
                                        rekord["Táv_Nyom_Ú"].ToÉrt_Double(),
                                        rekord["Delta_Ú"].ToÉrt_Double(),
                                        rekord["B_Átmérő_Ú"].ToÉrt_Double(),
                                        rekord["J_Átmérő_Ú"].ToÉrt_Double(),
                                        rekord["B_Axiális_Ú"].ToÉrt_Double(),
                                        rekord["J_Axiális_Ú"].ToÉrt_Double(),
                                        rekord["B_Radiális_Ú"].ToÉrt_Double(),
                                        rekord["J_Radiális_Ú"].ToÉrt_Double(),
                                        rekord["B_Nyom_Mag_Ú"].ToÉrt_Double(),
                                        rekord["J_Nyom_Mag_Ú"].ToÉrt_Double(),
                                        rekord["B_Nyom_Vast_Ú"].ToÉrt_Double(),
                                        rekord["J_nyom_Vast_Ú"].ToÉrt_Double(),
                                        rekord["B_Nyom_Vast_B_Ú"].ToÉrt_Double(),
                                        rekord["J_nyom_Vast_B_Ú"].ToÉrt_Double(),
                                        rekord["B_QR_Ú"].ToÉrt_Double(),
                                        rekord["J_QR_Ú"].ToÉrt_Double(),
                                        rekord["B_Szög_Ú"].ToÉrt_Double(),
                                        rekord["J_Szög_Ú"].ToÉrt_Double(),
                                        rekord["B_Profilhossz_Ú"].ToÉrt_Double(),
                                        rekord["J_Profilhossz_Ú"].ToÉrt_Double(),
                                        rekord["Eszterga_Id"].ToÉrt_Long(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["Státus"].ToÉrt_Int()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(Adat_Baross_Mérési_Adatok Adat)
        {
            try
            {
                string szöveg = $"UPDATE mérés SET ";
                szöveg += $" Azonosító='{Adat.Azonosító.Trim()}' ,";
                szöveg += $" Kerékpár_szám='{Adat.Kerékpár_szám.Trim()}', ";
                szöveg += $" Típus_Eszt='{Adat.Típus_Eszt.Trim()}', ";
                szöveg += $" Pozíció_Eszt={Adat.Pozíció_Eszt}, ";
                szöveg += $" Megjegyzés='{Adat.Megjegyzés.Trim()}' ";
                szöveg += $" WHERE Eszterga_Id={Adat.Eszterga_Id}";
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

        /// <summary>
        /// Státus módosítása azonosító alapján
        /// </summary>
        /// <param name="Adat"></param>
        public void Módosítás(List<Adat_Baross_Mérési_Adatok> Adatok)
        {
            try
            {
                List<string> SzövegGY = new List<string>();
                foreach (Adat_Baross_Mérési_Adatok Adat in Adatok)
                {
                    string szöveg = $"UPDATE mérés SET ";
                    szöveg += $" Státus={Adat.Státus} ";
                    szöveg += $" WHERE Eszterga_Id={Adat.Eszterga_Id}";
                    SzövegGY.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGY);
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

        /// <summary>
        /// Státus és megjegyzés módosítása azonosító alapján
        /// </summary>
        /// <param name="Adatok"></param>
        public void MódosításMeg(List<Adat_Baross_Mérési_Adatok> Adatok)
        {
            try
            {
                List<string> SzövegGY = new List<string>();
                foreach (Adat_Baross_Mérési_Adatok Adat in Adatok)
                {
                    string szöveg = $"UPDATE mérés SET Státus={Adat.Státus}, ";
                    szöveg += $" Megjegyzés='{Adat.Megjegyzés}'";
                    szöveg += $" WHERE Eszterga_Id={Adat.Eszterga_Id}";
                    SzövegGY.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGY);
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

        public void Törlés(List<long> Idk)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (long ID in Idk)
                {
                    string szöveg = $"DELETE FROM mérés WHERE Eszterga_Id={ID}";
                    SzövegGy.Add(szöveg);
                }
                MyA.ABtörlés(hely, jelszó, SzövegGy);
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

        public void Rögzítés(List<string> SzövegVáltok, List<string> Megjegyzések)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                for (int i = 0; i < SzövegVáltok.Count; i++)
                {
                    string szöveg = "INSERT INTO mérés (Dátum_1, Azonosító, Tulajdonos, kezelő, Profil, Profil_szám, Kerékpár_szám, Adat_1, Adat_2,";
                    szöveg += " Adat_3, Típus_Eszt, KMU, Pozíció_Eszt, Tengely_Aznosító, Adat_4, Dátum_2, Táv_Belső_Futó_K, Táv_Nyom_K, Delta_K,";
                    szöveg += " B_Átmérő_K, J_Átmérő_K, B_Axiális_K, J_Axiális_K, B_Radiális_K, J_Radiális_K, B_Nyom_Mag_K, J_Nyom_Mag_K, B_Nyom_Vast_K,";
                    szöveg += " J_nyom_Vast_K, B_Nyom_Vast_B_K, J_nyom_Vast_B_K, B_QR_K, J_QR_K, B_Profilhossz_K, J_Profilhossz_K, Dátum_3, Táv_Belső_Futó_Ú,";
                    szöveg += " Táv_Nyom_Ú, Delta_Ú, B_Átmérő_Ú, J_Átmérő_Ú, B_Axiális_Ú, J_Axiális_Ú, B_Radiális_Ú, J_Radiális_Ú, B_Nyom_Mag_Ú, J_Nyom_Mag_Ú,";
                    szöveg += " B_Nyom_Vast_Ú, J_nyom_Vast_Ú, B_Nyom_Vast_B_Ú, J_nyom_Vast_B_Ú, B_QR_Ú, J_QR_Ú, B_Szög_Ú, J_Szög_Ú, B_Profilhossz_Ú,";
                    szöveg += $" J_Profilhossz_Ú, Eszterga_Id, Megjegyzés, Státus) Values ({SzövegVáltok[i]} '{Megjegyzések[i]}', 1)";
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
        //Elkopó
        public List<Adat_Baross_Mérési_Adatok> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Baross_Mérési_Adatok> Adatok = new List<Adat_Baross_Mérési_Adatok>();
            Adat_Baross_Mérési_Adatok Adat;

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
                                Adat = new Adat_Baross_Mérési_Adatok(
                                        rekord["Dátum_1"].ToÉrt_DaTeTime(),
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Tulajdonos"].ToStrTrim(),
                                        rekord["kezelő"].ToStrTrim(),
                                        rekord["Profil"].ToStrTrim(),
                                        rekord["Profil_szám"].ToÉrt_Long(),
                                        rekord["Kerékpár_szám"].ToStrTrim(),
                                        rekord["Adat_1"].ToStrTrim(),
                                        rekord["Adat_2"].ToStrTrim(),
                                        rekord["Adat_3"].ToStrTrim(),
                                        rekord["Típus_Eszt"].ToStrTrim(),
                                        rekord["KMU"].ToÉrt_Long(),
                                        rekord["Pozíció_Eszt"].ToÉrt_Int(),
                                        rekord["Tengely_Aznosító"].ToStrTrim(),
                                        rekord["Adat_4"].ToStrTrim(),
                                        rekord["Dátum_2"].ToÉrt_DaTeTime(),
                                        rekord["Táv_Belső_Futó_K"].ToÉrt_Double(),
                                        rekord["Táv_Nyom_K"].ToÉrt_Double(),
                                        rekord["Delta_K"].ToÉrt_Double(),
                                        rekord["B_Átmérő_K"].ToÉrt_Double(),
                                        rekord["J_Átmérő_K"].ToÉrt_Double(),
                                        rekord["B_Axiális_K"].ToÉrt_Double(),
                                        rekord["J_Axiális_K"].ToÉrt_Double(),
                                        rekord["B_Radiális_K"].ToÉrt_Double(),
                                        rekord["J_Radiális_K"].ToÉrt_Double(),
                                        rekord["B_Nyom_Mag_K"].ToÉrt_Double(),
                                        rekord["J_Nyom_Mag_K"].ToÉrt_Double(),
                                        rekord["B_Nyom_Vast_K"].ToÉrt_Double(),
                                        rekord["J_nyom_Vast_K"].ToÉrt_Double(),
                                        rekord["B_Nyom_Vast_B_K"].ToÉrt_Double(),
                                        rekord["J_nyom_Vast_B_K"].ToÉrt_Double(),
                                        rekord["B_QR_K"].ToÉrt_Double(),
                                        rekord["J_QR_K"].ToÉrt_Double(),
                                        rekord["B_Profilhossz_K"].ToÉrt_Double(),
                                        rekord["J_Profilhossz_K"].ToÉrt_Double(),
                                        rekord["Dátum_3"].ToÉrt_DaTeTime(),
                                        rekord["Táv_Belső_Futó_Ú"].ToÉrt_Double(),
                                        rekord["Táv_Nyom_Ú"].ToÉrt_Double(),
                                        rekord["Delta_Ú"].ToÉrt_Double(),
                                        rekord["B_Átmérő_Ú"].ToÉrt_Double(),
                                        rekord["J_Átmérő_Ú"].ToÉrt_Double(),
                                        rekord["B_Axiális_Ú"].ToÉrt_Double(),
                                        rekord["J_Axiális_Ú"].ToÉrt_Double(),
                                        rekord["B_Radiális_Ú"].ToÉrt_Double(),
                                        rekord["J_Radiális_Ú"].ToÉrt_Double(),
                                        rekord["B_Nyom_Mag_Ú"].ToÉrt_Double(),
                                        rekord["J_Nyom_Mag_Ú"].ToÉrt_Double(),
                                        rekord["B_Nyom_Vast_Ú"].ToÉrt_Double(),
                                        rekord["J_nyom_Vast_Ú"].ToÉrt_Double(),
                                        rekord["B_Nyom_Vast_B_Ú"].ToÉrt_Double(),
                                        rekord["J_nyom_Vast_B_Ú"].ToÉrt_Double(),
                                        rekord["B_QR_Ú"].ToÉrt_Double(),
                                        rekord["J_QR_Ú"].ToÉrt_Double(),
                                        rekord["B_Szög_Ú"].ToÉrt_Double(),
                                        rekord["J_Szög_Ú"].ToÉrt_Double(),
                                        rekord["B_Profilhossz_Ú"].ToÉrt_Double(),
                                        rekord["J_Profilhossz_Ú"].ToÉrt_Double(),
                                        rekord["Eszterga_Id"].ToÉrt_Long(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["Státus"].ToÉrt_Int()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }
}

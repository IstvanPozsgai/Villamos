using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Baross_Mérési_Adatok
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Baross_Mérés.mdb";
        readonly string jelszó = "RónaiSándor";
        readonly string táblanév = "mérés";


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

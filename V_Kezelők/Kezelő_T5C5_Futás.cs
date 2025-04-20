using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_T5C5_Futás
    {
        readonly string jelszó = "lilaakác";
        string hely;

        private void FájlBeállítás(string Telephely, DateTime Dátum)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\főkönyv\futás\{Dátum.Year}\futás{Dátum:yyyyMMdd}nap.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Futásnapalap(hely.KönyvSzerk());
        }

        public List<Adat_T5C5_Futás> Lista_Adatok(string Telephely, DateTime Dátum)
        {
            FájlBeállítás(Telephely, Dátum);
            string szöveg = "SELECT * FROM futástábla order by azonosító";
            List<Adat_T5C5_Futás> Adatok = new List<Adat_T5C5_Futás>();
            Adat_T5C5_Futás Adat;

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
                                Adat = new Adat_T5C5_Futás(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Dátum"].ToÉrt_DaTeTime(),
                                    rekord["Futásstátus"].ToStrTrim(),
                                    rekord["Státus"].ToÉrt_Long()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Törlés(string Telephely, DateTime Dátum)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);
                MyA.ABtörlés(hely, jelszó, "DELETE * FROM Futástábla");
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

        public void Rögzítés(string Telephely, DateTime Dátum, List<Adat_T5C5_Futás> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_T5C5_Futás Adat in Adatok)
                {
                    string szöveg = "INSERT INTO Futástábla (azonosító, Dátum, Futásstátus, Státus) VALUES ( ";
                    szöveg += $"'{Adat.Azonosító}', '{Adat.Dátum:yyyy.MM.dd}', '{Adat.Futásstátus}', {Adat.Státus})";
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

        public void Módosítás(string Telephely, DateTime Dátum, List<Adat_T5C5_Futás> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_T5C5_Futás Adat in Adatok)
                {
                    string szöveg = "UPDATE futástábla SET ";
                    szöveg += $" dátum='{Adat.Dátum:yyyy.MM.dd}', ";
                    szöveg += $" Futásstátus='{Adat.Futásstátus}', ";
                    szöveg += $" státus={Adat.Státus} ";
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



        //Elkopó
        public List<Adat_T5C5_Futás> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_T5C5_Futás> Adatok = new List<Adat_T5C5_Futás>();
            Adat_T5C5_Futás Adat;

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
                                Adat = new Adat_T5C5_Futás(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Dátum"].ToÉrt_DaTeTime(),
                                    rekord["Futásstátus"].ToStrTrim(),
                                    rekord["Státus"].ToÉrt_Long()
                                    ); ;
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

using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Váltós_Naptár
    {
        readonly string jelszó = "katalin";
        readonly string táblanév = "Naptár";
        string hely;

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Év}\munkaidőnaptár.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Nappalosmunkarendlétrehozás(hely.KönyvSzerk());
        }

        public List<Adat_Váltós_Naptár> Lista_Adatok(int Év, string Tábla)
        {
            FájlBeállítás(Év);
            string szöveg = $"SELECT * FROM {táblanév}{Tábla} ORDER BY Dátum ";
            List<Adat_Váltós_Naptár> Adatok = new List<Adat_Váltós_Naptár>();
            Adat_Váltós_Naptár Adat;

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
                                Adat = new Adat_Váltós_Naptár(
                                          rekord["Nap"].ToStrTrim(),
                                          rekord["Dátum"].ToÉrt_DaTeTime()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(int Év, string Tábla, Adat_Váltós_Naptár Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = $"INSERT INTO {táblanév}{Tábla} (nap, dátum) VALUES (";
                szöveg += $"'{Adat.Nap}', ";
                szöveg += $"'{Adat.Dátum:yyyy.MM.dd}' )";
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

        public void Módosítás(int Év, string Tábla, Adat_Váltós_Naptár Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = $"UPDATE  {táblanév}{Tábla} SET ";
                szöveg += $" nap='{Adat.Nap}'";
                szöveg += $" WHERE dátum='#{Adat.Dátum:M-d-yy}#'";
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

        public void Rögzítés(int Év, string Tábla, List<Adat_Váltós_Naptár> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Váltós_Naptár Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév}{Tábla} (nap, dátum) VALUES (";
                    szöveg += $"'{Adat.Nap}', ";
                    szöveg += $"'{Adat.Dátum:yyyy.MM.dd}' )";
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

        public void Módosítás(int Év, string Tábla, List<Adat_Váltós_Naptár> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Váltós_Naptár Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév}{Tábla} SET ";
                    szöveg += $" nap='{Adat.Nap}'";
                    szöveg += $" WHERE dátum= #{Adat.Dátum:yyyy-MM-dd}#";
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
    }
}

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
    public class Kezelő_Váltós_Összesítő
    {
        readonly string jelszó = "katalin";
        string hely;

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Év}\munkaidőnaptár.mdb".KönyvSzerk();
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Nappalosmunkarendlétrehozás(hely.KönyvSzerk());
        }

        public List<Adat_Váltós_Összesítő> Lista_Adatok(int Év, string Tábla)
        {
            FájlBeállítás(Év);
            string szöveg = $"SELECT * FROM összesítő{Tábla}";
            List<Adat_Váltós_Összesítő> Adatok = new List<Adat_Váltós_Összesítő>();
            Adat_Váltós_Összesítő Adat;

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
                                Adat = new Adat_Váltós_Összesítő(
                                          Tábla,
                                          rekord["Perc"].ToÉrt_Long(),
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

        public void Rögzítés(int Év, string Tábla, Adat_Váltós_Összesítő Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Év}\munkaidőnaptár.mdb".KönyvSzerk();
                string szöveg = $"INSERT INTO összesítő{Tábla} (perc, dátum) VALUES (";
                szöveg += $"{Adat.Perc}, ";
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

        public void Módosítás(int Év, string Tábla, Adat_Váltós_Összesítő Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Év}\munkaidőnaptár.mdb".KönyvSzerk();
                string szöveg = $"UPDATE összesítő{Tábla} SET ";
                szöveg += $" perc={Adat.Perc} ";
                szöveg += $" WHERE dátum='{Adat.Dátum:M-d-yy}'";
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

        public void Rögzítés(int Év, string Tábla, List<Adat_Váltós_Összesítő> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Váltós_Összesítő Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO összesítő{Tábla} (perc, dátum) VALUES (";
                    szöveg += $"{Adat.Perc}, ";
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

        public void Módosítás(int Év, string Tábla, List<Adat_Váltós_Összesítő> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Váltós_Összesítő Adat in Adatok)
                {
                    string szöveg = $"UPDATE  összesítő{Tábla} SET ";
                    szöveg += $" perc={Adat.Perc} ";
                    szöveg += $" WHERE dátum=#{Adat.Dátum:M-d-yy}#";
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

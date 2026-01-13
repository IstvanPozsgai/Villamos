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
    public class Kezelő_Ciklus
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\ciklus.mdb";
        readonly string jelszó = "pocsaierzsi";
        readonly string táblanév = "ciklusrendtábla";

        public Kezelő_Ciklus()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Ciklusrendtábla(hely.KönyvSzerk());
        }

        public List<Adat_Ciklus> Lista_Adatok(bool Aktív = false)
        {
            string szöveg;
            if (Aktív)
                szöveg = $"SELECT * FROM {táblanév} WHERE [Törölt]='0' ORDER BY sorszám";
            else
                szöveg = $"SELECT * FROM {táblanév} ORDER BY sorszám";
            List<Adat_Ciklus> Adatok = new List<Adat_Ciklus>();
            Adat_Ciklus Adat;

            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";
            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                using (OleDbCommand Parancs = new OleDbCommand(szöveg, Kapcsolat))
                {
                    Kapcsolat.Open();
                    using (OleDbDataReader rekord = Parancs.ExecuteReader())
                    {
                        if (rekord.HasRows)
                        {
                            while (rekord.Read())
                            {
                                Adat = new Adat_Ciklus(
                                        rekord["Típus"].ToStrTrim(),
                                        rekord["Sorszám"].ToÉrt_Long(),
                                        rekord["Vizsgálatfok"].ToStrTrim(),
                                        rekord["Törölt"].ToStrTrim(),
                                        rekord["Névleges"].ToÉrt_Long(),
                                        rekord["Alsóérték"].ToÉrt_Long(),
                                        rekord["Felsőérték"].ToÉrt_Long()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Ciklus Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} (Típus, Sorszám, Vizsgálatfok, Törölt, névleges, alsóérték, felsőérték) VALUES (";
                szöveg += $"'{Adat.Típus}', ";
                szöveg += $"{Adat.Sorszám}, ";
                szöveg += $"'{Adat.Vizsgálatfok}', ";
                szöveg += $"'{Adat.Törölt}', ";
                szöveg += $"{Adat.Névleges}, ";
                szöveg += $"{Adat.Alsóérték}, ";
                szöveg += $"{Adat.Felsőérték}) ";
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

        public void Rögzítés(List<Adat_Ciklus> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Ciklus Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév} (Típus, Sorszám, Vizsgálatfok, Törölt, névleges, alsóérték, felsőérték) VALUES (";
                    szöveg += $"'{Adat.Típus}', ";
                    szöveg += $"{Adat.Sorszám}, ";
                    szöveg += $"'{Adat.Vizsgálatfok}', ";
                    szöveg += $"'{Adat.Törölt}', ";
                    szöveg += $"{Adat.Névleges}, ";
                    szöveg += $"{Adat.Alsóérték}, ";
                    szöveg += $"{Adat.Felsőérték}) ";
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

        public void Módosítás(Adat_Ciklus Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET  ";
                szöveg += $" vizsgálatfok='{Adat.Vizsgálatfok}', ";
                szöveg += $" Névleges={Adat.Névleges}, ";
                szöveg += $" alsóérték={Adat.Alsóérték}, ";
                szöveg += $" felsőérték={Adat.Felsőérték} ";
                szöveg += $" WHERE [típus]='{Adat.Típus}' AND [sorszám]={Adat.Sorszám} AND [törölt]='{Adat.Törölt}'";
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

        public void Módosítás_CAF(long kmErtek, int turesHatar)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET " +
                                $"Névleges={kmErtek}, " +
                                $"alsóérték={kmErtek * (1 - turesHatar / 100.0)}, " +
                                $"felsőérték={kmErtek * (1 + turesHatar / 100.0)} " +
                                $"WHERE [típus]='CAF_km'";
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

        public void Töröl(Adat_Ciklus Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET  ";
                szöveg += $" törölt='1' ";
                szöveg += $" WHERE [típus]='{Adat.Típus}' AND [sorszám]={Adat.Sorszám} AND [törölt]='{Adat.Törölt}'";
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

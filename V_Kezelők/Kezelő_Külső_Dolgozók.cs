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
    public class Kezelő_Külső_Dolgozók
    {
        readonly string táblanév = "Dolgozók";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Behajtási\Külső_adatok.mdb";
        readonly string jelszó = "Janda";

        public Kezelő_Külső_Dolgozók()
        {
            FájlBeállítás();
        }

        private void FájlBeállítás()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Külsős_Táblák(hely);
        }

        public List<Adat_Külső_Dolgozók> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM Dolgozók";
            List<Adat_Külső_Dolgozók> Adatok = new List<Adat_Külső_Dolgozók>();
            Adat_Külső_Dolgozók Adat;

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
                                Adat = new Adat_Külső_Dolgozók(
                                        rekord["Id"].ToÉrt_Double(),
                                        rekord["Név"].ToStrTrim(),
                                        rekord["Okmányszám"].ToStrTrim(),
                                        rekord["Anyjaneve"].ToStrTrim(),
                                        rekord["Születésihely"].ToStrTrim(),
                                        rekord["Születésiidő"].ToÉrt_DaTeTime(),
                                        rekord["Cégid"].ToÉrt_Double(),
                                        rekord["Státus"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Külső_Dolgozók Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} (id, név, okmányszám, anyjaneve, születésihely, születésiidő, cégid, státus) VALUES (";
                szöveg += $"{Adat.Id}, "; // id X
                szöveg += $"'{Adat.Név}', "; // név X
                szöveg += $"'{Adat.Okmányszám}', "; // okmányszám
                szöveg += "'_', "; // anyjaneve X
                szöveg += "'_', "; // születésihely
                szöveg += $"'{new DateTime(1900, 1, 1):yyyy.MM.dd}', "; //születésiidő
                szöveg += $"{Adat.Cégid}, "; // cégid X
                szöveg += $" {Adat.Státus}) ";
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

        public void Rögzítés(List<Adat_Külső_Dolgozók> Adatok)
        {
            try
            {
                List<string> szövegGy = new List<string>();
                foreach (Adat_Külső_Dolgozók Adat in Adatok)
                {
                    List<Adat_Külső_Dolgozók> ListAdatok = Lista_Adatok();
                    double id = ListAdatok.Any() ? ListAdatok.Max(a => a.Id) + 1 : 1;

                    string szöveg = $"INSERT INTO {táblanév} (id, név, okmányszám, anyjaneve, születésihely, születésiidő, cégid, státus) VALUES (";
                    szöveg += $"{id}, "; // id X
                    szöveg += $"'{Adat.Név}', "; // név X
                    szöveg += $"'{Adat.Okmányszám}', "; // okmányszám
                    szöveg += "'_', "; // anyjaneve X
                    szöveg += "'_', "; // születésihely
                    szöveg += $"'{new DateTime(1900, 1, 1):yyyy.MM.dd}', "; //születésiidő
                    szöveg += $"{Adat.Cégid}, "; // cégid X
                    szöveg += $" {Adat.Státus}) ";
                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);
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

        public void Módosítás(Adat_Külső_Dolgozók Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"okmányszám='{Adat.Okmányszám}', "; // okmányszám
                szöveg += $" státus={Adat.Státus} ";
                szöveg += $" WHERE Cégid={Adat.Cégid} AND név='{Adat.Név}'";
                szöveg += $" AND okmányszám='{Adat.Okmányszám}'";
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

        public void Módosítás(List<Adat_Külső_Dolgozók> Adatok)
        {
            try
            {
                List<string> szövegGy = new List<string>();
                foreach (Adat_Külső_Dolgozók Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév} SET ";
                    szöveg += $"okmányszám='{Adat.Okmányszám}', "; // okmányszám
                    szöveg += $" státus={Adat.Státus} ";
                    szöveg += $" WHERE Cégid={Adat.Cégid} AND név='{Adat.Név}'";
                    szöveg += $" AND okmányszám='{Adat.Okmányszám}'";
                    szövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);
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

        public void Törlés(List<Adat_Külső_Dolgozók> Adatok)
        {
            try
            {
                List<string> szövegGy = new List<string>();
                foreach (Adat_Külső_Dolgozók Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév} SET ";
                    szöveg += $"státus={Adat.Státus} ";
                    szöveg += $" WHERE id={Adat.Id}";
                    szövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);
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

        public void Döntés(Adat_Külső_Dolgozók Adat)
        {
            try
            {
                List<Adat_Külső_Dolgozók> Adatok = Lista_Adatok();
                double id = Adatok.Any() ? Adatok.Max(a => a.Id) + 1 : 1;

                bool vane = Adatok.Any(a => a.Cégid == Adat.Cégid && a.Név.Trim() == Adat.Név.Trim() && a.Okmányszám.Trim() == Adat.Okmányszám.Trim());

                if (vane)
                {
                    Módosítás(Adat);
                }
                else
                {
                    Adat_Külső_Dolgozók ADAT = new Adat_Külső_Dolgozók(
                        id,
                        Adat.Név,
                        Adat.Okmányszám,
                        Adat.Cégid,
                        true);
                    Rögzítés(ADAT);
                }

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

        public void Döntés(List<Adat_Külső_Dolgozók> ADATOK)
        {
            try
            {
                List<Adat_Külső_Dolgozók> Adatok = Lista_Adatok();
                double id = Adatok.Any() ? Adatok.Max(a => a.Id) + 1 : 1;


                List<Adat_Külső_Dolgozók> AdatokM = new List<Adat_Külső_Dolgozók>();
                List<Adat_Külső_Dolgozók> AdatokR = new List<Adat_Külső_Dolgozók>();
                foreach (Adat_Külső_Dolgozók Adat in ADATOK)
                {
                    bool vane = Adatok.Any(a => a.Cégid == Adat.Cégid && a.Név.Trim() == Adat.Név.Trim() && a.Okmányszám.Trim() == Adat.Okmányszám.Trim());

                    if (vane)
                    {
                        AdatokM.Add(Adat);
                    }
                    else
                    {
                        AdatokR.Add(Adat);
                    }
                }
                if (AdatokM.Count > 0) Módosítás(AdatokM);
                if (AdatokR.Count > 0) Rögzítés(AdatokR);
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
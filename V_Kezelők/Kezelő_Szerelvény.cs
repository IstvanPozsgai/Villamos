using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Szerelvény
    {
        readonly string jelszó = "pozsgaii";
        string hely;

        private void FájlBeállítás(string Telephely, bool előírt = false)
        {
            if (előírt)
                hely = $@"{Application.StartupPath}\{Telephely}\Adatok\villamos\szerelvényelőírt.mdb";
            else
                hely = $@"{Application.StartupPath}\{Telephely}\Adatok\villamos\szerelvény.mdb";

            if (!File.Exists(hely)) Adatbázis_Létrehozás.Szerelvénytáblalap(hely.KönyvSzerk());
        }

        public List<Adat_Szerelvény> Lista_Adatok(string Telephely, bool előírt = false)
        {
            FájlBeállítás(Telephely, előírt);
            string szöveg = "Select * FROM szerelvénytábla ORDER BY kocsi1";
            List<Adat_Szerelvény> AdatKocsik = new List<Adat_Szerelvény>();

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
                                Adat_Szerelvény Adat = new Adat_Szerelvény(
                                                rekord["id"].ToÉrt_Long(),
                                                rekord["szerelvényhossz"].ToÉrt_Long(),
                                                rekord["Kocsi1"].ToStrTrim(),
                                                rekord["Kocsi2"].ToStrTrim(),
                                                rekord["Kocsi3"].ToStrTrim(),
                                                rekord["Kocsi4"].ToStrTrim(),
                                                rekord["Kocsi5"].ToStrTrim(),
                                                rekord["Kocsi6"].ToStrTrim());
                                AdatKocsik.Add(Adat);
                            }
                        }
                    }
                }
            }
            return AdatKocsik;
        }

        public void Törlés(string Telephely, long Id, bool előírt = false)
        {
            try
            {
                FájlBeállítás(Telephely, előírt);
                string szöveg = $"DELETE FROM szerelvénytábla WHERE id={Id}";
                MyA.ABtörlés(hely, jelszó, szöveg);
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

        public void Törlés(string Telephely, List<long> Idk, bool előírt = false)
        {
            try
            {
                FájlBeállítás(Telephely, előírt);
                List<string> SzövegGy = new List<string>();
                foreach (long Id in Idk)
                {
                    string szöveg = $"DELETE FROM szerelvénytábla WHERE id={Id}";
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

        public void Módosítás(string Telephely, Adat_Szerelvény Adat, bool előírt = false)
        {
            try
            {
                FájlBeállítás(Telephely, előírt);
                string szöveg = "UPDATE szerelvénytábla SET ";
                szöveg += $"kocsi1='{Adat.Kocsi1}', ";
                szöveg += $"kocsi2='{Adat.Kocsi2}', ";
                szöveg += $"kocsi3='{Adat.Kocsi3}', ";
                szöveg += $"kocsi4='{Adat.Kocsi4}', ";
                szöveg += $"kocsi5='{Adat.Kocsi5}', ";
                szöveg += $"kocsi6='{Adat.Kocsi6}', ";
                szöveg += $"szerelvényhossz={Adat.Szerelvényhossz} ";
                szöveg += $" WHERE id={Adat.Szerelvény_ID}";
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

        public void Rögzítés(string Telephely, Adat_Szerelvény Adat, bool előírt = false)
        {
            try
            {
                FájlBeállítás(Telephely, előírt);
                string szöveg = "INSERT INTO szerelvénytábla (id, kocsi1, kocsi2, kocsi3, kocsi4, kocsi5, kocsi6, szerelvényhossz) VALUES (";
                szöveg += $"{Adat.Szerelvény_ID}, '{Adat.Kocsi1}', '{Adat.Kocsi2}', '{Adat.Kocsi3}', '{Adat.Kocsi4}', '{Adat.Kocsi5}', '{Adat.Kocsi6}', {Adat.Szerelvényhossz})";
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

        public void MódosításHossz(string Telephely, List<Adat_Szerelvény> Adatok, bool előírt = false)
        {
            try
            {
                FájlBeállítás(Telephely, előírt);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Szerelvény Adat in Adatok)
                {
                    string szöveg = "UPDATE szerelvénytábla SET ";
                    szöveg += $"szerelvényhossz={Adat.Szerelvényhossz} ";
                    szöveg += $" WHERE id={Adat.Szerelvény_ID}";
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

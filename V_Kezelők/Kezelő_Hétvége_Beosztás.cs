using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Hétvége_Beosztás
    {
        readonly string jelszó = "pozsgaii";
        string hely;
        readonly string táblanév = "beosztás";

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\villamos\előírásgyűjteményúj.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kiadáshétvége(hely.KönyvSzerk());
        }

        public List<Adat_Hétvége_Beosztás> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            string szöveg = $"SELECT * FROM {táblanév} order by vonal,id";
            List<Adat_Hétvége_Beosztás> Adatok = new List<Adat_Hétvége_Beosztás>();
            Adat_Hétvége_Beosztás Adat;

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
                                Adat = new Adat_Hétvége_Beosztás(
                                        rekord["id"].ToÉrt_Long(),
                                        rekord["vonal"].ToStrTrim(),
                                        rekord["kocsi1"].ToStrTrim(),
                                        rekord["kocsi2"].ToStrTrim(),
                                        rekord["kocsi3"].ToStrTrim(),
                                        rekord["kocsi4"].ToStrTrim(),
                                        rekord["kocsi5"].ToStrTrim(),
                                        rekord["kocsi6"].ToStrTrim(),
                                        rekord["vissza1"].ToStrTrim(),
                                        rekord["vissza2"].ToStrTrim(),
                                        rekord["vissza3"].ToStrTrim(),
                                        rekord["vissza4"].ToStrTrim(),
                                        rekord["vissza5"].ToStrTrim(),
                                        rekord["vissza6"].ToStrTrim()
                                       );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Törlés(string Telephely)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"DELETE FROM {táblanév} ";
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

        public void Törlés(string Telephely, string Azonosító)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"DELETE FROM {táblanév} where kocsi1='{Azonosító}'";
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

        public void Rögzítés(string Telephely, Adat_Hétvége_Beosztás Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"INSERT INTO {táblanév} (id, vonal, kocsi1, kocsi2, kocsi3, kocsi4, kocsi5, kocsi6, vissza1, vissza2, vissza3, vissza4, vissza5, vissza6) VALUES (";
                szöveg += $"{Sorszám(Telephely)}, ";
                szöveg += $"'{Adat.Vonal}', ";
                szöveg += $"'{Adat.Kocsi1}', ";
                szöveg += $"'{Adat.Kocsi2}', ";
                szöveg += $"'{Adat.Kocsi3}', ";
                szöveg += $"'{Adat.Kocsi4}', ";
                szöveg += $"'{Adat.Kocsi5}', ";
                szöveg += $"'{Adat.Kocsi6}', ";
                szöveg += $"'{Adat.Vissza1}', ";
                szöveg += $"'{Adat.Vissza2}', ";
                szöveg += $"'{Adat.Vissza3}', ";
                szöveg += $"'{Adat.Vissza4}', ";
                szöveg += $"'{Adat.Vissza5}', ";
                szöveg += $"'{Adat.Vissza6}') ";
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

        public void Módosítás(string Telephely, Adat_Hétvége_Beosztás Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $" vonal ='{Adat.Vonal}', ";
                szöveg += $" Kocsi2 ='{Adat.Kocsi2}', ";
                szöveg += $" Kocsi3 ='{Adat.Kocsi3}', ";
                szöveg += $" Kocsi4 ='{Adat.Kocsi4}', ";
                szöveg += $" Kocsi5 ='{Adat.Kocsi5}', ";
                szöveg += $" Kocsi6 ='{Adat.Kocsi6}', ";
                szöveg += $" Vissza1 ='{Adat.Vissza1}', ";
                szöveg += $" Vissza2 ='{Adat.Vissza2}', ";
                szöveg += $" Vissza3 ='{Adat.Vissza3}', ";
                szöveg += $" Vissza4 ='{Adat.Vissza4}', ";
                szöveg += $" Vissza5 ='{Adat.Vissza5}', ";
                szöveg += $" Vissza6 ='{Adat.Vissza6}' ";
                szöveg += $" WHERE kocsi1='{Adat.Kocsi1}'";
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

        public long Sorszám(string Telephely)
        {
            long Válasz = 1;
            try
            {
                FájlBeállítás(Telephely);
                List<Adat_Hétvége_Beosztás> Adatok = Lista_Adatok(Telephely);
                if (Adatok.Count > 0) Válasz = Adatok.Max(a => a.Id) + 1;

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
            return Válasz;
        }

    }

}

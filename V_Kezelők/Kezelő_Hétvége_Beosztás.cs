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
    public class Kezelő_Hétvége_Beosztás
    {
        readonly string jelszó = "pozsgaii";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\villamos\előírásgyűjteményúj.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kiadáshétvége(hely.KönyvSzerk());
        }

        public List<Adat_Hétvége_Beosztás> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            string szöveg = "SELECT * FROM beosztás order by vonal,id";
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
                string szöveg = "DELETE FROM beosztás ";
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


        public List<Adat_Hétvége_Beosztás> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
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
    }

}

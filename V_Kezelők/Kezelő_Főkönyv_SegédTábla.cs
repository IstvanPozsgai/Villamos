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
    public class Kezelő_Főkönyv_SegédTábla
    {
        readonly string jelszó = "lilaakác";
        string hely = "";

        private void FájlBeállítás(string Telephely, DateTime Dátum, string Napszak)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\főkönyv\{Dátum.Year}\nap\{Dátum:yyyyMMdd}{Napszak}nap.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Főkönyvtáblaalap(hely.KönyvSzerk());
        }

        public List<Adat_Főkönyv_SegédTábla> Lista_adatok(string Telephely, DateTime Dátum, string Napszak)
        {
            FájlBeállítás(Telephely, Dátum, Napszak);
            string szöveg = "SELECT * FROM segédtábla ";
            List<Adat_Főkönyv_SegédTábla> Adatok = new List<Adat_Főkönyv_SegédTábla>();
            Adat_Főkönyv_SegédTábla Adat;

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
                                Adat = new Adat_Főkönyv_SegédTábla(
                                    rekord["Id"].ToÉrt_Long(),
                                    rekord["Bejelentkezésinév"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, DateTime Dátum, string Napszak, Adat_Főkönyv_SegédTábla Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Napszak);
                string szöveg = $"INSERT INTO segédtábla (id, Bejelentkezésinév) VALUES ({Adat.Id}, '{Adat.Bejelentkezésinév}')";
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


        public void Törlés(string Telephely, DateTime Dátum, string Napszak)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Napszak);
                string szöveg = "DELETE FROM segédtábla";
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
        //Elkopó

        public Adat_Főkönyv_SegédTábla Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Főkönyv_SegédTábla Adat = null;

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
                            rekord.Read();
                            Adat = new Adat_Főkönyv_SegédTábla(
                                rekord["Id"].ToÉrt_Long(),
                                rekord["Bejelentkezésinév"].ToStrTrim()
                                );
                        }
                    }
                }
            }
            return Adat;
        }
    }
}

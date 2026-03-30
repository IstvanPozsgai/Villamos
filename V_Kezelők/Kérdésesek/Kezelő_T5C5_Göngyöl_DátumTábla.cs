using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_T5C5_Göngyöl_DátumTábla
    {
        string hely;
        readonly string jelszó = "pozsgaii";
        readonly string táblanév = "Dátumtábla";

        private void FájlBeállítás(string Telephely, DateTime Dátum)
        {
            if (Telephely == "Főmérnökség")
                hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\T5C5\Villamos3.mdb";
            else
                hely = $@"{Application.StartupPath}\{Telephely}\Adatok\főkönyv\futás\{Dátum.Year}\Villamos3-{Dátum.AddDays(-1):yyyyMMdd}.mdb";
        }

        public List<Adat_T5C5_Göngyöl_DátumTábla> Lista_Adatok(string Telephely, DateTime Dátum)
        {
            FájlBeállítás(Telephely, Dátum);
            string szöveg = $"SELECT * From {táblanév} ";
            List<Adat_T5C5_Göngyöl_DátumTábla> Adatok = new List<Adat_T5C5_Göngyöl_DátumTábla>();
            Adat_T5C5_Göngyöl_DátumTábla Adat;

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

                                Adat = new Adat_T5C5_Göngyöl_DátumTábla(
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["utolsórögzítés"].ToÉrt_DaTeTime(),
                                    rekord["Zárol"].ToÉrt_Bool()
                                    ); ;
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, DateTime Dátum, Adat_T5C5_Göngyöl_DátumTábla Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);
                string szöveg = $"INSERT INTO {táblanév} (telephely, utolsórögzítés, zárol) ";
                szöveg += $"VALUES ('{Adat.Telephely}',";
                szöveg += $"'{Adat.Utolsórögzítés:yyyy.MM.dd}',";
                szöveg += $"{Adat.Zárol})";
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

        public void Módosítás(string Telephely, DateTime Dátum, Adat_T5C5_Göngyöl_DátumTábla Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"utolsórögzítés='{Adat.Utolsórögzítés:yyyy.MM.dd}' ";
                szöveg += $"WHERE telephely='{Adat.Telephely}'";
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

        public void Zárolás(string Telephely, DateTime Dátum, string CMBTelephely, bool Zárolás)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);
                string szöveg = $"UPDATE {táblanév} SET Zárol={Zárolás} WHERE telephely='{CMBTelephely}'";
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

        public void ZárolásFelold(string Telephely, DateTime Dátum)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);
                string szöveg = $" UPDATE {táblanév} SET Zárol = False WHERE Zárol = True";
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

using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_T5C5_Göngyöl_DátumTábla
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\T5C5\Villamos3.mdb";
        readonly string jelszó = "pozsgaii";

        //Telephelyi adat vissza kell fejteni
        public List<Adat_T5C5_Göngyöl_DátumTábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
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

        public List<Adat_T5C5_Göngyöl_DátumTábla> Lista_Adatok()
        {
            string szöveg = $"SELECT * From Dátumtábla ";
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

        public void Rögzítés(Adat_T5C5_Göngyöl_DátumTábla Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO Dátumtábla (telephely, utolsórögzítés) ";
                szöveg += $"VALUES ('{Adat.Telephely}',";
                szöveg += $"'{Adat.Utolsórögzítés}')";
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

        public void Módosítás(Adat_T5C5_Göngyöl_DátumTábla Adat)
        {
            try
            {
                string szöveg = $"UPDATE Dátumtábla SET ";
                szöveg += $"utolsórögzítés='{Adat.Utolsórögzítés}' ";
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

    }

}

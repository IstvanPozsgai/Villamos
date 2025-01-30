using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Szolgálat
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb";
        readonly string jelszó = "Mocó";

        public List<Adat_Kiegészítő_Szolgálat> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Szolgálat Adat;
            List<Adat_Kiegészítő_Szolgálat> Adatok = new List<Adat_Kiegészítő_Szolgálat>();

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
                                Adat = new Adat_Kiegészítő_Szolgálat(
                                           rekord["sorszám"].ToÉrt_Int(),
                                           rekord["szolgálatnév"].ToStrTrim()
                                           );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Kiegészítő_Szolgálat> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM szolgálattábla order by sorszám";
            Adat_Kiegészítő_Szolgálat Adat;
            List<Adat_Kiegészítő_Szolgálat> Adatok = new List<Adat_Kiegészítő_Szolgálat>();

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
                                Adat = new Adat_Kiegészítő_Szolgálat(
                                           rekord["sorszám"].ToÉrt_Int(),
                                           rekord["szolgálatnév"].ToStrTrim()
                                           );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Kiegészítő_Szolgálat Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO szolgálattábla (sorszám, szolgálatnév) VALUES ";
                szöveg += $" ({Adat.Sorszám}, ";
                szöveg += $"'{Adat.Szolgálatnév}')";
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

        public void Törlés(Adat_Kiegészítő_Szolgálat Adat)
        {
            try
            {
                string szöveg = $"DELETE FROM szolgálattábla WHERE szolgálatnév='{Adat.Szolgálatnév}'";
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

        public void Módosítás(Adat_Kiegészítő_Szolgálat Adat)
        {
            try
            {
                string szöveg = $"UPDATE szolgálattábla SET szolgálatnév='{Adat.Szolgálatnév}' ";
                szöveg += $"WHERE sorszám={Adat.Sorszám} ";
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

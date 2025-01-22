using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;


namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Szolgálattelepei
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb";
        readonly string jelszó = "Mocó";

        public List<Adat_Kiegészítő_Szolgálattelepei> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Szolgálattelepei Adat;
            List<Adat_Kiegészítő_Szolgálattelepei> Adatok = new List<Adat_Kiegészítő_Szolgálattelepei>();

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
                                Adat = new Adat_Kiegészítő_Szolgálattelepei(
                                           rekord["sorszám"].ToÉrt_Int(),
                                           rekord["telephelynév"].ToStrTrim(),
                                           rekord["szolgálatnév"].ToStrTrim(),
                                           rekord["felelősmunkahely"].ToStrTrim()
                                           );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Kiegészítő_Szolgálattelepei> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM szolgálattelepeitábla order by sorszám";
            Adat_Kiegészítő_Szolgálattelepei Adat;
            List<Adat_Kiegészítő_Szolgálattelepei> Adatok = new List<Adat_Kiegészítő_Szolgálattelepei>();

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
                                Adat = new Adat_Kiegészítő_Szolgálattelepei(
                                           rekord["sorszám"].ToÉrt_Int(),
                                           rekord["telephelynév"].ToStrTrim(),
                                           rekord["szolgálatnév"].ToStrTrim(),
                                           rekord["felelősmunkahely"].ToStrTrim()
                                           );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Kiegészítő_Szolgálattelepei Adat)
        {
            try
            {
                string szöveg = "INSERT INTO szolgálattelepeitábla ( sorszám, szolgálatnév, telephelynév, felelősmunkahely )";
                szöveg += $" VALUES ({Adat.Sorszám}, ";
                szöveg += $"'{Adat.Szolgálatnév}', ";
                szöveg += $"'{Adat.Telephelynév}', ";
                szöveg += $"'{Adat.Felelősmunkahely}')";
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

        /// <summary>
        /// telephelynév
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(Adat_Kiegészítő_Szolgálattelepei Adat)
        {
            try
            {
                string szöveg = "UPDATE szolgálattelepeitábla SET ";
                szöveg += $" sorszám={Adat.Sorszám},";
                szöveg += $" szolgálatnév='{Adat.Szolgálatnév}', ";
                szöveg += $" felelősmunkahely='{Adat.Felelősmunkahely}' ";
                szöveg += $" WHERE telephelynév='{Adat.Telephelynév}'";
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

        /// <summary>
        /// telephelynév, sorszám
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Törlés(Adat_Kiegészítő_Szolgálattelepei Adat)
        {
            try
            {
                string szöveg = $"DELETE FROM szolgálattelepeitábla ";
                szöveg += $" where telephelynév='{Adat.Telephelynév}' and sorszám={Adat.Sorszám}";
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
    }




    public class Kezelő_Kiegészítő_SérülésSzöveg
    {
        public List<Adat_Kiegészítő_SérülésSzöveg> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_SérülésSzöveg Adat;
            List<Adat_Kiegészítő_SérülésSzöveg> Adatok = new List<Adat_Kiegészítő_SérülésSzöveg>();

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
                                Adat = new Adat_Kiegészítő_SérülésSzöveg(
                                           rekord["Id"].ToÉrt_Int(),
                                           rekord["Szöveg1"].ToStrTrim(),
                                           rekord["Szöveg2"].ToStrTrim(),
                                           rekord["Szöveg3"].ToStrTrim(),
                                           rekord["Szöveg4"].ToStrTrim(),
                                           rekord["Szöveg5"].ToStrTrim(),
                                           rekord["Szöveg6"].ToStrTrim(),
                                           rekord["Szöveg7"].ToStrTrim(),
                                           rekord["Szöveg8"].ToStrTrim(),
                                           rekord["Szöveg9"].ToStrTrim(),
                                           rekord["Szöveg10"].ToStrTrim(),
                                           rekord["Szöveg11"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kiegészítő_SérülésSzöveg Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_SérülésSzöveg Adat = null;

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

                            Adat = new Adat_Kiegészítő_SérülésSzöveg(
                                           rekord["Id"].ToÉrt_Int(),
                                           rekord["Szöveg1"].ToStrTrim(),
                                           rekord["Szöveg2"].ToStrTrim(),
                                           rekord["Szöveg3"].ToStrTrim(),
                                           rekord["Szöveg4"].ToStrTrim(),
                                           rekord["Szöveg5"].ToStrTrim(),
                                           rekord["Szöveg6"].ToStrTrim(),
                                           rekord["Szöveg7"].ToStrTrim(),
                                           rekord["Szöveg8"].ToStrTrim(),
                                           rekord["Szöveg9"].ToStrTrim(),
                                           rekord["Szöveg10"].ToStrTrim(),
                                           rekord["Szöveg11"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Kiegészítő_Részmunkakör
    {
        public List<Adat_Kiegészítő_Részmunkakör> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Részmunkakör Adat;
            List<Adat_Kiegészítő_Részmunkakör> Adatok = new List<Adat_Kiegészítő_Részmunkakör>();

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
                                Adat = new Adat_Kiegészítő_Részmunkakör(
                                           rekord["Id"].ToÉrt_Long(),
                                           rekord["Megnevezés"].ToStrTrim(),
                                           rekord["Id"].ToÉrt_Long()
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

    public class Kezelő_Kiegészítő_Kiegmunkakör
    {
        public List<Adat_Kiegészítő_Kiegmunkakör> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Kiegmunkakör Adat;
            List<Adat_Kiegészítő_Kiegmunkakör> Adatok = new List<Adat_Kiegészítő_Kiegmunkakör>();

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
                                Adat = new Adat_Kiegészítő_Kiegmunkakör(
                                           rekord["Id"].ToÉrt_Long(),
                                           rekord["Megnevezés"].ToStrTrim(),
                                           rekord["Id"].ToÉrt_Long()
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

    public class Kezelő_Kiegészítő_Doksi
    {
        public List<Adat_Kiegészítő_Doksi> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Doksi Adat;
            List<Adat_Kiegészítő_Doksi> Adatok = new List<Adat_Kiegészítő_Doksi>();

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
                                Adat = new Adat_Kiegészítő_Doksi(
                                           rekord["Kategória"].ToStrTrim(),
                                           rekord["Kód"].ToStrTrim(),
                                           rekord["Éves"].ToStrTrim()
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
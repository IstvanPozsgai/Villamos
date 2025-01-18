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
                string szöveg = $"DELETE FROM szolgálattelepeitábla '";
                szöveg += $" where telephelynév='{Adat.Telephelynév} and sorszám={Adat.Sorszám}";
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

    public class Kezelő_Kiegészítő_Turnusok
    {
        public List<Adat_Kiegészítő_Turnusok> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Turnusok Adat;
            List<Adat_Kiegészítő_Turnusok> Adatok = new List<Adat_Kiegészítő_Turnusok>();

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
                                Adat = new Adat_Kiegészítő_Turnusok(
                                           rekord["csoport"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kiegészítő_Turnusok Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Turnusok Adat = null;

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

                            Adat = new Adat_Kiegészítő_Turnusok(
                                       rekord["csoport"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Kiegészítő_Jogtípus
    {
        public List<Adat_Kiegészítő_Jogtípus> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Jogtípus Adat;
            List<Adat_Kiegészítő_Jogtípus> Adatok = new List<Adat_Kiegészítő_Jogtípus>();

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
                                Adat = new Adat_Kiegészítő_Jogtípus(
                                           rekord["sorszám"].ToÉrt_Long(),
                                           rekord["típus"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }

    public class Kezelő_Kiegészítő_JogVonal
    {
        public List<Adat_Kiegészítő_Jogvonal> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Jogvonal Adat;
            List<Adat_Kiegészítő_Jogvonal> Adatok = new List<Adat_Kiegészítő_Jogvonal>();

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
                                Adat = new Adat_Kiegészítő_Jogvonal(
                                           rekord["sorszám"].ToÉrt_Long(),
                                           rekord["Szám"].ToStrTrim(),
                                           rekord["Megnevezés"].ToStrTrim()
                                           );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kiegészítő_Jogvonal Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Jogvonal Adat = null;

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
                            Adat = new Adat_Kiegészítő_Jogvonal(
                                   rekord["sorszám"].ToÉrt_Long(),
                                   rekord["Szám"].ToString(),
                                   rekord["Megnevezés"].ToStrTrim()
                                   );
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Kiegészítő_Feorszámok
    {
        public List<Adat_Kiegészítő_Feorszámok> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Feorszámok Adat;
            List<Adat_Kiegészítő_Feorszámok> Adatok = new List<Adat_Kiegészítő_Feorszámok>();

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
                                Adat = new Adat_Kiegészítő_Feorszámok(
                                           rekord["sorszám"].ToÉrt_Long(),
                                           rekord["Feorszám"].ToStrTrim(),
                                           rekord["feormegnevezés"].ToStrTrim(),
                                           rekord["Státus"].ToÉrt_Long()
                                           );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kiegészítő_Feorszámok Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Feorszámok Adat = null;

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

                            Adat = new Adat_Kiegészítő_Feorszámok(
                                       rekord["sorszám"].ToÉrt_Long(),
                                       rekord["Feorszám"].ToStrTrim(),
                                       rekord["feormegnevezés"].ToStrTrim(),
                                       rekord["Státus"].ToÉrt_Long()
                                       );
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

    public class Kezelő_Kiegészítő_Váltóstábla
    {
        public List<Adat_Kiegészítő_Váltóstábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Váltóstábla> Adatok = new List<Adat_Kiegészítő_Váltóstábla>();
            Adat_Kiegészítő_Váltóstábla Adat;

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
                                Adat = new Adat_Kiegészítő_Váltóstábla(
                                       rekord["Id"].ToÉrt_Int(),
                                       rekord["Kezdődátum"].ToÉrt_DaTeTime(),
                                       rekord["Ciklusnap"].ToÉrt_Int(),
                                       rekord["Megnevezés"].ToStrTrim(),
                                       rekord["Csoport"].ToString());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }

    public class Kezelő_Kiegészítő_Beosztásciklus
    {
        public List<Adat_Kiegészítő_Beosztásciklus> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Beosztásciklus> Adatok = new List<Adat_Kiegészítő_Beosztásciklus>();
            Adat_Kiegészítő_Beosztásciklus Adat;

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
                                Adat = new Adat_Kiegészítő_Beosztásciklus(
                                       rekord["Id"].ToÉrt_Int(),
                                       rekord["Beosztáskód"].ToStrTrim(),
                                       rekord["Hétnapja"].ToStrTrim(),
                                       rekord["Beosztásszöveg"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kiegészítő_Beosztásciklus Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Beosztásciklus Adat = null;

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

                            Adat = new Adat_Kiegészítő_Beosztásciklus(
                                   rekord["Id"].ToÉrt_Int(),
                                   rekord["Beosztáskód"].ToStrTrim(),
                                   rekord["Hétnapja"].ToStrTrim(),
                                   rekord["Beosztásszöveg"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Kiegészítő_Beosegéd
    {
        public List<Adat_Kiegészítő_Beosegéd> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Beosegéd> Adatok = new List<Adat_Kiegészítő_Beosegéd>();
            Adat_Kiegészítő_Beosegéd Adat;

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
                                Adat = new Adat_Kiegészítő_Beosegéd(
                                     rekord["Beosztáskód"].ToStrTrim(),
                                     rekord["Túlóra"].ToÉrt_Int(),
                                     rekord["Kezdőidő"].ToÉrt_DaTeTime(),
                                     rekord["Végeidő"].ToÉrt_DaTeTime(),
                                     rekord["túlóraoka"].ToStrTrim(),
                                     rekord["telephely"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kiegészítő_Beosegéd Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Beosegéd Adat = null;

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

                            Adat = new Adat_Kiegészítő_Beosegéd(
                                 rekord["Beosztáskód"].ToStrTrim(),
                                 rekord["Túlóra"].ToÉrt_Int(),
                                 rekord["Kezdőidő"].ToÉrt_DaTeTime(),
                                 rekord["Végeidő"].ToÉrt_DaTeTime(),
                                 rekord["túlóraoka"].ToStrTrim(),
                                 rekord["telephely"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Kiegészítő_Túlórakeret
    {
        public List<Adat_Kiegészítő_Túlórakeret> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Túlórakeret> Adatok = new List<Adat_Kiegészítő_Túlórakeret>();
            Adat_Kiegészítő_Túlórakeret Adat;

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
                                Adat = new Adat_Kiegészítő_Túlórakeret(
                                     rekord["Határ"].ToÉrt_Int(),
                                     rekord["Parancs"].ToÉrt_Int(),
                                     rekord["Telephely"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kiegészítő_Túlórakeret Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Túlórakeret Adat = null;

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

                            Adat = new Adat_Kiegészítő_Túlórakeret(
                                 rekord["Határ"].ToÉrt_Int(),
                                 rekord["Parancs"].ToÉrt_Int(),
                                 rekord["Telephely"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Kiegészítő_Munkaidő
    {
        public List<Adat_Kiegészítő_Munkaidő> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Munkaidő> Adatok = new List<Adat_Kiegészítő_Munkaidő>();
            Adat_Kiegészítő_Munkaidő Adat;

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
                                Adat = new Adat_Kiegészítő_Munkaidő(
                                     rekord["munkarendelnevezés"].ToStrTrim(),
                                     rekord["munkaidő"].ToÉrt_Double()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kiegészítő_Munkaidő Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Kiegészítő_Munkaidő Adat = null;

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
                            Adat = new Adat_Kiegészítő_Munkaidő(
                                  rekord["munkarendelnevezés"].ToStrTrim(),
                                  rekord["munkaidő"].ToÉrt_Double()
                                  );
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Kiegészítő_Védelem
    {
        public List<Adat_Kiegészítő_Védelem> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Védelem> Adatok = new List<Adat_Kiegészítő_Védelem>();
            Adat_Kiegészítő_Védelem Adat;

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
                                Adat = new Adat_Kiegészítő_Védelem(
                                     rekord["sorszám"].ToÉrt_Long(),
                                     rekord["megnevezés"].ToStrTrim()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kiegészítő_Védelem Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Védelem Adat = null;

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
                            Adat = new Adat_Kiegészítő_Védelem(
                                  rekord["sorszám"].ToÉrt_Long(),
                                  rekord["megnevezés"].ToStrTrim()
                                  );
                        }
                    }
                }
            }
            return Adat;
        }


    }





    public class Kezelő_Kiegészítő_Munkakör
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő2.mdb";
        readonly string jelszó = "Mocó";
        public List<Adat_Kiegészítő_Munkakör> Lista_Adatok(string szöveg)
        {
            List<Adat_Kiegészítő_Munkakör> Adatok = new List<Adat_Kiegészítő_Munkakör>();
            Adat_Kiegészítő_Munkakör Adat;

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
                                Adat = new Adat_Kiegészítő_Munkakör(
                                     rekord["Id"].ToÉrt_Long(),
                                     rekord["Megnevezés"].ToStrTrim(),
                                     rekord["Kategória"].ToStrTrim(),
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

        public void Rögzítés(Adat_Kiegészítő_Munkakör Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO Munkakör (Id, megnevezés, Kategória,státus) VALUES ";
                szöveg += $"({Adat.Id}, '{Adat.Megnevezés}', '{Adat.Kategória}', {Adat.Státus})";
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
        /// Id szerint keresessünk
        /// </summary>
        /// <param name="Adat"></param>
        public void Módosítás(Adat_Kiegészítő_Munkakör Adat)
        {
            try
            {
                string szöveg = $"UPDATE munkakör SET ";
                szöveg += $" megnevezés='{Adat.Megnevezés}',";
                szöveg += $" Kategória='{Adat.Kategória}',";
                szöveg += $" státus={Adat.Státus}";
                szöveg += $" WHERE id={Adat.Id}";
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
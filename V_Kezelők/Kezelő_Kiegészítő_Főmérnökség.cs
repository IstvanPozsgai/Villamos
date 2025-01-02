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

        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Szolgálattelepei Adat)
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
        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_Szolgálattelepei Adat)
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
        public void Törlés(string hely, string jelszó, Adat_Kiegészítő_Szolgálattelepei Adat)
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


    public class Kezelő_Kiegészítő_Szolgálat
    {
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


        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Szolgálat Adat)
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

        /// <summary>
        /// szolgálatnév
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Törlés(string hely, string jelszó, Adat_Kiegészítő_Szolgálat Adat)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_Szolgálat Adat)
        {
            try
            {
                string szöveg = $"UPDATE szolgálattábla SET szolgálatnév='{Adat.Szolgálatnév}' ";
                szöveg += $"WHERE sorszám= '{Adat.Sorszám}' ";
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

    public class Kezelő_Kiegészítő_Könyvtár
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\kiegészítő2.mdb";
        readonly string jelszó = "Mocó";
        public List<Adat_Kiegészítő_Könyvtár> Lista_Adatok(string hely, string jelszó, string szöveg)
        {

            Adat_Kiegészítő_Könyvtár Adat;
            List<Adat_Kiegészítő_Könyvtár> Adatok = new List<Adat_Kiegészítő_Könyvtár>();

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
                                Adat = new Adat_Kiegészítő_Könyvtár(
                                           rekord["id"].ToÉrt_Int(),
                                           rekord["név"].ToStrTrim(),
                                           rekord["vezér1"].ToÉrt_Bool(),
                                           rekord["Csoport1"].ToÉrt_Int(),
                                           rekord["Csoport2"].ToÉrt_Int(),
                                           rekord["vezér2"].ToÉrt_Bool(),
                                           rekord["sorrend1"].ToÉrt_Int(),
                                           rekord["sorrend2"].ToÉrt_Int()
                                           );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Kiegészítő_Könyvtár> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM könyvtár ORDER BY név";
            Adat_Kiegészítő_Könyvtár Adat;
            List<Adat_Kiegészítő_Könyvtár> Adatok = new List<Adat_Kiegészítő_Könyvtár>();

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
                                Adat = new Adat_Kiegészítő_Könyvtár(
                                           rekord["id"].ToÉrt_Int(),
                                           rekord["név"].ToStrTrim(),
                                           rekord["vezér1"].ToÉrt_Bool(),
                                           rekord["Csoport1"].ToÉrt_Int(),
                                           rekord["Csoport2"].ToÉrt_Int(),
                                           rekord["vezér2"].ToÉrt_Bool(),
                                           rekord["sorrend1"].ToÉrt_Int(),
                                           rekord["sorrend2"].ToÉrt_Int()
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


    public class Kezelő_Kiegészítő_Sérülés
    {
        public List<Adat_Kiegészítő_Sérülés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {

            Adat_Kiegészítő_Sérülés Adat;
            List<Adat_Kiegészítő_Sérülés> Adatok = new List<Adat_Kiegészítő_Sérülés>();

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
                                Adat = new Adat_Kiegészítő_Sérülés(
                                           rekord["id"].ToÉrt_Int(),
                                           rekord["név"].ToStrTrim(),
                                           rekord["vezér1"].ToÉrt_Bool(),
                                           rekord["Csoport1"].ToÉrt_Int(),
                                           rekord["Csoport2"].ToÉrt_Int(),
                                           rekord["vezér2"].ToÉrt_Bool(),
                                           rekord["sorrend1"].ToÉrt_Int(),
                                           rekord["sorrend2"].ToÉrt_Int(),
                                           rekord["költséghely"].ToStrTrim()
                                           );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kiegészítő_Sérülés Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Kiegészítő_Sérülés Adat = null;

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
                                Adat = new Adat_Kiegészítő_Sérülés(
                                           rekord["id"].ToÉrt_Int(),
                                           rekord["név"].ToStrTrim(),
                                           rekord["vezér1"].ToÉrt_Bool(),
                                           rekord["Csoport1"].ToÉrt_Int(),
                                           rekord["Csoport2"].ToÉrt_Int(),
                                           rekord["vezér2"].ToÉrt_Bool(),
                                           rekord["sorrend1"].ToÉrt_Int(),
                                           rekord["sorrend2"].ToÉrt_Int(),
                                           rekord["költséghely"].ToStrTrim()
                                           );
                            }
                        }
                    }
                }
            }
            return Adat;
        }
        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Sérülés Adat)
        {
            try
            {
                string szöveg = "INSERT INTO sérülés ";
                szöveg += " (id, név, csoport1, csoport2, sorrend1, sorrend2, vezér1, vezér2, költséghely ) VALUES ";
                szöveg += $"({Adat.ID}, ";
                szöveg += $"'{Adat.Név}', ";
                szöveg += $"{Adat.Csoport1}, ";
                szöveg += $"{Adat.Csoport2}, ";
                szöveg += $"{Adat.Sorrend1}, ";
                szöveg += $"{Adat.Sorrend2}, ";
                szöveg += $"{Adat.Vezér1}, ";
                szöveg += $"{Adat.Vezér2}, ";
                szöveg += $"'{Adat.Költséghely}')";

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
        /// id
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_Sérülés Adat)
        {
            try
            {
                string szöveg = "UPDATE sérülés SET ";
                szöveg += $"név='{Adat.Név}', ";
                szöveg += $"csoport1={Adat.Csoport1}, ";
                szöveg += $"csoport2={Adat.Csoport2}, ";
                szöveg += $"sorrend1={Adat.Sorrend1}, ";
                szöveg += $"sorrend2={Adat.Sorrend2}, ";
                szöveg += $"vezér1={Adat.Vezér1}, ";
                szöveg += $"vezér2={Adat.Vezér2}, ";
                szöveg += $"költséghely='{Adat.Költséghely}' ";
                szöveg += $" WHERE id={Adat.ID}";

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
        /// id
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Törlés(string hely, string jelszó, Adat_Kiegészítő_Sérülés Adat)
        {
            try
            {
                string szöveg = $"Delete FROM sérülés where id={Adat.ID}";
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

    public class Kezelő_Kiegészítő_Főkategóriatábla
    {
        public List<Adat_Kiegészítő_Főkategóriatábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Főkategóriatábla> Adatok = new List<Adat_Kiegészítő_Főkategóriatábla>();
            Adat_Kiegészítő_Főkategóriatábla Adat;

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
                                Adat = new Adat_Kiegészítő_Főkategóriatábla(
                                     rekord["sorszám"].ToÉrt_Long(),
                                     rekord["főkategória"].ToStrTrim()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kiegészítő_Főkategóriatábla Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Főkategóriatábla Adat = null;

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
                            Adat = new Adat_Kiegészítő_Főkategóriatábla(
                                  rekord["sorszám"].ToÉrt_Long(),
                                  rekord["főkategória"].ToStrTrim()
                                  );
                        }
                    }
                }
            }
            return Adat;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Főkategóriatábla Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO főkategóriatábla (sorszám, főkategória) ";
                szöveg += $"VALUES ({Adat.Sorszám}, ";
                szöveg += $"'{Adat.Főkategória}')";
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
        /// főkategória
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Törlés(string hely, string jelszó, Adat_Kiegészítő_Főkategóriatábla Adat)
        {
            try
            {
                string szöveg = $"DELETE  FROM főkategóriatábla WHERE főkategória='{Adat.Főkategória}'";
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

        /// <summary>
        /// sorszám
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_Főkategóriatábla Adat)
        {
            try
            {
                string szöveg = $"UPDATE főkategóriatábla SET ";
                szöveg += $"főkategória='{Adat.Főkategória}' ";
                szöveg += $"WHERE sorszám= '{Adat.Sorszám}'";
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

    public class Kezelő_Kiegészítő_Típusrendezéstábla
    {
        public List<Adat_Kiegészítő_Típusrendezéstábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Típusrendezéstábla> Adatok = new List<Adat_Kiegészítő_Típusrendezéstábla>();
            Adat_Kiegészítő_Típusrendezéstábla Adat;

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
                                Adat = new Adat_Kiegészítő_Típusrendezéstábla(
                                     rekord["sorszám"].ToÉrt_Long(),
                                     rekord["főkategória"].ToStrTrim(),
                                     rekord["típus"].ToStrTrim(),
                                     rekord["alTípus"].ToStrTrim(),
                                     rekord["telephely"].ToStrTrim(),
                                     rekord["telephelyitípus"].ToStrTrim()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Típusrendezéstábla Adat)
        {
            try
            {
                string szöveg = "INSERT INTO típusrendezéstábla ( sorszám, főkategória, típus, altípus, telephely, telephelyitípus)";
                szöveg += $" VALUES ({Adat.Sorszám}, ";
                szöveg += $"'{Adat.Főkategória}', ";
                szöveg += $"'{Adat.Típus}', ";
                szöveg += $"'{Adat.AlTípus}', ";
                szöveg += $"'{Adat.Telephely}', ";
                szöveg += $"'{Adat.Telephelyitípus}')";
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
        /// sorszám
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Törlés(string hely, string jelszó, Adat_Kiegészítő_Típusrendezéstábla Adat)
        {
            try
            {
                string szöveg = $"DELETE FROM típusrendezéstábla where sorszám={Adat.Sorszám}";
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
        /// <summary>
        /// telephely, telephelyitípus
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_Típusrendezéstábla Adat)
        {
            try
            {
                string szöveg = $"UPDATE típusrendezéstábla SET ";
                szöveg += $"sorszám='{Adat.Sorszám}'";
                szöveg += $"WHERE telephely='{Adat.Telephely}' ";
                szöveg += $"and telephelyitípus='{Adat.Telephelyitípus}'";
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



    public class Kezelő_Kiegészítő_Típusaltípustábla
    {
        public List<Adat_Kiegészítő_Típusaltípustábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Típusaltípustábla> Adatok = new List<Adat_Kiegészítő_Típusaltípustábla>();
            Adat_Kiegészítő_Típusaltípustábla Adat;

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
                                Adat = new Adat_Kiegészítő_Típusaltípustábla(
                                     rekord["sorszám"].ToÉrt_Long(),
                                     rekord["főkategória"].ToStrTrim(),
                                     rekord["típus"].ToStrTrim(),
                                     rekord["alTípus"].ToStrTrim()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Típusaltípustábla Adat)
        {
            try
            {
                string szöveg = "INSERT INTO típusaltípustábla ( sorszám, Főkategória, típus, altípus )";
                szöveg += $" VALUES ({Adat.Sorszám}, ";
                szöveg += $"'{Adat.Főkategória}', ";
                szöveg += $"'{Adat.Típus}', ";
                szöveg += $"'{Adat.AlTípus}')";
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
        /// sorszám
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Törlés(string hely, string jelszó, Adat_Kiegészítő_Típusaltípustábla Adat)
        {
            try
            {
                string szöveg = $"DELETE FROM típusaltípustábla where  sorszám={Adat.Sorszám}";
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


        /// <summary>
        /// altípus
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_Típusaltípustábla Adat)
        {
            try
            {
                string szöveg = $"UPDATE típusaltípustábla SET ";
                szöveg += $"sorszám= '{Adat.Sorszám}'";
                szöveg += $"WHERE altípus='{Adat.AlTípus}'";
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

    public class Kezelő_Kiegészítő_Fortetípus
    {
        public List<Adat_Kiegészítő_Fortetípus> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Fortetípus> Adatok = new List<Adat_Kiegészítő_Fortetípus>();
            Adat_Kiegészítő_Fortetípus Adat;

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
                                Adat = new Adat_Kiegészítő_Fortetípus(
                                     rekord["sorszám"].ToÉrt_Long(),
                                     rekord["ftípus"].ToStrTrim(),
                                     rekord["telephely"].ToStrTrim(),
                                     rekord["telephelyitípus"].ToStrTrim()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Fortetípus Adat)
        {
            try
            {
                string szöveg = "INSERT INTO fortetípus ( sorszám, ftípus, telephely, telephelyitípus )";
                szöveg += $" VALUES ({Adat.Sorszám}, ";
                szöveg += $"'{Adat.Ftípus}', ";
                szöveg += $"'{Adat.Telephely}', ";
                szöveg += $"'{Adat.Telephelyitípus}' )";
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
        /// sorszám
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Törlés(string hely, string jelszó, Adat_Kiegészítő_Fortetípus Adat)
        {
            try
            {
                string szöveg = $"DELETE FROM fortetípus where  sorszám={Adat.Sorszám}";
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

    public class Kezelő_Kiegészítő_Mentésihelyek
    {
        public List<Adat_Kiegészítő_Mentésihelyek> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Mentésihelyek> Adatok = new List<Adat_Kiegészítő_Mentésihelyek>();
            Adat_Kiegészítő_Mentésihelyek Adat;

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
                                Adat = new Adat_Kiegészítő_Mentésihelyek(
                                     rekord["sorszám"].ToÉrt_Long(),
                                     rekord["alprogram"].ToStrTrim(),
                                     rekord["Elérésiút"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kiegészítő_Mentésihelyek Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Mentésihelyek Adat = null;

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

                            Adat = new Adat_Kiegészítő_Mentésihelyek(
                                 rekord["sorszám"].ToÉrt_Long(),
                                 rekord["alprogram"].ToStrTrim(),
                                 rekord["Elérésiút"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Mentésihelyek Adat)
        {
            string szöveg = $"INSERT INTO Mentésihelyek ( sorszám, alprogram, elérésiút )";
            szöveg += $" VALUES ({Adat.Sorszám}, ";
            szöveg += $"'{Adat.Alprogram}',";
            szöveg += $"'{Adat.Elérésiút}' )";

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        /// <summary>
        /// sorszám
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_Mentésihelyek Adat)
        {
            try
            {
                string szöveg = $"UPDATE Mentésihelyek SET ";
                szöveg += $" alprogram='{Adat.Alprogram}',";
                szöveg += $" elérésiút='{Adat.Elérésiút}' ";
                szöveg += $" WHERE sorszám={Adat.Sorszám} ";

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
        /// sorszám
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Törlés(string hely, string jelszó, Adat_Kiegészítő_Mentésihelyek Adat)
        {
            try
            {
                string szöveg = $"DELETE FROM Mentésihelyek WHERE sorszám={Adat.Sorszám}";
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
    public class Kezelő_Kiegészítő_Típuszínektábla
    {
        public List<Adat_Kiegészítő_Típuszínektábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Típuszínektábla> Adatok = new List<Adat_Kiegészítő_Típuszínektábla>();
            Adat_Kiegészítő_Típuszínektábla Adat;

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
                                Adat = new Adat_Kiegészítő_Típuszínektábla(
                                     rekord["típus"].ToStrTrim(),
                                     rekord["színszám"].ToÉrt_Long());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Típuszínektábla Adat)
        {
            string szöveg = $"INSERT INTO Típuszínektábla (típus, színszám) ";
            szöveg += $"VALUES ('{Adat.Típus}' ,";
            szöveg += $" {Adat.Színszám})";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        /// <summary>
        /// típus
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_Típuszínektábla Adat)
        {
            try
            {
                string szöveg = $"UPDATE Típuszínektábla SET ";
                szöveg += $"színszám= '{Adat.Színszám}',";
                szöveg += $"WHERE típus='{Adat.Típus}'";
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
        /// típus
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Törlés(string hely, string jelszó, Adat_Kiegészítő_Típuszínektábla Adat)
        {
            try
            {
                string szöveg = $"DELETE * FROM Típuszínektábla where típus='{Adat.Típus}'";
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

    public class Kezelő_Kiegészítő_Idő_Kor
    {
        public List<Adat_Kiegészítő_Idő_Kor> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Idő_Kor> Adatok = new List<Adat_Kiegészítő_Idő_Kor>();
            Adat_Kiegészítő_Idő_Kor Adat;

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
                                Adat = new Adat_Kiegészítő_Idő_Kor(
                                     rekord["id"].ToÉrt_Long(),
                                     rekord["kiadási"].ToÉrt_Long(),
                                     rekord["érkezési"].ToÉrt_Long()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kiegészítő_Idő_Kor Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Kiegészítő_Idő_Kor Adat = null;

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
                            Adat = new Adat_Kiegészítő_Idő_Kor(
                                   rekord["id"].ToÉrt_Long(),
                                   rekord["kiadási"].ToÉrt_Long(),
                                   rekord["érkezési"].ToÉrt_Long()
                                   );
                        }
                    }
                }
            }
            return Adat;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Idő_Kor Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO idő_korrekció  (id, kiadási, érkezési ) ";
                szöveg += $"VALUES ('{Adat.Id}, ";
                szöveg += $"{Adat.Kiadási}, ";
                szöveg += $"{Adat.Érkezési}) ";
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
        /// id
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_Idő_Kor Adat)
        {
            try
            {
                string szöveg = $"UPDATE idő_korrekció Set ";
                szöveg += $"érkezési={Adat.Érkezési}, ";
                szöveg += $"kiadási={Adat.Kiadási} ";
                szöveg += $" where id={Adat.Id} ";
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

    public class Kezelő_Kiegészítő_Adatok_Terjesztés
    {
        public List<Adat_Kiegészítő_Adatok_Terjesztés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Adatok_Terjesztés> Adatok = new List<Adat_Kiegészítő_Adatok_Terjesztés>();
            Adat_Kiegészítő_Adatok_Terjesztés Adat;

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
                                Adat = new Adat_Kiegészítő_Adatok_Terjesztés(
                                     rekord["id"].ToÉrt_Long(),
                                     rekord["szöveg"].ToStrTrim(),
                                     rekord["email"].ToStrTrim()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        /// <summary>
        /// id
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_Adatok_Terjesztés Adat)
        {
            try
            {
                string szöveg = $"UPDATE Adatok SET ";
                szöveg += $"szöveg='{Adat.Szöveg}', ";
                szöveg += $"email='{Adat.Email}' ";
                szöveg += $"WHERE '{Adat.Id}' ";
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


    public class Kezelő_Kiegészítő_Idő_Tábla
    {
        public List<Adat_Kiegészítő_Idő_Tábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Idő_Tábla> Adatok = new List<Adat_Kiegészítő_Idő_Tábla>();
            Adat_Kiegészítő_Idő_Tábla Adat;

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
                                Adat = new Adat_Kiegészítő_Idő_Tábla(
                                     rekord["sorszám"].ToÉrt_Long(),
                                     rekord["reggel"].ToÉrt_DaTeTime(),
                                     rekord["este"].ToÉrt_DaTeTime(),
                                     rekord["délután"].ToÉrt_DaTeTime()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kiegészítő_Idő_Tábla Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Idő_Tábla Adat = null;

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
                            Adat = new Adat_Kiegészítő_Idő_Tábla(
                                           rekord["sorszám"].ToÉrt_Long(),
                                           rekord["reggel"].ToÉrt_DaTeTime(),
                                           rekord["este"].ToÉrt_DaTeTime(),
                                           rekord["délután"].ToÉrt_DaTeTime()
                                           );
                        }
                    }
                }
            }
            return Adat;
        }


        /// <summary>
        /// sorszám
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_Idő_Tábla Adat)
        {
            try
            {
                string szöveg = $"UPDATE időtábla Set ";
                szöveg += $"reggel='{Adat.Reggel}' ";
                szöveg += $"where sorszám= '{Adat.Sorszám}' ";
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


    public class Kezelő_Kiegészítő_Reklám
    {
        public List<Adat_Kiegészítő_Reklám> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Reklám> Adatok = new List<Adat_Kiegészítő_Reklám>();
            Adat_Kiegészítő_Reklám Adat;

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
                                Adat = new Adat_Kiegészítő_Reklám(
                                     rekord["méret"].ToStrTrim()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Reklám Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO reklámtábla ( méret ) ";
                szöveg += $"VALUES ('{Adat.Méret}')";
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
        /// méret
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Törlés(string hely, string jelszó, Adat_Kiegészítő_Reklám Adat)
        {
            try
            {
                string szöveg = $"DELETE  FROM reklámtábla WHERE méret='{Adat.Méret}'";
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

    public class Kezelő_Kiegészítő_Forte_Vonal
    {
        public List<Adat_Kiegészítő_Forte_Vonal> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Forte_Vonal> Adatok = new List<Adat_Kiegészítő_Forte_Vonal>();
            Adat_Kiegészítő_Forte_Vonal Adat;

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
                                Adat = new Adat_Kiegészítő_Forte_Vonal(
                                     rekord["ForteVonal"].ToStrTrim()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Forte_Vonal Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO fortevonal (fortevonal) ";
                szöveg += $"VALUES ('{Adat.ForteVonal}')";
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
        /// fortevonal
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Törlés(string hely, string jelszó, Adat_Kiegészítő_Forte_Vonal Adat)
        {
            try
            {
                string szöveg = $"DELETE  FROM fortevonal WHERE fortevonal='{Adat.ForteVonal}'";
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
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;


namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_kiegészítő_telephely
    {
        public List<Adat_kiegészítő_telephely> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_kiegészítő_telephely> Adatok = new List<Adat_kiegészítő_telephely>();
            Adat_kiegészítő_telephely Adat;

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
                                Adat = new Adat_kiegészítő_telephely(
                                    rekord["sorszám"].ToÉrt_Long(),
                                    rekord["Telephelynév"].ToStrTrim(),
                                    rekord["Telephelykönyvtár"].ToStrTrim(),
                                    rekord["Fortekód"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_kiegészítő_telephely Adat)
        {
            try
            {
                string szöveg = "INSERT INTO telephelytábla ( sorszám, telephelynév, telephelykönyvtár, fortekód )";
                szöveg += $" VALUES ({Adat.Sorszám}, ";
                szöveg += $"'{Adat.Telephelynév}', ";
                szöveg += $"'{Adat.Telephelykönyvtár}', ";
                szöveg += $"'{Adat.Fortekód}')";
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
        public void Módosítás(string hely, string jelszó, Adat_kiegészítő_telephely Adat)
        {
            try
            {
                string szöveg = $"UPDATE telephelytábla SET ";
                szöveg += $" telephelykönyvtár='{Adat.Telephelykönyvtár}', ";
                szöveg += $" fortekód='{Adat.Fortekód}' ";
                szöveg += $" WHERE telephelynév='{Adat.Telephelynév}' and sorszám={Adat.Sorszám}";
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
        public void Törlés(string hely, string jelszó, Adat_kiegészítő_telephely Adat)
        {
            try
            {
                string szöveg = $"DELETE FROM telephelytábla where telephelynév='{Adat.Telephelynév}' and sorszám={Adat.Sorszám}";
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

    public class Kezelő_kiegészítő_Hibaterv
    {
        public List<Adat_Kiegészítő_Hibaterv> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Hibaterv> Adatok = new List<Adat_Kiegészítő_Hibaterv>();
            Adat_Kiegészítő_Hibaterv Adat;

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
                                Adat = new Adat_Kiegészítő_Hibaterv(
                                    rekord["id"].ToÉrt_Long(),
                                    rekord["szöveg"].ToStrTrim(),
                                    rekord["főkönyv"].ToÉrt_Bool()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Hibaterv Adat)
        {
            string szöveg = $"INSERT INTO hibaterv (id , szöveg, főkönyv ) ";
            szöveg += $" VALUES ({Adat.Id}, ";
            szöveg += $"'{Adat.Szöveg}', ";
            szöveg += $"{Adat.Főkönyv})";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        /// <summary>
        /// id
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Mósosítás(string hely, string jelszó, Adat_Kiegészítő_Hibaterv Adat)
        {
            try
            {
                string szöveg = $"UPDATE hibaterv SET ";
                szöveg += $"főkönyv={Adat.Főkönyv}, ";
                szöveg += $"szöveg='{Adat.Szöveg}' ";
                szöveg += $"WHERE id={Adat.Id}";
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
        public void Törlés(string hely, string jelszó, Adat_Kiegészítő_Hibaterv Adat)
        {
            try
            {
                string szöveg = $"DELETE * FROM hibaterv where id={Adat.Id}";
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

    public class Kezelő_Kiegészítő_Takarítás
    {
        public List<string> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<string> Adatok = new List<string>();

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
                                Adatok.Add(rekord["típus"].ToString());
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }

    public class Kezelő_Kiegészítő_főkönyvtábla
    {
        public List<Adat_Kiegészítő_főkönyvtábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_főkönyvtábla> Adatok = new List<Adat_Kiegészítő_főkönyvtábla>();
            Adat_Kiegészítő_főkönyvtábla Adat;

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
                                Adat = new Adat_Kiegészítő_főkönyvtábla(
                                          rekord["id"].ToÉrt_Long(),
                                          rekord["név"].ToStrTrim(),
                                          rekord["beosztás"].ToStrTrim()
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
        /// Id
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_főkönyvtábla Adat)
        {
            string szöveg = $"UPDATE Főkönyvtábla SET név='{Adat.Név}',";
            szöveg += $" beosztás='{Adat.Beosztás}'";
            szöveg += $" WHERE id={Adat.Id} ";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }


    }

    public class Kezelő_Kiegészítő_Feortipus
    {
        public List<Adat_Kiegészítő_Feortipus> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Feortipus> Adatok = new List<Adat_Kiegészítő_Feortipus>();
            Adat_Kiegészítő_Feortipus Adat;

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
                                Adat = new Adat_Kiegészítő_Feortipus(
                                          rekord["típus"].ToStrTrim(),
                                          rekord["ftípus"].ToStrTrim()
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

    public class Kezelő_Kiegészítő_Felmentés
    {
        public List<Adat_Kiegészítő_Felmentés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Felmentés> Adatok = new List<Adat_Kiegészítő_Felmentés>();
            Adat_Kiegészítő_Felmentés Adat;

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
                                Adat = new Adat_Kiegészítő_Felmentés(
                                        rekord["id"].ToÉrt_Int(),
                                        rekord["Címzett"].ToStrTrim(),
                                        rekord["Másolat"].ToStrTrim(),
                                        rekord["Tárgy"].ToStrTrim(),
                                        rekord["Kértvizsgálat"].ToStrTrim(),
                                        rekord["Bevezetés"].ToStrTrim(),
                                        rekord["Tárgyalás"].ToStrTrim(),
                                        rekord["Befejezés"].ToStrTrim(),
                                        rekord["CiklusTípus"].ToStrTrim()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kiegészítő_Felmentés Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Felmentés Adat = null;

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
                                Adat = new Adat_Kiegészítő_Felmentés(
                                        rekord["id"].ToÉrt_Int(),
                                        rekord["Címzett"].ToString(),
                                        rekord["Másolat"].ToStrTrim(),
                                        rekord["Tárgy"].ToStrTrim(),
                                        rekord["Kértvizsgálat"].ToStrTrim(),
                                        rekord["Bevezetés"].ToStrTrim(),
                                        rekord["Tárgyalás"].ToStrTrim(),
                                        rekord["Befejezés"].ToStrTrim(),
                                        rekord["CiklusTípus"].ToStrTrim()
                                          );
                            }
                        }
                    }
                }
            }
            return Adat;
        }

        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_Felmentés Adat)
        {
            try
            {
                string szöveg = $"UPDATE Felmentés SET ";
                szöveg += $"Címzett='{Adat.Címzett}', ";
                szöveg += $"Másolat='{Adat.Másolat}', ";
                szöveg += $"Tárgy='{Adat.Tárgy}', ";
                szöveg += $"Kértvizsgálat='{Adat.Kértvizsgálat}', ";
                szöveg += $"Bevezetés='{Adat.Bevezetés}', ";
                szöveg += $"Tárgyalás='{Adat.Tárgyalás}', ";
                szöveg += $"Befejezés='{Adat.Befejezés}', ";
                szöveg += $"CiklusTípus='{Adat.CiklusTípus}' ";
                szöveg += $" WHERE id={Adat.Id} ";
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


    public class Kezelő_Kiegészítő_Csoportbeosztás
    {
        readonly string jelszó = "Mocó";
        public List<Adat_Kiegészítő_Csoportbeosztás> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Csoportbeosztás> Adatok = new List<Adat_Kiegészítő_Csoportbeosztás>();
            Adat_Kiegészítő_Csoportbeosztás Adat;

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
                                Adat = new Adat_Kiegészítő_Csoportbeosztás(
                                        rekord["Sorszám"].ToÉrt_Long(),
                                        rekord["Csoportbeosztás"].ToStrTrim(),
                                        rekord["Típus"].ToStrTrim()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    
        public List<Adat_Kiegészítő_Csoportbeosztás> Lista_Adatok(string hely)
        {
            string szöveg = "SELECT * FROM csoportbeosztás order by sorszám";
            List<Adat_Kiegészítő_Csoportbeosztás> Adatok = new List<Adat_Kiegészítő_Csoportbeosztás>();
            Adat_Kiegészítő_Csoportbeosztás Adat;

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
                                Adat = new Adat_Kiegészítő_Csoportbeosztás(
                                        rekord["Sorszám"].ToÉrt_Long(),
                                        rekord["Csoportbeosztás"].ToStrTrim(),
                                        rekord["Típus"].ToStrTrim()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Csoportbeosztás Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO csoportbeosztás (sorszám, csoportbeosztás, típus) ";
                szöveg += $"VALUES ({Adat.Sorszám}, ";
                szöveg += $"'{Adat.Csoportbeosztás}', ";
                szöveg += $"'{Adat.Típus}' )";
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
        /// csoportbeosztás
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_Csoportbeosztás Adat)
        {
            try
            {
                string szöveg = " UPDATE csoportbeosztás SET ";
                szöveg += $" típus='{Adat.Típus}'";
                szöveg += $" WHERE csoportbeosztás='{Adat.Csoportbeosztás}'";
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


        public void Módosítás(string hely, string jelszó, List<Adat_Kiegészítő_Csoportbeosztás> Adat)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Kiegészítő_Csoportbeosztás rekord in Adat)
                {
                    string szöveg = " UPDATE csoportbeosztás SET ";
                    szöveg += $" típus='{rekord.Típus}'";
                    szöveg += $" WHERE csoportbeosztás='{rekord.Csoportbeosztás}'";
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

    public class Kezelő_Kiegészítő_Beosztáskódok
    {
        public List<string> Lista_AdatBeoKód(string hely, string jelszó, string szöveg)
        {
            List<string> Adatok = new List<string>();
            string Adat;

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
                                Adat = rekord["beosztáskód"].ToStrTrim();
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Kiegészítő_Beosztáskódok> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Beosztáskódok> Adatok = new List<Adat_Kiegészítő_Beosztáskódok>();
            Adat_Kiegészítő_Beosztáskódok Adat;

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
                                Adat = new Adat_Kiegészítő_Beosztáskódok(
                                        rekord["Sorszám"].ToÉrt_Long(),
                                        rekord["Beosztáskód"].ToStrTrim(),
                                        rekord["Munkaidőkezdet"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidővége"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidő"].ToÉrt_Int(),
                                        rekord["Munkarend"].ToÉrt_Int(),
                                        rekord["Napszak"].ToStrTrim(),
                                        rekord["Éjszakás"].ToÉrt_Bool(),
                                        rekord["Számoló"].ToÉrt_Bool(),
                                        rekord["0"].ToÉrt_Int(),
                                        rekord["1"].ToÉrt_Int(),
                                        rekord["2"].ToÉrt_Int(),
                                        rekord["3"].ToÉrt_Int(),
                                        rekord["4"].ToÉrt_Int(),
                                        rekord["5"].ToÉrt_Int(),
                                        rekord["6"].ToÉrt_Int(),
                                        rekord["7"].ToÉrt_Int(),
                                        rekord["8"].ToÉrt_Int(),
                                        rekord["9"].ToÉrt_Int(),
                                        rekord["10"].ToÉrt_Int(),
                                        rekord["11"].ToÉrt_Int(),
                                        rekord["12"].ToÉrt_Int(),
                                        rekord["13"].ToÉrt_Int(),
                                        rekord["14"].ToÉrt_Int(),
                                        rekord["15"].ToÉrt_Int(),
                                        rekord["16"].ToÉrt_Int(),
                                        rekord["17"].ToÉrt_Int(),
                                        rekord["18"].ToÉrt_Int(),
                                        rekord["19"].ToÉrt_Int(),
                                        rekord["20"].ToÉrt_Int(),
                                        rekord["21"].ToÉrt_Int(),
                                        rekord["22"].ToÉrt_Int(),
                                        rekord["23"].ToÉrt_Int(),
                                        rekord["Magyarázat"].ToStrTrim()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public Adat_Kiegészítő_Beosztáskódok Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Kiegészítő_Beosztáskódok Adat = null;

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

                            Adat = new Adat_Kiegészítő_Beosztáskódok(
                                        rekord["Sorszám"].ToÉrt_Long(),
                                        rekord["Beosztáskód"].ToStrTrim(),
                                        rekord["Munkaidőkezdet"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidővége"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidő"].ToÉrt_Int(),
                                        rekord["Munkarend"].ToÉrt_Int(),
                                        rekord["Napszak"].ToStrTrim(),
                                        rekord["Éjszakás"].ToÉrt_Bool(),
                                        rekord["Számoló"].ToÉrt_Bool(),
                                        rekord["0"].ToÉrt_Int(),
                                        rekord["1"].ToÉrt_Int(),
                                        rekord["2"].ToÉrt_Int(),
                                        rekord["3"].ToÉrt_Int(),
                                        rekord["4"].ToÉrt_Int(),
                                        rekord["5"].ToÉrt_Int(),
                                        rekord["6"].ToÉrt_Int(),
                                        rekord["7"].ToÉrt_Int(),
                                        rekord["8"].ToÉrt_Int(),
                                        rekord["9"].ToÉrt_Int(),
                                        rekord["10"].ToÉrt_Int(),
                                        rekord["11"].ToÉrt_Int(),
                                        rekord["12"].ToÉrt_Int(),
                                        rekord["13"].ToÉrt_Int(),
                                        rekord["14"].ToÉrt_Int(),
                                        rekord["15"].ToÉrt_Int(),
                                        rekord["16"].ToÉrt_Int(),
                                        rekord["17"].ToÉrt_Int(),
                                        rekord["18"].ToÉrt_Int(),
                                        rekord["19"].ToÉrt_Int(),
                                        rekord["20"].ToÉrt_Int(),
                                        rekord["21"].ToÉrt_Int(),
                                        rekord["22"].ToÉrt_Int(),
                                        rekord["23"].ToÉrt_Int(),
                                        rekord["Magyarázat"].ToStrTrim()
                                      );
                        }
                    }
                }
            }
            return Adat;
        }
    }


    public class Kezelő_Kiegészítő_Szabadságok
    {

        public List<Adat_Kiegészítő_Szabadságok> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Szabadságok> Adatok = new List<Adat_Kiegészítő_Szabadságok>();
            Adat_Kiegészítő_Szabadságok Adat;

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
                                Adat = new Adat_Kiegészítő_Szabadságok(
                                        rekord["Sorszám"].ToÉrt_Long(),
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

        public Adat_Kiegészítő_Szabadságok Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Kiegészítő_Szabadságok Adat = null;

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

                            Adat = new Adat_Kiegészítő_Szabadságok(
                                    rekord["Sorszám"].ToÉrt_Long(),
                                    rekord["Megnevezés"].ToStrTrim()
                                      );
                        }
                    }
                }
            }
            return Adat;
        }
    }


    public class Kezelő_Kiegészítő_Jelenlétiív
    {
        public List<Adat_Kiegészítő_Jelenlétiív> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Jelenlétiív> Adatok = new List<Adat_Kiegészítő_Jelenlétiív>();
            Adat_Kiegészítő_Jelenlétiív Adat;

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
                                Adat = new Adat_Kiegészítő_Jelenlétiív(
                                        rekord["id"].ToÉrt_Long(),
                                        rekord["Szervezet"].ToStrTrim()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kiegészítő_Jelenlétiív Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Kiegészítő_Jelenlétiív Adat = null;

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

                            Adat = new Adat_Kiegészítő_Jelenlétiív(
                                    rekord["id"].ToÉrt_Long(),
                                    rekord["Szervezet"].ToStrTrim()
                                      );
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Kiegészítő_Igen_Nem
    {
        public List<Adat_Kiegészítő_Igen_Nem> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Igen_Nem> Adatok = new List<Adat_Kiegészítő_Igen_Nem>();
            Adat_Kiegészítő_Igen_Nem Adat;

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
                                Adat = new Adat_Kiegészítő_Igen_Nem(
                                        rekord["id"].ToÉrt_Long(),
                                        rekord["Válasz"].ToÉrt_Bool(),
                                        rekord["Megjegyzés"].ToStrTrim()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kiegészítő_Igen_Nem Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Kiegészítő_Igen_Nem Adat = null;

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

                            Adat = new Adat_Kiegészítő_Igen_Nem(
                                    rekord["id"].ToÉrt_Long(),
                                    rekord["Válasz"].ToÉrt_Bool(),
                                    rekord["Megjegyzés"].ToStrTrim()
                                      );
                        }
                    }
                }
            }
            return Adat;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Igen_Nem Adat)
        {
            try
            {
                string szöveg = "INSERT INTO igen_nem  (id, válasz, megjegyzés) ";
                szöveg += $"VALUES ({Adat.Id}, ";
                szöveg += $"{Adat.Válasz}, ";
                szöveg += $"'{Adat.Megjegyzés}')";
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
        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_Igen_Nem Adat)
        {
            try
            {
                string szöveg = $"UPDATE igen_nem SET Válasz={Adat.Válasz} ";
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

    public class Kezelő_Telep_Kieg_Fortetípus
    {
        public List<Adat_Telep_Kieg_Fortetípus> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Telep_Kieg_Fortetípus> Adatok = new List<Adat_Telep_Kieg_Fortetípus>();
            Adat_Telep_Kieg_Fortetípus Adat;

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
                                Adat = new Adat_Telep_Kieg_Fortetípus(
                                        rekord["típus"].ToStrTrim(),
                                        rekord["ftípus"].ToStrTrim()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Telep_Kieg_Fortetípus Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO fortetipus (típus, ftípus) ";
                szöveg += $"VALUES ('{Adat.Típus}',";
                szöveg += $" '{Adat.Ftípus}')";
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
        /// típus, ftípus
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Törlés(string hely, string jelszó, Adat_Telep_Kieg_Fortetípus Adat)
        {
            try
            {
                string szöveg = $"DELETE * FROM fortetipus where típus='{Adat.Típus}' and ftípus='{Adat.Ftípus}'";
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

    public class Kezelő_Telep_Kiegészítő_SérülésCaf
    {
        public List<Adat_Telep_Kiegészítő_SérülésCaf> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Telep_Kiegészítő_SérülésCaf> Adatok = new List<Adat_Telep_Kiegészítő_SérülésCaf>();
            Adat_Telep_Kiegészítő_SérülésCaf Adat;

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
                                Adat = new Adat_Telep_Kiegészítő_SérülésCaf(
                                        rekord["Id"].ToÉrt_Int(),
                                        rekord["Cég"].ToStrTrim(),
                                        rekord["Név"].ToStrTrim(),
                                        rekord["Beosztás"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
        public Adat_Telep_Kiegészítő_SérülésCaf Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Telep_Kiegészítő_SérülésCaf Adat = null;

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

                            Adat = new Adat_Telep_Kiegészítő_SérülésCaf(
                                    rekord["Id"].ToÉrt_Int(),
                                        rekord["Cég"].ToStrTrim(),
                                        rekord["Név"].ToStrTrim(),
                                        rekord["Beosztás"].ToStrTrim());


                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Telep_Kiegészítő_Kidobó
    {

        public Adat_Telep_Kiegészítő_Kidobó Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Telep_Kiegészítő_Kidobó Adat = null;

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
                            Adat = new Adat_Telep_Kiegészítő_Kidobó(
                                                        rekord["Id"].ToÉrt_Long(),
                                                        rekord["Telephely"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Telep_Kiegészítő_Kidobó Adat)
        {
            string szöveg = $"INSERT INTO kidobó (id, telephely)";
            szöveg += $"VALUES ('{Adat.Id}',";
            szöveg += $"'{Adat.Telephely})'";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        /// <summary>
        /// id
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Telep_Kiegészítő_Kidobó Adat)
        {
            string szöveg = $"UPDATE kidobó SET ";
            szöveg += $"telephely='{Adat.Telephely}'";
            szöveg += $"WHERE id= '{Adat.Id}'";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }


    }


    public class Kezelő_Telep_Kiegészítő_SAP
    {
        public Adat_Telep_Kiegészítő_SAP Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Telep_Kiegészítő_SAP Adat = null;

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
                            Adat = new Adat_Telep_Kiegészítő_SAP(
                                                        rekord["Id"].ToÉrt_Long(),
                                                        rekord["Felelősmunkahely"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Telep_Kiegészítő_SAP Adat)
        {
            string szöveg = $"INSERT INTO sapmunkahely (id, felelősmunkahely)";
            szöveg += $"VALUES ({Adat.Id} ,'";
            szöveg += $"'{Adat.Felelősmunkahely}')";
            MyA.ABMódosítás(hely, jelszó, szöveg);

        }
        /// <summary>
        /// id
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Telep_Kiegészítő_SAP Adat)
        {
            string szöveg = $"UPDATE sapmunkahely SET ";
            szöveg += $"felelősmunkahely='{Adat.Felelősmunkahely}'";
            szöveg += $"WHERE id= '{Adat.Id}'";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

    }

    public class Kezelő_Telep_Kiegészítő_E3típus
    {
        public Adat_Telep_Kiegészítő_E3típus Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Telep_Kiegészítő_E3típus Adat = null;

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
                            Adat = new Adat_Telep_Kiegészítő_E3típus(
                                                        rekord["Típus"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }

        /// <summary>
        /// típus
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Törlés(string hely, string jelszó, Adat_Telep_Kiegészítő_E3típus Adat)
        {
            try
            {
                string szöveg = $"DELETE * FROM E3típus WHERE típus='{Adat.Típus}'";
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


        public void Rögzítés(string hely, string jelszó, Adat_Telep_Kiegészítő_E3típus Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO E3típus ( típus ) VALUES ('{Adat.Típus}')";
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

    public class Kezelő_Telep_Kiegészítő_Takarítástípus
    {
        public Adat_Telep_Kiegészítő_Takarítástípus Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Telep_Kiegészítő_Takarítástípus Adat = null;

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
                            Adat = new Adat_Telep_Kiegészítő_Takarítástípus(
                                                        rekord["Típus"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }

        /// <summary>
        /// típus
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Törlés(string hely, string jelszó, Adat_Telep_Kiegészítő_Takarítástípus Adat)
        {
            try
            {
                string szöveg = $"DELETE * FROM takarítástípus WHERE típus='{Adat.Típus}'";
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

        public void Rögzítés(string hely, string jelszó, Adat_Telep_Kiegészítő_Takarítástípus Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO takarítástípus (típus)  VALUES ('{Adat.Típus}')";
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





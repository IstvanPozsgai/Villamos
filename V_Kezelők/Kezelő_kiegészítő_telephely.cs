using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;


namespace Villamos.Kezelők
{
    public class Kezelő_kiegészítő_telephely
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb";
        readonly string jelszó = "Mocó";

        public Kezelő_kiegészítő_telephely()
        {
            //nincs elkészítve
            // if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Adatok_Napló(hely.KönyvSzerk());
        }

        public List<Adat_kiegészítő_telephely> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM telephelytábla order by sorszám";
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

        public void Rögzítés(Adat_kiegészítő_telephely Adat)
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

        public void Módosítás(Adat_kiegészítő_telephely Adat)
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

        public void Törlés(Adat_kiegészítő_telephely Adat)
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

        public void Csere(long sor1, long sor2)
        {
            try
            {
                string szöveg = $"UPDATE telephelytábla SET sorszám='{0}' WHERE  sorszám={sor1}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                szöveg = $"UPDATE telephelytábla SET sorszám='{sor1}' WHERE  sorszám={sor2}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                szöveg = $"UPDATE telephelytábla SET sorszám='{sor2}' WHERE  sorszám={0}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                szöveg = $"DELETE FROM telephelytábla where  sorszám={0}";
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

}





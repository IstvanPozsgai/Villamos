using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;


namespace Villamos.Kezelők
{
    public class Kezelő_Váltós_Váltóstábla
    {
        readonly string jelszó = "katalin";
        string hely;

        public List<Adat_Váltós_Váltóstábla> Lista_Adatok(int Dátum)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum}\munkaidőnaptár.mdb".KönyvSzerk();

            string szöveg = "SELECT * FROM Váltóstábla  ORDER BY telephely, év, félév, csoport";
            List<Adat_Váltós_Váltóstábla> Adatok = new List<Adat_Váltós_Váltóstábla>();
            Adat_Váltós_Váltóstábla Adat;

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
                                Adat = new Adat_Váltós_Váltóstábla(
                                          rekord["Telephely"].ToStrTrim(),
                                          rekord["Csoport"].ToStrTrim(),
                                          rekord["Év"].ToÉrt_Int(),
                                          rekord["Félév"].ToÉrt_Int(),
                                          rekord["ZKnap"].ToÉrt_Double(),
                                          rekord["Epnap"].ToÉrt_Double(),
                                          rekord["Tperc"].ToÉrt_Double()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(int Dátum, Adat_Váltós_Váltóstábla Adat)
        {
            try
            {
                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum}\munkaidőnaptár.mdb".KönyvSzerk();

                string szöveg = "INSERT INTO váltóstábla (év, félév, csoport, ZKnap, EPnap, Tperc, telephely ) VALUES (";
                szöveg += $" VALUES ({Adat.Év},";
                szöveg += $"{Adat.Félév}, ";
                szöveg += $"'{Adat.Csoport}', ";
                szöveg += $"{Adat.Zknap}, ";
                szöveg += $"{Adat.Epnap}, ";
                szöveg += $"{Adat.Tperc}, ";
                szöveg += $"'{Adat.Telephely}' )";

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

        public void Módosítás(int Dátum, Adat_Váltós_Váltóstábla Adat)
        {
            try
            {
                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum}\munkaidőnaptár.mdb".KönyvSzerk();
                string szöveg = " UPDATE  váltóstábla SET ";
                szöveg += $" ZKnap={Adat.Zknap}, ";
                szöveg += $" EPnap={Adat.Epnap}, ";
                szöveg += $" Tperc={Adat.Tperc} ";
                szöveg += $" WHERE  év={Adat.Év}";
                szöveg += $" and félév={Adat.Félév}";
                szöveg += $" and csoport='{Adat.Csoport}'";
                szöveg += $" and telephely='{Adat.Telephely}'";

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

        public void Törlés(int Dátum, Adat_Váltós_Váltóstábla Adat)
        {
            try
            {
                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum}\munkaidőnaptár.mdb".KönyvSzerk();
                string szöveg = $"DELETE FROM váltóstábla where év={Adat.Év}";
                szöveg += $" and félév={Adat.Félév}";
                szöveg += $" and csoport='{Adat.Csoport}'";
                szöveg += $" and telephely='{Adat.Telephely}'";
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
}

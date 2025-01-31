using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_főkönyvtábla
    {
        readonly string jelszó = "Mocó";

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

        public List<Adat_Kiegészítő_főkönyvtábla> Lista_Adatok(string Telephely)
        {
            List<Adat_Kiegészítő_főkönyvtábla> Adatok = new List<Adat_Kiegészítő_főkönyvtábla>();
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely}\adatok\segéd\Kiegészítő.mdb".KönyvSzerk();
                string szöveg = "SELECT * FROM Főkönyvtábla";

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
            return Adatok;
        }

        public void Módosítás(string Telephely, Adat_Kiegészítő_főkönyvtábla Adat)
        {
            try
            {
                string szöveg = $"UPDATE Főkönyvtábla SET név='{Adat.Név}',";
                szöveg += $" beosztás='{Adat.Beosztás}'";
                szöveg += $" WHERE id={Adat.Id} ";
                string hely = $@"{Application.StartupPath}\{Telephely}\adatok\segéd\Kiegészítő.mdb".KönyvSzerk();
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

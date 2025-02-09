using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Túlórakeret
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő1.mdb";
        readonly string jelszó = "Mocó";

        public Kezelő_Kiegészítő_Túlórakeret()
        {
            //nincs elkészítve
            // if (!File.Exists(hely)) Adatbázis_Létrehozás   (hely.KönyvSzerk());
        }

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

        public List<Adat_Kiegészítő_Túlórakeret> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM túlórakeret  ORDER BY telephely, határ";
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

        public void Rögzítés(Adat_Kiegészítő_Túlórakeret Adat)
        {
            try
            {
                string szöveg = "INSERT INTO túlórakeret (határ, telephely, parancs ) VALUES (";
                szöveg += $"{Adat.Határ}, ";
                szöveg += $"'{Adat.Telephely}', ";
                szöveg += $"{Adat.Parancs} )";

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

        public void Módosítás(Adat_Kiegészítő_Túlórakeret Adat)
        {
            try
            {
                string szöveg = " UPDATE  túlórakeret SET ";
                szöveg += $" parancs={Adat.Parancs} ";
                szöveg += $" WHERE határ={Adat.Határ} AND telephely='{Adat.Telephely}'";

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

        public void Törlés(Adat_Kiegészítő_Túlórakeret Adat)
        {
            try
            {
                string szöveg = $"DELETE FROM túlórakeret WHERE határ={Adat.Határ} AND telephely='{Adat.Telephely}'";
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

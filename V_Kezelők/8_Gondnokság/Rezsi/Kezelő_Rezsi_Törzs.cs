using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Rezsi_Törzs
    {
        readonly string jelszó = "csavarhúzó";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Rezsi\rezsitörzs.mdb";
        readonly string Táblanév = "törzs";

        public Kezelő_Rezsi_Törzs()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Rezsitörzs(hely.KönyvSzerk());
        }

        public List<Adat_Rezsi_Törzs> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {Táblanév} ORDER BY Azonosító";
            List<Adat_Rezsi_Törzs> Adatok = new List<Adat_Rezsi_Törzs>();
            Adat_Rezsi_Törzs Adat;

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
                                Adat = new Adat_Rezsi_Törzs(
                                       rekord["Azonosító"].ToStrTrim(),
                                       rekord["Megnevezés"].ToStrTrim(),
                                       rekord["Méret"].ToStrTrim(),
                                       rekord["státus"].ToÉrt_Int(),
                                       rekord["Csoport"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Rezsi_Törzs Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {Táblanév} (azonosító, megnevezés, Méret, státus, csoport ) VALUES (";
                szöveg += $"'{Adat.Azonosító}', ";
                szöveg += $"'{Adat.Megnevezés}', ";
                szöveg += $"'{Adat.Méret}', ";
                szöveg += $"{Adat.Státusz}, ";
                szöveg += $"'{Adat.Csoport}') ";
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

        public void Módosítás(Adat_Rezsi_Törzs Adat)
        {
            try
            {
                string szöveg = $"UPDATE {Táblanév} SET ";
                szöveg += $"megnevezés='{Adat.Megnevezés}', ";
                szöveg += $"Méret='{Adat.Méret}', ";
                szöveg += $"csoport='{Adat.Csoport}', ";
                szöveg += $"státus={Adat.Státusz} ";
                szöveg += $" WHERE  azonosító='{Adat.Azonosító}'";
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

        public void Nagybetűs()
        {
            try
            {
                List<Adat_Rezsi_Törzs> Adatok = Lista_Adatok();
                foreach (Adat_Rezsi_Törzs rekord in Adatok)
                {
                    if (rekord.Azonosító != rekord.Azonosító.ToUpper())
                    {
                        Adat_Rezsi_Törzs Adat = new Adat_Rezsi_Törzs(
                                                rekord.Azonosító.ToUpper(),
                                                rekord.Megnevezés,
                                                rekord.Méret,
                                                rekord.Státusz,
                                                rekord.Csoport);
                        Rögzítés(Adat);

                        string szöveg = $"DELETE FROM {Táblanév} WHERE Azonosító='{rekord.Azonosító}'";
                        MyA.ABtörlés(hely, jelszó, szöveg);
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
        }
    }
}

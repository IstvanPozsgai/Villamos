using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Telep_Kieg_Fortetípus
    {
        readonly string jelszó = "Mocó";

        public List<Adat_Telep_Kieg_Fortetípus> Lista_Adatok(string Telephely)
        {
            string hely = $@"{Application.StartupPath}\{Telephely}\adatok\segéd\Kiegészítő.mdb".Ellenőrzés();
            string szöveg = "SELECT *  FROM fortetipus ORDER BY ftípus";
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

        public void Rögzítés(string Telephely, Adat_Telep_Kieg_Fortetípus Adat)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely}\adatok\segéd\Kiegészítő.mdb".Ellenőrzés();
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

        public void Törlés(string Telephely, Adat_Telep_Kieg_Fortetípus Adat)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely}\adatok\segéd\Kiegészítő.mdb".Ellenőrzés();
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

}

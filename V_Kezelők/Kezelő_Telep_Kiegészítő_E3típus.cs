using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Telep_Kiegészítő_E3típus
    {
        readonly string jelszó = "Mocó";

        public List<Adat_Telep_Kiegészítő_E3típus> Lista_Adatok(string Telephely)
        {
            string hely = $@"{Application.StartupPath}\{Telephely}\adatok\segéd\Kiegészítő.mdb".KönyvSzerk();
            string szöveg = "SELECT * FROM E3típus order by típus";
            List<Adat_Telep_Kiegészítő_E3típus> Adatok = new List<Adat_Telep_Kiegészítő_E3típus>();
            Adat_Telep_Kiegészítő_E3típus Adat;

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
                                Adat = new Adat_Telep_Kiegészítő_E3típus(
                                            rekord["típus"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Törlés(string Telephely, Adat_Telep_Kiegészítő_E3típus Adat)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely}\adatok\segéd\Kiegészítő.mdb".KönyvSzerk();
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

        public void Rögzítés(string Telephely, Adat_Telep_Kiegészítő_E3típus Adat)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely}\adatok\segéd\Kiegészítő.mdb".KönyvSzerk();
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
}

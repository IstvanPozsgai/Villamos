using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Jármű_Állomány_Típus
    {
        readonly string jelszó = "pozsgaii";

        public List<Adat_Jármű_Állomány_Típus> Lista_Adatok(string Telephely)
        {
            string hely = $@"{Application.StartupPath}\{Telephely}\adatok\villamos\Jármű.mdb".KönyvSzerk();
            string szöveg = "Select * FROM típustábla order by id";

            List<Adat_Jármű_Állomány_Típus> Adatok = new List<Adat_Jármű_Állomány_Típus>();
            Adat_Jármű_Állomány_Típus Adat;
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
                                Adat = new Adat_Jármű_Állomány_Típus(
                                            rekord["Id"].ToÉrt_Long(),
                                            rekord["Állomány"].ToÉrt_Long(),
                                            rekord["típus"].ToStrTrim()
                                            );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, Adat_Jármű_Állomány_Típus Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO típustábla (id, típus, állomány)";
                szöveg += $" VALUES ({Adat.Id},";
                szöveg += $" '{Adat.Típus}',";
                szöveg += $" {Adat.Állomány} )";
                string hely = $@"{Application.StartupPath}\{Telephely}\adatok\villamos\Jármű.mdb".KönyvSzerk();
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

        public void Törlés(string Telephely, Adat_Jármű_Állomány_Típus Adat)
        {
            try
            {
                string szöveg = $"DELETE FROM típustábla WHERE típus='{Adat.Típus}'";
                string hely = $@"{Application.StartupPath}\{Telephely}\adatok\villamos\Jármű.mdb".KönyvSzerk();
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

        public void Módosítás(string Telephely, Adat_Jármű_Állomány_Típus Adat)
        {
            try
            {
                string szöveg = $"Update típustábla SET ";
                szöveg += $"id = '{Adat.Id}' ";
                szöveg += $"WHERE típus = '{Adat.Típus}'";
                string hely = $@"{Application.StartupPath}\{Telephely}\adatok\villamos\Jármű.mdb".KönyvSzerk();
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

using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Jelenlétiív
    {
        readonly string jelszó = "Mocó";
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

        public List<Adat_Kiegészítő_Jelenlétiív> Lista_Adatok(string Telephely)
        {
            string hely = $@"{Application.StartupPath}\{Telephely}\Adatok\segéd\Kiegészítő.mdb".Ellenőrzés();
            string szöveg = "SELECT * FROM jelenlétiív ORDER BY id";
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

        public void Rögzítés(string Telephely, Adat_Kiegészítő_Jelenlétiív Adat)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely}\Adatok\segéd\Kiegészítő.mdb".Ellenőrzés();
                string szöveg = $"INSERT INTO jelenlétiív (id, szervezet) Values ({Adat.Id},'{Adat.Szervezet}')";
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

        public void Módosítás(string Telephely, Adat_Kiegészítő_Jelenlétiív Adat)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely}\Adatok\segéd\Kiegészítő.mdb".Ellenőrzés();
                string szöveg = $"UPDATE jelenlétiív SET szervezet='{Adat.Szervezet}' where id={Adat.Id}";
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

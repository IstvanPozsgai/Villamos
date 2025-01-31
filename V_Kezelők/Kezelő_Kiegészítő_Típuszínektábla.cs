using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Típuszínektábla
    {
        readonly string jelszó = "Mocó";

        public List<Adat_Kiegészítő_Típuszínektábla> Lista_Adatok(string Telephely)
        {
            string hely = $@"{Application.StartupPath}\{Telephely}\adatok\segéd\Kiegészítő1.mdb".KönyvSzerk();
            string szöveg = "SELECT * FROM Típuszínektábla ORDER BY  típus";
            List<Adat_Kiegészítő_Típuszínektábla> Adatok = new List<Adat_Kiegészítő_Típuszínektábla>();
            Adat_Kiegészítő_Típuszínektábla Adat;

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
                                Adat = new Adat_Kiegészítő_Típuszínektábla(
                                     rekord["típus"].ToStrTrim(),
                                     rekord["színszám"].ToÉrt_Long());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, Adat_Kiegészítő_Típuszínektábla Adat)
        {
            string hely = $@"{Application.StartupPath}\{Telephely}\adatok\segéd\Kiegészítő1.mdb".KönyvSzerk();
            string szöveg = $"INSERT INTO Típuszínektábla (típus, színszám) ";
            szöveg += $"VALUES ('{Adat.Típus}' ,";
            szöveg += $" {Adat.Színszám})";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Módosítás(string Telephely, Adat_Kiegészítő_Típuszínektábla Adat)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely}\adatok\segéd\Kiegészítő1.mdb".KönyvSzerk();
                string szöveg = $"UPDATE Típuszínektábla SET ";
                szöveg += $"színszám= '{Adat.Színszám}',";
                szöveg += $"WHERE típus='{Adat.Típus}'";
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

        public void Törlés(string Telephely, Adat_Kiegészítő_Típuszínektábla Adat)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely}\adatok\segéd\Kiegészítő1.mdb".KönyvSzerk();
                string szöveg = $"DELETE * FROM Típuszínektábla where típus='{Adat.Típus}'";
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

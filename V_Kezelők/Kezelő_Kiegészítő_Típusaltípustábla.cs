using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Típusaltípustábla
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb".Ellenőrzés();
        readonly string jelszó = "Mocó";

        public List<Adat_Kiegészítő_Típusaltípustábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Típusaltípustábla> Adatok = new List<Adat_Kiegészítő_Típusaltípustábla>();
            Adat_Kiegészítő_Típusaltípustábla Adat;

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
                                Adat = new Adat_Kiegészítő_Típusaltípustábla(
                                     rekord["sorszám"].ToÉrt_Long(),
                                     rekord["főkategória"].ToStrTrim(),
                                     rekord["típus"].ToStrTrim(),
                                     rekord["alTípus"].ToStrTrim()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Kiegészítő_Típusaltípustábla> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM típusaltípustábla order by sorszám";
            List<Adat_Kiegészítő_Típusaltípustábla> Adatok = new List<Adat_Kiegészítő_Típusaltípustábla>();
            Adat_Kiegészítő_Típusaltípustábla Adat;

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
                                Adat = new Adat_Kiegészítő_Típusaltípustábla(
                                     rekord["sorszám"].ToÉrt_Long(),
                                     rekord["főkategória"].ToStrTrim(),
                                     rekord["típus"].ToStrTrim(),
                                     rekord["alTípus"].ToStrTrim()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Kiegészítő_Típusaltípustábla Adat)
        {
            try
            {
                string szöveg = "INSERT INTO típusaltípustábla ( sorszám, Főkategória, típus, altípus )";
                szöveg += $" VALUES ({Adat.Sorszám}, ";
                szöveg += $"'{Adat.Főkategória}', ";
                szöveg += $"'{Adat.Típus}', ";
                szöveg += $"'{Adat.AlTípus}')";
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

        public void Törlés(Adat_Kiegészítő_Típusaltípustábla Adat)
        {
            try
            {
                string szöveg = $"DELETE FROM típusaltípustábla where  sorszám={Adat.Sorszám}";
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

        public void Módosítás(Adat_Kiegészítő_Típusaltípustábla Adat)
        {
            try
            {
                string szöveg = $"UPDATE típusaltípustábla SET ";
                szöveg += $"sorszám= '{Adat.Sorszám}'";
                szöveg += $"WHERE altípus='{Adat.AlTípus}'";
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

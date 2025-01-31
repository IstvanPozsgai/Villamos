using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Jármű_Takarítás_Mátrix
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Takarítás\Jármű_Takarítás.mdb".KönyvSzerk();
        readonly string jelszó = "seprűéslapát";

        public List<Adat_Jármű_Takarítás_Mátrix> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_Mátrix> Adatok = new List<Adat_Jármű_Takarítás_Mátrix>();
            Adat_Jármű_Takarítás_Mátrix Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Mátrix(
                                        rekord["id"].ToÉrt_Int(),
                                        rekord["fajta"].ToStrTrim(),
                                        rekord["fajtamásik"].ToStrTrim(),
                                        rekord["igazság"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Jármű_Takarítás_Mátrix> Lista_Adat()
        {
            string szöveg = "SELECT * FROM mátrix order by id";
            List<Adat_Jármű_Takarítás_Mátrix> Adatok = new List<Adat_Jármű_Takarítás_Mátrix>();
            Adat_Jármű_Takarítás_Mátrix Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Mátrix(
                                        rekord["id"].ToÉrt_Int(),
                                        rekord["fajta"].ToStrTrim(),
                                        rekord["fajtamásik"].ToStrTrim(),
                                        rekord["igazság"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Jármű_Takarítás_Mátrix Adat)
        {
            string szöveg = "INSERT INTO mátrix (id, fajta, fajtamásik, igazság ) VALUES (";
            szöveg += $"{Adat.Id},";
            szöveg += $"'{Adat.Fajta}', ";
            szöveg += $"'{Adat.Fajtamásik}', ";
            szöveg += $"{Adat.Igazság})";

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Módosítás(Adat_Jármű_Takarítás_Mátrix Adat)
        {
            try
            {
                string szöveg = "UPDATE mátrix  SET ";
                szöveg += $" igazság={Adat.Igazság} ";
                szöveg += $" WHERE fajta='{Adat.Fajta}' AND fajtamásik='{Adat.Fajtamásik}'";
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

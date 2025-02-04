using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Jármű_Vendég
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
        readonly string jelszó = "pozsgaii";

        public Kezelő_Jármű_Vendég()
        {
            //  if (!File.Exists(hely)) Adatbázis_Létrehozás   (hely.KönyvSzerk());
        }

        public Dictionary<string, string> Szótár(string hely, string jelszó, string szöveg)
        {
            Dictionary<string, string> SzAdat = new Dictionary<string, string>();

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
                                SzAdat.Add(
                                      rekord["Azonosító"].ToStrTrim(),
                                      rekord["kiadótelephely"].ToStrTrim()
                                      );
                            }
                        }
                    }
                }
            }
            return SzAdat;
        }


        public void Rögzítés_Vendég(string hely, string jelszó, Adat_Jármű_Vendég Adat)
        {

            string szöveg = $"SELECT * FROM vendégtábla WHERE azonosító='{Adat.Azonosító.Trim()}'";
            Adat_Jármű_Vendég EgyAdat = Egy_Adat(hely, jelszó, szöveg);
            // rögzítjük az adatot

            if (EgyAdat != null)
            {
                // Ha már létezik, akkor módosítjuk
                szöveg = "UPDATE vendégtábla  SET ";
                szöveg += $"típus='{Adat.Típus.Trim()}', "; // típus
                szöveg += $"BázisTelephely='{Adat.BázisTelephely.Trim()}', "; // BázisTelephely
                szöveg += $"KiadóTelephely='{Adat.KiadóTelephely.Trim()}' "; // KiadóTelephely
                szöveg += $" WHERE azonosító='{Adat.Azonosító.Trim()}'";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                throw new HibásBevittAdat("Az adat módosítása megtörtént.");
            }
            else
            {
                // ha nem létezik 
                szöveg = "INSERT INTO vendégtábla  (  azonosító, típus, BázisTelephely, KiadóTelephely ) VALUES (";
                szöveg += $"'{Adat.Azonosító.Trim()}', "; // azonosító
                szöveg += $"'{Adat.Típus.Trim()}', "; // típus
                szöveg += $"'{Adat.BázisTelephely.Trim()}', "; // BázisTelephely
                szöveg += $"'{Adat.KiadóTelephely.Trim()}')";

                MyA.ABMódosítás(hely, jelszó, szöveg);
                throw new HibásBevittAdat("Az adat rögzítése megtörtént.");
            }
        }

        public void Törlés_Vendég(string hely, string jelszó, Adat_Jármű_Vendég Adat)
        {

            string szöveg = $"SELECT * FROM vendégtábla WHERE azonosító='{Adat.Azonosító.Trim()}'";
            Adat_Jármű_Vendég EgyAdat = Egy_Adat(hely, jelszó, szöveg);

            if (EgyAdat != null)
            {
                szöveg = $"DELETE FROM vendégtábla WHERE azonosító='{Adat.Azonosító.Trim()}'";
                MyA.ABtörlés(hely, jelszó, szöveg);
                throw new HibásBevittAdat("Az adat törlése megtörtént.");
            }

        }

        public List<Adat_Jármű_Vendég> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Vendég> Adatok = new List<Adat_Jármű_Vendég>();
            Adat_Jármű_Vendég Adat;
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
                                Adat = new Adat_Jármű_Vendég(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Típus"].ToStrTrim(),
                                    rekord["Bázistelephely"].ToStrTrim(),
                                    rekord["Kiadótelephely"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Jármű_Vendég> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM vendégtábla order by azonosító";
            List<Adat_Jármű_Vendég> Adatok = new List<Adat_Jármű_Vendég>();
            Adat_Jármű_Vendég Adat;
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
                                Adat = new Adat_Jármű_Vendég(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Típus"].ToStrTrim(),
                                    rekord["Bázistelephely"].ToStrTrim(),
                                    rekord["Kiadótelephely"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Jármű_Vendég Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Jármű_Vendég Adat = null;
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
                            rekord.Read();

                            Adat = new Adat_Jármű_Vendég(
                                rekord["Azonosító"].ToStrTrim(),
                                rekord["Típus"].ToStrTrim(),
                                rekord["Bázistelephely"].ToStrTrim(),
                                rekord["Kiadótelephely"].ToStrTrim()
                                );
                        }
                    }
                }
            }
            return Adat;
        }
    }
}

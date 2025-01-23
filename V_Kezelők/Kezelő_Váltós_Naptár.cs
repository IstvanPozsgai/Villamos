using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos_Kezelők
{
    public class Kezelő_Váltós_Naptár
    {
        readonly string jelszó = "katalin";
        public List<Adat_Váltós_Naptár> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Váltós_Naptár> Adatok = new List<Adat_Váltós_Naptár>();
            Adat_Váltós_Naptár Adat;

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
                                Adat = new Adat_Váltós_Naptár(
                                          rekord["Nap"].ToStrTrim(),
                                          rekord["Dátum"].ToÉrt_DaTeTime()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Váltós_Naptár> Lista_Adatok(int Év, int Tábla)
        {
            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Év}\munkaidőnaptár.mdb";
            string szöveg = $"SELECT * FROM naptár{Tábla}";
            List<Adat_Váltós_Naptár> Adatok = new List<Adat_Váltós_Naptár>();
            Adat_Váltós_Naptár Adat;

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
                                Adat = new Adat_Váltós_Naptár(
                                          rekord["Nap"].ToStrTrim(),
                                          rekord["Dátum"].ToÉrt_DaTeTime()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(int Év, Adat_Váltós_Naptár Adat)
        {
            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Év}\munkaidőnaptár.mdb";
            string szöveg = "INSERT INTO naptár (nap, dátum) VALUES (";
            szöveg += $"'{Adat.Nap}', ";
            szöveg += $"'{Adat.Dátum}' )";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }


        public void Módosítás(int Év, Adat_Váltós_Naptár Adat)
        {
            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Év}\munkaidőnaptár.mdb";
            string szöveg = " UPDATE  naptár SET ";
            szöveg += $" nap='{Adat.Nap}'";
            szöveg += $" WHERE dátum= '{Adat.Dátum}'";

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

    }
}

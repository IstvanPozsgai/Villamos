using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos_Kezelők
{
    public class Kezelő_Váltós_Naptár
    {
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

        public void Rögzítés(string hely, string jelszó, Adat_Váltós_Naptár Adat)
        {
            string szöveg = "INSERT INTO naptár (nap, dátum) VALUES (";
            szöveg += $"'{Adat.Nap}', ";
            szöveg += $"'{Adat.Dátum}' )";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        /// <summary>
        /// dátum
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Váltós_Naptár Adat)
        {
            string szöveg = " UPDATE  naptár SET ";
            szöveg += $" nap='{Adat.Nap}'";
            szöveg += $" WHERE dátum= '{Adat.Dátum}'";

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public Adat_Váltós_Naptár Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Váltós_Naptár Adat = null;

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
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }
}

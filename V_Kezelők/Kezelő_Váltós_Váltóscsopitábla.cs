using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyA = Adatbázis;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Váltós_Váltóscsopitábla
    {
        public List<Adat_Váltós_Váltóscsopitábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Váltós_Váltóscsopitábla> Adatok = new List<Adat_Váltós_Váltóscsopitábla>();
            Adat_Váltós_Váltóscsopitábla Adat;

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
                                Adat = new Adat_Váltós_Váltóscsopitábla(
                                          rekord["Csoport"].ToStrTrim(),
                                          rekord["Telephely"].ToStrTrim(),
                                          rekord["Név"].ToStrTrim()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Váltós_Váltóscsopitábla Adat)
        {
            string szöveg = "INSERT INTO tábla (csoport, telephely, név) VALUES (";
            szöveg += $"'{Adat.Csoport}', ";
            szöveg += $"'{Adat.Telephely}', ";
            szöveg += $"'{Adat.Név}') ";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        /// <summary>
        /// csoport, telephely
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Váltós_Váltóscsopitábla Adat)
        {
            string szöveg = " UPDATE  tábla SET ";
            szöveg += $" név='{Adat.Név}' ";
            szöveg += $" WHERE csoport='{Adat.Csoport}' and telephely='{Adat.Telephely}'";

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }
    }
}

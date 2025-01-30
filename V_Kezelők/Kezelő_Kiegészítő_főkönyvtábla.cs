using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_főkönyvtábla
    {
        readonly string jelszó = "Mocó";

        public List<Adat_Kiegészítő_főkönyvtábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_főkönyvtábla> Adatok = new List<Adat_Kiegészítő_főkönyvtábla>();
            Adat_Kiegészítő_főkönyvtábla Adat;

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
                                Adat = new Adat_Kiegészítő_főkönyvtábla(
                                          rekord["id"].ToÉrt_Long(),
                                          rekord["név"].ToStrTrim(),
                                          rekord["beosztás"].ToStrTrim()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Kiegészítő_főkönyvtábla> Lista_Adatok(string hely)
        {
            string szöveg = "SELECT * FROM Főkönyvtábla";
            List<Adat_Kiegészítő_főkönyvtábla> Adatok = new List<Adat_Kiegészítő_főkönyvtábla>();
            Adat_Kiegészítő_főkönyvtábla Adat;

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
                                Adat = new Adat_Kiegészítő_főkönyvtábla(
                                          rekord["id"].ToÉrt_Long(),
                                          rekord["név"].ToStrTrim(),
                                          rekord["beosztás"].ToStrTrim()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        /// <summary>
        /// Id
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, Adat_Kiegészítő_főkönyvtábla Adat)
        {
            string szöveg = $"UPDATE Főkönyvtábla SET név='{Adat.Név}',";
            szöveg += $" beosztás='{Adat.Beosztás}'";
            szöveg += $" WHERE id={Adat.Id} ";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }
    }
}

using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos_Kezelők
{
    public class Kezelő_Kiegészítő_Túlórakeret
    {
        public List<Adat_Kiegészítő_Túlórakeret> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Túlórakeret> Adatok = new List<Adat_Kiegészítő_Túlórakeret>();
            Adat_Kiegészítő_Túlórakeret Adat;

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
                                Adat = new Adat_Kiegészítő_Túlórakeret(
                                     rekord["Határ"].ToÉrt_Int(),
                                     rekord["Parancs"].ToÉrt_Int(),
                                     rekord["Telephely"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Túlórakeret Adat)
        {
            string szöveg = "INSERT INTO túlórakeret (határ, telephely, parancs ) VALUES (";
            szöveg += $"'{Adat.Határ}', ";
            szöveg += $"'{Adat.Telephely}', ";
            szöveg += $"{Adat.Parancs} )";

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        /// <summary>
        /// határ, telephely
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_Túlórakeret Adat)
        {
            string szöveg = " UPDATE  túlórakeret SET ";
            szöveg += $" parancs={Adat.Parancs} ";
            szöveg += $" WHERE határ={Adat.Határ} AND telephely='{Adat.Telephely}'";

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public Adat_Kiegészítő_Túlórakeret Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Túlórakeret Adat = null;

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

                            Adat = new Adat_Kiegészítő_Túlórakeret(
                                 rekord["Határ"].ToÉrt_Int(),
                                 rekord["Parancs"].ToÉrt_Int(),
                                 rekord["Telephely"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }
    }
}

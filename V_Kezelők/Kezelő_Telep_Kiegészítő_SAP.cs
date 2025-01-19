using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Telep_Kiegészítő_SAP
    {
        readonly string jelszó = "Mocó";

        public List<Adat_Telep_Kiegészítő_SAP> Lista_Adatok(string hely)
        {
            string szöveg = "SELECT * FROM sapmunkahely";
            List<Adat_Telep_Kiegészítő_SAP> Adatok = new List<Adat_Telep_Kiegészítő_SAP>();
            Adat_Telep_Kiegészítő_SAP Adat;

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
                                Adat = new Adat_Telep_Kiegészítő_SAP(
                                                rekord["Id"].ToÉrt_Long(),
                                                rekord["Felelősmunkahely"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, Adat_Telep_Kiegészítő_SAP Adat)
        {
            string szöveg = $"INSERT INTO sapmunkahely (id, felelősmunkahely)";
            szöveg += $"VALUES ({Adat.Id}, ";
            szöveg += $"'{Adat.Felelősmunkahely}')";
            MyA.ABMódosítás(hely, jelszó, szöveg);

        }

        /// <summary>
        /// id
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, Adat_Telep_Kiegészítő_SAP Adat)
        {
            string szöveg = $"UPDATE sapmunkahely SET ";
            szöveg += $"felelősmunkahely='{Adat.Felelősmunkahely}'";
            szöveg += $"WHERE id={Adat.Id}";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

    }

}

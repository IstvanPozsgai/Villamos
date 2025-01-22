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
    public class Kezelő_Kiegészítő_Turnusok
    {
        public List<Adat_Kiegészítő_Turnusok> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Turnusok Adat;
            List<Adat_Kiegészítő_Turnusok> Adatok = new List<Adat_Kiegészítő_Turnusok>();

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
                                Adat = new Adat_Kiegészítő_Turnusok(
                                           rekord["csoport"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Turnusok Adat)
        {
            string szöveg = $"INSERT INTO turnusok (csoport)";
            szöveg += $" VALUES ('{Adat.Csoport}')";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public Adat_Kiegészítő_Turnusok Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Turnusok Adat = null;

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

                            Adat = new Adat_Kiegészítő_Turnusok(
                                       rekord["csoport"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }
    }

}

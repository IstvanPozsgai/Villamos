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
    public class Kezelő_Váltós_Összesítő
    {
        public List<Adat_Váltós_Összesítő> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Váltós_Összesítő> Adatok = new List<Adat_Váltós_Összesítő>();
            Adat_Váltós_Összesítő Adat;

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
                                Adat = new Adat_Váltós_Összesítő(
                                          rekord["Perc"].ToÉrt_Long(),
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

        public void Rögzítés(string hely, string jelszó, Adat_Váltós_Összesítő Adat)
        {
            string szöveg = "INSERT INTO összesítő (perc, dátum) VALUES (";
            szöveg += $"{Adat.Perc}, ";
            szöveg += $"'{Adat.Dátum}' )";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        /// <summary>
        /// dátum
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Váltós_Összesítő Adat)
        {
            string szöveg = " UPDATE  összesítő SET ";
            szöveg += $" perc={Adat.Perc} ";
            szöveg += $" WHERE dátum= '{Adat.Dátum}'";

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public List<Adat_Váltós_Összesítő> Lista_Adatok(string hely, string jelszó, string szöveg, string csoport)
        {
            List<Adat_Váltós_Összesítő> Adatok = new List<Adat_Váltós_Összesítő>();
            Adat_Váltós_Összesítő Adat;

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
                                Adat = new Adat_Váltós_Összesítő(
                                          rekord["Perc"].ToÉrt_Long(),
                                          rekord["Dátum"].ToÉrt_DaTeTime(),
                                          csoport
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }
}

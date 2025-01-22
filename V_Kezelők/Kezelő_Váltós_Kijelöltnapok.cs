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
    public class Kezelő_Váltós_Kijelöltnapok
    {
        public List<Adat_Váltós_Kijelöltnapok> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Váltós_Kijelöltnapok> Adatok = new List<Adat_Váltós_Kijelöltnapok>();
            Adat_Váltós_Kijelöltnapok Adat;

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
                                Adat = new Adat_Váltós_Kijelöltnapok(
                                          rekord["Telephely"].ToStrTrim(),
                                          rekord["Csoport"].ToStrTrim(),
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

        public void Rögzítés(string hely, string jelszó, Adat_Váltós_Kijelöltnapok Adat)
        {
            string szöveg = "INSERT INTO kijelöltnapok (dátum, csoport,  telephely ) VALUES ( ";
            szöveg += $"'{Adat.Dátum}', ";
            szöveg += $"'{Adat.Csoport}', ";
            szöveg += $"'{Adat.Telephely}') ";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }
    }
}

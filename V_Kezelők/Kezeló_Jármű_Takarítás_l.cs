using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos.Kezelők
{
    public class Kezeló_Jármű_Takarítás
    {
        public List<Adat_Jármű_Takarítás_Takarítások> Takarítások_Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_Takarítások> Adatok = new List<Adat_Jármű_Takarítás_Takarítások>();
            Adat_Jármű_Takarítás_Takarítások Adat;

            string kapcsolatiszöveg = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= '" + hely + "'; Jet Oledb:Database Password=" + jelszó;
            OleDbConnection Kapcsolat;
            Kapcsolat = new OleDbConnection(kapcsolatiszöveg);
            OleDbCommand Parancs = new OleDbCommand(szöveg, Kapcsolat);
            Kapcsolat.Open();
            OleDbDataReader rekord = Parancs.ExecuteReader();

            if (rekord.HasRows)
            {
                while (rekord.Read())
                {
                    Adat = new Adat_Jármű_Takarítás_Takarítások(
                            rekord["azonosító"].ToString().Trim(),
                            DateTime.Parse (rekord["dátum"].ToString()),
                            rekord["takarítási_fajta"].ToString().Trim(),
                            rekord["telephely"].ToString().Trim(),
                            int.Parse(rekord["státus"].ToString())
                            );
                    Adatok.Add(Adat);
                }
            }

            Kapcsolat.Close();
            return Adatok;

        }


    }

    
}

using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Kezelők
{
    public class Kezelő_Jármű_Xnapos
    {
        public List<Adat_Jármű_Xnapos> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Xnapos> Adatok = new List<Adat_Jármű_Xnapos>();
            Adat_Jármű_Xnapos Adat;
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
                                Adat = new Adat_Jármű_Xnapos(
                                            rekord["kezdődátum"].ToÉrt_DaTeTime(),
                                            rekord["végdátum"].ToÉrt_DaTeTime(),
                                            rekord["azonosító"].ToStrTrim(),
                                            rekord["hibaleírása"].ToStrTrim()
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

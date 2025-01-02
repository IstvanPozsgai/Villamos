using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Vezénylés
    {
        public List<Adat_Vezénylés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Vezénylés> Adatok = new List<Adat_Vezénylés>();
            Adat_Vezénylés Adat = null;

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
                                Adat = new Adat_Vezénylés(
                                        rekord["azonosító"].ToStrTrim(),
                                        DateTime.Parse(rekord["dátum"].ToString()),
                                        rekord["státus"].ToÉrt_Int(),
                                        rekord["vizsgálatraütemez"].ToÉrt_Int(),
                                        rekord["takarításraütemez"].ToÉrt_Int(),
                                        rekord["vizsgálat"].ToStrTrim(),
                                        rekord["vizsgálatszám"].ToÉrt_Int(),
                                        rekord["rendelésiszám"].ToStrTrim(),
                                        rekord["álljon"].ToÉrt_Int(),
                                        rekord["fusson"].ToÉrt_Int(),
                                        rekord["törlés"].ToÉrt_Int(),
                                        rekord["szerelvényszám"].ToÉrt_Long(),
                                        rekord["típus"].ToStrTrim()
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

using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Kezelők
{
    public class Kezelő_összevont
    {
        public List<Adat_Összevont> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Összevont> Adatok = new List<Adat_Összevont>();
            Adat_Összevont Adat;

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
                                Adat = new Adat_Összevont(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["státus"].ToÉrt_Long(),
                                    rekord["Üzem"].ToStrTrim(),
                                    rekord["miótaáll"].ToÉrt_DaTeTime (),
                                    rekord["valóstípus"].ToStrTrim(),
                                    rekord["Üzembehelyezés"].ToÉrt_DaTeTime(),
                                    rekord["Hibaleírása"].ToStrTrim()
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

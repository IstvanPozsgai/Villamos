using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Védő_Könyvelés
    {
        public List<Adat_Védő_Könyvelés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Védő_Könyvelés> Adatok = new List<Adat_Védő_Könyvelés>();
            Adat_Védő_Könyvelés Adat;

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
                                Adat = new Adat_Védő_Könyvelés(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["szerszámkönyvszám"].ToStrTrim(),
                                        rekord["mennyiség"].ToÉrt_Double(),
                                        rekord["gyáriszám"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["státus"].ToÉrt_Bool()
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

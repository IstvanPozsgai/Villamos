using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_TTP_Alapadat
    {
        public List<Adat_TTP_Alapadat> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_TTP_Alapadat> Adatok = new List<Adat_TTP_Alapadat>();
            Adat_TTP_Alapadat Adat;

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
                                Adat = new Adat_TTP_Alapadat(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Gyártási_Év"].ToÉrt_DaTeTime(),
                                        rekord["TTP"].ToÉrt_Bool(),
                                        rekord["Megjegyzés"].ToStrTrim());
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

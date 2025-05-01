using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_TW600_AlapNapló
    {
        //        if (!File.Exists(TW6000_Napló)) Adatbázis_Létrehozás.TW6000táblanapló(TW6000_Napló.Trim());
        public List<Adat_TW6000_AlapNapló> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_TW6000_AlapNapló> Adatok = new List<Adat_TW6000_AlapNapló>();
            Adat_TW6000_AlapNapló Adat;

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
                                Adat = new Adat_TW6000_AlapNapló(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Ciklusrend"].ToStrTrim(),
                                        rekord["Kötöttstart"].ToÉrt_Bool(),
                                        rekord["Megállítás"].ToÉrt_Bool(),
                                        rekord["Oka"].ToStrTrim(),
                                        rekord["Rögzítésiidő"].ToÉrt_DaTeTime(),
                                        rekord["Rögzítő"].ToStrTrim(),
                                        rekord["Start"].ToÉrt_DaTeTime(),
                                        rekord["Vizsgdátum"].ToÉrt_DaTeTime(),
                                        rekord["Vizsgnév"].ToStrTrim(),
                                        rekord["Vizsgsorszám"].ToÉrt_Int()
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

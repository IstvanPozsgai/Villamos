using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_TTP_Tábla
    {
        public List<Adat_TTP_Tábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_TTP_Tábla> Adatok = new List<Adat_TTP_Tábla>();
            Adat_TTP_Tábla Adat;

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
                                Adat = new Adat_TTP_Tábla(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Lejárat_Dátum"].ToÉrt_DaTeTime(),
                                        rekord["Ütemezés_Dátum"].ToÉrt_DaTeTime(),
                                        rekord["TTP_Dátum"].ToÉrt_DaTeTime(),
                                        rekord["TTP_Javítás"].ToÉrt_Bool(),
                                        rekord["Rendelés"].ToStrTrim(),
                                        rekord["JavBefDát"].ToÉrt_DaTeTime(),
                                        rekord["Együtt"].ToStrTrim(),
                                        rekord["Státus"].ToÉrt_Int(),
                                        rekord["Megjegyzés"].ToStrTrim()
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

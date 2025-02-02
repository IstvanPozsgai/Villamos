using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Jármű_Takarítás_Ütemező
    {
        public List<Adat_Jármű_Takarítás_Ütemező> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_Ütemező> Adatok = new List<Adat_Jármű_Takarítás_Ütemező>();
            Adat_Jármű_Takarítás_Ütemező Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Ütemező(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["Kezdő_dátum"].ToÉrt_DaTeTime(),
                                        rekord["növekmény"].ToÉrt_Int(),
                                        rekord["Mérték"].ToStrTrim(),
                                        rekord["Takarítási_fajta"].ToStrTrim(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["státus"].ToÉrt_Int()
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

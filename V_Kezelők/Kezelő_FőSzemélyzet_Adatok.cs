using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_FőSzemélyzet_Adatok
    {
        public List<Adat_Személyzet_Adatok> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Személyzet_Adatok> Adatok = new List<Adat_Személyzet_Adatok>();
            Adat_Személyzet_Adatok Adat;

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
                                Adat = new Adat_Személyzet_Adatok(
                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["szolgálat"].ToStrTrim(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["viszonylat"].ToStrTrim(),
                                    rekord["forgalmiszám"].ToStrTrim(),
                                    rekord["tervindulás"].ToÉrt_DaTeTime(),
                                    rekord["azonosító"].ToStrTrim()
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

using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_FőKiadási_adatok
    {
        public List<Adat_FőKiadási_adatok> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_FőKiadási_adatok> Adatok = new List<Adat_FőKiadási_adatok>();
            Adat_FőKiadási_adatok Adat;

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
                                Adat = new Adat_FőKiadási_adatok(
                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["forgalomban"].ToÉrt_Long(),
                                    rekord["tartalék"].ToÉrt_Long(),
                                    rekord["kocsiszíni"].ToÉrt_Long(),
                                    rekord["félreállítás"].ToÉrt_Long(),
                                    rekord["főjavítás"].ToÉrt_Long(),
                                    rekord["személyzet"].ToÉrt_Long(),
                                    rekord["kiadás"].ToÉrt_Long(),
                                    rekord["főkategória"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["altípus"].ToStrTrim(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["szolgálat"].ToStrTrim(),
                                    rekord["telephelyitípus"].ToStrTrim(),
                                    rekord["munkanap"].ToÉrt_Long()
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

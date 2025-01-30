using System.Collections.Generic;
using System.Data.OleDb;
using MyF = Függvénygyűjtemény;

namespace Villamos.Kezelők
{
    public class Kezelő_Alap_Kiadás
    {
        public Dictionary<string, long> Szótár_TípusSzín(string hely, string jelszó, string szöveg)
        {
            Dictionary<string, long> Adatok = new Dictionary<string, long>();

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
                                Adatok.Add(rekord["típus"].ToStrTrim(),
                                rekord["színszám"].ToÉrt_Long());
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }
}

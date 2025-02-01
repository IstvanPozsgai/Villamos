using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Oktatásrajelöltek
    {
        public List<Adat_Oktatásrajelöltek> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Oktatásrajelöltek> Adatok = new List<Adat_Oktatásrajelöltek>();
            Adat_Oktatásrajelöltek Adat;

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
                                Adat = new Adat_Oktatásrajelöltek(
                                        rekord["HRazonosító"].ToStrTrim(),
                                        rekord["IDoktatás"].ToÉrt_Long(),
                                        rekord["mikortól"].ToÉrt_DaTeTime(),
                                        rekord["Státus"].ToÉrt_Long(),
                                        rekord["telephely"].ToStrTrim()
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


    public class Kezelő_OktatásiSegéd
    {
        public List<Adat_OktatásiSegéd> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_OktatásiSegéd> Adatok = new List<Adat_OktatásiSegéd>();
            Adat_OktatásiSegéd Adat;

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
                                Adat = new Adat_OktatásiSegéd(
                                    rekord["IDoktatás"].ToÉrt_Long(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["oktatásoka"].ToStrTrim(),
                                    rekord["Oktatástárgya"].ToStrTrim(),
                                    rekord["Oktatáshelye"].ToStrTrim(),
                                    rekord["oktatásidőtartama"].ToÉrt_Long(),
                                    rekord["Oktató"].ToStrTrim(),
                                    rekord["Oktatóbeosztása"].ToStrTrim(),
                                    rekord["Egyébszöveg"].ToStrTrim(),
                                    rekord["email"].ToStrTrim(),
                                    rekord["oktatás"].ToÉrt_Long()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public Adat_OktatásiSegéd Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_OktatásiSegéd Adat = null;

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
                            rekord.Read();

                            Adat = new Adat_OktatásiSegéd(
                                rekord["IDoktatás"].ToÉrt_Long(),
                                rekord["telephely"].ToStrTrim(),
                                rekord["oktatásoka"].ToStrTrim(),
                                rekord["Oktatástárgya"].ToStrTrim(),
                                rekord["Oktatáshelye"].ToStrTrim(),
                                rekord["oktatásidőtartama"].ToÉrt_Long(),
                                rekord["Oktató"].ToStrTrim(),
                                rekord["Oktatóbeosztása"].ToStrTrim(),
                                rekord["Egyébszöveg"].ToStrTrim(),
                                rekord["email"].ToStrTrim(),
                                rekord["oktatás"].ToÉrt_Long()
                                );
                        }
                    }
                }
            }
            return Adat;
        }
    }

}

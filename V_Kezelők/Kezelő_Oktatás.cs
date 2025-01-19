using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos_Kezelők
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

    public class Kezelő_Oktatás_Napló
    {

        public List<Adat_Oktatás_Napló> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Oktatás_Napló> Adatok = new List<Adat_Oktatás_Napló>();
            Adat_Oktatás_Napló Adat;

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
                                Adat = new Adat_Oktatás_Napló(
                                    rekord["ID"].ToÉrt_Long(),
                                    rekord["HRazonosító"].ToStrTrim(),
                                    rekord["IDoktatás"].ToÉrt_Long(),
                                    rekord["Oktatásdátuma"].ToÉrt_DaTeTime(),
                                    rekord["Kioktatta"].ToStrTrim(),
                                    rekord["Rögzítésdátuma"].ToÉrt_DaTeTime(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["PDFFájlneve"].ToStrTrim(),
                                    rekord["Számonkérés"].ToÉrt_Long(),
                                    rekord["státus"].ToÉrt_Long(),
                                    rekord["Rögzítő"].ToStrTrim(),
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

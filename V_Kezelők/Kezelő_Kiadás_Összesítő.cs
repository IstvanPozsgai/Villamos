using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiadás_Összesítő
    {
        public List<Adat_Kiadás_összesítő> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiadás_összesítő> Adatok = new List<Adat_Kiadás_összesítő>();
            Adat_Kiadás_összesítő Adat;

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
                                Adat = new Adat_Kiadás_összesítő(

                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["forgalomban"].ToÉrt_Int(),
                                    rekord["tartalék"].ToÉrt_Int(),
                                    rekord["kocsiszíni"].ToÉrt_Int(),
                                    rekord["félreállítás"].ToÉrt_Int(),
                                    rekord["főjavítás"].ToÉrt_Int(),
                                    rekord["személyzet"].ToÉrt_Int()
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

    public class Kezelő_Személyzet_Adatok
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

    public class Kezelő_Típuscsere_Adatok
    {
        public List<Adat_Típuscsere_Adatok> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Típuscsere_Adatok> Adatok = new List<Adat_Típuscsere_Adatok>();
            Adat_Típuscsere_Adatok Adat;

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
                                Adat = new Adat_Típuscsere_Adatok(
                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["szolgálat"].ToStrTrim(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["típuselőírt"].ToStrTrim(),
                                    rekord["típuskiadott"].ToStrTrim(),
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

    public class Kezelő_Forte_Kiadási_Adatok
    {
        public List<Adat_Forte_Kiadási_Adatok> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Forte_Kiadási_Adatok> Adatok = new List<Adat_Forte_Kiadási_Adatok>();
            Adat_Forte_Kiadási_Adatok Adat;

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
                                Adat = new Adat_Forte_Kiadási_Adatok(
                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["telephelyforte"].ToStrTrim(),
                                    rekord["típusforte"].ToStrTrim(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["kiadás"].ToÉrt_Long(),
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


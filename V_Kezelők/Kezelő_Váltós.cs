using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Váltós_Naptár
    {
        public List<Adat_Váltós_Naptár> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Váltós_Naptár> Adatok = new List<Adat_Váltós_Naptár>();
            Adat_Váltós_Naptár Adat;

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
                                Adat = new Adat_Váltós_Naptár(
                                          rekord["Nap"].ToStrTrim(),
                                          rekord["Dátum"].ToÉrt_DaTeTime()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Váltós_Naptár Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Váltós_Naptár Adat = null;

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
                                Adat = new Adat_Váltós_Naptár(
                                          rekord["Nap"].ToStrTrim(),
                                          rekord["Dátum"].ToÉrt_DaTeTime()
                                          );
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Váltós_Összesítő
    {
        public List<Adat_Váltós_Összesítő> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Váltós_Összesítő> Adatok = new List<Adat_Váltós_Összesítő>();
            Adat_Váltós_Összesítő Adat;

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
                                Adat = new Adat_Váltós_Összesítő(
                                          rekord["Perc"].ToÉrt_Long(),
                                          rekord["Dátum"].ToÉrt_DaTeTime()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Váltós_Összesítő> Lista_Adatok(string hely, string jelszó, string szöveg,string csoport)
        {
            List<Adat_Váltós_Összesítő> Adatok = new List<Adat_Váltós_Összesítő>();
            Adat_Váltós_Összesítő Adat;

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
                                Adat = new Adat_Váltós_Összesítő(
                                          rekord["Perc"].ToÉrt_Long(),
                                          rekord["Dátum"].ToÉrt_DaTeTime(),
                                          csoport
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


    public class Kezelő_Váltós_Kijelöltnapok
    {
        public List<Adat_Váltós_Kijelöltnapok> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Váltós_Kijelöltnapok> Adatok = new List<Adat_Váltós_Kijelöltnapok>();
            Adat_Váltós_Kijelöltnapok Adat;

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
                                Adat = new Adat_Váltós_Kijelöltnapok(
                                          rekord["Telephely"].ToStrTrim(),
                                          rekord["Csoport"].ToStrTrim(),
                                          rekord["Dátum"].ToÉrt_DaTeTime()
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

    public class Kezelő_Váltós_Váltóstábla
    {
        public List<Adat_Váltós_Váltóstábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Váltós_Váltóstábla> Adatok = new List<Adat_Váltós_Váltóstábla>();
            Adat_Váltós_Váltóstábla Adat;

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
                                Adat = new Adat_Váltós_Váltóstábla(
                                          rekord["Telephely"].ToStrTrim(),
                                          rekord["Csoport"].ToStrTrim(),
                                          rekord["Év"].ToÉrt_Int(),
                                          rekord["Félév"].ToÉrt_Int(),
                                          rekord["ZKnap"].ToÉrt_Double(),
                                          rekord["Epnap"].ToÉrt_Double(),
                                          rekord["Tperc"].ToÉrt_Double()
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

    public class Kezelő_Váltós_Váltóscsopitábla
    {
        public List<Adat_Váltós_Váltóscsopitábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Váltós_Váltóscsopitábla> Adatok = new List<Adat_Váltós_Váltóscsopitábla>();
            Adat_Váltós_Váltóscsopitábla Adat;

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
                                Adat = new Adat_Váltós_Váltóscsopitábla(
                                          rekord["Csoport"].ToStrTrim(),
                                          rekord["Telephely"].ToStrTrim(),
                                          rekord["Név"].ToStrTrim()
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

using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Épület_Adattábla
    {
        public List<Adat_Épület_Adattábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Épület_Adattábla> Adatok = new List<Adat_Épület_Adattábla>();
            Adat_Épület_Adattábla Adat;

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
                                Adat = new Adat_Épület_Adattábla(
                                          rekord["ID"].ToÉrt_Int(),
                                          rekord["Megnevezés"].ToStrTrim(),
                                          rekord["Osztály"].ToStrTrim(),
                                          rekord["Méret"].ToÉrt_Double(),
                                          rekord["Helységkód"].ToStrTrim(),
                                          rekord["Státus"].ToÉrt_Bool(),
                                          rekord["E1évdb"].ToÉrt_Int(),
                                          rekord["E2évdb"].ToÉrt_Int(),
                                          rekord["E3évdb"].ToÉrt_Int(),
                                          rekord["Kezd"].ToStrTrim(),
                                          rekord["Végez"].ToStrTrim(),
                                          rekord["Ellenőremail"].ToStrTrim(),
                                          rekord["Ellenőrneve"].ToStrTrim(),
                                          rekord["Ellenőrtelefonszám"].ToStrTrim(),
                                          rekord["Szemetes"].ToÉrt_Bool(),
                                          rekord["Kapcsolthelység"].ToStrTrim()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Épület_Adattábla Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Épület_Adattábla Adat = null;

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
                                Adat = new Adat_Épület_Adattábla(
                                          rekord["ID"].ToÉrt_Int(),
                                          rekord["Megnevezés"].ToStrTrim(),
                                          rekord["Osztály"].ToStrTrim(),
                                          rekord["Méret"].ToÉrt_Double(),
                                          rekord["Helységkód"].ToStrTrim(),
                                          rekord["Státus"].ToÉrt_Bool(),
                                          rekord["E1évdb"].ToÉrt_Int(),
                                          rekord["E2évdb"].ToÉrt_Int(),
                                          rekord["E3évdb"].ToÉrt_Int(),
                                          rekord["Kezd"].ToStrTrim(),
                                          rekord["Végez"].ToStrTrim(),
                                          rekord["Ellenőremail"].ToStrTrim(),
                                          rekord["Ellenőrneve"].ToStrTrim(),
                                          rekord["Ellenőrtelefonszám"].ToStrTrim(),
                                          rekord["Szemetes"].ToÉrt_Bool(),
                                          rekord["Kapcsolthelység"].ToStrTrim()
                                          );


                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Épület_Takarításosztály
    {
        public List<Adat_Épület_Takarításosztály> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Épület_Takarításosztály> Adatok = new List<Adat_Épület_Takarításosztály>();
            Adat_Épület_Takarításosztály Adat;

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
                                Adat = new Adat_Épület_Takarításosztály(
                                          rekord["ID"].ToÉrt_Int(),
                                          rekord["Osztály"].ToStrTrim(),
                                          rekord["E1Ft"].ToÉrt_Double(),
                                          rekord["E2Ft"].ToÉrt_Double(),
                                          rekord["E3Ft"].ToÉrt_Double(),
                                          rekord["Státus"].ToÉrt_Bool()
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



    public class Kezelő_Épület_Naptár
    {
        public List<Adat_Épület_Naptár> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Épület_Naptár> Adatok = new List<Adat_Épület_Naptár>();
            Adat_Épület_Naptár Adat;

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
                                Adat = new Adat_Épület_Naptár(
                                          rekord["Előterv"].ToÉrt_Bool(),
                                          rekord["Hónap"].ToÉrt_Int(),
                                          rekord["Igazolás"].ToÉrt_Bool(),
                                          rekord["Napok"].ToStrTrim()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Épület_Naptár Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Épület_Naptár Adat = null;

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
                                Adat = new Adat_Épület_Naptár(
                                          rekord["Előterv"].ToÉrt_Bool(),
                                          rekord["Hónap"].ToÉrt_Int(),
                                          rekord["Igazolás"].ToÉrt_Bool(),
                                          rekord["Napok"].ToStrTrim()
                                          );
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }


    public class Kezelő_Épület_Takarításrakijelölt
    {
        public List<Adat_Épület_Takarításrakijelölt> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Épület_Takarításrakijelölt> Adatok = new List<Adat_Épület_Takarításrakijelölt>();
            Adat_Épület_Takarításrakijelölt Adat;

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
                                Adat = new Adat_Épület_Takarításrakijelölt(
                                          rekord["E1elvégzettdb"].ToÉrt_Int(),
                                          rekord["E1kijelöltdb"].ToÉrt_Int(),
                                          rekord["E1rekijelölt"].ToStrTrim(),
                                          rekord["E2elvégzettdb"].ToÉrt_Int(),
                                          rekord["E2kijelöltdb"].ToÉrt_Int(),
                                          rekord["E2rekijelölt"].ToStrTrim(),
                                          rekord["E3elvégzettdb"].ToÉrt_Int(),
                                          rekord["E3kijelöltdb"].ToÉrt_Int(),
                                          rekord["E3rekijelölt"].ToStrTrim(),
                                          rekord["Helységkód"].ToStrTrim(),
                                          rekord["Hónap"].ToÉrt_Int(),
                                          rekord["Megnevezés"].ToStrTrim(),
                                          rekord["Osztály"].ToStrTrim()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Épület_Takarításrakijelölt Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Épület_Takarításrakijelölt Adat = null;

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
                                Adat = new Adat_Épület_Takarításrakijelölt(
                                          rekord["E1elvégzettdb"].ToÉrt_Int(),
                                          rekord["E1kijelöltdb"].ToÉrt_Int(),
                                          rekord["E1rekijelölt"].ToStrTrim(),
                                          rekord["E2elvégzettdb"].ToÉrt_Int(),
                                          rekord["E2kijelöltdb"].ToÉrt_Int(),
                                          rekord["E2rekijelölt"].ToStrTrim(),
                                          rekord["E3elvégzettdb"].ToÉrt_Int(),
                                          rekord["E3kijelöltdb"].ToÉrt_Int(),
                                          rekord["E3rekijelölt"].ToStrTrim(),
                                          rekord["Helységkód"].ToStrTrim(),
                                          rekord["Hónap"].ToÉrt_Int(),
                                          rekord["Megnevezés"]  .ToStrTrim(),
                                          rekord["Osztály"].ToStrTrim()
                                          );
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }
}

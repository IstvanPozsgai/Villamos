using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Jármű_Takarítás_Vezénylés
    {
        public List<Adat_Jármű_Takarítás_Vezénylés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_Vezénylés> Adatok = new List<Adat_Jármű_Takarítás_Vezénylés>();
            Adat_Jármű_Takarítás_Vezénylés Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Vezénylés(
                                        rekord["id"].ToÉrt_Long(),
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["takarítási_fajta"].ToStrTrim(),
                                        rekord["szerelvényszám"].ToÉrt_Long(),
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


    public class Kezelő_Jármű_Takarítás
    {
        public List<Adat_Jármű_Takarítás_Takarítások> Takarítások_Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_Takarítások> Adatok = new List<Adat_Jármű_Takarítás_Takarítások>();
            Adat_Jármű_Takarítás_Takarítások Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Takarítások(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["takarítási_fajta"].ToStrTrim(),
                                        rekord["telephely"].ToStrTrim(),
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

    public class Kezelő_Jármű_Takarítás_típus
    {
        public List<Adat_Jármű> Állomány_Lista_típus(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű> Adatok = new List<Adat_Jármű>();
            Adat_Jármű Adat;

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
                                Adat = new Adat_Jármű(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["típus"].ToStrTrim()
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

    public class Kezelő_Jármű_Takarítás_Napló
    {
        public List<Adat_Jármű_Takarítás_Napló> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_Napló> Adatok = new List<Adat_Jármű_Takarítás_Napló>();
            Adat_Jármű_Takarítás_Napló Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Napló(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["Takarítási_fajta"].ToStrTrim(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["Mikor"].ToÉrt_DaTeTime(),
                                        rekord["Módosító"].ToStrTrim(),
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



    public class Kezelő_Jármű_Takarítás_Teljesítés
    {
        public List<Adat_Jármű_Takarítás_Teljesítés> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_Teljesítés> Adatok = new List<Adat_Jármű_Takarítás_Teljesítés>();
            Adat_Jármű_Takarítás_Teljesítés Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Teljesítés(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["takarítási_fajta"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["megfelelt1"].ToÉrt_Int(),
                                        rekord["státus"].ToÉrt_Int(),
                                        rekord["megfelelt2"].ToÉrt_Int(),
                                        rekord["pótdátum"].ToÉrt_Bool(),
                                        rekord["napszak"].ToÉrt_Int(),
                                        rekord["mérték"].ToÉrt_Double(),
                                        rekord["típus"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }


    public class Kezelő_Jármű_Takarítás_J1
    {
        public List<Adat_Jármű_Takarítás_J1> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_J1> Adatok = new List<Adat_Jármű_Takarítás_J1>();
            Adat_Jármű_Takarítás_J1 Adat;

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
                                Adat = new Adat_Jármű_Takarítás_J1(
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["j1megfelelő"].ToÉrt_Int(),
                                        rekord["j1nemmegfelelő"].ToÉrt_Int(),
                                        rekord["napszak"].ToÉrt_Int(),
                                        rekord["típus"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }


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

    public class Kezelő_Jármű_Takarítás_Létszám
    {
        public List<Adat_Jármű_Takarítás_Létszám> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_Létszám> Adatok = new List<Adat_Jármű_Takarítás_Létszám>();
            Adat_Jármű_Takarítás_Létszám Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Létszám(
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["előírt"].ToÉrt_Int(),
                                        rekord["megjelent"].ToÉrt_Int(),
                                        rekord["napszak"].ToÉrt_Int(),
                                        rekord["ruhátlan"].ToÉrt_Int());
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

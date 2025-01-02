using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Főkönyv_Nap
    {
        public List<Adat_Főkönyv_Nap> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Főkönyv_Nap> Adatok = new List<Adat_Főkönyv_Nap>();
            Adat_Főkönyv_Nap Adat;

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
                                Adat = new Adat_Főkönyv_Nap(
                                    rekord["státus"].ToÉrt_Long(),
                                    rekord["hibaleírása"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["azonosító"].ToStrTrim(),
                                    rekord["szerelvény"].ToÉrt_Long(),
                                    rekord["viszonylat"].ToStrTrim(),
                                    rekord["forgalmiszám"].ToStrTrim(),
                                    rekord["kocsikszáma"].ToÉrt_Long(),
                                    rekord["tervindulás"].ToÉrt_DaTeTime(),
                                    rekord["tényindulás"].ToÉrt_DaTeTime(),
                                    rekord["tervérkezés"].ToÉrt_DaTeTime(),
                                    rekord["tényérkezés"].ToÉrt_DaTeTime(),
                                    rekord["miótaáll"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToString(),
                                    rekord["megjegyzés"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Főkönyv_Nap Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Főkönyv_Nap Adat = null;

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
                                Adat = new Adat_Főkönyv_Nap(
                                    rekord["státus"].ToÉrt_Long(),
                                    rekord["hibaleírása"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["azonosító"].ToStrTrim(),
                                    rekord["szerelvény"].ToÉrt_Long(),
                                    rekord["viszonylat"].ToStrTrim(),
                                    rekord["forgalmiszám"].ToStrTrim(),
                                    rekord["kocsikszáma"].ToÉrt_Long(),
                                    rekord["tervindulás"].ToÉrt_DaTeTime(),
                                    rekord["tényindulás"].ToÉrt_DaTeTime(),
                                    rekord["tervérkezés"].ToÉrt_DaTeTime(),
                                    rekord["tényérkezés"].ToÉrt_DaTeTime(),
                                    rekord["miótaáll"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["megjegyzés"].ToStrTrim()
                                    );

                            }
                        }
                    }
                }
            }
            return Adat;
        }

        public List<string> Lista_típus(string hely, string jelszó, string szöveg)
        {
            List<string> Adatok = new List<string>();


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
                                Adatok.Add(rekord["típus"].ToStrTrim());
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }

    public class Kezelő_Főkönyv_Személyzet
    {
        public List<Adat_Főkönyv_Személyzet> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Főkönyv_Személyzet> Adatok = new List<Adat_Főkönyv_Személyzet>();
            Adat_Főkönyv_Személyzet Adat;

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
                                Adat = new Adat_Főkönyv_Személyzet(
                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
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

    public class Kezelő_Főkönyv_Típuscsere
    {
        public List<Adat_FőKönyv_Típuscsere> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_FőKönyv_Típuscsere> Adatok = new List<Adat_FőKönyv_Típuscsere>();
            Adat_FőKönyv_Típuscsere Adat;

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
                                Adat = new Adat_FőKönyv_Típuscsere(
                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["típuselőírt"].ToStrTrim(),
                                    rekord["típuskiadott"].ToStrTrim(),
                                    rekord["viszonylat"].ToStrTrim(),
                                    rekord["forgalmiszám"].ToStrTrim(),
                                    rekord["tervindulás"].ToÉrt_DaTeTime(),
                                    rekord["azonosító"].ToStrTrim(),
                                    rekord["kocsi"].ToStrTrim()
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

    public class Kezelő_Főkönyv_ZSER
    {
        public List<Adat_Főkönyv_ZSER> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Főkönyv_ZSER> Adatok = new List<Adat_Főkönyv_ZSER>();
            Adat_Főkönyv_ZSER Adat;

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
                                Adat = new Adat_Főkönyv_ZSER(
                                    rekord["viszonylat"].ToStrTrim(),
                                    rekord["forgalmiszám"].ToStrTrim(),
                                    rekord["tervindulás"].ToÉrt_DaTeTime(),
                                    rekord["tényindulás"].ToÉrt_DaTeTime(),
                                    rekord["tervérkezés"].ToÉrt_DaTeTime(),
                                    rekord["tényérkezés"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["szerelvénytípus"].ToStrTrim(),
                                    rekord["kocsikszáma"].ToÉrt_Long(),
                                    rekord["megjegyzés"].ToStrTrim(),
                                    rekord["kocsi1"].ToStrTrim(),
                                    rekord["kocsi2"].ToStrTrim(),
                                    rekord["kocsi3"].ToStrTrim(),
                                    rekord["kocsi4"].ToStrTrim(),
                                    rekord["kocsi5"].ToStrTrim(),
                                    rekord["kocsi6"].ToStrTrim(),
                                    rekord["ellenőrző"].ToStrTrim(),
                                    rekord["Státus"].ToStrTrim()
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

    public class Kezelő_Főkönyv_Zser_Km
    {
        readonly string  jelszó= "pozsgaii";

        public List<Adat_Főkönyv_Zser_Km> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Főkönyv_Zser_Km> Adatok = new List<Adat_Főkönyv_Zser_Km>();
            Adat_Főkönyv_Zser_Km Adat;

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
                                Adat = new Adat_Főkönyv_Zser_Km(
                                    rekord["azonosító"].ToStrTrim(),
                                    rekord["Dátum"].ToÉrt_DaTeTime(),
                                    rekord["Napikm"].ToÉrt_Int(),
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

        public List<Adat_Főkönyv_Zser_Km> Lista_adatok(string hely)
        {
          string   szöveg = "SELECT * FROM tábla";
            List<Adat_Főkönyv_Zser_Km> Adatok = new List<Adat_Főkönyv_Zser_Km>();
            Adat_Főkönyv_Zser_Km Adat;

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
                                Adat = new Adat_Főkönyv_Zser_Km(
                                    rekord["azonosító"].ToStrTrim(),
                                    rekord["Dátum"].ToÉrt_DaTeTime(),
                                    rekord["Napikm"].ToÉrt_Int(),
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

    public class Kezelő_Főkönyv_SegédTábla
    {
        public List<Adat_Főkönyv_SegédTábla> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Főkönyv_SegédTábla> Adatok = new List<Adat_Főkönyv_SegédTábla>();
            Adat_Főkönyv_SegédTábla Adat;

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
                                Adat = new Adat_Főkönyv_SegédTábla(
                                    rekord["Id"].ToÉrt_Long(),
                                    rekord["Bejelentkezésinév"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Főkönyv_SegédTábla Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Főkönyv_SegédTábla Adat = null;

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
                            Adat = new Adat_Főkönyv_SegédTábla(
                                rekord["Id"].ToÉrt_Long(),
                                rekord["Bejelentkezésinév"].ToStrTrim()
                                );
                        }
                    }
                }
            }
            return Adat;
        }
    }
}

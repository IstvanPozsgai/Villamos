using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Védő_Könyv
    {
        public List<Adat_Védő_Könyv> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Védő_Könyv> Adatok = new List<Adat_Védő_Könyv>();
            Adat_Védő_Könyv Adat;

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
                                Adat = new Adat_Védő_Könyv(
                                        rekord["szerszámkönyvszám"].ToStrTrim(),
                                        rekord["szerszámkönyvnév"].ToStrTrim(),
                                        rekord["felelős1"].ToStrTrim(),
                                        rekord["státus"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Védő_Könyv Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Védő_Könyv Adat = null;

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
                                Adat = new Adat_Védő_Könyv(
                                        rekord["szerszámkönyvszám"].ToStrTrim(),
                                        rekord["szerszámkönyvnév"].ToStrTrim(),
                                        rekord["felelős1"].ToStrTrim(),
                                        rekord["státus"].ToÉrt_Bool()
                                        );
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Védő_Cikktörzs
    {
        public List<Adat_Védő_Cikktörzs> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Védő_Cikktörzs> Adatok = new List<Adat_Védő_Cikktörzs>();
            Adat_Védő_Cikktörzs Adat;

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
                                Adat = new Adat_Védő_Cikktörzs(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["Megnevezés"].ToStrTrim(),
                                        rekord["Méret"].ToStrTrim(),
                                        rekord["státus"].ToÉrt_Int(),
                                        rekord["költséghely"].ToStrTrim(),
                                        rekord["Védelem"].ToStrTrim(),
                                        rekord["Kockázat"].ToStrTrim(),
                                        rekord["Szabvány"].ToStrTrim(),
                                        rekord["Szint"].ToStrTrim(),
                                        rekord["Munk_megnevezés"].ToStrTrim()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Védő_Cikktörzs Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Védő_Cikktörzs Adat = null;

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
                                Adat = new Adat_Védő_Cikktörzs(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["Megnevezés"].ToStrTrim(),
                                        rekord["Méret"].ToStrTrim(),
                                        rekord["státus"].ToÉrt_Int(),
                                        rekord["költséghely"].ToStrTrim(),
                                        rekord["Védelem"].ToStrTrim(),
                                        rekord["Kockázat"].ToStrTrim(),
                                        rekord["Szabvány"].ToStrTrim(),
                                        rekord["Szint"].ToStrTrim(),
                                        rekord["Munk_megnevezés"].ToStrTrim()
                                        );
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Védő_Könyvelés
    {
        public List<Adat_Védő_Könyvelés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Védő_Könyvelés> Adatok = new List<Adat_Védő_Könyvelés>();
            Adat_Védő_Könyvelés Adat;

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
                                Adat = new Adat_Védő_Könyvelés(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["szerszámkönyvszám"].ToStrTrim(),
                                        rekord["mennyiség"].ToÉrt_Double(),
                                        rekord["gyáriszám"].ToStrTrim () ,
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["státus"].ToÉrt_Bool()
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

    public class Kezelő_Védő_Napló
    {
        public List<Adat_Védő_Napló> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Védő_Napló> Adatok = new List<Adat_Védő_Napló>();
            Adat_Védő_Napló Adat;

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
                                Adat = new Adat_Védő_Napló(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["Honnan"].ToStrTrim(),
                                        rekord["Hova"].ToStrTrim(),
                                        rekord["mennyiség"].ToÉrt_Double(),
                                        rekord["gyáriszám"].ToStrTrim(),
                                        rekord["Módosította"].ToStrTrim(),
                                        rekord["Módosításidátum"].ToÉrt_DaTeTime(),
                                        rekord["státus"].ToÉrt_Bool()
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

﻿using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_TW6000_Alap
    {
        public List<Adat_TW6000_Alap> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_TW6000_Alap> Adatok = new List<Adat_TW6000_Alap>();
            Adat_TW6000_Alap Adat;

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
                                Adat = new Adat_TW6000_Alap(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Ciklusrend"].ToStrTrim(),
                                        rekord["Kötöttstart"].ToÉrt_Bool(),
                                        rekord["Megállítás"].ToÉrt_Bool(),
                                        rekord["Start"].ToÉrt_DaTeTime(),
                                        rekord["Vizsgdátum"].ToÉrt_DaTeTime(),
                                        rekord["Vizsgnév"].ToStrTrim(),
                                        rekord["Vizsgsorszám"].ToÉrt_Int());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public Adat_TW6000_Alap Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_TW6000_Alap Adat = null;

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

                            Adat = new Adat_TW6000_Alap(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Ciklusrend"].ToStrTrim(),
                                    rekord["Kötöttstart"].ToÉrt_Bool(),
                                    rekord["Megállítás"].ToÉrt_Bool(),
                                    rekord["Start"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgdátum"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgnév"].ToStrTrim(),
                                    rekord["Vizsgsorszám"].ToÉrt_Int());
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_TW600_Telephely
    {
        public List<Adat_TW6000_Telephely> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_TW6000_Telephely> Adatok = new List<Adat_TW6000_Telephely>();
            Adat_TW6000_Telephely Adat;

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
                                Adat = new Adat_TW6000_Telephely(
                                        rekord["Sorrend"].ToÉrt_Int(),
                                        rekord["Telephely"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }

    public class Kezelő_TW600_Színezés
    {
        public List<Adat_TW6000_Színezés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_TW6000_Színezés> Adatok = new List<Adat_TW6000_Színezés>();
            Adat_TW6000_Színezés Adat;

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
                                Adat = new Adat_TW6000_Színezés(
                                        double.Parse(rekord["Szín"].ToString()),
                                        rekord["Vizsgálatnév"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }

    public class Kezelő_TW600_AlapNapló
    {
        public List<Adat_TW6000_AlapNapló> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_TW6000_AlapNapló> Adatok = new List<Adat_TW6000_AlapNapló>();
            Adat_TW6000_AlapNapló Adat;

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
                                Adat = new Adat_TW6000_AlapNapló(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Ciklusrend"].ToStrTrim(),
                                        rekord["Kötöttstart"].ToÉrt_Bool(),
                                        rekord["Megállítás"].ToÉrt_Bool(),
                                        rekord["Oka"].ToStrTrim(),
                                        rekord["Rögzítésiidő"].ToÉrt_DaTeTime(),
                                        rekord["Rögzítő"].ToStrTrim(),
                                        rekord["Start"].ToÉrt_DaTeTime(),
                                        rekord["Vizsgdátum"].ToÉrt_DaTeTime(),
                                        rekord["Vizsgnév"].ToStrTrim(),
                                        rekord["Vizsgsorszám"].ToÉrt_Int()
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

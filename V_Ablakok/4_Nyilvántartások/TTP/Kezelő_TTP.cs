using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos_Kezelők
{
    public class Kezelő_TTP_Alapadat
    {
        public List<Adat_TTP_Alapadat> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_TTP_Alapadat> Adatok = new List<Adat_TTP_Alapadat>();
            Adat_TTP_Alapadat Adat;

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
                                Adat = new Adat_TTP_Alapadat(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Gyártási_Év"].ToÉrt_DaTeTime(),
                                        rekord["TTP"].ToÉrt_Bool(),
                                        rekord["Megjegyzés"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }
    public class Kezelő_TTP_Naptár
    {
        public List<Adat_TTP_Naptár> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_TTP_Naptár> Adatok = new List<Adat_TTP_Naptár>();
            Adat_TTP_Naptár Adat;

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
                                Adat = new Adat_TTP_Naptár(
                                        rekord["Dátum"].ToÉrt_DaTeTime(),
                                        rekord["Munkanap"].ToÉrt_Bool());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }

    public class Kezelő_TTP_Tábla
    {
        public List<Adat_TTP_Tábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_TTP_Tábla> Adatok = new List<Adat_TTP_Tábla>();
            Adat_TTP_Tábla Adat;

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
                                Adat = new Adat_TTP_Tábla(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Lejárat_Dátum"].ToÉrt_DaTeTime(),
                                        rekord["Ütemezés_Dátum"].ToÉrt_DaTeTime(),
                                        rekord["TTP_Dátum"].ToÉrt_DaTeTime(),
                                        rekord["TTP_Javítás"].ToÉrt_Bool(),
                                        rekord["Rendelés"].ToStrTrim(),
                                        rekord["JavBefDát"].ToÉrt_DaTeTime(),
                                        rekord["Együtt"].ToStrTrim(), 
                                        rekord["Státus"].ToÉrt_Int (),
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


    public class Kezelő_TTP_Év
    {
        public List<Adat_TTP_Év> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_TTP_Év> Adatok = new List<Adat_TTP_Év>();
            Adat_TTP_Év Adat;

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
                                Adat = new Adat_TTP_Év(
                                        rekord["Év"].ToÉrt_Int(),
                                        rekord["Életkor"].ToÉrt_Int());
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

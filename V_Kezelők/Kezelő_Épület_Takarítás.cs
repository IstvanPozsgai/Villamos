using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Épület_Takarítás
    {
    }

    public class Kezelő_Épület_Takarítás_Osztály
    {

        public List<Adat_Épület_Takarítás_Osztály> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Épület_Takarítás_Osztály> Adatok = new List<Adat_Épület_Takarítás_Osztály>();
            Adat_Épület_Takarítás_Osztály Adat;

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
                                Adat = new Adat_Épület_Takarítás_Osztály(
                                        MyF.Érték_INT(rekord["id"].ToString()),
                                        rekord["Osztály"].ToStrTrim(),
                                        MyF.Érték_DOUBLE(rekord["E1Ft"].ToString()),
                                        MyF.Érték_DOUBLE(rekord["E2Ft"].ToString()),
                                        MyF.Érték_DOUBLE(rekord["E3Ft"].ToString()),
                                        MyF.Érték_BOOL(rekord["státus"].ToString())
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Épület_Takarítás_Osztály Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Épület_Takarítás_Osztály Adat = null;

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
                                Adat = new Adat_Épület_Takarítás_Osztály(
                                        MyF.Érték_INT(rekord["id"].ToString()),
                                        rekord["Osztály"].ToStrTrim(),
                                        MyF.Érték_DOUBLE(rekord["E1Ft"].ToString()),
                                        MyF.Érték_DOUBLE(rekord["E2Ft"].ToString()),
                                        MyF.Érték_DOUBLE(rekord["E3Ft"].ToString()),
                                        MyF.Érték_BOOL(rekord["státus"].ToString())
                                        );
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Épület_Takarítás_Adattábla
    {
        public List<Adat_Épület_Takarítás_Adattábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Épület_Takarítás_Adattábla> Adatok = new List<Adat_Épület_Takarítás_Adattábla>();
            Adat_Épület_Takarítás_Adattábla Adat;

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
                                Adat = new Adat_Épület_Takarítás_Adattábla(
                                        MyF.Érték_INT(rekord["id"].ToString()),
                                        rekord["Megnevezés"].ToStrTrim(),
                                        rekord["Osztály"].ToStrTrim(),
                                        MyF.Érték_DOUBLE(rekord["Méret"].ToString()),
                                        rekord["helységkód"].ToStrTrim(),
                                        MyF.Érték_BOOL(rekord["státus"].ToString()),
                                        MyF.Érték_INT(rekord["E1évdb"].ToString()),
                                        MyF.Érték_INT(rekord["E2évdb"].ToString()),
                                        MyF.Érték_INT(rekord["E3évdb"].ToString()),
                                        rekord["kezd"].ToStrTrim(),
                                        rekord["végez"].ToStrTrim(),
                                        rekord["ellenőremail"].ToStrTrim(),
                                        rekord["ellenőrneve"].ToStrTrim(),
                                        rekord["ellenőrtelefonszám"].ToStrTrim(),
                                        MyF.Érték_BOOL(rekord["szemetes"].ToString()),
                                        rekord["kapcsolthelység"].ToStrTrim()
                                       );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Épület_Takarítás_Adattábla Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Épület_Takarítás_Adattábla Adat = null;

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
                                Adat = new Adat_Épület_Takarítás_Adattábla(
                                        MyF.Érték_INT(rekord["id"].ToString()),
                                        rekord["Megnevezés"].ToStrTrim(),
                                        rekord["Osztály"].ToStrTrim(),
                                        MyF.Érték_DOUBLE(rekord["Méret"].ToString()),
                                        rekord["helységkód"].ToStrTrim(),
                                        MyF.Érték_BOOL(rekord["státus"].ToString()),
                                        MyF.Érték_INT(rekord["E1évdb"].ToString()),
                                        MyF.Érték_INT(rekord["E2évdb"].ToString()),
                                        MyF.Érték_INT(rekord["E3évdb"].ToString()),
                                        rekord["kezd"].ToStrTrim(),
                                        rekord["végez"].ToStrTrim(),
                                        rekord["ellenőremail"].ToStrTrim(),
                                        rekord["ellenőrneve"].ToStrTrim(),
                                        rekord["ellenőrtelefonszám"].ToStrTrim(),
                                        MyF.Érték_BOOL(rekord["szemetes"].ToString()),
                                        rekord["kapcsolthelység"].ToStrTrim()
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

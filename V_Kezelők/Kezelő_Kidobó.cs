using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Kidobó
    {
        public List<Adat_Kidobó> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kidobó> Adatok = new List<Adat_Kidobó>();
            Adat_Kidobó Adat;

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
                                Adat = new Adat_Kidobó(
                                    rekord["viszonylat"].ToStrTrim (),
                                    rekord["forgalmiszám"].ToStrTrim(),
                                    rekord["szolgálatiszám"].ToStrTrim(),
                                    rekord["jvez"].ToStrTrim(),
                                    rekord["kezdés"].ToÉrt_DaTeTime(),
                                    rekord["végzés"].ToÉrt_DaTeTime(),
                                    rekord["Kezdéshely"].ToStrTrim(),
                                    rekord["Végzéshely"].ToStrTrim(),
                                    rekord["Kód"].ToStrTrim(),
                                    rekord["Tárolásihely"].ToStrTrim(),
                                    rekord["Villamos"].ToStrTrim(),
                                    rekord["megjegyzés"].ToStrTrim(),
                                    rekord["szerelvénytípus"].ToStrTrim()
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

    public class Kezelő_Kidobó_Változat
    {
        public List<Adat_Kidobó_Változat> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kidobó_Változat> Adatok = new List<Adat_Kidobó_Változat>();
            Adat_Kidobó_Változat Adat;

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
                                Adat = new Adat_Kidobó_Változat(
                                      rekord["id"].ToÉrt_Long(),
                                      rekord["Változatnév"].ToStrTrim()
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

    public class Kezelő_Kidobó_Segéd
    {
        public List<Adat_Kidobó_Segéd> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kidobó_Segéd> Adatok = new List<Adat_Kidobó_Segéd>();
            Adat_Kidobó_Segéd Adat;

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
                                Adat = new Adat_Kidobó_Segéd(
                                    rekord["forgalmiszám"].ToStrTrim(),
                                    rekord["szolgálatiszám"].ToStrTrim(),
                                    rekord["kezdés"].ToÉrt_DaTeTime(),
                                    rekord["végzés"].ToÉrt_DaTeTime(),
                                    rekord["Kezdéshely"].ToStrTrim(),
                                    rekord["Végzéshely"].ToStrTrim(),
                                    rekord["Változatnév"].ToStrTrim(),
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

        public Adat_Kidobó_Segéd Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Kidobó_Segéd Adat = null;

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
                                Adat = new Adat_Kidobó_Segéd(
                                    rekord["forgalmiszám"].ToStrTrim(),
                                    rekord["szolgálatiszám"].ToStrTrim(),
                                    rekord["kezdés"].ToÉrt_DaTeTime(),
                                    rekord["végzés"].ToÉrt_DaTeTime(),
                                    rekord["Kezdéshely"].ToStrTrim(),
                                    rekord["Végzéshely"].ToStrTrim(),
                                    rekord["Változatnév"].ToStrTrim(),
                                    rekord["megjegyzés"].ToStrTrim()
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

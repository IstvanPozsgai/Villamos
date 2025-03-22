using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Kerék_Tábla
    {
        readonly string jelszó = "szabólászló";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerék.mdb";

        public Kezelő_Kerék_Tábla()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kerékbeolvasástábla(hely.KönyvSzerk());
        }



        public List<Adat_Kerék_Tábla> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM tábla";
            List<Adat_Kerék_Tábla> Adatok = new List<Adat_Kerék_Tábla>();
            Adat_Kerék_Tábla Adat;

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
                                Adat = new Adat_Kerék_Tábla(
                                        rekord["Kerékberendezés"].ToStrTrim(),
                                        rekord["kerékmegnevezés"].ToStrTrim(),
                                        rekord["kerékgyártásiszám"].ToStrTrim(),
                                        rekord["föléberendezés"].ToStrTrim(),
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["pozíció"].ToStrTrim(),
                                        rekord["Dátum"].ToÉrt_DaTeTime(),
                                        rekord["objektumfajta"].ToStrTrim()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        //elkopó
        public List<Adat_Kerék_Tábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kerék_Tábla> Adatok = new List<Adat_Kerék_Tábla>();
            Adat_Kerék_Tábla Adat;

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
                                Adat = new Adat_Kerék_Tábla(
                                        rekord["Kerékberendezés"].ToStrTrim(),
                                        rekord["kerékmegnevezés"].ToStrTrim(),
                                        rekord["kerékgyártásiszám"].ToStrTrim(),
                                        rekord["föléberendezés"].ToStrTrim(),
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["pozíció"].ToStrTrim(),
                                        rekord["Dátum"].ToÉrt_DaTeTime(),
                                        rekord["objektumfajta"].ToStrTrim()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kerék_Tábla Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kerék_Tábla Adat = null;

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
                            Adat = new Adat_Kerék_Tábla(
                                    rekord["Kerékberendezés"].ToStrTrim(),
                                    rekord["kerékmegnevezés"].ToStrTrim(),
                                    rekord["kerékgyártásiszám"].ToStrTrim(),
                                    rekord["föléberendezés"].ToStrTrim(),
                                    rekord["azonosító"].ToStrTrim(),
                                    rekord["pozíció"].ToStrTrim(),
                                    rekord["Dátum"].ToÉrt_DaTeTime(),
                                    rekord["objektumfajta"].ToStrTrim()
                                      );
                        }
                    }
                }
            }
            return Adat;
        }
    }

}

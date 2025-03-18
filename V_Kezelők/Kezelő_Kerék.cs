using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kerék_Mérés
    {
        readonly string jelszó = "szabólászló";
        string hely;

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Év}\telepikerék.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Méréstáblakerék(hely.KönyvSzerk());
        }

        public List<Adat_Kerék_Mérés> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            string szöveg = "SELECT * FROM keréktábla order by azonosító,pozíció ";

            List<Adat_Kerék_Mérés> Adatok = new List<Adat_Kerék_Mérés>();
            Adat_Kerék_Mérés Adat;

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
                                Adat = new Adat_Kerék_Mérés(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Pozíció"].ToStrTrim(),
                                        rekord["Kerékberendezés"].ToStrTrim(),
                                        rekord["Kerékgyártásiszám"].ToStrTrim(),
                                        rekord["Állapot"].ToStrTrim(),
                                        rekord["Méret"].ToÉrt_Int(),
                                        rekord["Módosító"].ToStrTrim(),
                                        rekord["Mikor"].ToÉrt_DaTeTime(),
                                        rekord["Oka"].ToStrTrim(),
                                        rekord["SAP"].ToÉrt_Int()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Kerék_Mérés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kerék_Mérés> Adatok = new List<Adat_Kerék_Mérés>();
            Adat_Kerék_Mérés Adat;

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
                                Adat = new Adat_Kerék_Mérés(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Pozíció"].ToStrTrim(),
                                        rekord["Kerékberendezés"].ToStrTrim(),
                                        rekord["Kerékgyártásiszám"].ToStrTrim(),
                                        rekord["Állapot"].ToStrTrim(),
                                        rekord["Méret"].ToÉrt_Int(),
                                        rekord["Módosító"].ToStrTrim(),
                                        rekord["Mikor"].ToÉrt_DaTeTime(),
                                        rekord["Oka"].ToStrTrim(),
                                        rekord["SAP"].ToÉrt_Int()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kerék_Mérés Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kerék_Mérés Adat = null;

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
                            Adat = new Adat_Kerék_Mérés(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Pozíció"].ToStrTrim(),
                                    rekord["Kerékberendezés"].ToStrTrim(),
                                    rekord["Kerékgyártásiszám"].ToStrTrim(),
                                    rekord["Állapot"].ToStrTrim(),
                                    rekord["Méret"].ToÉrt_Int(),
                                    rekord["Módosító"].ToStrTrim(),
                                    rekord["Mikor"].ToÉrt_DaTeTime(),
                                    rekord["Oka"].ToStrTrim(),
                                    rekord["SAP"].ToÉrt_Int()
                                      );
                        }
                    }
                }
            }
            return Adat;
        }

        public void Rögzít(string hely, string jelszó, Adat_Kerék_Mérés Adat)
        {

            string szöveg = "INSERT INTO keréktábla  (Azonosító, pozíció, kerékberendezés, kerékgyártásiszám, állapot, méret, mikor, Módosító, Oka, SAP) VALUES (";

            szöveg += $"'{Adat.Azonosító.Trim()}', ";
            szöveg += $"'{Adat.Pozíció.Trim()}', ";
            szöveg += $"'{Adat.Kerékberendezés.Trim()}', ";
            szöveg += $"'{Adat.Kerékgyártásiszám.Trim()}', ";
            szöveg += $"'{Adat.Állapot}', ";
            szöveg += $"{Adat.Méret}, ";
            szöveg += $"'{DateTime.Now}', ";
            szöveg += $"'{Program.PostásNév.Trim()}', ";
            szöveg += $"'{Adat.Oka.Trim()}', 0 )";

            Adatbázis.ABMódosítás(hely, jelszó, szöveg);
        }
    }

    public class Kezelő_Kerék_Tábla
    {
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

    public class Kezelő_Kerék_Eszterga
    {
        public List<Adat_Kerék_Eszterga> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kerék_Eszterga> Adatok = new List<Adat_Kerék_Eszterga>();
            Adat_Kerék_Eszterga Adat;

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
                                Adat = new Adat_Kerék_Eszterga(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Eszterga"].ToÉrt_DaTeTime(),
                                        rekord["Módosító"].ToStrTrim(),
                                        rekord["Mikor"].ToÉrt_DaTeTime(),
                                        rekord["kmu"].ToÉrt_Long()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kerék_Eszterga Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kerék_Eszterga Adat = null;

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
                            Adat = new Adat_Kerék_Eszterga(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Eszterga"].ToÉrt_DaTeTime(),
                                    rekord["Módosító"].ToStrTrim(),
                                    rekord["Mikor"].ToÉrt_DaTeTime(),
                                    rekord["kmu"].ToÉrt_Long()
                                    );
                        }
                    }
                }
            }
            return Adat;
        }
    }


    public class Kezelő_Kerék_Erő
    {
        public List<Adat_Kerék_Erő> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kerék_Erő> Adatok = new List<Adat_Kerék_Erő>();
            Adat_Kerék_Erő Adat;

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
                                Adat = new Adat_Kerék_Erő(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Van"].ToStrTrim(),
                                        rekord["Módosító"].ToStrTrim(),
                                        rekord["Mikor"].ToÉrt_DaTeTime()
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

    public class Kezelő_Kerék_Eszterga_Beállítás
    {
        public List<Adat_Kerék_Eszterga_Beállítás> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kerék_Eszterga_Beállítás> Adatok = new List<Adat_Kerék_Eszterga_Beállítás>();
            Adat_Kerék_Eszterga_Beállítás Adat;

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
                                Adat = new Adat_Kerék_Eszterga_Beállítás(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["KM_lépés"].ToÉrt_Int(),
                                        rekord["Idő_lépés"].ToÉrt_Int(),
                                        rekord["KM_IDŐ"].ToÉrt_Bool(),
                                        rekord["Ütemezve"].ToÉrt_DaTeTime()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kerék_Eszterga_Beállítás Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kerék_Eszterga_Beállítás Adat = null;

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
                            Adat = new Adat_Kerék_Eszterga_Beállítás(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["KM_lépés"].ToÉrt_Int(),
                                    rekord["Idő_lépés"].ToÉrt_Int(),
                                    rekord["KM_IDŐ"].ToÉrt_Bool(),
                                    rekord["Ütemezve"].ToÉrt_DaTeTime()
                                      );
                        }
                    }
                }
            }
            return Adat;
        }

        public void Rögzít(string hely, string jelszó, Adat_Kerék_Eszterga_Beállítás Adat)
        {
            string szöveg = $"SELECT * FROM Eszterga_Beállítás";
            Kezelő_Kerék_Eszterga_Beállítás Kezelő = new Kezelő_Kerék_Eszterga_Beállítás();
            List<Adat_Kerék_Eszterga_Beállítás> Adatok = Kezelő.Lista_Adatok(hely, jelszó, szöveg);
            Adat_Kerék_Eszterga_Beállítás Elem = (from a in Adatok
                                                  where a.Azonosító == Adat.Azonosító
                                                  select a).FirstOrDefault();

            if (Elem == null)
            {
                szöveg = "INSERT INTO eszterga_beállítás (Azonosító, KM_lépés, Idő_lépés, KM_IDŐ, Ütemezve) VALUES ";
                szöveg += $"('{Adat.Azonosító.Trim()}', {Adat.KM_lépés}, {Adat.Idő_lépés}, {Adat.KM_IDŐ}, '{Adat.Ütemezve:yyyy.MM.dd}'  )";
            }
            else
            {
                szöveg = "UPDATE eszterga_beállítás SET ";
                szöveg += $" KM_lépés={Adat.KM_lépés},";
                szöveg += $" Idő_lépés={Adat.Idő_lépés}, ";
                szöveg += $" KM_IDŐ={Adat.KM_IDŐ}, ";
                szöveg += $" Ütemezve='{Adat.Ütemezve:yyyy.MM.dd}' ";
                szöveg += $" WHERE azonosító='{Adat.Azonosító.Trim()}'";
            }
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }
    }
}

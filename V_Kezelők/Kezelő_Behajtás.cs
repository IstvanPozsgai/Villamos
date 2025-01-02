using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos_Kezelők
{
    public class Kezelő_Behajtás_Alap
    {
        public List<Adat_Behajtás_Alap> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Behajtás_Alap> Adatok = new List<Adat_Behajtás_Alap>();
            Adat_Behajtás_Alap Adat;

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
                                Adat = new Adat_Behajtás_Alap(
                                        rekord["Id"].ToÉrt_Int(),
                                        rekord["Adatbázisnév"].ToStrTrim(),
                                        rekord["Sorszámbetűjele"].ToString(),
                                        rekord["Sorszámkezdete"].ToÉrt_Int(),
                                        rekord["Engedélyérvényes"].ToÉrt_DaTeTime(),
                                        rekord["Státus"].ToÉrt_Int(),
                                        rekord["Adatbáziskönyvtár"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Behajtás_Alap Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Behajtás_Alap Adat = null;

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
                            Adat = new Adat_Behajtás_Alap(
                                        rekord["Id"].ToÉrt_Int(),
                                        rekord["Adatbázisnév"].ToStrTrim(),
                                        rekord["Sorszámbetűjele"].ToStrTrim(),
                                        rekord["Sorszámkezdete"].ToÉrt_Int(),
                                        rekord["Engedélyérvényes"].ToÉrt_DaTeTime(),
                                        rekord["Státus"].ToÉrt_Int(),
                                        rekord["Adatbáziskönyvtár"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Behajtás_Dolgozótábla
    {
        public List<Adat_Behajtás_Dolgozótábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Behajtás_Dolgozótábla> Adatok = new List<Adat_Behajtás_Dolgozótábla>();
            Adat_Behajtás_Dolgozótábla Adat;

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
                                Adat = new Adat_Behajtás_Dolgozótábla(
                                    rekord["SZTSZ"].ToStrTrim(),
                                    rekord["Családnévutónév"].ToStrTrim(),
                                    rekord["Szervezetiegység"].ToStrTrim(),
                                    rekord["Munkakör"].ToStrTrim(),
                                    rekord["Státus"].ToÉrt_Int());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Behajtás_Dolgozótábla Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Behajtás_Dolgozótábla Adat = null;

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
                            Adat = new Adat_Behajtás_Dolgozótábla(
                                        rekord["SZTSZ"].ToStrTrim(),
                                        rekord["Családnévutónév"].ToStrTrim(),
                                        rekord["Szervezetiegység"].ToStrTrim(),
                                        rekord["Munkakör"].ToStrTrim(),
                                        rekord["Státus"].ToÉrt_Int());
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Behajtás_Engedélyezés
    {
        public List<Adat_Behajtás_Engedélyezés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Behajtás_Engedélyezés> Adatok = new List<Adat_Behajtás_Engedélyezés>();
            Adat_Behajtás_Engedélyezés Adat;

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
                                Adat = new Adat_Behajtás_Engedélyezés(
                                        rekord["Id"].ToÉrt_Int(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["Emailcím"].ToStrTrim(),
                                        rekord["Gondnok"].ToÉrt_Bool(),
                                        rekord["Szakszolgálat"].ToÉrt_Bool(),
                                        rekord["Telefonszám"].ToStrTrim(),
                                        rekord["Szakszolgálatszöveg"].ToStrTrim(),
                                        rekord["Beosztás"].ToStrTrim(),
                                        rekord["Név"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }

    public class Kezelő_Behajtás_Jogosultság
    {
        public List<Adat_Behajtás_Jogosultság> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Behajtás_Jogosultság> Adatok = new List<Adat_Behajtás_Jogosultság>();
            Adat_Behajtás_Jogosultság Adat;

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
                                Adat = new Adat_Behajtás_Jogosultság(
                                        rekord["ID"].ToÉrt_Int(),
                                        rekord["Státustípus"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }

    public class Kezelő_Behajtás_Kérelemoka
    {
        public List<Adat_Behajtás_Kérelemoka> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Behajtás_Kérelemoka> Adatok = new List<Adat_Behajtás_Kérelemoka>();
            Adat_Behajtás_Kérelemoka Adat;

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
                                Adat = new Adat_Behajtás_Kérelemoka(
                                        rekord["Id"].ToÉrt_Int(),
                                        rekord["Ok"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Behajtás_Kérelemoka Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Behajtás_Kérelemoka Adat = null;

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

                            Adat = new Adat_Behajtás_Kérelemoka(
                                        rekord["Id"].ToÉrt_Int(),
                                        rekord["Ok"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Behajtás_Kérelemstátus
    {
        public List<Adat_Behajtás_Kérelemsátus> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Behajtás_Kérelemsátus> Adatok = new List<Adat_Behajtás_Kérelemsátus>();
            Adat_Behajtás_Kérelemsátus Adat;

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
                                Adat = new Adat_Behajtás_Kérelemsátus(
                                        rekord["ID"].ToÉrt_Int(),
                                        rekord["Státus"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Behajtás_Kérelemsátus Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Behajtás_Kérelemsátus Adat = null;

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

                            Adat = new Adat_Behajtás_Kérelemsátus(
                                        rekord["ID"].ToÉrt_Int(),
                                        rekord["Státus"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Behajtás_Szolgálati
    {
        public List<Adat_Behajtás_Szolgálati> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Behajtás_Szolgálati> Adatok = new List<Adat_Behajtás_Szolgálati>();
            Adat_Behajtás_Szolgálati Adat;

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
                                Adat = new Adat_Behajtás_Szolgálati(
                                        rekord["ID"].ToÉrt_Int(),
                                        rekord["Szolgálatihely"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }

    public class Kezelő_Behajtás_Telephelystátusz
    {
        public List<Adat_Behajtás_Telephelystátusz> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Behajtás_Telephelystátusz> Adatok = new List<Adat_Behajtás_Telephelystátusz>();
            Adat_Behajtás_Telephelystátusz Adat;

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
                                Adat = new Adat_Behajtás_Telephelystátusz(
                                        rekord["ID"].ToÉrt_Int(),
                                        rekord["Státus"].ToStrTrim(),
                                        rekord["Gondnok"].ToÉrt_Int(),
                                        rekord["Indoklás"].ToÉrt_Int());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Behajtás_Telephelystátusz Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Behajtás_Telephelystátusz Adat = null;

            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";
            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                using (OleDbCommand Parancs = new OleDbCommand(szöveg, Kapcsolat))
                {
                    using (OleDbDataReader rekord = Parancs.ExecuteReader())
                    {
                        rekord.Read();
                        Adat = new Adat_Behajtás_Telephelystátusz(
                                rekord["ID"].ToÉrt_Int(),
                                rekord["Státus"].ToStrTrim(),
                                rekord["Gondnok"].ToÉrt_Int(),
                                rekord["Indoklás"].ToÉrt_Int());
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Behajtás_Behajtási
    {
        public List<Adat_Behajtás_Behajtási> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Behajtás_Behajtási> Adatok = new List<Adat_Behajtás_Behajtási>();
            Adat_Behajtás_Behajtási Adat;

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
                                Adat = new Adat_Behajtás_Behajtási(
                                        rekord["Sorszám"].ToStrTrim(),
                                        rekord["Szolgálatihely"].ToStrTrim(),
                                        rekord["HRazonosító"].ToStrTrim(),
                                        rekord["Név"].ToStrTrim(),
                                        rekord["Rendszám"].ToStrTrim(),
                                        rekord["Angyalföld_engedély"].ToÉrt_Int(),
                                        rekord["Angyalföld_megjegyzés"].ToStrTrim(),
                                        rekord["Baross_engedély"].ToÉrt_Int(),
                                        rekord["Baross_megjegyzés"].ToStrTrim(),
                                        rekord["Budafok_engedély"].ToÉrt_Int(),
                                        rekord["Budafok_megjegyzés"].ToStrTrim(),
                                        rekord["Ferencváros_engedély"].ToÉrt_Int(),
                                        rekord["Ferencváros_megjegyzés"].ToStrTrim(),
                                        rekord["Fogaskerekű_engedély"].ToÉrt_Int(),
                                        rekord["Fogaskerekű_megjegyzés"].ToStrTrim(),
                                        rekord["Hungária_engedély"].ToÉrt_Int(),
                                        rekord["Hungária_megjegyzés"].ToStrTrim(),
                                        rekord["Kelenföld_engedély"].ToÉrt_Int(),
                                        rekord["Kelenföld_megjegyzés"].ToStrTrim(),
                                        rekord["Száva_engedély"].ToÉrt_Int(),
                                        rekord["Száva_megjegyzés"].ToStrTrim(),
                                        rekord["Szépilona_engedély"].ToÉrt_Int(),
                                        rekord["Szépilona_megjegyzés"].ToStrTrim(),
                                        rekord["Zugló_engedély"].ToÉrt_Int(),
                                        rekord["Zugló_megjegyzés"].ToStrTrim(),
                                        rekord["Korlátlan"].ToStrTrim(),
                                        rekord["Autók_száma"].ToÉrt_Int(),
                                        rekord["I_engedély"].ToÉrt_Int(),
                                        rekord["II_engedély"].ToÉrt_Int(),
                                        rekord["III_engedély"].ToÉrt_Int(),
                                        rekord["Státus"].ToÉrt_Int(),
                                        rekord["Dátum"].ToÉrt_DaTeTime(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["PDF"].ToStrTrim(),
                                        rekord["OKA"].ToStrTrim(),
                                        rekord["Érvényes"].ToÉrt_DaTeTime());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Behajtás_Behajtási Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Behajtás_Behajtási Adat = null;

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

                            Adat = new Adat_Behajtás_Behajtási(
                                        rekord["Sorszám"].ToStrTrim(),
                                        rekord["Szolgálatihely"].ToStrTrim(),
                                        rekord["HRazonosító"].ToStrTrim(),
                                        rekord["Név"].ToStrTrim(),
                                        rekord["Rendszám"].ToStrTrim(),
                                        rekord["Angyalföld_engedély"].ToÉrt_Int(),
                                        rekord["Angyalföld_megjegyzés"].ToStrTrim(),
                                        rekord["Baross_engedély"].ToÉrt_Int(),
                                        rekord["Baross_megjegyzés"].ToStrTrim(),
                                        rekord["Budafok_engedély"].ToÉrt_Int(),
                                        rekord["Budafok_megjegyzés"].ToStrTrim(),
                                        rekord["Ferencváros_engedély"].ToÉrt_Int(),
                                        rekord["Ferencváros_megjegyzés"].ToStrTrim(),
                                        rekord["Fogaskerekű_engedély"].ToÉrt_Int(),
                                        rekord["Fogaskerekű_megjegyzés"].ToStrTrim(),
                                        rekord["Hungária_engedély"].ToÉrt_Int(),
                                        rekord["Hungária_megjegyzés"].ToStrTrim(),
                                        rekord["Kelenföld_engedély"].ToÉrt_Int(),
                                        rekord["Kelenföld_megjegyzés"].ToStrTrim(),
                                        rekord["Száva_engedély"].ToÉrt_Int(),
                                        rekord["Száva_megjegyzés"].ToStrTrim(),
                                        rekord["Szépilona_engedély"].ToÉrt_Int(),
                                        rekord["Szépilona_megjegyzés"].ToStrTrim(),
                                        rekord["Zugló_engedély"].ToÉrt_Int(),
                                        rekord["Zugló_megjegyzés"].ToStrTrim(),
                                        rekord["Korlátlan"].ToStrTrim(),
                                        rekord["Autók_száma"].ToÉrt_Int(),
                                        rekord["I_engedély"].ToÉrt_Int(),
                                        rekord["II_engedély"].ToÉrt_Int(),
                                        rekord["III_engedély"].ToÉrt_Int(),
                                        rekord["Státus"].ToÉrt_Int(),
                                        rekord["Dátum"].ToÉrt_DaTeTime(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["PDF"].ToStrTrim(),
                                        rekord["OKA"].ToStrTrim(),
                                        rekord["Érvényes"].ToÉrt_DaTeTime());
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Behajtás_Behajtási_Napló
    {
        public List<Adat_Behajtás_Behajtási_Napló> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Behajtás_Behajtási_Napló> Adatok = new List<Adat_Behajtás_Behajtási_Napló>();
            Adat_Behajtás_Behajtási_Napló Adat;

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
                                Adat = new Adat_Behajtás_Behajtási_Napló(
                                        rekord["Sorszám"].ToStrTrim(),
                                        rekord["Szolgálatihely"].ToStrTrim(),
                                        rekord["HRazonosító"].ToStrTrim(),
                                        rekord["Név"].ToStrTrim(),
                                        rekord["Rendszám"].ToStrTrim(),
                                        rekord["Angyalföld_engedély"].ToÉrt_Int(),
                                        rekord["Angyalföld_megjegyzés"].ToStrTrim(),
                                        rekord["Baross_engedély"].ToÉrt_Int(),
                                        rekord["Baross_megjegyzés"].ToStrTrim(),
                                        rekord["Budafok_engedély"].ToÉrt_Int(),
                                        rekord["Budafok_megjegyzés"].ToStrTrim(),
                                        rekord["Ferencváros_engedély"].ToÉrt_Int(),
                                        rekord["Ferencváros_megjegyzés"].ToStrTrim(),
                                        rekord["Fogaskerekű_engedély"].ToÉrt_Int(),
                                        rekord["Fogaskerekű_megjegyzés"].ToStrTrim(),
                                        rekord["Hungária_engedély"].ToÉrt_Int(),
                                        rekord["Hungária_megjegyzés"].ToStrTrim(),
                                        rekord["Kelenföld_engedély"].ToÉrt_Int(),
                                        rekord["Kelenföld_megjegyzés"].ToStrTrim(),
                                        rekord["Száva_engedély"].ToÉrt_Int(),
                                        rekord["Száva_megjegyzés"].ToStrTrim(),
                                        rekord["Szépilona_engedély"].ToÉrt_Int(),
                                        rekord["Szépilona_megjegyzés"].ToStrTrim(),
                                        rekord["Zugló_engedély"].ToÉrt_Int(),
                                        rekord["Zugló_megjegyzés"].ToStrTrim(),
                                        rekord["Korlátlan"].ToStrTrim(),
                                        rekord["Autók_száma"].ToÉrt_Int(),
                                        rekord["I_engedély"].ToÉrt_Int(),
                                        rekord["II_engedély"].ToÉrt_Int(),
                                        rekord["III_engedély"].ToÉrt_Int(),
                                        rekord["Státus"].ToÉrt_Int(),
                                        rekord["Dátum"].ToÉrt_DaTeTime(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["PDF"].ToStrTrim(),
                                        rekord["OKA"].ToStrTrim(),
                                        rekord["ID"].ToÉrt_Int(),
                                        rekord["Rögzítette"].ToStrTrim(),
                                        rekord["Rögzítésdátuma"].ToÉrt_DaTeTime(),
                                        rekord["Érvényes"].ToÉrt_DaTeTime());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        /// <summary>
        /// Utolsó rögzített id szám
        /// </summary>
        /// <param name="hely"></param>
        /// <returns></returns>
        public double Napló_Id(string hely)
        {
            double válasz = 0;
            try
            {
                string szöveg = "SELECT * FROM alapadatok ORDER BY id DESC ";
                string jelszó = "forgalmirendszám";
                List<Adat_Behajtás_Behajtási_Napló> Adatok = Lista_Adatok(hely, jelszó, szöveg);
                if (Adatok == null) return válasz;
                válasz = Adatok.Max(a => a.ID);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return válasz;
        }

    }
}

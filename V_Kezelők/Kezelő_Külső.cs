using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Kezelők
{
    public class Kezelő_Külső_Cégek
    {
        public List<Adat_Külső_Cégek> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Külső_Cégek> Adatok = new List<Adat_Külső_Cégek>();
            Adat_Külső_Cégek Adat;

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
                                Adat = new Adat_Külső_Cégek(
                                        rekord["Cégid"].ToÉrt_Double(),
                                        rekord["Cég"].ToStrTrim(),
                                        rekord["Címe"].ToStrTrim(),
                                        rekord["Cég_email"].ToStrTrim(),
                                        rekord["Felelős_személy"].ToStrTrim(),
                                        rekord["Felelős_telefonszám"].ToStrTrim(),
                                        rekord["Munkaleírás"].ToStrTrim(),
                                        rekord["Mikor"].ToStrTrim(),
                                        rekord["Érv_kezdet"].ToÉrt_DaTeTime(),
                                        rekord["Érv_vég"].ToÉrt_DaTeTime(),
                                        rekord["Engedélyezés_dátuma"].ToÉrt_DaTeTime(),
                                        rekord["Engedélyező"].ToStrTrim(),
                                        rekord["Engedély"].ToÉrt_Int(),
                                        rekord["Státus"].ToÉrt_Bool(),
                                        rekord["Terület"].ToStrTrim()
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

    public class Kezelő_Külső_Gépjárművek
    {
        public List<Adat_Külső_Gépjárművek> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Külső_Gépjárművek> Adatok = new List<Adat_Külső_Gépjárművek>();
            Adat_Külső_Gépjárművek Adat;

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
                                Adat = new Adat_Külső_Gépjárművek(
                                        rekord["Id"].ToÉrt_Double(),
                                        rekord["Frsz"].ToStrTrim(),
                                        rekord["Cégid"].ToÉrt_Double(),
                                        rekord["Státus"].ToÉrt_Bool()
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

    public class Kezelő_Külső_Dolgozók
    {
        public List<Adat_Külső_Dolgozók> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Külső_Dolgozók> Adatok = new List<Adat_Külső_Dolgozók>();
            Adat_Külső_Dolgozók Adat;

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
                                Adat = new Adat_Külső_Dolgozók(
                                        rekord["Id"].ToÉrt_Double(),
                                        rekord["Név"].ToStrTrim(),
                                        rekord["Okmányszám"].ToStrTrim(),
                                        rekord["Anyjaneve"].ToStrTrim(),
                                        rekord["Születésihely"].ToStrTrim(),
                                        rekord["Születésiidő"].ToÉrt_DaTeTime(),
                                        rekord["Cégid"].ToÉrt_Double(),
                                        rekord["Státus"].ToÉrt_Bool()
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

    public class Kezelő_Külső_Telephelyek
    {
        public List<Adat_Külső_Telephelyek> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Külső_Telephelyek> Adatok = new List<Adat_Külső_Telephelyek>();
            Adat_Külső_Telephelyek Adat;

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
                                Adat = new Adat_Külső_Telephelyek(
                                        rekord["Id"].ToÉrt_Double(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["Cégid"].ToÉrt_Double(),
                                        rekord["Státus"].ToÉrt_Bool()
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

    public class Kezelő_Külső_Email
    {
        public List<Adat_Külső_Email> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Külső_Email> Adatok = new List<Adat_Külső_Email>();
            Adat_Külső_Email Adat;

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
                                Adat = new Adat_Külső_Email(
                                        rekord["Id"].ToÉrt_Double(),
                                        rekord["Másolat"].ToStrTrim(),
                                        rekord["Aláírás"].ToStrTrim()
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
    public class Kezelő_Külső_Lekérdezés_Autó
    {
        public List<Adat_Külső_Lekérdezés_Autó> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Külső_Lekérdezés_Autó> Adatok = new List<Adat_Külső_Lekérdezés_Autó>();
            Adat_Külső_Lekérdezés_Autó Adat;

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
                                Adat = new Adat_Külső_Lekérdezés_Autó(
                                           rekord["Frsz"].ToStrTrim(),
                                           rekord["Cég"].ToStrTrim(),
                                           rekord["Telephely"].ToStrTrim(),
                                           rekord["Munkaleírás"].ToStrTrim()
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

    public class Kezelő_Külső_Lekérdezés_Személy
    {
        public List<Adat_Külső_Lekérdezés_Személy> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Külső_Lekérdezés_Személy> Adatok = new List<Adat_Külső_Lekérdezés_Személy>();
            Adat_Külső_Lekérdezés_Személy Adat;

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
                                Adat = new Adat_Külső_Lekérdezés_Személy(
                                           rekord["Név"].ToStrTrim(),
                                           rekord["Okmányszám"].ToStrTrim(),
                                           rekord["Cég"].ToStrTrim(),
                                           rekord["Munkaleírás"].ToStrTrim()
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

using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

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


}

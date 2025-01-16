using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_MEO_Naptábla
    {
        public List<Adat_MEO_Naptábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_MEO_Naptábla> Adatok = new List<Adat_MEO_Naptábla>();
            Adat_MEO_Naptábla Adat;

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
                                Adat = new Adat_MEO_Naptábla(
                                        rekord["Id"].ToÉrt_Int());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
        public Adat_MEO_Naptábla Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_MEO_Naptábla Adat = null;

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
                                Adat = new Adat_MEO_Naptábla(
                                        rekord["Id"].ToÉrt_Int());
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }



    public class Kezelő_MEO_KerékMérés
    {
        public List<Adat_MEO_KerékMérés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_MEO_KerékMérés> Adatok = new List<Adat_MEO_KerékMérés>();
            Adat_MEO_KerékMérés Adat;

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
                                Adat = new Adat_MEO_KerékMérés(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Bekövetkezés"].ToÉrt_DaTeTime(),
                                        rekord["Üzem"].ToStrTrim(),
                                        rekord["Törölt"].ToÉrt_Bool(),
                                        rekord["Mikor"].ToÉrt_DaTeTime(),
                                        rekord["Ki"].ToStrTrim(),
                                        rekord["Típus"].ToStrTrim()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_MEO_KerékMérés Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_MEO_KerékMérés Adat = null;

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
                                Adat = new Adat_MEO_KerékMérés(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Bekövetkezés"].ToÉrt_DaTeTime(),
                                        rekord["Üzem"].ToStrTrim(),
                                        rekord["Törölt"].ToÉrt_Bool(),
                                        rekord["Mikor"].ToÉrt_DaTeTime(),
                                        rekord["Ki"].ToStrTrim(),
                                        rekord["Típus"].ToStrTrim()
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

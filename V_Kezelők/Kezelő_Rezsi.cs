using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Rezsi
    {



        public List<Adat_Rezsi_Hely> Lista_Adatok_Hely(string hely, string jelszó, string szöveg)
        {
            List<Adat_Rezsi_Hely> Adatok = new List<Adat_Rezsi_Hely>();
            Adat_Rezsi_Hely Adat;

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
                                Adat = new Adat_Rezsi_Hely(
                                       rekord["Azonosító"].ToStrTrim(),
                                       rekord["Állvány"].ToStrTrim(),
                                       rekord["Polc"].ToStrTrim(),
                                       rekord["Helyiség"].ToStrTrim(),
                                       rekord["Megjegyzés"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Rezsi_Hely EgyAdat_Hely(string hely, string jelszó, string szöveg)
        {
            Adat_Rezsi_Hely Adat = null;

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

                            Adat = new Adat_Rezsi_Hely(
                                       rekord["Azonosító"].ToStrTrim(),
                                       rekord["Állvány"].ToStrTrim(),
                                       rekord["Polc"].ToStrTrim(),
                                       rekord["Helyiség"].ToStrTrim(),
                                       rekord["Megjegyzés"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }

        public List<Adat_Rezsi_Listanapló> Lista_Adatok_Listanapló(string hely, string jelszó, string szöveg)
        {
            List<Adat_Rezsi_Listanapló> Adatok = new List<Adat_Rezsi_Listanapló>();
            Adat_Rezsi_Listanapló Adat;

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
                                Adat = new Adat_Rezsi_Listanapló(
                                       rekord["Azonosító"].ToStrTrim(),
                                       rekord["Honnan"].ToStrTrim(),
                                       rekord["Hova"].ToStrTrim(),
                                       rekord["Mennyiség"].ToStrTrim(),
                                       rekord["Mirehasznál"].ToStrTrim(),
                                       rekord["Módosította"].ToStrTrim(),
                                       rekord["módosításidátum"].ToÉrt_DaTeTime(),
                                       rekord["Státus"].ToÉrt_Bool());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Rezsi_Listanapló EgyAdat_Listanapló(string hely, string jelszó, string szöveg)
        {
            Adat_Rezsi_Listanapló Adat = null;

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

                            Adat = new Adat_Rezsi_Listanapló(
                                 rekord["Azonosító"].ToStrTrim(),
                                       rekord["Honnan"].ToStrTrim(),
                                       rekord["Hova"].ToStrTrim(),
                                       rekord["Mennyiség"].ToStrTrim(),
                                       rekord["Mirehasznál"].ToStrTrim(),
                                       rekord["Módosította"].ToStrTrim(),
                                       rekord["módosításidátum"].ToÉrt_DaTeTime(),
                                       rekord["Státus"].ToÉrt_Bool());
                        }
                    }
                }
            }
            return Adat;
        }

        public List<Adat_Rezsi_Lista> Lista_Adatok_Lista(string hely, string jelszó, string szöveg)
        {
            List<Adat_Rezsi_Lista> Adatok = new List<Adat_Rezsi_Lista>();
            Adat_Rezsi_Lista Adat;

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
                                Adat = new Adat_Rezsi_Lista(
                                       rekord["Azonosító"].ToStrTrim(),
                                       rekord["Mennyiség"].ToStrTrim(),
                                       Convert.ToDateTime(rekord["Dátum"].ToString()),
                                       Convert.ToBoolean(rekord["státus"].ToString()));
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Rezsi_Lista EgyAdat_Lista(string hely, string jelszó, string szöveg)
        {
            Adat_Rezsi_Lista Adat = null;

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

                            Adat = new Adat_Rezsi_Lista(
                                 rekord["Azonositó"].ToStrTrim(),
                                       rekord["Mennyiség"].ToStrTrim(),
                                       Convert.ToDateTime(rekord["Dátum"].ToString()),
                                       Convert.ToBoolean(rekord["státus"].ToString()));
                        }
                    }
                }
            }
            return Adat;
        }
    }
}

using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;


namespace Villamos.Kezelők
{


    public class Kezelő_Kiegészítő_SérülésSzöveg
    {
        public List<Adat_Kiegészítő_SérülésSzöveg> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_SérülésSzöveg Adat;
            List<Adat_Kiegészítő_SérülésSzöveg> Adatok = new List<Adat_Kiegészítő_SérülésSzöveg>();

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
                                Adat = new Adat_Kiegészítő_SérülésSzöveg(
                                           rekord["Id"].ToÉrt_Int(),
                                           rekord["Szöveg1"].ToStrTrim(),
                                           rekord["Szöveg2"].ToStrTrim(),
                                           rekord["Szöveg3"].ToStrTrim(),
                                           rekord["Szöveg4"].ToStrTrim(),
                                           rekord["Szöveg5"].ToStrTrim(),
                                           rekord["Szöveg6"].ToStrTrim(),
                                           rekord["Szöveg7"].ToStrTrim(),
                                           rekord["Szöveg8"].ToStrTrim(),
                                           rekord["Szöveg9"].ToStrTrim(),
                                           rekord["Szöveg10"].ToStrTrim(),
                                           rekord["Szöveg11"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kiegészítő_SérülésSzöveg Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_SérülésSzöveg Adat = null;

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

                            Adat = new Adat_Kiegészítő_SérülésSzöveg(
                                           rekord["Id"].ToÉrt_Int(),
                                           rekord["Szöveg1"].ToStrTrim(),
                                           rekord["Szöveg2"].ToStrTrim(),
                                           rekord["Szöveg3"].ToStrTrim(),
                                           rekord["Szöveg4"].ToStrTrim(),
                                           rekord["Szöveg5"].ToStrTrim(),
                                           rekord["Szöveg6"].ToStrTrim(),
                                           rekord["Szöveg7"].ToStrTrim(),
                                           rekord["Szöveg8"].ToStrTrim(),
                                           rekord["Szöveg9"].ToStrTrim(),
                                           rekord["Szöveg10"].ToStrTrim(),
                                           rekord["Szöveg11"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Kiegészítő_Részmunkakör
    {
        public List<Adat_Kiegészítő_Részmunkakör> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Részmunkakör Adat;
            List<Adat_Kiegészítő_Részmunkakör> Adatok = new List<Adat_Kiegészítő_Részmunkakör>();

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
                                Adat = new Adat_Kiegészítő_Részmunkakör(
                                           rekord["Id"].ToÉrt_Long(),
                                           rekord["Megnevezés"].ToStrTrim(),
                                           rekord["Id"].ToÉrt_Long()
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


    public class Kezelő_Kiegészítő_Doksi
    {
        public List<Adat_Kiegészítő_Doksi> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Doksi Adat;
            List<Adat_Kiegészítő_Doksi> Adatok = new List<Adat_Kiegészítő_Doksi>();

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
                                Adat = new Adat_Kiegészítő_Doksi(
                                           rekord["Kategória"].ToStrTrim(),
                                           rekord["Kód"].ToStrTrim(),
                                           rekord["Éves"].ToStrTrim()
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
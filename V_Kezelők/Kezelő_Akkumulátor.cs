using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Kezelők
{
    public class Kezelő_Akkumulátor
    {
        public List<Adat_Akkumulátor> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Akkumulátor> Adatok = new List<Adat_Akkumulátor>();
            Adat_Akkumulátor Adat;

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
                                Adat = new Adat_Akkumulátor(
                                        rekord["Beépítve"].ToStrTrim(),
                                        rekord["Fajta"].ToStrTrim(),
                                        rekord["Gyártó"].ToStrTrim(),
                                        rekord["Gyáriszám"].ToStrTrim(),
                                        rekord["Típus"].ToStrTrim(),
                                        rekord["Garancia"].ToÉrt_DaTeTime(),
                                        rekord["Gyártásiidő"].ToÉrt_DaTeTime(),
                                        rekord["Státus"].ToÉrt_Int(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["Módosításdátuma"].ToÉrt_DaTeTime(),
                                        rekord["Kapacitás"].ToÉrt_Int(),
                                        rekord["Telephely"].ToStrTrim()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Akkumulátor Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Akkumulátor Adat = null;

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
                            Adat = new Adat_Akkumulátor(
                                 rekord["Beépítve"].ToStrTrim(),
                                 rekord["Fajta"].ToStrTrim(),
                                 rekord["Gyártó"].ToStrTrim(),
                                 rekord["Gyáriszám"].ToStrTrim(),
                                 rekord["Típus"].ToStrTrim(),
                                 rekord["Garancia"].ToÉrt_DaTeTime(),
                                 rekord["Gyártásiidő"].ToÉrt_DaTeTime(),
                                 rekord["Státus"].ToÉrt_Int(),
                                 rekord["Megjegyzés"].ToStrTrim(),
                                 rekord["Módosításdátuma"].ToÉrt_DaTeTime(),
                                 rekord["Kapacitás"].ToÉrt_Int(),
                                 rekord["Telephely"].ToStrTrim()
                              );
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Akkumulátor_Napló
    {
    }

    public class Kezelő_Akkumulátor_Mérés
    {
        public List<Adat_Akkumulátor_Mérés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Akkumulátor_Mérés> Adatok = new List<Adat_Akkumulátor_Mérés>();
            Adat_Akkumulátor_Mérés Adat;

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
                                Adat = new Adat_Akkumulátor_Mérés(
                                        rekord["Gyáriszám"].ToStrTrim(),
                                        rekord["kisütésiáram"].ToÉrt_Long(),
                                        rekord["kezdetifesz"].ToÉrt_Double(),
                                        rekord["végfesz"].ToÉrt_Double(),
                                        rekord["kisütésiidő"].ToÉrt_DaTeTime(),
                                        rekord["kapacitás"].ToÉrt_Double(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["van"].ToStrTrim(),
                                        rekord["Mérésdátuma"].ToÉrt_DaTeTime(),
                                        rekord["Rögzítés"].ToÉrt_DaTeTime(),
                                        rekord["Rögzítő"].ToStrTrim(),
                                        rekord["id"].ToÉrt_Long()
                                         );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public Adat_Akkumulátor_Mérés Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Akkumulátor_Mérés Adat = null;

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
                            Adat = new Adat_Akkumulátor_Mérés(
                                      rekord["Gyáriszám"].ToStrTrim(),
                                      rekord["kisütésiáram"].ToÉrt_Long(),
                                      rekord["kezdetifesz"].ToÉrt_Double(),
                                      rekord["végfesz"].ToÉrt_Double(),
                                      rekord["kisütésiidő"].ToÉrt_DaTeTime(),
                                      rekord["kapacitás"].ToÉrt_Double(),
                                      rekord["Megjegyzés"].ToStrTrim(),
                                      rekord["van"].ToStrTrim(),
                                      rekord["Mérésdátuma"].ToÉrt_DaTeTime(),
                                      rekord["Rögzítés"].ToÉrt_DaTeTime(),
                                      rekord["Rögzítő"].ToStrTrim(),
                                      rekord["id"].ToÉrt_Long()
                                       );
                        }
                    }
                }
            }
            return Adat;
        }
    }
}

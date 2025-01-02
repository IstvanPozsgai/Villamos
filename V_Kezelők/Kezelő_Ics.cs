using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Kezelők
{
    public class Kezelő_Ics
    {
        public List<Adat_ICS> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_ICS> Adatok = new List<Adat_ICS>();
            Adat_ICS Adat;

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
                                Adat = new Adat_ICS(
                                       rekord["Azonosító"].ToStrTrim(),
                                       rekord["Takarítás"].ToÉrt_DaTeTime(),
                                       rekord["E2"].ToÉrt_Int(),
                                       rekord["E3"].ToÉrt_Int()
                                       );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public Adat_ICS Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_ICS Adat = null;

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
                            Adat = new Adat_ICS(
                                   rekord["Azonosító"].ToStrTrim(),
                                   rekord["Takarítás"].ToÉrt_DaTeTime(),
                                   rekord["E2"].ToÉrt_Int(),
                                   rekord["E3"].ToÉrt_Int()
                                   );
                        }
                    }
                }
            }
            return Adat;
        }
    }


    public class Kezelő_ICS_Előterv
    {
        public List<Adat_ICS_Előterv> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_ICS_Előterv> Adatok = new List<Adat_ICS_Előterv>();
            Adat_ICS_Előterv Adat;

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
                                Adat = new Adat_ICS_Előterv(
                                       rekord["ID"].ToÉrt_Long(),
                                       rekord["Azonosító"].ToStrTrim(),
                                       rekord["Jjavszám"].ToÉrt_Long(),
                                       rekord["KMUkm"].ToÉrt_Long(),
                                       rekord["KMUdátum"].ToÉrt_DaTeTime(),

                                       rekord["Vizsgfok"].ToStrTrim(),
                                       rekord["Vizsgdátumk"].ToÉrt_DaTeTime(),
                                       rekord["Vizsgdátumv"].ToÉrt_DaTeTime(),
                                       rekord["Vizsgkm"].ToÉrt_Long(),
                                       rekord["Havikm"].ToÉrt_Long(),

                                       rekord["Vizsgsorszám"].ToÉrt_Long(),
                                       rekord["Fudátum"].ToÉrt_DaTeTime(),
                                       rekord["Teljeskm"].ToÉrt_Long(),
                                       rekord["Ciklusrend"].ToStrTrim(),
                                       rekord["V2végezte"].ToStrTrim(),

                                       rekord["KövV2_sorszám"].ToÉrt_Long(),
                                       rekord["KövV2"].ToStrTrim(),
                                       rekord["KövV_sorszám"].ToÉrt_Long(),
                                       rekord["KövV"].ToStrTrim(),
                                       rekord["Törölt"].ToÉrt_Bool(),

                                       rekord["Módosító"].ToStrTrim(),
                                       rekord["Mikor"].ToÉrt_DaTeTime(),
                                       rekord["Honostelephely"].ToStrTrim(),
                                       rekord["Tervsorszám"].ToÉrt_Long(),

                                       rekord["Kerék_K1"].ToÉrt_Double(),
                                       rekord["Kerék_K2"].ToÉrt_Double(),
                                       rekord["Kerék_K3"].ToÉrt_Double(),
                                       rekord["Kerék_K4"].ToÉrt_Double(),
                                       rekord["Kerék_K5"].ToÉrt_Double(),
                                       rekord["Kerék_K6"].ToÉrt_Double(),
                                       rekord["Kerék_K7"].ToÉrt_Double(),
                                       rekord["Kerék_K8"].ToÉrt_Double(),
                                       rekord["Kerék_min"].ToÉrt_Double(),

                                       rekord["V2V3Számláló"].ToÉrt_Long()
                                       );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_ICS_Előterv Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_ICS_Előterv Adat = null;

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
                            DateTime KMUdátum = DateTime.TryParse(rekord["KMUdátum"].ToString(), out KMUdátum) ? KMUdátum : new DateTime(1900, 1, 1);
                            DateTime Vizsgdátumk = DateTime.TryParse(rekord["Vizsgdátumk"].ToString(), out Vizsgdátumk) ? Vizsgdátumk : new DateTime(1900, 1, 1);
                            DateTime Vizsgdátumv = DateTime.TryParse(rekord["Vizsgdátumv"].ToString(), out Vizsgdátumv) ? Vizsgdátumv : new DateTime(1900, 1, 1);
                            DateTime Fudátum = DateTime.TryParse(rekord["Fudátum"].ToString(), out Fudátum) ? Fudátum : new DateTime(1900, 1, 1);
                            DateTime Mikor = DateTime.TryParse(rekord["Mikor"].ToString(), out Mikor) ? Mikor : new DateTime(1900, 1, 1);


                            Adat = new Adat_ICS_Előterv(
                                   rekord["ID"].ToÉrt_Long(),
                                   rekord["Azonosító"].ToStrTrim(),
                                   rekord["Jjavszám"].ToÉrt_Long(),
                                   rekord["KMUkm"].ToÉrt_Long(),
                                   rekord["KMUdátum"].ToÉrt_DaTeTime(),

                                   rekord["Vizsgfok"].ToStrTrim(),
                                   rekord["Vizsgdátumk"].ToÉrt_DaTeTime(),
                                   rekord["Vizsgdátumv"].ToÉrt_DaTeTime(),
                                   rekord["Vizsgkm"].ToÉrt_Long(),
                                   rekord["Havikm"].ToÉrt_Long(),

                                   rekord["Vizsgsorszám"].ToÉrt_Long(),
                                   rekord["Fudátum"].ToÉrt_DaTeTime(),
                                   rekord["Teljeskm"].ToÉrt_Long(),
                                   rekord["Ciklusrend"].ToStrTrim(),
                                   rekord["V2végezte"].ToStrTrim(),

                                   rekord["KövV2_sorszám"].ToÉrt_Long(),
                                   rekord["KövV2"].ToStrTrim(),
                                   rekord["KövV_sorszám"].ToÉrt_Long(),
                                   rekord["KövV"].ToStrTrim(),
                                   rekord["Törölt"].ToÉrt_Bool(),

                                   rekord["Módosító"].ToStrTrim(),
                                   rekord["Mikor"].ToÉrt_DaTeTime(),
                                   rekord["Honostelephely"].ToStrTrim(),
                                   rekord["Tervsorszám"].ToÉrt_Long(),

                                   rekord["Kerék_K1"].ToÉrt_Double(),
                                   rekord["Kerék_K2"].ToÉrt_Double(),
                                   rekord["Kerék_K3"].ToÉrt_Double(),
                                   rekord["Kerék_K4"].ToÉrt_Double(),
                                   rekord["Kerék_K5"].ToÉrt_Double(),
                                   rekord["Kerék_K6"].ToÉrt_Double(),
                                   rekord["Kerék_K7"].ToÉrt_Double(),
                                   rekord["Kerék_K8"].ToÉrt_Double(),
                                   rekord["Kerék_min"].ToÉrt_Double(),

                                   rekord["V2V3Számláló"].ToÉrt_Long()
                                   );

                        }
                    }
                }
            }
            return Adat;
        }
    }
}

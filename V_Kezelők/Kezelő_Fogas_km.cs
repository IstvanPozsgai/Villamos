using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Kezelők
{
    public class Kezelő_Fogas_km
    {
        public List<Adat_Fogas_Km> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Fogas_Km> Adatok = new List<Adat_Fogas_Km>();
            Adat_Fogas_Km Adat;

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
                                Adat = new Adat_Fogas_Km(
                                        rekord["id"].ToÉrt_Long(),
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["jjavszám"].ToÉrt_Long(),
                                        rekord["KMUkm"].ToÉrt_Long(),
                                        rekord["KMUdátum"].ToÉrt_DaTeTime(),
                                        rekord["vizsgfok"].ToStrTrim(),
                                        rekord["vizsgdátumk"].ToÉrt_DaTeTime(),
                                        rekord["vizsgdátumv"].ToÉrt_DaTeTime(),
                                        rekord["vizsgkm"].ToÉrt_Long(),
                                        rekord["havikm"].ToÉrt_Long(),
                                        rekord["vizsgsorszám"].ToÉrt_Long(),
                                        rekord["fudátum"].ToÉrt_DaTeTime(),
                                        rekord["Teljeskm"].ToÉrt_Long(),
                                        rekord["Ciklusrend"].ToStrTrim(),
                                        rekord["V2végezte"].ToStrTrim(),
                                        rekord["KövV2_sorszám"].ToÉrt_Long(),
                                        rekord["KövV2"].ToStrTrim(),
                                        rekord["KövV_sorszám"].ToÉrt_Long(),
                                        rekord["KövV"].ToStrTrim(),
                                        rekord["törölt"].ToÉrt_Bool(),
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

        public Adat_Fogas_Km Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Fogas_Km Adat = null;

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
                            Adat = new Adat_Fogas_Km(
                                    rekord["id"].ToÉrt_Long(),
                                    rekord["azonosító"].ToString().Trim(),
                                    rekord["jjavszám"].ToÉrt_Long(),
                                    rekord["KMUkm"].ToÉrt_Long(),
                                    rekord["KMUdátum"].ToÉrt_DaTeTime(),
                                    rekord["vizsgfok"].ToString().Trim(),
                                    rekord["vizsgdátumk"].ToÉrt_DaTeTime(),
                                    rekord["vizsgdátumv"].ToÉrt_DaTeTime(),
                                    rekord["vizsgkm"].ToÉrt_Long(),
                                    rekord["havikm"].ToÉrt_Long(),
                                    rekord["vizsgsorszám"].ToÉrt_Long(),
                                    rekord["fudátum"].ToÉrt_DaTeTime(),
                                    rekord["Teljeskm"].ToÉrt_Long(),
                                    rekord["Ciklusrend"].ToString().Trim(),
                                    rekord["V2végezte"].ToString().Trim(),
                                    rekord["KövV2_sorszám"].ToÉrt_Long(),
                                    rekord["KövV2"].ToString().Trim(),
                                    rekord["KövV_sorszám"].ToÉrt_Long(),
                                    rekord["KövV"].ToString().Trim(),
                                    rekord["törölt"].ToÉrt_Bool(),
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

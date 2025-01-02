using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Nosztalgia_Állomány
    {
        public List<Adat_Nosztalgia_Állomány> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Nosztalgia_Állomány Adat = null;
            List<Adat_Nosztalgia_Állomány> Adatok = new List<Adat_Nosztalgia_Állomány>();

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
                                //DateTime utolsórögzítés = DateTime.TryParse(rekord["utolsórögzítés"].ToString(), out utolsórögzítés) ? utolsórögzítés : new DateTime(1900, 1, 1);
                                //DateTime vizsgálatdátuma = DateTime.TryParse(rekord["vizsgálatdátuma"].ToString(), out vizsgálatdátuma) ? vizsgálatdátuma : new DateTime(1900, 1, 1);
                                //DateTime utolsóforgalminap = DateTime.TryParse(rekord["utolsóforgalminap"].ToString(), out utolsóforgalminap) ? utolsóforgalminap : new DateTime(1900, 1, 1);

                                Adat = new Adat_Nosztalgia_Állomány(
                                                        rekord["azonosító"].ToStrTrim(),
                                                        rekord["ciklus_idő"].ToStrTrim(),
                                                        rekord["ciklus_km1"].ToStrTrim(),
                                                        rekord["ciklus_km2"].ToStrTrim(),
                                                        rekord["gyártó"].ToStrTrim(),
                                                        rekord["év"].ToÉrt_Int(),
                                                        rekord["Ntípus"].ToStrTrim(),
                                                        rekord["eszközszám"].ToStrTrim(),
                                                        rekord["leltári_szám"].ToStrTrim(),
                                                        rekord["vizsgálatdátuma_idő"].ToÉrt_DaTeTime(),
                                                        rekord["vizsgálatdátuma_km"].ToÉrt_DaTeTime(),
                                                        rekord["vizsgálatfokozata"].ToStrTrim(),
                                                        rekord["vizsgálatszáma_idő"].ToStrTrim(),
                                                        rekord["vizsgálatszáma_km"].ToStrTrim(),
                                                        rekord["utolsóforgalminap"].ToÉrt_DaTeTime(),
                                                        rekord["km_v"].ToÉrt_Int(),
                                                        rekord["km_u"].ToÉrt_Int(),
                                                        rekord["utolsórögzítés"].ToÉrt_DaTeTime(),
                                                        rekord["telephely"].ToStrTrim()
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

    public class Kezelő_Nosztagia_Futás
    {
        public List<Adat_Nosztagia_Futás> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Nosztagia_Futás Adat;
            List<Adat_Nosztagia_Futás> Adatok = new List<Adat_Nosztagia_Futás>();

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
                                Adat = new Adat_Nosztagia_Futás(
                                                        rekord["azonosító"].ToStrTrim(),
                                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                                        rekord["státusz"].ToÉrt_Bool(),
                                                        rekord["mikor"].ToÉrt_DaTeTime(),
                                                        rekord["ki"].ToString(),
                                                        rekord["telephely"].ToStrTrim()
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

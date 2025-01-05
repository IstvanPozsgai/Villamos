using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Beosztáskódok
    {
        readonly string jelszó = "Mocó";

        public List<Adat_Kiegészítő_Beosztáskódok> Lista_Adatok(string hely)
        {
            string szöveg = "SELECT * FROM Beosztáskódok";
            List<Adat_Kiegészítő_Beosztáskódok> Adatok = new List<Adat_Kiegészítő_Beosztáskódok>();
            Adat_Kiegészítő_Beosztáskódok Adat;

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
                                Adat = new Adat_Kiegészítő_Beosztáskódok(
                                        rekord["Sorszám"].ToÉrt_Long(),
                                        rekord["Beosztáskód"].ToStrTrim(),
                                        rekord["Munkaidőkezdet"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidővége"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidő"].ToÉrt_Int(),
                                        rekord["Munkarend"].ToÉrt_Int(),
                                        rekord["Napszak"].ToStrTrim(),
                                        rekord["Éjszakás"].ToÉrt_Bool(),
                                        rekord["Számoló"].ToÉrt_Bool(),
                                        rekord["0"].ToÉrt_Int(),
                                        rekord["1"].ToÉrt_Int(),
                                        rekord["2"].ToÉrt_Int(),
                                        rekord["3"].ToÉrt_Int(),
                                        rekord["4"].ToÉrt_Int(),
                                        rekord["5"].ToÉrt_Int(),
                                        rekord["6"].ToÉrt_Int(),
                                        rekord["7"].ToÉrt_Int(),
                                        rekord["8"].ToÉrt_Int(),
                                        rekord["9"].ToÉrt_Int(),
                                        rekord["10"].ToÉrt_Int(),
                                        rekord["11"].ToÉrt_Int(),
                                        rekord["12"].ToÉrt_Int(),
                                        rekord["13"].ToÉrt_Int(),
                                        rekord["14"].ToÉrt_Int(),
                                        rekord["15"].ToÉrt_Int(),
                                        rekord["16"].ToÉrt_Int(),
                                        rekord["17"].ToÉrt_Int(),
                                        rekord["18"].ToÉrt_Int(),
                                        rekord["19"].ToÉrt_Int(),
                                        rekord["20"].ToÉrt_Int(),
                                        rekord["21"].ToÉrt_Int(),
                                        rekord["22"].ToÉrt_Int(),
                                        rekord["23"].ToÉrt_Int(),
                                        rekord["Magyarázat"].ToStrTrim()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }



        public List<string> Lista_AdatBeoKód(string hely, string jelszó, string szöveg)
        {
            List<string> Adatok = new List<string>();
            string Adat;

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
                                Adat = rekord["beosztáskód"].ToStrTrim();
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Kiegészítő_Beosztáskódok> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Beosztáskódok> Adatok = new List<Adat_Kiegészítő_Beosztáskódok>();
            Adat_Kiegészítő_Beosztáskódok Adat;

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
                                Adat = new Adat_Kiegészítő_Beosztáskódok(
                                        rekord["Sorszám"].ToÉrt_Long(),
                                        rekord["Beosztáskód"].ToStrTrim(),
                                        rekord["Munkaidőkezdet"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidővége"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidő"].ToÉrt_Int(),
                                        rekord["Munkarend"].ToÉrt_Int(),
                                        rekord["Napszak"].ToStrTrim(),
                                        rekord["Éjszakás"].ToÉrt_Bool(),
                                        rekord["Számoló"].ToÉrt_Bool(),
                                        rekord["0"].ToÉrt_Int(),
                                        rekord["1"].ToÉrt_Int(),
                                        rekord["2"].ToÉrt_Int(),
                                        rekord["3"].ToÉrt_Int(),
                                        rekord["4"].ToÉrt_Int(),
                                        rekord["5"].ToÉrt_Int(),
                                        rekord["6"].ToÉrt_Int(),
                                        rekord["7"].ToÉrt_Int(),
                                        rekord["8"].ToÉrt_Int(),
                                        rekord["9"].ToÉrt_Int(),
                                        rekord["10"].ToÉrt_Int(),
                                        rekord["11"].ToÉrt_Int(),
                                        rekord["12"].ToÉrt_Int(),
                                        rekord["13"].ToÉrt_Int(),
                                        rekord["14"].ToÉrt_Int(),
                                        rekord["15"].ToÉrt_Int(),
                                        rekord["16"].ToÉrt_Int(),
                                        rekord["17"].ToÉrt_Int(),
                                        rekord["18"].ToÉrt_Int(),
                                        rekord["19"].ToÉrt_Int(),
                                        rekord["20"].ToÉrt_Int(),
                                        rekord["21"].ToÉrt_Int(),
                                        rekord["22"].ToÉrt_Int(),
                                        rekord["23"].ToÉrt_Int(),
                                        rekord["Magyarázat"].ToStrTrim()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public Adat_Kiegészítő_Beosztáskódok Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Kiegészítő_Beosztáskódok Adat = null;

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

                            Adat = new Adat_Kiegészítő_Beosztáskódok(
                                        rekord["Sorszám"].ToÉrt_Long(),
                                        rekord["Beosztáskód"].ToStrTrim(),
                                        rekord["Munkaidőkezdet"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidővége"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidő"].ToÉrt_Int(),
                                        rekord["Munkarend"].ToÉrt_Int(),
                                        rekord["Napszak"].ToStrTrim(),
                                        rekord["Éjszakás"].ToÉrt_Bool(),
                                        rekord["Számoló"].ToÉrt_Bool(),
                                        rekord["0"].ToÉrt_Int(),
                                        rekord["1"].ToÉrt_Int(),
                                        rekord["2"].ToÉrt_Int(),
                                        rekord["3"].ToÉrt_Int(),
                                        rekord["4"].ToÉrt_Int(),
                                        rekord["5"].ToÉrt_Int(),
                                        rekord["6"].ToÉrt_Int(),
                                        rekord["7"].ToÉrt_Int(),
                                        rekord["8"].ToÉrt_Int(),
                                        rekord["9"].ToÉrt_Int(),
                                        rekord["10"].ToÉrt_Int(),
                                        rekord["11"].ToÉrt_Int(),
                                        rekord["12"].ToÉrt_Int(),
                                        rekord["13"].ToÉrt_Int(),
                                        rekord["14"].ToÉrt_Int(),
                                        rekord["15"].ToÉrt_Int(),
                                        rekord["16"].ToÉrt_Int(),
                                        rekord["17"].ToÉrt_Int(),
                                        rekord["18"].ToÉrt_Int(),
                                        rekord["19"].ToÉrt_Int(),
                                        rekord["20"].ToÉrt_Int(),
                                        rekord["21"].ToÉrt_Int(),
                                        rekord["22"].ToÉrt_Int(),
                                        rekord["23"].ToÉrt_Int(),
                                        rekord["Magyarázat"].ToStrTrim()
                                      );
                        }
                    }
                }
            }
            return Adat;
        }
    }

}

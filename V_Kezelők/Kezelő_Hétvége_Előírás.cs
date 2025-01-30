using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Kezelők
{
    public class Kezelő_Hétvége_Előírás
    {
        public List<Adat_Hétvége_Előírás> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Hétvége_Előírás> Adatok = new List<Adat_Hétvége_Előírás>();
            Adat_Hétvége_Előírás Adat;

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
                                Adat = new Adat_Hétvége_Előírás(
                                        rekord["id"].ToÉrt_Long(),
                                        rekord["vonal"].ToStrTrim(),
                                        rekord["Mennyiség"].ToÉrt_Long(),
                                        rekord["red"].ToÉrt_Int(),
                                        rekord["green"].ToÉrt_Int(),
                                        rekord["blue"].ToÉrt_Int()
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

    public class Kezelő_Hétvége_Beosztás
    {

        public List<Adat_Hétvége_Beosztás> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Hétvége_Beosztás> Adatok = new List<Adat_Hétvége_Beosztás>();
            Adat_Hétvége_Beosztás Adat;

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
                                Adat = new Adat_Hétvége_Beosztás(
                                        rekord["id"].ToÉrt_Long(),
                                        rekord["vonal"].ToStrTrim(),
                                        rekord["kocsi1"].ToStrTrim(),
                                        rekord["kocsi2"].ToStrTrim(),
                                        rekord["kocsi3"].ToStrTrim(),
                                        rekord["kocsi4"].ToStrTrim(),
                                        rekord["kocsi5"].ToStrTrim(),
                                        rekord["kocsi6"].ToStrTrim(),
                                        rekord["vissza1"].ToStrTrim(),
                                        rekord["vissza2"].ToStrTrim(),
                                        rekord["vissza3"].ToStrTrim(),
                                        rekord["vissza4"].ToStrTrim(),
                                        rekord["vissza5"].ToStrTrim(),
                                        rekord["vissza6"].ToStrTrim()
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

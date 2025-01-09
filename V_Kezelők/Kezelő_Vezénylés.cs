using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Vezénylés
    {
        readonly string jelszó = "tápijános";
        public List<Adat_Vezénylés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Vezénylés> Adatok = new List<Adat_Vezénylés>();
            Adat_Vezénylés Adat = null;

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
                                Adat = new Adat_Vezénylés(
                                        rekord["azonosító"].ToStrTrim(),
                                        DateTime.Parse(rekord["dátum"].ToString()),
                                        rekord["státus"].ToÉrt_Int(),
                                        rekord["vizsgálatraütemez"].ToÉrt_Int(),
                                        rekord["takarításraütemez"].ToÉrt_Int(),
                                        rekord["vizsgálat"].ToStrTrim(),
                                        rekord["vizsgálatszám"].ToÉrt_Int(),
                                        rekord["rendelésiszám"].ToStrTrim(),
                                        rekord["álljon"].ToÉrt_Int(),
                                        rekord["fusson"].ToÉrt_Int(),
                                        rekord["törlés"].ToÉrt_Int(),
                                        rekord["szerelvényszám"].ToÉrt_Long(),
                                        rekord["típus"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Vezénylés> Lista_Adatok(string hely)
        {
            string szöveg = "SELECT * FROM vezényléstábla";
            List<Adat_Vezénylés> Adatok = new List<Adat_Vezénylés>();
            Adat_Vezénylés Adat = null;

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
                                Adat = new Adat_Vezénylés(
                                        rekord["azonosító"].ToStrTrim(),
                                        DateTime.Parse(rekord["dátum"].ToString()),
                                        rekord["státus"].ToÉrt_Int(),
                                        rekord["vizsgálatraütemez"].ToÉrt_Int(),
                                        rekord["takarításraütemez"].ToÉrt_Int(),
                                        rekord["vizsgálat"].ToStrTrim(),
                                        rekord["vizsgálatszám"].ToÉrt_Int(),
                                        rekord["rendelésiszám"].ToStrTrim(),
                                        rekord["álljon"].ToÉrt_Int(),
                                        rekord["fusson"].ToÉrt_Int(),
                                        rekord["törlés"].ToÉrt_Int(),
                                        rekord["szerelvényszám"].ToÉrt_Long(),
                                        rekord["típus"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<string> Lista_Pályaszámok(string hely, DateTime Dátum)
        {
            string szöveg = "SELECT * FROM vezényléstábla where törlés=0 and vizsgálatraütemez=1  and  vizsgálat='E3' ";
            szöveg += $" and dátum= #{Dátum:yyyy-MM-dd}#  order by  azonosító";
            List<string> Adatok = new List<string>();
            string Adat;
            try
            {
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
                                    Adat = rekord["Azonosító"].ToStrTrim();
                                    Adatok.Add(Adat);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Lista_Pályaszámok\n" + szöveg, ex.StackTrace, ex.Source, ex.HResult);
            }
            return Adatok;
        }
    }
}

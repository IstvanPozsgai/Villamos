using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_TW6000_Alap
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos4TW.mdb";
        readonly string jelszó = "czapmiklós";

        public Kezelő_TW6000_Alap()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.TW6000tábla(hely.KönyvSzerk());
        }

        public List<Adat_TW6000_Alap> Lista_Adatok()
        {
            List<Adat_TW6000_Alap> Adatok = new List<Adat_TW6000_Alap>();
            Adat_TW6000_Alap Adat;
            string szöveg = $"SELECT * FROM alap";

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
                                Adat = new Adat_TW6000_Alap(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Ciklusrend"].ToStrTrim(),
                                        rekord["Kötöttstart"].ToÉrt_Bool(),
                                        rekord["Megállítás"].ToÉrt_Bool(),
                                        rekord["Start"].ToÉrt_DaTeTime(),
                                        rekord["Vizsgdátum"].ToÉrt_DaTeTime(),
                                        rekord["Vizsgnév"].ToStrTrim(),
                                        rekord["Vizsgsorszám"].ToÉrt_Int());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }




        //Elkpoó
        public List<Adat_TW6000_Alap> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_TW6000_Alap> Adatok = new List<Adat_TW6000_Alap>();
            Adat_TW6000_Alap Adat;

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
                                Adat = new Adat_TW6000_Alap(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Ciklusrend"].ToStrTrim(),
                                        rekord["Kötöttstart"].ToÉrt_Bool(),
                                        rekord["Megállítás"].ToÉrt_Bool(),
                                        rekord["Start"].ToÉrt_DaTeTime(),
                                        rekord["Vizsgdátum"].ToÉrt_DaTeTime(),
                                        rekord["Vizsgnév"].ToStrTrim(),
                                        rekord["Vizsgsorszám"].ToÉrt_Int());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public Adat_TW6000_Alap Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_TW6000_Alap Adat = null;

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

                            Adat = new Adat_TW6000_Alap(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Ciklusrend"].ToStrTrim(),
                                    rekord["Kötöttstart"].ToÉrt_Bool(),
                                    rekord["Megállítás"].ToÉrt_Bool(),
                                    rekord["Start"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgdátum"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgnév"].ToStrTrim(),
                                    rekord["Vizsgsorszám"].ToÉrt_Int());
                        }
                    }
                }
            }
            return Adat;
        }
    }
}

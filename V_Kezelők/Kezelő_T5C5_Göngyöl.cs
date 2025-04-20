using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_T5C5_Göngyöl
    {
        string hely;
        readonly string jelszó = "pozsgaii";

        private void FájlBeállítás(string Telephely, DateTime Dátum)
        {
            if (Telephely == "Főmérnökség")
                hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\T5C5\Villamos3.mdb";
            else
                hely = $@"{Application.StartupPath}\{Telephely}\adatok\főkönyv\futás\{Dátum.Year}\Villamos3-{Dátum.AddDays(-1):yyyyMMdd}.mdb";

            if (!File.Exists(hely)) Adatbázis_Létrehozás.Futásnaptábla_Létrehozás(hely.KönyvSzerk());

        }



        public List<Adat_T5C5_Göngyöl> Lista_Adatok(string Telephely, DateTime Dátum)
        {
            FájlBeállítás(Telephely, Dátum);
            string szöveg = $"SELECT * FROM Állománytábla ORDER BY azonosító";
            List<Adat_T5C5_Göngyöl> Adatok = new List<Adat_T5C5_Göngyöl>();
            Adat_T5C5_Göngyöl Adat;

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
                                Adat = new Adat_T5C5_Göngyöl(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Utolsórögzítés"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgálatdátuma"].ToÉrt_DaTeTime(),
                                    rekord["Utolsóforgalminap"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgálatfokozata"].ToStrTrim(),
                                    rekord["Vizsgálatszáma"].ToÉrt_Int(),
                                    rekord["Futásnap"].ToÉrt_Int(),
                                    rekord["Telephely"].ToStrTrim()
                                    ); ;
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        //Elkopó
        public List<Adat_T5C5_Göngyöl> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_T5C5_Göngyöl> Adatok = new List<Adat_T5C5_Göngyöl>();
            Adat_T5C5_Göngyöl Adat;

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
                                Adat = new Adat_T5C5_Göngyöl(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Utolsórögzítés"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgálatdátuma"].ToÉrt_DaTeTime(),
                                    rekord["Utolsóforgalminap"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgálatfokozata"].ToStrTrim(),
                                    rekord["Vizsgálatszáma"].ToÉrt_Int(),
                                    rekord["Futásnap"].ToÉrt_Int(),
                                    rekord["Telephely"].ToStrTrim()
                                    ); ;
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_T5C5_Göngyöl Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_T5C5_Göngyöl Adat = null;

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
                                Adat = new Adat_T5C5_Göngyöl(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Utolsórögzítés"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgálatdátuma"].ToÉrt_DaTeTime(),
                                    rekord["Utolsóforgalminap"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgálatfokozata"].ToStrTrim(),
                                    rekord["Vizsgálatszáma"].ToÉrt_Int(),
                                    rekord["Futásnap"].ToÉrt_Int(),
                                    rekord["Telephely"].ToStrTrim()
                                    ); ;
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }
}

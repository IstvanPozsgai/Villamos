using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Nosztalgia_Tevékenység
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Nosztalgia\FutásnapNoszt.mdb";
        readonly string jelszó = "kloczkal";
        readonly string táblanév = "Tevékenység";

        public Kezelő_Nosztalgia_Tevékenység()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Futásnaptábla_Nosztalgia(hely.KönyvSzerk());
        }

        public List<Adat_Nosztalgia_Tevékenység> Lista_Adat()
        {
            string szöveg = $"SELECT * FROM {táblanév}";

            Adat_Nosztalgia_Tevékenység Adat;
            List<Adat_Nosztalgia_Tevékenység> Adatok = new List<Adat_Nosztalgia_Tevékenység>();

            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";
            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                using (OleDbCommand Parancs = new OleDbCommand(szöveg, Kapcsolat))
                {
                    Kapcsolat.Open();
                    using (OleDbDataReader rekord = Parancs.ExecuteReader())
                    {
                        if (rekord.HasRows)
                        {
                            while (rekord.Read())
                            {

                                Adat = new Adat_Nosztalgia_Tevékenység(
                                                        rekord["azonosító"].ToStrTrim(),
                                                        rekord["ciklus_idő"].ToStrTrim(),
                                                        rekord["ciklus_km1"].ToStrTrim(),
                                                        rekord["ciklus_km2"].ToStrTrim(),
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

    public class Kezelő_Nosztalgia_Állomány
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Nosztalgia\FutásnapNoszt.mdb";
        readonly string jelszó = "kloczkal";
        readonly string táblanév = "Állomány";

        public Kezelő_Nosztalgia_Állomány()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Futásnaptábla_Nosztalgia(hely.KönyvSzerk());
        }

        public List<Adat_Nosztalgia_Állomány> Lista_Adat()
        {
            string szöveg = $"SELECT * FROM {táblanév}";

            Adat_Nosztalgia_Állomány Adat;
            List<Adat_Nosztalgia_Állomány> Adatok = new List<Adat_Nosztalgia_Állomány>();

            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";
            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                using (OleDbCommand Parancs = new OleDbCommand(szöveg, Kapcsolat))
                {
                    Kapcsolat.Open();
                    using (OleDbDataReader rekord = Parancs.ExecuteReader())
                    {
                        if (rekord.HasRows)
                        {
                            while (rekord.Read())
                            {

                                Adat = new Adat_Nosztalgia_Állomány(
                                                        rekord["azonosító"].ToStrTrim(),
                                                        rekord["gyártó"].ToStrTrim(),
                                                        rekord["év"].ToStrTrim(),
                                                        rekord["Ntípus"].ToStrTrim(),
                                                        rekord["eszközszám"].ToStrTrim(),
                                                        rekord["leltári_szám"].ToStrTrim()
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
        readonly string jelszó = "kloczkal";
        readonly string táblanév = "Futás";
        public List<Adat_Nosztagia_Futás> Lista_Adat(DateTime Dátum)
        {

            string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Nosztalgia\Futás_{Dátum.Year}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.NosztFutás(hely.KönyvSzerk());

            string szöveg = $"SELECT * FROM {táblanév}";

            Adat_Nosztagia_Futás Adat;
            List<Adat_Nosztagia_Futás> Adatok = new List<Adat_Nosztagia_Futás>();

            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";
            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                using (OleDbCommand Parancs = new OleDbCommand(szöveg, Kapcsolat))
                {
                    Kapcsolat.Open();
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

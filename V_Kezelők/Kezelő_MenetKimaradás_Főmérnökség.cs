using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_MenetKimaradás_Főmérnökség
    {
        readonly string jelszó = "lilaakác";
        readonly string táblanév = "menettábla";
        string hely;

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Év}\{Év}_menet_adatok.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Menekimaradás_Főmérnökség(hely.KönyvSzerk());
        }

        public List<Adat_Menetkimaradás_Főmérnökség> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            List<Adat_Menetkimaradás_Főmérnökség> Adatok = new List<Adat_Menetkimaradás_Főmérnökség>();
            Adat_Menetkimaradás_Főmérnökség Adat;
            string szöveg = $"SELECT * FROM {táblanév}";

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
                                Adat = new Adat_Menetkimaradás_Főmérnökség(
                                    rekord["viszonylat"].ToStrTrim(),
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["eseményjele"].ToStrTrim(),
                                    rekord["bekövetkezés"].ToÉrt_DaTeTime(),
                                    rekord["kimaradtmenet"].ToÉrt_Long(),
                                    rekord["jvbeírás"].ToStrTrim(),
                                    rekord["vmbeírás"].ToStrTrim(),
                                    rekord["javítás"].ToStrTrim(),
                                    rekord["id"].ToÉrt_Long(),
                                    rekord["törölt"].ToÉrt_Bool(),
                                    rekord["jelentés"].ToStrTrim(),
                                    rekord["tétel"].ToÉrt_Long(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["szolgálat"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        //Elkopó
        public List<Adat_Menetkimaradás_Főmérnökség> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Menetkimaradás_Főmérnökség> Adatok = new List<Adat_Menetkimaradás_Főmérnökség>();
            Adat_Menetkimaradás_Főmérnökség Adat;

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
                                Adat = new Adat_Menetkimaradás_Főmérnökség(
                                    rekord["viszonylat"].ToStrTrim(),
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["eseményjele"].ToStrTrim(),
                                    rekord["bekövetkezés"].ToÉrt_DaTeTime(),
                                    rekord["kimaradtmenet"].ToÉrt_Long(),
                                    rekord["jvbeírás"].ToStrTrim(),
                                    rekord["vmbeírás"].ToStrTrim(),
                                    rekord["javítás"].ToStrTrim(),
                                    rekord["id"].ToÉrt_Long(),
                                    rekord["törölt"].ToÉrt_Bool(),
                                    rekord["jelentés"].ToStrTrim(),
                                    rekord["tétel"].ToÉrt_Long(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["szolgálat"].ToStrTrim()
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

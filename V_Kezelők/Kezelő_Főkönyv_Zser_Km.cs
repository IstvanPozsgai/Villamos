using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Főkönyv_Zser_Km
    {
        readonly string jelszó = "pozsgaii";
        string hely;

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Év}\Napi_km_Zser_{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.ZSER_km(hely.KönyvSzerk());
        }

        public List<Adat_Főkönyv_Zser_Km> Lista_adatok(int Év)
        {
            FájlBeállítás(Év);
            string szöveg = "SELECT * FROM tábla";
            List<Adat_Főkönyv_Zser_Km> Adatok = new List<Adat_Főkönyv_Zser_Km>();
            Adat_Főkönyv_Zser_Km Adat;

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
                                Adat = new Adat_Főkönyv_Zser_Km(
                                    rekord["azonosító"].ToStrTrim(),
                                    rekord["Dátum"].ToÉrt_DaTeTime(),
                                    rekord["Napikm"].ToÉrt_Int(),
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


        public List<Adat_Főkönyv_Zser_Km> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Főkönyv_Zser_Km> Adatok = new List<Adat_Főkönyv_Zser_Km>();
            Adat_Főkönyv_Zser_Km Adat;

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
                                Adat = new Adat_Főkönyv_Zser_Km(
                                    rekord["azonosító"].ToStrTrim(),
                                    rekord["Dátum"].ToÉrt_DaTeTime(),
                                    rekord["Napikm"].ToÉrt_Int(),
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

using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;


namespace Villamos.Kezelők
{
    public class Kezelő_Nap_Hiba
    {
        string hely;
        readonly string jelszó = "pozsgaii";

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\villamos\Új_napihiba.mdb";
            //nincs elkészítve
            // if (!File.Exists(hely)) Adatbázis_Létrehozás   (hely.KönyvSzerk());
        }

        public List<Adat_Nap_Hiba> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Nap_Hiba> Adatok = new List<Adat_Nap_Hiba>();
            Adat_Nap_Hiba Adat;
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
                                Adat = new Adat_Nap_Hiba(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["mikori"].ToÉrt_DaTeTime(),
                                    rekord["beálló"].ToStrTrim(),
                                    rekord["üzemképtelen"].ToStrTrim(),
                                    rekord["üzemképeshiba"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["státus"].ToÉrt_Long()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Nap_Hiba> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            string szöveg = "SELECT * FROM hiba";
            List<Adat_Nap_Hiba> Adatok = new List<Adat_Nap_Hiba>();
            Adat_Nap_Hiba Adat;
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
                                Adat = new Adat_Nap_Hiba(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["mikori"].ToÉrt_DaTeTime(),
                                    rekord["beálló"].ToStrTrim(),
                                    rekord["üzemképtelen"].ToStrTrim(),
                                    rekord["üzemképeshiba"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["státus"].ToÉrt_Long()
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

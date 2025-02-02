using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Jármű_Takarítás_Napló
    {
        readonly string jelszó = "seprűéslapát";
        string hely;

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Takarítás\Jármű_takarítás_Napló_{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Járműtakarító_Főmérnök_Napló(hely.KönyvSzerk());
        }

        public List<Adat_Jármű_Takarítás_Napló> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            string szöveg = "SELECT * FROM takarítások_napló";
            List<Adat_Jármű_Takarítás_Napló> Adatok = new List<Adat_Jármű_Takarítás_Napló>();
            Adat_Jármű_Takarítás_Napló Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Napló(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["Takarítási_fajta"].ToStrTrim(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["Mikor"].ToÉrt_DaTeTime(),
                                        rekord["Módosító"].ToStrTrim(),
                                        rekord["státus"].ToÉrt_Int()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Jármű_Takarítás_Napló> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_Napló> Adatok = new List<Adat_Jármű_Takarítás_Napló>();
            Adat_Jármű_Takarítás_Napló Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Napló(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["Takarítási_fajta"].ToStrTrim(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["Mikor"].ToÉrt_DaTeTime(),
                                        rekord["Módosító"].ToStrTrim(),
                                        rekord["státus"].ToÉrt_Int()
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

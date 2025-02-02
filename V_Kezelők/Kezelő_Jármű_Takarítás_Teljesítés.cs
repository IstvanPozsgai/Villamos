using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Jármű_Takarítás_Teljesítés
    {
        readonly string jelszó = "seprűéslapát";
        string hely;

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Takarítás\Takarítás_{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Járműtakarító_Telephely_tábla(hely.KönyvSzerk());
        }

        public List<Adat_Jármű_Takarítás_Teljesítés> Lista_Adat(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = "SELECT * FROM teljesítés";

            List<Adat_Jármű_Takarítás_Teljesítés> Adatok = new List<Adat_Jármű_Takarítás_Teljesítés>();
            Adat_Jármű_Takarítás_Teljesítés Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Teljesítés(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["takarítási_fajta"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["megfelelt1"].ToÉrt_Int(),
                                        rekord["státus"].ToÉrt_Int(),
                                        rekord["megfelelt2"].ToÉrt_Int(),
                                        rekord["pótdátum"].ToÉrt_Bool(),
                                        rekord["napszak"].ToÉrt_Int(),
                                        rekord["mérték"].ToÉrt_Double(),
                                        rekord["típus"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Jármű_Takarítás_Teljesítés> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_Teljesítés> Adatok = new List<Adat_Jármű_Takarítás_Teljesítés>();
            Adat_Jármű_Takarítás_Teljesítés Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Teljesítés(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["takarítási_fajta"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["megfelelt1"].ToÉrt_Int(),
                                        rekord["státus"].ToÉrt_Int(),
                                        rekord["megfelelt2"].ToÉrt_Int(),
                                        rekord["pótdátum"].ToÉrt_Bool(),
                                        rekord["napszak"].ToÉrt_Int(),
                                        rekord["mérték"].ToÉrt_Double(),
                                        rekord["típus"].ToStrTrim());
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

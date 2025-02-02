using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Jármű_Takarítás
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Takarítás\Jármű_takarítás.mdb";
        readonly string jelszó = "seprűéslapát";

        public Kezelő_Jármű_Takarítás()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Járműtakarító_Főmérnök_tábla(hely.KönyvSzerk());
        }

        public List<Adat_Jármű_Takarítás_Takarítások> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM takarítások";
            List<Adat_Jármű_Takarítás_Takarítások> Adatok = new List<Adat_Jármű_Takarítás_Takarítások>();
            Adat_Jármű_Takarítás_Takarítások Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Takarítások(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["takarítási_fajta"].ToStrTrim(),
                                        rekord["telephely"].ToStrTrim(),
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

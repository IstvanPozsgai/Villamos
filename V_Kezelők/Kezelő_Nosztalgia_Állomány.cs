using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;

namespace Villamos.Kezelők
{
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
}

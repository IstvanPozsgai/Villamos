using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
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

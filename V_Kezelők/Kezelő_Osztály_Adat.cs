using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;


namespace Villamos.Kezelők
{
    public class Kezelő_Osztály_Adat
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\osztály.mdb";
        readonly string jelszó = "kéménybe";

        public Kezelő_Osztály_Adat()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Osztálytábla(hely.KönyvSzerk());
        }

        public List<Adat_Osztály_Adat> Lista_Adat()
        {
            string szöveg = "select * from osztályadatok";
            List<Adat_Osztály_Adat> Adatok = new List<Adat_Osztály_Adat>();
            Adat_Osztály_Adat Adat;

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
                                List<string> AdatokGy = new List<string>();
                                List<string> Mezőnevek = new List<string>();
                                for (int i = 0; i < rekord.FieldCount; i++)
                                {
                                    string Mezőnév = rekord.GetName(i).ToStrTrim();
                                    if (Mezőnév.Contains("Adat"))
                                    {
                                        AdatokGy.Add(rekord.GetString(i).ToStrTrim());
                                        Mezőnevek.Add(Mezőnév);
                                    }
                                }

                                Adat = new Adat_Osztály_Adat(
                                    rekord["Azonosító"].ToStrTrim(),
                                    AdatokGy,
                                    Mezőnevek
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

using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Épület_Takarításrakijelölt
    {
        string hely;
        readonly string jelszó = "seprűéslapát";
        readonly string táblanév = "takarításrakijelölt";

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely.Trim()}\Adatok\Épület\{Év}épülettakarítás.mdb".KönyvSzerk();
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Épülettakarítótábla(hely);
        }

        public List<Adat_Épület_Takarításrakijelölt> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = "SELECT * FROM takarításrakijelölt";
            List<Adat_Épület_Takarításrakijelölt> Adatok = new List<Adat_Épület_Takarításrakijelölt>();

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
                                Adat_Épület_Takarításrakijelölt Adat = new Adat_Épület_Takarításrakijelölt(
                                          rekord["E1elvégzettdb"].ToÉrt_Int(),
                                          rekord["E1kijelöltdb"].ToÉrt_Int(),
                                          rekord["E1rekijelölt"].ToStrTrim(),
                                          rekord["E2elvégzettdb"].ToÉrt_Int(),
                                          rekord["E2kijelöltdb"].ToÉrt_Int(),
                                          rekord["E2rekijelölt"].ToStrTrim(),
                                          rekord["E3elvégzettdb"].ToÉrt_Int(),
                                          rekord["E3kijelöltdb"].ToÉrt_Int(),
                                          rekord["E3rekijelölt"].ToStrTrim(),
                                          rekord["Helységkód"].ToStrTrim(),
                                          rekord["Hónap"].ToÉrt_Int(),
                                          rekord["Megnevezés"].ToStrTrim(),
                                          rekord["Osztály"].ToStrTrim()
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

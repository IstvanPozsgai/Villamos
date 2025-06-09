using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Épület_Adattábla
    {
        readonly string jelszó = "seprűéslapát";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Épület\épülettörzs.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Épülettakarításlétrehozás(hely);
        }

        public List<Adat_Épület_Adattábla> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            List<Adat_Épület_Adattábla> Adatok = new List<Adat_Épület_Adattábla>();
            Adat_Épület_Adattábla Adat;
            string szöveg = "SELECT * FROM Adattábla ";

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
                                Adat = new Adat_Épület_Adattábla(
                                          rekord["ID"].ToÉrt_Int(),
                                          rekord["Megnevezés"].ToStrTrim(),
                                          rekord["Osztály"].ToStrTrim(),
                                          rekord["Méret"].ToÉrt_Double(),
                                          rekord["Helységkód"].ToStrTrim(),
                                          rekord["Státus"].ToÉrt_Bool(),
                                          rekord["E1évdb"].ToÉrt_Int(),
                                          rekord["E2évdb"].ToÉrt_Int(),
                                          rekord["E3évdb"].ToÉrt_Int(),
                                          rekord["Kezd"].ToStrTrim(),
                                          rekord["Végez"].ToStrTrim(),
                                          rekord["Ellenőremail"].ToStrTrim(),
                                          rekord["Ellenőrneve"].ToStrTrim(),
                                          rekord["Ellenőrtelefonszám"].ToStrTrim(),
                                          rekord["Szemetes"].ToÉrt_Bool(),
                                          rekord["Kapcsolthelység"].ToStrTrim()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        //elkopó
        public List<Adat_Épület_Adattábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Épület_Adattábla> Adatok = new List<Adat_Épület_Adattábla>();
            Adat_Épület_Adattábla Adat;

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
                                Adat = new Adat_Épület_Adattábla(
                                          rekord["ID"].ToÉrt_Int(),
                                          rekord["Megnevezés"].ToStrTrim(),
                                          rekord["Osztály"].ToStrTrim(),
                                          rekord["Méret"].ToÉrt_Double(),
                                          rekord["Helységkód"].ToStrTrim(),
                                          rekord["Státus"].ToÉrt_Bool(),
                                          rekord["E1évdb"].ToÉrt_Int(),
                                          rekord["E2évdb"].ToÉrt_Int(),
                                          rekord["E3évdb"].ToÉrt_Int(),
                                          rekord["Kezd"].ToStrTrim(),
                                          rekord["Végez"].ToStrTrim(),
                                          rekord["Ellenőremail"].ToStrTrim(),
                                          rekord["Ellenőrneve"].ToStrTrim(),
                                          rekord["Ellenőrtelefonszám"].ToStrTrim(),
                                          rekord["Szemetes"].ToÉrt_Bool(),
                                          rekord["Kapcsolthelység"].ToStrTrim()
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

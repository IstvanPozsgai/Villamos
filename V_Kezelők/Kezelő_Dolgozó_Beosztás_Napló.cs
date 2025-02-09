using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Dolgozó_Beosztás_Napló
    {
        readonly string jelszó = "kerekeskút";
        string hely;

        private void FájlBeállítás(string Telephely, DateTime Dátum)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\naplózás\{Dátum:yyyyMM}napló.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Beosztás_Naplózása(hely.KönyvSzerk());
        }

        public List<Adat_Dolgozó_Beosztás_Napló> Lista_Adatok(string Telephely, DateTime Dátum)
        {
            FájlBeállítás(Telephely, Dátum);
            string szöveg = $"SELECT * FROM adatok ";
            List<Adat_Dolgozó_Beosztás_Napló> Adatok = new List<Adat_Dolgozó_Beosztás_Napló>();
            Adat_Dolgozó_Beosztás_Napló Adat;

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
                                Adat = new Adat_Dolgozó_Beosztás_Napló(
                                          rekord["Sorszám"].ToÉrt_Double(),
                                          rekord["Dátum"].ToÉrt_DaTeTime(),
                                          rekord["Beosztáskód"].ToStrTrim(),
                                          rekord["Túlóra"].ToÉrt_Int(),
                                          rekord["Túlórakezd"].ToÉrt_DaTeTime(),
                                          rekord["Túlóravég"].ToÉrt_DaTeTime(),
                                          rekord["Csúszóra"].ToÉrt_Int(),
                                          rekord["CSúszórakezd"].ToÉrt_DaTeTime(),
                                          rekord["Csúszóravég"].ToÉrt_DaTeTime(),
                                          rekord["Megjegyzés"].ToStrTrim(),
                                          rekord["Túlóraok"].ToStrTrim(),
                                          rekord["Szabiok"].ToStrTrim(),
                                          rekord["kért"].ToÉrt_Bool(),
                                          rekord["Csúszok"].ToStrTrim(),
                                          rekord["Rögzítette"].ToStrTrim(),
                                          rekord["Rögzítésdátum"].ToÉrt_DaTeTime(),
                                          rekord["dolgozónév"].ToStrTrim(),
                                          rekord["Törzsszám"].ToStrTrim(),
                                          rekord["AFTóra"].ToÉrt_Int(),
                                          rekord["AFTok"].ToStrTrim()
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

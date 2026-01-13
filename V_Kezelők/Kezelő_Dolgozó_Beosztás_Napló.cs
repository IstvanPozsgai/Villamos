using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Dolgozó_Beosztás_Napló
    {
        readonly string jelszó = "kerekeskút";
        string hely;
        string táblanév = "adatok";

        private void FájlBeállítás(string Telephely, DateTime Dátum)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\naplózás\{Dátum:yyyyMM}napló.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Beosztás_Naplózása(hely.KönyvSzerk());
        }

        public List<Adat_Dolgozó_Beosztás_Napló> Lista_Adatok(string Telephely, DateTime Dátum)
        {
            FájlBeállítás(Telephely, Dátum);
            string szöveg = $"SELECT * FROM {táblanév} ";
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


        public void Rögzítés(string Telephely, DateTime Dátum, List<Adat_Dolgozó_Beosztás_Napló> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Dolgozó_Beosztás_Napló Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév} (Sorszám, Dátum, Beosztáskód, Túlóra, Túlórakezd, Túlóravég, Csúszóra, CSúszórakezd, Csúszóravég, Megjegyzés,";
                    szöveg += " Túlóraok, Szabiok, Kért, Csúszok, Rögzítette, rögzítésdátum, dolgozónév, Törzsszám,AFTóra, AFTok )";
                    szöveg += " VALUES (";
                    szöveg += $"'{Adat.Sorszám}', ";// Sorszám
                    szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', ";// Dátum
                    szöveg += $"'{Adat.Beosztáskód}', ";// Beosztáskód
                    szöveg += $"{Adat.Túlóra}, ";// Túlóra
                    szöveg += $"'{Adat.Túlórakezd}', ";// Túlórakezd
                    szöveg += $"'{Adat.Túlóravég}', ";// Túlóravég
                    szöveg += $"{Adat.Csúszóra}, ";// Csúszóra
                    szöveg += $"'{Adat.CSúszórakezd}', ";// CSúszórakezd
                    szöveg += $"'{Adat.Csúszóravég}', ";// Csúszóravég
                    szöveg += $"'{Adat.Megjegyzés}', ";// Megjegyzés
                    szöveg += $"'{Adat.Túlóraok}', ";// Túlóraok
                    szöveg += $"'{Adat.Szabiok}', ";// Szabiok
                    szöveg += $"{Adat.Kért}, ";// Kért
                    szöveg += $"'{Adat.Csúszok}', ";// Csúszok
                    szöveg += $"'{Adat.Rögzítette}', ";// Rögzítette
                    szöveg += $"'{Adat.Rögzítésdátum}', ";// rögzítésdátum
                    szöveg += $"'{Adat.Dolgozónév}',";// dolgozónév
                    szöveg += $"'{Adat.Törzsszám}',";// Törzsszám
                    szöveg += $"{Adat.AFTóra}, ";// AFTóra
                    szöveg += $"'{Adat.AFTok}' ";// AFTok
                    szöveg += ")";
                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

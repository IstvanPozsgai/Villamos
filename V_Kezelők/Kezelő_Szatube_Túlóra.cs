using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Szatube_Túlóra
    {
        string hely;
        readonly string jelszó = "kertitörpe";
        readonly string táblanév = "Túlóra";

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\szatubecs\{Év}szatubecs.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.SzaTuBe_tábla(hely.KönyvSzerk());
        }

        public List<Adat_Szatube_Túlóra> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Szatube_Túlóra> Adatok = new List<Adat_Szatube_Túlóra>();
            Adat_Szatube_Túlóra Adat;

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

                                Adat = new Adat_Szatube_Túlóra(
                                          rekord["sorszám"].ToÉrt_Double(),
                                          rekord["Törzsszám"].ToStrTrim(),
                                          rekord["Dolgozónév"].ToStrTrim(),
                                          rekord["Kezdődátum"].ToÉrt_DaTeTime(),
                                          rekord["Befejeződátum"].ToÉrt_DaTeTime(),
                                          rekord["Kivettnap"].ToÉrt_Int(),
                                          rekord["Szabiok"].ToStrTrim(),
                                          rekord["Státus"].ToÉrt_Int(),
                                          rekord["Rögzítette"].ToStrTrim(),
                                          rekord["rögzítésdátum"].ToÉrt_DaTeTime(),
                                          rekord["Kezdőidő"].ToÉrt_DaTeTime(),
                                          rekord["Befejezőidő"].ToÉrt_DaTeTime()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(string Telephely, int Év, DateTime Dátumtól, DateTime Dátumig, List<string> Hr_Azonosítók, int Státus)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                List<string> szövegGy = new List<string>();
                foreach (string Hr_Azonosító in Hr_Azonosítók)
                {
                    string szöveg = $"UPDATE {táblanév} SET státus={Státus} WHERE törzsszám='{Hr_Azonosító}' AND  kezdődátum>=#{Dátumtól:M-d-yy}#";
                    szöveg += $" AND  kezdődátum<=#{Dátumig:M-d-yy}#";
                    szövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);
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

        public void Státus(string Telephely, int Év, List<double> Sorszámok, int státus)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                List<string> SzövegGy = new List<string>();
                foreach (double Sorszám in Sorszámok)
                {
                    string szöveg = $"Update {táblanév} set státus={státus} Where sorszám={Sorszám}";
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

        //Elkopó
        public List<Adat_Szatube_Túlóra> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.SzaTuBe_tábla(hely);
            List<Adat_Szatube_Túlóra> Adatok = new List<Adat_Szatube_Túlóra>();
            Adat_Szatube_Túlóra Adat;

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

                                Adat = new Adat_Szatube_Túlóra(
                                          rekord["sorszám"].ToÉrt_Double(),
                                          rekord["Törzsszám"].ToStrTrim(),
                                          rekord["Dolgozónév"].ToStrTrim(),
                                          rekord["Kezdődátum"].ToÉrt_DaTeTime(),
                                          rekord["Befejeződátum"].ToÉrt_DaTeTime(),
                                          rekord["Kivettnap"].ToÉrt_Int(),
                                          rekord["Szabiok"].ToStrTrim(),
                                          rekord["Státus"].ToÉrt_Int(),
                                          rekord["Rögzítette"].ToStrTrim(),
                                          rekord["rögzítésdátum"].ToÉrt_DaTeTime(),
                                          rekord["Kezdőidő"].ToÉrt_DaTeTime(),
                                          rekord["Befejezőidő"].ToÉrt_DaTeTime()
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

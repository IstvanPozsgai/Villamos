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
    public class Kezelő_Szatube_Túlóra
    {
        string hely;
        readonly string jelszó = "kertitörpe";
        readonly string táblanév = "Túlóra";

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\szatubecs\{Év}szatubecs.mdb";
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

        public void Módosítás(string Telephely, int Év, Adat_Szatube_Túlóra Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"törzsszám='{Adat.Törzsszám}', "; //törzsszám
                szöveg += $"dolgozónév='{Adat.Dolgozónév.Trim()}', "; //dolgozónév
                szöveg += $"Kezdődátum='{Adat.Kezdődátum:yyyy.MM.dd}', "; //Kezdődátum
                szöveg += $"Befejeződátum='{Adat.Befejeződátum:yyyy.MM.dd}', "; //Befejeződátum
                szöveg += $"Kivettnap={Adat.Kivettnap}, ";   //Kivettnap
                szöveg += $"Szabiok='{Adat.Szabiok.Trim()}', "; //Szabiok
                szöveg += $"rögzítette='{Adat.Rögzítette.Trim()}', "; //rögzítette
                szöveg += $"rögzítésdátum='{Adat.Rögzítésdátum}',  "; //rögzítésdátum
                szöveg += $"Kezdőidő='{Adat.Kezdőidő:HH:mm:ss}',  "; //Kezdőidő
                szöveg += $"Befejezőidő='{Adat.Befejezőidő:HH:mm:ss}'  "; //Befejezőidő
                szöveg += $" WHERE törzsszám='{Adat.Törzsszám.Trim()}' AND [Kezdődátum]=#{Adat.Kezdődátum:M-d-yy}# AND [státus]<>3";
                MyA.ABMódosítás(hely, jelszó, szöveg);
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

        public void Törlés(string Telephely, int Év, List<Adat_Szatube_Túlóra> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Szatube_Túlóra Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév} SET ";
                    szöveg += $"Státus={Adat.Státus} ";   //Státus
                    szöveg += $" WHERE törzsszám='{Adat.Törzsszám}' AND [Kezdődátum]=#{Adat.Kezdődátum:M-d-yy}# AND [státus]<>3";
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

        public void Rögzítés(string Telephely, int Év, Adat_Szatube_Túlóra Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"INSERT INTO {táblanév} ";
                szöveg += "(Sorszám, Törzsszám, Dolgozónév, Kezdődátum, Befejeződátum, Kivettnap, Szabiok, Státus, Rögzítette, Rögzítésdátum, Kezdőidő, Befejezőidő) VALUES (";
                szöveg += $"{Adat.Sorszám}, ";   //Sorszám
                szöveg += $"'{Adat.Törzsszám.Trim()}', "; //törzsszám
                szöveg += $"'{Adat.Dolgozónév.Trim()}', "; //dolgozónév
                szöveg += $"'{Adat.Kezdődátum:yyyy.MM.dd}', "; //Kezdődátum
                szöveg += $"'{Adat.Befejeződátum:yyyy.MM.dd}', "; //Befejeződátum
                szöveg += $"{Adat.Kivettnap}, ";   //Kivettnap
                szöveg += $"'{Adat.Szabiok.Trim()}', "; //Szabiok
                szöveg += $"{Adat.Státus}, ";   //Státus
                szöveg += $"'{Adat.Rögzítette}', "; //rögzítette
                szöveg += $"'{Adat.Rögzítésdátum}', "; //rögzítésdátum
                szöveg += $"'{Adat.Kezdőidő:HH:mm:ss}', "; //Kezdőidő
                szöveg += $"'{Adat.Befejezőidő:HH:mm:ss}') "; //Befejezőidő
                MyA.ABMódosítás(hely, jelszó, szöveg);
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

using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;
namespace Villamos.Kezelők
{
    public class Kezelő_Szatube_Szabadság
    {
        readonly string jelszó = "kertitörpe";
        string hely;
        readonly string táblanév = "Szabadság";

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\Szatubecs\{Év}SzaTuBeCs.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.SzaTuBe_tábla(hely);
        }

        public List<Adat_Szatube_Szabadság> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = $"Select * FROM {táblanév}";
            List<Adat_Szatube_Szabadság> Adatok = new List<Adat_Szatube_Szabadság>();


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

                                Adat_Szatube_Szabadság Adat = new Adat_Szatube_Szabadság(
                                          rekord["sorszám"].ToÉrt_Double(),
                                          rekord["Törzsszám"].ToStrTrim(),
                                          rekord["Dolgozónév"].ToStrTrim(),
                                          rekord["Kezdődátum"].ToÉrt_DaTeTime(),
                                          rekord["Befejeződátum"].ToÉrt_DaTeTime(),
                                          rekord["Kivettnap"].ToÉrt_Int(),
                                          rekord["Szabiok"].ToStrTrim(),
                                          rekord["Státus"].ToÉrt_Int(),
                                          rekord["Rögzítette"].ToStrTrim(),
                                          rekord["rögzítésdátum"].ToÉrt_DaTeTime()
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

        public void Módosítás(string Telephely, int Év, Adat_Szatube_Szabadság Adat, double sorszám)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"UPDATE {táblanév} SET sorszám={sorszám} WHERE Törzsszám='{Adat.Törzsszám.Trim()}' AND Kezdődátum>=#{Adat.Kezdődátum:yyyy-MM-dd}#";
                szöveg += $"AND  Befejeződátum<=#{Adat.Befejeződátum:yyyy-MM-dd}# AND státus<>3";
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

        public void Módosítás(string Telephely, int Év, Adat_Szatube_Szabadság Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"UPDATE szabadság SET szabiok='{Adat.Szabiok}' WHERE törzsszám='{Adat.Törzsszám}' AND [Kezdődátum]=#{Adat.Kezdődátum:M-d-yy}# AND [státus]<>3";
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

        public void Rögzítés(string Telephely, int Év, Adat_Szatube_Szabadság Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"INSERT INTO {táblanév} ";
                szöveg += " (Sorszám ,törzsszám, dolgozónév, kezdődátum, befejeződátum, kivettnap, Szabiok, Státus, rögzítette, rögzítésdátum )";
                szöveg += " VALUES (";
                szöveg += $"{Adat.Sorszám},";                   //Sorszám
                szöveg += $"'{Adat.Törzsszám}', ";              //törzsszám
                szöveg += $"'{Adat.Dolgozónév}', ";             //dolgozónév
                szöveg += $"'{Adat.Kezdődátum:yyyy.MM.dd}', ";  //kezdődátum
                szöveg += $"'{Adat.Befejeződátum:yyyy.MM.dd}', "; //befejeződátum
                szöveg += $"{Adat.Kivettnap}, ";                       //kivettnap
                szöveg += $"'{Adat.Szabiok}', ";      //Szabiok
                szöveg += $"{Adat.Státus}, ";                             //Státus
                szöveg += $"'{Adat.Rögzítette}', "; //rögzítette
                szöveg += $"'{Adat.Rögzítésdátum}')";  //rögzítésdátum
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

        public void StátusÁllítás(string Telephely, int Év, int Státus, List<double> Sorszámok)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                List<string> SzövegGy = new List<string>();
                foreach (double Sorszám in Sorszámok)
                {
                    string szöveg = $"Update {táblanév} set státus={Státus} Where sorszám={Sorszám} AND státus<>3";
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

        public void Státus(string Telephely, int Év, List<double> Sorszámok)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                List<string> szövegGy = new List<string>();
                for (int i = 0; i < Sorszámok.Count; i++)
                {
                    string szöveg = $"UPDATE {táblanév} SET ";
                    szöveg += $"sorszám=0, Státus=0 ";
                    szöveg += $" WHERE sorszám={Sorszámok[i]}";
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

        public void Státus(string Telephely, int Év, List<string> Törzsszámok, List<DateTime> Kezdődátumok)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                List<string> szövegGy = new List<string>();
                for (int i = 0; i < Törzsszámok.Count; i++)
                {
                    string szöveg = $"UPDATE {táblanév} SET ";
                    szöveg += $"Státus=3 ";   //Státus
                    szöveg += $" WHERE törzsszám='{Törzsszámok[i]}' AND [Kezdődátum]=#{Kezdődátumok[i]:M-d-yy}# AND [státus]<>3";
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
    }
}

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
    public class Kezelő_Kidobó
    {
        readonly string jelszó = "lilaakác";
        string hely;
        readonly string táblanév = "kidobótábla";

        private void FájlBeállítás(string Telephely, DateTime Dátum)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Főkönyv\Kidobó\{Dátum.Year}\{Dátum:yyyyMMdd}Forte.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kidobóadattábla(hely.KönyvSzerk());
        }

        public List<Adat_Kidobó> Lista_Adat(string Telephely, DateTime Dátum, bool Törzsszám = false)
        {
            FájlBeállítás(Telephely, Dátum);
            string szöveg = $"SELECT * FROM {táblanév}  order by szolgálatiszám";
            List<Adat_Kidobó> Adatok = new List<Adat_Kidobó>();
            Adat_Kidobó Adat;

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
                                if (Törzsszám)

                                {
                                    Adat = new Adat_Kidobó(
                                    rekord["viszonylat"].ToStrTrim(),
                                    rekord["forgalmiszám"].ToStrTrim(),
                                    rekord["szolgálatiszám"].ToStrTrim(),
                                    rekord["jvez"].ToStrTrim(),
                                    rekord["kezdés"].ToÉrt_DaTeTime(),
                                    rekord["végzés"].ToÉrt_DaTeTime(),
                                    rekord["Kezdéshely"].ToStrTrim(),
                                    rekord["Végzéshely"].ToStrTrim(),
                                    rekord["Kód"].ToStrTrim(),
                                    rekord["Tárolásihely"].ToStrTrim(),
                                    rekord["Villamos"].ToStrTrim(),
                                    rekord["megjegyzés"].ToStrTrim(),
                                    rekord["szerelvénytípus"].ToStrTrim(),
                                    rekord["Törzsszám"].ToStrTrim()
                                    );
                                    Adatok.Add(Adat);
                                }
                                else
                                {
                                    Adat = new Adat_Kidobó(
                                         rekord["viszonylat"].ToStrTrim(),
                                         rekord["forgalmiszám"].ToStrTrim(),
                                         rekord["szolgálatiszám"].ToStrTrim(),
                                         rekord["jvez"].ToStrTrim(),
                                         rekord["kezdés"].ToÉrt_DaTeTime(),
                                         rekord["végzés"].ToÉrt_DaTeTime(),
                                         rekord["Kezdéshely"].ToStrTrim(),
                                         rekord["Végzéshely"].ToStrTrim(),
                                         rekord["Kód"].ToStrTrim(),
                                         rekord["Tárolásihely"].ToStrTrim(),
                                         rekord["Villamos"].ToStrTrim(),
                                         rekord["megjegyzés"].ToStrTrim(),
                                         rekord["szerelvénytípus"].ToStrTrim()
                                         );
                                    Adatok.Add(Adat);
                                }
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(string Telephely, DateTime Dátum, Adat_Kidobó Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);
                string szöveg = "UPDATE kidobótábla  SET ";
                szöveg += $"Kezdéshely='{Adat.Kezdéshely}', ";
                szöveg += $"Végzéshely='{Adat.Végzéshely}', ";
                szöveg += $"megjegyzés='{Adat.Megjegyzés}', ";
                szöveg += $" Kezdés='{Adat.Kezdés:HH:mm}', ";
                szöveg += $" végzés='{Adat.Végzés:HH:mm}' ";
                szöveg += $" WHERE szolgálatiszám='{Adat.Szolgálatiszám}'";
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

        public void Módosítás(string Telephely, DateTime Dátum, List<Adat_Kidobó> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Kidobó Adat in Adatok)
                {
                    string szöveg = "UPDATE kidobótábla  SET ";
                    szöveg += $"Kezdéshely='{Adat.Kezdéshely}', ";
                    szöveg += $"Végzéshely='{Adat.Végzéshely}', ";
                    szöveg += $"megjegyzés='{Adat.Megjegyzés}', ";
                    szöveg += $" Kezdés='{Adat.Kezdés:HH:mm}', ";
                    szöveg += $" végzés='{Adat.Végzés:HH:mm}' ";
                    szöveg += $" WHERE szolgálatiszám='{Adat.Szolgálatiszám}'";
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

        public void Rögzítés(string Telephely, DateTime Dátum, List<Adat_Kidobó> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Kidobó Adat in Adatok)
                {
                    string szöveg = "INSERT INTO Kidobótábla (viszonylat, forgalmiszám, szolgálatiszám, ";
                    szöveg += " jvez, kezdés, végzés, ";
                    szöveg += " Kezdéshely, Végzéshely, Kód, ";
                    szöveg += " Tárolásihely, Villamos, Megjegyzés, ";
                    szöveg += " szerelvénytípus, Törzsszám ) VALUES (";
                    szöveg += $"'{Adat.Viszonylat}', "; // viszonylat
                    szöveg += $"'{Adat.Forgalmiszám}', "; // forgalmiszám
                    szöveg += $"'{Adat.Szolgálatiszám}', "; // szolgálatiszám
                    szöveg += $"'{Adat.Jvez}', "; // jvez
                    szöveg += $"'{Adat.Kezdés:HH:mm:ss}', "; // kezdés
                    szöveg += $"'{Adat.Végzés:HH:mm:ss}', "; // végzés
                    szöveg += $"'{Adat.Kezdéshely}', "; // kezdéshely
                    szöveg += $"'{Adat.Végzéshely}', "; // végzéshely
                    szöveg += $"'{Adat.Kód}', "; // kód
                    szöveg += $"'{Adat.Tárolásihely}', "; // tárolásihely
                    szöveg += $"'{Adat.Villamos}', "; // villamos
                    szöveg += $"'{Adat.Megjegyzés}', "; // megjegyzés
                    szöveg += $"'{Adat.Szerelvénytípus}', "; // szerelvénytípus
                    szöveg += $"'{Adat.Törzsszám}') "; // Törzsszám
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

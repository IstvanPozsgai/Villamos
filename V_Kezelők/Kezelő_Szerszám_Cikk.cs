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
    public class Kezelő_Szerszám_Cikk
    {
        readonly string jelszó = "csavarhúzó";
        string hely;
        readonly string táblanév = "cikktörzs";

        private void FájlBeállítás(string Telephely, string Melyik)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\{Melyik}\Adatok\Szerszám.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Szerszám_nyilvántartás(hely.KönyvSzerk());
        }

        public List<Adat_Szerszám_Cikktörzs> Lista_Adatok(string Telephely, string Melyik)
        {
            string szöveg = $"SELECT * FROM {táblanév} ORDER BY azonosító";
            FájlBeállítás(Telephely, Melyik);
            Adat_Szerszám_Cikktörzs Adat;
            List<Adat_Szerszám_Cikktörzs> Adatok = new List<Adat_Szerszám_Cikktörzs>();

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
                                Adat = new Adat_Szerszám_Cikktörzs(
                                           rekord["Azonosító"].ToStrTrim(),
                                           rekord["megnevezés"].ToStrTrim(),
                                           rekord["méret"].ToStrTrim(),
                                           rekord["hely"].ToStrTrim(),
                                           rekord["leltáriszám"].ToStrTrim(),
                                           rekord["Beszerzésidátum"].ToÉrt_DaTeTime(),
                                           rekord["státus"].ToÉrt_Int(),
                                           rekord["költséghely"].ToStrTrim(),
                                           rekord["gyáriszám"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(string Telephely, string Melyik, Adat_Szerszám_Cikktörzs Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Melyik);
                string szöveg = $"UPDATE {táblanév}  SET ";
                szöveg += $"megnevezés='{Adat.Megnevezés}', ";
                szöveg += $"méret='{Adat.Méret}', ";
                szöveg += $"leltáriszám='{Adat.Leltáriszám}', ";
                szöveg += $"költséghely='{Adat.Költséghely}', ";
                szöveg += $"hely='{Adat.Hely}', ";
                szöveg += $"státus='{Adat.Státus}', ";
                szöveg += $"gyáriszám='{Adat.Gyáriszám}', ";
                szöveg += $" Beszerzésidátum='{Adat.Beszerzésidátum:yyyy.MM.dd}' ";
                szöveg += $" WHERE azonosító='{Adat.Azonosító}'";
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

        public void Rögzítés(string Telephely, string Melyik, Adat_Szerszám_Cikktörzs Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Melyik);
                string szöveg = $"INSERT INTO {táblanév}  (azonosító, megnevezés, méret, leltáriszám, Beszerzésidátum, státus, hely, költséghely, gyáriszám) VALUES (";
                szöveg += $"'{Adat.Azonosító}', ";
                szöveg += $"'{Adat.Megnevezés}', ";
                szöveg += $"'{Adat.Méret}', ";
                szöveg += $"'{Adat.Leltáriszám}', ";
                szöveg += $"'{Adat.Beszerzésidátum:yyyy.MM.dd}', ";
                szöveg += $"{Adat.Státus}, ";
                szöveg += $"'{Adat.Hely}', ";
                szöveg += $"'{Adat.Költséghely}', ";
                szöveg += $"'{Adat.Gyáriszám}') ";
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

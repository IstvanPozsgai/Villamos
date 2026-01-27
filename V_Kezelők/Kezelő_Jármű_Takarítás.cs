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
    public class Kezelő_Jármű_Takarítás
    {
        readonly Kezelő_Jármű_Takarítás_Napló KézNapló = new Kezelő_Jármű_Takarítás_Napló();
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Takarítás\Jármű_takarítás.mdb";
        readonly string jelszó = "seprűéslapát";
        readonly string táblanév = "takarítások";

        public Kezelő_Jármű_Takarítás()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Járműtakarító_Főmérnök_tábla(hely.KönyvSzerk());
        }

        public List<Adat_Jármű_Takarítás_Takarítások> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Jármű_Takarítás_Takarítások> Adatok = new List<Adat_Jármű_Takarítás_Takarítások>();
            Adat_Jármű_Takarítás_Takarítások Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Takarítások(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["takarítási_fajta"].ToStrTrim(),
                                        rekord["telephely"].ToStrTrim(),
                                        rekord["státus"].ToÉrt_Int()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Jármű_Takarítás_Takarítások Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév}  (azonosító, dátum, takarítási_fajta, telephely, státus ) VALUES (";
                szöveg += $"'{Adat.Azonosító}', ";         // azonosító
                szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', ";  // dátum
                szöveg += $"'{Adat.Takarítási_fajta}', ";  // takarítási_fajta
                szöveg += $"'{Adat.Telephely}', ";         // telephely
                szöveg += $" {Adat.Státus})";              // státus
                MyA.ABMódosítás(hely, jelszó, szöveg);
                KézNapló.Rögzítés(DateTime.Now.Year, Adat);
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

        public void Módosítás_Dátum(Adat_Jármű_Takarítás_Takarítások Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév}  SET ";
                szöveg += $"dátum ='{Adat.Dátum:yyyy.MM.dd}', ";
                szöveg += $"státus ={Adat.Státus} ";
                szöveg += $" WHERE [azonosító]='{Adat.Azonosító}'";
                szöveg += $" AND takarítási_fajta='{Adat.Takarítási_fajta}'";
                szöveg += $" AND Telephely='{Adat.Telephely}'";
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

        public void Módosítás(Adat_Jármű_Takarítás_Takarítások Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév}  SET ";
                szöveg += $"dátum ='{Adat.Dátum:yyyy.MM.dd}', ";
                szöveg += $"státus ={Adat.Státus} ";
                szöveg += $" WHERE [azonosító]='{Adat.Azonosító}'";
                szöveg += $" AND takarítási_fajta='{Adat.Takarítási_fajta}'";
                szöveg += $" AND Telephely='{Adat.Telephely}'";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                KézNapló.Rögzítés(DateTime.Now.Year, Adat);
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

﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Váltós_Kijelöltnapok
    {
        readonly string jelszó = "katalin";
        string hely;
        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Év}\munkaidőnaptár.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Nappalosmunkarendlétrehozás(hely.KönyvSzerk());
        }

        public List<Adat_Váltós_Kijelöltnapok> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            string szöveg = "SELECT * FROM kijelöltnapok ";
            List<Adat_Váltós_Kijelöltnapok> Adatok = new List<Adat_Váltós_Kijelöltnapok>();
            Adat_Váltós_Kijelöltnapok Adat;

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
                                Adat = new Adat_Váltós_Kijelöltnapok(
                                          rekord["Telephely"].ToStrTrim(),
                                          rekord["Csoport"].ToStrTrim(),
                                          rekord["Dátum"].ToÉrt_DaTeTime()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(int Év, Adat_Váltós_Kijelöltnapok Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = "INSERT INTO kijelöltnapok (dátum, csoport,  telephely ) VALUES ( ";
                szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', ";
                szöveg += $"'{Adat.Csoport}', ";
                szöveg += $"'{Adat.Telephely}') ";
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

        public void Rögzítés(int Év, List<Adat_Váltós_Kijelöltnapok> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Váltós_Kijelöltnapok Adat in Adatok)
                {
                    string szöveg = "INSERT INTO kijelöltnapok (dátum, csoport,  telephely ) VALUES ( ";
                    szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', ";
                    szöveg += $"'{Adat.Csoport}', ";
                    szöveg += $"'{Adat.Telephely}') ";
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

        public void Törlés(int Év, Adat_Váltós_Kijelöltnapok Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = $"DELETE FROM kijelöltnapok  WHERE csoport='{Adat.Csoport}'";
                szöveg += $" And Telephely='{Adat.Telephely}'";
                szöveg += $" And dátum=#{Adat.Dátum:M-d-yy}#";
                MyA.ABtörlés(hely, jelszó, szöveg);
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

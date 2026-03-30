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
    public class Kezelő_Dolgozó_Személyes
    {
        readonly string jelszó = "forgalmiutasítás";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Főmérnökség2.mdb";
        readonly string táblanév = "személyes";

        public Kezelő_Dolgozó_Személyes()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Személyes_AdatTábla(hely.KönyvSzerk());
        }

        public List<Adat_Dolgozó_Személyes> Lista_Adatok()
        {
            List<Adat_Dolgozó_Személyes> Adatok = new List<Adat_Dolgozó_Személyes>();
            Adat_Dolgozó_Személyes Adat;
            string szöveg = $"SELECT * FROM {táblanév}";
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
                                Adat = new Adat_Dolgozó_Személyes(
                                          rekord["Anyja"].ToStrTrim(),
                                          rekord["dolgozószám"].ToStrTrim(),
                                          rekord["Ideiglenescím"].ToStrTrim(),
                                          rekord["Lakcím"].ToStrTrim(),
                                          rekord["Leánykori"].ToStrTrim(),
                                          rekord["Születésihely"].ToStrTrim(),
                                          rekord["Születésiidő"].ToÉrt_DaTeTime(),
                                          rekord["Telefonszám1"].ToStrTrim(),
                                          rekord["Telefonszám2"].ToStrTrim(),
                                          rekord["Telefonszám3"].ToStrTrim()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(Adat_Dolgozó_Személyes Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $" Leánykori='{Adat.Leánykori}', ";
                szöveg += $" Anyja='{Adat.Anyja}', ";
                szöveg += $" Születésiidő='{Adat.Születésiidő:yyyy.MM.dd}', ";
                szöveg += $" Születésihely='{Adat.Születésihely}', ";
                szöveg += $" Lakcím='{Adat.Lakcím}', ";
                szöveg += $" Ideiglenescím='{Adat.Ideiglenescím}', ";
                szöveg += $" Telefonszám1='{Adat.Telefonszám1}', ";
                szöveg += $" Telefonszám2='{Adat.Telefonszám2}', ";
                szöveg += $" Telefonszám3='{Adat.Telefonszám3}' ";
                szöveg += $" WHERE dolgozószám='{Adat.Dolgozószám}'";
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

        public void Rögzítés(Adat_Dolgozó_Személyes Adat)
        {
            try
            {

                string szöveg = $"INSERT INTO {táblanév} (dolgozószám, leánykori, anyja, születésiidő, születésihely, lakcím, ideiglenescím, telefonszám1, telefonszám2, telefonszám3 )";
                szöveg += " VALUES ";
                szöveg += $"('{Adat.Dolgozószám}', ";
                szöveg += $"'{Adat.Leánykori}', ";
                szöveg += $"'{Adat.Anyja}', ";
                szöveg += $"'{Adat.Születésiidő:yyyy.MM.dd}', ";
                szöveg += $"'{Adat.Születésihely}', ";
                szöveg += $"'{Adat.Lakcím}', ";
                szöveg += $"'{Adat.Ideiglenescím}', ";
                szöveg += $"'{Adat.Telefonszám1}', ";
                szöveg += $"'{Adat.Telefonszám2}', ";
                szöveg += $"'{Adat.Telefonszám3}')";
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

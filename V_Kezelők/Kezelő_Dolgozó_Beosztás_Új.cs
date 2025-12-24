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
    public class Kezelő_Dolgozó_Beosztás_Új
    {
        readonly string jelszó = "kiskakas";
        string hely;
        readonly string táblanév = "Beosztás";

        private void FájlBeállítás(string Telephely, DateTime Dátum, bool Eszterga)
        {
            if (Eszterga)
                hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Beosztás\{Dátum.Year}\EsztBeosztás{Dátum:yyyyMM}.mdb";
            else
                hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Beosztás\{Dátum.Year}\Ebeosztás{Dátum:yyyyMM}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Dolgozói_Beosztás_Adatok_Új(hely.KönyvSzerk());
        }


        public List<Adat_Dolgozó_Beosztás_Új> Lista_Adatok(string Telephely, DateTime Dátum, bool Eszterga = false)
        {
            FájlBeállítás(Telephely, Dátum, Eszterga);
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Dolgozó_Beosztás_Új> Adatok = new List<Adat_Dolgozó_Beosztás_Új>();
            Adat_Dolgozó_Beosztás_Új Adat;

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

                                Adat = new Adat_Dolgozó_Beosztás_Új(
                                          rekord["dolgozószám"].ToStrTrim(),
                                          rekord["Nap"].ToÉrt_DaTeTime(),
                                          rekord["Beosztáskód"].ToStrTrim(),
                                          rekord["Ledolgozott"].ToÉrt_Int(),

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

        public void Rögzítés(string Telephely, DateTime Dátum, List<Adat_Dolgozó_Beosztás_Új> Adatok, bool Eszterga = false)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Eszterga);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Dolgozó_Beosztás_Új Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév} (Dolgozószám, Nap, Beosztáskód, Ledolgozott, " +
                                                        "Túlóra, Túlórakezd, Túlóravég, Csúszóra, " +
                                                        "CSúszórakezd, Csúszóravég, Megjegyzés, Túlóraok, " +
                                                        "Szabiok, kért, Csúszok, AFTóra, " +
                                                        "AFTok ) VALUES (";
                    szöveg += $"'{Adat.Dolgozószám}', ";   //    Dolgozószám,
                    szöveg += $"'{Adat.Nap}', ";   //    Nap,
                    szöveg += $"'{Adat.Beosztáskód}', ";   //    Beosztáskód,
                    szöveg += $"{Adat.Ledolgozott}, ";   //    Ledolgozott,
                    szöveg += $"{Adat.Túlóra}, ";   //    Túlóra,
                    szöveg += $"'{Adat.Túlórakezd}', ";   //    Túlórakezd,
                    szöveg += $"'{Adat.Túlóravég}', ";   //    Túlóravég,
                    szöveg += $"{Adat.Csúszóra}, ";   //    Csúszóra,
                    szöveg += $"'{Adat.CSúszórakezd}', ";   //    CSúszórakezd,
                    szöveg += $"'{Adat.Csúszóravég}', ";   //    Csúszóravég,
                    szöveg += $"'{Adat.Megjegyzés}', ";   //    MegjegyzésVáltozó,
                    szöveg += $"'{Adat.Túlóraok}', ";   //    Túlóraok,
                    szöveg += $"'{Adat.Szabiok}', ";   //    Szabiok,
                    szöveg += $"{Adat.Kért} , ";   //    kért,
                    szöveg += $"'{Adat.Csúszok}', ";   //    Csúszok,
                    szöveg += $"{Adat.AFTóra}, ";   //    AFTóra,
                    szöveg += $"'{Adat.AFTok}' ) ";   //    AFTok,
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

        public void MódosításCsúsz(string Telephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Adat, bool Eszterga = false)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Eszterga);
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"Csúszóra={Adat.Csúszóra}, ";// Csúszóra
                szöveg += $"CSúszórakezd='{Adat.CSúszórakezd}', ";// CSúszórakezd
                szöveg += $"Csúszóravég='{Adat.Csúszóravég}', ";// Csúszóravég
                szöveg += $"Csúszok='{Adat.Csúszok}' ";// Csúszok
                szöveg += $" WHERE Dolgozószám='{Adat.Dolgozószám}' AND nap=#{Adat.Nap:MM-dd-yyyy}#";
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

        public void MódosításAft(string Telephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Adat, bool Eszterga = false)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Eszterga);
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"AFTóra={Adat.AFTóra}, ";// AFTóra
                szöveg += $"AFTok='{Adat.AFTok}' ";// AFTok
                szöveg += $" WHERE Dolgozószám='{Adat.Dolgozószám}' AND nap=#{Adat.Nap:MM-dd-yyyy}#";

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

        public void MódosításTúl(string Telephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Adat, bool Eszterga = false)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Eszterga);
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"Túlóra={Adat.Túlóra}, ";// Túlóra
                szöveg += $"Túlórakezd='{Adat.Túlórakezd}', ";// Túlórakezd
                szöveg += $"Túlóravég='{Adat.Túlóravég}', ";// Túlóravég
                szöveg += $"Túlóraok='{Adat.Túlóraok.Trim()}' ";// Túlóraok
                szöveg += $" WHERE Dolgozószám='{Adat.Dolgozószám}' AND nap=#{Adat.Nap:MM-dd-yyyy}#";
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

        public void Törlés(string Telephely, DateTime Dátum, DateTime Dátumtól, DateTime Dátumig, bool Eszterga = false)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Eszterga);
                string szöveg = $"DELETE FROM {táblanév} WHERE  nap>=#{Dátumtól:yyyy-MM-dd}# AND nap<=#{Dátumig:yyyy-MM-dd}# ";
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

        public void Törlés(string Telephely, DateTime Dátum, DateTime Dátumtól, DateTime Dátumig, List<string> HrAzonosítók, bool Eszterga = false)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Eszterga);
                List<string> szövegGy = new List<string>();
                foreach (string Hr_Azonosító in HrAzonosítók)
                {
                    string szöveg = $"DELETE FROM {táblanév} ";
                    szöveg += $" WHERE nap>=#{Dátumtól:M-d-yy}# ";
                    szöveg += $" AND nap<=#{Dátumig:M-d-yy}# ";
                    szöveg += $" AND Dolgozószám='{Hr_Azonosító}'";
                    szövegGy.Add(szöveg);
                }
                MyA.ABtörlés(hely, jelszó, szövegGy);
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

        public void Módosítás(string Telephely, DateTime Dátum, List<Adat_Dolgozó_Beosztás_Új> Adatok, bool Eszterga = false)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Eszterga);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Dolgozó_Beosztás_Új Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév} SET ";
                    szöveg += $"Beosztáskód='{Adat.Beosztáskód}', ";// Beosztáskód
                    szöveg += $" Ledolgozott={Adat.Ledolgozott}, ";// Ledolgozott
                    szöveg += $"Túlóra={Adat.Túlóra}, ";// Túlóra
                    szöveg += $"Túlórakezd='{Adat.Túlórakezd}', ";// Túlórakezd
                    szöveg += $"Túlóravég='{Adat.Túlóravég}', ";// Túlóravég
                    szöveg += $"Csúszóra={Adat.Csúszóra}, ";// Csúszóra
                    szöveg += $"CSúszórakezd='{Adat.CSúszórakezd}', ";// CSúszórakezd
                    szöveg += $"Csúszóravég='{Adat.Csúszóravég}', ";// Csúszóravég
                    szöveg += $"Megjegyzés='{Adat.Megjegyzés}', ";// Megjegyzés
                    szöveg += $"Túlóraok='{Adat.Túlóraok}', ";// Túlóraok
                    szöveg += $"Szabiok='{Adat.Szabiok}', ";// Szabiok
                    szöveg += $"Kért={Adat.Kért}, ";// Kért
                    szöveg += $"Csúszok='{Adat.Csúszok}', ";// Csúszok
                    szöveg += $"AFTóra={Adat.AFTóra}, ";// AFTóra
                    szöveg += $"AFTok='{Adat.AFTok}' ";// AFTok
                    szöveg += $" WHERE Dolgozószám='{Adat.Dolgozószám}' AND nap=#{Adat.Nap:MM-dd-yyyy}#";
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

        public void MódosításMegj(string Telephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Adat, bool Eszterga = false)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Eszterga);
                string szöveg = "UPDATE beosztás SET ";
                szöveg += $"Megjegyzés='{Adat.Megjegyzés.Trim()}', ";// Megjegyzés
                szöveg += $"Kért={Adat.Kért} ";// Kért
                szöveg += $" WHERE Dolgozószám='{Adat.Dolgozószám}' AND nap=#{Adat.Nap:MM-dd-yyyy}#";
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

        public void MódosításSzabiOk(string Telephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Adat, bool Eszterga = false)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Eszterga);
                string szöveg = "UPDATE beosztás SET ";
                szöveg += $"Szabiok='{Adat.Szabiok}' ";// AFTóra
                szöveg += $" WHERE Dolgozószám='{Adat.Dolgozószám}' AND nap=#{Adat.Nap:MM-dd-yyyy}#";
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

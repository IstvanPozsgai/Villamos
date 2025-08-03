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

        public void Rögzítés(string Telephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Adat, bool Eszterga = false)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Eszterga);

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


        public void Módosítás(string Telephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Adat, bool Eszterga = false)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Eszterga);

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
                string szöveg = $"DELETE FROM beosztás WHERE  nap>=#{Dátumtól:yyyy-MM-dd}# AND nap<=#{Dátumig:yyyy-MM-dd}# ";
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



        //elkopó
        public List<Adat_Dolgozó_Beosztás_Új> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
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

        public Adat_Dolgozó_Beosztás_Új Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Dolgozó_Beosztás_Új Adat = null;

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
                            }
                        }
                    }
                }
            }
            return Adat;
        }

    }
}

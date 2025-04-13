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
    public class Kezelő_Főkönyv_ZSER
    {
        readonly string jelszó = "lilaakác";
        string hely;

        private void FájlBeállítás(string Telephely, DateTime Dátum, string Napszak)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\főkönyv\{Dátum.Year}\zser\zser{Dátum:yyyyMMdd}{Napszak}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Zseltáblaalap(hely.KönyvSzerk());
        }

        public List<Adat_Főkönyv_ZSER> Lista_Adatok(string Telephely, DateTime Dátum, string Napszak)
        {
            FájlBeállítás(Telephely, Dátum, Napszak);
            string szöveg = "SELECT * FROM zseltábla Order By viszonylat,forgalmiszám,tervindulás";
            List<Adat_Főkönyv_ZSER> Adatok = new List<Adat_Főkönyv_ZSER>();
            Adat_Főkönyv_ZSER Adat;

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
                                Adat = new Adat_Főkönyv_ZSER(
                                    rekord["viszonylat"].ToStrTrim(),
                                    rekord["forgalmiszám"].ToStrTrim(),
                                    rekord["tervindulás"].ToÉrt_DaTeTime(),
                                    rekord["tényindulás"].ToÉrt_DaTeTime(),
                                    rekord["tervérkezés"].ToÉrt_DaTeTime(),
                                    rekord["tényérkezés"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["szerelvénytípus"].ToStrTrim(),
                                    rekord["kocsikszáma"].ToÉrt_Long(),
                                    rekord["megjegyzés"].ToStrTrim(),
                                    rekord["kocsi1"].ToStrTrim(),
                                    rekord["kocsi2"].ToStrTrim(),
                                    rekord["kocsi3"].ToStrTrim(),
                                    rekord["kocsi4"].ToStrTrim(),
                                    rekord["kocsi5"].ToStrTrim(),
                                    rekord["kocsi6"].ToStrTrim(),
                                    rekord["ellenőrző"].ToStrTrim(),
                                    rekord["Státus"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, DateTime Dátum, string Napszak, List<Adat_Főkönyv_ZSER> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Napszak);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Főkönyv_ZSER Adat in Adatok)
                {
                    string szöveg = "INSERT INTO ZSELtábla (viszonylat, forgalmiszám, tervindulás, tényindulás, tervérkezés, tényérkezés, státus, ";
                    szöveg += " szerelvénytípus, kocsikszáma, megjegyzés, kocsi1, kocsi2, kocsi3, kocsi4, kocsi5, kocsi6, ellenőrző, napszak)  VALUES (";
                    szöveg += $"'{Adat.Viszonylat}', '{Adat.Forgalmiszám}', '{Adat.Tervindulás:yyyy.MM.dd HH:mm:ss}', '{Adat.Tényindulás:yyyy.MM.dd HH:mm:ss}', ";
                    szöveg += $"'{Adat.Tervérkezés:yyyy.MM.dd HH:mm:ss}', '{Adat.Tényérkezés:yyyy.MM.dd HH:mm:ss}', '{Adat.Státus}', ";
                    szöveg += $"'{Adat.Szerelvénytípus}', {Adat.Kocsikszáma}, '{Adat.Megjegyzés}', '{Adat.Kocsi1}', '{Adat.Kocsi2}', '{Adat.Kocsi3}', '{Adat.Kocsi4}', ";
                    szöveg += $"'{Adat.Kocsi5}', '{Adat.Kocsi6}', '{Adat.Ellenőrző}', '{Adat.Napszak}')";
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

        public void Rögzítés(string Telephely, DateTime Dátum, string Napszak, Adat_Főkönyv_ZSER Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Napszak);

                string szöveg = "INSERT INTO ZSELtábla (viszonylat, forgalmiszám, tervindulás, tényindulás, tervérkezés, tényérkezés, státus, ";
                szöveg += " szerelvénytípus, kocsikszáma, megjegyzés, kocsi1, kocsi2, kocsi3, kocsi4, kocsi5, kocsi6, ellenőrző, napszak)  VALUES (";
                szöveg += $"'{Adat.Viszonylat}', '{Adat.Forgalmiszám}', '{Adat.Tervindulás:yyyy.MM.dd HH:mm:ss}', '{Adat.Tényindulás:yyyy.MM.dd HH:mm:ss}', ";
                szöveg += $"'{Adat.Tervérkezés:yyyy.MM.dd HH:mm:ss}', '{Adat.Tényérkezés:yyyy.MM.dd HH:mm:ss}', '{Adat.Státus}', ";
                szöveg += $"'{Adat.Szerelvénytípus}', {Adat.Kocsikszáma}, '{Adat.Megjegyzés}', '{Adat.Kocsi1}', '{Adat.Kocsi2}', '{Adat.Kocsi3}', '{Adat.Kocsi4}', ";
                szöveg += $"'{Adat.Kocsi5}', '{Adat.Kocsi6}', '{Adat.Ellenőrző}', '{Adat.Napszak}')";

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

        public void Módosítás(string Telephely, DateTime Dátum, string Napszak, Adat_Főkönyv_ZSER Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Napszak);
                string szöveg = "UPDATE zseltábla  SET ";
                szöveg += $"tényindulás='{Adat.Tényindulás}', ";  // tényindulás
                szöveg += $"tervérkezés='{Adat.Tervérkezés}', "; // tervérkezés
                szöveg += $"tényérkezés='{Adat.Tényérkezés}', "; // tényérkezés
                szöveg += $"napszak='{Adat.Napszak}', "; // napszak
                szöveg += $"szerelvénytípus='{Adat.Szerelvénytípus}', "; // szerelvénytípus
                szöveg += $"kocsikszáma={Adat.Kocsikszáma}, "; // kocsikszáma
                szöveg += $"megjegyzés='{Adat.Megjegyzés}', "; // megjegyzés
                szöveg += $"kocsi1='{Adat.Kocsi1}', ";// kocsi1
                szöveg += $"kocsi2='{Adat.Kocsi2}', ";// kocsi2
                szöveg += $"kocsi3='{Adat.Kocsi3}', ";// kocsi3
                szöveg += $"kocsi4='{Adat.Kocsi4}', ";// kocsi4
                szöveg += $"kocsi5='{Adat.Kocsi5}', ";// kocsi5
                szöveg += $"kocsi6='{Adat.Kocsi6}', "; // kocsi6
                szöveg += $"ellenőrző='{Adat.Ellenőrző}', "; // ellenőrző
                szöveg += $"Státus='{Adat.Státus}'"; // Státus
                szöveg += $" WHERE viszonylat='{Adat.Viszonylat}' ";
                szöveg += $" And forgalmiszám='{Adat.Forgalmiszám}' ";
                szöveg += $"And tervindulás=#{Adat.Tervindulás:MM-dd-yyyy HH:mm:ss}#";
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

        public void Módosítás_Ellenőr(string Telephely, DateTime Dátum, string Napszak, List<Adat_Főkönyv_ZSER> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Napszak);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Főkönyv_ZSER Adat in Adatok)
                {
                    string szöveg = $"UPDATE zseltábla SET ellenőrző='{Adat.Ellenőrző}' ";
                    szöveg += $" WHERE viszonylat='{Adat.Viszonylat}' AND ";
                    szöveg += $" forgalmiszám='{Adat.Forgalmiszám}' AND ";
                    szöveg += $" tervindulás=#{Adat.Tervindulás:yyyy-MM-dd HH:mm:ss}# ";
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

        public void Módosítás_Napszak(string Telephely, DateTime Dátum, string Napszak, List<Adat_Főkönyv_ZSER> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Napszak);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Főkönyv_ZSER Adat in Adatok)
                {
                    string szöveg = $"UPDATE zseltábla SET napszak='{Adat.Napszak}' ";
                    szöveg += $" WHERE viszonylat='{Adat.Viszonylat}' AND ";
                    szöveg += $" forgalmiszám='{Adat.Forgalmiszám}' AND ";
                    szöveg += $" tervindulás=#{Adat.Tervindulás:yyyy-MM-dd HH:mm:ss}# ";
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

        public void Törlés(string Telephely, DateTime Dátum, string Napszak)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Napszak);
                MyA.ABtörlés(hely, jelszó, "DELETE * FROM zseltábla");

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

        public void Törlés(string Telephely, DateTime Dátum, string Napszak, Adat_Főkönyv_ZSER Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Napszak);
                string szöveg = $"DELETE FROM zseltábla  WHERE viszonylat='{Adat.Viszonylat}' ";
                szöveg += $" And forgalmiszám='{Adat.Forgalmiszám}' ";
                szöveg += $" And tervindulás=#{Adat.Tervindulás:MM-dd-yyyy HH:mm:ss}#";
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


        //Elkopó
        public List<Adat_Főkönyv_ZSER> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Főkönyv_ZSER> Adatok = new List<Adat_Főkönyv_ZSER>();
            Adat_Főkönyv_ZSER Adat;

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
                                Adat = new Adat_Főkönyv_ZSER(
                                    rekord["viszonylat"].ToStrTrim(),
                                    rekord["forgalmiszám"].ToStrTrim(),
                                    rekord["tervindulás"].ToÉrt_DaTeTime(),
                                    rekord["tényindulás"].ToÉrt_DaTeTime(),
                                    rekord["tervérkezés"].ToÉrt_DaTeTime(),
                                    rekord["tényérkezés"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["szerelvénytípus"].ToStrTrim(),
                                    rekord["kocsikszáma"].ToÉrt_Long(),
                                    rekord["megjegyzés"].ToStrTrim(),
                                    rekord["kocsi1"].ToStrTrim(),
                                    rekord["kocsi2"].ToStrTrim(),
                                    rekord["kocsi3"].ToStrTrim(),
                                    rekord["kocsi4"].ToStrTrim(),
                                    rekord["kocsi5"].ToStrTrim(),
                                    rekord["kocsi6"].ToStrTrim(),
                                    rekord["ellenőrző"].ToStrTrim(),
                                    rekord["Státus"].ToStrTrim()
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

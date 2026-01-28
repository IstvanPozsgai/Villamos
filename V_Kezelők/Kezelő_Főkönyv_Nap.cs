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
    public class Kezelő_Főkönyv_Nap
    {
        readonly string jelszó = "lilaakác";
        string hely = "";
        readonly string táblanév = "Adattábla";


        private void FájlBeállítás(string Telephely, DateTime Dátum, string Napszak, bool Létrejön = true)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\főkönyv\{Dátum.Year}\nap\{Dátum:yyyyMMdd}{Napszak}nap.mdb";
            if (!File.Exists(hely) && Létrejön) Adatbázis_Létrehozás.Főkönyvtáblaalap(hely.KönyvSzerk());
        }

        public List<Adat_Főkönyv_Nap> Lista_Adatok(string Telephely, DateTime Dátum, string Napszak, bool Eredeti = false)
        {
            FájlBeállítás(Telephely, Dátum, Napszak, false);
            string szöveg = $"SELECT * FROM {táblanév} ORDER BY azonosító";
            List<Adat_Főkönyv_Nap> Adatok = new List<Adat_Főkönyv_Nap>();
            Adat_Főkönyv_Nap Adat;
            if (!File.Exists(hely)) return Adatok;
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
                                if (Eredeti)
                                {
                                    Adat = new Adat_Főkönyv_Nap(
                                               rekord["státus"].ToÉrt_Long(),
                                               rekord["hibaleírása"].ToStrTrim(),
                                               rekord["típus"].ToStrTrim(),
                                               rekord["azonosító"].ToStrTrim(),
                                               rekord["szerelvény"].ToÉrt_Long(),
                                               rekord["viszonylat"].ToStrTrim(),
                                               rekord["forgalmiszám"].ToStrTrim(),
                                               rekord["kocsikszáma"].ToÉrt_Long(),
                                               rekord["tervindulás"].ToÉrt_DaTeTime(),
                                               rekord["tényindulás"].ToÉrt_DaTeTime(),
                                               rekord["tervérkezés"].ToÉrt_DaTeTime(),
                                               rekord["tényérkezés"].ToÉrt_DaTeTime(),
                                               rekord["miótaáll"].ToÉrt_DaTeTime(),
                                               rekord["napszak"].ToString(),
                                               rekord["megjegyzés"].ToStrTrim()
                                               );
                                    Adatok.Add(Adat);
                                }
                                else
                                {
                                    Adat = new Adat_Főkönyv_Nap(
                                              rekord["státus"].ToÉrt_Long(),
                                              rekord["hibaleírása"].ToStrTrim(),
                                              rekord["típus"].ToStrTrim(),
                                              rekord["azonosító"].ToStrTrim(),
                                              rekord["szerelvény"].ToÉrt_Long(),
                                              rekord["viszonylat"].ToStrTrim(),
                                              rekord["forgalmiszám"].ToStrTrim(),
                                              rekord["kocsikszáma"].ToÉrt_Long(),
                                              rekord["tervindulás"].ToÉrt_DaTeTime(),
                                              rekord["tényindulás"].ToÉrt_DaTeTime(),
                                              rekord["tervérkezés"].ToÉrt_DaTeTime(),
                                              rekord["tényérkezés"].ToÉrt_DaTeTime(),
                                              rekord["miótaáll"].ToÉrt_DaTeTime(),
                                              rekord["napszak"].ToString(),
                                              rekord["megjegyzés"].ToStrTrim(),
                                              Telephely
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

        public void Rögzítés(string Telephely, DateTime Dátum, string Napszak, List<Adat_Főkönyv_Nap> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Napszak);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Főkönyv_Nap Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév}  (Státus, hibaleírása, típus, azonosító, szerelvény, ";
                    szöveg += "viszonylat, forgalmiszám, kocsikszáma, tervindulás, tényindulás, ";
                    szöveg += "tervérkezés, tényérkezés, miótaáll, napszak, megjegyzés ) VALUES (";
                    szöveg += $"{Adat.Státus},";              //  Státus
                    szöveg += $" '{Adat.Hibaleírása}',";        //  hibaleírása
                    szöveg += $" '{Adat.Típus}',";            //  típus
                    szöveg += $" '{Adat.Azonosító}',";        //  azonosító
                    szöveg += $" {Adat.Szerelvény}, ";        //  szerelvény
                    szöveg += $"'{Adat.Viszonylat}',";        //  viszonylat
                    szöveg += $" '{Adat.Forgalmiszám}',";     //  forgalmiszám
                    szöveg += $" {Adat.Kocsikszáma},";        //  kocsikszáma
                    szöveg += $" '{Adat.Tervindulás}',";      //  tervindulás
                    szöveg += $" '{Adat.Tényindulás}', ";     //  tényindulás
                    szöveg += $" '{Adat.Tervérkezés}', ";     //  tervérkezés
                    szöveg += $" '{Adat.Tényérkezés}', ";     //  tényérkezés
                    szöveg += $" '{Adat.Miótaáll}', ";        //  miótaáll
                    szöveg += $" '{Adat.Napszak}', ";         //  napszak
                    szöveg += $" '{Adat.Megjegyzés}')";       //  megjegyzés
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

        public void Rögzítés(string Telephely, DateTime Dátum, string Napszak, Adat_Főkönyv_Nap Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Napszak);

                string szöveg = $"INSERT INTO {táblanév}  (Státus, hibaleírása, típus, azonosító, szerelvény, ";
                szöveg += "viszonylat, forgalmiszám, kocsikszáma, tervindulás, tényindulás, ";
                szöveg += "tervérkezés, tényérkezés, miótaáll, napszak, megjegyzés ) VALUES (";
                szöveg += $"{Adat.Státus},";              //  Státus
                szöveg += $" '{Adat.Hibaleírása}',";        //  hibaleírása
                szöveg += $" '{Adat.Típus}',";            //  típus
                szöveg += $" '{Adat.Azonosító}',";        //  azonosító
                szöveg += $" {Adat.Szerelvény}, ";        //  szerelvény
                szöveg += $"'{Adat.Viszonylat}',";        //  viszonylat
                szöveg += $" '{Adat.Forgalmiszám}',";     //  forgalmiszám
                szöveg += $" {Adat.Kocsikszáma},";        //  kocsikszáma
                szöveg += $" '{Adat.Tervindulás}',";      //  tervindulás
                szöveg += $" '{Adat.Tényindulás}', ";     //  tényindulás
                szöveg += $" '{Adat.Tervérkezés}', ";     //  tervérkezés
                szöveg += $" '{Adat.Tényérkezés}', ";     //  tényérkezés
                szöveg += $" '{Adat.Miótaáll}', ";        //  miótaáll
                szöveg += $" '{Adat.Napszak}', ";         //  napszak
                szöveg += $" '{Adat.Megjegyzés}')";       //  megjegyzés

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

        public void Módosítás(string Telephely, DateTime Dátum, string Napszak, List<string> Azonosítók)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Napszak);
                List<string> SzövegGy = new List<string>();
                foreach (string Azonosító in Azonosítók)
                {
                    string szöveg = $"UPDATE {táblanév} SET viszonylat='-', forgalmiszám='-',  ";
                    szöveg += "tervindulás='1900.01.01 00:00:00', ";
                    szöveg += "tényindulás='1900.01.01 00:00:00', ";
                    szöveg += "tervérkezés='1900.01.01 00:00:00', ";
                    szöveg += "tényérkezés='1900.01.01 00:00:00', ";
                    szöveg += "napszak='_', ";
                    szöveg += "megjegyzés='_' ";
                    szöveg += $" WHERE azonosító='{Azonosító}'";
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

        public void Módosítás(string Telephely, DateTime Dátum, string Napszak, Adat_Főkönyv_Nap Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Napszak);
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $" Státus={Adat.Státus}, ";
                szöveg += $" hibaleírása='{Adat.Hibaleírása}', ";
                szöveg += $" típus='{Adat.Típus}', ";
                szöveg += $" szerelvény={Adat.Szerelvény}, ";
                szöveg += $" viszonylat='{Adat.Viszonylat}', ";
                szöveg += $" forgalmiszám='{Adat.Forgalmiszám}', ";
                szöveg += $" kocsikszáma={Adat.Kocsikszáma}, ";
                szöveg += $" tervindulás='{Adat.Tervindulás}', ";
                szöveg += $" tényindulás='{Adat.Tényindulás}', ";
                szöveg += $" tervérkezés='{Adat.Tervérkezés}', ";
                szöveg += $" tényérkezés='{Adat.Tényérkezés}', ";
                szöveg += $" miótaáll='{Adat.Miótaáll}', ";
                szöveg += $" napszak='{Adat.Napszak}', ";
                szöveg += $" megjegyzés='{Adat.Megjegyzés}' ";
                szöveg += $" WHERE azonosító='{Adat.Azonosító}' ";
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

        public void Módosítás_Napi(string Telephely, DateTime Dátum, string Napszak, Adat_Főkönyv_Nap Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Napszak);
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"viszonylat='{Adat.Viszonylat}', ";
                szöveg += $"forgalmiszám='{Adat.Forgalmiszám}', ";
                szöveg += $"kocsikszáma={Adat.Kocsikszáma}, ";
                szöveg += $"tervindulás='{Adat.Tervindulás}', ";
                szöveg += $"tényindulás='{Adat.Tényindulás}', ";
                szöveg += $"tervérkezés='{Adat.Tervérkezés}', ";
                szöveg += $"tényérkezés='{Adat.Tényérkezés}', ";
                szöveg += $"napszak='{Adat.Napszak}', ";
                szöveg += $"megjegyzés='{Adat.Megjegyzés}' ";
                szöveg += $"WHERE azonosító='{Adat.Azonosító}'";
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

        public void Módosítás_Áttölt(string Telephely, DateTime Dátum, string Napszak, List<Adat_Főkönyv_Nap> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum, Napszak);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Főkönyv_Nap Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév} SET ";
                    szöveg += $"viszonylat='{Adat.Viszonylat}', ";
                    szöveg += $"forgalmiszám='{Adat.Forgalmiszám}', ";
                    szöveg += $"kocsikszáma={Adat.Kocsikszáma}, ";
                    szöveg += $"tervindulás='{Adat.Tervindulás}', ";
                    szöveg += $"tényindulás='{Adat.Tényindulás}', ";
                    szöveg += $"tervérkezés='{Adat.Tervérkezés}', ";
                    szöveg += $"tényérkezés='{Adat.Tényérkezés}' ";
                    szöveg += $"WHERE azonosító='{Adat.Azonosító}'";
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
                MyA.ABtörlés(hely, jelszó, $"DELETE * FROM {táblanév}");
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

        public void Törlés(string Telephely, DateTime Dátum, string Napszak, string Azonosító)
        {

            try
            {
                FájlBeállítás(Telephely, Dátum, Napszak);
                string szöveg = $"DELETE FROM {táblanév} where azonosító='{Azonosító}'";
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

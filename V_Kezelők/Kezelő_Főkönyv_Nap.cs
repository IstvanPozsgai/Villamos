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
    public class Kezelő_Főkönyv_Nap
    {
        readonly string jelszó = "lilaakác";
        string hely = "";

        private void FájlBeállítás(string Telephely, DateTime Dátum, string Napszak, bool Létrejön = true)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\főkönyv\{Dátum.Year}\nap\{Dátum:yyyyMMdd}{Napszak}nap.mdb";
            if (!File.Exists(hely) && Létrejön) Adatbázis_Létrehozás.Főkönyvtáblaalap(hely.KönyvSzerk());
        }

        public List<Adat_Főkönyv_Nap> Lista_Adatok(string Telephely, DateTime Dátum, string Napszak)
        {
            FájlBeállítás(Telephely, Dátum, Napszak, false);
            string szöveg = "SELECT * FROM Adattábla ORDER BY azonosító";
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
                    string szöveg = "INSERT INTO Adattábla  (Státus, hibaleírása, típus, azonosító, szerelvény, ";
                    szöveg += "viszonylat, forgalmiszám, kocsikszáma, tervindulás, tényindulás, ";
                    szöveg += "tervérkezés, tényérkezés, miótaáll, napszak, megjegyzés ) VALUES (";
                    szöveg += $"{Adat.Státus},";              //  Státus
                    szöveg += $" '{Adat.Azonosító}',";        //  hibaleírása
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


        //Elkopó
        public List<Adat_Főkönyv_Nap> Lista_adatok(string hely, string jelszó, string szöveg)
        {

            List<Adat_Főkönyv_Nap> Adatok = new List<Adat_Főkönyv_Nap>();
            Adat_Főkönyv_Nap Adat;

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
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Főkönyv_Nap Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Főkönyv_Nap Adat = null;

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
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["megjegyzés"].ToStrTrim()
                                    );

                            }
                        }
                    }
                }
            }
            return Adat;
        }

        public List<string> Lista_típus(string hely, string jelszó, string szöveg)
        {
            List<string> Adatok = new List<string>();


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
                                Adatok.Add(rekord["típus"].ToStrTrim());
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }
}

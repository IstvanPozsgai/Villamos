using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos_Kezelők
{
    public class Kezelő_Akkumulátor
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Akkumulátor\akku.mdb";
        readonly string jelszó = "kasosmiklós";
        public List<Adat_Akkumulátor> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Akkumulátor> Adatok = new List<Adat_Akkumulátor>();
            Adat_Akkumulátor Adat;

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
                                Adat = new Adat_Akkumulátor(
                                        rekord["Beépítve"].ToStrTrim(),
                                        rekord["Fajta"].ToStrTrim(),
                                        rekord["Gyártó"].ToStrTrim(),
                                        rekord["Gyáriszám"].ToStrTrim(),
                                        rekord["Típus"].ToStrTrim(),
                                        rekord["Garancia"].ToÉrt_DaTeTime(),
                                        rekord["Gyártásiidő"].ToÉrt_DaTeTime(),
                                        rekord["Státus"].ToÉrt_Int(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["Módosításdátuma"].ToÉrt_DaTeTime(),
                                        rekord["Kapacitás"].ToÉrt_Int(),
                                        rekord["Telephely"].ToStrTrim()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Akkumulátor> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM akkutábla";
            List<Adat_Akkumulátor> Adatok = new List<Adat_Akkumulátor>();
            Adat_Akkumulátor Adat;

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
                                Adat = new Adat_Akkumulátor(
                                        rekord["Beépítve"].ToStrTrim(),
                                        rekord["Fajta"].ToStrTrim(),
                                        rekord["Gyártó"].ToStrTrim(),
                                        rekord["Gyáriszám"].ToStrTrim(),
                                        rekord["Típus"].ToStrTrim(),
                                        rekord["Garancia"].ToÉrt_DaTeTime(),
                                        rekord["Gyártásiidő"].ToÉrt_DaTeTime(),
                                        rekord["Státus"].ToÉrt_Int(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["Módosításdátuma"].ToÉrt_DaTeTime(),
                                        rekord["Kapacitás"].ToÉrt_Int(),
                                        rekord["Telephely"].ToStrTrim()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Akkumulátor Adat)
        {
            try
            {
                string szöveg = "INSERT INTO akkutábla ";
                szöveg += "(beépítve, fajta, gyártó, Gyáriszám, típus, garancia, gyártásiidő, státus, Megjegyzés, Módosításdátuma, kapacitás, Telephely)";
                szöveg += " VALUES (";
                szöveg += $"'{Adat.Beépítve}', "; //beépítve       ,
                szöveg += $"'{Adat.Fajta}', "; //fajta,
                szöveg += $"'{Adat.Gyártó}', "; //gyártó,
                szöveg += $"'{Adat.Gyáriszám}', "; //Gyáriszám,
                szöveg += $"'{Adat.Típus}', "; //típus,
                szöveg += $"'{Adat.Garancia:yyyy.MM.dd}', "; //garancia,
                szöveg += $"'{Adat.Gyártásiidő:yyyy.MM.dd}', "; //gyártásiidő,
                szöveg += $"{Adat.Státus}, "; //státus,
                szöveg += $"'{Adat.Megjegyzés}', "; //Megjegyzés,
                szöveg += $"'{Adat.Módosításdátuma}', "; //Módosításdátuma,
                szöveg += $"{Adat.Kapacitás}, "; //kapacitás,
                szöveg += $"'{Adat.Telephely}' )"; //Telephely
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

        public void Módosítás(Adat_Akkumulátor Adat)
        {
            try
            {
                string szöveg = " UPDATE akkutábla SET ";
                szöveg += $" fajta='{Adat.Fajta}', ";
                szöveg += $" gyártó='{Adat.Gyártó}', ";
                szöveg += $" típus='{Adat.Típus}', ";
                szöveg += $" garancia='{Adat.Garancia:yyyy.MM.dd}', ";
                szöveg += $" gyártásiidő='{Adat.Gyártásiidő:yyyy.MM.dd}', ";
                szöveg += $" Megjegyzés='{Adat.Megjegyzés}', ";
                szöveg += $" Módosításdátuma='{Adat.Módosításdátuma}', ";
                szöveg += $" kapacitás={Adat.Kapacitás} ";
                szöveg += $" WHERE Gyáriszám='{Adat.Gyáriszám}' ";
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

        public void Módosítás_Státus(Adat_Akkumulátor Adat)
        {
            try
            {
                string szöveg = " UPDATE akkutábla SET ";
                szöveg += $" beépítve='{Adat.Beépítve}', ";
                szöveg += $" státus={Adat.Státus}, ";
                szöveg += $" Módosításdátuma='{Adat.Módosításdátuma:yyyy.MM.dd}' ";
                szöveg += $" WHERE Gyáriszám='{Adat.Gyáriszám}' ";
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
        public void Módosítás(string Telep, string gyáriszám)
        {
            try
            {
                string szöveg = $"UPDATE akkutábla SET  telephely='{Telep.Trim()}' WHERE Gyáriszám='{gyáriszám.Trim()}' ";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString() + $"\n{hely},\nTelephely:{Telep},\nGyáriszám:{gyáriszám}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

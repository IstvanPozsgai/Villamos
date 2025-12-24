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
    public class Kezelő_TW6000_Előterv
    {
        readonly string jelszó = "czapmiklós";

        private void FájlBeállítás(string hely)
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.TW6000tábla(hely.KönyvSzerk());

        }

        public void Rögzítés(string hely, List<Adat_TW6000_Alap> Adatok)
        {
            try
            {
                FájlBeállítás(hely);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_TW6000_Alap Adat in Adatok)
                {
                    string szöveg = "INSERT INTO alap (azonosító, start, ciklusrend, megállítás, kötöttstart, vizsgsorszám, vizsgnév, vizsgdátum) VALUES (";
                    szöveg += $"'{Adat.Azonosító}', ";
                    szöveg += $"'{Adat.Start:yyyy.MM.dd}', ";
                    szöveg += $"'{Adat.Ciklusrend}', ";
                    szöveg += $"{Adat.Megállítás}, ";
                    szöveg += $"{Adat.Kötöttstart}, ";
                    szöveg += $"{Adat.Vizsgsorszám}, ";
                    szöveg += $"'{Adat.Vizsgnév}', ";
                    szöveg += $"'{Adat.Vizsgdátum:yyyy.MM.dd}') ";
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

        public List<Adat_TW6000_Alap> Lista_Adatok(string hely)
        {
            FájlBeállítás(hely);
            List<Adat_TW6000_Alap> Adatok = new List<Adat_TW6000_Alap>();
            Adat_TW6000_Alap Adat;
            string szöveg = $"SELECT * FROM alap";

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
                                Adat = new Adat_TW6000_Alap(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Ciklusrend"].ToStrTrim(),
                                        rekord["Kötöttstart"].ToÉrt_Bool(),
                                        rekord["Megállítás"].ToÉrt_Bool(),
                                        rekord["Start"].ToÉrt_DaTeTime(),
                                        rekord["Vizsgdátum"].ToÉrt_DaTeTime(),
                                        rekord["Vizsgnév"].ToStrTrim(),
                                        rekord["Vizsgsorszám"].ToÉrt_Int());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_TW6000_Ütemezés> Lista_AdatokÜtem(string hely)
        {
            string szöveg = "SELECT * FROM ütemezés";
            List<Adat_TW6000_Ütemezés> Adatok = new List<Adat_TW6000_Ütemezés>();
            Adat_TW6000_Ütemezés Adat;

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
                                Adat = new Adat_TW6000_Ütemezés(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["Ciklusrend"].ToStrTrim(),
                                        rekord["Elkészült"].ToÉrt_Bool(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["státus"].ToÉrt_Long(),
                                        rekord["velkészülés"].ToÉrt_DaTeTime(),
                                        rekord["vesedékesség"].ToÉrt_DaTeTime(),
                                        rekord["vizsgfoka"].ToStrTrim(),
                                        rekord["vsorszám"].ToÉrt_Long(),
                                        rekord["vütemezés"].ToÉrt_DaTeTime(),
                                        rekord["Vvégezte"].ToStrTrim()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, List<Adat_TW6000_Ütemezés> Adatok)
        {
            try
            {
                FájlBeállítás(hely);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_TW6000_Ütemezés Adat in Adatok)
                {
                    string szöveg = "INSERT INTO ütemezés (azonosító, ciklusrend, elkészült, megjegyzés, ";
                    szöveg += " státus, velkészülés, vesedékesség, vizsgfoka, ";
                    szöveg += " vsorszám, vütemezés, vvégezte) VALUES (";
                    szöveg += $"'{Adat.Azonosító}', ";
                    szöveg += $"'{Adat.Ciklusrend}', ";
                    szöveg += $"{Adat.Elkészült},";
                    szöveg += $" '{Adat.Megjegyzés}',";
                    szöveg += $" {Adat.Státus},";
                    szöveg += $" '{Adat.Velkészülés:yyyy.MM.dd}', ";
                    szöveg += $"'{Adat.Vesedékesség:yyyy.MM.dd}', ";
                    szöveg += $"'{Adat.Vizsgfoka}', ";
                    szöveg += $"{Adat.Vsorszám}, ";
                    szöveg += $"'{Adat.Vütemezés:yyyy.MM.dd}', ";
                    szöveg += $"'{Adat.Vvégezte}' )";
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

    }
}

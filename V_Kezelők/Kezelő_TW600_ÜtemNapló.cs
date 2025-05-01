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
    public class Kezelő_TW600_ÜtemNapló
    {
        string hely;
        readonly string jelszó = "czapmiklós";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\főmérnökség\napló\naplóTW6000Ütem_{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.TW6000ütemnapló(hely.KönyvSzerk());
        }

        public List<Adat_TW6000_ÜtemNapló> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            string szöveg = $"SELECT * FROM ütemezésnapló";
            List<Adat_TW6000_ÜtemNapló> Adatok = new List<Adat_TW6000_ÜtemNapló>();
            Adat_TW6000_ÜtemNapló Adat;

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
                                Adat = new Adat_TW6000_ÜtemNapló(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Ciklusrend"].ToStrTrim(),
                                        rekord["Elkészült"].ToÉrt_Bool(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["Rögzítésideje"].ToÉrt_DaTeTime(),
                                        rekord["Rögzítő"].ToStrTrim(),
                                        rekord["Státus"].ToÉrt_Long(),
                                        rekord["Velkészülés"].ToÉrt_DaTeTime(),
                                        rekord["Vesedékesség"].ToÉrt_DaTeTime(),
                                        rekord["Vizsgfoka"].ToStrTrim(),
                                        rekord["Vsorszám"].ToÉrt_Long(),
                                        rekord["Vütemezés"].ToÉrt_DaTeTime(),
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

        public void Rögzítés(int Év, Adat_TW6000_ÜtemNapló Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = "INSERT INTO ütemezésnapló (azonosító, ciklusrend, vizsgfoka, vsorszám, megjegyzés, vesedékesség, vütemezés, vvégezte, ";
                szöveg += "  velkészülés, státus, elkészült, rögzítő, rögzítésideje) VALUES (";
                szöveg += $"'{Adat.Azonosító}', ";
                szöveg += $"'{Adat.Ciklusrend}', ";
                szöveg += $"'{Adat.Vizsgfoka}', ";
                szöveg += $"{Adat.Vsorszám}, ";
                szöveg += $"'{Adat.Megjegyzés}', ";
                szöveg += $"'{Adat.Vesedékesség}', ";
                szöveg += $"'{Adat.Vütemezés}', ";
                szöveg += $"'{Adat.Vvégezte}', ";
                szöveg += $"'{Adat.Velkészülés}', ";
                szöveg += $"{Adat.Státus}, ";
                szöveg += $"{Adat.Elkészült}, ";
                szöveg += $"'{Adat.Rögzítő}', ";
                szöveg += $"'{Adat.Rögzítésideje}') ";
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

        public void Rögzítés(int Év, List<Adat_TW6000_Ütemezés> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_TW6000_Ütemezés Adat in Adatok)
                {
                    string szöveg = "INSERT INTO ütemezésnapló (azonosító, ciklusrend, elkészült, megjegyzés, ";
                    szöveg += " státus, velkészülés, vesedékesség, vizsgfoka, ";
                    szöveg += " vsorszám, vütemezés, vvégezte,Rögzítésideje, Rögzítő ) VALUES (";
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
                    szöveg += $"'{Adat.Vvégezte}',";
                    szöveg += $"'{DateTime.Now}', ";
                    szöveg += $"'{Program.PostásNév}')";
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

        public void Rögzítés(int Év, Adat_TW6000_Ütemezés Adat)
        {
            try
            {
                FájlBeállítás(Év);

                string szöveg = "INSERT INTO ütemezésnapló (azonosító, ciklusrend, elkészült, megjegyzés, ";
                szöveg += " státus, velkészülés, vesedékesség, vizsgfoka, ";
                szöveg += " vsorszám, vütemezés, vvégezte,Rögzítésideje, Rögzítő ) VALUES (";
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
                szöveg += $"'{Adat.Vvégezte}',";
                szöveg += $"'{DateTime.Now}', ";
                szöveg += $"'{Program.PostásNév}')";

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

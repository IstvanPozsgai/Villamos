using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_TW600_AlapNapló
    {
        readonly string jelszó = "czapmiklós";
        string hely;

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\napló\naplóTW6000_{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.TW6000táblanapló(hely.KönyvSzerk());
        }


        public void Rögzítés(int Év, Adat_TW6000_Alap Adat, string oka)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = "INSERT INTO alapnapló (azonosító, start, ciklusrend, megállítás, kötöttstart, vizsgsorszám, vizsgnév, vizsgdátum, oka, rögzítő, rögzítésiidő) VALUES (";
                szöveg += $"'{Pályaszám.Text.Trim()}', ";
                szöveg += $"'{StartDátum.Value:yyyy.MM.dd}', ";
                szöveg += $"'{Ciklusrend.Text.Trim()}', ";
                if (Megállítás.Checked)
                    szöveg += "true, ";

                else
                    szöveg += "false, ";

                if (KötöttStart.Checked)
                    szöveg += "true, ";

                else
                    szöveg += "false, ";
                szöveg += $"{Sorszámvizsg}, ";
                szöveg += $"'{Vizsgsorszám.Text.Trim()}', ";
                szöveg += $"'{Vizsgdátum.Value:yyyy.MM.dd}', ";
                szöveg += $"'{Oka.Text.Trim()}', ";
                szöveg += $"'{Program.PostásTelephely.Trim()}', ";
                szöveg += $"'{DateTime.Now}') ";

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

        //elkopó
        public List<Adat_TW6000_AlapNapló> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_TW6000_AlapNapló> Adatok = new List<Adat_TW6000_AlapNapló>();
            Adat_TW6000_AlapNapló Adat;

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
                                Adat = new Adat_TW6000_AlapNapló(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Ciklusrend"].ToStrTrim(),
                                        rekord["Kötöttstart"].ToÉrt_Bool(),
                                        rekord["Megállítás"].ToÉrt_Bool(),
                                        rekord["Oka"].ToStrTrim(),
                                        rekord["Rögzítésiidő"].ToÉrt_DaTeTime(),
                                        rekord["Rögzítő"].ToStrTrim(),
                                        rekord["Start"].ToÉrt_DaTeTime(),
                                        rekord["Vizsgdátum"].ToÉrt_DaTeTime(),
                                        rekord["Vizsgnév"].ToStrTrim(),
                                        rekord["Vizsgsorszám"].ToÉrt_Int()
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

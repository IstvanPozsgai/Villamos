using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Főkönyv_Zser_Km
    {
        readonly string jelszó = "pozsgaii";
        string hely;
        readonly string táblanév = "tábla";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Év}\Napi_km_Zser_{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.ZSER_km(hely.KönyvSzerk());
        }

        public List<Adat_Főkönyv_Zser_Km> Lista_adatok(int Év)
        {
            FájlBeállítás(Év);
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Főkönyv_Zser_Km> Adatok = new List<Adat_Főkönyv_Zser_Km>();
            Adat_Főkönyv_Zser_Km Adat;

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
                                Adat = new Adat_Főkönyv_Zser_Km(
                                    rekord["azonosító"].ToStrTrim(),
                                    rekord["Dátum"].ToÉrt_DaTeTime(),
                                    rekord["Napikm"].ToÉrt_Int(),
                                    rekord["telephely"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public int FutottKm(string Azonosító, DateTime Dátum)
        {
            int Válasz = 0;
            try
            {
                List<Adat_Főkönyv_Zser_Km> AdatokKM = Lista_adatok(Dátum.Year);
                if (AdatokKM != null && AdatokKM.Count > 0) Válasz = AdatokKM.Where(a => a.Azonosító == Azonosító && a.Dátum > Dátum).Sum(a => a.Napikm);
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
            return Válasz;
        }

        public void Törlés(string Telephely, DateTime Dátum)
        {
            try
            {
                FájlBeállítás(Dátum.Year);
                string szöveg = $"DELETE FROM {táblanév} WHERE telephely='{Telephely}' AND dátum=#{Dátum:MM-dd-yyyy}#";
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

        public void Rögzítés(List<Adat_Főkönyv_Zser_Km> Adatok, int Év)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Főkönyv_Zser_Km Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév} (azonosító, dátum, napikm, telephely ) VALUES (";
                    szöveg += $"'{Adat.Azonosító}', '{Adat.Dátum:yyyy.MM.dd}', {Adat.Napikm}, '{Adat.Telephely}')";
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

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
    public class Kezelő_Nap_Hiba
    {
        string hely;
        readonly string jelszó = "pozsgaii";
        readonly string táblanév = "hiba";

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\villamos\Új_napihiba.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Napihibatábla(hely.KönyvSzerk());
        }



        public List<Adat_Nap_Hiba> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Nap_Hiba> Adatok = new List<Adat_Nap_Hiba>();
            Adat_Nap_Hiba Adat;
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
                                Adat = new Adat_Nap_Hiba(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["mikori"].ToÉrt_DaTeTime(),
                                    rekord["beálló"].ToStrTrim(),
                                    rekord["üzemképtelen"].ToStrTrim(),
                                    rekord["üzemképeshiba"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["státus"].ToÉrt_Long()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Törlés(string Telephely)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"DELETE FROM {táblanév} ";
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

        public void Rögzítés(string Telephely, List<Adat_Nap_Hiba> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Nap_Hiba Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév} (azonosító, mikori, beálló, üzemképtelen, üzemképeshiba, típus, státus ) VALUES (";
                    szöveg += $"'{Adat.Azonosító}', ";
                    szöveg += $"'{Adat.Mikori}', ";
                    szöveg += $"'{Adat.Beálló}', ";
                    szöveg += $"'{Adat.Üzemképtelen}', ";
                    szöveg += $"'{Adat.Üzemképeshiba}', ";
                    szöveg += $"'{Adat.Típus}', ";
                    szöveg += $"{Adat.Státus}) ";
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


        //elkopó
        public List<Adat_Nap_Hiba> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Nap_Hiba> Adatok = new List<Adat_Nap_Hiba>();
            Adat_Nap_Hiba Adat;
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
                                Adat = new Adat_Nap_Hiba(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["mikori"].ToÉrt_DaTeTime(),
                                    rekord["beálló"].ToStrTrim(),
                                    rekord["üzemképtelen"].ToStrTrim(),
                                    rekord["üzemképeshiba"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["státus"].ToÉrt_Long()
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

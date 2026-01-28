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

    public class Kezelő_Jármű_Javításiátfutástábla
    {
        readonly string jelszó = "plédke";
        string hely = "";
        readonly string táblanév = "xnapostábla";

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\hibanapló\Napi.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Javításiátfutástábla(hely.KönyvSzerk());
        }

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\hibanapló\Elkészült{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Javításiátfutástábla(hely.KönyvSzerk());
        }

        public List<Adat_Jármű_Javításiátfutástábla> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Jármű_Javításiátfutástábla> Adatok = new List<Adat_Jármű_Javításiátfutástábla>();
            Adat_Jármű_Javításiátfutástábla Adat;
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
                                Adat = new Adat_Jármű_Javításiátfutástábla(
                                        rekord["kezdődátum"].ToÉrt_DaTeTime(),
                                        rekord["végdátum"].ToÉrt_DaTeTime(),
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["hibaleírása"].ToStrTrim()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Jármű_Javításiátfutástábla> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = $"SELECT * FROM {táblanév} ";
            List<Adat_Jármű_Javításiátfutástábla> Adatok = new List<Adat_Jármű_Javításiátfutástábla>();
            Adat_Jármű_Javításiátfutástábla Adat;
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
                                Adat = new Adat_Jármű_Javításiátfutástábla(
                                        rekord["kezdődátum"].ToÉrt_DaTeTime(),
                                        rekord["végdátum"].ToÉrt_DaTeTime(),
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["hibaleírása"].ToStrTrim()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, List<Adat_Jármű_Javításiátfutástábla> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Jármű_Javításiátfutástábla Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév} (azonosító, kezdődátum, végdátum, hibaleírása) VALUES (";
                    szöveg += $"'{Adat.Azonosító.Trim()}', ";
                    szöveg += $"'{Adat.Kezdődátum:yyyy.MM.dd}', '{Adat.Végdátum:yyyy.MM.dd}', '{Adat.Hibaleírása}')";
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

        public void Módosítás(string Telephely, List<Adat_Jármű_Javításiátfutástábla> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Jármű_Javításiátfutástábla Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév} SET hibaleírása='{Adat.Hibaleírása}' WHERE [azonosító]='{Adat.Azonosító}'";
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

        public void Törlés(string Telephely, List<string> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<string> SzövegGy = new List<string>();
                foreach (string Adat in Adatok)
                {
                    string szöveg = $"DELETE FROM {táblanév} WHERE azonosító='{Adat}'";
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



        public void Rögzítés(string Telephely, int Év, List<Adat_Jármű_Javításiátfutástábla> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Jármű_Javításiátfutástábla Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév} (azonosító, kezdődátum, végdátum, hibaleírása) VALUES (";
                    szöveg += $"'{Adat.Azonosító.Trim()}', ";
                    szöveg += $"'{Adat.Kezdődátum:yyyy.MM.dd}', '{Adat.Végdátum:yyyy.MM.dd}', '{Adat.Hibaleírása}')";
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

        public void Módosítás(string Telephely, int Év, List<Adat_Jármű_Javításiátfutástábla> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Jármű_Javításiátfutástábla Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév} SET hibaleírása='{Adat.Hibaleírása}' WHERE [azonosító]='{Adat.Azonosító}'";
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

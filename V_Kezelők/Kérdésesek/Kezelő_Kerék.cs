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
    public class Kezelő_Kerék_Mérés
    {
        readonly string jelszó = "szabólászló";
        string hely;
        readonly string táblanév = "keréktábla";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Év}\telepikerék.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Méréstáblakerék(hely.KönyvSzerk());
        }

        public List<Adat_Kerék_Mérés> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            string szöveg = $"SELECT * FROM {táblanév} order by azonosító,pozíció ";

            List<Adat_Kerék_Mérés> Adatok = new List<Adat_Kerék_Mérés>();
            Adat_Kerék_Mérés Adat;

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
                                Adat = new Adat_Kerék_Mérés(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Pozíció"].ToStrTrim(),
                                        rekord["Kerékberendezés"].ToStrTrim(),
                                        rekord["Kerékgyártásiszám"].ToStrTrim(),
                                        rekord["Állapot"].ToStrTrim(),
                                        rekord["Méret"].ToÉrt_Int(),
                                        rekord["Módosító"].ToStrTrim(),
                                        rekord["Mikor"].ToÉrt_DaTeTime(),
                                        rekord["Oka"].ToStrTrim(),
                                        rekord["SAP"].ToÉrt_Int()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(int Év, Adat_Kerék_Mérés Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = $"INSERT INTO {táblanév}  (Azonosító, pozíció, kerékberendezés, kerékgyártásiszám, állapot, méret, mikor, Módosító, Oka, SAP) VALUES (";

                szöveg += $"'{Adat.Azonosító.Trim()}', ";
                szöveg += $"'{Adat.Pozíció.Trim()}', ";
                szöveg += $"'{Adat.Kerékberendezés.Trim()}', ";
                szöveg += $"'{Adat.Kerékgyártásiszám.Trim()}', ";
                szöveg += $"'{Adat.Állapot}', ";
                szöveg += $"{Adat.Méret}, ";
                szöveg += $"'{DateTime.Now}', ";
                szöveg += $"'{Program.PostásNév.Trim()}', ";
                szöveg += $"'{Adat.Oka.Trim()}', ";
                szöveg += $"{Adat.SAP} )";

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

        public void Rögzítés(int Év, List<Adat_Kerék_Mérés> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Kerék_Mérés Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév}  (Azonosító, pozíció, kerékberendezés, kerékgyártásiszám, állapot, méret, mikor, Módosító, Oka, SAP) VALUES (";
                    szöveg += $"'{Adat.Azonosító.Trim()}', ";
                    szöveg += $"'{Adat.Pozíció.Trim()}', ";
                    szöveg += $"'{Adat.Kerékberendezés.Trim()}', ";
                    szöveg += $"'{Adat.Kerékgyártásiszám.Trim()}', ";
                    szöveg += $"'{Adat.Állapot}', ";
                    szöveg += $"{Adat.Méret}, ";
                    szöveg += $"'{DateTime.Now}', ";
                    szöveg += $"'{Program.PostásNév.Trim()}', ";
                    szöveg += $"'{Adat.Oka.Trim()}', ";
                    szöveg += $"{Adat.SAP} )";
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

        public void Módosítás(int Év, List<Adat_Kerék_Mérés> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Kerék_Mérés Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév}  SET SAP={Adat.SAP} WHERE ";
                    szöveg += $" kerékberendezés='{Adat.Kerékberendezés}' and ";
                    szöveg += $" mikor=#{Adat.Mikor:yyyy-MM-dd HH:mm:ss}#";
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

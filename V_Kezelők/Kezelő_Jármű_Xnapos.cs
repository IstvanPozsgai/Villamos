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
    public class Kezelő_Jármű_Xnapos
    {
        readonly string jelszó = "plédke";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\hibanapló\napi.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Javításiátfutástábla(hely.KönyvSzerk());
        }

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\hibanapló\Elkészült{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Javításiátfutástábla(hely.KönyvSzerk());
        }

        public List<Adat_Jármű_Xnapos> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = $"SELECT * FROM xnapostábla";
            List<Adat_Jármű_Xnapos> Adatok = new List<Adat_Jármű_Xnapos>();
            Adat_Jármű_Xnapos Adat;
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
                                Adat = new Adat_Jármű_Xnapos(
                                            rekord["kezdődátum"].ToÉrt_DaTeTime(),
                                            rekord["végdátum"].ToÉrt_DaTeTime(),
                                            rekord["azonosító"].ToStrTrim(),
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

        public List<Adat_Jármű_Xnapos> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            string szöveg = $"SELECT * FROM xnapostábla";
            List<Adat_Jármű_Xnapos> Adatok = new List<Adat_Jármű_Xnapos>();
            Adat_Jármű_Xnapos Adat;
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
                                Adat = new Adat_Jármű_Xnapos(
                                            rekord["kezdődátum"].ToÉrt_DaTeTime(),
                                            rekord["végdátum"].ToÉrt_DaTeTime(),
                                            rekord["azonosító"].ToStrTrim(),
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

        public void Rögzítés(string Telephely, Adat_Jármű_Xnapos Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"INSERT INTO xnapostábla (kezdődátum, végdátum,  azonosító,  hibaleírása) VALUES (";
                szöveg += $"'{Adat.Kezdődátum}', '{Adat.Végdátum}', ";
                szöveg += $"'{Adat.Azonosító}', ";
                szöveg += $"'{Adat.Hibaleírása}')";
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

        public void Rögzítés(string Telephely, int Év, Adat_Jármű_Xnapos Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"INSERT INTO xnapostábla (kezdődátum, végdátum,  azonosító,  hibaleírása) VALUES (";
                szöveg += $"'{Adat.Kezdődátum}', '{Adat.Végdátum}', ";
                szöveg += $"'{Adat.Azonosító}', ";
                szöveg += $"'{Adat.Hibaleírása}')";
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

        public void Módosítás(string Telephely, Adat_Jármű_Xnapos Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE xnapostábla SET hibaleírása='{Adat.Hibaleírása}' ";
                szöveg += $" WHERE [azonosító]='{Adat.Azonosító}' ";
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

        public void Törlés(string Telephely, string Azonosító)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"DELETE FROM xnapostábla WHERE [azonosító]='{Azonosító}' ";
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

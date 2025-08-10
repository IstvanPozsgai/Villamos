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
    public class Kezelő_Kerék_Tábla
    {
        readonly string jelszó = "szabólászló";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerék.mdb";
        readonly string táblanév = "tábla";

        public Kezelő_Kerék_Tábla()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kerékbeolvasástábla(hely.KönyvSzerk());
        }


        public List<Adat_Kerék_Tábla> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Kerék_Tábla> Adatok = new List<Adat_Kerék_Tábla>();

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
                                Adat_Kerék_Tábla Adat = new Adat_Kerék_Tábla(
                                        rekord["Kerékberendezés"].ToStrTrim(),
                                        rekord["kerékmegnevezés"].ToStrTrim(),
                                        rekord["kerékgyártásiszám"].ToStrTrim(),
                                        rekord["föléberendezés"].ToStrTrim(),
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["pozíció"].ToStrTrim(),
                                        rekord["Dátum"].ToÉrt_DaTeTime(),
                                        rekord["objektumfajta"].ToStrTrim()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public void Módosítás(List<Adat_Kerék_Tábla> Adatok)
        {
            try
            {


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


        public void Rögzítés(List<Adat_Kerék_Tábla> Adatok)
        {
            try
            {
                List<string> SzövegGY = new List<string>();
                foreach (Adat_Kerék_Tábla Adat in Adatok)
                {
                    string szöveg = "INSERT INTO tábla (kerékberendezés, kerékmegnevezés, kerékgyártásiszám, föléberendezés, azonosító, pozíció, objektumfajta, dátum) VALUES (";
                    szöveg += $"'{Adat.Kerékberendezés}', "; // kerékberendezés
                    szöveg += $"'{Adat.Kerékmegnevezés}', "; // kerékmegnevezés
                    szöveg += $"'{Adat.Kerékgyártásiszám}', "; // kerékgyártásiszám
                    szöveg += $"'{Adat.Föléberendezés}', "; // föléberendezés
                    szöveg += $"'{Adat.Azonosító}', "; // azonosító
                    szöveg += $"'{Adat.Pozíció}', "; // pozíció
                    szöveg += $"'{Adat.Objektumfajta}', "; // objektumfajta
                    szöveg += $"'{Adat.Dátum:yyyy.MM.dd}') "; // dátum
                    SzövegGY.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGY);
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

        public void Törlés(List<string> Adatok)
        {
            try
            {
                List<string> SzövegGY = new List<string>();
                foreach (string Adat in Adatok)
                {
                    string szöveg = $"DELETE FROM tábla WHERE [kerékberendezés]='{Adat}'";
                    SzövegGY.Add(szöveg);
                }
                MyA.ABtörlés(hely, jelszó, SzövegGY);
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
        public List<Adat_Kerék_Tábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kerék_Tábla> Adatok = new List<Adat_Kerék_Tábla>();
            Adat_Kerék_Tábla Adat;

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
                                Adat = new Adat_Kerék_Tábla(
                                        rekord["Kerékberendezés"].ToStrTrim(),
                                        rekord["kerékmegnevezés"].ToStrTrim(),
                                        rekord["kerékgyártásiszám"].ToStrTrim(),
                                        rekord["föléberendezés"].ToStrTrim(),
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["pozíció"].ToStrTrim(),
                                        rekord["Dátum"].ToÉrt_DaTeTime(),
                                        rekord["objektumfajta"].ToStrTrim()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kerék_Tábla Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kerék_Tábla Adat = null;

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
                            rekord.Read();
                            Adat = new Adat_Kerék_Tábla(
                                    rekord["Kerékberendezés"].ToStrTrim(),
                                    rekord["kerékmegnevezés"].ToStrTrim(),
                                    rekord["kerékgyártásiszám"].ToStrTrim(),
                                    rekord["föléberendezés"].ToStrTrim(),
                                    rekord["azonosító"].ToStrTrim(),
                                    rekord["pozíció"].ToStrTrim(),
                                    rekord["Dátum"].ToÉrt_DaTeTime(),
                                    rekord["objektumfajta"].ToStrTrim()
                                      );
                        }
                    }
                }
            }
            return Adat;
        }
    }

}

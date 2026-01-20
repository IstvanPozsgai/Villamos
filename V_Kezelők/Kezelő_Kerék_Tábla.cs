using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;
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

        /// <summary>
        /// Alapra állítja a beépítési adatokat
        /// </summary>
        /// <param name="Adatok"></param>
        public void Módosítás_Alapra(List<Adat_Kerék_Tábla> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Kerék_Tábla Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév} SET ";
                    szöveg += $" [pozíció]='{Adat.Pozíció}', azonosító='{Adat.Azonosító}', föléberendezés='{Adat.Föléberendezés}'";
                    szöveg += $" WHERE [kerékberendezés]='{Adat.Kerékberendezés}'";
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

        public void Módosítás(List<Adat_Kerék_Tábla> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Kerék_Tábla Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév} SET";
                    szöveg += $" kerékmegnevezés='{Adat.Kerékmegnevezés}', ";
                    szöveg += $" kerékgyártásiszám='{Adat.Kerékgyártásiszám}', ";
                    szöveg += $" föléberendezés='{Adat.Föléberendezés}', ";
                    szöveg += $" azonosító='{Adat.Azonosító}', ";
                    szöveg += $" pozíció='{Adat.Pozíció}', ";
                    szöveg += $" objektumfajta='{Adat.Objektumfajta}', ";
                    szöveg += $" dátum='{Adat.Dátum:yyyy.MM.dd}' ";
                    szöveg += $" WHERE  [kerékberendezés]='{Adat.Kerékberendezés}'";
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

        public void Rögzítés(List<Adat_Kerék_Tábla> Adatok)
        {
            try
            {
                List<string> SzövegGY = new List<string>();
                foreach (Adat_Kerék_Tábla Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO tábla (kerékberendezés, kerékmegnevezés, kerékgyártásiszám, föléberendezés, azonosító, pozíció, objektumfajta, dátum) VALUES (";
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

        public void Osztályoz(List<Adat_Kerék_Tábla> AdatokBe)
        {
            try
            {
                List<Adat_Kerék_Tábla> Adatok = Lista_Adatok();
                List<Adat_Kerék_Tábla> AdatokGyAlap = new List<Adat_Kerék_Tábla>();
                List<Adat_Kerék_Tábla> AdatokGy = new List<Adat_Kerék_Tábla>();
                List<Adat_Kerék_Tábla> AdatokGyR = new List<Adat_Kerék_Tábla>();
                if (Adatok != null)
                {
                    foreach (Adat_Kerék_Tábla Elem in AdatokBe)
                    {
                        // a pozícióban eddig volt berendezést felszabadítja
                        string RégiBerszám = (from a in Adatok
                                              where a.Pozíció == Elem.Pozíció && a.Azonosító == Elem.Azonosító && a.Kerékberendezés != Elem.Kerékberendezés
                                              select a.Kerékberendezés).FirstOrDefault();
                        if (RégiBerszám != null)
                        {
                            Adat_Kerék_Tábla Adat = new Adat_Kerék_Tábla(RégiBerszám, "_", "_", "_");
                            AdatokGy.Add(Adat);
                        }
                        //Ha benne van, de rossz helyen
                        Adat_Kerék_Tábla Rekord_berendezés = (from a in Adatok
                                                              where (a.Kerékberendezés == Elem.Kerékberendezés && a.Azonosító != Elem.Azonosító)
                                                                 || (a.Kerékberendezés == Elem.Kerékberendezés && a.Pozíció != Elem.Pozíció)
                                                              select a).FirstOrDefault();
                        if (Rekord_berendezés != null) AdatokGy.Add(Elem);

                        //Ha nincs benne
                        Rekord_berendezés = (from a in Adatok
                                             where (a.Kerékberendezés == Elem.Kerékberendezés)
                                             select a).FirstOrDefault();
                        if (Rekord_berendezés == null) AdatokGyR.Add(Elem);
                    }

                }
                if (AdatokGyAlap.Count > 0) Módosítás_Alapra(AdatokGyAlap);
                if (AdatokGy.Count > 0) Módosítás(AdatokGy);
                if (AdatokGyR.Count > 0) Rögzítés(AdatokGyR);
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

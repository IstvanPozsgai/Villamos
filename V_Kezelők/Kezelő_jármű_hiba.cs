﻿using System;
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
    public class Kezelő_jármű_hiba
    {
        readonly string jelszó = "pozsgaii";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\villamos\hiba.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Hibatáblalap(hely.KönyvSzerk());
        }

        public List<Adat_Jármű_hiba> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_hiba> Adatok = new List<Adat_Jármű_hiba>();

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
                                Adat_Jármű_hiba adat = new Adat_Jármű_hiba(
                                    rekord["létrehozta"].ToStrTrim(),
                                    rekord["korlát"].ToÉrt_Long(),
                                    rekord["hibaleírása"].ToStrTrim(),
                                    rekord["idő"].ToÉrt_DaTeTime(),
                                    rekord["javítva"].ToÉrt_Bool(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["azonosító"].ToStrTrim(),
                                    rekord["hibáksorszáma"].ToÉrt_Long()
                                    );
                                Adatok.Add(adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Jármű_hiba Adat)
        {
            try
            {    // rögzítjük az adatot
                string szöveg = "SELECT * FROM hibatábla";
                List<Adat_Jármű_hiba> Adatok = Lista_adatok(hely, jelszó, szöveg);

                Adat_Jármű_hiba Elem = (from a in Adatok
                                        where a.Azonosító == Adat.Azonosító
                                        && a.Hibaleírása.Contains(Adat.Hibaleírása)
                                        select a).FirstOrDefault();

                if (Elem == null)
                {
                    long Sorszám = 1;
                    Adatok = (from a in Adatok
                              where a.Azonosító == Adat.Azonosító
                              select a).ToList();

                    if (Adatok != null && Adatok.Count > 0)
                        Sorszám = Adatok.Max(a => a.Hibáksorszáma) + 1;
                    // ha nem létezik 
                    szöveg = "INSERT INTO hibatábla  ( létrehozta, korlát, hibaleírása, idő, javítva, típus, azonosító, hibáksorszáma ) VALUES (";
                    szöveg += $"'{Adat.Létrehozta.Trim()}', ";
                    szöveg += $"{Adat.Korlát}, ";
                    szöveg += $"'{Adat.Hibaleírása.Trim()}', ";
                    szöveg += $"'{Adat.Idő}', ";
                    szöveg += $"{Adat.Javítva}, ";
                    szöveg += $"'{Adat.Típus.Trim()}', ";
                    szöveg += $"'{Adat.Azonosító.Trim()}', ";
                    szöveg += $"{Sorszám})";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Hiba Rögzítés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosítás(string hely, string jelszó, Adat_Jármű_hiba Adat)
        {
            try
            {
                string szöveg = "UPDATE Hibatábla SET ";
                szöveg += $"Korlát={Adat.Korlát}, ";
                szöveg += $"létrehozta='{Program.PostásNév.Trim()}', ";
                szöveg += $"hibaleírása='{Adat.Hibaleírása}', ";
                szöveg += $"idő='{DateTime.Now}'";
                szöveg += $" WHERE azonosító='{Adat.Azonosító}'";
                szöveg += $" AND hibáksorszáma={Adat.Hibáksorszáma}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Hiba Módosítás", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Törlés(string hely, string jelszó, Adat_Jármű_hiba Adat)
        {
            try
            {
                string szöveg = "DELETE FROM Hibatábla ";
                szöveg += $" WHERE azonosító='{Adat.Azonosító}' AND hibáksorszáma={Adat.Hibáksorszáma}";
                MyA.ABtörlés(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Hiba Törlés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<Adat_Jármű_hiba> Lista_adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            List<Adat_Jármű_hiba> Adatok = new List<Adat_Jármű_hiba>();
            string szöveg = "SELECT * FROM hibatábla";

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
                                Adat_Jármű_hiba adat = new Adat_Jármű_hiba(
                                    rekord["létrehozta"].ToStrTrim(),
                                    rekord["korlát"].ToÉrt_Long(),
                                    rekord["hibaleírása"].ToStrTrim(),
                                    rekord["idő"].ToÉrt_DaTeTime(),
                                    rekord["javítva"].ToÉrt_Bool(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["azonosító"].ToStrTrim(),
                                    rekord["hibáksorszáma"].ToÉrt_Long()
                                    );
                                Adatok.Add(adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, Adat_Jármű_hiba Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<Adat_Jármű_hiba> Adatok = Lista_adatok(Telephely);

                Adat_Jármű_hiba Elem = (from a in Adatok
                                        where a.Azonosító == Adat.Azonosító
                                        && a.Hibaleírása.Contains(Adat.Hibaleírása)
                                        select a).FirstOrDefault();

                if (Elem == null)
                {
                    long Sorszám = 1;
                    Adatok = (from a in Adatok
                              where a.Azonosító == Adat.Azonosító
                              select a).ToList();

                    if (Adatok != null && Adatok.Count > 0)
                        Sorszám = Adatok.Max(a => a.Hibáksorszáma) + 1;
                    // ha nem létezik 
                    string szöveg = "INSERT INTO hibatábla  ( létrehozta, korlát, hibaleírása, idő, javítva, típus, azonosító, hibáksorszáma ) VALUES (";
                    szöveg += $"'{Adat.Létrehozta.Trim()}', ";
                    szöveg += $"{Adat.Korlát}, ";
                    szöveg += $"'{Adat.Hibaleírása.Trim()}', ";
                    szöveg += $"'{Adat.Idő}', ";
                    szöveg += $"{Adat.Javítva}, ";
                    szöveg += $"'{Adat.Típus.Trim()}', ";
                    szöveg += $"'{Adat.Azonosító.Trim()}', ";
                    szöveg += $"{Sorszám})";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }

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

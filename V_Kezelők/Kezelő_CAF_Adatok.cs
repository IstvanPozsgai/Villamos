﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_CAF_Adatok
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
        readonly string jelszó = "CzabalayL";

        public Kezelő_CAF_Adatok()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.CAFtábla(hely.KönyvSzerk());
        }

        public List<Adat_CAF_Adatok> Lista_Adatok(int Év = 1900)
        {
            string szöveg = "SELECT * FROM adatok ORDER BY azonosító";
            List<Adat_CAF_Adatok> Adatok = new List<Adat_CAF_Adatok>();
            Adat_CAF_Adatok Adat;

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
                                Adat = new Adat_CAF_Adatok(
                                        rekord["id"].ToÉrt_Double(),
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["Vizsgálat"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["Dátum_program"].ToÉrt_DaTeTime(),
                                        rekord["Számláló"].ToÉrt_Long(),
                                        rekord["Státus"].ToÉrt_Int(),
                                        rekord["KM_Sorszám"].ToÉrt_Int(),
                                        rekord["IDŐ_Sorszám"].ToÉrt_Int(),
                                        rekord["IDŐvKM"].ToÉrt_Int(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["KmRogzitett_e"].ToÉrt_Bool(),
                                        rekord["Telephely"].ToStrTrim()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            if (Év != 1900) Adatok.AddRange(ElőzőÉvek(Év));

            return Adatok;
        }

        public List<Adat_CAF_Adatok> ElőzőÉvek(int Év)
        {
            List<Adat_CAF_Adatok> Válasz = new List<Adat_CAF_Adatok>();
            try
            {
                for (int i = Év; i < DateTime.Today.Year; i++)
                {
                    string helyNapló = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF_{i}.mdb";
                    if (File.Exists(helyNapló))
                        Válasz.AddRange(Lista_Adatok(helyNapló));
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
            return Válasz;
        }

        public Adat_CAF_Adatok Egy_Adat(string Azonosító, int IDŐvKM = 1)
        {
            Adat_CAF_Adatok Adat = null;
            try
            {
                List<Adat_CAF_Adatok> Adatok = Lista_Adatok();
                if (Adatok.Count > 0)
                    if (IDŐvKM == 1)
                        Adat = (from a in Adatok
                                where a.Azonosító == Azonosító
                                && a.Státus < 9
                                orderby a.Dátum descending
                                select a).FirstOrDefault();
                    else
                        Adat = (from a in Adatok
                                where a.Azonosító == Azonosító
                                && a.Státus < 9
                                && a.IDŐvKM == 2
                                orderby a.Dátum descending
                                select a).FirstOrDefault();
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
            return Adat;
        }

        public Adat_CAF_Adatok Egy_Adat_Id_Előző(string Azonosító, double Id)
        {
            Adat_CAF_Adatok Adat = null;
            try
            {
                List<Adat_CAF_Adatok> Adatok = Lista_Adatok();
                if (Adatok.Count > 0)
                    Adat = (from a in Adatok
                            where a.Id < Id
                            && a.Azonosító == Azonosító
                            && a.Státus < 9
                            orderby a.Id descending
                            select a).FirstOrDefault();
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
            return Adat;
        }

        public Adat_CAF_Adatok Egy_Adat_Spec(string Azonosító, DateTime Dátum, int státus = 8)
        {
            Adat_CAF_Adatok Adat = null;
            try
            {
                List<Adat_CAF_Adatok> Adatok = Lista_Adatok();
                if (Adatok.Count > 0)
                    Adat = (from a in Adatok
                            where a.Dátum == Dátum
                            && a.Azonosító == Azonosító
                            && a.Státus < státus
                            orderby a.Id descending
                            select a).FirstOrDefault();
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
            return Adat;
        }

        public Adat_CAF_Adatok Utolso_Ido_Vizsgalat_Adatai(string Azonosító)
        {
            return Lista_Adatok()
                .Where(a => a.Státus == 6 && a.Azonosító == $"{Azonosító}" && a.IDŐvKM == 1)
                .OrderByDescending(a => a.Dátum)
                .FirstOrDefault();
        }

        public Adat_CAF_Adatok Utolso_Km_Vizsgalat_Adatai(string Azonosító)
        {
            return Lista_Adatok()
                .Where(a => a.Státus == 6 && a.Azonosító == $"{Azonosító}" && a.IDŐvKM == 2 && a.Megjegyzés.Trim() != "Ütemezési Segéd")
                .OrderByDescending(a => a.Dátum)
                .FirstOrDefault();
        }

        public double Sorszám()
        {
            double válasz = 1;
            try
            {
                List<Adat_CAF_Adatok> Adatok = Lista_Adatok();
                if (Adatok.Count > 0) válasz = Adatok.Max(a => a.Id) + 1;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Sorsszám", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return válasz;
        }

        public void Döntés(Adat_CAF_Adatok Adat)
        {
            try
            {
                double sorszám;
                // ha nincs kitöltve az id, megkeressük a következő számot
                if (Adat.Id == 0)
                    sorszám = Sorszám();
                else
                    sorszám = Adat.Id;

                List<Adat_CAF_Adatok> Adatok = Lista_Adatok();

                Adat_CAF_Adatok Elem = (from a in Adatok
                                        where a.Id == sorszám
                                        select a).FirstOrDefault();

                if (Elem != null)
                    Módosítás(Adat);
                else
                    Rögzítés(Adat, sorszám);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "RögzítiMódosít", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Döntés(List<Adat_CAF_Adatok> Adatok, Adat_CAF_Adatok Adat)
        {
            try
            {
                double sorszám;
                // ha nincs kitöltve az id, megkeressük a következő számot
                if (Adat.Id == 0)
                    sorszám = Sorszám();
                else
                    sorszám = Adat.Id;

                //   List<Adat_CAF_Adatok> Adatok = Lista_Adatok();

                Adat_CAF_Adatok Elem = (from a in Adatok
                                        where a.Id == sorszám
                                        select a).FirstOrDefault();

                if (Elem != null)
                    Módosítás(Adat);
                else
                    Rögzítés(Adat, sorszám);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "RögzítiMódosít", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Rögzítés(Adat_CAF_Adatok Adat, double Sorszám)
        {
            try
            {
                string szöveg = "INSERT INTO adatok (id, azonosító, vizsgálat, Dátum, számláló, státus, km_sorszám, idő_sorszám, idővKM, megjegyzés, Dátum_program, Telephely, KmRogzitett_e) VALUES (";
                szöveg += $"{Sorszám}, "; // id 
                szöveg += $"'{Adat.Azonosító}', "; // azonosító
                szöveg += $"'{Adat.Vizsgálat.Trim()}', "; // vizsgálat
                szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', "; // Dátum
                szöveg += $"{Adat.Számláló}, "; // számláló
                szöveg += $"{Adat.Státus}, "; // státus 
                szöveg += $"{Adat.KM_Sorszám}, "; // km_sorszám
                szöveg += $"{Adat.IDŐ_Sorszám}, "; // idő_sorszám
                szöveg += $"{Adat.IDŐvKM}, ";// idővKM
                szöveg += $"'{Adat.Megjegyzés}', "; // megjegyzés
                szöveg += $"'{Adat.Dátum_program:yyyy.MM.dd}', ";
                szöveg += $"'{Adat.Telephely}',";
                szöveg += $"{Adat.Státus == 2})"; //KmRogzitett_e
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

        public void Rögzítés(List<Adat_CAF_Adatok> Adatok)
        {
            try
            {
                double sorszám = Sorszám();
                List<string> SzövegGy = new List<string>();
                foreach (Adat_CAF_Adatok Adat in Adatok)
                {
                    string szöveg = "INSERT INTO adatok (id, azonosító, vizsgálat, Dátum, számláló, státus, km_sorszám, idő_sorszám, idővKM, megjegyzés, Dátum_program, Telephely, KmRogzitett_e) VALUES (";
                    szöveg += $"{sorszám}, "; // id 
                    szöveg += $"'{Adat.Azonosító}', "; // azonosító
                    szöveg += $"'{Adat.Vizsgálat.Trim()}', "; // vizsgálat
                    szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', "; // Dátum
                    szöveg += $"{Adat.Számláló}, "; // számláló
                    szöveg += $"{Adat.Státus}, "; // státus 
                    szöveg += $"{Adat.KM_Sorszám}, "; // km_sorszám
                    szöveg += $"{Adat.IDŐ_Sorszám}, "; // idő_sorszám
                    szöveg += $"{Adat.IDŐvKM}, ";// idővKM
                    szöveg += $"'{Adat.Megjegyzés}', "; // megjegyzés
                    szöveg += $"'{Adat.Dátum_program:yyyy.MM.dd}', ";
                    szöveg += $"'{Adat.Telephely}',"; // Telephely
                    szöveg += $"{Adat.Státus == 2})"; //KmRogzitett_e
                    SzövegGy.Add(szöveg);
                    sorszám++;
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

        public void Rögzítés(List<Adat_CAF_Adatok> Adatok, string Arhív)
        {
            try
            {
                double sorszám = Sorszám();
                List<string> SzövegGy = new List<string>();
                foreach (Adat_CAF_Adatok Adat in Adatok)
                {
                    string szöveg = "INSERT INTO adatok (id, azonosító, vizsgálat, Dátum, számláló, státus, km_sorszám, idő_sorszám, idővKM, megjegyzés, Dátum_program, Telephely, KmRogzitett_e) VALUES (";
                    szöveg += $"{sorszám}, "; // id 
                    szöveg += $"'{Adat.Azonosító}', "; // azonosító
                    szöveg += $"'{Adat.Vizsgálat.Trim()}', "; // vizsgálat
                    szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', "; // Dátum
                    szöveg += $"{Adat.Számláló}, "; // számláló
                    szöveg += $"{Adat.Státus}, "; // státus 
                    szöveg += $"{Adat.KM_Sorszám}, "; // km_sorszám
                    szöveg += $"{Adat.IDŐ_Sorszám}, "; // idő_sorszám
                    szöveg += $"{Adat.IDŐvKM}, ";// idővKM
                    szöveg += $"'{Adat.Megjegyzés}', "; // megjegyzés
                    szöveg += $"'{Adat.Dátum_program:yyyy.MM.dd}', ";
                    szöveg += $"'{Adat.Telephely}',";
                    szöveg += $"{Adat.Státus == 2})"; //KmRogzitett_e
                    SzövegGy.Add(szöveg);
                    sorszám++;
                }
                MyA.ABMódosítás(Arhív, jelszó, SzövegGy);
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

        public void Módosítás(Adat_CAF_Adatok Adat)
        {
            try
            {
                string szöveg = "UPDATE adatok  SET ";
                szöveg += $"vizsgálat='{Adat.Vizsgálat}', "; // vizsgálat
                szöveg += $"Dátum='{Adat.Dátum:yyyy.MM.dd}', "; // Dátum
                szöveg += $"számláló={Adat.Számláló}, "; // számláló
                szöveg += $"státus={Adat.Státus}, "; // státus 
                szöveg += $"km_sorszám={Adat.KM_Sorszám}, "; // km_sorszám
                szöveg += $"idő_sorszám={Adat.IDŐ_Sorszám}, "; // idő_sorszám
                szöveg += $"megjegyzés='{Adat.Megjegyzés}', ";// megjegyzés
                szöveg += $"idővKM={Adat.IDŐvKM}, ";
                szöveg += $"Telephely='{Adat.Telephely}', ";
                szöveg += $"KmRogzitett_e={Adat.Státus == 2}"; //KmRogzitett_e
                szöveg += $" WHERE id={Adat.Id} ";
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

        public void Módosítás_Státus(double Sorszám, int státus, string Telephely = "")
        {
            string szöveg;
            try
            {
                szöveg = $"UPDATE adatok  SET Státus={státus}, KmRogzitett_e={státus == 2}, Telephely='{Telephely}'  WHERE id={Sorszám}";
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

        public void Módosítás_Km(double Id, int Szamlalo, string Telephely = "")
        {
            string szöveg;
            try
            {
                szöveg = $"UPDATE adatok SET Számláló={Szamlalo}, KmRogzitett_e=FALSE, Telephely='{Telephely}' WHERE id={Id}";
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

        public void Törlés(DateTime Dátum, string Azonosító, int státus = 0)
        {
            try
            {
                string szöveg = $"DELETE FROM adatok WHERE [Dátum]>=#{Dátum:MM-dd-yyyy}# AND azonosító='{Azonosító}' And státus={státus}";
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

        public void Törlés(List<Adat_CAF_Adatok_Pót> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_CAF_Adatok_Pót Adat in Adatok)
                {
                    string szöveg = $"DELETE FROM adatok WHERE [Dátum]>=#{Adat.Dátum:MM-dd-yyyy}# AND azonosító='{Adat.Azonosító}' And státus={Adat.Státus}";
                    SzövegGy.Add(szöveg);
                }
                MyA.ABtörlés(hely, jelszó, SzövegGy);
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

        public void TörlésArchív(List<Adat_CAF_Adatok> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_CAF_Adatok Adat in Adatok)
                {
                    string szöveg = $"DELETE FROM adatok WHERE id={Adat.Id}";
                    SzövegGy.Add(szöveg);
                }
                MyA.ABtörlés(hely, jelszó, SzövegGy);
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

        public void Ütemez(List<Adat_CAF_Adatok_Pót> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_CAF_Adatok_Pót Adat in Adatok)
                {
                    string szöveg = "UPDATE adatok  SET Státus=2 ";
                    szöveg += $" WHERE azonosító='{Adat.Azonosító}' AND dátum>=#{Adat.Dátumtól:MM-dd-yyyy}# ";
                    szöveg += $" AND dátum<=#{Adat.Dátumig:MM-dd-yyyy}# AND Státus={Adat.Státus} AND KmRogzitett_e=TRUE";
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

        public void Archíválás(DateTime Dátum, List<Adat_CAF_Adatok> Adatok)
        {
            try
            {
                string helyNapló = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF_{Dátum.Year}.mdb";
                if (File.Exists(helyNapló)) throw new HibásBevittAdat("Már az archiválás bevefejőzött.");
                //Adattábla létrehozása
                Adatbázis_Létrehozás.CAFAdatokArchív(helyNapló.KönyvSzerk());
                //Rögzítjük az adatokat
                Rögzítés(Adatok, helyNapló);
                //Töröljük az adatokat
                TörlésArchív(Adatok);
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

        public List<Adat_CAF_Adatok> Lista_Adatok(string hely)
        {
            List<Adat_CAF_Adatok> Adatok = new List<Adat_CAF_Adatok>();
            Adat_CAF_Adatok Adat;
            string szöveg = "SELECT * FROM adatok ORDER BY azonosító";
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
                                //DateTime dátum = DateTime.TryParse(rekord["dátum"].ToString(), out dátum) ? dátum : new DateTime(1900, 1, 1);
                                //DateTime Dátum_program = DateTime.TryParse(rekord["Dátum_program"].ToString(), out Dátum_program) ? Dátum_program : new DateTime(1900, 1, 1);
                                Adat = new Adat_CAF_Adatok(
                                        rekord["id"].ToÉrt_Double(),
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["Vizsgálat"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["Dátum_program"].ToÉrt_DaTeTime(),
                                        rekord["Számláló"].ToÉrt_Long(),
                                        rekord["Státus"].ToÉrt_Int(),
                                        rekord["KM_Sorszám"].ToÉrt_Int(),
                                        rekord["IDŐ_Sorszám"].ToÉrt_Int(),
                                        rekord["IDŐvKM"].ToÉrt_Int(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["KmRogzitett_e"].ToÉrt_Bool(),
                                        rekord["Telephely"].ToStrTrim()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        //Ez állítja át a jelenleg is adb-ben lévő elemeket True státuszura, a későbbiekben már figyeli az előütemező/módosító metódus.
        public void StatustVizsgal(List<Adat_CAF_Adatok> Adatok)
        {
            try
            {
                foreach (Adat_CAF_Adatok item in Adatok)
                {

                    if (item.Státus == 2)
                    {
                        string szöveg = "UPDATE adatok  SET ";
                        szöveg += $"KmRogzitett_e=True ";
                        szöveg += $" WHERE id={item.Id}";
                        MyA.ABMódosítás(hely, jelszó, szöveg);
                    }
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

        // Amikor 2x szerepel egy ID, akkor azt vegyük alapul, ahol a KM érték van és nem 0? Viszont vannak olyanok is,
        // ahol mindkét helyen eltérő KM szerepel (Pl. 47347 id - 2231 psz.).
        // Ilyenkor vegyem a nagyobbat alapul?
        //public void KmHibas_eVizsgal(List<Adat_CAF_Adatok> Adatok)
        //{

        //    var Villamos_Palyaszamok = Adatok.GroupBy(a => a.Azonosító)
        //                                           .ToList();
        //    try
        //    {
        //        List<string> SzövegGy = new List<string>();
        //        for (global::System.Int32 i = 0; i < Villamos_Palyaszamok.Count; i++)
        //        {
        //            //Itt a végén azért kell a key, mert a GroupBy miat groupingolva adja vissza kulcs-értékként.
        //            List<Adat_CAF_Adatok> Vizsgalando_Adatok = Adatok.Where(a => a.Státus == 6 && a.Megjegyzés != "Ütemezési Segéd" && a.Azonosító == Villamos_Palyaszamok[i].Key)
        //                                  .OrderBy(a => a.Dátum)
        //                                  .ToList();

        //            for (global::System.Int32 j = 0; j < Vizsgalando_Adatok.Count - 1; j++)
        //            {
        //                if (Vizsgalando_Adatok[j + 1].Számláló < Vizsgalando_Adatok[j].Számláló)
        //                {
        //                    string szoveg = $"UPDATE adatok SET KmHibas_e=TRUE WHERE id={Vizsgalando_Adatok[j + 1].Id}";
        //                    SzövegGy.Add(szoveg);
        //                }
        //            }
        //        }
        //        MyA.ABMódosítás(hely, jelszó, SzövegGy);
        //    }
        //    catch (HibásBevittAdat ex)
        //    {
        //        MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    catch (Exception ex)
        //    {
        //        HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
        //        MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}
    }
}

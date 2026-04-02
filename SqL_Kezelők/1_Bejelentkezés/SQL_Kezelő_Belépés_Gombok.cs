using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class SQL_Kezelő_Belépés_Gombok
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\SQL\Belépés.db";
        readonly string jelszó = "ForgalmiUtasítás";
        readonly string táblanév = "Tbl_Bejelentkezés_Gombok";

        public SQL_Kezelő_Belépés_Gombok()
        {
            if (!File.Exists(hely)) Tábla_Létrehozás();
            if (!MyA.SqLite_ABvanTábla(hely, jelszó, táblanév)) Tábla_Létrehozás();
        }

        public void Tábla_Létrehozás()
        {
            try
            {
                string szöveg = $@"CREATE TABLE {táblanév} (
                                GombokId INTEGER PRIMARY KEY AUTOINCREMENT,
                                FromName TEXT, 
                                GombName TEXT, 
                                GombFelirat TEXT, 
                                Szervezet TEXT, 
                                Látható INTEGER, 
                                Törölt INTEGER,
                                Súgó Integer    
                                );";
                MyA.SqLite_TáblaLétrehozás(hely.KönyvSzerk(), jelszó, szöveg);
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

        public List<Adat_Bejelentkezés_Gombok> Lista_Adatok()
        {
            List<Adat_Bejelentkezés_Gombok> Adatok = new List<Adat_Bejelentkezés_Gombok>();
            try
            {
                Adatok = MyA.Lista_Adatok(hely, jelszó, táblanév, rekord => new Adat_Bejelentkezés_Gombok(
                                rekord["GombokId"].ToÉrt_Int(),
                                rekord["FromName"].ToStrTrim(),
                                rekord["GombName"].ToStrTrim(),
                                rekord["GombFelirat"].ToStrTrim(),
                                rekord["Szervezet"].ToStrTrim(),
                                rekord["Látható"].ToÉrt_Bool(),
                                rekord["Törölt"].ToÉrt_Bool(),
                                rekord["Súgó"].ToÉrt_Bool()
                                ));
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
            return Adatok;
        }

        public void Döntés(Adat_Bejelentkezés_Gombok Adat)
        {
            try
            {
                List<Adat_Bejelentkezés_Gombok> Adatok = Lista_Adatok();
                //Ha 0 id-vel érkezik akkor rögzíjük
                if (Adat.GombokId == 0)
                {
                    // Ha van ilyen gomb már a lapon akkor csak akkor engedjük rögzíteni, ha többször akarjuk felhasználni a gombot.
                    Adat_Bejelentkezés_Gombok gomb = (from a in Adatok
                                                      where a.GombName == Adat.GombName
                                                      && a.FormName == Adat.FormName
                                                      && a.Törölt == false
                                                      select a).FirstOrDefault();
                    if (gomb != null)
                    {
                        DialogResult valasz = MessageBox.Show($"Ez a {gomb.GombokId} szám alatt már szerepel, létre akarsz hozni egy új elemet?",
                                   "Megerősítés",
                                   MessageBoxButtons.OKCancel,
                                   MessageBoxIcon.Question);
                        if (valasz != DialogResult.OK) return;
                    }
                    Rögzítés(Adat);
                }
                else
                {
                    Módosítás(Adat);
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

        public void Rögzítés(Adat_Bejelentkezés_Gombok Adat)
        {
            try
            {

                string szöveg = $"INSERT INTO {táblanév} ( FromName, GombName, GombFelirat, Szervezet, Látható, Törölt, Súgó) VALUES ";
                szöveg += $@"( @FromName, @GombName, @GombFelirat, @Szervezet, @Látható, @Törölt, @Súgó)";

                SqliteCommand cmd = new SqliteCommand(szöveg);

                cmd.Parameters.AddWithValue("@FromName", Adat.FormName);
                cmd.Parameters.AddWithValue("@GombName", Adat.GombName);
                cmd.Parameters.AddWithValue("@GombFelirat", Adat.GombFelirat);
                cmd.Parameters.AddWithValue("@Szervezet", Adat.Szervezet);
                cmd.Parameters.AddWithValue("@Látható", Adat.Látható);
                cmd.Parameters.AddWithValue("@Törölt", Adat.Törölt);
                cmd.Parameters.AddWithValue("@Súgó", Adat.Súgó);

                MyA.SqLite_Módosítás(hely, jelszó, cmd);
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

        public void Módosítás(Adat_Bejelentkezés_Gombok Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $@"FromName=@FromName, ";
                szöveg += $@"GombName=@GombName, ";
                szöveg += $@"GombFelirat=@GombFelirat, ";
                szöveg += $@"Szervezet=@Szervezet, ";
                szöveg += $@"Látható=@Látható, ";
                szöveg += $@"Törölt=@Törölt, ";
                szöveg += $@"Súgó=@Súgó ";
                szöveg += $@"WHERE GombokId=@GombokId;";

                SqliteCommand cmd = new SqliteCommand(szöveg);

                cmd.Parameters.AddWithValue("@GombokId", Adat.GombokId);
                cmd.Parameters.AddWithValue("@FromName", Adat.FormName);
                cmd.Parameters.AddWithValue("@GombName", Adat.GombName);
                cmd.Parameters.AddWithValue("@GombFelirat", Adat.GombFelirat);
                cmd.Parameters.AddWithValue("@Szervezet", Adat.Szervezet);
                cmd.Parameters.AddWithValue("@Látható", Adat.Látható);
                cmd.Parameters.AddWithValue("@Törölt", Adat.Törölt);
                cmd.Parameters.AddWithValue("@Súgó", Adat.Súgó);

                MyA.SqLite_Módosítás(hely, jelszó, cmd);
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

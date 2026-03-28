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
    // JAVÍTANDÓ:Nincs bekötve törölhető
    public class Kezelő_Belépés_Gombok
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\ÚJ_Belépés.mdb";
        readonly string jelszó = "ForgalmiUtasítás";
        readonly string táblanév = "Tábla_Gombok";

        public Kezelő_Belépés_Gombok()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Adatbázis_Gombok(hely.KönyvSzerk());
            if (!AdatBázis_kezelés.TáblaEllenőrzés(hely, jelszó, táblanév)) Adatbázis_Létrehozás.Adatbázis_Gombok(hely);
        }

        public List<Adat_Bejelentkezés_Gombok> Lista_Adatok()
        {
            List<Adat_Bejelentkezés_Gombok> Adatok = new List<Adat_Bejelentkezés_Gombok>();
            string szöveg = $"SELECT * FROM {táblanév}";
            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";

            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                using (OleDbCommand Parancs = new OleDbCommand(szöveg, Kapcsolat))
                {
                    Kapcsolat.Open();
                    using (OleDbDataReader rekord = Parancs.ExecuteReader())
                    {
                        if (rekord.HasRows)
                        {
                            while (rekord.Read())
                            {
                                Adat_Bejelentkezés_Gombok Adat = new Adat_Bejelentkezés_Gombok(
                                        rekord["GombokId"].ToÉrt_Int(),
                                        rekord["FromName"].ToStrTrim(),
                                        rekord["GombName"].ToStrTrim(),
                                        rekord["GombFelirat"].ToStrTrim(),
                                        rekord["Szervezet"].ToStrTrim(),
                                        rekord["Látható"].ToÉrt_Bool(),
                                        rekord["Törölt"].ToÉrt_Bool());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
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
                                                      && a.FromName == Adat.FromName
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
                string szöveg = $"INSERT INTO {táblanév} (  FromName, GombName, GombFelirat, Szervezet, Látható, Törölt) VALUES (";
                szöveg += $"'{Adat.FromName}', '{Adat.GombName}', '{Adat.GombFelirat}', '{Adat.Szervezet}', {Adat.Látható}, {Adat.Törölt})";
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

        public void Módosítás(Adat_Bejelentkezés_Gombok Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"FromName ='{Adat.FromName}', ";
                szöveg += $"GombName ='{Adat.GombName}', ";
                szöveg += $"GombFelirat ='{Adat.GombFelirat}', ";
                szöveg += $"Szervezet ='{Adat.Szervezet}', ";
                szöveg += $"Látható ={Adat.Látható}, ";
                szöveg += $"Törölt ={Adat.Törölt} ";
                szöveg += $"WHERE GombokId = {Adat.GombokId}";
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
    }
}

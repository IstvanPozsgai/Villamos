using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Kezelők;
using static System.IO.File;
using MyA = Adatbázis;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok.Beosztás
{
    public class Beosztás_Rögzítés
    {
        #region Kezelők,Listák

        readonly Kezelő_Dolgozó_Beosztás_Napló KézNapló = new Kezelő_Dolgozó_Beosztás_Napló();
        List<Adat_Dolgozó_Beosztás_Napló> AdatokNapló = new List<Adat_Dolgozó_Beosztás_Napló>();

        readonly Kezelő_Szatube_Beteg KézBeteg = new Kezelő_Szatube_Beteg();
        List<Adat_Szatube_Beteg> AdatokBeteg = new List<Adat_Szatube_Beteg>();

        readonly Kezelő_Szatube_Szabadság KézSzabad = new Kezelő_Szatube_Szabadság();
        List<Adat_Szatube_Szabadság> AdatokSzabad = new List<Adat_Szatube_Szabadság>();

        readonly Kezelő_Szatube_Túlóra KézTúlóra = new Kezelő_Szatube_Túlóra();
        List<Adat_Szatube_Túlóra> AdatokTúlóra = new List<Adat_Szatube_Túlóra>();

        readonly Kezelő_Szatube_Aft KézAft = new Kezelő_Szatube_Aft();
        List<Adat_Szatube_AFT> AdatokAft = new List<Adat_Szatube_AFT>();

        readonly Kezelő_Szatube_Csúsztatás KézCsúsztatás = new Kezelő_Szatube_Csúsztatás();
        List<Adat_Szatube_Csúsztatás> AdatokCsúsztatás = new List<Adat_Szatube_Csúsztatás>();

        private void ListaNapló(string Cmbtelephely)
        {
            try
            {
                AdatokNapló.Clear();

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\naplózás\{DateTime.Now:yyyyMM}napló.mdb";
                string jelszó = "kerekeskút";
                string szöveg = "Select * FROM adatok";

                AdatokNapló = KézNapló.Lista_Adatok(hely, jelszó, szöveg);
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

        private void ListaBeteg(string Cmbtelephely, DateTime Dátum)
        {
            try
            {
                AdatokBeteg.Clear();

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\Szatubecs\{Dátum.Year}Szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg = "SELECT * FROM beteg";

                AdatokBeteg = KézBeteg.Lista_Adatok(hely, jelszó, szöveg);
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

        private void ListaSzabad(string Cmbtelephely, DateTime Dátum)
        {
            try
            {
                AdatokSzabad.Clear();

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\Szatubecs\{Dátum.Year}Szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg = "SELECT * FROM szabadság";

                AdatokSzabad = KézSzabad.Lista_Adatok(hely, jelszó, szöveg);
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

        private void ListaTúlóra(string Cmbtelephely, DateTime Dátum)
        {
            try
            {
                AdatokTúlóra.Clear();

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\Szatubecs\{Dátum.Year}Szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg = $"SELECT * FROM Túlóra ";

                AdatokTúlóra = KézTúlóra.Lista_Adatok(hely, jelszó, szöveg);
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

        private void ListaAft(string Cmbtelephely, DateTime Dátum)
        {
            try
            {
                AdatokAft.Clear();

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\Szatubecs\{Dátum.Year}Szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg = $"SELECT * FROM AFT ";

                AdatokAft = KézAft.Lista_Adatok(hely, jelszó, szöveg);
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

        private void ListaCsúsztatás(string Cmbtelephely, DateTime Dátum)
        {
            try
            {
                AdatokCsúsztatás.Clear();

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\Szatubecs\{Dátum.Year}Szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg = $"SELECT * FROM csúsztatás";

                AdatokCsúsztatás = KézCsúsztatás.Lista_Adatok(hely, jelszó, szöveg);
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
        #endregion

        #region Rögzítések
        public void Rögzít_BEO(string Cmbtelephely, DateTime Dátum, string Beosztáskód, string ElőzőBeosztásKód, string HR_Azonosító, int Ledolgozott, string Dolgozónév)
        {
            string szöveg = "Nincs hiba";
            try
            {

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Beosztás\{Dátum.Year}\Ebeosztás{Dátum:yyyyMM}.mdb";

                string jelszó = "kiskakas";
                szöveg = $"SELECT * FROM Beosztás WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND nap=#{Dátum:MM-dd-yyyy}#";

                Kezelő_Dolgozó_Beosztás_Új Kéz = new Kezelő_Dolgozó_Beosztás_Új();
                Adat_Dolgozó_Beosztás_Új Rekord_Old = Kéz.Egy_Adat(hely, jelszó, szöveg);

                string szabiok = Beosztáskód.ToUpper().Contains("SZ") ? "Normál kivétel" : "";

                string AFTok = "";
                int AFTóra = 0;
                if (Beosztáskód.Length > 0 && Beosztáskód.Substring(0, 1) == "A")
                {
                    AFTok = "Nincs kitöltve";
                    AFTóra = Ledolgozott;
                }

                DateTime Túlórakezd = new DateTime(1900, 1, 1, 0, 0, 0);
                DateTime Túlóravég = new DateTime(1900, 1, 1, 0, 0, 0);
                int Túlóra = 0;
                if (Beosztáskód.Length > 0 && (Beosztáskód.Contains("NE") || Beosztáskód.Contains("ÉE")))
                {
                    Adat_Kiegészítő_Beosztáskódok ELEM = Beosztás_Adatok(Cmbtelephely, Beosztáskód);
                    if (ELEM != null)
                    {
                        Túlórakezd = ELEM.Munkaidőkezdet;
                        Túlóravég = ELEM.Munkaidővége;
                        Túlóra = ELEM.Munkaidő;
                    }
                }

                if (Rekord_Old == null)
                {
                    szöveg = "INSERT INTO beosztás (Dolgozószám, Nap, Beosztáskód, Ledolgozott, Túlóra, Túlórakezd, Túlóravég, Csúszóra, CSúszórakezd, Csúszóravég, Megjegyzés, Túlóraok, Szabiok, Kért, Csúszok, AFTóra, AFTok)";
                    szöveg += " VALUES (";
                    szöveg += $"'{HR_Azonosító.Trim()}',";// Dolgozószám
                    szöveg += $"'{Dátum:yyyy.MM.dd}', ";// Nap
                    szöveg += $"'{Beosztáskód.Trim()}', ";// Beosztáskód
                    szöveg += $"{Ledolgozott}, ";// Ledolgozott
                    szöveg += $"{Túlóra}, ";// Túlóra
                    szöveg += $"'{Túlórakezd}', ";// Túlórakezd
                    szöveg += $"'{Túlóravég}', ";// Túlóravég
                    szöveg += $"0, ";// Csúszóra
                    szöveg += $"'1900.01.01. 00:00:00', ";// CSúszórakezd
                    szöveg += $"'1900.01.01. 00:00:00', ";// Csúszóravég
                    szöveg += $"'', ";// Megjegyzés
                    szöveg += $"'_', ";// Túlóraok
                    szöveg += $"'{szabiok}', ";// Szabiok
                    szöveg += $"false, ";// Kért
                    szöveg += $"'_', ";// Csúszok
                    szöveg += $"{AFTóra}, ";// AFTóra
                    szöveg += $"'{AFTok}' ";// AFTok
                    szöveg += ")";
                }
                else
                {
                    szöveg = "UPDATE beosztás SET ";
                    szöveg += $"Beosztáskód='{Beosztáskód.Trim()}', ";// Beosztáskód
                    szöveg += $" Ledolgozott={Ledolgozott}, ";// Ledolgozott
                    szöveg += $"Túlóra={Túlóra}, ";// Túlóra
                    szöveg += $"Túlórakezd='{Túlórakezd}', ";// Túlórakezd
                    szöveg += $"Túlóravég='{Túlóravég}', ";// Túlóravég
                    szöveg += $"Csúszóra=0, ";// Csúszóra
                    szöveg += $"CSúszórakezd='1900.01.01. 00:00:00', ";// CSúszórakezd
                    szöveg += $"Csúszóravég='1900.01.01. 00:00:00', ";// Csúszóravég
                    szöveg += $"Megjegyzés='', ";// Megjegyzés
                    szöveg += $"Túlóraok='_', ";// Túlóraok
                    szöveg += $"Szabiok='{szabiok}', ";// Szabiok
                    szöveg += $"Kért=false, ";// Kért
                    szöveg += $"Csúszok='_', ";// Csúszok
                    szöveg += $"AFTóra={AFTóra}, ";// AFTóra
                    szöveg += $"AFTok='{AFTok}' ";// AFTok
                    szöveg += $" WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND nap=#{Dátum:MM-dd-yyyy}#";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);


                szöveg = $"SELECT * FROM Beosztás WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND nap=#{Dátum:MM-dd-yyyy}#";
                Adat_Dolgozó_Beosztás_Új Rekord_Új = Kéz.Egy_Adat(hely, jelszó, szöveg);

                if (Beosztáskód.Length > 0 && Beosztáskód.Substring(0, 1) == "A")
                    AFT_Átírás(Cmbtelephely, Dátum, Rekord_Új, Dolgozónév);

                if (Beosztáskód.Length > 0 && (Beosztáskód.Contains("NE") || Beosztáskód.Contains("ÉE")))
                    Túlóra_Átírás(Cmbtelephely, Dátum, Rekord_Új, Dolgozónév);

                if (Beosztáskód.Length > 1 && Beosztáskód.Substring(0, 2) == "SZ")
                    Szabadság_Átírás(Cmbtelephely, Dátum, Rekord_Új, Dolgozónév);

                if (Beosztáskód.Length > 0 && Beosztáskód.Substring(0, 1) == "B")
                    Beteg_Átírás(Cmbtelephely, Dátum, Rekord_Új, Dolgozónév);

                Rekord_Új = Kéz.Egy_Adat(hely, jelszó, szöveg);
                Naplózás(Cmbtelephely, Rekord_Új, Dolgozónév);

                Ellenőrzés_Csúsztatás(Cmbtelephely, Dátum, HR_Azonosító);

                if (ElőzőBeosztásKód.Length > 0 && ElőzőBeosztásKód.Substring(0, 1) == "A")
                {
                    Aft_Törlés(Cmbtelephely, Dátum, Rekord_Új);
                    Ellenőrzés_Aft(Cmbtelephely, Dátum, HR_Azonosító);
                }

                if (ElőzőBeosztásKód.Length > 0 && (ElőzőBeosztásKód.Contains("NE") || ElőzőBeosztásKód.Contains("ÉE")))
                {
                    Túlóra_Törlés(Cmbtelephely, Dátum, Rekord_Új);
                    Ellenőrzés_Túlóra(Cmbtelephely, Dátum, HR_Azonosító);
                }

                if (ElőzőBeosztásKód.Length > 0 && ElőzőBeosztásKód.Substring(0, 1) == "B")
                {
                    Beteg_Törlés(Cmbtelephely, Dátum, Rekord_Új);
                    Ellenőrzés_Beteg(Cmbtelephely, Dátum, HR_Azonosító);
                }

                if (ElőzőBeosztásKód.Length > 1 && ElőzőBeosztásKód.Substring(0, 2) == "SZ")
                {
                    Szabadság_Törlés(Cmbtelephely, Dátum, Rekord_Új);
                    Ellenőrzés_Szabadság(Cmbtelephely, Dátum, HR_Azonosító);
                }

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, szöveg, ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Adat_Kiegészítő_Beosztáskódok Beosztás_Adatok(string cmbtelephely, string beosztáskód)
        {
            Adat_Kiegészítő_Beosztáskódok válasz = null;

            try
            {
                string hely = $@"{Application.StartupPath}\{cmbtelephely.Trim()}\Adatok\Segéd\kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg = $"SELECT * FROM Beosztáskódok WHERE beosztáskód='{beosztáskód}'";


                Kezelő_Kiegészítő_Beosztáskódok Kéz = new Kezelő_Kiegészítő_Beosztáskódok();
                válasz = Kéz.Egy_Adat(hely, jelszó, szöveg);

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
            return válasz;
        }

        private void Naplózás(string Cmbtelephely, Adat_Dolgozó_Beosztás_Új Rekord, string dolgozónév)
        {
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\naplózás\{DateTime.Now:yyyyMM}napló.mdb";
            string jelszó = "kerekeskút";
            string szöveg;
            if (!Exists(hely)) Adatbázis_Létrehozás.Beosztás_Naplózása(hely);
            try
            {
                ListaNapló(Cmbtelephely);
                Adat_Dolgozó_Beosztás_Napló AdatNapló = (from a in AdatokNapló
                                                         orderby a.Sorszám descending
                                                         select a).FirstOrDefault();
                double sorszám = 1;
                if (AdatNapló != null) sorszám = AdatNapló.Sorszám + 1;

                szöveg = "INSERT INTO adatok (Sorszám, Dátum, Beosztáskód, Túlóra, Túlórakezd, Túlóravég, Csúszóra, CSúszórakezd, Csúszóravég, Megjegyzés,";
                szöveg += " Túlóraok, Szabiok, Kért, Csúszok, Rögzítette, rögzítésdátum, dolgozónév, Törzsszám,AFTóra, AFTok )";
                szöveg += " VALUES (";
                szöveg += $"'{sorszám}', ";// Sorszám
                szöveg += $"'{Rekord.Nap}', ";// Dátum
                szöveg += $"'{Rekord.Beosztáskód.Trim()}', ";// Beosztáskód
                szöveg += $"{Rekord.Túlóra}, ";// Túlóra
                szöveg += $"'{Rekord.Túlórakezd}', ";// Túlórakezd
                szöveg += $"'{Rekord.Túlóravég}', ";// Túlóravég
                szöveg += $"{Rekord.Csúszóra}, ";// Csúszóra
                szöveg += $"'{Rekord.CSúszórakezd}', ";// CSúszórakezd
                szöveg += $"'{Rekord.Csúszóravég}', ";// Csúszóravég
                szöveg += $"'{Rekord.Megjegyzés.Trim()}', ";// Megjegyzés
                szöveg += $"'{Rekord.Túlóraok.Trim()}', ";// Túlóraok
                szöveg += $"'{Rekord.Szabiok.Trim()}', ";// Szabiok
                szöveg += $"{Rekord.Kért}, ";// Kért
                szöveg += $"'{Rekord.Csúszok.Trim()}', ";// Csúszok
                szöveg += $"'{Program.PostásNév.Trim()}', ";// Rögzítette
                szöveg += $"'{DateTime.Now}', ";// rögzítésdátum
                szöveg += $"'{dolgozónév.Trim()}',";// dolgozónév
                szöveg += $"'{Rekord.Dolgozószám.Trim()}',";// Törzsszám
                szöveg += $"{Rekord.AFTóra}, ";// AFTóra
                szöveg += $"'{Rekord.AFTok.Trim()}' ";// AFTok
                szöveg += ")";
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

        public void Naplózás(string Cmbtelephely, string Művelet)
        {
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\naplózás\{DateTime.Now:yyyyMM}napló.mdb";
            string jelszó = "kerekeskút";
            string szöveg;
            if (!Exists(hely)) Adatbázis_Létrehozás.Beosztás_Naplózása(hely);

            try
            {
                ListaNapló(Cmbtelephely);

                Adat_Dolgozó_Beosztás_Napló AdatNapló = (from a in AdatokNapló
                                                         orderby a.Sorszám descending
                                                         select a).FirstOrDefault();
                double sorszám = 1;
                if (AdatNapló != null) sorszám = AdatNapló.Sorszám + 1;

                szöveg = "INSERT INTO adatok (Sorszám, Dátum, Beosztáskód, Túlóra, Túlórakezd, Túlóravég, Csúszóra, CSúszórakezd, Csúszóravég, Megjegyzés,";
                szöveg += " Túlóraok, Szabiok, Kért, Csúszok, Rögzítette, rögzítésdátum, dolgozónév, Törzsszám,AFTóra, AFTok )";
                szöveg += " VALUES (";
                szöveg += $"'{sorszám}', ";// Sorszám
                szöveg += $"'{DateTime.Today:yyyy.MM.dd}', ";// Dátum
                szöveg += $"'{000}', ";// Beosztáskód
                szöveg += $"{0}, ";// Túlóra
                szöveg += $"'{new DateTime(1900, 1, 1, 0, 0, 0)}', ";// Túlórakezd
                szöveg += $"'{new DateTime(1900, 1, 1, 0, 0, 0)}', ";// Túlóravég
                szöveg += $"{0}, ";// Csúszóra
                szöveg += $"'{new DateTime(1900, 1, 1, 0, 0, 0)}', ";// CSúszórakezd
                szöveg += $"'{new DateTime(1900, 1, 1, 0, 0, 0)}', ";// Csúszóravég
                szöveg += $"'{Művelet}', ";// Megjegyzés
                szöveg += $"'{0}', ";// Túlóraok
                szöveg += $"'{0000}', ";// Szabiok
                szöveg += $"{false}, ";// Kért
                szöveg += $"'{0000}', ";// Csúszok
                szöveg += $"'{Program.PostásNév.Trim()}', ";// Rögzítette
                szöveg += $"'{DateTime.Now}', ";// rögzítésdátum
                szöveg += $"'{Művelet}',";// dolgozónév
                szöveg += $"'{000000}',";// Törzsszám
                szöveg += $"{0}, ";// AFTóra
                szöveg += $"'{0000}' ";// AFTok
                szöveg += ")";
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

        public void Rögzít_Megjegyzés(string Cmbtelephely, DateTime Dátum, string HR_Azonosító, string Megjegyzés, bool Kért, string Dolgozónév)
        {
            string szöveg = "Nincs hiba";
            try
            {

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Beosztás\{Dátum.Year}\Ebeosztás{Dátum:yyyyMM}.mdb";

                string jelszó = "kiskakas";
                szöveg = $"SELECT * FROM Beosztás WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND nap=#{Dátum:MM-dd-yyyy}#";

                Kezelő_Dolgozó_Beosztás_Új Kéz = new Kezelő_Dolgozó_Beosztás_Új();
                Adat_Dolgozó_Beosztás_Új Rekord = Kéz.Egy_Adat(hely, jelszó, szöveg);

                string Beosztáskód = "";
                string AFTok = "";
                int AFTóra = 0;
                int Ledolgozott = 0;
                string szabiok = "";


                if (Rekord == null)
                {
                    szöveg = "INSERT INTO beosztás (Dolgozószám, Nap, Beosztáskód, Ledolgozott, Túlóra, Túlórakezd, Túlóravég, Csúszóra, CSúszórakezd, Csúszóravég, Megjegyzés, Túlóraok, Szabiok, Kért, Csúszok, AFTóra, AFTok)";
                    szöveg += " VALUES (";
                    szöveg += $"'{HR_Azonosító.Trim()}',";// Dolgozószám
                    szöveg += $"'{Dátum:yyyy.MM.dd}', ";// Nap
                    szöveg += $"'{Beosztáskód.Trim()}', ";// Beosztáskód
                    szöveg += $"{Ledolgozott}, ";// Ledolgozott
                    szöveg += $"0, ";// Túlóra
                    szöveg += $"'1900.01.01. 00:00:00', ";// Túlórakezd
                    szöveg += $"'1900.01.01. 00:00:00', ";// Túlóravég
                    szöveg += $"0, ";// Csúszóra
                    szöveg += $"'1900.01.01. 00:00:00', ";// CSúszórakezd
                    szöveg += $"'1900.01.01. 00:00:00', ";// Csúszóravég
                    szöveg += $"'{Megjegyzés.Trim()}', ";// Megjegyzés
                    szöveg += $"'_', ";// Túlóraok
                    szöveg += $"'{szabiok}', ";// Szabiok
                    szöveg += $"{Kért}, ";// Kért
                    szöveg += $"'_', ";// Csúszok
                    szöveg += $"{AFTóra}, ";// AFTóra
                    szöveg += $"'{AFTok}' ";// AFTok
                    szöveg += ")";
                }
                else
                {
                    szöveg = "UPDATE beosztás SET ";
                    szöveg += $"Megjegyzés='{Megjegyzés.Trim()}', ";// Megjegyzés
                    szöveg += $"Kért={Kért} ";// Kért

                    szöveg += $" WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND nap=#{Dátum:MM-dd-yyyy}#";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);

                szöveg = $"SELECT * FROM Beosztás WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND nap=#{Dátum:MM-dd-yyyy}#";
                Rekord = Kéz.Egy_Adat(hely, jelszó, szöveg);
                Naplózás(Cmbtelephely, Rekord, Dolgozónév);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, szöveg, ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Rögzít_Szabadság(string Cmbtelephely, DateTime Dátum, string Beosztáskód, string HR_Azonosító, int Ledolgozott, string Szabiok, string Dolgozónév)
        {
            string szöveg = "Nincs hiba";
            try
            {

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Beosztás\{Dátum.Year}\Ebeosztás{Dátum:yyyyMM}.mdb";

                string jelszó = "kiskakas";
                szöveg = $"SELECT * FROM Beosztás WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND nap=#{Dátum:MM-dd-yyyy}#";

                Kezelő_Dolgozó_Beosztás_Új Kéz = new Kezelő_Dolgozó_Beosztás_Új();
                Adat_Dolgozó_Beosztás_Új Rekord = Kéz.Egy_Adat(hely, jelszó, szöveg);


                if (Rekord == null)
                {
                    szöveg = "INSERT INTO beosztás (Dolgozószám, Nap, Beosztáskód, Ledolgozott, Túlóra, Túlórakezd, Túlóravég, Csúszóra, CSúszórakezd, Csúszóravég, Megjegyzés, Túlóraok, Szabiok, Kért, Csúszok, AFTóra, AFTok)";
                    szöveg += " VALUES (";
                    szöveg += $"'{HR_Azonosító.Trim()}',";// Dolgozószám
                    szöveg += $"'{Dátum:yyyy.MM.dd}', ";// Nap
                    szöveg += $"'{Beosztáskód.Trim()}', ";// Beosztáskód
                    szöveg += $"{Ledolgozott}, ";// Ledolgozott
                    szöveg += $"0, ";// Túlóra
                    szöveg += $"'1900.01.01. 00:00:00', ";// Túlórakezd
                    szöveg += $"'1900.01.01. 00:00:00', ";// Túlóravég
                    szöveg += $"0, ";// Csúszóra
                    szöveg += $"'1900.01.01. 00:00:00', ";// CSúszórakezd
                    szöveg += $"'1900.01.01. 00:00:00', ";// Csúszóravég
                    szöveg += $"'', ";// Megjegyzés
                    szöveg += $"'_', ";// Túlóraok
                    szöveg += $"'{Szabiok}', ";// Szabiok
                    szöveg += $"false, ";// Kért
                    szöveg += $"'_', ";// Csúszok
                    szöveg += $"0, ";// AFTóra
                    szöveg += $"'' ";// AFTok
                    szöveg += ")";
                }
                else
                {
                    szöveg = "UPDATE beosztás SET ";
                    szöveg += $"Szabiok='{Szabiok}' ";// AFTóra
                    szöveg += $" WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND nap=#{Dátum:MM-dd-yyyy}#";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);

                //újra beolvassuk a módosítás/létrehozás utáni állapotot
                szöveg = $"SELECT * FROM Beosztás WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND nap=#{Dátum:MM-dd-yyyy}#";
                Rekord = Kéz.Egy_Adat(hely, jelszó, szöveg);

                Szabadság_Átírás(Cmbtelephely, Dátum, Rekord, Dolgozónév);
                Naplózás(Cmbtelephely, Rekord, Dolgozónév);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, szöveg, ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Beteg
        private void Beteg_Átírás(string Cmbtelephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Rekord_Új, string Dolgozónév)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\Szatubecs\{Dátum.Year}Szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg;

                ListaBeteg(Cmbtelephely, Dátum);
                Adat_Szatube_Beteg AdatBeteg = (from a in AdatokBeteg
                                                where a.Törzsszám == Rekord_Új.Dolgozószám.Trim()
                                                && a.Kezdődátum.ToShortDateString() == Rekord_Új.Nap.ToShortDateString()
                                                && a.Státus != 3
                                                orderby a.Sorszám descending
                                                select a).FirstOrDefault();

                if (AdatBeteg == null)
                {
                    Adat_Szatube_Beteg Elem = (from a in AdatokBeteg
                                               orderby a.Sorszám descending
                                               select a).FirstOrDefault();

                    double sorszám = 1;
                    if (Elem != null) sorszám = Elem.Sorszám + 1;

                    szöveg = "INSERT INTO beteg ";
                    szöveg += "(Sorszám, Törzsszám, Dolgozónév, Kezdődátum, Befejeződátum, Kivettnap, Szabiok, Státus, Rögzítette, Rögzítésdátum) VALUES (";
                    szöveg += $"{sorszám}, ";   //Sorszám
                    szöveg += $"'{Rekord_Új.Dolgozószám.Trim()}', "; //törzsszám
                    szöveg += $"'{Dolgozónév.Trim()}', "; //dolgozónév
                    szöveg += $"'{Rekord_Új.Nap:yyyy.MM.dd}', "; //Kezdődátum
                    szöveg += $"'{Rekord_Új.Nap:yyyy.MM.dd}', "; //Befejeződátum
                    szöveg += $"1, ";   //Kivettnap
                    szöveg += $"'', "; //Szabiok
                    szöveg += $"0, ";   //Státus
                    szöveg += $"'{Program.PostásNév.Trim()}', "; //rögzítette
                    szöveg += $"'{DateTime.Now}') "; //rögzítésdátum
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

        private void Beteg_Törlés(string Cmbtelephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Rekord_Új)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\Szatubecs\{Dátum.Year}Szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg;

                ListaBeteg(Cmbtelephely, Dátum);
                Adat_Szatube_Beteg AdatBeteg = (from a in AdatokBeteg
                                                where a.Törzsszám == Rekord_Új.Dolgozószám.Trim()
                                                && a.Kezdődátum.ToShortDateString() == Rekord_Új.Nap.ToShortDateString()
                                                && a.Státus != 3
                                                select a).FirstOrDefault();

                if (AdatBeteg != null)
                {
                    szöveg = "UPDATE Beteg SET ";
                    szöveg += $"Státus=3 ";   //Státus
                    szöveg += $" WHERE törzsszám='{Rekord_Új.Dolgozószám.Trim()}' AND [Kezdődátum]=#{Rekord_Új.Nap:M-d-yy}# AND [státus]<>3";
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

        public void Ellenőrzés_Beteg(string Cmbtelephely, DateTime Dátum, string HR_Azonosító)
        {
            try
            {
                string helydolg = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Dolgozók.mdb";
                string jelszódolg = "forgalmiutasítás";

                Kezelő_Dolgozó_Alap KézDolg = new Kezelő_Dolgozó_Alap();
                Adat_Dolgozó_Alap Adat_Dolg;


                DateTime hónapelső = MyF.Hónap_elsőnapja(Dátum);
                DateTime hónaputolsó = MyF.Hónap_utolsónapja(Dátum);

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\Szatubecs\{Dátum.Year}Szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg = $"SELECT * FROM Beteg WHERE törzsszám='{HR_Azonosító.Trim()}' AND ";
                szöveg += $"[Kezdődátum]>=#{hónapelső:MM-dd-yyyy}# AND [Kezdődátum]<=#{hónaputolsó:MM-dd-yyyy}# AND [státus]<>3";

                Kezelő_Szatube_Beteg Kéz_SZA = new Kezelő_Szatube_Beteg();
                List<Adat_Szatube_Beteg> Adatok_SZA = Kéz_SZA.Lista_Adatok(hely, jelszó, szöveg);

                string helybeo = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Beosztás\{Dátum.Year}\Ebeosztás{Dátum:yyyyMM}.mdb";
                string jelszóbeo = "kiskakas";
                szöveg = $"SELECT * FROM Beosztás WHERE Dolgozószám='{HR_Azonosító.Trim()}'";

                Kezelő_Dolgozó_Beosztás_Új Kéz = new Kezelő_Dolgozó_Beosztás_Új();
                List<Adat_Dolgozó_Beosztás_Új> Adatok_Beo = Kéz.Lista_Adatok(helybeo, jelszóbeo, szöveg);

                string BeoKód;
                //Megkeressük a beosztás táblában a SZATUBE-ben tároltat, ha nem létezik akkor töröltre állítjuk
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Szatube_Beteg rekord in Adatok_SZA)
                {
                    BeoKód = (from a in Adatok_Beo
                              where rekord.Kezdődátum.ToString("yyyy.MM.dd") == a.Nap.ToString("yyyy.MM.dd")
                              select a.Beosztáskód).FirstOrDefault();

                    if (BeoKód.Length > 0 && BeoKód.Substring(0, 1) != "B")
                    {

                        szöveg = "UPDATE Beteg SET ";
                        szöveg += $" Státus=3 ";   //Státus
                        szöveg += $" WHERE törzsszám='{HR_Azonosító.Trim()}' AND [Kezdődátum]=#{rekord.Kezdődátum:M-d-yy}# AND [státus]<>3";
                        SzövegGy.Add(szöveg);
                    }
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
                // leellenőrizzük, hogy a beosztás táblában létezik és ha SZATUBE nem létezik akkor rögzítjük.
                szöveg = $"SELECT * FROM Beteg WHERE törzsszám='{HR_Azonosító.Trim()}' AND ";
                szöveg += $"[Kezdődátum]>=#{hónapelső:M-d-yy}# AND [Kezdődátum]<=#{hónaputolsó:M-d-yy}# AND [státus]<>3";
                Adatok_SZA = Kéz_SZA.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Dolgozó_Beosztás_Új rekord in Adatok_Beo)
                {
                    if (rekord.Beosztáskód.Length > 0 && rekord.Beosztáskód.Substring(0, 1) == "B")
                    {
                        szöveg = $"SELECT * FROM Dolgozóadatok WHERE Dolgozószám='{HR_Azonosító}'";
                        Adat_Dolg = KézDolg.Egy_Adat(helydolg, jelszódolg, szöveg);
                        Beteg_Átírás(Cmbtelephely, Dátum, rekord, Adat_Dolg.DolgozóNév.Trim());
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
        #endregion

        #region Szabadság
        private void Szabadság_Átírás(string Cmbtelephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Rekord_Új, string Dolgozónév)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\Szatubecs\{Dátum.Year}Szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg = $"SELECT * FROM Szabadság ";
                szöveg += $" WHERE törzsszám='{Rekord_Új.Dolgozószám.Trim()}' AND [Kezdődátum]=#{Rekord_Új.Nap:M-d-yy}# AND [státus]<>3";


                Kezelő_Szatube_Szabadság Kéz = new Kezelő_Szatube_Szabadság();
                Adat_Szatube_Szabadság Adat = Kéz.Egy_Adat(hely, jelszó, szöveg);

                if (Adat == null)
                {
                    //Sorszám azért nulla, hogy ezen a számra gyűjtük össze a szabadságokat
                    szöveg = "INSERT INTO Szabadság ";
                    szöveg += "(Sorszám, Törzsszám, Dolgozónév, Kezdődátum, Befejeződátum, Kivettnap, Szabiok, Státus, Rögzítette, Rögzítésdátum) VALUES (";
                    szöveg += $"0, ";   //Sorszám
                    szöveg += $"'{Rekord_Új.Dolgozószám.Trim()}', "; //törzsszám
                    szöveg += $"'{Dolgozónév.Trim()}', "; //dolgozónév
                    szöveg += $"'{Rekord_Új.Nap:yyyy.MM.dd}', "; //Kezdődátum
                    szöveg += $"'{Rekord_Új.Nap:yyyy.MM.dd}', "; //Befejeződátum
                    szöveg += $"1, ";   //Kivettnap
                    szöveg += $"'{Rekord_Új.Szabiok.Trim()}', "; //Szabiok
                    szöveg += $"0, ";   //Státus
                    szöveg += $"'{Program.PostásNév.Trim()}', "; //rögzítette
                    szöveg += $"'{DateTime.Now}') "; //rögzítésdátum
                }
                else
                {
                    szöveg = $"UPDATE szabadság SET szabiok='{Rekord_Új.Szabiok.Trim()}' WHERE törzsszám='{Rekord_Új.Dolgozószám.Trim()}' AND [Kezdődátum]=#{Rekord_Új.Nap:M-d-yy}# AND [státus]<>3";
                }
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

        private void Szabadság_Törlés(string Cmbtelephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Rekord_Új)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\Szatubecs\{Dátum.Year}Szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg;

                ListaSzabad(Cmbtelephely, Dátum);
                Adat_Szatube_Szabadság AdatSzabad = (from a in AdatokSzabad
                                                     where a.Törzsszám == Rekord_Új.Dolgozószám.Trim()
                                                     && a.Kezdődátum.ToShortDateString() == Rekord_Új.Nap.ToShortDateString()
                                                     && a.Státus != 3
                                                     select a).FirstOrDefault();

                if (AdatSzabad != null)
                {
                    if (AdatSzabad.Sorszám != 0)
                    {
                        szöveg = "UPDATE Szabadság SET ";
                        szöveg += $"sorszám=0, Státus=0 ";
                        szöveg += $" WHERE sorszám={AdatSzabad.Sorszám}";
                        MyA.ABMódosítás(hely, jelszó, szöveg);
                    }
                    //Státust állítjuk a törölt elemnél 0-ra
                    szöveg = "UPDATE Szabadság SET ";
                    szöveg += $"Státus=3 ";   //Státus
                    szöveg += $" WHERE törzsszám='{Rekord_Új.Dolgozószám.Trim()}' AND [Kezdődátum]=#{Rekord_Új.Nap:M-d-yy}# AND [státus]<>3";
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

        public void Ellenőrzés_Szabadság(string Cmbtelephely, DateTime Dátum, string HR_Azonosító)
        {
            try
            {
                string helydolg = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Dolgozók.mdb";
                string jelszódolg = "forgalmiutasítás";

                Kezelő_Dolgozó_Alap KézDolg = new Kezelő_Dolgozó_Alap();
                Adat_Dolgozó_Alap Adat_Dolg;


                DateTime hónapelső = MyF.Hónap_elsőnapja(Dátum);
                DateTime hónaputolsó = MyF.Hónap_utolsónapja(Dátum);

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\Szatubecs\{Dátum.Year}Szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg = $"SELECT * FROM Szabadság WHERE törzsszám='{HR_Azonosító.Trim()}' AND ";
                szöveg += $"[Kezdődátum]>=#{hónapelső:MM-dd-yyyy}# AND [Kezdődátum]<=#{hónaputolsó:MM-dd-yyyy}# AND [státus]<>3";

                Kezelő_Szatube_Szabadság Kéz_SZA = new Kezelő_Szatube_Szabadság();
                List<Adat_Szatube_Szabadság> Adatok_SZA = Kéz_SZA.Lista_Adatok(hely, jelszó, szöveg);

                string helybeo = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Beosztás\{Dátum.Year}\Ebeosztás{Dátum:yyyyMM}.mdb";
                string jelszóbeo = "kiskakas";
                szöveg = $"SELECT * FROM Beosztás WHERE Dolgozószám='{HR_Azonosító.Trim()}'";

                Kezelő_Dolgozó_Beosztás_Új Kéz = new Kezelő_Dolgozó_Beosztás_Új();
                List<Adat_Dolgozó_Beosztás_Új> Adatok_Beo = Kéz.Lista_Adatok(helybeo, jelszóbeo, szöveg);

                string BeoKód;
                //Megkeressük a beosztás táblában a SZATUBE-ben tároltat, ha nem létezik akkor töröltre állítjuk
                List<string> szövegGy = new List<string>();
                foreach (Adat_Szatube_Szabadság rekord in Adatok_SZA)
                {
                    BeoKód = (from a in Adatok_Beo
                              where rekord.Kezdődátum.ToString("yyyy.MM.dd") == a.Nap.ToString("yyyy.MM.dd")
                              select a.Beosztáskód).FirstOrDefault();

                    if (BeoKód != null && BeoKód.Length > 1 && BeoKód.Substring(0, 2) != "SZ")
                    {
                        if (rekord.Sorszám != 0)
                        {
                            szöveg = "UPDATE Szabadság SET ";
                            szöveg += $"sorszám=0, Státus=0 ";
                            szöveg += $" WHERE sorszám={rekord.Sorszám}";
                            szövegGy.Add(szöveg);
                        }

                        szöveg = "UPDATE Szabadság SET ";
                        szöveg += $" Státus=3 ";   //Státus
                        szöveg += $" WHERE törzsszám='{HR_Azonosító.Trim()}' AND [Kezdődátum]=#{rekord.Kezdődátum:yyyy-MM-dd}# AND [státus]<>3";
                        szövegGy.Add(szöveg);
                    }
                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);

                // leellenőrizzük, hogy a beosztás táblában létezik és ha SZATUBE nem létezik akkor rögzítjük.
                szöveg = $"SELECT * FROM Szabadság WHERE törzsszám='{HR_Azonosító.Trim()}' AND ";
                szöveg += $"[Kezdődátum]>=#{hónapelső:yyyy-MM-dd} # AND [Kezdődátum]<=# {hónaputolsó:yyyy-MM-dd}# AND [státus]<>3";
                Adatok_SZA = Kéz_SZA.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Dolgozó_Beosztás_Új rekord in Adatok_Beo)
                {

                    if (rekord.Beosztáskód.Length > 1 && rekord.Beosztáskód.Substring(0, 2) == "SZ")
                    {
                        szöveg = $"SELECT * FROM Dolgozóadatok WHERE Dolgozószám='{HR_Azonosító}'";
                        Adat_Dolg = KézDolg.Egy_Adat(helydolg, jelszódolg, szöveg);
                        Szabadság_Átírás(Cmbtelephely, Dátum, rekord, Adat_Dolg.DolgozóNév.Trim());
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
        #endregion

        #region Túlóra
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cmbtelephely">Telephely neve</param>
        /// <param name="Dátum">Rögzítendő nap</param>
        /// <param name="Beosztáskód">Rögzítendő beosztáskód</param>
        /// <param name="HR_Azonosító"></param>
        /// <param name="Ledolgozott"></param>
        /// <param name="Túlórakezd"></param>
        /// <param name="Túlóravég"></param>
        /// <param name="túlóra"></param>
        /// <param name="TúlóraOk"></param>
        /// <param name="Dolgozónév"></param>
        public void Rögzít_Túlóra(string Cmbtelephely, DateTime Dátum, string Beosztáskód, string HR_Azonosító, int Ledolgozott, DateTime Túlórakezd, DateTime Túlóravég, int túlóra, string TúlóraOk, string Dolgozónév)
        {
            string szöveg = "Nincs hiba";
            try
            {

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Beosztás\{Dátum.Year}\Ebeosztás{Dátum:yyyyMM}.mdb";

                string jelszó = "kiskakas";
                szöveg = $"SELECT * FROM Beosztás WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND nap=#{Dátum:MM-dd-yyyy}#";

                Kezelő_Dolgozó_Beosztás_Új Kéz = new Kezelő_Dolgozó_Beosztás_Új();
                Adat_Dolgozó_Beosztás_Új Rekord_Old = Kéz.Egy_Adat(hely, jelszó, szöveg);


                if (Rekord_Old == null)
                {
                    szöveg = "INSERT INTO beosztás (Dolgozószám, Nap, Beosztáskód, Ledolgozott, Túlóra, Túlórakezd, Túlóravég, Csúszóra, CSúszórakezd, Csúszóravég, Megjegyzés, Túlóraok, Szabiok, Kért, Csúszok, AFTóra, AFTok)";
                    szöveg += " VALUES (";
                    szöveg += $"'{HR_Azonosító.Trim()}',";// Dolgozószám
                    szöveg += $"'{Dátum:yyyy.MM.dd}', ";// Nap
                    szöveg += $"'{Beosztáskód.Trim()}', ";// Beosztáskód
                    szöveg += $"{Ledolgozott}, ";// Ledolgozott
                    szöveg += $"{túlóra}, ";// Túlóra
                    szöveg += $"'{Túlórakezd}', ";// Túlórakezd
                    szöveg += $"'{Túlóravég}', ";// Túlóravég
                    szöveg += $"0, ";// Csúszóra
                    szöveg += $"'1900.01.01. 00:00:00', ";// CSúszórakezd
                    szöveg += $"'1900.01.01. 00:00:00', ";// Csúszóravég
                    szöveg += $"'', ";// Megjegyzés
                    szöveg += $"'{TúlóraOk.Trim()}', ";// Túlóraok
                    szöveg += $"'', ";// Szabiok
                    szöveg += $"false, ";// Kért
                    szöveg += $"'_', ";// Csúszok
                    szöveg += $"0, ";// AFTóra
                    szöveg += $"'' ";// AFTok
                    szöveg += ")";
                }
                else
                {
                    szöveg = "UPDATE beosztás SET ";
                    szöveg += $"Túlóra={túlóra}, ";// Túlóra
                    szöveg += $"Túlórakezd='{Túlórakezd}', ";// Túlórakezd
                    szöveg += $"Túlóravég='{Túlóravég}', ";// Túlóravég
                    szöveg += $"Túlóraok='{TúlóraOk.Trim()}' ";// Túlóraok
                    szöveg += $" WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND nap=#{Dátum:MM-dd-yyyy}#";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);

                szöveg = $"SELECT * FROM Beosztás WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND nap=#{Dátum:MM-dd-yyyy}#";
                Adat_Dolgozó_Beosztás_Új Rekord_Új = Kéz.Egy_Adat(hely, jelszó, szöveg);
                Naplózás(Cmbtelephely, Rekord_Új, Dolgozónév);

                if (Rekord_Új.Túlóra != 0)
                    Túlóra_Átírás(Cmbtelephely, Dátum, Rekord_Új, Dolgozónév);
                else
                    Túlóra_Törlés(Cmbtelephely, Dátum, Rekord_Új);

                Ellenőrzés_Túlóra(Cmbtelephely, Dátum, HR_Azonosító);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, szöveg, ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Túlóra_Átírás(string Cmbtelephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Rekord_Új, string Dolgozónév)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\Szatubecs\{Dátum.Year}Szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg;
                ListaTúlóra(Cmbtelephely, Dátum);
                Adat_Szatube_Túlóra AdatTúlóra = (from a in AdatokTúlóra
                                                  where a.Törzsszám == Rekord_Új.Dolgozószám.Trim()
                                                        && a.Kezdődátum.ToShortDateString() == Rekord_Új.Nap.ToShortDateString()
                                                        && a.Státus != 3
                                                  select a).FirstOrDefault();

                if (AdatTúlóra == null)
                {
                    Adat_Szatube_Túlóra Elem = (from a in AdatokTúlóra
                                                orderby a.Sorszám descending
                                                select a).FirstOrDefault();
                    double sorszám = 1;
                    if (Elem != null) sorszám = Elem.Sorszám + 1;

                    szöveg = "INSERT INTO Túlóra ";
                    szöveg += "(Sorszám, Törzsszám, Dolgozónév, Kezdődátum, Befejeződátum, Kivettnap, Szabiok, Státus, Rögzítette, Rögzítésdátum, Kezdőidő, Befejezőidő) VALUES (";
                    szöveg += $"{sorszám}, ";   //Sorszám
                    szöveg += $"'{Rekord_Új.Dolgozószám.Trim()}', "; //törzsszám
                    szöveg += $"'{Dolgozónév.Trim()}', "; //dolgozónév
                    szöveg += $"'{Rekord_Új.Nap:yyyy.MM.dd}', "; //Kezdődátum
                    szöveg += $"'{Rekord_Új.Nap:yyyy.MM.dd}', "; //Befejeződátum
                    szöveg += $"{Rekord_Új.Túlóra}, ";   //Kivettnap
                    szöveg += $"'{Rekord_Új.Túlóraok.Trim()}', "; //Szabiok
                    szöveg += $"0, ";   //Státus
                    szöveg += $"'{Program.PostásNév.Trim()}', "; //rögzítette
                    szöveg += $"'{DateTime.Now}', "; //rögzítésdátum
                    szöveg += $"'{Rekord_Új.Túlórakezd:HH:mm:ss}', "; //Kezdőidő
                    szöveg += $"'{Rekord_Új.Túlóravég:HH:mm:ss}') "; //Befejezőidő
                }
                else
                {
                    if (Rekord_Új.Túlóra == 0)
                    {
                        // ha lenullázzuk akkor a státust állítjuk
                        szöveg = "UPDATE Túlóra SET ";
                        szöveg += $"Státus=3, ";   //Státus
                        szöveg += $" WHERE törzsszám='{Rekord_Új.Dolgozószám.Trim()}' AND [Kezdődátum]=#{Rekord_Új.Nap:M-d-yy}# AND [státus]<>3";
                    }
                    else
                    {
                        // Módosítjuk 
                        szöveg = "UPDATE Túlóra SET ";
                        szöveg += $"törzsszám='{Rekord_Új.Dolgozószám.Trim()}', "; //törzsszám
                        szöveg += $"dolgozónév='{Dolgozónév.Trim()}', "; //dolgozónév
                        szöveg += $"Kezdődátum='{Rekord_Új.Nap:yyyy.MM.dd}', "; //Kezdődátum
                        szöveg += $"Befejeződátum='{Rekord_Új.Nap:yyyy.MM.dd}', "; //Befejeződátum
                        szöveg += $"Kivettnap={Rekord_Új.Túlóra}, ";   //Kivettnap
                        szöveg += $"Szabiok='{Rekord_Új.Túlóraok.Trim()}', "; //Szabiok
                        szöveg += $"rögzítette='{Program.PostásNév.Trim()}', "; //rögzítette
                        szöveg += $"rögzítésdátum='{DateTime.Now}',  "; //rögzítésdátum
                        szöveg += $"Kezdőidő='{Rekord_Új.Túlórakezd:HH:mm:ss}',  "; //Kezdőidő
                        szöveg += $"Befejezőidő='{Rekord_Új.Túlóravég:HH:mm:ss}'  "; //Befejezőidő
                        szöveg += $" WHERE törzsszám='{Rekord_Új.Dolgozószám.Trim()}' AND [Kezdődátum]=#{Rekord_Új.Nap:M-d-yy}# AND [státus]<>3";
                    }
                }
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

        private void Túlóra_Törlés(string Cmbtelephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Rekord_Új)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\Szatubecs\{Dátum.Year}Szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg;

                ListaTúlóra(Cmbtelephely, Dátum);
                Adat_Szatube_Túlóra AdatTúlóra = (from a in AdatokTúlóra
                                                  where a.Törzsszám == Rekord_Új.Dolgozószám.Trim()
                                                        && a.Kezdődátum.ToShortDateString() == Rekord_Új.Nap.ToShortDateString()
                                                        && a.Státus != 3
                                                  select a).FirstOrDefault();

                if (AdatTúlóra != null)
                {
                    szöveg = "UPDATE Túlóra SET ";
                    szöveg += $"Státus=3 ";   //Státus
                    szöveg += $" WHERE törzsszám='{Rekord_Új.Dolgozószám.Trim()}' AND [Kezdődátum]=#{Rekord_Új.Nap:M-d-yy}# AND [státus]<>3";
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

        public void Ellenőrzés_Túlóra(string Cmbtelephely, DateTime Dátum, string HR_Azonosító)
        {
            try
            {
                string helydolg = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Dolgozók.mdb";
                string jelszódolg = "forgalmiutasítás";

                Kezelő_Dolgozó_Alap KézDolg = new Kezelő_Dolgozó_Alap();
                Adat_Dolgozó_Alap Adat_Dolg;


                DateTime hónapelső = MyF.Hónap_elsőnapja(Dátum);
                DateTime hónaputolsó = MyF.Hónap_utolsónapja(Dátum);

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\Szatubecs\{Dátum.Year}Szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg = $"SELECT * FROM Túlóra WHERE törzsszám='{HR_Azonosító.Trim()}' AND ";
                szöveg += $"[Kezdődátum]>=#{hónapelső:MM-dd-yyyy} # AND [Kezdődátum]<=# {hónaputolsó:MM-dd-yyyy}# AND [státus]<>3";

                Kezelő_Szatube_Túlóra Kézcsúsz = new Kezelő_Szatube_Túlóra();
                List<Adat_Szatube_Túlóra> Adatok_Aft = Kézcsúsz.Lista_Adatok(hely, jelszó, szöveg);

                string helybeo = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Beosztás\{Dátum.Year}\Ebeosztás{Dátum:yyyyMM}.mdb";
                string jelszóbeo = "kiskakas";
                szöveg = $"SELECT * FROM Beosztás WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND Túlóra<>0";

                Kezelő_Dolgozó_Beosztás_Új Kéz = new Kezelő_Dolgozó_Beosztás_Új();
                List<Adat_Dolgozó_Beosztás_Új> Adatok_Beo = Kéz.Lista_Adatok(helybeo, jelszóbeo, szöveg);

                int óra;
                //Megkeressük a beosztás táblában a SZATUBE-ben tároltat, ha nem létezik akkor töröltre állítjuk

                List<string> SzövegGy = new List<string>();
                foreach (Adat_Szatube_Túlóra rekord in Adatok_Aft)
                {
                    óra = (from a in Adatok_Beo
                           where rekord.Kezdődátum.ToString("yyyy.MM.dd") == a.Nap.ToString("yyyy.MM.dd")
                           select a.Túlóra).FirstOrDefault();

                    if (rekord.Kivettnap != óra)
                    {

                        szöveg = "UPDATE Túlóra SET ";
                        szöveg += $" Státus=3 ";   //Státus
                        szöveg += $" WHERE törzsszám='{HR_Azonosító.Trim()}' AND [Kezdődátum]=#{rekord.Kezdődátum:M-d-yy}# AND [státus]<>3";
                        SzövegGy.Add(szöveg);
                    }
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
                // leellenőrizzük, hogy a beosztás táblában létezik és ha SZATUBE nem létezik akkor rögzítjük.
                szöveg = $"SELECT * FROM Túlóra WHERE törzsszám='{HR_Azonosító.Trim()}' AND ";
                szöveg += $"[Kezdődátum]>=#{hónapelső:MM-dd-yyyy} # AND [Kezdődátum]<=# {hónaputolsó:MM-dd-yyyy}# AND [státus]<>3";
                Adatok_Aft = Kézcsúsz.Lista_Adatok(hely, jelszó, szöveg);
                foreach (Adat_Dolgozó_Beosztás_Új rekord in Adatok_Beo)
                {
                    óra = (from a in Adatok_Aft
                           where rekord.Nap.ToString("yyyy.MM.dd") == a.Kezdődátum.ToString("yyyy.MM.dd")
                           select a.Kivettnap).FirstOrDefault();
                    if (rekord.Túlóra != óra)
                    {
                        szöveg = $"SELECT * FROM Dolgozóadatok WHERE Dolgozószám='{HR_Azonosító}'";
                        Adat_Dolg = KézDolg.Egy_Adat(helydolg, jelszódolg, szöveg);
                        Túlóra_Átírás(Cmbtelephely, Dátum, rekord, Adat_Dolg.DolgozóNév.Trim());
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

        public int Túlóra_Keret_Ellenőrzés(string Cmbtelephely, DateTime Dátum, string HR_Azonosító)
        {

            int válasz = 0;
            try
            {
                // *****************************************
                // leellenőrizzük, hogy lehet-e még túlóráznia
                // *****************************************
                double Eddigi_TúlÓra = Évestúlóra_Keret_Figyelés(Cmbtelephely, Dátum, HR_Azonosító);


                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Kiegészítő1.mdb";
                string jelszó = "Mocó";
                string szöveg = $"SELECT * FROM túlórakeret  order by határ";

                Kezelő_Kiegészítő_Túlórakeret Kéz = new Kezelő_Kiegészítő_Túlórakeret();
                List<Adat_Kiegészítő_Túlórakeret> Elemek = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                string telephely = (from a in Elemek
                                    where a.Telephely.Trim() == Cmbtelephely.Trim()
                                    select a.Telephely).FirstOrDefault();

                //ha nincs ilyen telephely
                if (telephely == null)
                    szöveg = $"SELECT * FROM túlórakeret WHERE Telephely='_'";
                else
                    szöveg = $"SELECT * FROM túlórakeret WHERE Telephely='{Cmbtelephely.Trim()}'";

                Elemek = Kéz.Lista_Adatok(hely, jelszó, szöveg);


                foreach (Adat_Kiegészítő_Túlórakeret elem in Elemek)
                {
                    if (Eddigi_TúlÓra > elem.Határ * 60 && válasz == 0)
                        válasz = elem.Parancs;
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
            return válasz;
        }

        public double Évestúlóra_Keret_Figyelés(string Cmbtelephely, DateTime Dátum, string Hrazonosító)
        {
            double válasz = 0;
            try
            {
                ListaTúlóra(Cmbtelephely, Dátum);

                List<Adat_Szatube_Túlóra> Adatok = (from a in AdatokTúlóra
                                                    where a.Törzsszám == Hrazonosító.Trim()
                                                    && a.Státus != 3
                                                    select a).ToList();
                if (Adatok != null) válasz = Adatok.Select(a => a.Kivettnap).Sum();
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
            return válasz;
        }
        #endregion

        #region AFT
        public void Rögzít_AFT(string Cmbtelephely, DateTime Dátum, string Beosztáskód, string HR_Azonosító, int Ledolgozott, string AFTok, int AFTóra, string Dolgozónév)
        {
            string szöveg = "Nincs hiba";
            try
            {

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Beosztás\{Dátum.Year}\Ebeosztás{Dátum:yyyyMM}.mdb";

                string jelszó = "kiskakas";
                szöveg = $"SELECT * FROM Beosztás WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND nap=#{Dátum:MM-dd-yyyy}#";

                Kezelő_Dolgozó_Beosztás_Új Kéz = new Kezelő_Dolgozó_Beosztás_Új();
                Adat_Dolgozó_Beosztás_Új Rekord_Old = Kéz.Egy_Adat(hely, jelszó, szöveg);


                if (Rekord_Old == null)
                {
                    szöveg = "INSERT INTO beosztás (Dolgozószám, Nap, Beosztáskód, Ledolgozott, Túlóra, Túlórakezd, Túlóravég, Csúszóra, CSúszórakezd, Csúszóravég, Megjegyzés, Túlóraok, Szabiok, Kért, Csúszok, AFTóra, AFTok)";
                    szöveg += " VALUES (";
                    szöveg += $"'{HR_Azonosító.Trim()}',";// Dolgozószám
                    szöveg += $"'{Dátum:yyyy.MM.dd}', ";// Nap
                    szöveg += $"'{Beosztáskód.Trim()}', ";// Beosztáskód
                    szöveg += $"{Ledolgozott}, ";// Ledolgozott
                    szöveg += $"0, ";// Túlóra
                    szöveg += $"'1900.01.01. 00:00:00', ";// Túlórakezd
                    szöveg += $"'1900.01.01. 00:00:00', ";// Túlóravég
                    szöveg += $"0, ";// Csúszóra
                    szöveg += $"'1900.01.01. 00:00:00', ";// CSúszórakezd
                    szöveg += $"'1900.01.01. 00:00:00', ";// Csúszóravég
                    szöveg += $"'', ";// Megjegyzés
                    szöveg += $"'_', ";// Túlóraok
                    szöveg += $"'', ";// Szabiok
                    szöveg += $"false, ";// Kért
                    szöveg += $"'_', ";// Csúszok
                    szöveg += $"{AFTóra}, ";// AFTóra
                    szöveg += $"'{AFTok}' ";// AFTok
                    szöveg += ")";
                }
                else
                {
                    szöveg = "UPDATE beosztás SET ";
                    szöveg += $"AFTóra={AFTóra}, ";// AFTóra
                    szöveg += $"AFTok='{AFTok}' ";// AFTok
                    szöveg += $" WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND nap=#{Dátum:MM-dd-yyyy}#";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);

                szöveg = $"SELECT * FROM Beosztás WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND nap=#{Dátum:MM-dd-yyyy}#";
                Adat_Dolgozó_Beosztás_Új Rekord_Új = Kéz.Egy_Adat(hely, jelszó, szöveg);
                Naplózás(Cmbtelephely, Rekord_Új, Dolgozónév);

                if (Rekord_Új.AFTóra != 0)
                    AFT_Átírás(Cmbtelephely, Dátum, Rekord_Új, Dolgozónév);
                else
                    Aft_Törlés(Cmbtelephely, Dátum, Rekord_Új);

                Ellenőrzés_Aft(Cmbtelephely, Dátum, HR_Azonosító);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, szöveg, ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AFT_Átírás(string Cmbtelephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Rekord_Új, string Dolgozónév)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\Szatubecs\{Dátum.Year}Szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg;

                ListaAft(Cmbtelephely, Dátum);
                Adat_Szatube_AFT AdatAft = (from a in AdatokAft
                                            where a.Törzsszám == Rekord_Új.Dolgozószám.Trim()
                                            && a.Dátum.ToShortDateString() == Rekord_Új.Nap.ToShortDateString()
                                            && a.Státus != 3
                                            select a).FirstOrDefault();

                if (AdatAft == null)
                {
                    Adat_Szatube_AFT Elem = (from a in AdatokAft
                                             orderby a.Sorszám descending
                                             select a).FirstOrDefault();
                    double sorszám = 1;
                    if (Elem != null) sorszám = Elem.Sorszám + 1;

                    szöveg = "INSERT INTO aft ";
                    szöveg += "(Sorszám, törzsszám, dolgozónév, dátum, AFTóra, AFTok, Státus, rögzítette, rögzítésdátum) VALUES (";
                    szöveg += $"{sorszám}, ";   //Sorszám
                    szöveg += $"'{Rekord_Új.Dolgozószám.Trim()}', "; //törzsszám
                    szöveg += $"'{Dolgozónév.Trim()}', "; //dolgozónév
                    szöveg += $"'{Rekord_Új.Nap:yyyy.MM.dd}', "; //dátum
                    szöveg += $"{Rekord_Új.AFTóra}, ";   //AFTóra
                    szöveg += $"'{Rekord_Új.AFTok.Trim()}', "; //AFTok
                    szöveg += $"0, ";   //Státus
                    szöveg += $"'{Program.PostásNév.Trim()}', "; //rögzítette
                    szöveg += $"'{DateTime.Now}' ) "; //rögzítésdátum
                }
                else
                {
                    if (Rekord_Új.AFTóra == 0)
                    {
                        // ha lenullázzuk akkor a státust állítjuk
                        szöveg = "UPDATE aft SET ";
                        szöveg += $"Státus=3, ";   //Státus
                        szöveg += $" WHERE törzsszám='{Rekord_Új.Dolgozószám.Trim()}' AND [dátum]=#{Rekord_Új.Nap:M-d-yy}# AND [státus]<>3";
                    }
                    else
                    {
                        // Módosítjuk 
                        szöveg = "UPDATE aft SET ";
                        szöveg += $"törzsszám='{Rekord_Új.Dolgozószám.Trim()}', "; //törzsszám
                        szöveg += $"dolgozónév='{Dolgozónév.Trim()}', "; //dolgozónév
                        szöveg += $"dátum='{Rekord_Új.Nap:yyyy.MM.dd}', "; //dátum
                        szöveg += $"AFTóra={Rekord_Új.AFTóra}, ";   //AFTóra
                        szöveg += $"AFTok='{Rekord_Új.AFTok.Trim()}', "; //AFTok
                        szöveg += $"rögzítette='{Program.PostásNév.Trim()}', "; //rögzítette
                        szöveg += $"rögzítésdátum='{DateTime.Now}'  "; //rögzítésdátum
                        szöveg += $" WHERE törzsszám='{Rekord_Új.Dolgozószám.Trim()}' AND [dátum]=#{Rekord_Új.Nap:M-d-yy}# AND [státus]<>3";
                    }
                }
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

        private void Aft_Törlés(string Cmbtelephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Rekord_Új)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\Szatubecs\{Dátum.Year}Szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg;

                ListaAft(Cmbtelephely, Dátum);
                Adat_Szatube_AFT AdatAft = (from a in AdatokAft
                                            where a.Törzsszám == Rekord_Új.Dolgozószám.Trim()
                                            && a.Dátum.ToShortDateString() == Rekord_Új.Nap.ToShortDateString()
                                            && a.Státus != 3
                                            select a).FirstOrDefault();

                if (AdatAft != null)
                {
                    szöveg = "UPDATE aft SET ";
                    szöveg += $"Státus=3 ";   //Státus
                    szöveg += $" WHERE törzsszám='{Rekord_Új.Dolgozószám.Trim()}' AND [dátum]=#{Rekord_Új.Nap:M-d-yy}# AND [státus]<>3";
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

        public void Ellenőrzés_Aft(string Cmbtelephely, DateTime Dátum, string HR_Azonosító)
        {
            try
            {
                string helydolg = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Dolgozók.mdb";
                string jelszódolg = "forgalmiutasítás";

                Kezelő_Dolgozó_Alap KézDolg = new Kezelő_Dolgozó_Alap();
                Adat_Dolgozó_Alap Adat_Dolg;


                DateTime hónapelső = MyF.Hónap_elsőnapja(Dátum);
                DateTime hónaputolsó = MyF.Hónap_utolsónapja(Dátum);

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\Szatubecs\{Dátum.Year}Szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg = $"SELECT * FROM aft WHERE törzsszám='{HR_Azonosító.Trim()}' AND ";
                szöveg += $"[dátum]>=#{hónapelső:M-d-yy}# AND [dátum]<=#{hónaputolsó:M-d-yy}# AND [státus]<>3";

                Kezelő_Szatube_Aft Kézcsúsz = new Kezelő_Szatube_Aft();
                List<Adat_Szatube_AFT> Adatok_Aft = Kézcsúsz.Lista_Adatok(hely, jelszó, szöveg);

                string helybeo = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Beosztás\{Dátum.Year}\Ebeosztás{Dátum:yyyyMM}.mdb";
                string jelszóbeo = "kiskakas";
                szöveg = $"SELECT * FROM Beosztás WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND AFTóra<>0";

                Kezelő_Dolgozó_Beosztás_Új Kéz = new Kezelő_Dolgozó_Beosztás_Új();
                List<Adat_Dolgozó_Beosztás_Új> Adatok_Beo = Kéz.Lista_Adatok(helybeo, jelszóbeo, szöveg);

                int óra;
                //Megkeressük a beosztás táblában a SZATUBE-ben tároltat, ha nem létezik akkor töröltre állítjuk

                List<string> SzövegGy = new List<string>();
                foreach (Adat_Szatube_AFT rekord in Adatok_Aft)
                {
                    óra = (from a in Adatok_Beo
                           where rekord.Dátum.ToString("yyyy.MM.dd") == a.Nap.ToString("yyyy.MM.dd")
                           select a.AFTóra).FirstOrDefault();

                    if (rekord.AFTóra != óra)
                    {

                        szöveg = "UPDATE aft SET ";
                        szöveg += $" Státus=3 ";   //Státus
                        szöveg += $" WHERE törzsszám='{HR_Azonosító.Trim()}' AND [dátum]=#{rekord.Dátum:M-d-yy}# AND [státus]<>3";
                        SzövegGy.Add(szöveg);
                    }
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
                // leellenőrizzük, hogy a beosztás táblában létezik és ha SZATUBE nem létezik akkor rögzítjük.
                szöveg = $"SELECT * FROM aft WHERE törzsszám='{HR_Azonosító.Trim()}' AND ";
                szöveg += $"[dátum]>=#{hónapelső:M-d-yy}# AND [dátum]<=#{hónaputolsó:M-d-yy}# AND [státus]<>3";
                Adatok_Aft = Kézcsúsz.Lista_Adatok(hely, jelszó, szöveg);
                foreach (Adat_Dolgozó_Beosztás_Új rekord in Adatok_Beo)
                {
                    óra = (from a in Adatok_Aft
                           where rekord.Nap.ToString("yyyy.MM.dd") == a.Dátum.ToString("yyyy.MM.dd")
                           select a.AFTóra).FirstOrDefault();
                    if (rekord.AFTóra != óra)
                    {
                        szöveg = $"SELECT * FROM Dolgozóadatok WHERE Dolgozószám='{HR_Azonosító}'";
                        Adat_Dolg = KézDolg.Egy_Adat(helydolg, jelszódolg, szöveg);
                        AFT_Átírás(Cmbtelephely, Dátum, rekord, Adat_Dolg.DolgozóNév.Trim());
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
        #endregion


        #region Csúsztatás
        public void Rögzít_Csúsztatás(string Cmbtelephely, DateTime Dátum, string Beosztáskód, string HR_Azonosító, int Ledolgozott, DateTime CSúszórakezd, DateTime Csúszóravég, int Csúszóra, string Csúszok, string Dolgozónév)
        {
            string szöveg = "Nincs hiba";
            try
            {

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Beosztás\{Dátum.Year}\Ebeosztás{Dátum:yyyyMM}.mdb";

                string jelszó = "kiskakas";
                szöveg = $"SELECT * FROM Beosztás WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND nap=#{Dátum:MM-dd-yyyy}#";

                Kezelő_Dolgozó_Beosztás_Új Kéz = new Kezelő_Dolgozó_Beosztás_Új();
                Adat_Dolgozó_Beosztás_Új Rekord_Old = Kéz.Egy_Adat(hely, jelszó, szöveg);


                if (Rekord_Old == null)
                {
                    szöveg = "INSERT INTO beosztás (Dolgozószám, Nap, Beosztáskód, Ledolgozott, Túlóra, Túlórakezd, Túlóravég, Csúszóra, CSúszórakezd, Csúszóravég, Megjegyzés, Túlóraok, Szabiok, Kért, Csúszok, AFTóra, AFTok)";
                    szöveg += " VALUES (";
                    szöveg += $"'{HR_Azonosító.Trim()}',";// Dolgozószám
                    szöveg += $"'{Dátum:yyyy.MM.dd}', ";// Nap
                    szöveg += $"'{Beosztáskód.Trim()}', ";// Beosztáskód
                    szöveg += $"{Ledolgozott}, ";// Ledolgozott
                    szöveg += $"0, ";// Túlóra
                    szöveg += $"'1900.01.01. 00:00:00', ";// Túlórakezd
                    szöveg += $"'1900.01.01. 00:00:00', ";// Túlóravég
                    szöveg += $"{Csúszóra}, ";// Csúszóra
                    szöveg += $"'{CSúszórakezd}', ";// CSúszórakezd
                    szöveg += $"'{Csúszóravég}', ";// Csúszóravég
                    szöveg += $"'', ";// Megjegyzés
                    szöveg += $"'', ";// Túlóraok
                    szöveg += $"'', ";// Szabiok
                    szöveg += $"false, ";// Kért
                    szöveg += $"'{Csúszok}', ";// Csúszok
                    szöveg += $"0, ";// AFTóra
                    szöveg += $"'' ";// AFTok
                    szöveg += ")";
                }
                else
                {
                    szöveg = "UPDATE beosztás SET ";
                    szöveg += $"Csúszóra={Csúszóra}, ";// Csúszóra
                    szöveg += $"CSúszórakezd='{CSúszórakezd}', ";// CSúszórakezd
                    szöveg += $"Csúszóravég='{Csúszóravég}', ";// Csúszóravég
                    szöveg += $"Csúszok='{Csúszok}' ";// Csúszok
                    szöveg += $" WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND nap=#{Dátum:MM-dd-yyyy}#";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);

                szöveg = $"SELECT * FROM Beosztás WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND nap=#{Dátum:MM-dd-yyyy}#";
                Adat_Dolgozó_Beosztás_Új Rekord_Új = Kéz.Egy_Adat(hely, jelszó, szöveg);

                Naplózás(Cmbtelephely, Rekord_Új, Dolgozónév);
                if (Rekord_Új.Csúszóra != 0)
                    Csúsztatás_Átírás(Cmbtelephely, Dátum, Rekord_Új, Dolgozónév);
                else
                    Csúsztatás_Törlés(Cmbtelephely, Dátum, Rekord_Új);

                Ellenőrzés_Csúsztatás(Cmbtelephely, Dátum, HR_Azonosító);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, szöveg, ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Ellenőrzés_Csúsztatás(string Cmbtelephely, DateTime Dátum, string HR_Azonosító)
        {
            try
            {
                DateTime hónapelső = MyF.Hónap_elsőnapja(Dátum);
                DateTime hónaputolsó = MyF.Hónap_utolsónapja(Dátum);

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\Szatubecs\{Dátum.Year}Szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg = $"SELECT * FROM csúsztatás WHERE törzsszám='{HR_Azonosító.Trim()}' AND ";
                szöveg += $"[kezdődátum]>=#{hónapelső:M-d-yy}# AND [befejeződátum]<=#{hónaputolsó:M-d-yy}# AND [státus]<>3";

                Kezelő_Szatube_Csúsztatás Kézcsúsz = new Kezelő_Szatube_Csúsztatás();
                List<Adat_Szatube_Csúsztatás> AdatokCsúszik = Kézcsúsz.Lista_Adatok(hely, jelszó, szöveg);

                string helybeo = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Beosztás\{Dátum.Year}\Ebeosztás{Dátum:yyyyMM}.mdb";
                string jelszóbeo = "kiskakas";
                szöveg = $"SELECT * FROM Beosztás WHERE Dolgozószám='{HR_Azonosító.Trim()}' AND Csúszóra<>0";

                Kezelő_Dolgozó_Beosztás_Új Kéz = new Kezelő_Dolgozó_Beosztás_Új();
                List<Adat_Dolgozó_Beosztás_Új> Adatok_Beo = Kéz.Lista_Adatok(helybeo, jelszóbeo, szöveg);

                int óra;
                //Megkeressük a beosztás táblában a SZATUBE-ben tároltat, ha nem létezik akkor töröltre állítjuk
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Szatube_Csúsztatás rekord in AdatokCsúszik)
                {
                    óra = (from a in Adatok_Beo
                           where rekord.Kezdődátum.ToString("yyyy.MM.dd") == a.Nap.ToString("yyyy.MM.dd")
                           select a.Csúszóra).FirstOrDefault();

                    if (rekord.Kivettnap != óra)
                    {

                        szöveg = "UPDATE csúsztatás SET ";
                        szöveg += $" Státus=3 ";   //Státus
                        szöveg += $" WHERE törzsszám='{HR_Azonosító.Trim()}' AND [kezdődátum]=#{rekord.Kezdődátum:M-d-yy}# AND [státus]<>3";
                        SzövegGy.Add(szöveg);

                    }
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

        private void Csúsztatás_Átírás(string Cmbtelephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Rekord_Új, string Dolgozónév)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\Szatubecs\{Dátum.Year}Szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg;

                ListaCsúsztatás(Cmbtelephely, Dátum);
                Adat_Szatube_Csúsztatás AdatCsúsztatás = (from a in AdatokCsúsztatás
                                                          where a.Törzsszám == Rekord_Új.Dolgozószám.Trim()
                                                          && a.Kezdődátum.ToShortDateString() == Rekord_Új.Nap.ToShortDateString()
                                                          && a.Státus != 3
                                                          select a).FirstOrDefault();

                if (AdatCsúsztatás == null)
                {
                    Adat_Szatube_Csúsztatás Elem = (from a in AdatokCsúsztatás
                                                    orderby a.Sorszám descending
                                                    select a).FirstOrDefault();
                    double sorszám = 1;
                    if (Elem != null) sorszám = Elem.Sorszám + 1;

                    szöveg = "INSERT INTO csúsztatás ";
                    szöveg += "(Sorszám, törzsszám, dolgozónév, kezdődátum, befejeződátum, kivettnap, Szabiok, Státus, rögzítette, rögzítésdátum, kezdőidő, befejezőidő) VALUES (";
                    szöveg += $"{sorszám}, ";   //Sorszám
                    szöveg += $"'{Rekord_Új.Dolgozószám.Trim()}', "; //törzsszám
                    szöveg += $"'{Dolgozónév.Trim()}', "; //dolgozónév
                    szöveg += $"'{Rekord_Új.Nap:yyyy.MM.dd}', "; //kezdődátum
                    szöveg += $"'{Rekord_Új.Nap:yyyy.MM.dd}', "; //befejeződátum
                    szöveg += $"{Rekord_Új.Csúszóra}, ";   //kivettnap
                    szöveg += $"'{Rekord_Új.Csúszok.Trim()}', "; //Szabiok
                    szöveg += $"0, ";   //Státus
                    szöveg += $"'{Program.PostásNév.Trim()}', "; //rögzítette
                    szöveg += $"'{DateTime.Now}', "; //rögzítésdátum
                    szöveg += $"'{Rekord_Új.CSúszórakezd}', "; //kezdőidő
                    szöveg += $"'{Rekord_Új.Csúszóravég}' )"; //befejezőidő
                }
                else
                {
                    if (Rekord_Új.Csúszóra == 0)
                    {
                        // ha lenullázzuk akkor a státust állítjuk
                        szöveg = "UPDATE csúsztatás SET ";
                        szöveg += $"Státus=3, ";   //Státus
                        szöveg += $" WHERE törzsszám='{Rekord_Új.Dolgozószám.Trim()}' AND [kezdődátum]=#{Rekord_Új.Nap:M-d-yy}# AND [státus]<>3";
                    }
                    else
                    {
                        // Módosítjuk 
                        szöveg = "UPDATE csúsztatás SET ";
                        szöveg += $"kivettnap={Rekord_Új.Csúszóra}, ";   //kivettnap
                        szöveg += $"Szabiok='{Rekord_Új.Csúszok.Trim()}', "; //Szabiok
                        szöveg += $"rögzítette='{Program.PostásNév.Trim()}', "; //rögzítette
                        szöveg += $"rögzítésdátum='{DateTime.Now}', "; //rögzítésdátum
                        szöveg += $"kezdőidő='{Rekord_Új.CSúszórakezd}', "; //kezdőidő
                        szöveg += $"befejezőidő='{Rekord_Új.Csúszóravég}'"; //befejezőidő
                        szöveg += $" WHERE törzsszám='{Rekord_Új.Dolgozószám.Trim()}' AND [kezdődátum]=#{Rekord_Új.Nap:M-d-yy}# AND [státus]<>3";
                    }
                }
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

        private void Csúsztatás_Törlés(string Cmbtelephely, DateTime Dátum, Adat_Dolgozó_Beosztás_Új Rekord_Új)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\Szatubecs\{Dátum.Year}Szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg;

                ListaCsúsztatás(Cmbtelephely, Dátum);
                Adat_Szatube_Csúsztatás AdatCsúsztatás = (from a in AdatokCsúsztatás
                                                          where a.Törzsszám == Rekord_Új.Dolgozószám.Trim()
                                                          && a.Kezdődátum.ToShortDateString() == Rekord_Új.Nap.ToShortDateString()
                                                          && a.Státus != 3
                                                          select a).FirstOrDefault();

                if (AdatCsúsztatás != null)
                {
                    szöveg = "UPDATE csúsztatás SET ";
                    szöveg += $"Státus=3, ";   //Státus
                    szöveg += $" WHERE törzsszám='{Rekord_Új.Dolgozószám.Trim()}' AND [kezdődátum]=#{Rekord_Új.Nap:M-d-yy}# AND [státus]<>3";
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
        #endregion
    }
}
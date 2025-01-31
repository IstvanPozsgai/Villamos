﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Utasítás
    {
        readonly string jelszó = "katalin";
        public double Új_utasítás(string Telephely, int Év, string mese)
        {
            double Válasz = 1;
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely.Trim()}\adatok\üzenetek\{Év}utasítás.mdb".KönyvSzerk();
                // ha nem létezik akkor létrehozzuk
                Válasz = Sorszám(Telephely, Év);

                Adat_Utasítás ADAT = new Adat_Utasítás(Válasz, mese, Program.PostásNév.Trim(), DateTime.Now, 0);
                Rögzítés(Telephely, Év, ADAT);
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

        public List<Adat_Utasítás> Lista_Adatok(string Telephely, int Év)
        {
            string hely = $@"{Application.StartupPath}\{Telephely.Trim()}\adatok\üzenetek\{Év}utasítás.mdb".KönyvSzerk();
            string szöveg = "SELECT * FROM üzenetek ORDER BY sorszám desc";
            List<Adat_Utasítás> Adatok = new List<Adat_Utasítás>();
            Adat_Utasítás Adat;

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
                                Adat = new Adat_Utasítás(
                                    rekord["Sorszám"].ToÉrt_Double(),
                                    rekord["Szöveg"].ToStrTrim(),
                                    rekord["Írta"].ToStrTrim(),
                                    rekord["Mikor"].ToÉrt_DaTeTime(),
                                    rekord["Érvényes"].ToÉrt_Double()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public double Rögzítés(string Telephely, int Év, Adat_Utasítás Adat)
        {
            double Válasz = 1;
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely.Trim()}\adatok\üzenetek\{Év}utasítás.mdb".KönyvSzerk();
                Válasz = Sorszám(Telephely, Év);
                string szöveg = "INSERT INTO üzenetek (sorszám, szöveg, írta, mikor, érvényes) VALUES ";
                szöveg += $"({Válasz}, '{Adat.Szöveg}', '{Adat.Írta}', '{Adat.Mikor}', {Adat.Érvényes})";
                MyA.ABMódosítás(hely, jelszó, szöveg);

                Kezelő_utasítás_Olvasás Kéz = new Kezelő_utasítás_Olvasás();
                Kéz.Rögzítés(Telephely, Év, new Adat_utasítás_olvasás(0, Adat.Írta, Válasz, Adat.Mikor, false));
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

        private double Sorszám(string Telephely, int Év)
        {
            double Válasz = 1;
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely.Trim()}\adatok\üzenetek\{Év}utasítás.mdb".KönyvSzerk();
                List<Adat_Utasítás> AdatokÜzenet = Lista_Adatok(Telephely, Év);
                // megkeressük az utolsó sorszámot
                if (AdatokÜzenet != null && AdatokÜzenet.Count > 0) Válasz = AdatokÜzenet.Max(a => a.Sorszám) + 1;
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

        public Adat_Utasítás ElsőOlvasatlan(string Telephely, int Év)
        {
            Adat_Utasítás Válasz = null;
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely.Trim()}\adatok\üzenetek\{Év}utasítás.mdb".KönyvSzerk();
                Kezelő_utasítás_Olvasás Kéz = new Kezelő_utasítás_Olvasás();

                List<Adat_utasítás_olvasás> AdatokOlvasás = Kéz.Lista_Adatok(Telephely, Év);
                AdatokOlvasás = (from a in AdatokOlvasás
                                 where a.Ki == Program.PostásNév.Trim()
                                 orderby a.Sorszám descending
                                 select a).ToList();

                List<Adat_Utasítás> AdatokÜzenet = Lista_Adatok(Telephely, Év);
                AdatokÜzenet = AdatokÜzenet.Where(a => a.Érvényes == 0).OrderByDescending(a => a.Sorszám).ToList();
                foreach (Adat_Utasítás elem in AdatokÜzenet)
                {
                    Adat_utasítás_olvasás Elem = (from a in AdatokOlvasás
                                                  where a.Üzenetid == elem.Sorszám
                                                  select a).FirstOrDefault();
                    if (Elem == null)
                    {
                        Válasz = elem;
                        return Válasz;
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
            return Válasz;
        }

        public void Módosítás(string Telephely, int Év, Adat_Utasítás Adat)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely.Trim()}\adatok\üzenetek\{Év}utasítás.mdb".KönyvSzerk();
                string szöveg = $"UPDATE üzenetek SET érvényes={Adat.Érvényes}, szöveg='{Adat.Szöveg}' WHERE sorszám={Adat.Sorszám}";
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

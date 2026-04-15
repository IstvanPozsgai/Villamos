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
    public class Kezelő_Utasítás
    {
        readonly string jelszó = "katalin";
        string hely;
        readonly string táblanév = "üzenetek";

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely.Trim()}\Adatok\üzenetek\{Év}utasítás.mdb";
            if (Telephely.Trim() != "" && !File.Exists(hely)) Adatbázis_Létrehozás.UtasításadatokTábla(hely.KönyvSzerk());
        }

        public double Új_utasítás(string Telephely, int Év, string mese)
        {
            double Válasz = 1;
            try
            {
                FájlBeállítás(Telephely, Év);
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
            FájlBeállítás(Telephely, Év);
            string szöveg = $"SELECT * FROM {táblanév} ORDER BY sorszám desc";
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
                FájlBeállítás(Telephely, Év);
                Válasz = Sorszám(Telephely, Év);
                string szöveg = $"INSERT INTO {táblanév} (sorszám, szöveg, írta, mikor, érvényes) VALUES ";
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
                FájlBeállítás(Telephely, Év);
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
                FájlBeállítás(Telephely, Év);
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
                FájlBeállítás(Telephely, Év);
                string szöveg = $"UPDATE {táblanév} SET érvényes={Adat.Érvényes}, szöveg='{Adat.Szöveg}' WHERE sorszám={Adat.Sorszám}";
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

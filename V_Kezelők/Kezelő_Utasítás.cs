using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using static System.IO.File;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Utasítás
    {
        readonly string jelszó = "katalin";
        public double Új_utasítás(string hely, string mese)
        {
            double Válasz = 1;
            try
            {
                // ha nem létezik akkor létrehozzuk
                if (!Exists(hely)) Adatbázis_Létrehozás.UtasításadatokTábla(hely);

                Válasz = Sorszám(hely);

                Adat_Utasítás ADAT = new Adat_Utasítás(Válasz, mese, Program.PostásNév.Trim(), DateTime.Now, 0);
                Rögzítés(hely, ADAT);
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

        public List<Adat_Utasítás> Lista_Adatok(string hely)
        {
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

        public List<Adat_Utasítás> Lista_Adatok(string hely, string szöveg)
        {
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

        public double Rögzítés(string hely, Adat_Utasítás Adat)
        {
            double Válasz = 1;
            try
            {
                Válasz = Sorszám(hely);
                string szöveg = "INSERT INTO üzenetek (sorszám, szöveg, írta, mikor, érvényes) VALUES ";
                szöveg += $"({Válasz}, '{Adat.Szöveg}', '{Adat.Írta}', '{Adat.Mikor}', {Adat.Érvényes})";
                MyA.ABMódosítás(hely, jelszó, szöveg);

                Kezelő_utasítás_Olvasás Kéz = new Kezelő_utasítás_Olvasás();
                Kéz.Rögzítés(hely, new Adat_utasítás_olvasás(0, Adat.Írta, Válasz, Adat.Mikor, false));
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

        private double Sorszám(string hely)
        {
            double Válasz = 1;
            try
            {
                List<Adat_Utasítás> AdatokÜzenet = Lista_Adatok(hely);
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

        public Adat_Utasítás ElsőOlvasatlan(string hely)
        {
            Adat_Utasítás Válasz = null;
            try
            {
                Kezelő_utasítás_Olvasás Kéz = new Kezelő_utasítás_Olvasás();
                string szöveg = $"Select * FROM Olvasás WHERE ki='{Program.PostásNév.Trim()}' ORDER BY sorszám DESC";
                List<Adat_utasítás_olvasás> AdatokOlvasás = Kéz.Lista_Adatok(hely,  szöveg);

                szöveg = "Select * FROM Üzenetek WHERE érvényes=0 ORDER BY sorszám DESC";
                List<Adat_Utasítás> AdatokÜzenet = Lista_Adatok(hely, szöveg);
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

        public void Módosítás(string hely, Adat_Utasítás Adat)
        {
            try
            {
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

    public class Kezelő_utasítás_Olvasás
    {
        readonly string jelszó = "katalin";
        public List<Adat_utasítás_olvasás> Lista_Adatok(string hely)
        {
            string szöveg = "SELECT * From olvasás order by sorszám desc";
            List<Adat_utasítás_olvasás> Adatok = new List<Adat_utasítás_olvasás>();
            Adat_utasítás_olvasás Adat;

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
                                Adat = new Adat_utasítás_olvasás(
                                rekord["Sorszám"].ToÉrt_Double(),
                                rekord["ki"].ToStrTrim(),
                                rekord["Üzenetid"].ToÉrt_Double(),
                                rekord["Mikor"].ToÉrt_DaTeTime(),
                                rekord["Olvasva"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_utasítás_olvasás> Lista_Adatok(string hely, string szöveg)
        {
            List<Adat_utasítás_olvasás> Adatok = new List<Adat_utasítás_olvasás>();
            Adat_utasítás_olvasás Adat;

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
                                Adat = new Adat_utasítás_olvasás(
                                rekord["Sorszám"].ToÉrt_Double(),
                                rekord["ki"].ToStrTrim(),
                                rekord["Üzenetid"].ToÉrt_Double(),
                                rekord["Mikor"].ToÉrt_DaTeTime(),
                                rekord["Olvasva"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        private double Sorszám(string hely)
        {
            double Válasz = 1;
            try
            {
                List<Adat_utasítás_olvasás> AdatokÜzenet = Lista_Adatok(hely);
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

        public void Rögzítés(string hely, Adat_utasítás_olvasás Adat)
        {
            try
            {
                string szöveg = "INSERT INTO olvasás (sorszám, ki, üzenetid, mikor, olvasva) VALUES ";
                szöveg += $"({Sorszám(hely)}, '{Adat.Ki}', {Adat.Üzenetid}, '{Adat.Mikor}', {Adat.Olvasva})";
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

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
    public class Kezelő_Üzenet
    {
        readonly string jelszó = "katalin";
        public List<Adat_Üzenet> Lista_Adatok(string hely, string szöveg)
        {
            List<Adat_Üzenet> Adatok = new List<Adat_Üzenet>();
            Adat_Üzenet Adat;

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
                                Adat = new Adat_Üzenet(
                                    rekord["Sorszám"].ToÉrt_Double(),
                                    rekord["Szöveg"].ToStrTrim(),
                                    rekord["Írta"].ToStrTrim(),
                                    rekord["Mikor"].ToÉrt_DaTeTime(),
                                    rekord["válaszsorszám"].ToÉrt_Double()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, Adat_Üzenet Adat)
        {
            try
            {
                if (!Exists(hely)) Adatbázis_Létrehozás.ALÜzenetadatok(hely);
                double id = Sorszám(hely);

                string szöveg = "INSERT INTO üzenetek  (sorszám, szöveg, írta, mikor,válaszsorszám ) VALUES (";
                szöveg += $"{id}, "; // sorszám
                szöveg += $"'{Adat.Szöveg}', "; // szöveg
                szöveg += $"'{Adat.Írta}', ";
                szöveg += $"'{DateTime.Now}', {Adat.Válaszsorszám}) ";  // mikor,válaszsorszám
                MyA.ABMódosítás(hely, jelszó, szöveg);

                Kezelő_Üzenet_Olvas Kéz = new Kezelő_Üzenet_Olvas();
                Adat_Üzenet_Olvasás ADAT = new Adat_Üzenet_Olvasás(0,
                                                   Program.PostásNév.Trim(),
                                                   id,
                                                   DateTime.Now,
                                                   false);
                Kéz.Rögzítés(hely, ADAT);
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

        private double Sorszám(string hely)
        {
            double Válasz = 1;
            try
            {
                string szöveg = "Select * FROM Üzenetek";
                List<Adat_Üzenet> AdatokÜzenet = Lista_Adatok(hely, szöveg);
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

        public Adat_Üzenet ElsőOlvasatlan(string hely)
        {
            Adat_Üzenet Válasz = null;
            try
            {

                Kezelő_Üzenet_Olvas Kéz = new Kezelő_Üzenet_Olvas();
                string szöveg = $"Select * FROM Olvasás WHERE ki='{Program.PostásNév.Trim()}'  ORDER BY sorszám DESC";
                List<Adat_Üzenet_Olvasás> AdatokOlvasás = Kéz.Lista_Adatok(hely, szöveg);

                szöveg = "Select * FROM Üzenetek ORDER BY sorszám DESC";
                List<Adat_Üzenet> AdatokÜzenet = Lista_Adatok(hely, szöveg);
                foreach (Adat_Üzenet elem in AdatokÜzenet)
                {
                    Adat_Üzenet_Olvasás Elem = (from a in AdatokOlvasás
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
    }

    public class Kezelő_Üzenet_Olvas
    {
        readonly string jelszó = "katalin";
        public List<Adat_Üzenet_Olvasás> Lista_Adatok(string hely, string szöveg)
        {
            List<Adat_Üzenet_Olvasás> Adatok = new List<Adat_Üzenet_Olvasás>();
            Adat_Üzenet_Olvasás Adat;

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
                                Adat = new Adat_Üzenet_Olvasás(
                                    rekord["Sorszám"].ToÉrt_Double(),
                                    rekord["ki"].ToStrTrim(),
                                    rekord["üzenetid"].ToÉrt_Double(),
                                    rekord["Mikor"].ToÉrt_DaTeTime(),
                                    rekord["olvasva"].ToÉrt_Bool()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, Adat_Üzenet_Olvasás Adat)
        {
            try
            {

                string szöveg = $"SELECT * FROM olvasás";
                List<Adat_Üzenet_Olvasás> Adatok = Lista_Adatok(hely, szöveg);

                Adat_Üzenet_Olvasás vane = (from a in Adatok
                                            where a.Üzenetid == Adat.Üzenetid 
                                            && a.Ki == Program.PostásNév.Trim()
                                            select a).FirstOrDefault();
                if (vane == null)
                {
                    double i = 1;
                    if (Adatok.Count > 0) i = Adatok.Max(a => a.Sorszám) + 1;

                    szöveg = "INSERT INTO olvasás ";
                    szöveg += "(sorszám, ki, üzenetid, mikor, olvasva)";
                    szöveg += " VALUES ";
                    szöveg += $"({i}, '{Program.PostásNév.Trim()}', {Adat.Üzenetid}, '{DateTime.Now}', -1)";
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

        public void Rögzítés(string hely, List<Adat_Üzenet_Olvasás> AdatokOlvas)
        {
            try
            {
                string szöveg = $"SELECT * FROM olvasás";
                List<Adat_Üzenet_Olvasás> Adatok = Lista_Adatok(hely, szöveg);

                double i = 0;
                if (Adatok.Count > 0) i = Adatok.Max(a => a.Sorszám);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Üzenet_Olvasás item in AdatokOlvas)
                {
                    Adat_Üzenet_Olvasás vane = (from a in Adatok
                                                where a.Üzenetid == item.Üzenetid
                                                && a.Ki == Program.PostásNév.Trim()
                                                select a).FirstOrDefault();
                    if (vane == null)
                    {
                        i++;
                        szöveg = "INSERT INTO olvasás ";
                        szöveg += "(sorszám, ki, üzenetid, mikor, olvasva)";
                        szöveg += " VALUES ";
                        szöveg += $"({i}, '{Program.PostásNév.Trim()}', {item.Üzenetid}, '{DateTime.Now}', -1)";
                        SzövegGy.Add(szöveg);
                    }
                }
                if (SzövegGy != null && SzövegGy.Count > 0) MyA.ABMódosítás(hely, jelszó, SzövegGy);
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

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
    public class Kezelő_Üzenet
    {
        readonly Kezelő_Üzenet_Olvas KézOlvas = new Kezelő_Üzenet_Olvas();

        readonly string jelszó = "katalin";
        string hely;

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely.Trim()}\adatok\üzenetek\{Év}üzenet.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.ALÜzenetadatok(hely.KönyvSzerk());
        }

        public List<Adat_Üzenet> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = $"SELECT * FROM üzenetek ";
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


        public void Rögzítés(string Telephely, int Év, Adat_Üzenet Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                double id = Sorszám(Telephely, Év);

                string szöveg = $"INSERT INTO üzenetek  (sorszám, szöveg, írta, mikor,válaszsorszám ) VALUES (";
                szöveg += $"{id}, "; // sorszám
                szöveg += $"'{Adat.Szöveg}', "; // szöveg
                szöveg += $"'{Adat.Írta}', ";
                szöveg += $"'{DateTime.Now}', {Adat.Válaszsorszám}) ";  // mikor,válaszsorszám
                MyA.ABMódosítás(hely, jelszó, szöveg);

                if (Adat.Írta == Program.PostásNév)
                {
                    Kezelő_Üzenet_Olvas Kéz = new Kezelő_Üzenet_Olvas();
                    Adat_Üzenet_Olvasás ADAT = new Adat_Üzenet_Olvasás(0,
                                                       Program.PostásNév.Trim(),
                                                       id,
                                                       DateTime.Now,
                                                       false);
                    Kéz.Rögzítés(Telephely, Év, ADAT);
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

        private double Sorszám(string Telephely, int Év)
        {
            double Válasz = 1;
            try
            {
                List<Adat_Üzenet> AdatokÜzenet = Lista_Adatok(Telephely, Év);
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

        public Adat_Üzenet ElsőOlvasatlan(string Telephely, int Év)
        {
            Adat_Üzenet Válasz = null;
            try
            {
                FájlBeállítás(Telephely, Év);
                List<Adat_Üzenet_Olvasás> AdatokOlvasás = KézOlvas.Lista_Adatok(Telephely, Év);
                AdatokOlvasás = AdatokOlvasás.Where(a => a.Ki == Program.PostásNév.Trim()).ToList();


                List<Adat_Üzenet> AdatokÜzenet = Lista_Adatok(Telephely, Év).OrderByDescending(a => a.Sorszám).ToList();
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


}

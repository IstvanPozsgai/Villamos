using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{

    public class Kezelő_utasítás_Olvasás
    {
        readonly string jelszó = "katalin";
        public List<Adat_utasítás_olvasás> Lista_Adatok(string Telephely, int Év)
        {
            string hely = $@"{Application.StartupPath}\{Telephely.Trim()}\adatok\üzenetek\{Év}utasítás.mdb".KönyvSzerk();
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

        private double Sorszám(string Telephely, int Év)
        {
            double Válasz = 1;
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely.Trim()}\adatok\üzenetek\{Év}utasítás.mdb".KönyvSzerk();
                List<Adat_utasítás_olvasás> AdatokÜzenet = Lista_Adatok(Telephely, Év);
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

        public void Rögzítés(string Telephely, int Év, Adat_utasítás_olvasás Adat)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely.Trim()}\adatok\üzenetek\{Év}utasítás.mdb".KönyvSzerk();
                string szöveg = "INSERT INTO olvasás (sorszám, ki, üzenetid, mikor, olvasva) VALUES ";
                szöveg += $"({Sorszám(Telephely, Év)}, '{Adat.Ki}', {Adat.Üzenetid}, '{Adat.Mikor}', {Adat.Olvasva})";
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

using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kulcs_Kettő
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Villamos10.mdb";
        readonly string jelszó = "fütyülősbarack";
        readonly string táblanév = "Adattábla";

        public Kezelő_Kulcs_Kettő()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kulcs_Adatok_Kettő(hely.KönyvSzerk());
        }

        public List<Adat_Kulcs> Lista_Adatok()
        {
            List<Adat_Kulcs> Adatok = new List<Adat_Kulcs>();
            Adat_Kulcs Adat;

            string szöveg = $"SELECT * FROM {táblanév}";

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
                                Adat = new Adat_Kulcs(
                                    rekord["adat1"].ToStrTrim(),
                                    rekord["adat2"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzít(List<Adat_Kulcs> Adatok)
        {
            try
            {
                if (Adatok == null) return;
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Kulcs Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév} ";
                    szöveg += "(adat1, adat2) VALUES ";
                    szöveg += $"('{Adat.Adat1}', ";
                    szöveg += $" '{Adat.Adat2}')";
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
                HibaNapló.Log(ex.Message, "Kulcs rögzítés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Rögzít(Adat_Kulcs Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} ";
                szöveg += "(adat1, adat2) VALUES ";
                szöveg += $"('{Adat.Adat1}', ";
                szöveg += $" '{Adat.Adat2}')";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Kulcs rögzítés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosít(List<Adat_Kulcs> Adatok)
        {
            try
            {
                if (Adatok == null) return;
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Kulcs Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév} SET ";
                    szöveg += $" Adat2='{Adat.Adat2}'";
                    szöveg += $" WHERE Adat1 like '%{Adat.Adat1}%'";
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
                HibaNapló.Log(ex.Message, "Kulcs rögzítés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosít(Adat_Kulcs Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $" Adat2='{Adat.Adat2}'";
                szöveg += $" WHERE Adat1 like '%{Adat.Adat1}%'";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Kulcs rögzítés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

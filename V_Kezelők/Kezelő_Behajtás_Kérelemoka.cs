using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Behajtás_Kérelemoka
    {
        readonly string jelszó = "egérpad";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\behajtási\Behajtási_alap.mdb";

        public Kezelő_Behajtás_Kérelemoka()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Alap(hely.KönyvSzerk());
        }

        public List<Adat_Behajtás_Kérelemoka> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM Kérelemoka ORDER BY id";
            List<Adat_Behajtás_Kérelemoka> Adatok = new List<Adat_Behajtás_Kérelemoka>();
            Adat_Behajtás_Kérelemoka Adat;

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
                                Adat = new Adat_Behajtás_Kérelemoka(
                                        rekord["Id"].ToÉrt_Int(),
                                        rekord["Ok"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Behajtás_Kérelemoka Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO Kérelemoka (id, ok) VALUES ({Sorszám()}, '{Adat.Ok}')";
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

        public void Módosítás(Adat_Behajtás_Kérelemoka Adat)
        {
            try
            {
                string szöveg = $"UPDATE kérelemoka SET id={Adat.Id} WHERE ok='{Adat.Ok}'";
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

        private int Sorszám()
        {
            int válasz = 0;
            try
            {
                List<Adat_Behajtás_Kérelemoka> Adatok = Lista_Adatok();
                if (Adatok != null) válasz = Adatok.Max(a => a.Id) + 1;
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

        public void Törlés(Adat_Behajtás_Kérelemoka Adat)
        {
            try
            {
                // ha van alatta kocsi akkor nem engedjük törölni a típust
                string szöveg = $"DELETE FROM Kérelemoka WHERE ok='{Adat.Ok}'";
                MyA.ABtörlés(hely, jelszó, szöveg);
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

        public void Csere(string Elem)
        {
            try
            {
                Adat_Behajtás_Kérelemoka Előző = null;
                List<Adat_Behajtás_Kérelemoka> Adatok = Lista_Adatok();
                foreach (Adat_Behajtás_Kérelemoka Adat in Adatok)
                {
                    if (Adat.Ok == Elem)
                    {
                        Módosítás(new Adat_Behajtás_Kérelemoka(Előző.Id, Adat.Ok));
                        Módosítás(new Adat_Behajtás_Kérelemoka(Adat.Id, Előző.Ok));
                        break;
                    }
                    Előző = Adat;
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
    }
}

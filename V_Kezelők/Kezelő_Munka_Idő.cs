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
    public class Kezelő_Munka_Idő
    {
        readonly string jelszó = "felépítés";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\Munkalap\munkalapösszesítő.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Munkalapkedvencek(hely.KönyvSzerk());
        }

        public List<Adat_Munka_Idő> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            string szöveg = $"SELECT * FROM időválaszték ORDER BY id";
            List<Adat_Munka_Idő> Adatok = new List<Adat_Munka_Idő>();
            Adat_Munka_Idő Adat;

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
                                Adat = new Adat_Munka_Idő(
                                          rekord["ID"].ToÉrt_Long(),
                                          rekord["Idő"].ToÉrt_Long()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, Adat_Munka_Idő Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"INSERT INTO időválaszték (id, idő) VALUES ({Sorszám(hely)},{Adat.Idő})";
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

        public void Módosítás(string Telephely, Adat_Munka_Idő Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE időválaszték SET idő={Adat.Idő} WHERE id={Adat.ID}";
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

        public void Törlés(string Telephely, Adat_Munka_Idő Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"DELETE FROM időválaszték WHERE idő={Adat.Idő}";
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

        private long Sorszám(string Telephely)
        {
            long válasz = 1;
            try
            {
                FájlBeállítás(Telephely);
                List<Adat_Munka_Idő> Adatok = Lista_Adatok(Telephely);
                if (Adatok != null) válasz = Adatok.Max(x => x.ID) + 1;
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

        public void Csere(string Telephely, long Sorelőző, long Sor)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<Adat_Munka_Idő> Adatok = Lista_Adatok(hely);

                Adat_Munka_Idő Előző = (from a in Adatok
                                        where a.Idő == Sorelőző
                                        select a).FirstOrDefault();
                Adat_Munka_Idő Következő = (from a in Adatok
                                            where a.Idő == Sor
                                            select a).FirstOrDefault();
                if (Előző != null && Következő != null)
                {
                    Adat_Munka_Idő ÚjElőző = new Adat_Munka_Idő(Előző.ID, Következő.Idő);
                    Adat_Munka_Idő ÚjKövetkező = new Adat_Munka_Idő(Következő.ID, Előző.Idő);
                    Módosítás(hely, ÚjElőző);
                    Módosítás(hely, ÚjKövetkező);
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

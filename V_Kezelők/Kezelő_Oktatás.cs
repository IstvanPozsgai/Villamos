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
    public class Kezelő_Oktatásrajelöltek
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Főmérnökség_oktatás.mdb";
        readonly string jelszó = "pázmányt";

        public Kezelő_Oktatásrajelöltek()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Oktatás_ALAP(hely.KönyvSzerk());
        }

        public List<Adat_Oktatásrajelöltek> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM oktatásrajelöltek";
            List<Adat_Oktatásrajelöltek> Adatok = new List<Adat_Oktatásrajelöltek>();
            Adat_Oktatásrajelöltek Adat;

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
                                Adat = new Adat_Oktatásrajelöltek(
                                        rekord["HRazonosító"].ToStrTrim(),
                                        rekord["IDoktatás"].ToÉrt_Long(),
                                        rekord["mikortól"].ToÉrt_DaTeTime(),
                                        rekord["Státus"].ToÉrt_Long(),
                                        rekord["telephely"].ToStrTrim()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(List<Adat_Oktatásrajelöltek> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Oktatásrajelöltek Adat in Adatok)
                {
                    string szöveg = "INSERT INTO oktatásrajelöltek (HRazonosító, IDoktatás, Mikortól,  státus,  telephely)";
                    szöveg += $" VALUES ('{Adat.HRazonosító}', ";
                    szöveg += $"{Adat.IDoktatás}, ";
                    szöveg += $"'{Adat.Mikortól:yyyy.MM.dd}', {Adat.Státus},";
                    szöveg += $"'{Adat.Telephely}') ";
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
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void Módosítás_Ütem(List<Adat_Oktatásrajelöltek> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Oktatásrajelöltek Adat in Adatok)
                {
                    string szöveg = $"UPDATE oktatásrajelöltek SET mikortól='{Adat.Mikortól:yyyy.MM.dd}'";
                    szöveg += $" WHERE idoktatás={Adat.IDoktatás}";
                    szöveg += $" and hrazonosító='{Adat.HRazonosító}'";
                    szöveg += $" AND telephely='{Adat.Telephely}'";
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
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosítás_Státus(List<Adat_Oktatásrajelöltek> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Oktatásrajelöltek Adat in Adatok)
                {
                    string szöveg = "UPDATE oktatásrajelöltek SET státus=1 ";
                    szöveg += $" WHERE idoktatás={Adat.IDoktatás}";
                    szöveg += $" and hrazonosító='{Adat.HRazonosító}'";
                    szöveg += $" AND telephely='{Adat.Telephely}'";
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
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosítás_Státus_Dátum(List<Adat_Oktatásrajelöltek> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Oktatásrajelöltek Adat in Adatok)
                {
                    string szöveg = $"UPDATE oktatásrajelöltek SET mikortól='{Adat.Mikortól:yyyy.MM.dd}' ";
                    szöveg += $" WHERE idoktatás={Adat.IDoktatás}";
                    szöveg += $" and hrazonosító='{Adat.HRazonosító}'";
                    szöveg += $" AND telephely='{Adat.Telephely}'";
                    szöveg += $" AND státus={Adat.Státus} ";
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
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosítás_Státus(Adat_Oktatásrajelöltek Adat)
        {
            try
            {
                string szöveg = "UPDATE oktatásrajelöltek SET státus=1 ";
                szöveg += $" WHERE idoktatás={Adat.IDoktatás}";
                szöveg += $" and hrazonosító='{Adat.HRazonosító}'";
                szöveg += $" AND telephely='{Adat.Telephely}'";
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

        public void Módosítás_Státus_Dátum(Adat_Oktatásrajelöltek Adat)
        {
            try
            {
                string szöveg = $"UPDATE oktatásrajelöltek SET mikortól='{Adat.Mikortól:yyyy.MM.dd}' ";
                szöveg += $" WHERE idoktatás={Adat.IDoktatás}";
                szöveg += $" and hrazonosító='{Adat.HRazonosító}'";
                szöveg += $" AND telephely='{Adat.Telephely}'";
                szöveg += $" AND státus={Adat.Státus} ";
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

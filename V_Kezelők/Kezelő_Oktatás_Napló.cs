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

    public class Kezelő_Oktatás_Napló
    {
        string hely;
        readonly string jelszó = "pázmányt";

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Év}\Oktatásnapló_{Telephely}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Oktatás_Napló(hely.KönyvSzerk());
        }

        public List<Adat_Oktatás_Napló> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = "SELECT * FROM oktatásnapló ";
            List<Adat_Oktatás_Napló> Adatok = new List<Adat_Oktatás_Napló>();
            Adat_Oktatás_Napló Adat;

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
                                Adat = new Adat_Oktatás_Napló(
                                    rekord["ID"].ToÉrt_Long(),
                                    rekord["HRazonosító"].ToStrTrim(),
                                    rekord["IDoktatás"].ToÉrt_Long(),
                                    rekord["Oktatásdátuma"].ToÉrt_DaTeTime(),
                                    rekord["Kioktatta"].ToStrTrim(),
                                    rekord["Rögzítésdátuma"].ToÉrt_DaTeTime(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["PDFFájlneve"].ToStrTrim(),
                                    rekord["Számonkérés"].ToÉrt_Long(),
                                    rekord["státus"].ToÉrt_Long(),
                                    rekord["Rögzítő"].ToStrTrim(),
                                    rekord["Megjegyzés"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, int Év, Adat_Oktatás_Napló Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = "INSERT INTO Oktatásnapló";
                szöveg += "( Id, Hrazonosító, IDoktatás, oktatásdátuma, kioktatta, rögzítésdátuma, telephely, PDFfájlneve, Számonkérés, státus, rögzítő, megjegyzés)";
                szöveg += " VALUES (";
                szöveg += $"{Sorszám(Telephely, Év)}, "; //id
                szöveg += $"'{Adat.HRazonosító}', "; //Hrazonosító
                szöveg += $"{Adat.IDoktatás}, "; //IDoktatás
                szöveg += $"'{Adat.Oktatásdátuma}', ";
                szöveg += $"'{Adat.Kioktatta}', ";
                szöveg += $"'{DateTime.Now}', ";
                szöveg += $"'{Adat.Telephely}', ";
                szöveg += $"'{Adat.PDFFájlneve}', ";
                szöveg += $"{Adat.Számonkérés}, 0, ";
                szöveg += $"'{Adat.Rögzítő}', ";
                szöveg += $"'{Adat.Megjegyzés}'  )";
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

        public void Rögzítés(string Telephely, int Év, List<Adat_Oktatás_Napló> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                long sorszám = Sorszám(Telephely, Év);
                List<string> SzövegGy = new List<string>();

                foreach (Adat_Oktatás_Napló Adat in Adatok)
                {
                    string szöveg = "INSERT INTO Oktatásnapló";
                    szöveg += "( Id, Hrazonosító, IDoktatás, oktatásdátuma, kioktatta, rögzítésdátuma, telephely, PDFfájlneve, Számonkérés, státus, rögzítő, megjegyzés)";
                    szöveg += " VALUES (";
                    szöveg += $"{Sorszám(Telephely, Év)}, "; //id
                    szöveg += $"'{Adat.HRazonosító}', "; //Hrazonosító
                    szöveg += $"{Adat.IDoktatás}, "; //IDoktatás
                    szöveg += $"'{Adat.Oktatásdátuma}', ";
                    szöveg += $"'{Adat.Kioktatta}', ";
                    szöveg += $"'{DateTime.Now}', ";
                    szöveg += $"'{Adat.Telephely}', ";
                    szöveg += $"'{Adat.PDFFájlneve}', ";
                    szöveg += $"{Adat.Számonkérés}, 0, ";
                    szöveg += $"'{Adat.Rögzítő}', ";
                    szöveg += $"'{Adat.Megjegyzés}'  )";
                    SzövegGy.Add(szöveg);
                    sorszám++;
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private long Sorszám(string Telephely, int Év)
        {
            long Válasz = 1;
            try
            {
                List<Adat_Oktatás_Napló> Adatok = Lista_Adatok(Telephely, Év);
                if (Adatok == null) return Válasz;
                Válasz = Adatok.Max(a => a.ID) + 1;
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


        public void Törlés(string Telephely, int Év, List<Adat_Oktatás_Napló> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Oktatás_Napló Adat in Adatok)
                {
                    string szöveg = $"UPDATE oktatásnapló SET státus={Adat.Státus}, ";
                    szöveg = $" rögzítő='{Adat.Rögzítő}', ";
                    szöveg += $" rögzítésdátuma='{Adat.Rögzítésdátuma}'";
                    szöveg += $" Where id={Adat.ID}";
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
    }
}

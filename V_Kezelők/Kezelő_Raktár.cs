using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.V_Adatbázis;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Raktár
    {
        readonly string hely;
        readonly string jelszó = "SzőkeLászló";
        readonly string táblanév = "RaktárTábla";

        public Kezelő_Raktár()
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\rezsi\RaktárKészlet.mdb".KönyvSzerk();
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Raktárkészlet(hely);
        }

        public List<Adat_Raktár> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Raktár> Adatok = new List<Adat_Raktár>();
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
                                Adat_Raktár Adat = new Adat_Raktár(
                                        rekord["Cikkszám"].ToStrTrim(),
                                        rekord["Sarzs"].ToStrTrim(),
                                        rekord["Raktárhely"].ToStrTrim(),
                                        rekord["Mennyiség"].ToÉrt_Double()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public void Rögzítés(List<Adat_Raktár> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Raktár Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév} (Cikkszám, Sarzs, Raktárhely, Mennyiség) VALUES (";
                    szöveg += $"'{Adat.Cikkszám}', ";
                    szöveg += $"'{Adat.Sarzs}', ";
                    szöveg += $"'{Adat.Raktárhely}', ";
                    szöveg += $"{Adat.Mennyiség.ToString().Replace(",", ".")})";
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

        public void Törlés(string Raktárhely)
        {
            try
            {
                string szöveg = $"DELETE * FROM {táblanév} WHERE Raktárhely='{Raktárhely}'";
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

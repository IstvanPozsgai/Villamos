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
    public class Kezelő_Sérülés_Anyag
    {
        string hely;
        readonly string jelszó = "tükör";
        readonly string táblanév = "Anyag";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Év}\sérülés{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely.KönyvSzerk());
        }

        public List<Adat_Sérülés_Anyag> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Sérülés_Anyag> Adatok = new List<Adat_Sérülés_Anyag>();
            Adat_Sérülés_Anyag Adat;

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
                                Adat = new Adat_Sérülés_Anyag(
                                           rekord["cikkszám"].ToStrTrim(),
                                           rekord["anyagnév"].ToStrTrim(),
                                           rekord["mennyiség"].ToÉrt_Double(),
                                           rekord["me"].ToStrTrim(),
                                           rekord["ár"].ToÉrt_Double(),
                                           rekord["állapot"].ToStrTrim(),
                                           rekord["Rendelés"].ToÉrt_Double(),
                                           rekord["mozgásnem"].ToStrTrim()
                                                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Törlés(int Év, List<double> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGy = new List<string>();
                foreach (double Adat in Adatok)
                {
                    string szöveg = $"DELETE FROM {táblanév} WHERE rendelés={Adat}";
                    SzövegGy.Add(szöveg);
                }
                if (SzövegGy.Count > 0) MyA.ABtörlés(hely, jelszó, SzövegGy);
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

        public void Rögzítés(int Év, List<Adat_Sérülés_Anyag> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Sérülés_Anyag Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév} (cikkszám, anyagnév, mennyiség, me, ár, állapot, rendelés, mozgásnem ) VALUES (";
                    szöveg += $"'{Adat.Cikkszám}', ";
                    szöveg += $"'{Adat.Anyagnév} ', ";
                    szöveg += $"{Adat.Mennyiség.ToStrTrim().Replace(",", ".")}, ";  // a tizedes vessző miatt ponttal rögzítem
                    szöveg += $"'{Adat.Me} ', ";
                    szöveg += $"{Adat.Ár}, ";
                    szöveg += $"'{Adat.Állapot}', ";
                    szöveg += $"{Adat.Rendelés}, ";
                    szöveg += $"'{Adat.Mozgásnem}') ";
                    SzövegGy.Add(szöveg);
                }
                if (SzövegGy.Count > 0) MyA.ABMódosítás(hely, jelszó, SzövegGy);
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

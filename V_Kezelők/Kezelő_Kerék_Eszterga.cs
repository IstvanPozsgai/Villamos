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
    public class Kezelő_Kerék_Eszterga
    {
        readonly string jelszó = "szabólászló";
        string hely;
        readonly string táblanév = "esztergatábla";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Év}\telepikerék.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Méréstáblakerék(hely.KönyvSzerk());
        }

        public List<Adat_Kerék_Eszterga> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Kerék_Eszterga> Adatok = new List<Adat_Kerék_Eszterga>();

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
                                Adat_Kerék_Eszterga Adat = new Adat_Kerék_Eszterga(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Eszterga"].ToÉrt_DaTeTime(),
                                        rekord["Módosító"].ToStrTrim(),
                                        rekord["Mikor"].ToÉrt_DaTeTime(),
                                        rekord["kmu"].ToÉrt_Long()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(int Év, Adat_Kerék_Eszterga Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = $"INSERT INTO {táblanév} (eszterga, mikor, módosító, azonosító, kmu)  VALUES (";
                szöveg += $"'{Adat.Eszterga:yyyy.MM.dd}', ";
                szöveg += $"'{Adat.Mikor}', ";
                szöveg += $"'{Adat.Módosító}', ";
                szöveg += $"'{Adat.Azonosító}', ";
                szöveg += $"{Adat.KMU} )";
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

        public void Rögzítés(int Év, List<Adat_Kerék_Eszterga> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> szövegGy = new List<string>();
                foreach (Adat_Kerék_Eszterga Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév} (eszterga, mikor, módosító, azonosító, kmu)  VALUES (";
                    szöveg += $"'{Adat.Eszterga:yyyy.MM.dd}', ";
                    szöveg += $"'{Adat.Mikor}', ";
                    szöveg += $"'{Adat.Módosító}', ";
                    szöveg += $"'{Adat.Azonosító}', ";
                    szöveg += $"{Adat.KMU} )";
                    szövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);
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

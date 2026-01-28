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
    public class Kezelő_Jármű_Napló
    {
        readonly string jelszó = "pozsgaii";
        string hely;       
        readonly string táblanév = "állománytáblanapló";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\napló\napló{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kocsitípusanapló(hely.KönyvSzerk());
        }

        public void Rögzítés(int Év, Adat_Jármű_Napló Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = $"INSERT INTO {táblanév} (azonosító, típus, honnan, hova, törölt, Módosító, mikor, céltelep, üzenet) VALUES (";
                szöveg += $"'{Adat.Azonosító.Trim()}', ";
                szöveg += $"'{Adat.Típus.Trim()}', ";
                szöveg += $"'{Adat.Honnan.Trim()}', ";
                szöveg += $"'{Adat.Hova.Trim()}', ";
                szöveg += $"{Adat.Törölt}, ";
                szöveg += $"'{Adat.Módosító.Trim()}', ";
                szöveg += $"'{Adat.Mikor}', ";
                szöveg += $"'{Adat.Céltelep.Trim()}', ";
                szöveg += $"{Adat.Üzenet}) ";

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

        public List<Adat_Jármű_Napló> Lista_adatok(int Év)
        {
            FájlBeállítás(Év);
            string szöveg = $"SELECT * FROM {táblanév} ";
            List<Adat_Jármű_Napló> Adatok = new List<Adat_Jármű_Napló>();
            Adat_Jármű_Napló Adat;
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
                                Adat = new Adat_Jármű_Napló(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Típus"].ToStrTrim(),
                                    rekord["hova"].ToStrTrim(),
                                    rekord["honnan"].ToStrTrim(),
                                    rekord["törölt"].ToÉrt_Bool(),
                                    rekord["Módosító"].ToStrTrim(),
                                    rekord["Mikor"].ToÉrt_DaTeTime(),
                                    rekord["Céltelep"].ToStrTrim(),
                                    rekord["üzenet"].ToÉrt_Int()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public void Módosítás(int Év, List<Adat_Jármű_Napló> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Jármű_Napló rekord in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév}  SET üzenet=1 WHERE üzenet=0 AND céltelep='{rekord.Céltelep}' AND Azonosító='{rekord.Azonosító}'";
                    SzövegGy.Add(szöveg);
                }
                string hely = $@"{Application.StartupPath}\Főmérnökség\napló\napló{Év}.mdb".KönyvSzerk();
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

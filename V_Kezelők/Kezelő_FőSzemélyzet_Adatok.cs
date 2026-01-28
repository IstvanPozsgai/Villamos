using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_FőSzemélyzet_Adatok
    {
        string hely;
        readonly string jelszó = "pozsi";
        readonly string táblanév = "személyzettábla";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Év}\{Év}_személyzet_adatok.mdb".KönyvSzerk();
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kiadásiszemélyzetfőmérnöktábla(hely); ;
        }

        public List<Adat_Személyzet_Adatok> Lista_adatok(int Év)
        {
            List<Adat_Személyzet_Adatok> Adatok = new List<Adat_Személyzet_Adatok>();
            FájlBeállítás(Év);
            string szöveg = $"SELECT * FROM {táblanév} ";

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
                                Adat_Személyzet_Adatok Adat = new Adat_Személyzet_Adatok(
                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["szolgálat"].ToStrTrim(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["viszonylat"].ToStrTrim(),
                                    rekord["forgalmiszám"].ToStrTrim(),
                                    rekord["tervindulás"].ToÉrt_DaTeTime(),
                                    rekord["azonosító"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(int Év, List<Adat_Személyzet_Adatok> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Személyzet_Adatok Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév} (dátum, napszak, telephely, szolgálat, típus, viszonylat, forgalmiszám, tervindulás, azonosító ) VALUES (";
                    szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', ";
                    szöveg += $"'{Adat.Napszak}', ";
                    szöveg += $"'{Adat.Telephely}', ";
                    szöveg += $"'{Adat.Szolgálat}', ";
                    szöveg += $"'{Adat.Típus}', ";
                    szöveg += $"'{Adat.Viszonylat}', ";
                    szöveg += $"'{Adat.Forgalmiszám}', ";
                    szöveg += $"'{Adat.Tervindulás:HH:mm}', ";
                    szöveg += $"'{Adat.Azonosító}') ";
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

        public void Törlés(int Év, Adat_Személyzet_Adatok Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = $"DELETE FROM {táblanév} WHERE [dátum]=#{Adat.Dátum:M-d-yy}#";
                if (Adat.Napszak.Trim() != "")
                    szöveg += $" and napszak='{Adat.Napszak}' ";
                szöveg += $" and telephely='{Adat.Telephely}'";
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
    }
}

using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_FőTípuscsere_Adatok
    {
        string hely;
        readonly string jelszó = "pozsi";
        readonly string táblanév = "típuscseretábla";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Év}\{Év}_típuscsere_adatok.mdb".KönyvSzerk();
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kiadásitípuscserefőmérnöktábla(hely);
        }

        public List<Adat_Típuscsere_Adatok> Lista_adatok(int Év)
        {
            FájlBeállítás(Év);
            List<Adat_Típuscsere_Adatok> Adatok = new List<Adat_Típuscsere_Adatok>();
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
                                Adat_Típuscsere_Adatok Adat = new Adat_Típuscsere_Adatok(
                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["szolgálat"].ToStrTrim(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["típuselőírt"].ToStrTrim(),
                                    rekord["típuskiadott"].ToStrTrim(),
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

        public void Törlés(int Év, Adat_Típuscsere_Adatok Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = $"DELETE FROM típuscseretábla WHERE [dátum]=#{Adat.Dátum:M-d-yy}#";
                if (Adat.Napszak.Trim() != "")
                    szöveg += $" and napszak='{Adat.Napszak}'";
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

        public void Rögzítés(int Év, List<Adat_Típuscsere_Adatok> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Típuscsere_Adatok Adat in Adatok)
                {

                    string szöveg = $"INSERT INTO {táblanév} (dátum, napszak, telephely, szolgálat, típuselőírt, típuskiadott, viszonylat, forgalmiszám, tervindulás, azonosító  ) VALUES (";
                    szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', ";
                    szöveg += $"'{Adat.Napszak}', ";
                    szöveg += $"'{Adat.Telephely}', ";
                    szöveg += $"'{Adat.Szolgálat}', ";
                    szöveg += $"'{Adat.Típuselőírt}', ";
                    szöveg += $"'{Adat.Típuskiadott}', ";
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

    }
}

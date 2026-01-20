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
    public class Kezelő_T5C5_Göngyöl
    {
        string hely;
        readonly string jelszó = "pozsgaii";

        private void FájlBeállítás(string Telephely, DateTime Dátum)
        {
            if (Telephely == "Főmérnökség")
                hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\T5C5\Villamos3.mdb";
            else
                hely = $@"{Application.StartupPath}\{Telephely}\Adatok\főkönyv\futás\{Dátum.Year}\Villamos3-{Dátum:yyyyMMdd}.mdb";

            if (!File.Exists(hely)) Adatbázis_Létrehozás.Futásnaptábla_Létrehozás(hely.KönyvSzerk());

        }

        /// <summary>
        /// Ez az eljárás átrögzíti egy helyi fájlba a telephely tartozó adatokat, csak a telephely kocsijait
        /// </summary>
        /// <param name="Telephely"></param>
        /// <param name="Dátum"></param>
        /// <param name="Adatok"></param>
        public void Rögzítés(string Telephely, DateTime Dátum, List<Adat_T5C5_Göngyöl> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_T5C5_Göngyöl Adat in Adatok)
                {
                    string telep = "_";
                    if (Telephely == Adat.Telephely) telep = Adat.Telephely;

                    string szöveg = $"INSERT INTO állománytábla (azonosító, utolsórögzítés, vizsgálatdátuma, utolsóforgalminap, Vizsgálatfokozata, vizsgálatszáma, futásnap, telephely) VALUES (";
                    szöveg += $"'{Adat.Azonosító}', ";                       // azonosító
                    szöveg += $"'{Adat.Utolsórögzítés:yyyy.MM.dd}', ";       // utolsórögzítés
                    szöveg += $"'{Adat.Vizsgálatdátuma:yyyy.MM.dd}', ";      // vizsgálatdátuma
                    szöveg += $"'{Adat.Utolsóforgalminap:yyyy.MM.dd}', ";    // utolsóforgalminap
                    szöveg += $"'{Adat.Vizsgálatfokozata}', ";               // Vizsgálatfokozata
                    szöveg += $"{Adat.Vizsgálatszáma}, ";                    // vizsgálatszáma
                    szöveg += $"{Adat.Futásnap}, ";                          // futásnap
                    szöveg += $"'{telep}')";
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

        public void Rögzítés(string Telephely, DateTime Dátum, Adat_T5C5_Göngyöl Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);
                string telep = "_";
                if (Telephely == Adat.Telephely) telep = Adat.Telephely;

                string szöveg = $"INSERT INTO állománytábla (azonosító, utolsórögzítés, vizsgálatdátuma, utolsóforgalminap, Vizsgálatfokozata, vizsgálatszáma, futásnap, telephely) VALUES (";
                szöveg += $"'{Adat.Azonosító}', ";                       // azonosító
                szöveg += $"'{Adat.Utolsórögzítés:yyyy.MM.dd}', ";       // utolsórögzítés
                szöveg += $"'{Adat.Vizsgálatdátuma:yyyy.MM.dd}', ";      // vizsgálatdátuma
                szöveg += $"'{Adat.Utolsóforgalminap:yyyy.MM.dd}', ";    // utolsóforgalminap
                szöveg += $"'{Adat.Vizsgálatfokozata}', ";               // Vizsgálatfokozata
                szöveg += $"{Adat.Vizsgálatszáma}, ";                    // vizsgálatszáma
                szöveg += $"{Adat.Futásnap}, ";                          // futásnap
                szöveg += $"'{telep}')";

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

        public List<Adat_T5C5_Göngyöl> Lista_Adatok(string Telephely, DateTime Dátum)
        {
            FájlBeállítás(Telephely, Dátum);
            string szöveg = $"SELECT * FROM Állománytábla ORDER BY azonosító";
            List<Adat_T5C5_Göngyöl> Adatok = new List<Adat_T5C5_Göngyöl>();
            Adat_T5C5_Göngyöl Adat;

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
                                Adat = new Adat_T5C5_Göngyöl(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Utolsórögzítés"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgálatdátuma"].ToÉrt_DaTeTime(),
                                    rekord["Utolsóforgalminap"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgálatfokozata"].ToStrTrim(),
                                    rekord["Vizsgálatszáma"].ToÉrt_Int(),
                                    rekord["Futásnap"].ToÉrt_Int(),
                                    rekord["Telephely"].ToStrTrim()
                                    ); ;
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(string Telephely, DateTime Dátum, List<Adat_T5C5_Göngyöl> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);
                List<string> szövegGy = new List<string>();
                foreach (Adat_T5C5_Göngyöl Adat in Adatok)
                {
                    string szöveg = $"UPDATE állománytábla SET ";
                    szöveg += $" utolsórögzítés='{Adat.Utolsórögzítés:yyyy.MM.dd}', ";
                    szöveg += $" vizsgálatdátuma='{Adat.Vizsgálatdátuma:yyyy.MM.dd}', ";
                    szöveg += $" Vizsgálatfokozata='{Adat.Vizsgálatfokozata}', ";
                    szöveg += $" vizsgálatszáma={Adat.Vizsgálatszáma}, ";
                    szöveg += $" futásnap={Adat.Futásnap}, ";
                    szöveg += $" utolsóforgalminap='{Adat.Utolsóforgalminap:yyyy.MM.dd}', ";
                    szöveg += $" telephely='{Adat.Telephely}' ";
                    szöveg += $" WHERE azonosító='{Adat.Azonosító}'";
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

        public void Módosítás(string Telephely, DateTime Dátum, Adat_T5C5_Göngyöl Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);
                string szöveg = $"UPDATE állománytábla SET ";
                szöveg += $" utolsórögzítés='{Adat.Utolsórögzítés:yyyy.MM.dd}', ";
                szöveg += $" vizsgálatdátuma='{Adat.Vizsgálatdátuma:yyyy.MM.dd}', ";
                szöveg += $" Vizsgálatfokozata='{Adat.Vizsgálatfokozata}', ";
                szöveg += $" vizsgálatszáma={Adat.Vizsgálatszáma}, ";
                szöveg += $" futásnap={Adat.Futásnap}, ";
                szöveg += $" utolsóforgalminap='{Adat.Utolsóforgalminap:yyyy.MM.dd}', ";
                szöveg += $" telephely='{Adat.Telephely}' ";
                szöveg += $" WHERE azonosító='{Adat.Azonosító}'";

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

        public void Törlés(string Telephely, DateTime Dátum, List<string> Azonosítók)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);
                List<string> szövegGy = new List<string>();
                foreach (string Adat in Azonosítók)
                {
                    string szöveg = $"DELETE FROM állománytábla WHERE [azonosító]='{Adat}'";
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

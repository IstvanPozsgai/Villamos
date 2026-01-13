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
    public class Kezelő_Technológia_Változat
    {
        readonly string jelszó = "Bezzegh";
        string hely;

        private void FájlBeállítás(string Típus, string Telephely)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Technológia\{Típus}.mdb".KönyvSzerk();
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Technológia_Telep(hely, Telephely);
            string szöveg = $"SELECT * FROM {Telephely}";
            if (!MyA.ABvanTábla(hely, jelszó, szöveg)) Adatbázis_Létrehozás.Technológia_Telep(hely, Telephely);
        }

        public List<Adat_Technológia_Változat> Lista_Adatok(string Típus, string Telephely)
        {
            FájlBeállítás(Típus, Telephely);
            string szöveg = $"SELECT * FROM {Telephely}";
            List<Adat_Technológia_Változat> Adatok = new List<Adat_Technológia_Változat>();
            Adat_Technológia_Változat Adat;

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
                                Adat = new Adat_Technológia_Változat(
                                    rekord["technológia_Id"].ToÉrt_Long(),
                                    rekord["változatnév"].ToStrTrim(),
                                    rekord["végzi"].ToStrTrim(),
                                    rekord["karbantartási_fokozat"].ToStrTrim()
                                    );

                                Adatok.Add(Adat);
                            }
                        }

                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(string Típus, string Telephely, List<Adat_Technológia_Változat> Adatok)
        {
            try
            {
                FájlBeállítás(Típus, Telephely);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Technológia_Változat Adat in Adatok)
                {
                    string szöveg = $"UPDATE {Telephely}  SET ";
                    szöveg += $"Végzi='{Adat.Végzi}' ";
                    szöveg += $"WHERE Karbantartási_fokozat = '{Adat.Karbantartási_fokozat}' ";
                    szöveg += $"AND Változatnév='{Adat.Változatnév}' AND technológia_Id={Adat.Technológia_Id} ";
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

        public void Rögzítés(string Típus, string Telephely, List<Adat_Technológia_Változat> Adatok)
        {
            try
            {
                FájlBeállítás(Típus, Telephely);
                List<string> SzövegGy = new List<string>(); foreach (Adat_Technológia_Változat Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {Telephely}  (technológia_Id, Karbantartási_fokozat, Változatnév, Végzi ) VALUES (";
                    szöveg += $"{Adat.Technológia_Id}, '{Adat.Karbantartási_fokozat}', '{Adat.Változatnév}', '{Adat.Végzi}') ";
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

        public void Törlés(string Típus, string Telephely, Adat_Technológia_Változat Adat)
        {
            try
            {
                FájlBeállítás(Típus, Telephely);
                string szöveg = $"DELETE FROM  {Telephely}  ";
                szöveg += $"WHERE Karbantartási_fokozat = '{Adat.Karbantartási_fokozat}' ";
                szöveg += $"AND Változatnév='{Adat.Változatnév}' AND technológia_Id={Adat.Technológia_Id}  AND végzi='{Adat.Végzi}'";
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

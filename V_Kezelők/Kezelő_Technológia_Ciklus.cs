using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{

    public class Kezelő_Technológia_Ciklus
    {
        readonly string jelszó = "Bezzegh";
        string hely;

        private void FájlBeállítás(string Típus)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{Típus}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Technológia_Adat(hely.KönyvSzerk());
        }

        public List<Adat_technológia_Ciklus> Lista_Adatok(string Típus)
        {
            FájlBeállítás(Típus);
            string szöveg = "SELECT * FROM karbantartás ORDER BY sorszám";
            List<Adat_technológia_Ciklus> Adatok = new List<Adat_technológia_Ciklus>();
            Adat_technológia_Ciklus Adat;

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
                                Adat = new Adat_technológia_Ciklus(
                                    rekord["sorszám"].ToÉrt_Int(),
                                    rekord["fokozat"].ToStrTrim(),
                                    rekord["csoportos"].ToÉrt_Int(),
                                    rekord["elérés"].ToStrTrim(),
                                    rekord["verzió"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }

                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Típus, Adat_technológia_Ciklus Adat)
        {
            try
            {
                string szöveg;
                List<Adat_technológia_Ciklus> AdatokCiklus = Lista_Adatok(Típus);
                int sorszám = 1;
                if (AdatokCiklus != null && AdatokCiklus.Count > 0)
                    sorszám = AdatokCiklus.Max(a => a.Sorszám) + 1;
                Adat_technológia_Ciklus Elem = AdatokCiklus.Where(a => a.Sorszám == Adat.Sorszám).FirstOrDefault();

                if (Elem == null)
                {
                    szöveg = "INSERT INTO Karbantartás  (Sorszám, Fokozat, Csoportos, Elérés, Verzió) VALUES (";
                    szöveg += $"{sorszám}, ";
                    szöveg += $"'{Adat.Fokozat}', ";
                    szöveg += $"{Adat.Csoportos}, ";
                    szöveg += $"'{Adat.Elérés}', ";
                    szöveg += $"'{Adat.Verzió}' )";
                }
                else
                {
                    szöveg = "UPDATE Karbantartás  SET ";
                    szöveg += $"Fokozat='{Adat.Fokozat}', ";
                    szöveg += $"Csoportos={Adat.Csoportos}, ";
                    szöveg += $"Elérés='{Adat.Elérés}', ";
                    szöveg += $"Verzió='{Adat.Verzió}' ";
                    szöveg += $"WHERE Sorszám={Adat.Sorszám}";
                }
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

        public void Törlés(string Típus, int Sorszám)
        {
            try
            {
                List<Adat_technológia_Ciklus> AdatokCiklus = Lista_Adatok(Típus);
                Adat_technológia_Ciklus Elem = AdatokCiklus.Where(a => a.Sorszám == Sorszám).FirstOrDefault();

                if (Elem != null)
                {
                    string szöveg = $"DELETE FROM Karbantartás WHERE sorszám={Sorszám}";
                    MyA.ABtörlés(hely, jelszó, szöveg);
                }
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


        public List<Adat_technológia_Ciklus> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_technológia_Ciklus> Adatok = new List<Adat_technológia_Ciklus>();
            Adat_technológia_Ciklus Adat;

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
                                Adat = new Adat_technológia_Ciklus(
                                    rekord["sorszám"].ToÉrt_Int(),
                                    rekord["fokozat"].ToStrTrim(),
                                    rekord["csoportos"].ToÉrt_Int(),
                                    rekord["elérés"].ToStrTrim(),
                                    rekord["verzió"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }

                    }
                }
            }
            return Adatok;
        }

        public Adat_technológia_Ciklus Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_technológia_Ciklus Adat = null;

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
                                Adat = new Adat_technológia_Ciklus(
                                    rekord["sorszám"].ToÉrt_Int(),
                                    rekord["fokozat"].ToStrTrim(),
                                    rekord["csoportos"].ToÉrt_Int(),
                                    rekord["elérés"].ToStrTrim(),
                                     rekord["verzió"].ToStrTrim()
                                    );
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }

}

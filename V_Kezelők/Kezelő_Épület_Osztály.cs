using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Épület_Takarítás_Osztály
    {
        readonly string jelszó = "seprűéslapát";
        readonly string táblanév = "takarításosztály";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Épület\épülettörzs.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Épülettakarításlétrehozás(hely.KönyvSzerk());
        }

        public List<Adat_Épület_Takarítás_Osztály> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            List<Adat_Épület_Takarítás_Osztály> Adatok = new List<Adat_Épület_Takarítás_Osztály>();
            string szöveg = $"SELECT * FROM {táblanév} order by id";
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
                                Adat_Épület_Takarítás_Osztály Adat = new Adat_Épület_Takarítás_Osztály(
                                        rekord["id"].ToÉrt_Int(),
                                        rekord["Osztály"].ToStrTrim(),
                                        rekord["E1Ft"].ToÉrt_Double(),
                                        rekord["E2Ft"].ToÉrt_Double(),
                                        rekord["E3Ft"].ToÉrt_Double(),
                                        rekord["státus"].ToÉrt_Bool());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(string Telephely, Adat_Épület_Takarítás_Osztály Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE {táblanév}  SET ";
                szöveg += $"osztály='{Adat.Osztály}', ";
                szöveg += $"E1Ft={Adat.E1Ft.ToString().Replace(",", ".")}, ";
                szöveg += $"E2Ft={Adat.E2Ft.ToString().Replace(",", ".")}, ";
                szöveg += $"E3Ft={Adat.E3Ft.ToString().Replace(",", ".")}";
                szöveg += $" WHERE id={Adat.Id}";
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

        public void Rögzítés(string Telephely, Adat_Épület_Takarítás_Osztály Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"INSERT INTO {táblanév}  (id, osztály, E1Ft, E2Ft, E3Ft, státus) VALUES (";
                szöveg += $"{Sorszám(Telephely)}, ";
                szöveg += $"'{Adat.Osztály}', ";
                szöveg += $"{Adat.E1Ft.ToString().Replace(",", ".")}, ";
                szöveg += $"{Adat.E2Ft.ToString().Replace(",", ".")}, ";
                szöveg += $"{Adat.E3Ft.ToString().Replace(",", ".")}, false )";
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

        public void Módosítás(string Telephely, List<Adat_Épület_Takarítás_Osztály> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Épület_Takarítás_Osztály Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév}  SET ";
                    szöveg += $"E1Ft={Adat.E1Ft.ToString().Replace(',', '.')}, ";
                    szöveg += $"E2Ft={Adat.E2Ft.ToString().Replace(',', '.')}, ";
                    szöveg += $"E3Ft={Adat.E3Ft.ToString().Replace(',', '.')} ";
                    szöveg += $" WHERE osztály='{Adat.Osztály}'";
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

        public void Rögzítés(string Telephely, List<Adat_Épület_Takarítás_Osztály> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<string> SzövegGy = new List<string>();
                int i = Sorszám(Telephely);
                foreach (Adat_Épület_Takarítás_Osztály Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév} (id, osztály, E1Ft, E2Ft, E3Ft, státus) VALUES (";
                    szöveg += $"{i}, ";
                    szöveg += $"'{Adat.Osztály}', ";
                    szöveg += $"{Adat.E1Ft.ToString().Replace(",", ".")}, ";
                    szöveg += $"{Adat.E2Ft.ToString().Replace(",", ".")}, ";
                    szöveg += $"{Adat.E3Ft.ToString().Replace(",", ".")}, false )";
                    SzövegGy.Add(szöveg);
                    i++;
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

        public void Törlés(string Telephely, int Id)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE {táblanév}  SET státus=true ";
                szöveg += $" WHERE id={Id}";
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

        public void Csere(string Telephely, int sorszám)
        {
            try
            {
                List<Adat_Épület_Takarítás_Osztály> Adatok = Lista_Adatok(Telephely).Where(a => a.Státus == false).ToList();
                Adat_Épület_Takarítás_Osztály Elem = Adatok.FirstOrDefault(a => a.Id == sorszám) ?? throw new HibásBevittAdat("A kiválasztott osztály nem található az adatbázisban.");
                int index = Adatok.FindIndex(a => a.Id == sorszám);
                Adat_Épület_Takarítás_Osztály Előző = ((index > 0) ? Adatok[index - 1] : null) ?? throw new HibásBevittAdat("A kiválasztott osztály nem található az adatbázisban.");
                Adat_Épület_Takarítás_Osztály ÚjElőző = new Adat_Épület_Takarítás_Osztály(
                                Előző.Id,
                                Elem.Osztály,
                                Elem.E1Ft,
                                Elem.E2Ft,
                                Elem.E3Ft,
                                Elem.Státus);
                Adat_Épület_Takarítás_Osztály ÚjElem = new Adat_Épület_Takarítás_Osztály(
                                Elem.Id,
                                Előző.Osztály,
                                Előző.E1Ft,
                                Előző.E2Ft,
                                Előző.E3Ft,
                                Előző.Státus);
                Módosítás(Telephely, ÚjElőző);
                Módosítás(Telephely, ÚjElem);

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

        private int Sorszám(string Telephely)
        {
            int válasz = 1;
            try
            {
                List<Adat_Épület_Takarítás_Osztály> Adatok = Lista_Adatok(Telephely);
                if (Adatok.Count > 0) válasz = Adatok.Max(a => a.Id) + 1;
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
            return válasz;
        }
    }
}

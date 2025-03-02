using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Jármű_Takarítás_Vezénylés
    {
        readonly string jelszó = "seprűéslapát";
        string hely;

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Takarítás\Takarítás_{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Járműtakarító_Telephely_tábla(hely.KönyvSzerk());
        }

        public List<Adat_Jármű_Takarítás_Vezénylés> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = "SELECT * FROM vezénylés";
            List<Adat_Jármű_Takarítás_Vezénylés> Adatok = new List<Adat_Jármű_Takarítás_Vezénylés>();
            Adat_Jármű_Takarítás_Vezénylés Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Vezénylés(
                                        rekord["id"].ToÉrt_Long(),
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["takarítási_fajta"].ToStrTrim(),
                                        rekord["szerelvényszám"].ToÉrt_Long(),
                                        rekord["státus"].ToÉrt_Int()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Jármű_Takarítás_Vezénylés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_Vezénylés> Adatok = new List<Adat_Jármű_Takarítás_Vezénylés>();
            Adat_Jármű_Takarítás_Vezénylés Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Vezénylés(
                                        rekord["id"].ToÉrt_Long(),
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["takarítási_fajta"].ToStrTrim(),
                                        rekord["szerelvényszám"].ToÉrt_Long(),
                                        rekord["státus"].ToÉrt_Int()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Döntés(string Telephely, int Év, Adat_Jármű_Takarítás_Vezénylés Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);

                List<Adat_Jármű_Takarítás_Vezénylés> Adatok = Lista_Adatok(Telephely, Év);
                Adat_Jármű_Takarítás_Vezénylés Elem = (from a in Adatok
                                                       where a.Azonosító == Adat.Azonosító
                                                       && a.Takarítási_fajta == Adat.Takarítási_fajta
                                                       && a.Státus != 9
                                                       && a.Dátum.ToShortDateString() == Adat.Dátum.ToShortDateString()
                                                       select a).FirstOrDefault();

                if (Elem == null)
                    Rögzítés(Telephely, Év, Adat);
                else
                    Módosítás(Telephely, Év, Adat);
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

        public void Rögzítés(string Telephely, int Év, Adat_Jármű_Takarítás_Vezénylés Adat)
        {
            try
            {
                string szöveg = "INSERT INTO Vezénylés (id, azonosító, dátum, takarítási_fajta, szerelvényszám,  státus ) VALUES (";
                szöveg += $"{Sorszám(Telephely, Év)}, "; // id
                szöveg += $" '{Adat.Azonosító}', "; // azonosító
                szöveg += $" '{Adat.Dátum:yyyy.MM.dd}', ";  // dátum
                szöveg += $"'{Adat.Takarítási_fajta}', "; // takarítási_fajta
                szöveg += $"{Adat.Szerelvényszám}, "; // szerelvényszám
                szöveg += "0)";      // státus
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

        public void Módosítás(string Telephely, int Év, Adat_Jármű_Takarítás_Vezénylés Adat)
        {
            try
            {
                string szöveg = "UPDATE Vezénylés  SET  státus=0 ";
                szöveg += $" WHERE dátum=#{Adat.Dátum:MM-dd-yyyy}# And azonosító='{Adat.Azonosító}' AND takarítási_fajta='{Adat.Takarítási_fajta}'";
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

        public void Törlés(string Telephely, int Év, Adat_Jármű_Takarítás_Vezénylés Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                List<Adat_Jármű_Takarítás_Vezénylés> Adatok = Lista_Adatok(Telephely, Év);
                Adat_Jármű_Takarítás_Vezénylés Elem = (from a in Adatok
                                                       where a.Azonosító == Adat.Azonosító
                                                       && a.Takarítási_fajta == Adat.Takarítási_fajta
                                                       && a.Státus != 9
                                                       && a.Dátum.ToShortDateString() == Adat.Dátum.ToShortDateString()
                                                       select a).FirstOrDefault();
                if (Elem != null)
                {
                    string szöveg = "UPDATE Vezénylés  SET  státus=9 ";
                    szöveg += $" WHERE dátum=#{Adat.Dátum:MM-dd-yyyy}# And azonosító='{Adat.Azonosító}' AND takarítási_fajta='{Adat.Takarítási_fajta}'";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
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

        private long Sorszám(string Telephely, int Év)
        {
            long Válasz = 1;
            try
            {
                FájlBeállítás(Telephely, Év);
                List<Adat_Jármű_Takarítás_Vezénylés> Adatok = Lista_Adatok(Telephely, Év);
                if (Adatok != null && Adatok.Count > 0) Válasz = Adatok.Max(a => a.Id) + 1;
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
            return Válasz;
        }
    }
}

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
    public class Kezelő_Forte_Kiadási_Adatok
    {
        readonly string jelszó = "gémkapocs";
        string hely;

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Év}\{Év}_fortekiadási_adatok.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Fortekiadásifőmtábla(hely.KönyvSzerk());
        }

        public List<Adat_Forte_Kiadási_Adatok> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            string szöveg = "SELECT * FROM fortekiadástábla";
            List<Adat_Forte_Kiadási_Adatok> Adatok = new List<Adat_Forte_Kiadási_Adatok>();
            Adat_Forte_Kiadási_Adatok Adat;

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
                                Adat = new Adat_Forte_Kiadási_Adatok(
                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["telephelyforte"].ToStrTrim(),
                                    rekord["típusforte"].ToStrTrim(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["kiadás"].ToÉrt_Long(),
                                    rekord["munkanap"].ToÉrt_Long()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(int Év, List<Adat_Forte_Kiadási_Adatok> Adatok)
        {
            FájlBeállítás(Év);
            List<string> SzövegGy = new List<string>();
            foreach (Adat_Forte_Kiadási_Adatok Adat in Adatok)
            {
                string szöveg = "INSERT INTO fortekiadástábla  (dátum, napszak, telephelyforte, típusforte, telephely, típus, kiadás, munkanap  ) VALUES (";
                szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', ";
                szöveg += $"'{Adat.Napszak}', ";
                szöveg += $"'{Adat.Telephelyforte}', ";
                szöveg += $"'{Adat.Típusforte}', ";
                szöveg += $"'{Adat.Telephely}', ";
                szöveg += $"'{Adat.Típus}', ";
                szöveg += $"{Adat.Kiadás}, ";
                szöveg += $"{Adat.Munkanap}) ";
                SzövegGy.Add(szöveg);
            }
            MyA.ABMódosítás(hely, jelszó, SzövegGy);
        }

        public void Módosítás(int Év, List<Adat_Forte_Kiadási_Adatok> Adatok)
        {
            try
            {
                List<Adat_Forte_Kiadási_Adatok> Adatok_Forte = Lista_Adatok(Év);

                List<string> SzövegGy = new List<string>();
                foreach (Adat_Forte_Kiadási_Adatok Adat in Adatok)
                {
                    string szöveg = "UPDATE fortekiadástábla  SET ";
                    szöveg += $"telephely='{Adat.Telephely}', ";
                    szöveg += $"típus='{Adat.Típus}' ";
                    szöveg += $" WHERE [dátum]=#{Adat.Dátum:M-d-yy}# AND napszak='{Adat.Napszak}' AND ";
                    szöveg += $" telephelyforte='{Adat.Telephelyforte}' AND típusforte='{Adat.Típusforte}'";
                    Adat_Forte_Kiadási_Adatok Elem = (from a in Adatok_Forte
                                                      where a.Dátum == Adat.Dátum
                                                      && a.Napszak == Adat.Napszak
                                                      && a.Telephelyforte == Adat.Telephelyforte
                                                      && a.Típusforte == Adat.Típusforte
                                                      select a).FirstOrDefault();
                    if (Elem != null) SzövegGy.Add(szöveg);
                }
                if (SzövegGy != null && SzövegGy.Count > 0) MyA.ABMódosítás(hely, jelszó, SzövegGy);
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

        public void Módosítás(int Év, DateTime Dátum, int munkanap)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = "UPDATE fortekiadástábla  SET ";
                szöveg += $"munkanap={munkanap}";
                szöveg += $" WHERE [dátum]=#{Dátum:M-d-yy}#";
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

        public void Törlés(int Év, DateTime Dátum)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = $"DELETE FROM fortekiadástábla where [dátum]=#{Dátum:M-d-yy}#";
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

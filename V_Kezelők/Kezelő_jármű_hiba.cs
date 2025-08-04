using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;
using MyF = Függvénygyűjtemény;

namespace Villamos.Kezelők
{
    public class Kezelő_jármű_hiba
    {
        readonly string jelszó = "pozsgaii";
        string hely, helynapló;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\villamos\hiba.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Hibatáblalap(hely.KönyvSzerk());
        }

        private void FájlBeállítás(string Telephely, DateTime Dátum)
        {
            helynapló = $@"{Application.StartupPath}\{Telephely}\adatok\hibanapló\{Dátum:yyyyMM}hibanapló.mdb";
            if (!File.Exists(helynapló)) Adatbázis_Létrehozás.Hibatáblalap(helynapló.KönyvSzerk());
        }

        public List<Adat_Jármű_hiba> Lista_Adatok(string Telephely, DateTime Dátum)
        {
            FájlBeállítás(Telephely, Dátum);
            List<Adat_Jármű_hiba> Adatok = new List<Adat_Jármű_hiba>();
            string szöveg = "SELECT * FROM hibatábla";

            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{helynapló}'; Jet Oledb:Database Password={jelszó}";
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
                                Adat_Jármű_hiba adat = new Adat_Jármű_hiba(
                                    rekord["létrehozta"].ToStrTrim(),
                                    rekord["korlát"].ToÉrt_Long(),
                                    rekord["hibaleírása"].ToStrTrim(),
                                    rekord["idő"].ToÉrt_DaTeTime(),
                                    rekord["javítva"].ToÉrt_Bool(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["azonosító"].ToStrTrim(),
                                    rekord["hibáksorszáma"].ToÉrt_Long()
                                    );
                                Adatok.Add(adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Jármű_hiba> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            List<Adat_Jármű_hiba> Adatok = new List<Adat_Jármű_hiba>();
            string szöveg = "SELECT * FROM hibatábla ORDER BY Azonosító";

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
                                Adat_Jármű_hiba adat = new Adat_Jármű_hiba(
                                    rekord["létrehozta"].ToStrTrim(),
                                    rekord["korlát"].ToÉrt_Long(),
                                    rekord["hibaleírása"].ToStrTrim(),
                                    rekord["idő"].ToÉrt_DaTeTime(),
                                    rekord["javítva"].ToÉrt_Bool(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["azonosító"].ToStrTrim(),
                                    rekord["hibáksorszáma"].ToÉrt_Long()
                                    );
                                Adatok.Add(adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, Adat_Jármű_hiba Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<Adat_Jármű_hiba> Adatok = Lista_Adatok(Telephely);

                Adat_Jármű_hiba Elem = (from a in Adatok
                                        where a.Azonosító == Adat.Azonosító
                                        && a.Hibaleírása.Contains(Adat.Hibaleírása)
                                        select a).FirstOrDefault();

                if (Elem == null)
                {
                    long Sorszám = 1;
                    Adatok = (from a in Adatok
                              where a.Azonosító == Adat.Azonosító
                              select a).ToList();

                    if (Adatok != null && Adatok.Count > 0)
                        Sorszám = Adatok.Max(a => a.Hibáksorszáma) + 1;
                    // ha nem létezik 
                    string szöveg = "INSERT INTO hibatábla  ( létrehozta, korlát, hibaleírása, idő, javítva, típus, azonosító, hibáksorszáma ) VALUES (";
                    szöveg += $"'{Adat.Létrehozta.Trim()}', ";
                    szöveg += $"{Adat.Korlát}, ";
                    szöveg += $"'{Adat.Hibaleírása.Trim()}', ";
                    szöveg += $"'{Adat.Idő}', ";
                    szöveg += $"{Adat.Javítva}, ";
                    szöveg += $"'{Adat.Típus.Trim()}', ";
                    szöveg += $"'{Adat.Azonosító.Trim()}', ";
                    szöveg += $"{Sorszám})";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                    Újrasorszámoz(Telephely, Adat.Azonosító);
                    Rögzítés_Napló(Telephely, DateTime.Now, Adat);
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

        public void Rögzítés_Napló(string Telephely, DateTime Dátum, Adat_Jármű_hiba Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);

                List<Adat_Jármű_hiba> AdatokNapló = Lista_Adatok(Telephely, Dátum);
                long Sorszám = 1;

                if (AdatokNapló != null && AdatokNapló.Count > 0) Sorszám = AdatokNapló.Max(a => a.Hibáksorszáma) + 1;
                // ha nem létezik 
                string szöveg = "INSERT INTO hibatábla  ( létrehozta, korlát, hibaleírása, idő, javítva, típus, azonosító, hibáksorszáma ) VALUES (";
                szöveg += $"'{Adat.Létrehozta.Trim()}', ";
                szöveg += $"{Adat.Korlát}, ";
                szöveg += $"'{Adat.Hibaleírása.Trim()}', ";
                szöveg += $"'{Adat.Idő}', ";
                szöveg += $"{Adat.Javítva}, ";
                szöveg += $"'{Adat.Típus.Trim()}', ";
                szöveg += $"'{Adat.Azonosító.Trim()}', ";
                szöveg += $"{Sorszám})";
                MyA.ABMódosítás(helynapló, jelszó, szöveg);

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




        public void Törlés(string Telephely, Adat_Jármű_hiba Adat, bool naplóz = true)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "DELETE FROM Hibatábla ";
                szöveg += $" WHERE azonosító='{Adat.Azonosító}' AND hibáksorszáma={Adat.Hibáksorszáma}";
                MyA.ABtörlés(hely, jelszó, szöveg);
                if (naplóz) Rögzítés_Napló(Telephely, DateTime.Now, Adat);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Hiba Törlés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Törlés(string Telephely, string Azonosító)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"DELETE FROM hibatábla WHERE [azonosító]='{Azonosító}'";
                MyA.ABtörlés(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Hiba Törlés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosítás(string Telephely, Adat_Jármű_hiba Adat, bool naplóz = true)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE Hibatábla SET ";
                szöveg += $"Korlát={Adat.Korlát}, ";
                szöveg += $"létrehozta='{Program.PostásNév.Trim()}', ";
                szöveg += $"hibaleírása='{Adat.Hibaleírása}', ";
                szöveg += $"idő='{DateTime.Now}'";
                szöveg += $" WHERE azonosító='{Adat.Azonosító}'";
                szöveg += $" AND hibáksorszáma={Adat.Hibáksorszáma}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                if (naplóz) Rögzítés_Napló(Telephely, DateTime.Now, Adat);
                Újrasorszámoz(Telephely, Adat.Azonosító);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Hiba Módosítás", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Újrasorszámoz(string Telephely, string Azonosító)
        {
            try
            {
                FájlBeállítás(Telephely);

                List<Adat_Jármű_hiba> Adatok = Lista_Adatok(Telephely);
                Adatok = (from a in Adatok
                          where a.Azonosító == Azonosító
                          orderby a.Korlát descending, a.Hibáksorszáma
                          select a).ToList();

                List<string> szövegGy = new List<string>();
                for (int i = 0; i < Adatok.Count; i++)
                {
                    string szöveg = $"UPDATE Hibatábla SET hibáksorszáma={i + 1} WHERE azonosító='{Azonosító}'";
                    szöveg += $" And  hibaleírása='{Adatok[i].Hibaleírása}' AND idő=#{Adatok[i].Idő:MM-dd-yyyy HH:mm:ss}#";
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

        public void Ismétlődő_Elemek(string Telephely)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<Adat_Jármű_hiba> Adatok = Lista_Adatok(Telephely);
                for (int i = 0; i < Adatok.Count; i++)
                {
                    List<Adat_Jármű_hiba> Ismétlődés = (from a in Adatok
                                                        where a.Azonosító == Adatok[i].Azonosító
                                                        && a.Hibaleírása == Adatok[i].Hibaleírása
                                                        && a.Hibáksorszáma == Adatok[i].Hibáksorszáma
                                                        select a).ToList();
                    if (Ismétlődés != null && Ismétlődés.Count > 1)
                    {
                        Törlés(Telephely, Ismétlődés[0], false);
                        Rögzítés(Telephely, Ismétlődés[0]);
                    }
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

        public void Csere(string Telephely, int Sorszám, string Azonosító)
        {
            try
            {
                List<Adat_Jármű_hiba> Adatok = Lista_Adatok(Telephely);
                Adat_Jármű_hiba Előző = (from a in Adatok
                                         where a.Hibáksorszáma == Sorszám - 1 && a.Azonosító == Azonosító
                                         select a).FirstOrDefault();
                Adat_Jármű_hiba Következő = (from a in Adatok
                                             where a.Hibáksorszáma == Sorszám && a.Azonosító == Azonosító
                                             select a).FirstOrDefault();

                if (Előző == null || Következő == null) return;         //Ha valamelyik nincs akkor kilép

                string szöveg = "UPDATE hibatábla  SET ";
                szöveg += $"hibáksorszáma={Következő.Hibáksorszáma} ";
                szöveg += $" WHERE létrehozta='{Előző.Létrehozta}' AND hibaleírása='{Előző.Hibaleírása}' AND azonosító='{Előző.Azonosító}'";
                MyA.ABMódosítás(hely, jelszó, szöveg);

                szöveg = "UPDATE hibatábla  SET ";
                szöveg += $"hibáksorszáma={Előző.Hibáksorszáma} ";
                szöveg += $" WHERE létrehozta='{Következő.Létrehozta}' AND hibaleírása='{Következő.Hibaleírása}' AND azonosító='{Következő.Azonosító}'";
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

        public void Rögzítés_Napló(string Telephely, DateTime Dátum, List<Adat_Jármű_hiba> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);

                List<Adat_Jármű_hiba> AdatokNapló = Lista_Adatok(Telephely, Dátum);
                long Sorszám = 1;

                if (AdatokNapló != null && AdatokNapló.Count > 0) Sorszám = AdatokNapló.Max(a => a.Hibáksorszáma) + 1;

                List<string> SzövegGy = new List<string>();
                foreach (Adat_Jármű_hiba Adat in Adatok)
                {
                    // ha nem létezik 
                    string szöveg = "INSERT INTO hibatábla  ( létrehozta, korlát, hibaleírása, idő, javítva, típus, azonosító, hibáksorszáma ) VALUES (";
                    szöveg += $"'{Adat.Létrehozta.Trim()}', ";
                    szöveg += $"{Adat.Korlát}, ";
                    szöveg += $"'{MyF.Szöveg_Tisztítás(Adat.Hibaleírása.Trim(), 0, -1)}', ";
                    szöveg += $"'{Adat.Idő}', ";
                    szöveg += $"{Adat.Javítva}, ";
                    szöveg += $"'{Adat.Típus.Trim()}', ";
                    szöveg += $"'{Adat.Azonosító.Trim()}', ";
                    szöveg += $"{Sorszám})";
                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(helynapló, jelszó, SzövegGy);

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

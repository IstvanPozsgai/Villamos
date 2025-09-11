using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;


namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Csoportbeosztás
    {
        readonly string jelszó = "Mocó";
        string hely;
        readonly string táblanév = "csoportbeosztás";

        private bool FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\segéd\Kiegészítő.mdb";
            return File.Exists(hely);
            //nincs elkészítve
            // if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Adatok_Napló(hely.KönyvSzerk());
        }


        public List<Adat_Kiegészítő_Csoportbeosztás> Lista_Adatok(string Telephely)
        {
            List<Adat_Kiegészítő_Csoportbeosztás> Adatok = new List<Adat_Kiegészítő_Csoportbeosztás>();
            if (FájlBeállítás(Telephely))
            {
                string szöveg = $"SELECT * FROM {táblanév} order by sorszám";

                Adat_Kiegészítő_Csoportbeosztás Adat;

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
                                    Adat = new Adat_Kiegészítő_Csoportbeosztás(
                                            rekord["Sorszám"].ToÉrt_Long(),
                                            rekord["Csoportbeosztás"].ToStrTrim(),
                                            rekord["Típus"].ToStrTrim()
                                              );
                                    Adatok.Add(Adat);
                                }
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, Adat_Kiegészítő_Csoportbeosztás Adat)
        {
            try
            {
                if (FájlBeállítás(Telephely))
                {
                    string szöveg = $"INSERT INTO {táblanév} (sorszám, csoportbeosztás, típus) ";
                    szöveg += $"VALUES ({Sorszám(Telephely)}, ";
                    szöveg += $"'{Adat.Csoportbeosztás}', ";
                    szöveg += $"'{Adat.Típus}' )";
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

        public void Módosítás(string Telephely, Adat_Kiegészítő_Csoportbeosztás Adat)
        {
            try
            {
                if (FájlBeállítás(Telephely))
                {
                    string szöveg = $"UPDATE {táblanév} SET ";
                    szöveg += $" típus='{Adat.Típus}'";
                    szöveg += $" WHERE csoportbeosztás='{Adat.Csoportbeosztás}'";
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

        public void Módosítás(string Telephely, List<Adat_Kiegészítő_Csoportbeosztás> Adat)
        {
            try
            {
                if (FájlBeállítás(Telephely))
                {
                    List<string> SzövegGy = new List<string>();
                    foreach (Adat_Kiegészítő_Csoportbeosztás rekord in Adat)
                    {
                        string szöveg = $"UPDATE {táblanév} SET ";
                        szöveg += $" típus='{rekord.Típus}'";
                        szöveg += $" WHERE csoportbeosztás='{rekord.Csoportbeosztás}'";
                        SzövegGy.Add(szöveg);

                    }
                    MyA.ABMódosítás(hely, jelszó, SzövegGy);
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

        private long Sorszám(string Telephely)
        {
            long Válasz = 1;
            try
            {
                if (FájlBeállítás(Telephely))
                {
                    List<Adat_Kiegészítő_Csoportbeosztás> Adatok = Lista_Adatok(Telephely);
                    if (Adatok != null && Adatok.Count > 0) Válasz = Adatok.Max(a => a.Sorszám) + 1;
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
            return Válasz;
        }

        public void Törlés(string Telephely, long Sorszám)
        {
            try
            {
                if (FájlBeállítás(Telephely))
                {
                    string szöveg = $" DELETE FROM {táblanév} WHERE sorszám={Sorszám}";
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

        public void Csere(string Telephely, long Sorszám1, long Sorszám2)
        {
            try
            {
                if (FájlBeállítás(Telephely))
                {
                    List<Adat_Kiegészítő_Csoportbeosztás> Adatok = Lista_Adatok(Telephely);
                    Adat_Kiegészítő_Csoportbeosztás Adat1 = Adatok.Find(a => a.Sorszám == Sorszám1);
                    Adat_Kiegészítő_Csoportbeosztás Adat2 = Adatok.Find(a => a.Sorszám == Sorszám2);
                    if (Adat1 != null && Adat2 != null)
                    {
                        string szöveg = $" UPDATE {táblanév} SET ";
                        szöveg += $" sorszám={Adat2.Sorszám}";
                        szöveg += $" WHERE csoportbeosztás='{Adat1.Csoportbeosztás}'";
                        MyA.ABMódosítás(hely, jelszó, szöveg);
                        szöveg = $" UPDATE {táblanév} SET ";
                        szöveg += $" sorszám={Adat1.Sorszám}";
                        szöveg += $" WHERE csoportbeosztás='{Adat2.Csoportbeosztás}'";
                        MyA.ABMódosítás(hely, jelszó, szöveg);
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

        public void SorszámEllenőrzés(string Telephely)
        {
            try
            {
                if (FájlBeállítás(Telephely))
                {
                    List<Adat_Kiegészítő_Csoportbeosztás> AdatokÖ = Lista_Adatok(Telephely);

                    int i = 1;

                    List<string> SzövegGy = new List<string>();
                    foreach (Adat_Kiegészítő_Csoportbeosztás rekord in AdatokÖ)
                    {
                        long ideig = rekord.Sorszám - 1;
                        if (i != ideig)
                        {   //Ha a sorszám nem a következő akkor módosítjuk

                            string szöveg = $"UPDATE {táblanév}  SET ";
                            szöveg += $"sorszám={i + 1}";
                            szöveg += $" WHERE csoportbeosztás='{rekord.Csoportbeosztás}' AND  Típus='{rekord.Típus}'";
                            SzövegGy.Add(szöveg);
                        }
                        i++;
                    }
                    MyA.ABMódosítás(hely, jelszó, SzövegGy);
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
    }
}

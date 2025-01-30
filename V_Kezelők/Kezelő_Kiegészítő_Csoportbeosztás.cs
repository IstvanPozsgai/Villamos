using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;


namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Csoportbeosztás
    {
        readonly string jelszó = "Mocó";
        public List<Adat_Kiegészítő_Csoportbeosztás> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Csoportbeosztás> Adatok = new List<Adat_Kiegészítő_Csoportbeosztás>();
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
            return Adatok;
        }

        public List<Adat_Kiegészítő_Csoportbeosztás> Lista_Adatok(string hely)
        {
            string szöveg = "SELECT * FROM csoportbeosztás order by sorszám";
            List<Adat_Kiegészítő_Csoportbeosztás> Adatok = new List<Adat_Kiegészítő_Csoportbeosztás>();
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
            return Adatok;
        }

        public void Rögzítés(string hely, Adat_Kiegészítő_Csoportbeosztás Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO csoportbeosztás (sorszám, csoportbeosztás, típus) ";
                szöveg += $"VALUES ({Sorszám(hely)}, ";
                szöveg += $"'{Adat.Csoportbeosztás}', ";
                szöveg += $"'{Adat.Típus}' )";
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

        /// <summary>
        /// csoportbeosztás
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, Adat_Kiegészítő_Csoportbeosztás Adat)
        {
            try
            {
                string szöveg = " UPDATE csoportbeosztás SET ";
                szöveg += $" típus='{Adat.Típus}'";
                szöveg += $" WHERE csoportbeosztás='{Adat.Csoportbeosztás}'";
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


        public void Módosítás(string hely, List<Adat_Kiegészítő_Csoportbeosztás> Adat)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Kiegészítő_Csoportbeosztás rekord in Adat)
                {
                    string szöveg = " UPDATE csoportbeosztás SET ";
                    szöveg += $" típus='{rekord.Típus}'";
                    szöveg += $" WHERE csoportbeosztás='{rekord.Csoportbeosztás}'";
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


        private long Sorszám(string hely)
        {
            long Válasz = 1;
            try
            {
                List<Adat_Kiegészítő_Csoportbeosztás> Adatok = Lista_Adatok(hely);
                if (Adatok != null && Adatok.Count > 0) Válasz = Adatok.Max(a => a.Sorszám) + 1;
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

        public void Törlés(string hely, long Sorszám)
        {
            try
            {
                string szöveg = $" DELETE FROM csoportbeosztás WHERE sorszám={Sorszám}";
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

        public void Csere(string hely, long Sorszám1, long Sorszám2)
        {
            try
            {
                List<Adat_Kiegészítő_Csoportbeosztás> Adatok = Lista_Adatok(hely);
                Adat_Kiegészítő_Csoportbeosztás Adat1 = Adatok.Find(a => a.Sorszám == Sorszám1);
                Adat_Kiegészítő_Csoportbeosztás Adat2 = Adatok.Find(a => a.Sorszám == Sorszám2);
                if (Adat1 != null && Adat2 != null)
                {
                    string szöveg = $" UPDATE csoportbeosztás SET ";
                    szöveg += $" sorszám={Adat2.Sorszám}";
                    szöveg += $" WHERE csoportbeosztás='{Adat1.Csoportbeosztás}'";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                    szöveg = $" UPDATE csoportbeosztás SET ";
                    szöveg += $" sorszám={Adat1.Sorszám}";
                    szöveg += $" WHERE csoportbeosztás='{Adat2.Csoportbeosztás}'";
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

        public void SorszámEllenőrzés(string hely)
        {
            try
            {
                List<Adat_Kiegészítő_Csoportbeosztás> AdatokÖ = Lista_Adatok(hely);

                int i = 1;

                List<string> SzövegGy = new List<string>();
                foreach (Adat_Kiegészítő_Csoportbeosztás rekord in AdatokÖ)
                {
                    long ideig = rekord.Sorszám - 1;
                    if (i != ideig)
                    {   //Ha a sorszám nem a következő akkor módosítjuk

                        string szöveg = "UPDATE csoportbeosztás  SET ";
                        szöveg += $"sorszám={i + 1}";
                        szöveg += $" WHERE csoportbeosztás='{rekord.Csoportbeosztás}' AND  Típus='{rekord.Típus}'";
                        SzövegGy.Add(szöveg);
                    }
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
    }
}

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
    public class Kezelő_Munka_Folyamat
    {
        readonly string jelszó = "kismalac";
        string hely;

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Munkalap\munkalap{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Munkalap_tábla(hely.KönyvSzerk());
        }

        public List<Adat_Munka_Folyamat> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = "SELECT * FROM folyamattábla ORDER BY id";
            List<Adat_Munka_Folyamat> Adatok = new List<Adat_Munka_Folyamat>();
            Adat_Munka_Folyamat Adat;

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
                                Adat = new Adat_Munka_Folyamat(
                                          rekord["ID"].ToÉrt_Long(),
                                          rekord["rendelésiszám"].ToStrTrim(),
                                          rekord["azonosító"].ToStrTrim(),
                                          rekord["munkafolyamat"].ToStrTrim(),
                                          rekord["látszódik"].ToÉrt_Bool()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        private long Sorszám(string Telephely, int Év)
        {
            long Válasz = 1;
            try
            {
                List<Adat_Munka_Folyamat> Adatok = Lista_Adatok(Telephely, Év);
                if (Adatok != null && Adatok.Count > 0) Válasz = Adatok.Max(x => x.ID) + 1;
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

        public void Módosítás(string Telephely, int Év, Adat_Munka_Folyamat Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = " UPDATE  folyamattábla SET ";
                szöveg += $" Rendelésiszám='{Adat.Rendelésiszám}', ";
                szöveg += $" azonosító='{Adat.Azonosító}', ";
                szöveg += $" munkafolyamat='{Adat.Munkafolyamat}', ";
                szöveg += $" látszódik={Adat.Látszódik} ";
                szöveg += $" WHERE id={Adat.ID}";
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

        public void Módosítás(string Telephely, int Év, long sorszám, bool látszódik)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $" UPDATE folyamattábla SET látszódik={látszódik} WHERE id={sorszám}";
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

        public void Rögzítés(string Telephely, int Év, Adat_Munka_Folyamat Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = "INSERT INTO folyamattábla (id, Rendelésiszám, azonosító, munkafolyamat, látszódik)  VALUES (";
                szöveg += $"{Sorszám(Telephely, Év)}, ";
                szöveg += $"'{Adat.Rendelésiszám}', ";
                szöveg += $"'{Adat.Azonosító}', ";
                szöveg += $"'{Adat.Munkafolyamat}', ";
                szöveg += $" {Adat.Látszódik} ) ";
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

        public void Törlés(string Telephely, int Év)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = "DELETE FROM folyamattábla WHERE látszódik=false";
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

        public void AdatbázisLétrehozás(string Telephely, int Év)
        {
            try
            {
                // ha nincs olyan évi adatbázis, akkor létrehozzuk az előző évi alapján ha van.
                string helyi = $@"{Application.StartupPath}\{Telephely}\Adatok\Munkalap\munkalap{Év}.mdb";
                if (!File.Exists(hely))
                {
                    Adatbázis_Létrehozás.Munkalap_tábla(helyi);
                    //HA Van előző évi akkor az adatokat átmásoljuk
                    hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Munkalap\munkalap{Év - 1}.mdb";
                    if (File.Exists(hely))
                    {
                        Folyamat_Átír(Telephely, Év);
                        Munkarend_Átír(Telephely, Év);
                        Szolgálat_Átír(Telephely, Év);
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

        private void Munkarend_Átír(string Telephely, int Év)
        {
            string helyi = $@"{Application.StartupPath}\{Telephely}\Adatok\Munkalap\munkalap{Év - 1}.mdb";
            Kezelő_MunkaRend kéz = new Kezelő_MunkaRend();
            List<Adat_MunkaRend> AdatokÖ = kéz.Lista_Adatok(Telephely, Év - 1);
            List<Adat_MunkaRend> Adatok = (from a in AdatokÖ
                                           where a.Látszódik == true
                                           select a).ToList();

            helyi = $@"{Application.StartupPath}\{Telephely}\Adatok\Munkalap\munkalap{Év}.mdb";
            int id = 0;

            List<string> SzövegGy = new List<string>();
            foreach (Adat_MunkaRend rekord in Adatok)
            {
                // új adat rögzítése
                id++;
                string szöveg = "INSERT INTO munkarendtábla (id, munkarend, látszódik)  VALUES (";
                szöveg += id + ", ";
                szöveg += "'" + rekord.Munkarend.Trim() + "', ";
                szöveg += " true ) ";
                SzövegGy.Add(szöveg);
            }
            MyA.ABMódosítás(helyi, jelszó, SzövegGy);
        }

        private void Folyamat_Átír(string Telephely, int Év)
        {
            try
            {
                string helyi = $@"{Application.StartupPath}\{Telephely} \Adatok\Munkalap\munkalap {Év - 1}.mdb";
                List<Adat_Munka_Folyamat> AdatokÖ = Lista_Adatok(Telephely, Év - 1);
                List<Adat_Munka_Folyamat> Adatok = (from a in AdatokÖ
                                                    where a.Látszódik == true
                                                    select a).ToList();
                int id = 0;

                helyi = $@"{Application.StartupPath}\{Telephely}\Adatok\Munkalap\munkalap{Év}.mdb";

                List<string> SzövegGy = new List<string>();
                foreach (Adat_Munka_Folyamat rekord in Adatok)
                {
                    // új adat rögzítése
                    id++;
                    string szöveg = "INSERT INTO folyamattábla (id, Rendelésiszám, azonosító, munkafolyamat, látszódik)  VALUES (";
                    szöveg += id + ", ";
                    szöveg += "'" + rekord.Rendelésiszám.Trim() + "', ";
                    szöveg += "'" + rekord.Azonosító.Trim() + "', ";
                    szöveg += "'" + rekord.Munkafolyamat.Trim() + "', ";
                    szöveg += " true ) ";
                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(helyi, jelszó, SzövegGy);
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

        private void Szolgálat_Átír(string Telephely, int Év)
        {
            try
            {
                string helyi = $@"{Application.StartupPath}\{Telephely} \Adatok\Munkalap\munkalap {Év - 1}.mdb";
                string szöveg = "SELECT * FROM szolgálattábla";
                string jelszó = "kismalac";

                Kezelő_Munka_Szolgálat KézSzolgálat = new Kezelő_Munka_Szolgálat();
                List<Adat_Munka_Szolgálat> Adatok = KézSzolgálat.Lista_Adatok(helyi);

                helyi = $@"{Application.StartupPath}\{Telephely} \Adatok\Munkalap\munkalap {Év}.mdb";

                List<string> SzövegGy = new List<string>();
                foreach (Adat_Munka_Szolgálat rekord in Adatok)
                {
                    szöveg = "INSERT INTO szolgálattábla (költséghely, szolgálat, üzem, A1, A2, A3, A4, A5, A6, A7)  VALUES (";
                    szöveg += $"'{rekord.Költséghely}',";
                    szöveg += $"'{rekord.Szolgálat}',";
                    szöveg += $"'{rekord.Üzem}',";
                    szöveg += " '0', '0', '0', '0', '0', '0', '0' )";
                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(helyi, jelszó, SzövegGy);
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

        public void ÚjraSorszámoz(string Telephely, int Év)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                List<Adat_Munka_Folyamat> Adatok = Lista_Adatok(Telephely, Év);

                List<string> SzövegGy = new List<string>();
                long i = 0;
                foreach (Adat_Munka_Folyamat elem in Adatok)
                {
                    i++;
                    string szöveg = $"UPDATE folyamattábla SET id={i} WHERE munkafolyamat='{elem.Munkafolyamat}'";
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

        public void MódosításRendelés(string Telephely, int Év, string Rendelés, string Újrendelés)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"UPDATE folyamattábla SET Rendelésiszám='{Újrendelés}' WHERE Rendelésiszám='{Rendelés}'";
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

        public void MódosításPálya(string Telephely, int Év, string Pályaszám, string ÚjPályaszám)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"UPDATE folyamattábla SET azonosító='{ÚjPályaszám}' WHERE azonosító='{Pályaszám}'";
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

        public void Csere(string Telephely, int Év, long sorszámElső, long sorszámMásodik)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"UPDATE folyamattábla SET id=0 WHERE id={sorszámMásodik}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                szöveg = $"UPDATE folyamattábla SET id={sorszámMásodik} WHERE id={sorszámElső}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                szöveg = $"UPDATE folyamattábla SET id={sorszámElső} WHERE id={0}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                szöveg = "DELETE FROM folyamattábla WHERE id=0";
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

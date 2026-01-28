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
    public class Kezelő_Osztály_Név
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\osztály.mdb";
        readonly string jelszó = "kéménybe";


        public Kezelő_Osztály_Név()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Osztálytábla(hely.KönyvSzerk());
        }

        public List<Adat_Osztály_Név> Lista_Adat()
        {
            string szöveg = $"SELECT * FROM osztálytábla order by id";
            List<Adat_Osztály_Név> Adatok = new List<Adat_Osztály_Név>();
            Adat_Osztály_Név Adat;

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
                                Adat = new Adat_Osztály_Név(
                                     MyF.Érték_INT(rekord["id"].ToStrTrim()),
                                     rekord["Osztálynév"].ToStrTrim(),
                                     rekord["Osztálymező"].ToStrTrim(),
                                     rekord["Használatban"].ToÉrt_Bool()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(Adat_Osztály_Név Adat)
        {
            try
            {
                string szöveg = $"UPDATE  osztálytábla SET";
                szöveg += $" osztálynév='{Adat.Osztálynév}', ";
                szöveg += $" osztálymező='{Adat.Osztálymező}', ";
                szöveg += $" használatban={Adat.Használatban} ";
                szöveg += $" where id={Adat.Id} ";
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

        private void Rögzítés(Adat_Osztály_Név Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO osztálytábla (id, osztálynév, osztálymező, használatban) VALUES (";
                szöveg += $"{Adat.Id}, ";
                szöveg += $"'{Adat.Osztálynév}', ";
                szöveg += $"'{Adat.Osztálymező}', ";
                szöveg += $"{Adat.Használatban}) ";
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

        public void ÚjMező()
        {
            try
            {
                //Megkeressük, hogy melyik az utolsó Mezőnév
                List<Adat_Osztály_Név> Adatok = Lista_Adat().OrderBy(a => a.Osztálymező).ToList();
                int sorszám = 0;
                foreach (Adat_Osztály_Név elem in Adatok)
                    if (elem.Osztálymező.Substring(4).ToÉrt_Int() > sorszám) sorszám = elem.Osztálymező.Substring(4).ToÉrt_Int();

                sorszám++;

                //Létrehozzuk az új mezőt
                string Mezőnév = $"Adat{sorszám}";
                AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
                ADAT.AB_Új_Oszlop(hely, jelszó, "osztályadatok", Mezőnév, "MEMO");

                //Rögzítjük a mezőnevet az Osztálytáblában
                Adat_Osztály_Név Adat = new Adat_Osztály_Név(
                                    Sorszám(),
                                    "_",
                                    Mezőnév,
                                    false);
                Rögzítés(Adat);

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

        private int Sorszám()
        {
            int Válasz = 1;
            try
            {
                List<Adat_Osztály_Név> Adatok = Lista_Adat();
                if (Adatok != null) Válasz = Adatok.Max(a => a.Id) + 1;

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

        public string Mezőnév(string Leírás)
        {
            string Válasz = "";
            try
            {
                List<Adat_Osztály_Név> Adatok = Lista_Adat();

                Adat_Osztály_Név Adat = Adatok.Where(a => a.Osztálynév == Leírás).FirstOrDefault();
                if (Adat != null) Válasz = Adat.Osztálymező;
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

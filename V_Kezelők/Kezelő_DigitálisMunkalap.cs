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
    public class Kezelő_DigitálisMunkalap_Fej
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\DigitálisMunkalap\MunkalapAdatok.mdb";
        readonly string jelszó = "";

        public Kezelő_DigitálisMunkalap_Fej()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.DigitálisMunkalap(hely.KönyvSzerk());
        }

        private List<Adat_DigitálisMunkalap_Fej> Lista_Adatok()
        {
            List<Adat_DigitálisMunkalap_Fej> Adatok = new List<Adat_DigitálisMunkalap_Fej>();
            Adat_DigitálisMunkalap_Fej Adat;
            string szöveg = "SELECT * FROM FejTábla ";

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
                                Adat = new Adat_DigitálisMunkalap_Fej(
                                                  rekord["ID"].ToÉrt_Long(),
                                                  rekord["típus"].ToStrTrim(),
                                                  rekord["Karbantartási_fokozat"].ToStrTrim(),
                                                  rekord["EllDolgozóNév"].ToStrTrim(),
                                                  rekord["EllDolgozószám"].ToStrTrim(),
                                                  rekord["Telephely"].ToStrTrim(),
                                                  rekord["Dátum"].ToÉrt_DaTeTime()
                                                  );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public long Sorszám()
        {
            long Válasz = 1;
            try
            {
                List<Adat_DigitálisMunkalap_Fej> Adatok = Lista_Adatok();
                if (Adatok != null && Adatok.Count > 0) Válasz = Adatok.Max(x => x.Id) + 1;
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



        public void Rögzítés(Adat_DigitálisMunkalap_Fej Adat)
        {
            try
            {
                string szöveg = "INSERT INTO FejTábla (Id, Típus, Karbantartási_fokozat, EllDolgozóNév, EllDolgozószám, Telephely, Dátum) ";
                szöveg += " VALUES (";
                szöveg += $"{Adat.Id}, ";//Id
                szöveg += $"'{Adat.Típus}', ";//Típus
                szöveg += $"'{Adat.Karbantartási_fokozat}', ";//Karbantartási_fokozat
                szöveg += $"'{Adat.EllDolgozóNév}', ";//EllDolgozóNév
                szöveg += $"'{Adat.EllDolgozószám}', ";//EllDolgozószám
                szöveg += $"'{Adat.Telephely}', ";//Telephely
                szöveg += $"'{Adat.Dátum:yyyy.MM.dd}') ";//Dátum

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
    }

}

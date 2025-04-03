using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_DigitálisMunkalap_Kocsik
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\DigitálisMunkalap\MunkalapAdatok.mdb";
        readonly string jelszó = "";
        public Kezelő_DigitálisMunkalap_Kocsik()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.DigitálisMunkalap(hely.KönyvSzerk());
        }

        public List<Adat_DigitálisMunkalap_Kocsik> Lista_Adatok()
        {
            List<Adat_DigitálisMunkalap_Kocsik> Adatok = new List<Adat_DigitálisMunkalap_Kocsik>();
            Adat_DigitálisMunkalap_Kocsik Adat;
            string szöveg = "SELECT * FROM KocsikTábla ";

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
                                Adat = new Adat_DigitálisMunkalap_Kocsik(
                                                  rekord["Fej_Id"].ToÉrt_Long(),
                                                  rekord["Azonosító"].ToStrTrim(),
                                                  rekord["KMU"].ToÉrt_Long(),
                                                  rekord["Rendelés"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(List<Adat_DigitálisMunkalap_Kocsik> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_DigitálisMunkalap_Kocsik Adat in Adatok)
                {
                    string szöveg = "INSERT  INTO KocsikTábla (Fej_Id, Azonosító, KMU, Rendelés) ";
                    szöveg += " VALUES (";
                    szöveg += $"{Adat.Fej_Id}, ";
                    szöveg += $"'{Adat.Azonosító}',";
                    szöveg += $"{Adat.KMU},";
                    szöveg += $"'{Adat.Rendelés}')";
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
    }

}

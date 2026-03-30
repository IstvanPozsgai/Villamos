using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{


    public class Kezelő_DigitálisMunkalap_Dolgozó
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\DigitálisMunkalap\MunkalapAdatok.mdb";
        readonly string jelszó = "";
        readonly string táblanév = "DolgozóTábla";
        public Kezelő_DigitálisMunkalap_Dolgozó()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.DigitálisMunkalap(hely.KönyvSzerk());
        }

        public List<Adat_DigitálisMunkalap_Dolgozó> Lista_Adatok()
        {
            List<Adat_DigitálisMunkalap_Dolgozó> Adatok = new List<Adat_DigitálisMunkalap_Dolgozó>();
            Adat_DigitálisMunkalap_Dolgozó Adat;
            string szöveg = $"SELECT * FROM {táblanév} ";

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
                                Adat = new Adat_DigitálisMunkalap_Dolgozó(
                                                  rekord["DolgozóNév"].ToStrTrim(),
                                                  rekord["Dolgozószám"].ToStrTrim(),
                                                  rekord["Fej_Id"].ToÉrt_Long(),
                                                  rekord["Technológia_Id"].ToÉrt_Long()
                                                  );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(List<Adat_DigitálisMunkalap_Dolgozó> Adatok)
        {
            try
            {

                List<string> SzövegGy = new List<string>();
                foreach (Adat_DigitálisMunkalap_Dolgozó Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév} (Fej_Id, DolgozóNév, Dolgozószám, Technológia_Id) ";
                    szöveg += " VALUES (";
                    szöveg += $"{Adat.Fej_Id}, ";  //Fej_Id
                    szöveg += $"'{Adat.DolgozóNév}', ";  //DolgozóNév
                    szöveg += $"'{Adat.Dolgozószám}', ";  //Dolgozószám
                    szöveg += $"{Adat.Technológia_Id}) ";  //Technológia_Id
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

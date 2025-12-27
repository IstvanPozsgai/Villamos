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
    public class Kezelő_Sérülés_Jelentés
    {
        string hely;
        readonly string jelszó = "tükör";
        readonly string táblanév = "költség";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Év}\sérülés{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely.KönyvSzerk());
        }

        public List<Adat_Sérülés_Jelentés> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Sérülés_Jelentés> Adatok = new List<Adat_Sérülés_Jelentés>();
            Adat_Sérülés_Jelentés Adat;

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
                                Adat = new Adat_Sérülés_Jelentés(
                                           rekord["Sorszám"].ToÉrt_Int(),
                                           rekord["Telephely"].ToStrTrim(),
                                           rekord["Dátum"].ToÉrt_DaTeTime(),
                                           rekord["Balesethelyszín"].ToStrTrim(),
                                           rekord["Viszonylat"].ToStrTrim(),
                                           rekord["Rendszám"].ToStrTrim(),
                                           rekord["Járművezető"].ToStrTrim(),
                                           rekord["Rendelésszám"].ToÉrt_Int(),
                                           rekord["Kimenetel"].ToÉrt_Int(),
                                           rekord["Státus"].ToÉrt_Int(),
                                           rekord["Iktatószám"].ToStrTrim(),
                                           rekord["Típus"].ToStrTrim(),
                                           rekord["Szerelvény"].ToStrTrim(),
                                           rekord["Forgalmiakadály"].ToÉrt_Int(),
                                           rekord["Műszaki"].ToÉrt_Bool(),
                                           rekord["Anyagikár"].ToÉrt_Bool(),
                                           rekord["Biztosító"].ToStrTrim(),
                                           rekord["Személyisérülés"].ToÉrt_Bool(),
                                           rekord["Személyisérülés1"].ToÉrt_Bool(),
                                           rekord["Biztosítóidő"].ToÉrt_Int(),
                                           rekord["Mivelütközött"].ToStrTrim(),
                                           rekord["Anyagikárft"].ToÉrt_Int(),
                                           rekord["Leírás"].ToStrTrim(),
                                           rekord["Leírás1"].ToStrTrim(),
                                           rekord["Balesethelyszín1"].ToStrTrim(),
                                           rekord["Esemény"].ToStrTrim(),
                                           rekord["Anyagikárft1"].ToÉrt_Int(),
                                           rekord["Státus1"].ToÉrt_Int(),
                                           rekord["Kmóraállás"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Státus1Elk(int Év, List<Adat_Sérülés_Jelentés> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGY = new List<string>();
                foreach (Adat_Sérülés_Jelentés Adat in Adatok)
                {
                    string szöveg = "UPDATE jelentés  SET ";
                    szöveg += $" státus1={Adat.Státus1} ";
                    szöveg += $" WHERE [sorszám]={Adat.Sorszám}";
                    SzövegGY.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGY);
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

        public void VisszaÁllít(int Év, int Sorszám)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = "UPDATE jelentés  SET ";
                szöveg += $" státus=1 ";
                szöveg += $" státus1=1 ";
                szöveg += $" WHERE [sorszám]={Sorszám}";
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

        public void Módosítás(int Év, Adat_Sérülés_Jelentés Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = "UPDATE jelentés  SET ";
                szöveg += $"Telephely='{Adat.Telephely}', ";
                szöveg += $"Dátum='{Adat.Dátum}', ";
                szöveg += $"Balesethelyszín='{Adat.Balesethelyszín}', ";
                szöveg += $"Viszonylat='{Adat.Viszonylat}', ";
                szöveg += $"Rendszám='{Adat.Rendszám}', ";
                szöveg += $"járművezető='{Adat.Járművezető}', ";
                szöveg += $"Rendelésszám={Adat.Rendelésszám}, ";
                szöveg += $"státus={Adat.Státus}, ";
                szöveg += $"kimenetel={Adat.Kimenetel}, ";
                szöveg += $"Státus1={Adat.Státus1}, ";
                szöveg += $"iktatószám='{Adat.Iktatószám}', ";
                szöveg += $"Típus='{Adat.Típus}', ";
                szöveg += $"Szerelvény='{Adat.Szerelvény}',";
                szöveg += $"forgalmiakadály={Adat.Forgalmiakadály}, ";
                szöveg += $"műszaki={Adat.Műszaki}, ";
                szöveg += $"anyagikár={Adat.Anyagikár}, ";
                szöveg += $"biztosító='{Adat.Biztosító}', ";
                szöveg += $"személyisérülés={Adat.Személyisérülés}, ";
                szöveg += $"személyisérülés1={Adat.Személyisérülés1}, ";
                szöveg += $"biztosítóidő={Adat.Biztosítóidő}, ";
                szöveg += $"mivelütközött='{Adat.Mivelütközött}', ";
                szöveg += $"anyagikárft={Adat.Anyagikárft}, ";
                szöveg += $"Leírás='{Adat.Leírás}', ";
                szöveg += $"Leírás1='{Adat.Leírás1}', ";
                szöveg += $"Balesethelyszín1='{Adat.Balesethelyszín1}', ";
                szöveg += $"esemény='{Adat.Esemény}', ";
                szöveg += $"anyagikárft1={Adat.Anyagikárft1}, ";
                szöveg += $"kmóraállás='{Adat.Kmóraállás}' ";
                szöveg += $" WHERE [sorszám]={Adat.Sorszám}";
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

        public void Rögzítés(int Év, Adat_Sérülés_Jelentés Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = "INSERT INTO jelentés  (sorszám, Telephely, Dátum, Balesethelyszín, ";
                szöveg += "Viszonylat, Rendszám, járművezető,  Rendelésszám, ";
                szöveg += "státus, kimenetel, Státus1, iktatószám, ";
                szöveg += "Típus, Szerelvény, forgalmiakadály, műszaki, ";
                szöveg += "anyagikár, biztosító, személyisérülés, személyisérülés1, ";
                szöveg += "biztosítóidő, mivelütközött, anyagikárft, Leírás,";
                szöveg += "Leírás1, Balesethelyszín1, esemény, anyagikárft1, ";
                szöveg += "kmóraállás ) VALUES (";
                szöveg += $"{Adat.Sorszám}, ";
                szöveg += $"'{Adat.Telephely}', ";
                szöveg += $"'{Adat.Dátum}', ";
                szöveg += $"'{Adat.Balesethelyszín}', ";

                szöveg += $"'{Adat.Viszonylat}', ";
                szöveg += $"'{Adat.Rendszám}', ";
                szöveg += $"'{Adat.Járművezető}', ";
                szöveg += $"{Adat.Rendelésszám}, ";

                szöveg += $"{Adat.Státus}, ";
                szöveg += $"{Adat.Kimenetel}, ";
                szöveg += $"{Adat.Státus1}, ";
                szöveg += $"'{Adat.Iktatószám}', ";

                szöveg += $"'{Adat.Típus}', ";
                szöveg += $"'{Adat.Szerelvény}',";
                szöveg += $"{Adat.Forgalmiakadály}, ";
                szöveg += $"{Adat.Műszaki}, ";

                szöveg += $"{Adat.Anyagikár}, ";
                szöveg += $"'{Adat.Biztosító}', ";
                szöveg += $"{Adat.Személyisérülés}, ";
                szöveg += $"{Adat.Személyisérülés1}, ";

                szöveg += $"{Adat.Biztosítóidő}, ";
                szöveg += $"'{Adat.Mivelütközött}', ";
                szöveg += $"{Adat.Anyagikárft}, ";
                szöveg += $"'{Adat.Leírás}', ";

                szöveg += $"'{Adat.Leírás1}', ";
                szöveg += $"'{Adat.Balesethelyszín1}', ";
                szöveg += $"'{Adat.Esemény}', ";
                szöveg += $"{Adat.Anyagikárft1}, ";
                szöveg += $"'{Adat.Kmóraállás}') ";

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

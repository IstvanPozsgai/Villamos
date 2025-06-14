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
    public class Kezelő_MenetKimaradás_Főmérnökség
    {
        readonly string jelszó = "lilaakác";
        readonly string táblanév = "menettábla";
        string hely;

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Év}\{Év}_menet_adatok.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Menekimaradás_Főmérnökség(hely.KönyvSzerk());
        }

        public List<Adat_Menetkimaradás_Főmérnökség> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            List<Adat_Menetkimaradás_Főmérnökség> Adatok = new List<Adat_Menetkimaradás_Főmérnökség>();
            Adat_Menetkimaradás_Főmérnökség Adat;
            string szöveg = $"SELECT * FROM {táblanév}";

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
                                Adat = new Adat_Menetkimaradás_Főmérnökség(
                                    rekord["viszonylat"].ToStrTrim(),
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["eseményjele"].ToStrTrim(),
                                    rekord["bekövetkezés"].ToÉrt_DaTeTime(),
                                    rekord["kimaradtmenet"].ToÉrt_Long(),
                                    rekord["jvbeírás"].ToStrTrim(),
                                    rekord["vmbeírás"].ToStrTrim(),
                                    rekord["javítás"].ToStrTrim(),
                                    rekord["id"].ToÉrt_Long(),
                                    rekord["törölt"].ToÉrt_Bool(),
                                    rekord["jelentés"].ToStrTrim(),
                                    rekord["tétel"].ToÉrt_Long(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["szolgálat"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(int Év, List<Adat_Menetkimaradás_Főmérnökség> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<Adat_Menetkimaradás_Főmérnökség> AdatokB = Lista_Adatok(Év);
                List<string> szövegGy = new List<string>();
                foreach (Adat_Menetkimaradás_Főmérnökség rekord in Adatok)
                {
                    Adat_Menetkimaradás_Főmérnökség jvbeírás = (from a in AdatokB
                                                                where a.Tétel == rekord.Tétel && a.Jelentés == rekord.Jelentés
                                                                select a).FirstOrDefault();
                    if (jvbeírás == null)
                    {
                        // ha nincs a főmérnökségi táblába akkor rögzítjük
                        string szöveg = $"INSERT INTO {táblanév} ";
                        // rekord nevek
                        szöveg += "(viszonylat, azonosító, típus, Eseményjele, Bekövetkezés, kimaradtmenet, jvbeírás, javítás, jelentés, tétel, ";
                        szöveg += " vmbeírás, id, telephely, szolgálat, törölt )";
                        szöveg += " VALUES  ( ";
                        // értékek
                        szöveg += $"'{rekord.Viszonylat}', ";
                        szöveg += $"'{rekord.Azonosító}', ";
                        szöveg += $"'{rekord.Típus}', ";
                        szöveg += $"'{rekord.Eseményjele}', ";
                        szöveg += $"'{rekord.Bekövetkezés}', ";
                        szöveg += $"{rekord.Kimaradtmenet}, ";
                        szöveg += $"'{rekord.Jvbeírás.Replace('"', '°').Replace('\'', '°')}', ";
                        szöveg += $"'{rekord.Javítás.Replace('"', '°').Replace('\'', '°')}', ";
                        szöveg += $"'{rekord.Jelentés}', ";
                        szöveg += $"{rekord.Tétel}, ";
                        szöveg += $"'{rekord.Vmbeírás}' , ";
                        szöveg += "0, ";
                        szöveg += $"'{rekord.Telephely}', ";
                        szöveg += $"'{rekord.Szolgálat}', ";
                        szöveg += $"{rekord.Törölt})";
                        szövegGy.Add(szöveg);
                    }
                    MyA.ABMódosítás(hely, jelszó, szövegGy);
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

        public void Törlés(int Év, DateTime Dátumtól, DateTime Dátumig)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = $"DELETE * FROM {táblanév} WHERE bekövetkezés>=#{Dátumtól:MM-dd-yyyy} 00:00:0#";
                szöveg += $" and bekövetkezés<=#{Dátumig:MM-dd-yyyy} 23:59:59#";
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

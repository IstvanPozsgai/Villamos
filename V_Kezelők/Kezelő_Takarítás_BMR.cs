using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Takarítás_BMR
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Takarítás\BMR.mdb";
        readonly string jelszó = "seprűéslapát";
        readonly string táblanév = "TakarításBMR";

        public Kezelő_Takarítás_BMR()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.TakarításBMRlétrehozás(hely.KönyvSzerk());
        }

        public List<Adat_Takarítás_BMR> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév} ORDER BY ID";
            List<Adat_Takarítás_BMR> Adatok = new List<Adat_Takarítás_BMR>();
            Adat_Takarítás_BMR Adat;

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
                                Adat = new Adat_Takarítás_BMR(
                                        rekord["Id"].ToÉrt_Int(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["JárműÉpület"].ToStrTrim(),
                                        rekord["BMRszám"].ToStrTrim(),
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

        public void Rögzít(Adat_Takarítás_BMR Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} (Id, Telephely, JárműÉpület, BMRszám, Dátum) VALUES (";
                szöveg += $"{Adat.Id}, '{Adat.Telephely}', '{Adat.JárműÉpület}', '{Adat.BMRszám}', '{Adat.Dátum}')";
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


        public void Rögzít(List<Adat_Takarítás_BMR> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Takarítás_BMR Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév} (Id, Telephely, JárműÉpület, BMRszám, Dátum) VALUES (";
                    szöveg += $"{Adat.Id}, '{Adat.Telephely}', '{Adat.JárműÉpület}', '{Adat.BMRszám}', '{Adat.Dátum}')";
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


        public void Módosít(Adat_Takarítás_BMR Adat)
        {
            string szöveg = $"UPDATE {táblanév}  SET ";
            //szöveg += $"Dátum='{Adat.Dátum.ToShortDateString()}', ";
            //szöveg += $"Telephely='{Adat.Telephely}', ";
            //szöveg += $"JárműÉpület='{Adat.JárműÉpület}', ";
            szöveg += $"BMRszám='{Adat.BMRszám}' ";
            szöveg += $" WHERE id={Adat.Id}";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Módosít(List<Adat_Takarítás_BMR> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Takarítás_BMR Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév}  SET ";
                    //szöveg += $"Dátum='{Adat.Dátum.ToShortDateString()}', ";
                    //szöveg += $"Telephely='{Adat.Telephely}', ";
                    //szöveg += $"JárműÉpület='{Adat.JárműÉpület}', ";
                    szöveg += $"BMRszám='{Adat.BMRszám}' ";
                    szöveg += $" WHERE id={Adat.Id}";
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

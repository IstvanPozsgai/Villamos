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
    public class Kezelő_Sérülés_Költség
    {
        string hely;
        readonly string jelszó = "tükör";
        readonly string táblanév = "költség";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Év}\sérülés{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely.KönyvSzerk());
        }

        public List<Adat_Sérülés_Költség> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            string szöveg = $"SELECT * FROM {táblanév} ORDER BY Rendelés";
            List<Adat_Sérülés_Költség> Adatok = new List<Adat_Sérülés_Költség>();
            Adat_Sérülés_Költség Adat;

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
                                Adat = new Adat_Sérülés_Költség(
                                           rekord["Rendelés"].ToÉrt_Int(),
                                           rekord["Anyagköltség"].ToÉrt_Int(),
                                           rekord["Munkaköltség"].ToÉrt_Int(),
                                           rekord["Gépköltség"].ToÉrt_Int(),
                                           rekord["Szolgáltatás"].ToÉrt_Int(),
                                           rekord["Státus"].ToÉrt_Int());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public void Törlés(int Év, List<double> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGy = new List<string>();
                foreach (double Adat in Adatok)
                {
                    string szöveg = $"DELETE FROM {táblanév} WHERE rendelés={Adat}";
                    SzövegGy.Add(szöveg);
                }
                if (SzövegGy.Count > 0) MyA.ABtörlés(hely, jelszó, SzövegGy);
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


        public void Rögzítés(int Év, List<Adat_Sérülés_Költség> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Sérülés_Költség Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév} (rendelés, anyagköltség, munkaköltség, gépköltség, szolgáltatás, státus ) VALUES (";
                    szöveg += $"{Adat.Rendelés}, ";
                    szöveg += $"{Adat.Anyagköltség}, ";
                    szöveg += $"{Adat.Munkaköltség}, ";
                    szöveg += $"{Adat.Gépköltség}, ";
                    szöveg += $"{Adat.Szolgáltatás}, ";
                    szöveg += $"{Adat.Státus}) ";
                    SzövegGy.Add(szöveg);
                }
                if (SzövegGy.Count > 0) MyA.ABMódosítás(hely, jelszó, SzövegGy);
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

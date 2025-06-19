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
    public class Kezelő_TTP_Naptár
    {
        readonly string hely = $@"{Application.StartupPath}/Főmérnökség/adatok/TTP/TTP_Adatbázis.mdb";
        readonly string jelszó = "rudolfg";

        public Kezelő_TTP_Naptár()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.TTP_Adatbázis(hely.KönyvSzerk());
        }


        public List<Adat_TTP_Naptár> Lista_Adatok()
        {
            List<Adat_TTP_Naptár> Adatok = new List<Adat_TTP_Naptár>();
            Adat_TTP_Naptár Adat;
            string szöveg = "SELECT * FROM TTP_Naptár";
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
                                Adat = new Adat_TTP_Naptár(
                                        rekord["Dátum"].ToÉrt_DaTeTime(),
                                        rekord["Munkanap"].ToÉrt_Bool());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(Adat_TTP_Naptár Adat)
        {
            try
            {
                string szöveg = $"UPDATE TTP_Naptár SET Munkanap={Adat.Munkanap} WHERE Dátum=#{Adat.Dátum:MM-dd-yyyy}# ";
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


        public void Rögzítés(List<Adat_TTP_Naptár> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_TTP_Naptár Adat in Adatok)
                {
                    string szöveg = "INSERT INTO TTP_Naptár (Dátum, Munkanap) VALUES (";
                    szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', {Adat.Munkanap})";
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

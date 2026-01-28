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
    public class Kezelő_TTP_Év
    {
        readonly string hely = $@"{Application.StartupPath}/Főmérnökség/adatok/TTP/TTP_Adatbázis.mdb";
        readonly string jelszó = "rudolfg";

        public Kezelő_TTP_Év()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.TTP_Adatbázis(hely.KönyvSzerk());
        }


        public void Rögzítés(Adat_TTP_Év Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO TTP_Év (Életkor, Év) ";
                szöveg += "VALUES (";
                szöveg += $" {Adat.Életkor}, ";
                szöveg += $" {Adat.Év} )";
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


        public void Módosítás(Adat_TTP_Év Adat)
        {
            try
            {
                string szöveg = $"UPDATE TTP_Év SET Év={Adat.Év}";
                szöveg += $" WHERE Életkor={Adat.Életkor}";
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


        public void Törlés(int Életkor)
        {
            try
            {
                string szöveg = $"DELETE FROM TTP_Év WHERE Életkor={Életkor}";
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


        public List<Adat_TTP_Év> Lista_Adatok()
        {
            List<Adat_TTP_Év> Adatok = new List<Adat_TTP_Év>();
            Adat_TTP_Év Adat;
            string szöveg = $"SELECT * FROM TTP_Év  ORDER BY Életkor";
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
                                Adat = new Adat_TTP_Év(
                                        rekord["Év"].ToÉrt_Int(),
                                        rekord["Életkor"].ToÉrt_Int());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }

}

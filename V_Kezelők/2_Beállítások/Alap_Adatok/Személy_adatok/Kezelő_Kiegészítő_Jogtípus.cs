using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Jogtípus
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kiegészítő2.mdb";
        readonly string jelszó = "Mocó";
        readonly string táblanév = "jogtípus";

        public Kezelő_Kiegészítő_Jogtípus()
        {
            //nincs elkészítve
            // if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Adatok_Napló(hely.KönyvSzerk());
        }

        public List<Adat_Kiegészítő_Jogtípus> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM  {táblanév} order by  sorszám";
            Adat_Kiegészítő_Jogtípus Adat;
            List<Adat_Kiegészítő_Jogtípus> Adatok = new List<Adat_Kiegészítő_Jogtípus>();

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
                                Adat = new Adat_Kiegészítő_Jogtípus(
                                           rekord["sorszám"].ToÉrt_Long(),
                                           rekord["típus"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Kiegészítő_Jogtípus Adat)
        {
            try
            {
                // új rögtzítés
                string szöveg = $"INSERT INTO {táblanév} ( típus ) VALUES ( '{Adat.Típus}' )";
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

        public void Módosítás(Adat_Kiegészítő_Jogtípus Adat)
        {
            try
            {
                // módosítás
                string szöveg = $"UPDATE {táblanév}  SET ";
                szöveg += $" típus='{Adat.Típus}' ";
                szöveg += $"WHERE sorszám={Adat.Sorszám}";
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

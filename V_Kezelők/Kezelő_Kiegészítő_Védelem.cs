using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;
namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Védelem
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kiegészítő2.mdb".KönyvSzerk();
        readonly string jelszó = "Mocó";

        public Kezelő_Kiegészítő_Védelem()
        {
            //nincs elkészítve
            // if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Adatok_Napló(hely.KönyvSzerk());
        }

        public List<Adat_Kiegészítő_Védelem> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM védelem  order by  sorszám";
            List<Adat_Kiegészítő_Védelem> Adatok = new List<Adat_Kiegészítő_Védelem>();
            Adat_Kiegészítő_Védelem Adat;

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
                                Adat = new Adat_Kiegészítő_Védelem(
                                     rekord["sorszám"].ToÉrt_Long(),
                                     rekord["megnevezés"].ToStrTrim()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Kiegészítő_Védelem Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO védelem ( sorszám, megnevezés ) VALUES ({Sorszám()}, '{Adat.Megnevezés}' )";     // új rögtzítés
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

        public void Módosítás(Adat_Kiegészítő_Védelem Adat)
        {
            try
            {
                string szöveg = $"UPDATE védelem  SET megnevezés='{Adat.Megnevezés}' WHERE sorszám={Adat.Sorszám}";     // módosítás
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

        public long Sorszám()
        {
            long Válasz = 1;
            try
            {
                List<Adat_Kiegészítő_Védelem> Adatok = Lista_Adatok();
                if (Adatok != null && Adatok.Count > 0) Válasz = Adatok.Max(x => x.Sorszám) + 1;
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

    }


}

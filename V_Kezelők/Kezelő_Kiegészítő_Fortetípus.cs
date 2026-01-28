using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{

    public class Kezelő_Kiegészítő_Fortetípus
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kiegészítő.mdb".KönyvSzerk();
        readonly string jelszó = "Mocó";
        readonly string táblanév = "fortetípus";

        public Kezelő_Kiegészítő_Fortetípus()
        {
            //nincs elkészítve
            // if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Adatok_Napló(hely.KönyvSzerk());
        }

        public List<Adat_Kiegészítő_Fortetípus> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév} order by sorszám";
            List<Adat_Kiegészítő_Fortetípus> Adatok = new List<Adat_Kiegészítő_Fortetípus>();
            Adat_Kiegészítő_Fortetípus Adat;

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
                                Adat = new Adat_Kiegészítő_Fortetípus(
                                     rekord["sorszám"].ToÉrt_Long(),
                                     rekord["ftípus"].ToStrTrim(),
                                     rekord["telephely"].ToStrTrim(),
                                     rekord["telephelyitípus"].ToStrTrim()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Kiegészítő_Fortetípus Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} ( sorszám, ftípus, telephely, telephelyitípus )";
                szöveg += $" VALUES ({Adat.Sorszám}, ";
                szöveg += $"'{Adat.Ftípus}', ";
                szöveg += $"'{Adat.Telephely}', ";
                szöveg += $"'{Adat.Telephelyitípus}' )";
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

        public void Törlés(Adat_Kiegészítő_Fortetípus Adat)
        {
            try
            {
                string szöveg = $"DELETE FROM {táblanév} where  sorszám={Adat.Sorszám}";
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

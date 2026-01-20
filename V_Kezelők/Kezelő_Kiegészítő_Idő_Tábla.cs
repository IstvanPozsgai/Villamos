using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Idő_Tábla
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kiegészítő.mdb".KönyvSzerk();
        readonly string jelszó = "Mocó";

        public Kezelő_Kiegészítő_Idő_Tábla()
        {
            //nincs elkészítve
            // if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Adatok_Napló(hely.KönyvSzerk());
        }

        public List<Adat_Kiegészítő_Idő_Tábla> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM időtábla";
            List<Adat_Kiegészítő_Idő_Tábla> Adatok = new List<Adat_Kiegészítő_Idő_Tábla>();
            Adat_Kiegészítő_Idő_Tábla Adat;

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
                                Adat = new Adat_Kiegészítő_Idő_Tábla(
                                     rekord["sorszám"].ToÉrt_Long(),
                                     rekord["reggel"].ToÉrt_DaTeTime(),
                                     rekord["este"].ToÉrt_DaTeTime(),
                                     rekord["délután"].ToÉrt_DaTeTime()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(Adat_Kiegészítő_Idő_Tábla Adat)
        {
            try
            {
                string szöveg = $"UPDATE időtábla Set ";
                szöveg += $"reggel='{Adat.Reggel}', ";
                szöveg += $"este='{Adat.Este}', ";
                szöveg += $"délután='{Adat.Délután}' ";
                szöveg += $"where sorszám={Adat.Sorszám} ";
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

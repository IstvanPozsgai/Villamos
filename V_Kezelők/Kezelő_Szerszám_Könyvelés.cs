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
    public class Kezelő_Szerszám_könvyvelés
    {
        readonly string jelszó = "csavarhúzó";
        string hely;
        readonly string táblanév = "Könyvelés";

        private void FájlBeállítás(string Melyik, string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\{Melyik}\Szerszám.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Szerszám_nyilvántartás(hely.KönyvSzerk());
        }

        public List<Adat_Szerszám_Könyvelés> Lista_Adatok(string Melyik, string Telephely)
        {
            FájlBeállítás(Melyik, Telephely);
            string szöveg = $"SELECT * FROM {táblanév} ORDER BY azonosító";
            List<Adat_Szerszám_Könyvelés> Adatok = new List<Adat_Szerszám_Könyvelés>();
            Adat_Szerszám_Könyvelés Adat;

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
                                Adat = new Adat_Szerszám_Könyvelés(
                                       rekord["mennyiség"].ToÉrt_Int(),
                                       rekord["dátum"].ToÉrt_DaTeTime(),
                                       rekord["Azonosító"].ToStrTrim(),
                                       rekord["szerszámkönyvszám"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Melyik, string Telephely, Adat_Szerszám_Könyvelés Adat)
        {
            try
            {
                FájlBeállítás(Melyik, Telephely);
                string szöveg = $"INSERT INTO {táblanév}  (azonosító, Szerszámkönyvszám, mennyiség, dátum ) VALUES (";
                szöveg += $"'{Adat.AzonosítóMás}', ";
                szöveg += $"'{Adat.SzerszámkönyvszámMás}', ";
                szöveg += $"{Adat.Mennyiség}, ";
                szöveg += $"'{DateTime.Now}') ";
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

        public void Módosítás(string Melyik, string Telephely, Adat_Szerszám_Könyvelés Adat)
        {
            try
            {
                FájlBeállítás(Melyik, Telephely);
                string szöveg = $"UPDATE {táblanév}  SET ";
                szöveg += $" mennyiség={Adat.Mennyiség}, ";
                szöveg += $" dátum='{DateTime.Now}' ";
                szöveg += $" WHERE Szerszámkönyvszám='{Adat.SzerszámkönyvszámMás}' And ";
                szöveg += $" azonosító='{Adat.AzonosítóMás}'";
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

        public void Törlés(string Melyik, string Telephely, Adat_Szerszám_Könyvelés Adat)
        {
            try
            {
                FájlBeállítás(Melyik, Telephely);
                string szöveg = $"DELETE FROM  {táblanév} ";
                szöveg += $" WHERE Szerszámkönyvszám='{Adat.SzerszámkönyvszámMás}' And ";
                szöveg += $" azonosító='{Adat.AzonosítóMás}'";
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


using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Szerszám_Könyv
    {
        readonly string jelszó = "csavarhúzó";
        string hely;
        readonly string táblanév = "könyvtörzs";

        private void FájlBeállítás(string Telephely, string Melyik)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\{Melyik}\Adatok\Szerszám.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Szerszám_nyilvántartás(hely.KönyvSzerk());
        }

        public List<Adat_Szerszám_Könyvtörzs> Lista_Adatok(string Melyik, string Telephely)
        {
            string szöveg = $"SELECT * FROM {táblanév}";
            FájlBeállítás(Melyik, Telephely);
            Adat_Szerszám_Könyvtörzs Adat;
            List<Adat_Szerszám_Könyvtörzs> Adatok = new List<Adat_Szerszám_Könyvtörzs>();
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
                                Adat = new Adat_Szerszám_Könyvtörzs(
                                           rekord["szerszámkönyvszám"].ToStrTrim(),
                                           rekord["szerszámkönyvnév"].ToStrTrim(),
                                           rekord["felelős1"].ToStrTrim(),
                                           rekord["felelős2"].ToStrTrim(),
                                           rekord["státus"].ToÉrt_Bool());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Melyik, string Telephely, Adat_Szerszám_Könyvtörzs Adat)
        {
            try
            {
                FájlBeállítás(Melyik, Telephely);
                string szöveg = $"INSERT INTO {táblanév}  (Szerszámkönyvszám, Szerszámkönyvnév, felelős1, felelős2, státus ) VALUES (";
                szöveg += $"'{Adat.Szerszámkönyvszám}', ";
                szöveg += $"'{Adat.Szerszámkönyvnév}', ";
                szöveg += $"'{Adat.Felelős1}', ";
                szöveg += $"'{Adat.Felelős2}', ";
                szöveg += $"{Adat.Státus}) ";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Könyv rögzítés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void Döntés(string Melyik, string Telephely, Adat_Szerszám_Könyvtörzs Adat)
        {
            try
            {
                List<Adat_Szerszám_Könyvtörzs> Adatok = Lista_Adatok(Melyik, Telephely);
                Adat_Szerszám_Könyvtörzs ADAT = (from a in Adatok
                                                 where a.Szerszámkönyvszám == Adat.Szerszámkönyvszám
                                                 select a).FirstOrDefault();
                if (Adat == null) Rögzítés(Melyik, Telephely, Adat);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Könyv rögzítés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosítás(string Melyik, string Telephely, Adat_Szerszám_Könyvtörzs Adat)
        {
            try
            {
                FájlBeállítás(Melyik, Telephely);
                string szöveg = $"UPDATE {táblanév}  SET ";
                szöveg += $"Szerszámkönyvnév='{Adat.Szerszámkönyvnév.Trim()}', ";
                szöveg += $"felelős1='{Adat.Felelős1.Trim()}', ";
                szöveg += $"felelős2='{Adat.Felelős2.Trim()}', ";
                szöveg += $"státus={Adat.Státus} ";
                szöveg += $" WHERE Szerszámkönyvszám='{Adat.Szerszámkönyvszám.Trim()}'";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Könyv módosítás", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

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
    public class Kezelő_TW6000_Alap
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\villamos4TW.mdb";
        readonly string jelszó = "czapmiklós";
        readonly string táblanév = "alap";

        public Kezelő_TW6000_Alap()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.TW6000tábla(hely.KönyvSzerk());
        }

        public List<Adat_TW6000_Alap> Lista_Adatok()
        {
            List<Adat_TW6000_Alap> Adatok = new List<Adat_TW6000_Alap>();
            Adat_TW6000_Alap Adat;
            string szöveg = $"SELECT * FROM {táblanév}";

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
                                Adat = new Adat_TW6000_Alap(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Ciklusrend"].ToStrTrim(),
                                        rekord["Kötöttstart"].ToÉrt_Bool(),
                                        rekord["Megállítás"].ToÉrt_Bool(),
                                        rekord["Start"].ToÉrt_DaTeTime(),
                                        rekord["Vizsgdátum"].ToÉrt_DaTeTime(),
                                        rekord["Vizsgnév"].ToStrTrim(),
                                        rekord["Vizsgsorszám"].ToÉrt_Int());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_TW6000_Alap Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} (azonosító, start, ciklusrend, megállítás, kötöttstart, vizsgsorszám, vizsgnév, vizsgdátum) VALUES (";
                szöveg += $"'{Adat.Azonosító}', ";
                szöveg += $"'{Adat.Start:yyyy.MM.dd}', ";
                szöveg += $"'{Adat.Ciklusrend}', ";
                szöveg += $"{Adat.Megállítás}, ";
                szöveg += $"{Adat.Kötöttstart}, ";
                szöveg += $"{Adat.Vizsgsorszám}, ";
                szöveg += $"'{Adat.Vizsgnév}', ";
                szöveg += $"'{Adat.Vizsgdátum:yyyy.MM.dd}') ";
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

        public void Módosítás(Adat_TW6000_Alap Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $" Start='{Adat.Start:yyyy.MM.dd}', ";
                szöveg += $" ciklusrend='{Adat.Ciklusrend}', ";
                szöveg += $" megállítás={Adat.Megállítás}, ";
                szöveg += $" kötöttstart={Adat.Kötöttstart}, ";
                szöveg += $" vizsgsorszám={Adat.Vizsgsorszám}, ";
                szöveg += $" vizsgnév='{Adat.Vizsgnév}', ";
                szöveg += $" vizsgdátum='{Adat.Vizsgdátum:yyyy.MM.dd}' ";
                szöveg += $" WHERE azonosító='{Adat.Azonosító} '";
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

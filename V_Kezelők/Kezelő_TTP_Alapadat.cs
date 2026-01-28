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
    public class Kezelő_TTP_Alapadat
    {
        readonly string hely = $@"{Application.StartupPath}/Főmérnökség/adatok/TTP/TTP_Adatbázis.mdb";
        readonly string jelszó = "rudolfg";

        public Kezelő_TTP_Alapadat()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.TTP_Adatbázis(hely.KönyvSzerk());
        }

        public List<Adat_TTP_Alapadat> Lista_Adatok()
        {
            List<Adat_TTP_Alapadat> Adatok = new List<Adat_TTP_Alapadat>();
            Adat_TTP_Alapadat Adat;
            string szöveg = $"SELECT * FROM TTP_Alapadat";

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
                                Adat = new Adat_TTP_Alapadat(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Gyártási_Év"].ToÉrt_DaTeTime(),
                                        rekord["TTP"].ToÉrt_Bool(),
                                        rekord["Megjegyzés"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public void Rögzítés(Adat_TTP_Alapadat Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO TTP_Alapadat (Azonosító, Gyártási_Év, TTP, Megjegyzés)";
                szöveg += $"VALUES (";
                szöveg += $"'{Adat.Azonosító}',";
                szöveg += $"'{Adat.Gyártási_Év:yyyy.MM.dd}',";
                szöveg += $"{Adat.TTP},";
                szöveg += $"'{Adat.Megjegyzés}')";
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


        public void Módosítás(Adat_TTP_Alapadat Adat)
        {
            try
            {
                string szöveg = $"UPDATE TTP_Alapadat SET Gyártási_Év='{Adat.Gyártási_Év:yyyy.MM.dd}', ";
                szöveg += $"TTP={Adat.TTP}, ";
                szöveg += $"Megjegyzés='{Adat.Megjegyzés}' ";
                szöveg += $"WHERE Azonosító='{Adat.Azonosító}'";
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

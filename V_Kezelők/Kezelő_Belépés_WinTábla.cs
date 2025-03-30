using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Belépés_WinTábla
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\belépés.mdb".KönyvSzerk();
        readonly string jelszó = "forgalmiutasítás";

        public Kezelő_Belépés_WinTábla()
        {
            // Nincs kidolgozva
            //if (!File.Exists(hely)) Adatbázis_Létrehozás.(hely.KönyvSzerk());
        }

        public List<Adat_Belépés_WinTábla> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM WinTábla";

            List<Adat_Belépés_WinTábla> Adatok = new List<Adat_Belépés_WinTábla>();
            Adat_Belépés_WinTábla Adat;

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
                                Adat = new Adat_Belépés_WinTábla(
                                    rekord["név"].ToStrTrim(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["WinUser"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Belépés_WinTábla Adat)
        {
            try
            {
                string szöveg = "INSERT INTO Wintábla (Név, telephely, WinUser) VALUES ";
                szöveg += $"('{Adat.Név}', ";
                szöveg += $"'{Adat.Telephely}', ";
                szöveg += $"'{Adat.WinUser}')";
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

        public void Módosítás(Adat_Belépés_WinTábla Adat)
        {
            try
            {
                string szöveg = $"UPDATE WinTábla SET ";
                szöveg += $" telephely='{Adat.Telephely}', ";
                szöveg += $" WinUser='{Adat.WinUser}' ";
                szöveg += $" WHERE név='{Adat.Név}'";
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

using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Telep_Kiegészítő_SAP
    {
        readonly string jelszó = "Mocó";
        string hely;
        readonly string táblanév = "sapmunkahely";

        private bool FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\segéd\Kiegészítő.mdb";
            return File.Exists(hely);
            //nincs elkészítve
            // if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Adatok_Napló(hely.KönyvSzerk());
        }
        public List<Adat_Telep_Kiegészítő_SAP> Lista_Adatok(string Telephely)
        { 
            List<Adat_Telep_Kiegészítő_SAP> Adatok = new List<Adat_Telep_Kiegészítő_SAP>();
            if (FájlBeállítás(Telephely))
            {
                string szöveg = $"SELECT * FROM {táblanév}";
            
                Adat_Telep_Kiegészítő_SAP Adat;

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
                                    Adat = new Adat_Telep_Kiegészítő_SAP(
                                                    rekord["Id"].ToÉrt_Long(),
                                                    rekord["Felelősmunkahely"].ToStrTrim());
                                    Adatok.Add(Adat);
                                }
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, Adat_Telep_Kiegészítő_SAP Adat)
        {
            try
            {
                if (FájlBeállítás(Telephely))
                {
                    string szöveg = $"INSERT INTO {táblanév} (id, felelősmunkahely)";
                    szöveg += $"VALUES ({Adat.Id}, ";
                    szöveg += $"'{Adat.Felelősmunkahely}')";
                    MyA.ABMódosítás(hely, jelszó, szöveg);

                }
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

        public void Módosítás(string Telephely, Adat_Telep_Kiegészítő_SAP Adat)
        {
            try
            {
                if (FájlBeállítás(Telephely))
                {
                    string szöveg = $"UPDATE {táblanév} SET ";
                    szöveg += $"felelősmunkahely='{Adat.Felelősmunkahely}'";
                    szöveg += $"WHERE id={Adat.Id}";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }
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

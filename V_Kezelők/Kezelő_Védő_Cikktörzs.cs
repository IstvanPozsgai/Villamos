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
    public class Kezelő_Védő_Cikktörzs
    {
        readonly string jelszó = "csavarhúzó";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Védő\Védőtörzs.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Védőtörzs_készítés(hely.KönyvSzerk());
        }

        public List<Adat_Védő_Cikktörzs> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            List<Adat_Védő_Cikktörzs> Adatok = new List<Adat_Védő_Cikktörzs>();
            Adat_Védő_Cikktörzs Adat;
            string szöveg = $"SELECT * FROM lista ORDER BY azonosító";

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
                                Adat = new Adat_Védő_Cikktörzs(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["Megnevezés"].ToStrTrim(),
                                        rekord["Méret"].ToStrTrim(),
                                        rekord["státus"].ToÉrt_Int(),
                                        rekord["költséghely"].ToStrTrim(),
                                        rekord["Védelem"].ToStrTrim(),
                                        rekord["Kockázat"].ToStrTrim(),
                                        rekord["Szabvány"].ToStrTrim(),
                                        rekord["Szint"].ToStrTrim(),
                                        rekord["Munk_megnevezés"].ToStrTrim()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, Adat_Védő_Cikktörzs Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"INSERT INTO lista  (azonosító, megnevezés, méret,  státus, költséghely, Védelem, Kockázat, Szabvány, Szint, Munk_megnevezés ) VALUES (";
                szöveg += $"'{Adat.Azonosító}', "; //azonosító
                szöveg += $"'{Adat.Megnevezés}', ";  //megnevezés
                szöveg += $"'{Adat.Méret}', "; //méret
                szöveg += $"{Adat.Státus}, "; //státus
                szöveg += $"'{Adat.Költséghely}', "; // költséghely
                szöveg += $"'{Adat.Védelem}', ";
                szöveg += $"'{Adat.Kockázat}', ";
                szöveg += $"'{Adat.Szabvány}', ";
                szöveg += $"'{Adat.Szint}', ";
                szöveg += $"'{Adat.Munk_megnevezés}') ";
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

        public void Módosítás(string Telephely, Adat_Védő_Cikktörzs Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE lista  SET ";
                szöveg += $"megnevezés='{Adat.Megnevezés}', ";
                szöveg += $"méret='{Adat.Méret}', ";
                szöveg += $"költséghely='{Adat.Költséghely}', ";
                szöveg += $"státus={Adat.Státus}, ";
                szöveg += $"védelem='{Adat.Védelem}', ";
                szöveg += $"kockázat='{Adat.Kockázat}', ";
                szöveg += $"szabvány='{Adat.Szabvány}', ";
                szöveg += $"Szint='{Adat.Szint}', ";
                szöveg += $"Munk_Megnevezés='{Adat.Munk_megnevezés}' ";
                szöveg += $" WHERE azonosító='{Adat.Azonosító}'";
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
